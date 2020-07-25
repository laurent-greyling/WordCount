using System;
using System.Collections.Generic;
using System.Linq;
using WordCount.Library.Utilities;

namespace WordCount.Library.Services
{
    public class WordFrequencyAnalyzer : IWordFrequencyAnalyzer
    {
        public int CalculateFrequencyForWord(string text, string word)
        {
            Ensure.ArgumentNotNullOrEmptyString(text, nameof(text));
            Ensure.ArgumentNotNullOrEmptyString(word, nameof(word));

            var wordList = ParagraphWordList(text);

            //Query to return the count of a specified word in the text
            return wordList
                .Where(w => w.ToLowerInvariant() == word.ToLowerInvariant())
                .Count();
        }

        public int CalculateHighestFrequency(string text)
        {
            Ensure.ArgumentNotNullOrEmptyString(text, nameof(text));

            var wordFrequency = WordFrequencyDictioniary(text);
            
            return wordFrequency.FirstOrDefault().Value;
        }

        public IList<IWordFrequency> CalculateMostFrequentNWords(string text, int n)
        {
            Ensure.ArgumentNotNullOrEmptyString(text, nameof(text));

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
        /// Convert the string/paragraph/sentence into an array of words
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string[] ParagraphWordList(string text) 
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
        /// Order in descending will put highest frequency into position 0 in list
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private IOrderedEnumerable<KeyValuePair<string,int>> WordFrequencyDictioniary(string text) 
        {
            var wordList = ParagraphWordList(text);

            return wordList
               .Where(word => word.ToLowerInvariant() != string.Empty)
               .GroupBy(word => word.ToLowerInvariant())
               .ToDictionary(word => word.Key.ToLowerInvariant(), frequency => frequency.Count())
               .OrderByDescending(frequency => frequency.Value);
        }
    }
}
