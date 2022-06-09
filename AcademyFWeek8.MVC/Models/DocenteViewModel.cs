namespace AcademyFWeek8.MVC.Models
{
    public class DocenteViewModel : PersonaViewModel
    {
        public string Telefono { get; set; }
        public ICollection<LezioneViewModel> Lezioni { get; set; } = new List<LezioneViewModel>();

        public override string ToString()
        {
            return base.ToString() + $"\t tel.{Telefono}";
        }
    }
}
