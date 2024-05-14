using API.Model;

namespace API
{
    public class LoggedUser
    {
        public string? Id { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }

        public LoggedUser(User? user)
        {
            if (user != null)
            {
                Username = user.Username;
                IsAdmin = user.Role == UserRole.Admin;
                Id = user.UserId.ToString();
            }
            else
            {
                Username = "GUEST";
                IsAdmin = false;
                Id = string.Empty;
            }
        }
    }
}