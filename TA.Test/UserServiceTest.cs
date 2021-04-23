using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TA.DAL;
using TA.Repository.Interface;
using TA.Services.Implementation;
using TA.Services.Interface;

namespace TA.Test
{
    [TestFixture]
   public class UserServiceTest
    {
        private IUserService _userService;
        private IUserRepository _userRepository;

        [SetUp]
        public void Setup()
        {
            _userService = new UserService(_userRepository);
        }

        [TearDown]
        public void TearDown()
        {
            _userService = null;
        }

        [Test]
        public void ShouldCheckUserExists(string username)
        {
            var result = _userService.CheckUserExists(username);

            Assert.That(result.username.Equals(username));
        }

        [Test]
        public void ShouldRegister()
        {
            UserModel user = new UserModel();

            var result = _userService.Register(user);

            Assert.IsTrue(result);

        }

        [Test]
        public void ShouldLogin(string loginid,string password)
        {
            UserLoginModel login = new UserLoginModel();
            login.email = loginid;
            login.password = password;

            var result = _userService.Login(login);

            Assert.IsTrue(result != null);
        }

        [Test]
        public void ShouldSaveToken(string email, string token)
        {
            var result = _userService.SaveToken(email, token);

            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldChangePassword(string loginId,string password,string newpassword)
        {
            string login = loginId;
            ChangePasswordModel changePassword = new ChangePasswordModel();
            changePassword.password = password;
            changePassword.newPassword = newpassword;

            var result = _userService.ChangePassword(loginId, changePassword);

            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldResetPassword(string loginid,string password)
        {
            ResetPasswordModel resetPassword = new ResetPasswordModel();
            resetPassword.email = loginid;
            resetPassword.password = password;

            var result = _userService.ResetPassword(resetPassword);

            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldGetAllUsers()
        {
            var result = _userService.GetAllUsers();

            Assert.That(result.Count != 0);
        }

        [Test]
        public void ShouldGetUserById(string userId)
        {

            var result = _userService.GetUserById(userId);

            Assert.That(result.userId.Equals(userId));
        }

        [Test]
        public void ShouldGetUserByUsername(string username)
        {

            var result = _userService.GetUserByUsername(username);

            Assert.That(result != null);
        }

    }
}
