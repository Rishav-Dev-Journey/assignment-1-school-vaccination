namespace vaccinationtrackserver.DTOs
{
    // DashboardDTO: Defines the structure for dashboard metrics
    public class DashboardDTO
    {
        // TotalStudents: Total number of registered students
        public int TotalStudents { get; set; }

        // VaccinatedStudents: Number of vaccinated students
        public int VaccinatedStudents { get; set; }

        // VaccinationPercentage: Percentage of students vaccinated
        public double VaccinationPercentage { get; set; }

        // UpcomingDrives: List of drives within the next 30 days
        public System.Collections.Generic.List<VaccinationDriveDTO> UpcomingDrives { get; set; }
    }
}