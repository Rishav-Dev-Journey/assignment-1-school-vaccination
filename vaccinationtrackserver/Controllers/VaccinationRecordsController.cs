namespace vaccinationtrackserver.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using vaccinationtrackserver.Data;
    using vaccinationtrackserver.DTOs;
    using vaccinationtrackserver.Models;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationRecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VaccinationRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/vaccinationrecords
        // Marks a student as vaccinated
        [HttpPost]
        public async Task<IActionResult> AddVaccinationRecord([FromBody] VaccinationRecordDTO recordDTO)
        {
            // Validate input
            if (recordDTO == null || recordDTO.StudentID <= 0 || recordDTO.DriveID <= 0)
            {
                return BadRequest("Invalid vaccination record data.");
            }

            // Check if student exists
            var student = await _context.Students.FindAsync(recordDTO.StudentID);
            if (student == null)
            {
                return NotFound("Student not found.");
            }

            // Check if drive exists
            var drive = await _context.VaccinationDrives.FindAsync(recordDTO.DriveID);
            if (drive == null)
            {
                return NotFound("Drive not found.");
            }

            // Check if student is already vaccinated for this vaccine
            var existingRecord = await _context.VaccinationRecords
                .FirstOrDefaultAsync(r => r.StudentID == recordDTO.StudentID && r.VaccineName == recordDTO.VaccineName);
            if (existingRecord != null)
            {
                return BadRequest("Student already vaccinated for this vaccine.");
            }

            // Map DTO to model
            var record = new VaccinationRecord
            {
                StudentID = recordDTO.StudentID,
                DriveID = recordDTO.DriveID,
                VaccineName = recordDTO.VaccineName,
                VaccinationDate = recordDTO.VaccinationDate
            };

            // Update student's vaccination status
            student.VaccinationStatus = true;
            student.UpdatedAt = System.DateTime.UtcNow;

            // Add to database
            _context.VaccinationRecords.Add(record);
            await _context.SaveChangesAsync();

            // Update DTO with generated ID
            recordDTO.RecordID = record.RecordID;

            // Return 201 Created
            return CreatedAtAction(nameof(GetVaccinationRecord), new { id = record.RecordID }, recordDTO);
        }

        // GET: api/vaccinationrecords/{id}
        // Retrieves a vaccination record by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVaccinationRecord(int id)
        {
            // Find record by ID
            var record = await _context.VaccinationRecords.FindAsync(id);

            // Check if record exists
            if (record == null)
            {
                return NotFound("Vaccination record not found.");
            }

            // Map to DTO
            var recordDTO = new VaccinationRecordDTO
            {
                RecordID = record.RecordID,
                StudentID = record.StudentID,
                DriveID = record.DriveID,
                VaccineName = record.VaccineName,
                VaccinationDate = record.VaccinationDate
            };

            // Return 200 OK
            return Ok(recordDTO);
        }
    }
}