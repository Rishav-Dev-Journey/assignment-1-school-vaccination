namespace vaccinationtrackserver.DTOs
{
    // StudentDTO: Defines the structure for student-related API requests/responses
    public class StudentDTO
    {
        // StudentID: Unique identifier for the student
        public int StudentID { get; set; }

        // Name: Student's full name
        public string Name { get; set; }

        // Class: Student's class (e.g., 'Grade 5')
        public string Class { get; set; }

        // DateOfBirth: Student's date of birth (optional)
        public System.DateTime? DateOfBirth { get; set; }

        // VaccinationStatus: Indicates if the student is vaccinated
        public bool VaccinationStatus { get; set; }
    }
}