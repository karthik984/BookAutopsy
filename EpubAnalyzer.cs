
namespace EPUBWordCounter
{
    public class EpubAnalyzer
    {
    public Dictionary<string,int> getEpubWordCount(string epubFilePath)
        {
            var fileHelper = new FileHelper();

            List<string> Words = fileHelper.getWordsFromEpub(epubFilePath);
            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            foreach(var word in Words)
            {
                string cleandWord = word.ToLower();
                wordCount[cleandWord] = wordCount.ContainsKey(cleandWord) ? wordCount[cleandWord] + 1 : 1;
            }
                        
            return wordCount;
        }

    }

}
