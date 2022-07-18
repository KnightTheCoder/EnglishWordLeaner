namespace EnglishWordGameBackEnd.Models
{
    /// <summary>
    /// Represents a word in the database
    /// </summary>
    public class Word
    {
        /// <summary>
        /// The id of the word
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The origianl word
        /// </summary>
        public string OriginalWord { get; set; }

        /// <summary>
        /// The meaning of the word
        /// </summary>
        public string Meaning { get; set; }

        /// <summary>
        /// The id of the category it belongs to
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// The id of the language it is in
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// Initializes a word with default values
        /// </summary>
        public Word()
        {
            this.ID = 0;
            this.OriginalWord = String.Empty;
            this.Meaning = String.Empty;
            this.CategoryId = 1;
            this.LanguageId = 1;
        }

        /// <summary>
        /// Initializes a word with the given values
        /// </summary>
        /// <param name="id"></param>
        /// <param name="originalWord"></param>
        /// <param name="meaning"></param>
        /// <param name="categoryId"></param>
        /// <param name="languageId"></param>
        public Word(int id, string originalWord, string meaning, int categoryId, int languageId)
        {
            this.ID = id;
            this.OriginalWord = originalWord;
            this.Meaning = meaning;
            this.CategoryId = categoryId;
            this.LanguageId = languageId;
        }

        public override string ToString()
        {
            return $"ID: {this.ID}, Original Word: {this.OriginalWord}, " +
                $"Meaning: {this.Meaning}, Category ID: {this.CategoryId}, " +
                $"LanguageId: {this.LanguageId}";
        }
    }
}
