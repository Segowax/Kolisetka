using System.ComponentModel.DataAnnotations;

namespace Kolisetka.MVC.Models.User
{
    public class LoginVM
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
