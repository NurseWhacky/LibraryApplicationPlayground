﻿using API.Model;

namespace API
{
    public class LoggedUser
    {
        public string Username { get; set; }
        public bool IsAdmin { get; set; }

        public LoggedUser(User user)
        {
            Username = user.Username;
            IsAdmin = user.Role == UserRole.Admin;
        }
    }
}