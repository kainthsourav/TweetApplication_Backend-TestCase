using System.ComponentModel.DataAnnotations;

namespace TA.DAL
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }

        public string email { get; set; }
        public string token { get; set; }
    }
}
