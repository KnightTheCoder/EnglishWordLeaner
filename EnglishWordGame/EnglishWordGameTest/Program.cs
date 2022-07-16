using EnglishWordGameBackEnd.Database;
using EnglishWordGameBackEnd.Models;

namespace EnglishWordGameTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WordDatabase wordDatabase = new WordDatabase();

            Console.Write("Category to add: ");
            string categoryName = Console.ReadLine();

            if(wordDatabase.AddCategory(categoryName))
                Console.WriteLine("Success!");

            else
                Console.WriteLine("Failed!");

            Console.WriteLine("Category to remove: ");
            categoryName = Console.ReadLine();

            if (wordDatabase.RemoveCategory(wordDatabase.GetCategory(categoryName).ID))
                Console.WriteLine("Success!");

            else
                Console.WriteLine("Failed!");

            foreach(Category category in wordDatabase.GetAllCategories())
            {
                Console.WriteLine($"ID: {category.ID}, Name: {category.Name}");
            }

            Console.ReadKey();
        }
    }
}