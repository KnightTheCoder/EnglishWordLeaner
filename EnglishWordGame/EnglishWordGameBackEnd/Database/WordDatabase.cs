using EnglishWordGameBackEnd.Models;
using System.Data;
using System.Data.SQLite;

namespace EnglishWordGameBackEnd.Database
{
    /// <summary>
    /// Represents a database of words
    /// </summary>
    public class WordDatabase
    {
        /// <summary>
        /// SQLite connection to the database
        /// </summary>
        private SQLiteConnection connection;

        /// <summary>
        /// The name of the database
        /// </summary>
        private readonly string name;


        /// <summary>
        /// Initializes a <see cref="WordDatabase"/> with the default name 'worddatabase'
        /// </summary>
        public WordDatabase()
        {
            this.name = "worddatabase";
            InitializeWordDatabase();
        }

        /// <summary>
        /// Initializes a <see cref="WordDatabase"/> with the given name
        /// </summary>
        public WordDatabase(string name)
        {
            this.name = name;
            InitializeWordDatabase();
        }


        /// <summary>
        /// Initializes the <see cref="SQLiteConnection"/>,
        /// creates the database file,
        /// and creates the tables
        /// </summary>
        private void InitializeWordDatabase()
        {
            this.connection = new SQLiteConnection("Data Source = " +
                $"{name}.sqlite;Version = 3;");

            if (!File.Exists($"./{name}.sqlite"))
                SQLiteConnection.CreateFile($"{name}.sqlite");

            CreateTables();
        }


        /// <summary>
        /// Opens a <see cref="SQLiteConnection"/> if it isn't open
        /// </summary>
        private void OpenConnection()
        {
            if (this.connection.State != ConnectionState.Open)
                this.connection.Open();
        }

        /// <summary>
        /// Closes a <see cref="SQLiteConnection"/> if it isn't closed
        /// </summary>
        private void CloseConnection()
        {
            if (this.connection.State != ConnectionState.Closed)
                this.connection.Close();
        }
        
        /// <summary>
        /// Creates the languages, categories and words tables
        /// </summary>
        private void CreateTables()
        {
            string query = "CREATE TABLE IF NOT EXISTS languages (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "name TEXT" +
                ");"
                +
                "CREATE TABLE IF NOT EXISTS categories (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "name TEXT" +
                ");"
                +
                "CREATE TABLE IF NOT EXISTS words (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "original_word TEXT," +
                "meaning TEXT," +
                "category_id INTEGER," +
                "language_id INTEGER," +
                "FOREIGN KEY(category_id) REFERENCES categories(id) ON UPDATE CASCADE," +
                "FOREIGN KEY(language_id) REFERENCES languages(id) ON UPDATE CASCADE" +
                ");";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            OpenConnection();
            command.ExecuteNonQuery();
            CloseConnection();
        }

        /// <summary>
        /// Executes the given command
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <returns>true if it succeeded, false if it failed</returns>
        private bool ExecuteCommand(SQLiteCommand command)
        {
            OpenConnection();
            int affectedRows = command.ExecuteNonQuery();
            CloseConnection();

            return affectedRows > 0;
        }

        /// <summary>
        /// Adds a language to the database
        /// </summary>
        /// <param name="name">The name of the language</param>
        /// <returns>true if it succeeded, false if it failed</returns>
        public bool AddLanguage(string name)
        {
            string query = "INSERT INTO languages(name) VALUES (@name)";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);

            return ExecuteCommand(command);
        }

        /// <summary>
        /// Removes a language from the database
        /// </summary>
        /// <param name="id">The id of the language</param>
        /// <returns>true if it succeeded, false if it failed</returns>
        public bool RemoveLanguage(int id)
        {
            string query = "DELETE FROM languages WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            return ExecuteCommand(command);
        }

        /// <summary>
        /// Modifies a language's properties in the database
        /// </summary>
        /// <param name="id">The id of the language to modify</param>
        /// <param name="name">The name to change the language to</param>
        /// <returns>true if it succeeded, false if it failed</returns>
        public bool ModifyLanguage(int id, string name)
        {
            string query = "UPDATE languages SET name = @name WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@id", id);

