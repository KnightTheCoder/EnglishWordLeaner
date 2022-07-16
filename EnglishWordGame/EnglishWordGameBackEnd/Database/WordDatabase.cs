using EnglishWordGameBackEnd.Models;
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
                "name TEXT" +
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
            string query = "INSERT INTO categories(name) VALUES (@name)";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);

            OpenConnection();
            int affectedRows = command.ExecuteNonQuery();
            CloseConnection();
            
            return affectedRows > 0;
        }

        public bool RemoveCategory(int id)
        {
            string query = "DELETE FROM categories WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            OpenConnection();
            int affectedRows = command.ExecuteNonQuery();
            CloseConnection();

            return affectedRows > 0;
        }

        public Category GetCategory(string name)
        {
            Category category = new Category();
            string query = "SELECT id, name FROM categories WHERE name = @name";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);

            OpenConnection();
            SQLiteDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                category.ID = reader.GetInt32("id");
                category.Name = reader.GetString("name");
            }
            CloseConnection();

            return category;
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            string query = "SELECT id, name FROM categories";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            OpenConnection();
            SQLiteDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                Category category = new Category(reader.GetInt32("id"), reader.GetString("name"));
                categories.Add(category);
            }
            CloseConnection();

            return categories;
        }

    }
}
