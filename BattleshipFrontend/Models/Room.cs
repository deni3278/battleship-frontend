namespace BattleshipFrontend.Models
{
    public class Room
    {
        public string Name { get; set; } = null!;
        public User Owner { get; set; } = null!;
        public User? Opponent { get; set; }
        public bool IsOwnerReady { get; set; }
        public bool IsOpponentReady { get; set; }
    }
}