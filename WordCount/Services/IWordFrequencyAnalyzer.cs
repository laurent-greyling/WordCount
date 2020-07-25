using System.Collections.Generic;

namespace WordCount.Services
{
    public interface IWordFrequencyAnalyzer
    {
        /// <summary>
        /// Return the value of the highest occuring word in the series 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        int CalculateHighestFrequency(string text);

        /// <summary>
        /// Return the frequency of a specified word in the series
        /// </summary>
        /// <param name="text"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        int CalculateFrequencyForWord(string text, string word);

        /// <summary>
        /// Return a list of the most frequent "n" words in the input text,
        /// </summary>
        /// <param name="text"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        IList<IWordFrequency> CalculateMostFrequentNWords(string text, int n);
    }
}
