using System;

namespace FlawedLoginSystem
{
    //Represents a user within the authentication system.
    public class User
    {
        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public string Email { get; set; }

        public bool ForcePasswordReset { get; set; }

        public int FailedLoginAttempts { get; set; }

        public bool IsLocked { get; set; }

        public DateTime? LastLogin { get; set; }

        public string Role { get; set; }

        public string TwoFactorSecret { get; set; }

        public override string ToString()
        {
            return $"{Username} ({Role ?? "User"})";
        }
    }
}
