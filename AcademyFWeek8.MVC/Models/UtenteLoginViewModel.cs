using AcademyFWeek8.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace AcademyFWeek8.MVC.Models
{
    public class UtenteLoginViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
        public Ruolo Ruolo { get; set; } = Ruolo.User;
    }
}
