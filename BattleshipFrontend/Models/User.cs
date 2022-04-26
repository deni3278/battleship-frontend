namespace BattleshipFrontend.Models
{
    public class User
    {
        public string ConnectionId { get; set; }
        public string DisplayName { get; set; }
        public Room? Room { get; set; }
    }
}