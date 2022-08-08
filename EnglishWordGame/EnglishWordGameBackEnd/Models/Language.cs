namespace EnglishWordGameBackEnd.Models
{
    /// <summary>
    /// Represents a language in the database
    /// </summary>
    public class Language
    {
        /// <summary>
        /// The id of the language
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The name of the language
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Initializes a <see cref="Language"/> with default values
        /// </summary>
        public Language()
        {
            this.ID = 0;
            this.Name = String.Empty;
        }

        /// <summary>
        /// Initializes a <see cref="Language"/> with the given values
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public Language(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return $"ID: {this.ID}, Name: {this.Name}";
        }
    }
}
