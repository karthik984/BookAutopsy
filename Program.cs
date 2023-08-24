using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

namespace EPUBWordCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            string epubFilePath = @"C:\Karthik\zaz\BookAutopsy\assets\theidiot.epub";
            string nounList = @"C:\Karthik\zaz\BookAutopsy\assets\nounlist.txt";
            var fileHelper = new FileHelper();
            List<string> nouns = fileHelper.getWordsFromText(nounList);
            
            var epubAnalyzer = new EpubAnalyzer();

            Dictionary<string, int> dictWordCount = epubAnalyzer.getEpubWordCount(epubFilePath);
            int limit = 500;

            var topWords = dictWordCount
                .Where(x => nouns.Contains(x.Key))
                .OrderByDescending(pair => pair.Value)
                .Take(limit);
            
            foreach (var word in topWords)
            Console.WriteLine($"{word.Key} - {word.Value} times");
        }


    }
}
