using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringMods
{
    class Modifier
    {
        #region Variables
        private List<string> Words { get; set; }
        private List<string> ReplacedWords { get; set; }
        public string InputPhrase { get; set; }
        #endregion

        /// <summary>
        /// Initialize objects
        /// </summary>
        private void Initialize()
        {
            Words = new List<string>();
            ReplacedWords = new List<string>();
        }

        /// <summary>
        /// Driving method that controls what happens.
        /// </summary>
        public void Start()
        {
            Initialize();
            Words = SplitInput();
            OutputWords();
        }

        /// <summary>
        /// Loop through each word in the phrase, look at each character and determine if it needs to output to the screen
        /// </summary>
        private void OutputWords()
        {
            if (Words.Count == 0) return;

            #region Transform Words
            foreach (var word in Words)
            {
                int wordLen = BetweenCount(word);
                bool outputLen = false;
                string newWord = string.Empty;

                for (int i = 0; i < word.Length; ++i)
                {
                    // Output if necessary
                    if (OutputNedded(word, i))
                    {
                        //Console.Write(word[i]);
                        newWord += word[i];
                    }

                    // Output the length of the word
                    if ((i > (Math.Floor((decimal)wordLen / 2) - 1) && !outputLen) || (word.Length <= 2 && i == 0))
                    {
                        //Console.Write(wordLen);
                        newWord += wordLen.ToString();
                        outputLen = true;
                    }
                }

                //Console.WriteLine();
                ReplacedWords.Add(newWord);
            }
            #endregion
            #region Output to Console
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var word in ReplacedWords)
                Console.Write(word + " ");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            #endregion
        }

        /// <summary>
        /// Determine if the current character in a word should be output or not
        /// </summary>
        /// <param name="word"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool OutputNedded(string word, int index)
        {
            if (!Char.IsLetter(word[index])) return true;

            if (index == FirstLetterIndex(word) || index == LastLetterIndex(word))
                return true;

            return false;
        }

        /// <summary>
        /// Given a string return the index of the last letter, ignores symbols and numbers.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private int LastLetterIndex(string word)
        {
            if (string.IsNullOrEmpty(word)) return 0;

            for (int i = word.Length - 1; i >= 0; --i)
                if (Char.IsLetter(word[i]))
                    return i;

            return 0;
        }

        /// <summary>
        /// Given a string return the index of the first letter, ignores symbols and numbers.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private int FirstLetterIndex(string word)
        {
            if (string.IsNullOrEmpty(word)) return 0;

            for (int i = 0; i < word.Length; ++i)
                if (Char.IsLetter(word[i]))
                    return i;
            
            return 0;
        }

        /// <summary>
        /// The number of letters between the first letter and the last letter, symbols and numbers are ignored. Notice that the first letter and last letter are not counted.
        /// </summary>
        /// <param name="word">A string whose alphabetic length is desired.</param>
        /// <returns></returns>
        private int BetweenCount(string word)
        {
            if (string.IsNullOrEmpty(word)) return 0;

            HashSet<char> uniqueCharacters = new HashSet<char>();
            string lowerWord = word.ToLower();

            for(int i = 0; i < word.Length; ++i)
                if(i != FirstLetterIndex(lowerWord) && i != LastLetterIndex(lowerWord))
                    if (Char.IsLetter(lowerWord[i]))
                        uniqueCharacters.Add(lowerWord[i]);

            return uniqueCharacters.Count;
        }

        /// <summary>
        /// Splits the input phrase into a list of words.
        /// </summary>
        /// <returns></returns>
        private List<string> SplitInput()
        {
            List<string> toReturn = new List<string>();

            if (string.IsNullOrEmpty(InputPhrase)) return toReturn;

            int startIndex = 0;
            int endIndex = 0;

            for (int i = 0; i < InputPhrase.Length; ++i)
            {
                ++endIndex;
                if (!Char.IsLetter(InputPhrase[i]))
                {
                    string newWord = InputPhrase.Substring(startIndex, endIndex - startIndex);
                    toReturn.Add(newWord);
                    startIndex = endIndex;
                }
            }

            string finalWord = InputPhrase.Substring(startIndex, endIndex - startIndex);
            toReturn.Add(finalWord);

            return toReturn;
        }

        /// <summary>
        /// Captures the phrase from the user. Not used anymore after refactoring.
        /// </summary>
        /// <returns></returns>
        public string GetInput()
        {
            string toReturn = string.Empty;

            Console.Write("Phrase: ");

            while (toReturn == string.Empty)
            {
                toReturn = Console.ReadLine();
                if (toReturn == string.Empty)
                    Console.Write("You must enter some kind of value, try again...\nPhrase: ");
            }

            return toReturn;
        }
    }
}
