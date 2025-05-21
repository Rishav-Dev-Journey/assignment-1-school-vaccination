namespace vaccinationtrackserver.DTOs
{
    // VaccinationDriveDTO: Defines the structure for vaccination drive API requests/responses
    public class VaccinationDriveDTO
    {
        // DriveID: Unique identifier for the vaccination drive
        public int DriveID { get; set; }

        // VaccineName: Name of the vaccine (e.g., 'Polio')
        public string VaccineName { get; set; }

        // DriveDate: Date of the vaccination drive
        public System.DateTime DriveDate { get; set; }

        // AvailableDoses: Number of available vaccine doses
        public int AvailableDoses { get; set; }

        // ApplicableClasses: Classes eligible for the drive (e.g., 'Grade 5-7')
        public string ApplicableClasses { get; set; }
    }
}