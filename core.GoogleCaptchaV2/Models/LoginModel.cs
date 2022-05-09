using System.ComponentModel.DataAnnotations;

namespace core.GoogleCaptchaV2.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is Required!"), Display(Prompt = "Please enter usernme")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is Required!"),
         DataType(DataType.Password),
         Display(Prompt = "Please enter password")]
        public string Password { get; set; }
    }
}
