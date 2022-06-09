using System.ComponentModel.DataAnnotations;

namespace AcademyFWeek8.MVC.Models
{
    public class LezioneViewModel
    {
        public int LezioneId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DataOraInizio { get; set; }
        public int Durata { get; set; }
        public string Aula { get; set; }

        //Fk verso docente
        public int DocenteID { get; set; }
        public ICollection<DocenteViewModel>? Docente { get; set; } = new List<DocenteViewModel>();

        //Fk verso Corso
        public string CorsoCodice { get; set; }
        public CorsoViewModel? Corso { get; set; }


        public override string ToString()
        {
            return $"Lezione: {LezioneId}\tData:{DataOraInizio}\t Aula: {Aula}\tDurata (in giorni) : {Durata}";
        }
    }
}
