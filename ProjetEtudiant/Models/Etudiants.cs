namespace PeojetEtudiant.Models
{
    public class Etudiants
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int Age { get; set; }

        public int ClasseId { get; set; }
        // Navigation property
        public Classse Classe { get; set; }
    }
}
