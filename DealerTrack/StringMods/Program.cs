using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StringMods
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Type a word or phrase and press enter, type 'quit' or 'exit' to end the program.\nPhrase: ");
            string phrase = ValidateInput();

            while (phrase != "quit" && phrase != "exit")
            {
                Modifier modifier = new Modifier() { InputPhrase = phrase };
                modifier.Start();

                Console.Write("Phrase: ");
                phrase = ValidateInput();
            }

            Console.WriteLine("Thanks for playing!\nDon't forget to hire me!");
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Reads in a line and validates the input, if the input is empty it will ask you to try again.
        /// </summary>
        /// <returns></returns>
        private static string ValidateInput()
        {
            string phrase = Console.ReadLine();
            while (string.IsNullOrEmpty(phrase))
            {
                Console.Write("Your phrase is empty, please try again\nPhrase: ");
                phrase = Console.ReadLine();
            }
            return phrase;
        }
    }
}
