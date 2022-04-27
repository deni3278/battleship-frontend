namespace BattleshipFrontend.Models
{
    public class User
    {
        public string ConnectionId { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public Room? Room { get; set; }
    }
}