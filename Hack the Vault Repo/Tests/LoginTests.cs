using System;
using Xunit;

namespace FlawedLoginSystem.Tests
{
    public class LoginTests
    {
        private readonly LoginService _loginService;

        public LoginTests()
        {
            _loginService = new LoginService();
        }

        [Fact]
        public void RegisterUser_ShouldStoreHashedPasswordAndSalt()
        {
            string username = "testuser";
            string password = "StrongP@ssw0rd";

            _loginService.RegisterUser(username, password);
            var user = _loginService.GetUser(username);

            Assert.NotNull(user);
            Assert.NotEqual(password, user.PasswordHash); 
            Assert.False(string.IsNullOrEmpty(user.Salt));
        }

        [Fact]
        public void AuthenticateUser_ShouldReturnTrue_ForCorrectCredentials()
        {
            string username = "validuser";
            string password = "ValidPass123!";
            _loginService.RegisterUser(username, password);

            bool result = _loginService.AuthenticateUser(username, password);

            Assert.True(result);
        }

        [Fact]
        public void AuthenticateUser_ShouldReturnFalse_ForIncorrectPassword()
        {
            string username = "invalidpassuser";
            string correctPassword = "MyRealPassword!";
            string wrongPassword = "WrongOne!";
            _loginService.RegisterUser(username, correctPassword);

            bool result = _loginService.AuthenticateUser(username, wrongPassword);

            Assert.False(result);
        }

        [Fact]
        public void AuthenticateUser_ShouldReturnFalse_IfUserDoesNotExist()
        {
            bool result = _loginService.AuthenticateUser("ghostuser", "doesntmatter");

            Assert.False(result);
        }

        [Fact]
        public void AuthenticateUser_ShouldReturnFalse_WhenAccountIsLocked()
        {
            var username = "lockeduser";
            var password = "LockMeIn!";
            _loginService.RegisterUser(username, password);

            var user = _loginService.GetUser(username);
            user.IsLocked = true;

            bool result = _loginService.AuthenticateUser(username, password);

            Assert.False(result);
        }

        [Fact]
        public void MultipleFailedLogins_ShouldLockUser_AfterThreshold()
        {
            // Arrange
            string username = "bruteforceuser";
            string password = "CorrectPass123";
            _loginService.RegisterUser(username, password);

            var user = _loginService.GetUser(username);

            for (int i = 0; i < 5; i++)
            {
                _loginService.AuthenticateUser(username, "WrongPass!");
            }

            Assert.True(user.IsLocked);
        }

        [Fact]
        public void User_ShouldBeAbleToLogin_AfterResettingPassword()
        {
            string username = "resetpassuser";
            string oldPassword = "OldPass!";
            string newPassword = "NewPass123#";

            _loginService.RegisterUser(username, oldPassword);

            _loginService.RegisterUser(username, newPassword); 

            bool result = _loginService.AuthenticateUser(username, newPassword);

            Assert.True(result);
        }
    }
}
