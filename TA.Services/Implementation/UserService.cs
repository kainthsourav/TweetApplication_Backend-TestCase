using System;
using System.Collections.Generic;
using TA.DAL;
using TA.Repository.Interface;
using TA.Services.Interface;

namespace TA.Services.Implementation
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserModel CheckUserExists(string username)
        {
            UserModel user = new UserModel();
            try
            {
                if (username.Contains("@"))
                {
                    user = _userRepository.FindByCondition(x => x.email.Equals(username));
                }
                else
                {
                    user = _userRepository.FindByCondition(x => x.username.Equals(username));
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return user;
        }

        public bool Register(UserModel user)
        {
            UserModel isUserExists = new UserModel();
            bool isCreated = false;
            try
            {
                isUserExists = CheckUserExists(user.email);
                if (isUserExists == null)
                {
                    user.createdAt = DateTime.Now;
                    isCreated = _userRepository.Create(user);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return isCreated;
        }

        public UserModel Login(UserLoginModel user)
        {
            UserModel saveUser = new UserModel();
            try
            {
                if (user.email.Contains("@"))
                {
                    saveUser = CheckUserExists(user.email);
                }
                else
                {
                    saveUser = CheckUserExists(user.email);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return saveUser;
        }

        public bool SaveToken(string email, string token)
        {
            UserModel user = new UserModel();
            try
            {
                user = CheckUserExists(user.email);
                if (user != null)
                {
                    user.token = token;
                    _userRepository.Update(user);
                    return true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
            return false;
        }

        // Change Password
        public bool ChangePassword(string loginId, ChangePasswordModel changePassword)
        {
            UserModel isUserExists = new UserModel();
            try
            {
                isUserExists = _userRepository.FindByCondition(x => x.username.Equals(loginId) &&
                x.password.Equals(changePassword.password));

                if (isUserExists != null)
                {
                    isUserExists.password = changePassword.newPassword;
                    _userRepository.Update(isUserExists);
                    return true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
            return false;
        }

        // Reset Password
        public bool ResetPassword(ResetPasswordModel resetPassword)
        {
            UserModel isUserExists = new UserModel();
            try
            {
                isUserExists = CheckUserExists(resetPassword.email);
                if (isUserExists != null)
                {
                    isUserExists.password = resetPassword.password;
                    isUserExists.updatedAt = DateTime.Now;
                    _userRepository.Update(isUserExists);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return false;
        }

        public List<UserModel> GetAllUsers()
        {
            return _userRepository.FindAll();
        }

        public UserModel GetUserById(string userId)
        {
            UserModel user = new UserModel();
            try
            {
                user = _userRepository.FindByCondition(x => x.userId.Equals(userId));
            }
            catch (Exception)
            {
                throw;
            }

            return user;
        }

        public UserModel GetUserByUsername(string username)
        {
            UserModel userModels = new UserModel();
            userModels = _userRepository.FindByCondition(x => x.username.Contains(username));
            return userModels;
        }
     

    }

}
