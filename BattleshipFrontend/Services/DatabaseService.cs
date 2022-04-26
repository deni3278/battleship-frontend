using System;
using System.Diagnostics;
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
            Debug.WriteLine("Initializing database.");
            
            await _connection.CreateTableAsync<Self>();

            var rows = await _connection.Table<Self>().CountAsync();

            if (rows == 0) await _connection.InsertAsync(new Self { DisplayName = string.Empty });
        }

        public async Task<string> GetDisplayNameAsync()
        {
            Debug.WriteLine("Retrieving display name from database.");
            
            return (await _connection.Table<Self>().FirstAsync()).DisplayName;
        }

        public async Task<bool> SetDisplayNameAsync(string displayName)
        {
            Debug.WriteLine("Updating display name in database.");
            
            var user = await _connection.Table<Self>().FirstAsync();
            user.DisplayName = displayName;

            return await _connection.UpdateAsync(user) > 0;
        }
    }
}