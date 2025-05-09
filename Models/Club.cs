
namespace ITC2Wedstrijd.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string Naam { get; set; }

        public ICollection<Ploeg> Ploegen { get; set; } = new List<Ploeg>();
        public override bool Equals(object obj)
        {
            return obj is Club club && Id == club.Id;
        }
    }
}
