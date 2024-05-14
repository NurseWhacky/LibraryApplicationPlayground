namespace API.DTOs
{
    public class UserLoginDTO
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

        public UserLoginDTO(string username = null, string password = null)
        {
            Username = username;
            Password = password;
        }
    }
}