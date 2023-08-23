using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VersOne.Epub;
using Stanford.NLP;
using Stanford.NLP.POSTagger;

namespace EPUBWordCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            string epubFilePath = @"C:\Karthik\zaz\BookAutopsy\source\The Idiot.epub";

            // Load the POS tagger model
            var posTagger = new EnglishPOSTagger();

            EpubBook epubBook = EpubReader.ReadBook(epubFilePath);

            List<string> allWords = new List<string>();
            foreach (EpubLocalTextContentFile textContentFile in epubBook.ReadingOrder)
            {
                string content = textContentFile.Content;
                string[] words = content.Split(new char[] { ' ', '\t', '\n', '\r', '.', ',', ';', '!', '?', '-' }, StringSplitOptions.RemoveEmptyEntries);
                allWords.AddRange(words);
            }

            // Load a list of common stop words (you can extend this list if needed)
            var stopWords = new HashSet<string>
            {
                "the", "and", "of", "a", "in", "to", "is", "it", "for", "with", "on", "at", "by", "that", "you"
            };

            // Count word occurrences
            Dictionary<string, int> wordCount = new Dictionary<string, int>();
            foreach (string word in allWords)
            {
                string cleanedWord = word.ToLower(); // Convert to lowercase for consistent counting
                if (!stopWords.Contains(cleanedWord))
                {
                    if (wordCount.ContainsKey(cleanedWord))
                    {
                        wordCount[cleanedWord]++;
                    }
                    else
                    {
                        wordCount[cleanedWord] = 1;
                    }
                }
            }

            // Get the nouns
            var nouns = new List<string>();
            foreach (var word in wordCount.Keys)
            {
                var tags = posTagger.Tag(new List<string> { word });
                if (tags[0] == "NN") // "NN" is the tag for nouns
                {
                    nouns.Add(word);
                }
            }

            // Get top 10 most used nouns
            var topNouns = nouns.GroupBy(noun => noun)
                                .OrderByDescending(group => group.Count())
                                .Take(10)
                                .Select(group => group.Key);

            // Display top nouns
            Console.WriteLine("Top 10 most used nouns:");
            foreach (string noun in topNouns)
            {
                Console.WriteLine($"{noun}: {wordCount[noun]} occurrences");
            }

            // Close the EpubBook instance
            epubBook.Close();
        }
    }
}
