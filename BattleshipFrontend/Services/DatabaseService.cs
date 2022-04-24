using System;
using System.IO;
using System.Threading.Tasks;
using BattleshipFrontend.Models;
using SQLite;

namespace BattleshipFrontend.Services
{
    public class DatabaseService
    {
        private const SQLiteOpenFlags OpenFlags = SQLiteOpenFlags.Create | 
                                                  SQLiteOpenFlags.ReadWrite | 
                                                  SQLiteOpenFlags.NoMutex;

        private readonly SQLiteAsyncConnection _connection;

        public DatabaseService()
        {
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbPath = Path.Combine(localAppData, "Battleship.db3");
            
            _connection = new SQLiteAsyncConnection(dbPath, OpenFlags);
        }

        public async Task InitializeTablesAsync()
        {
            await _connection.CreateTableAsync<User>();

            var rows = await _connection.Table<User>().CountAsync();

            if (rows == 0) await _connection.InsertAsync(new User { DisplayName = string.Empty });
        }
    }
}