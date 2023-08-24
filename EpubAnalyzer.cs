
namespace EPUBWordCounter
{
    public class WordAnalyzer
    {
        public Dictionary<string,int> GetEpubWordCount(string epubFilePath)
            {
                var fileHelper = new FileHelper();
                string temp;

                List<string> Words = fileHelper.getWordsFromEpub(epubFilePath);
                Dictionary<string, int> wordCount = new Dictionary<string, int>();

                foreach(var word in Words)
                {
                    string cleandWord = word.ToLower();
                    wordCount[cleandWord] = wordCount.ContainsKey(cleandWord) ? wordCount[cleandWord] + 1 : 1;
                }
                            
                return wordCount;
            }

        public Dictionary<string, int> PrintMostUsedWords(Dictionary<string, int> dictWordCount, List<string> lookupWords, string category, int limit = 50)
        {
            var mostUsedWords = dictWordCount
                .Where(x => lookupWords.Contains(x.Key))
                .OrderByDescending(pair => pair.Value)
                .Take(limit)
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            Console.WriteLine($"Most used {category} in the book:");
            foreach (var word in mostUsedWords)
                Console.WriteLine($"{word.Key} - {word.Value} times");
            
            return mostUsedWords;
        }
    }

}
