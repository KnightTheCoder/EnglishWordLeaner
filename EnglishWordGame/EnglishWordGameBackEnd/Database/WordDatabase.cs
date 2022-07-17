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
            this.connection = new SQLiteConnection("Data Source = " +
                $"{name}.sqlite;Version = 3;");

            if (!File.Exists($"./{name}.sqlite"))
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
                "original_word TEXT," +
                "meaning TEXT," +
                "category_id INTEGER," +
                "FOREIGN KEY(category_id) REFERENCES categories(id) ON UPDATE CASCADE" +
                ");";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            OpenConnection();
            command.ExecuteNonQuery();
            CloseConnection();
        }

        private bool ExecuteCommand(SQLiteCommand command)
        {
            OpenConnection();
            int affectedRows = command.ExecuteNonQuery();
            CloseConnection();

            return affectedRows > 0;
        }

        public bool AddCategory(string name)
        {
            string query = "INSERT INTO categories(name) VALUES (@name)";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);

            return ExecuteCommand(command);
        }

        public bool RemoveCategory(int id)
        {
            string query = "DELETE FROM categories WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            return ExecuteCommand(command);
        }

        public Category GetCategory(int id)
        {
            string query = "SELECT id, name FROM categories WHERE id = @id";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            Category category = new Category();

            OpenConnection();
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
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
                Category category = new Category(reader.GetInt32("id"),
                    reader.GetString("name"));
                categories.Add(category);
            }
            CloseConnection();

            return categories;
        }

        public bool AddWord(string original_word, string meaning, int category_id)
        {
            string query = "INSERT INTO words(original_word, meaning, " +
                "category_id) VALUES (@original_word, @meaning, @category_id)";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@original_word", original_word);
            command.Parameters.AddWithValue("@meaning", meaning);
            command.Parameters.AddWithValue("@category_id", category_id);

            return ExecuteCommand(command);
        }

        public bool RemoveWord(int id)
        {
            string query = "DELETE FROM words WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            return ExecuteCommand(command);
        }

        public Word GetWord(int id)
        {
            string query = "SELECT id, original_word, meaning, category_id " +
                "FROM words WHERE id = @id";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            Word word = new Word();

            OpenConnection();
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                word.ID = reader.GetInt32("id");
                word.OriginalWord = reader.GetString("original_word");
                word.Meaning = reader.GetString("meaning");
                word.CategoryId = reader.GetInt32("category_id");
            }
            CloseConnection();

            return word;
        }

        public List<Word> GetAllWords()
        {
            List<Word> words = new List<Word>();
            string query = "SELECT id, original_word, meaning, category_id " +
                "FROM words";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            OpenConnection();
            SQLiteDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                Word word = new Word(reader.GetInt32("id"),
                    reader.GetString("original_word"),
                    reader.GetString("meaning"),
                    reader.GetInt32("category_id")
                    );
                words.Add(word);
            }
            CloseConnection();

            return words;
        }

    }
}
