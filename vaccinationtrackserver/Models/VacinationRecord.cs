// Namespace: Groups related classes under the project
using vaccinationtrackserver.Models;
    // VaccinationRecord class: Represents the VaccinationRecords table
    public class VaccinationRecord
    {
        // Key attribute: Marks RecordID as the primary key
        [System.ComponentModel.DataAnnotations.Key]
        // RecordID: Unique identifier for each vaccination record
        public int RecordID { get; set; }

        // Required attribute: Ensures StudentID is not null
        [System.ComponentModel.DataAnnotations.Required]
        // StudentID: Foreign key linking to the Students table
        public int StudentID { get; set; }

        // ForeignKey attribute: Defines the relationship with the Student entity
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("StudentID")]
        // Student: Navigation property for accessing the related Student
        public Student Student { get; set; }

        // Required attribute: Ensures DriveID is not null
        [System.ComponentModel.DataAnnotations.Required]
        // DriveID: Foreign key linking to the VaccinationDrives table
        public int DriveID { get; set; }

        // ForeignKey attribute: Defines the relationship with the VaccinationDrive entity
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("DriveID")]
        // VaccinationDrive: Navigation property for accessing the related VaccinationDrive
        public VaccinationDrive VaccinationDrive { get; set; }

        // Required attribute: Ensures VaccineName is not null
        [System.ComponentModel.DataAnnotations.Required]
        // StringLength: Limits VaccineName to 100 characters
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        // VaccineName: Stores the vaccine name (redundant for consistency)
        public string VaccineName { get; set; }

        // Required attribute: Ensures VaccinationDate is not null
        [System.ComponentModel.DataAnnotations.Required]
        // VaccinationDate: Date the student was vaccinated
        public System.DateTime VaccinationDate { get; set; }

        // CreatedAt: Tracks when the record was created
        public System.DateTime CreatedAt { get; set; } = System.DateTime.UtcNow;
    }