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

            LanguageTest();
            CategoryTest();
            WordTest();

            Console.ReadKey();
        }

        static void EvaluateSuccess(bool condition)
        {
            if(condition)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed!");
                Console.ResetColor();
            }
        }

        static void LanguageTest()
        {
            Console.Write("Language to add: ");
            string languageName = Console.ReadLine();

            EvaluateSuccess(wordDatabase.AddLanguage(languageName));

            List<Language> languages = wordDatabase.GetAllLanguages();
            foreach (Language language in languages)
            {
                Console.WriteLine(language);
            }

            Console.Write("Language ID to remove: ");
            int language_id = int.Parse(Console.ReadLine());

            EvaluateSuccess(wordDatabase.RemoveLanguage(wordDatabase.GetLanguage(language_id).ID));

            languages = wordDatabase.GetAllLanguages();
            foreach (Language language in languages)
            {
                Console.WriteLine(language);
            }
        }

        static void CategoryTest()
        {
            Console.Write("Category to add: ");
            string categoryName = Console.ReadLine();

            EvaluateSuccess(wordDatabase.AddCategory(categoryName));

            List<Category> categories = wordDatabase.GetAllCategories();
            foreach (Category category in categories)
            {
                Console.WriteLine(category);
            }

            Console.Write("Category ID to remove: ");
            int category_id = int.Parse(Console.ReadLine());

            EvaluateSuccess(wordDatabase.RemoveCategory(wordDatabase.GetCategory(category_id).ID));



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

            Console.Write("Language ID: ");
            int language_id = int.Parse(Console.ReadLine());

            EvaluateSuccess(wordDatabase.AddWord(english, hungarian, wordDatabase.GetCategory(category_id).ID, wordDatabase.GetLanguage(language_id).ID));



            List<Word> words = wordDatabase.GetAllWords();
            foreach (Word word in words)
            {
                //Console.WriteLine(word);
                Console.WriteLine($"ID: {word.ID}, Original Word: {word.OriginalWord}, " +
                    $"Meaning: {word.Meaning}, Category: {wordDatabase.GetCategory(word.CategoryId).Name}, " +
                    $"Language: {wordDatabase.GetLanguage(word.LanguageId).Name}");
            }

            Console.Write("Word ID to remove: ");
            int word_id = int.Parse(Console.ReadLine());

            EvaluateSuccess(wordDatabase.RemoveWord(wordDatabase.GetWord(word_id).ID));


            words = wordDatabase.GetAllWords();
            foreach (Word word in words)
            {
                //Console.WriteLine(word);
                Console.WriteLine($"ID: {word.ID}, Original Word: {word.OriginalWord}, " +
                    $"Meaning: {word.Meaning}, Category: {wordDatabase.GetCategory(word.CategoryId).Name}, " +
                    $"Language: {wordDatabase.GetLanguage(word.LanguageId).Name}");
            }
        }
    }
}