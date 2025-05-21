namespace vaccinationtrackserver.DTOs
{
    // LoginDTO: Defines the structure for login requests
    public class LoginDTO
    {
        // Username: The coordinator's login username
        public string Username { get; set; }

        // Password: The coordinator's password (plain text for simplicity)
        public string Password { get; set; }
    }
}