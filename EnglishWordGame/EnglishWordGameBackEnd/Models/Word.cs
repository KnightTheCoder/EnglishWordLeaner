namespace EnglishWordGameBackEnd.Models
{
    public class Word
    {
        public int ID { get; set; }
        public string EnglishWord { get; set; }
        public string HungarianWord { get; set; }
        public int CategoryId { get; set; }

        public Word()
        {
            this.ID = 1;
            this.EnglishWord = String.Empty;
            this.HungarianWord = String.Empty;
            this.CategoryId = 1;
        }

        public Word(int id, string englishWord, string hungarianWord, int category)
        {
            this.ID = id;
            this.EnglishWord = englishWord;
            this.HungarianWord = hungarianWord;
            this.CategoryId = category;
        }
    }
}
