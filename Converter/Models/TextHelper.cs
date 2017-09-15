using System;
using System.Linq;

namespace Converter.Models
{
    public static class TextHelper
    {
        public static string[] GetSortedWordsFromString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new string[] { };
            }
            var punctuation = str.Where(Char.IsPunctuation).Distinct().ToArray();
            string[] words = str.Split(new char[0], StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => x.Trim(punctuation))
                                .Where(x => !string.IsNullOrEmpty(x))
                                .ToArray();
            Array.Sort(words);
            return words;
        }

        public static string[] SplitToSentences(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new string[] { };
            }
            return str.Split('.')
                      .Where(s => !string.IsNullOrWhiteSpace(s))
                      .ToArray();
        } 
    }
}