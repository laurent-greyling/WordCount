﻿using WordCount.Services;
using Xunit;

namespace WordCount.Tests.Services
{
    public class WordFrequencyAnalyzerTest
    {
        private string _phrase = "The sun shines over the lake";
        private string _word = "ThE";
        private int _nthValue = 3;
        private WordFrequencyAnalyzer Result;

        public WordFrequencyAnalyzerTest()
        {
            Result = new WordFrequencyAnalyzer();
        }

        [Fact]
        public void Frequency_Of_Word_Return_Occurence_Of_Specified_Word() 
        {
            var wordFrequency = Result.CalculateFrequencyForWord(_phrase, _word);

            Assert.Equal(2, wordFrequency);
        }

        [Fact]
        public void Highest_Frequency_Of_Word_Return_Count_Of_Highest_Occurence_Of_Word()
        {
            var wordFrequency = Result.CalculateHighestFrequency(_phrase);

            Assert.Equal(2, wordFrequency);
        }

        [Fact]
        public void Most_Frequent_N_Returns_List_Of_N_Words_With_Frequency()
        {
            var wordFrequency = Result.CalculateMostFrequentNWords(_phrase, _nthValue);

            Assert.Equal("the", wordFrequency[0].Word);
            Assert.Equal(2, wordFrequency[0].Frequency);
            Assert.Equal("lake", wordFrequency[1].Word);
            Assert.Equal(1, wordFrequency[1].Frequency);
            Assert.Equal("over", wordFrequency[2].Word);
            Assert.Equal(1, wordFrequency[2].Frequency);
        }
    }
}