            return ExecuteCommand(command);
        }

        /// <summary>
        /// Gets a language from the database
        /// </summary>
        /// <param name="id">The id of the language to get</param>
        /// <returns>A <see cref="Language"/> object</returns>
        public Language GetLanguage(int id)
        {
            string query = "SELECT id, name FROM languages WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            Language language = new Language();

            OpenConnection();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    language.ID = reader.GetInt32("id");
                    language.Name = reader.GetString("name");
                }
            }
            CloseConnection();

            return language;
        }
        
        /// <summary>
        /// Gets all languages in the database
        /// </summary>
        /// <returns>A list of <see cref="Language"/> objects</returns>
        public List<Language> GetAllLanguages()
        {
            List<Language> languages = new List<Language>();
            string query = "SELECT id, name FROM languages";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            OpenConnection();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Language language = new Language(reader.GetInt32("id"),
                        reader.GetString("name"));
                    languages.Add(language);
                }
            }
            CloseConnection();

            return languages;
        }

        /// <summary>
        /// Adds a category to the database
        /// </summary>
        /// <param name="name">The name of the category</param>
        /// <returns>true if it succeeded, false if it failed</returns>
        public bool AddCategory(string name)
        {
            string query = "INSERT INTO categories(name) VALUES (@name)";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);

            return ExecuteCommand(command);
        }

        /// <summary>
        /// Removes a category from the database
        /// </summary>
        /// <param name="id">The id of the category</param>
        /// <returns>true if it succeeded, false if it failed</returns>
        public bool RemoveCategory(int id)
        {
            string query = "DELETE FROM categories WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            return ExecuteCommand(command);
        }

        /// <summary>
        /// Modifies a category's properties in the database
        /// </summary>
        /// <param name="id">The id of the category to modify</param>
        /// <param name="name">The name to change the category to</param>
        /// <returns>true if it succeeded, false if it failed</returns>
        public bool ModifyCategory(int id, string name)
        {
            string query = "UPDATE categories SET name = @name WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@id", id);

            return ExecuteCommand(command);
        }

        /// <summary>
        /// Gets a category from the database
        /// </summary>
        /// <param name="id">The id of the category to get</param>
        /// <returns>A <see cref="Category"/> object</returns>
        public Category GetCategory(int id)
        {
            string query = "SELECT id, name FROM categories WHERE id = @id";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            Category category = new Category();

            OpenConnection();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    category.ID = reader.GetInt32("id");
                    category.Name = reader.GetString("name");
                }
            }
            CloseConnection();

            return category;
        }

        /// <summary>
        /// Gets all categories in the database
        /// </summary>
        /// <returns>A list of <see cref="Category"/> objects</returns>
        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            string query = "SELECT id, name FROM categories";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            OpenConnection();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Category category = new Category(reader.GetInt32("id"),
                        reader.GetString("name"));
                    categories.Add(category);
                }
            }
            CloseConnection();

            return categories;
        }

        /// <summary>
        /// Adds a word to the database
        /// </summary>
        /// <param name="original_word">The original word</param>
        /// <param name="meaning">The meaning of the word</param>
        /// <param name="category_id">The id of the category it belongs to</param>
        /// <param name="language_id">The id of the language it is in</param>
        /// <returns>true if it succeeded, false if it failed</returns>
        public bool AddWord(string original_word, string meaning, int category_id, int language_id)
        {
            string query = "INSERT INTO words(original_word, meaning, " +
                "category_id, language_id) VALUES (@original_word, @meaning, @category_id, @language_id)";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@original_word", original_word);
            command.Parameters.AddWithValue("@meaning", meaning);
            command.Parameters.AddWithValue("@category_id", category_id);
            command.Parameters.AddWithValue("@language_id", language_id);

            return ExecuteCommand(command);
        }

        /// <summary>
        /// Removes a word from the database
        /// </summary>
        /// <param name="id">The id of the word</param>
        /// <returns>true if it succeeded, false if it failed</returns>
        public bool RemoveWord(int id)
        {
            string query = "DELETE FROM words WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            return ExecuteCommand(command);
        }

        /// <summary>
        /// Modifies a word's properties in the database
        /// </summary>
        /// <param name="id">The id of the word</param>
        /// <param name="original_word">The original word</param>
        /// <param name="meaning">The meaning of the word</param>
        /// <param name="category_id">The id of the category it belongs to</param>
        /// <param name="language_id">The id of the language it is in</param>
        /// <returns>true if it succeeded, false if it failed</returns>
        public bool ModifyWord(int id, string original_word, string meaning, int category_id, int language_id)
        {
            string query = "UPDATE words SET original_word = @original_word, " +
                "meaning = @meaning, category_id = @category_id, " +
                "language_id = @language_id WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@original_word", original_word);
            command.Parameters.AddWithValue("@meaning", meaning);
            command.Parameters.AddWithValue("@category_id", category_id);
            command.Parameters.AddWithValue("@language_id", language_id);
            command.Parameters.AddWithValue("@id", id);

            return ExecuteCommand(command);
        }

        /// <summary>
        /// Gets a word from the database
        /// </summary>
        /// <param name="id">The id of the category to get</param>
        /// <returns>A <see cref="Word"/> object</returns>
        public Word GetWord(int id)
        {
            string query = "SELECT id, original_word, meaning, category_id, " +
                "language_id FROM words WHERE id = @id";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            Word word = new Word();

            OpenConnection();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    word.ID = reader.GetInt32("id");
                    word.OriginalWord = reader.GetString("original_word");
                    word.Meaning = reader.GetString("meaning");
                    word.CategoryId = reader.GetInt32("category_id");
                    word.LanguageId = reader.GetInt32("language_id");
                }
            }
            CloseConnection();

            return word;
        }

        /// <summary>
        /// Gets all words in the database
        /// </summary>
        /// <returns>A list of <see cref="Word"/> objects</returns>
        public List<Word> GetAllWords()
        {
            List<Word> words = new List<Word>();
            string query = "SELECT id, original_word, meaning, category_id, " +
                "language_id FROM words";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            OpenConnection();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Word word = new Word(reader.GetInt32("id"),
                        reader.GetString("original_word"),
                        reader.GetString("meaning"),
                        reader.GetInt32("category_id"),
                        reader.GetInt32("language_id")
                        );
                    words.Add(word);
                }
            }
            CloseConnection();

            return words;
        }

    }
}
