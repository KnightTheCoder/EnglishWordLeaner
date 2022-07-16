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
                Category category = new Category(reader.GetInt32("id"),
                    reader.GetString("name"));
                categories.Add(category);
            }
            CloseConnection();

            return categories;
        }

        public bool AddWord(string english_word, string hungarian_word, int category_id)
        {
            string query = "INSERT INTO words(english_word, hungarian_word, " +
                "category_id) VALUES (@english_word, @hungarian_word, @category_id)";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@english_word", english_word);
            command.Parameters.AddWithValue("@hungarian_word", hungarian_word);
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

        public Word GetWord(string english_word)
        {
            Word word = new Word();
            string query = "SELECT id, english_word, hungarian_word, category_id " +
                "FROM words WHERE english_word = @english_word";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@english_word", english_word);

            OpenConnection();
            SQLiteDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                word.ID = reader.GetInt32("id");
                word.EnglishWord = reader.GetString("english_word");
                word.HungarianWord = reader.GetString("hungarian_word");
                word.CategoryId = reader.GetInt32("category_id");
            }
            CloseConnection();

            return word;
        }

        public List<Word> GetAllWords()
        {
            List<Word> words = new List<Word>();
            string query = "SELECT id, english_word, hungarian_word, category_id " +
                "FROM words";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            OpenConnection();
            SQLiteDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                Word word = new Word(reader.GetInt32("id"),
                    reader.GetString("english_word"),
                    reader.GetString("hungarian_word"),
                    reader.GetInt32("category_id")
                    );
                words.Add(word);
            }
            CloseConnection();

            return words;
        }

    }
}
