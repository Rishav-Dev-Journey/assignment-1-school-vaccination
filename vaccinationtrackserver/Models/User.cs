// Namespace: Groups related classes under the project
namespace vaccinationtrackserver.Models
{
    // User class: Represents the Users table for authentication
    public class User
    {
        // Key attribute: Marks UserID as the primary key
        [System.ComponentModel.DataAnnotations.Key]
        // UserID: Unique identifier for each user
        public int UserID { get; set; }

        // Required attribute: Ensures Username is not null
        [System.ComponentModel.DataAnnotations.Required]
        // StringLength: Limits Username to 50 characters
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        // Username: Stores the coordinator's login username
        public string Username { get; set; }

        // Required attribute: Ensures PasswordHash is not null
        [System.ComponentModel.DataAnnotations.Required]
        // StringLength: Limits PasswordHash to 255 characters
        [System.ComponentModel.DataAnnotations.StringLength(255)]
        // PasswordHash: Stores the password (plain text for simplicity, per assignment)
        public string PasswordHash { get; set; }

        // StringLength: Limits Role to 20 characters
        [System.ComponentModel.DataAnnotations.StringLength(20)]
        // Role: Defines user role (e.g., 'Coordinator'), defaults to 'Coordinator'
        public string Role { get; set; } = "Coordinator";

        // CreatedAt: Tracks when the user was created
        public System.DateTime CreatedAt { get; set; } = System.DateTime.UtcNow;
    }
}