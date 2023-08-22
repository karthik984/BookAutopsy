using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VersOne.Epub;

namespace EPUBWordCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            string epubFilePath = @"C:\Karthik\zaz\BookAutopsy\source\The Idiot.epub";

            EpubBook epubBook = EpubReader.ReadBook(epubFilePath);

            List<string> allWords = new List<string>();

            foreach (EpubLocalTextContentFile textContentFile in epubBook.ReadingOrder)
            {
                string content = textContentFile.Content;
                string[] words = content.Split(new char[] { ' ', '\t', '\n', '\r', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
                allWords.AddRange(words);
            }

            // Count word occurrences
            Dictionary<string, int> wordCount = new Dictionary<string, int>();
            foreach (string word in allWords)
            {
                string cleanedWord = word.ToLower(); // Convert to lowercase for consistent counting
                if (wordCount.ContainsKey(cleanedWord))
                {
                    wordCount[cleanedWord]++;
                }
                else
                {
                    wordCount[cleanedWord] = 1;
                }
            }

            // Get top 10 words
            var topWords = wordCount.OrderByDescending(pair => pair.Value)
                                   .Take(10)
                                   .Select(pair => pair.Key);

            // Display top words
            Console.WriteLine("Top 10 most used words:");
            foreach (string word in topWords)
            {
                Console.WriteLine($"{word}: {wordCount[word]} occurrences");
            }

            

        }
    }
}
