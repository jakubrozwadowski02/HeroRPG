namespace HeroRPG.Dtos.Fight
{
    public class AttackResultDto
    {
        public string Attacker { get; set; } = string.Empty;
        public string Opponent { get; set; } = string.Empty;
        public int AttackertHP { get; set; }
        public int OpponentHP { get; set; }
        public int Damage { get; set; }
    }
}
