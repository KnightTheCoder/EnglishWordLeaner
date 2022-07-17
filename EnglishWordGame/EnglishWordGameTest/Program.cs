using EnglishWordGameBackEnd.Database;
using EnglishWordGameBackEnd.Models;

namespace EnglishWordGameTest
{
    internal class Program
    {
        private static WordDatabase wordDatabase;

        static void Main(string[] args)
        {
            wordDatabase = new WordDatabase("english_learning");

            CategoryTest();
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

            List<Category> categories = wordDatabase.GetAllCategories();
            foreach (Category category in categories)
            {
                Console.WriteLine(category);
            }

            Console.Write("Category ID to remove: ");
            int category_id = int.Parse(Console.ReadLine());

            if (wordDatabase.RemoveCategory(wordDatabase.GetCategory(category_id).ID))
                Console.WriteLine("Success!");

            else
                Console.WriteLine("Failed!");


            categories = wordDatabase.GetAllCategories();
            foreach (Category category in categories)
            {
                Console.WriteLine(category);
            }
        }

        static void WordTest()
        {
            Console.Write("English word: ");
            string english = Console.ReadLine();

            Console.Write("Meaning: ");
            string hungarian = Console.ReadLine();

            Console.Write("Category ID: ");
            int category_id = int.Parse(Console.ReadLine());

            if (wordDatabase.AddWord(english, hungarian, wordDatabase.GetCategory(category_id).ID))
                Console.WriteLine("Success!");

            else
                Console.WriteLine("Failed!");



            List<Word> words = wordDatabase.GetAllWords();
            foreach (Word word in words)
            {
                Console.WriteLine(word);
            }

            Console.Write("Word ID to remove: ");
            int word_id = int.Parse(Console.ReadLine());

            if(wordDatabase.RemoveWord(wordDatabase.GetWord(word_id).ID))
                Console.WriteLine("Success!");

            else
                Console.WriteLine("Failed!");


            words = wordDatabase.GetAllWords();
            foreach (Word word in words)
            {
                Console.WriteLine(word);
            }
        }
    }
}