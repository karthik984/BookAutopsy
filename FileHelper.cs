using VersOne.Epub;

namespace EPUBWordCounter
{
    public class FileHelper
    {
    public List<String> getWordsFromEpub(string path)
        {
            EpubBook book = EpubReader.ReadBook(path);
            List<string> allWords = new List<string>();
            IEnumerable<string>? cleanedWords = new List<string>();

            foreach(EpubLocalTextContentFile textContentFile in book.ReadingOrder)
            {
                string content = textContentFile.Content;
                string[] words = content.Split(new char[] { ' ', '\t', '\n', '\r', '.', ',', ';', '!', '?','-' }, StringSplitOptions.RemoveEmptyEntries);
                allWords.AddRange(words);

                cleanedWords = allWords.Where(word => !word.Contains("bold"))
                        .Where(word => !word.Contains("span"))
                        .Where(word => !word.Contains("calibre"))
                        .Where(word => !word.Contains("Content-Type"))
                        .Where(word => !word.Contains("class="))
                        .Where(word => !word.Contains("href="))
                        .Where(word => !word.Contains("/>"))
                        .Where(word => !word.Contains("</"))
                        .Where(word => !word.Contains("<"))
                        .Where(word => !word.Contains(">"))
                        .Where(word => !word.Contains("-"))
                        .Where(word => !word.Contains('"'))
                        .Select(word => word);
            }

            return cleanedWords.ToList();
        }

        public List<String> getWordsFromText(string path)
        {
            //returns list of words from a text file with each line having each word
            var words = new List<String>();

            using(StreamReader reader = new StreamReader(path))
            {
            string line;

            while((line = reader.ReadLine()) != null)            
                 words.Add(line.ToLower()
                               .Trim());

            }
 
            return words;
        }
    }

}
