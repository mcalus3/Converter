using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Converter.Models
{
    public class SortedDocumentFactory
    {
        private static SortedDocumentFactory instance;
        
        public static SortedDocumentFactory Instance
        {
            get
            {
                if (SortedDocumentFactory.instance == null)
                {
                    SortedDocumentFactory.instance = new SortedDocumentFactory();
                }
                return SortedDocumentFactory.instance;
            }
            private set { SortedDocumentFactory.instance = value; }
        }

        public SortedDocument CreateSortedDocument(string documentType, string text)
        {
            switch (documentType)
            {
                case "csv":
                    return new CsvSortedDocument(text);

                case "xml":
                    return new XmlSortedDocument(text);
                default:
                    throw new InvalidOperationException("Document type \"" + documentType + "\" not supported.");
            }
        }
    }
}