using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Converter.Models
{
    public class XmlSortedDocument : SortedDocument
    {
        private XmlDocument xmlDocument;

        public XmlSortedDocument(string str)
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
            XmlElement textElement = (XmlElement)doc.AppendChild(doc.CreateElement("text"));

            foreach (string sentence in TextHelper.SplitToSentences(str))
            {
                XmlElement sentenceElement = (XmlElement)textElement.AppendChild(doc.CreateElement("sentence"));

                foreach (string word in TextHelper.GetSortedWordsFromString(sentence))
                {
                    XmlNode wordNode = doc.CreateElement("word");
                    wordNode.InnerText = word;
                    sentenceElement.AppendChild(wordNode);
                }
            }

            this.xmlDocument = doc;
        }

        public string GetFormattedString()
        {
            using (MemoryStream mStream = new MemoryStream())
            using (XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;

                this.xmlDocument.Save(writer);
                writer.Flush();
                mStream.Flush();
                mStream.Position = 0;
                StreamReader sReader = new StreamReader(mStream);
                String FormattedXML = sReader.ReadToEnd();
                return FormattedXML;
            }
        }

    }
}