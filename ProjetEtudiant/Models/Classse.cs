namespace PeojetEtudiant.Models
{
    public class Classse
    {
        public int Id_c { get; set; }
        public string labelle { get; set; }
        public IList<Etudiants> Etudiants { get; set; } = new List<Etudiants>();
    }
}
