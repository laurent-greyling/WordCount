using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCount.Library.Services
{
    public class WordFrequencyAnalyzer : IWordFrequencyAnalyzer
    {
        public int CalculateFrequencyForWord(string text, string word)
        {             
            var textArray = ParagraphTextAsArray(text);

            //Query to return the count of a specified word in the text
            return textArray
                .Where(x => x.ToLowerInvariant() == word.ToLowerInvariant())
                .Count();
        }

        public int CalculateHighestFrequency(string text)
        {
            var dictionairy = WordFrequencyDictioniary(text);
            
            var wordFrequency = dictionairy.ToList();

            return wordFrequency[0].Value;
        }

        public IList<IWordFrequency> CalculateMostFrequentNWords(string text, int n)
        {
            var wordFrequencies = new List<IWordFrequency>();

            var dictionairy = WordFrequencyDictioniary(text);

            var wordFrequencyList = dictionairy
                .ThenBy(x => x.Key)
                .Take(n)
                .Select(x=> 
                {
                    return new WordFrequency
                    {
                        Word = x.Key,
                        Frequency = x.Value
                    };
                });

            wordFrequencies.AddRange(wordFrequencyList);
            return wordFrequencies;
        }

        /// <summary>
        /// Convert the string into an array of words 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string[] ParagraphTextAsArray(string text) 
        {
            return text.Split(
                new char[]
                {
                    '.',
                    '?',
                    '!',
                    ' ',
                    ';',
                    ':',
                    ','
                }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Query to get frequency of all words in the paragraph as dictionary
        ///Order in descending will put highest frrequency into position 0 in list
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private IOrderedEnumerable<KeyValuePair<string,int>> WordFrequencyDictioniary(string text) 
        {
            var textArray = ParagraphTextAsArray(text);

            return textArray
               .Where(x => x.ToLowerInvariant() != string.Empty)
               .GroupBy(x => x.ToLowerInvariant())
               .ToDictionary(x => x.Key.ToLowerInvariant(), y => y.Count())
               .OrderByDescending(x => x.Value);
        }
    }
}
