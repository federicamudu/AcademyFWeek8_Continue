using System.ComponentModel.DataAnnotations;

namespace AcademyFWeek8.MVC.Models
{
    public class CorsoViewModel
    {
        [Required(ErrorMessage = "Campo Obbligatorio")]
        public string CorsoCodice { get; set; }
        [Required(ErrorMessage = "Campo Obbligatorio")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Obbligatorio")]
        public string Descrizione { get; set; }
        public ICollection<StudenteViewModel> Studenti { get; set; } = new List<StudenteViewModel>();

        public override string ToString()
        {
            return $"{CorsoCodice}\t{Nome}\t{Descrizione}";
        }
    }
}
