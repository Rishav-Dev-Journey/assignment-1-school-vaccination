// Namespace: Groups related classes under the project
namespace vaccinationtrackserver.Models
{
    // VaccinationDrive class: Represents the VaccinationDrives table
    public class VaccinationDrive
    {
        // Key attribute: Marks DriveID as the primary key
        [System.ComponentModel.DataAnnotations.Key]
        // DriveID: Unique identifier for each vaccination drive
        public int DriveID { get; set; }

        // Required attribute: Ensures VaccineName is not null
        [System.ComponentModel.DataAnnotations.Required]
        // StringLength: Limits VaccineName to 100 characters
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        // VaccineName: Stores the name of the vaccine (e.g., 'Polio')
        public string VaccineName { get; set; }

        // Required attribute: Ensures DriveDate is not null
        [System.ComponentModel.DataAnnotations.Required]
        // DriveDate: Stores the date of the vaccination drive
        public System.DateTime DriveDate { get; set; }

        // Required attribute: Ensures AvailableDoses is not null
        [System.ComponentModel.DataAnnotations.Required]
        // AvailableDoses: Number of vaccine doses available
        public int AvailableDoses { get; set; }

        // Required attribute: Ensures ApplicableClasses is not null
        [System.ComponentModel.DataAnnotations.Required]
        // StringLength: Limits ApplicableClasses to 100 characters
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        // ApplicableClasses: Classes eligible for the drive (e.g., 'Grade 5-7')
        public string ApplicableClasses { get; set; }

        // CreatedAt: Tracks when the drive was created
        public System.DateTime CreatedAt { get; set; } = System.DateTime.UtcNow;

        // UpdatedAt: Tracks when the drive was last updated
        public System.DateTime UpdatedAt { get; set; } = System.DateTime.UtcNow;
    }
}