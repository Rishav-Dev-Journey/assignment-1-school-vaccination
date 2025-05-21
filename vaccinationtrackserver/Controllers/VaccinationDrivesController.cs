namespace vaccinationtrackserver.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using vaccinationtrackserver.Data;
    using vaccinationtrackserver.DTOs;
    using vaccinationtrackserver.Models;
    using System.Threading.Tasks;
    using System.Linq;

    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationDrivesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VaccinationDrivesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/vaccinationdrives
        // Creates a new vaccination drive
        [HttpPost]
        public async Task<IActionResult> CreateDrive([FromBody] VaccinationDriveDTO driveDTO)
        {
            // Validate input
            if (driveDTO == null || string.IsNullOrEmpty(driveDTO.VaccineName) || driveDTO.AvailableDoses < 0)
            {
                return BadRequest("Invalid drive data.");
            }

            // Validate drive date (15 days in advance)
            if (!IsValidDriveDate(driveDTO.DriveDate))
            {
                return BadRequest("Drive must be scheduled at least 15 days in advance.");
            }

            // Map DTO to model
            var drive = new VaccinationDrive
            {
                VaccineName = driveDTO.VaccineName,
                DriveDate = driveDTO.DriveDate,
                AvailableDoses = driveDTO.AvailableDoses,
                ApplicableClasses = driveDTO.ApplicableClasses
            };

            // Add to database
            _context.VaccinationDrives.Add(drive);
            await _context.SaveChangesAsync();

            // Update DTO with generated ID
            driveDTO.DriveID = drive.DriveID;

            // Return 201 Created
            return CreatedAtAction(nameof(GetDrive), new { id = drive.DriveID }, driveDTO);
        }

        // GET: api/vaccinationdrives/{id}
        // Retrieves a drive by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDrive(int id)
        {
            // Find drive by ID
            var drive = await _context.VaccinationDrives.FindAsync(id);

            // Check if drive exists
            if (drive == null)
            {
                return NotFound("Drive not found.");
            }

            // Map to DTO
            var driveDTO = new VaccinationDriveDTO
            {
                DriveID = drive.DriveID,
                VaccineName = drive.VaccineName,
                DriveDate = drive.DriveDate,
                AvailableDoses = drive.AvailableDoses,
                ApplicableClasses = drive.ApplicableClasses
            };

            // Return 200 OK
            return Ok(driveDTO);
        }

        // GET: api/vaccinationdrives
        // Retrieves upcoming drives (within 30 days)
        [HttpGet]
        public async Task<IActionResult> GetUpcomingDrives()
        {
            // Query drives within 30 days
            var drives = await _context.VaccinationDrives
                .Where(d => d.DriveDate <= System.DateTime.UtcNow.AddDays(30))
                .Select(d => new VaccinationDriveDTO
                {
                    DriveID = d.DriveID,
                    VaccineName = d.VaccineName,
                    DriveDate = d.DriveDate,
                    AvailableDoses = d.AvailableDoses,
                    ApplicableClasses = d.ApplicableClasses
                })
                .ToListAsync();

            // Return 200 OK
            return Ok(drives);
        }

        // PUT: api/vaccinationdrives/{id}
        // Updates a drive
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDrive(int id, [FromBody] VaccinationDriveDTO driveDTO)
        {
            // Validate input
            if (driveDTO == null || driveDTO.DriveID != id)
            {
                return BadRequest("Invalid drive data.");
            }

            // Find existing drive
            var drive = await _context.VaccinationDrives.FindAsync(id);
            if (drive == null)
            {
                return NotFound("Drive not found.");
            }

            // Check if drive is in the past
            if (drive.DriveDate < System.DateTime.UtcNow)
            {
                return BadRequest("Cannot edit past drives.");
            }

            // Validate drive date
            if (!IsValidDriveDate(driveDTO.DriveDate))
            {
                return BadRequest("Drive must be scheduled at least 15 days in advance.");
            }

            // Update fields
            drive.VaccineName = driveDTO.VaccineName;
            drive.DriveDate = driveDTO.DriveDate;
            drive.AvailableDoses = driveDTO.AvailableDoses;
            drive.ApplicableClasses = driveDTO.ApplicableClasses;
            drive.UpdatedAt = System.DateTime.UtcNow;

            // Save changes
            await _context.SaveChangesAsync();

            // Return 200 OK
            return Ok(driveDTO);
        }

        // Static helper method: Validates drive date
        private static bool IsValidDriveDate(System.DateTime driveDate)
        {
            // Check if date is at least 15 days in the future
            return driveDate >= System.DateTime.UtcNow.AddDays(15);
        }
    }
}