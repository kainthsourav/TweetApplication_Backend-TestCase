using System.ComponentModel.DataAnnotations;

namespace TA.DAL
{
    public class ForgotPasswordModel
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string ClientURI { get; set; }
    }
}
