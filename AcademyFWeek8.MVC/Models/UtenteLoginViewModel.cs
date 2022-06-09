using System.ComponentModel.DataAnnotations;

namespace AcademyFWeek8.MVC.Models
{
    public class UtenteLoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } 
    }
}
