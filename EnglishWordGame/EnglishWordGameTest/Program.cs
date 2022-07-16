using EnglishWordGameBackEnd.Database;
using EnglishWordGameBackEnd.Models;

namespace EnglishWordGameTest
{
    internal class Program
    {
        private static WordDatabase wordDatabase;

        static void Main(string[] args)
        {
            wordDatabase = new WordDatabase();

            //CategoryTest();
            WordTest();

            Console.ReadKey();
        }

        static void CategoryTest()
        {
            Console.Write("Category to add: ");
            string categoryName = Console.ReadLine();

            if (wordDatabase.AddCategory(categoryName))
                Console.WriteLine("Success!");

            else
                Console.WriteLine("Failed!");



            Console.Write("Category to remove: ");
            categoryName = Console.ReadLine();

            if (wordDatabase.RemoveCategory(wordDatabase.GetCategory(categoryName).ID))
                Console.WriteLine("Success!");

            else
                Console.WriteLine("Failed!");



            foreach (Category category in wordDatabase.GetAllCategories())
            {
                Console.WriteLine($"ID: {category.ID}, Name: {category.Name}");
            }
        }

        static void WordTest()
        {
            Console.Write("English word: ");
            string english = Console.ReadLine();

            Console.Write("Meaning: ");
            string hungarian = Console.ReadLine();

            Console.Write("Category: ");
            string category = Console.ReadLine();

            if (wordDatabase.AddWord(english, hungarian, wordDatabase.GetCategory(category).ID))
                Console.WriteLine("Success!");

            else
                Console.WriteLine("Failed!");



            Console.Write("Word to remove: ");
            string english_word = Console.ReadLine();

            if(wordDatabase.RemoveWord(wordDatabase.GetWord(english_word).ID))
                Console.WriteLine("Success!");

            else
                Console.WriteLine("Failed!");

            

            foreach(Word word in wordDatabase.GetAllWords())
            {
                Console.WriteLine($"ID: {word.ID}, English word: {word.EnglishWord}, Hungarian word: {word.HungarianWord}, Category id: {word.CategoryId}");
            }
        }
    }
}