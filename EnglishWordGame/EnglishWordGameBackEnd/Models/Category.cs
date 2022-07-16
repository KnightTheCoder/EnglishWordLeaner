﻿namespace EnglishWordGameBackEnd.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Category()
        {
            this.ID = 1;
            this.Name = String.Empty;
        }

        public Category(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }
}
