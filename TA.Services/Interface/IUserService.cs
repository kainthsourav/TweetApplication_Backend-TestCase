using System.Collections.Generic;
using TA.DAL;

namespace TA.Services.Interface
{
   public interface IUserService
    {

        UserModel CheckUserExists(string username);
        bool Register(UserModel userModel);
        UserModel Login(UserLoginModel user);
        bool SaveToken(string email, string token);
        bool ChangePassword(string loginId, ChangePasswordModel changePassword);
        bool ResetPassword(ResetPasswordModel resetPassword);
        List<UserModel> GetAllUsers();
        UserModel GetUserById(string userId);
        UserModel GetUserByUsername(string username);

    }
}
