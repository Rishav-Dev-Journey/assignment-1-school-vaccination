// Namespace: Groups related classes under the project
namespace vaccinationtrackserver.Models
{
    // Student class: Represents the Students table
    public class Student
    {
        // Key attribute: Marks StudentID as the primary key
        [System.ComponentModel.DataAnnotations.Key]
        // StudentID: Unique identifier for each student
        public int StudentID { get; set; }

        // Required attribute: Ensures Name is not null
        [System.ComponentModel.DataAnnotations.Required]
        // StringLength: Limits Name to 100 characters
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        // Name: Stores the student's full name
        public string Name { get; set; }

        // Required attribute: Ensures Class is not null
        [System.ComponentModel.DataAnnotations.Required]
        // StringLength: Limits Class to 10 characters
        [System.ComponentModel.DataAnnotations.StringLength(10)]
        // Class: Stores the student's class (e.g., 'Grade 5')
        public string Class { get; set; }

        // DateOfBirth: Optional field for student's date of birth
        public System.DateTime? DateOfBirth { get; set; }

        // VaccinationStatus: Indicates if the student is vaccinated (default: false)
        public bool VaccinationStatus { get; set; } = false;

        // CreatedAt: Tracks when the student record was created
        public System.DateTime CreatedAt { get; set; } = System.DateTime.UtcNow;

        // UpdatedAt: Tracks when the student record was last updated
        public System.DateTime UpdatedAt { get; set; } = System.DateTime.UtcNow;
    }
}