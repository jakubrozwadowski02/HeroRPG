namespace HeroRPG.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public Hero Hero { get; set; }
        public int HeroId { get; set; }
    }
}
