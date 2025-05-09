
namespace ITC2Wedstrijd.Models
{
    public class Ploeg 
    {
        public int Id { get; set; }
        public string Naam {  get; set; }
        public int CategorieId { get; set; }
        public Categorie Categorie { get; set;}
        public int ClubId { get; set; }
        public Club Club { get; set; }
        public int SportId {  get; set; }
        public Sport Sport { get; set; }
        public int AantalGewonnen { get; set; }
        public int AantalVerloren { get; set; }
        public int AantalGelijk { get; set; }
        public IEnumerable<Speler> Spelers { get; set; } = new List<Speler>();

        public override string ToString()
        {
            return Naam;
        }
        public override bool Equals(object? obj)
        {
            return obj is Ploeg ploeg && ploeg.Id == Id;
        }
    }
}
