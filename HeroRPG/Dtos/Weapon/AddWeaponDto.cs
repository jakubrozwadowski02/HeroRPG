namespace HeroRPG.Dtos.Weapon
{
    public class AddWeaponDto
    {
        public string Name { get; set; } = String.Empty;
        public int Damage { get; set; }
        public int HeroId { get; set; }
    }
}
