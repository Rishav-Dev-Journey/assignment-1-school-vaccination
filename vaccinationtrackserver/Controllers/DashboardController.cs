namespace vaccinationtrackserver.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using vaccinationtrackserver.Data;
    using vaccinationtrackserver.DTOs;
    using System.Threading.Tasks;
    using System.Linq;

    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/dashboard
        // Retrieves dashboard metrics
        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            // Calculate total students
            var totalStudents = await _context.Students.CountAsync();

            // Calculate vaccinated students
            var vaccinatedStudents = await _context.Students.CountAsync(s => s.VaccinationStatus);

            // Calculate vaccination percentage
            var vaccinationPercentage = totalStudents > 0 ? (double)vaccinatedStudents / totalStudents * 100 : 0;

            // Get upcoming drives (within 30 days)
            var upcomingDrives = await _context.VaccinationDrives
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

            // Create DTO
            var dashboardDTO = new DashboardDTO
            {
                TotalStudents = totalStudents,
                VaccinatedStudents = vaccinatedStudents,
                VaccinationPercentage = vaccinationPercentage,
                UpcomingDrives = upcomingDrives
            };

            // Return 200 OK
            return Ok(dashboardDTO);
        }
    }
}