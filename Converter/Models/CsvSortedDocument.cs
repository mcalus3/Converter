using CsvHelper;
using System.IO;
using System.Linq;

namespace Converter.Models
{
    public class CsvSortedDocument : SortedDocument
    {
        private string csvDoc;

        public CsvSortedDocument(string inputString)
        {
            if(string.IsNullOrEmpty(inputString))
            {
                this.csvDoc = "";
                return;
            }

            int maxWords = TextHelper.SplitToSentences(inputString).Select(s => TextHelper.GetSortedWordsFromString(s))
                                                           .OrderBy(x => x.Length)
                                                           .Last()
                                                           .Length;
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer))
            {
                csv.Configuration.Delimiter = ", "; 
                int i = 0;
                csv.WriteField("");
                for (i = 1; i <= maxWords; i++)
                {
                    csv.WriteField("Word " + i);
                }
                csv.NextRecord();

                i = 0;
                foreach (var sentence in TextHelper.SplitToSentences(inputString))
                {
                    i++;
                    csv.WriteField("Sentence " + i.ToString());

                    foreach (var word in TextHelper.GetSortedWordsFromString(sentence))
                    {
                        csv.WriteField(word);
                    }
                    csv.NextRecord();
                }
                writer.Flush();
                stream.Flush();
                stream.Position = 0;
                StreamReader sReader = new StreamReader(stream);
                this.csvDoc = sReader.ReadToEnd();
            }
        }

        public string GetFormattedString()
        {
            return csvDoc;
        }
    }
}