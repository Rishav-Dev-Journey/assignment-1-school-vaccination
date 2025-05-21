namespace vaccinationtrackserver.DTOs
{
    // VaccinationRecordDTO: Defines the structure for vaccination record API requests/responses
    public class VaccinationRecordDTO
    {
        // RecordID: Unique identifier for the vaccination record
        public int RecordID { get; set; }

        // StudentID: ID of the vaccinated student
        public int StudentID { get; set; }

        // DriveID: ID of the vaccination drive
        public int DriveID { get; set; }

        // VaccineName: Name of the vaccine administered
        public string VaccineName { get; set; }

        // VaccinationDate: Date the vaccination occurred
        public System.DateTime VaccinationDate { get; set; }
    }
}