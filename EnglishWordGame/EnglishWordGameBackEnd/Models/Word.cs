namespace EnglishWordGameBackEnd.Models
{
    public class Word
    {
        public int ID { get; set; }
        public string OriginalWord { get; set; }
        public string Meaning { get; set; }
        public int CategoryId { get; set; }

        public Word()
        {
            this.ID = 0;
            this.OriginalWord = String.Empty;
            this.Meaning = String.Empty;
            this.CategoryId = 1;
        }

        public Word(int id, string originalWord, string meaning, int categoryId)
        {
            this.ID = id;
            this.OriginalWord = originalWord;
            this.Meaning = meaning;
            this.CategoryId = categoryId;
        }

        public override string ToString()
        {
            return $"ID: {this.ID}, Original Word: {this.OriginalWord}, " +
                $"Meaning: {this.Meaning}, Category ID: {this.CategoryId}";
        }
    }
}
