
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TA.DAL;
using TA.Services.Interface;

namespace TA.WebApi.Controllers
{
    [Route("api/v1.0/tweets/")]
    [Authorize]
    public class UserController : Controller
    {
        private  readonly IUserService _userService;
        private readonly IConfiguration _config;
   
        public UserController(IUserService userService,IConfiguration configuration)
        {
            _userService = userService;
            _config = configuration;
        }

        // User Registeration
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public JsonResult Register([FromBody] UserModel user)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    string token = GenerateJWT();
                    user.token = token;
                    bool creationStatus = _userService.Register(user);
                    if (creationStatus)
                    {
                        return new JsonResult("User registered successfully");
                    }
                    return new JsonResult("User already exists");
                }
                else
                {
                    return new JsonResult("Please enter all the valid inputs");
                }
               
            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }
            return new JsonResult("Error");
        }

        // User Login
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public JsonResult Login([FromBody] UserLoginModel user)
        {
            IActionResult response = Unauthorized();
            UserModel userInfo = new UserModel();
            try
            {
                if(ModelState.IsValid)
                {
                    var userExist = _userService.Login(user);
                    if (userExist == null)
                    {
                        return new JsonResult("User not found");
                    }
                    else if (userExist != null && (userExist.password == user.password))
                    {
                        var tokenString = GenerateJSONWebToken(user.email);
                        userExist.token = tokenString;
                        return new JsonResult(userExist);
                    }
                    else
                    {
                        return new JsonResult("Username or Password is incorrect");
                    }
                }
                else
                {
                    return new JsonResult("Enter all the valid required fields");   
                }
                
                
            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }
            return new JsonResult("Error");
        }

        // Change Password
        [HttpPut]
        [Route("ChangePassword/{loginId}")]
        public JsonResult ChangePassword(string loginId, [FromBody] ChangePasswordModel changePassword)
        {
            try
            {
                bool valid = _userService.ChangePassword(loginId, changePassword);
                if (valid)
                {
                    return new JsonResult("Password changed successfully");
                }
                return new JsonResult("Old password is incorrect");
            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }
            return new JsonResult("Error");
        }

        private string GenerateJWT()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateJSONWebToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Email, email)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordModel forgotPassword)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new JsonResult("Invalid Request");
                }

                var user = _userService.CheckUserExists(forgotPassword.Email);

                if (user !=  null)
                {
                    var token = GenerateJSONWebToken(forgotPassword.Email);
                   
                    var isTokenGenerater = _userService.SaveToken(user.email,token);
                    if(isTokenGenerater != false)
                    {

                        var param = new Dictionary<string, string>
                        {
                            {"token", token },
                            {"email", forgotPassword.Email }
                        };

                      //  var callback = QueryHelpers.AddQueryString(forgotPassword.ClientURI, param);

                       // var message = new Message(new string[] { forgotPassword.Email }, "Reset password token", callback);

                      //  _emailSender.SendEmail(message);

                        return new JsonResult("Mail Sent");
                    }

                }
            }
            catch(Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }

            return new JsonResult("Invalid Request");
        }


        [HttpPut]
        [Route("ResetPassword")]

        [AllowAnonymous]
        public JsonResult ResetPassword([FromBody] ResetPasswordModel resetPassword)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return new JsonResult("Invalid Request");
                }

                var user = _userService.CheckUserExists(resetPassword.email);
                if (user != null)
                {
                    if(user.token == resetPassword.token)
                    {
                        bool valid = _userService.ResetPassword(resetPassword);

                        if (valid)
                        {
                            return new JsonResult("Password changed successfully");
                        }
                    }
                }
               
            }
            catch(Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }

            return new JsonResult("Invalid Request");
        }


        // Getting User By User Id
        [HttpGet]
        [Route("GetUserById/{id}")]
        public JsonResult GetUserById(string id)
       {
            UserModel userById = new UserModel();
            try
            {
                userById = _userService.GetUserById(id);
                if(userById != null)
                {
                    return new JsonResult(userById);
                }
                return new JsonResult("User not found");
            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }
            return new JsonResult("Error");
        }

        [HttpGet]
        [Route("GetUserByUsername/{username}")]
        public JsonResult GetUserByUsername(string username)
        {
            UserModel userByUserName = new UserModel();
            try
            {
                userByUserName = _userService.GetUserByUsername(username);
                if (userByUserName != null)
                {
                    return new JsonResult(userByUserName);
                }
                return new JsonResult("User not found");
            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }
            return new JsonResult("Error");
        }


        // Getting All Users 
        [Route("users/all")]
        public JsonResult GetAllUsers()
        {
            List<UserModel> allUsers = new List<UserModel>();
            try
            {
                allUsers = _userService.GetAllUsers().ToList();
                if(allUsers != null)
                {
                    return new JsonResult(allUsers);
                }
                return new JsonResult("Users not found");
            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }
            return new JsonResult("Error");
        }

    }
}
