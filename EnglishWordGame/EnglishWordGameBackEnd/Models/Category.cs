namespace EnglishWordGameBackEnd.Models
{
    /// <summary>
    /// Represents a category in the database
    /// </summary>
    public class Category
    {
        /// <summary>
        /// The id of the category
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The name of the category
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Initializes a category with default values
        /// </summary>
        public Category()
        {
            this.ID = 0;
            this.Name = String.Empty;
        }

        /// <summary>
        /// Initializes a category with the given values
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public Category(int id, string name)
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
