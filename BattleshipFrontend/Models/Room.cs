namespace BattleshipFrontend.Models
{
    public class Room
    {
        public string Name { get; set; }
        public User Owner { get; set; }
        public User? Opponent { get; set; }
    }
}