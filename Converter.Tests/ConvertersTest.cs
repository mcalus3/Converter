using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Converter.Models;

namespace Converter.Tests
{
    [TestClass]
    public class ConvertersTest
    {
        [TestMethod]
        public void ConvertToCsvTest()
        {
            //setUp
            string inputString = @"Mary had a little lamb. Peter called for the wolf, and Aesop came. 
Cinderella likes shoes.
";
            string inputString2 = @"  Mary   had a little  lamb  . 


  Peter   called for the wolf   ,  and Aesop came .
 Cinderella  likes shoes."
;
            string outputString = @", Word 1, Word 2, Word 3, Word 4, Word 5, Word 6, Word 7, Word 8
Sentence 1, a, had, lamb, little, Mary
Sentence 2, Aesop, and, called, came, for, Peter, the, wolf
Sentence 3, Cinderella, likes, shoes
";

            // Test  
            CsvSortedDocument csv = new CsvSortedDocument(inputString);
            CsvSortedDocument csv2 = new CsvSortedDocument(inputString2);
            Assert.AreEqual(outputString, csv.GetFormattedString(), "Csv files doesn't match");
            Assert.AreEqual(outputString, csv2.GetFormattedString(), "Csv files doesn't match");
        }

        [TestMethod]
        public void ConvertToXmlTest()
        {
            //setUp
            string inputString = @"Mary had a little lamb. Peter called for the wolf, and Aesop came. 
Cinderella likes shoes.
";
            string inputString2 = @"  Mary   had a little  lamb  . 


  Peter   called for the wolf   ,  and Aesop came .
 Cinderella  likes shoes."
;
            string outputString = @"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>
<text>
  <sentence>
    <word>a</word>
    <word>had</word>
    <word>lamb</word>
    <word>little</word>
    <word>Mary</word>
  </sentence>
  <sentence>
    <word>Aesop</word>
    <word>and</word>
    <word>called</word>
    <word>came</word>
    <word>for</word>
    <word>Peter</word>
    <word>the</word>
    <word>wolf</word>
  </sentence>
  <sentence>
    <word>Cinderella</word>
    <word>likes</word>
    <word>shoes</word>
  </sentence>
</text>";

            // Test  
            var xml = new XmlSortedDocument(inputString);
            var xml2 = new XmlSortedDocument(inputString2);
            Assert.AreEqual(outputString, xml.GetFormattedString(), "XML files doesn't match");
            Assert.AreEqual(outputString, xml2.GetFormattedString(), "XML files doesn't match");
        }
    }
}
