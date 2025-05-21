namespace vaccinationtrackserver.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using vaccinationtrackserver.Data;
    using vaccinationtrackserver.DTOs;
    using vaccinationtrackserver.Models;
    using CsvHelper;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/students
        // Adds a new student
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentDTO studentDTO)
        {
            // Validate input
            if (studentDTO == null || string.IsNullOrEmpty(studentDTO.Name) || string.IsNullOrEmpty(studentDTO.Class))
            {
                return BadRequest("Invalid student data.");
            }

            // Map DTO to model
            var student = new Student
            {
                Name = studentDTO.Name,
                Class = studentDTO.Class,
                DateOfBirth = studentDTO.DateOfBirth,
                VaccinationStatus = studentDTO.VaccinationStatus
            };

            // Add to database
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            // Update DTO with generated ID
            studentDTO.StudentID = student.StudentID;

            // Return 201 Created with the created resource
            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentID }, studentDTO);
        }

        // GET: api/students/{id}
        // Retrieves a student by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            // Find student by ID
            var student = await _context.Students.FindAsync(id);

            // Check if student exists
            if (student == null)
            {
                return NotFound("Student not found.");
            }

            // Map to DTO
            var studentDTO = new StudentDTO
            {
                StudentID = student.StudentID,
                Name = student.Name,
                Class = student.Class,
                DateOfBirth = student.DateOfBirth,
                VaccinationStatus = student.VaccinationStatus
            };

            // Return 200 OK
            return Ok(studentDTO);
        }

        // GET: api/students
        // Retrieves students with optional search filter
        [HttpGet]
        public async Task<IActionResult> GetStudents([FromQuery] string? search)
        {
            // Start with all students
            var query = _context.Students.AsQueryable();

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.Name.Contains(search) || s.Class.Contains(search));
            }

            // Map to DTOs
            var students = await query
                .Select(s => new StudentDTO
                {
                    StudentID = s.StudentID,
                    Name = s.Name,
                    Class = s.Class,
                    DateOfBirth = s.DateOfBirth,
                    VaccinationStatus = s.VaccinationStatus
                })
                .ToListAsync();

            // Return 200 OK
            return Ok(students);
        }

        // POST: api/students/bulk
        // Uploads students via CSV
        [HttpPost("bulk")]
        public async Task<IActionResult> BulkUpload([FromForm] Microsoft.AspNetCore.Http.IFormFile file)
        {
            // Validate file
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Validate file extension
            if (!System.IO.Path.GetExtension(file.FileName).Equals(".csv", System.StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Only CSV files are supported.");
            }

            // Read CSV
            using (var stream = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(stream, System.Globalization.CultureInfo.InvariantCulture))
            {
                // Map CSV to StudentDTO
                var studentDTOs = csv.GetRecords<StudentDTO>().ToList();

                // Validate data
                if (!studentDTOs.Any())
                {
                    return BadRequest("CSV file is empty.");
                }

                // Convert DTOs to models
                var students = studentDTOs.Select(dto => new Student
                {
                    Name = dto.Name,
                    Class = dto.Class,
                    DateOfBirth = dto.DateOfBirth,
                    VaccinationStatus = dto.VaccinationStatus
                }).ToList();

                // Add to database
                _context.Students.AddRange(students);
                await _context.SaveChangesAsync();

                // Return 200 OK
                return Ok(new { Message = $"{students.Count} students uploaded successfully." });
            }
        }

        // Static helper method: Validates student data
        // Static because it doesn't depend on instance state
        private static bool IsValidStudent(StudentDTO studentDTO)
        {
            // Check for required fields
            return !string.IsNullOrEmpty(studentDTO.Name) && !string.IsNullOrEmpty(studentDTO.Class);
        }
    }
}