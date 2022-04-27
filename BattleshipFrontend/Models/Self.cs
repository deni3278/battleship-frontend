using SQLite;

namespace BattleshipFrontend.Models
{
    public class Self
    {
        [PrimaryKey] public int Id { get; set; } = 1;
        [NotNull] public string DisplayName { get; set; } = null!;
    }
}