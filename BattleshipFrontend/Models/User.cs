using SQLite;

namespace BattleshipFrontend.Models
{
    public class User
    {
        [PrimaryKey] public int Id => 1;
        [NotNull] public string DisplayName { get; set; }
    }
}