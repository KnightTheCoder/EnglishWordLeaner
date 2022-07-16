namespace EnglishWordGameBackEnd.Models
{
    public class Word
    {
        public int ID { get; set; }
        public string EnglishWord { get; set; }
        public string HungarianWord { get; set; }
        public string Category { get; set; }

        public Word()
        {
            this.ID = 1;
            this.EnglishWord = String.Empty;
            this.HungarianWord = String.Empty;
            this.Category = String.Empty;
        }

        public Word(int id, string englishWord, string hungarianWord, string category)
        {
            this.ID = id;
            this.EnglishWord = englishWord;
            this.HungarianWord = hungarianWord;
            this.Category = category;
        }
    }
}
