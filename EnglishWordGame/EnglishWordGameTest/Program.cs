using EnglishWordGameBackEnd.Database;

namespace EnglishWordGameTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WordDatabase wordDatabase = new WordDatabase();

            Console.Write("Category: ");
            string categoryName = Console.ReadLine();

            if(wordDatabase.AddCategory(categoryName))
                Console.WriteLine("Success!");

            else
                Console.WriteLine("Failed!");
        }
    }
}