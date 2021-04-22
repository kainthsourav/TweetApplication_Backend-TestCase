using System.ComponentModel.DataAnnotations;

namespace TA.DAL
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }


        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
    }
}