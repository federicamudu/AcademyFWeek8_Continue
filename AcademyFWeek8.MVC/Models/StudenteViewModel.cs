using System.ComponentModel.DataAnnotations;

namespace AcademyFWeek8.MVC.Models
{
    public class StudenteViewModel : PersonaViewModel
    {
        [Required]
        public string TitoloStudio { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataNascita { get; set; }

        //Fk verso Corso
        [Required]
        public string CorsoCodice { get; set; }
        public CorsoViewModel? Corso { get; set; }


        public override string ToString()
        {
            return base.ToString() + $"\tnato il {DataNascita.ToShortDateString()} \t Tipolo di Studio: {TitoloStudio}";
        }
    }
}
