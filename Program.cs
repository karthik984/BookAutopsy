namespace EPUBWordCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            string epubFilePath = @"assets\xyz.epub";
            string nounList = @"assets\nounlist.txt";
            string verbList = @"assets\verblist.txt";
            string commonList = @"assets\commonlist.txt";
            int limit = 50;

            var fileHelper = new FileHelper();
            List<string> nouns = fileHelper.GetWordsFromText(nounList);
            List<string> verbs = fileHelper.GetWordsFromText(verbList);
            List<string> commonWords = fileHelper.GetWordsFromText(commonList);
            
            var epubAnalyzer = new WordAnalyzer();

            Dictionary<string, int> dictWordCount = epubAnalyzer.GetEpubWordCount(epubFilePath);

            var wordAnalyzer = new WordAnalyzer();

            wordAnalyzer.PrintMostUsedWords(dictWordCount, nouns, "nouns", limit);        
            wordAnalyzer.PrintMostUsedWords(dictWordCount, verbs, "verb", limit);
            wordAnalyzer.PrintMostUsedWords(dictWordCount, commonWords, "common words", limit);
        }


    }
}
