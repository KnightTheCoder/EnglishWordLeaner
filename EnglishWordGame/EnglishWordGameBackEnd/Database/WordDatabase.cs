using System.Data;
using System.Data.SQLite;

namespace EnglishWordGameBackEnd.Database
{
    public class WordDatabase
    {
        private SQLiteConnection connection;
        private string name;

        public WordDatabase()
        {
            this.name = "worddatabase";
            InitializeWordDatabase();
        }

        public WordDatabase(string name)
        {
            this.name = name;
            InitializeWordDatabase();
        }

        public void InitializeWordDatabase()
        {
            this.connection = new SQLiteConnection($"Data Source = {name}.sqlite;Version = 3;");

            if (!File.Exists($"{name}.sqlite"))
                SQLiteConnection.CreateFile($"{name}.sqlite");

            CreateTables();
        }

        private void OpenConnection()
        {
            if (this.connection.State != ConnectionState.Open)
                this.connection.Open();
        }

        private void CloseConnection()
        {
            if (this.connection.State != ConnectionState.Closed)
                this.connection.Close();
        }
        
        private void CreateTables()
        {
            string query = "CREATE TABLE IF NOT EXISTS categories (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "category TEXT" +
                ");"
                +
                "CREATE TABLE IF NOT EXISTS words (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "english_word TEXT," +
                "hungarian_word TEXT," +
                "category_id TEXT," +
                "FOREIGN KEY(category_id) REFERENCES categories(id) ON UPDATE CASCADE" +
                ");";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            OpenConnection();
            command.ExecuteNonQuery();
            CloseConnection();
        }

        public bool AddCategory(string name)
        {
            string query = "INSERT INTO categories(category) VALUES (@category)";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@category", name);

            OpenConnection();
            int affectedRows = command.ExecuteNonQuery();
            CloseConnection();
            
            return affectedRows > 0;
        }
    }
}
