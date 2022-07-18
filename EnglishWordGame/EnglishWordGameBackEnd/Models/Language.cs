namespace EnglishWordGameBackEnd.Models
{
    public class Language
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Language()
        {
            this.ID = 0;
            this.Name = String.Empty;
        }

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
