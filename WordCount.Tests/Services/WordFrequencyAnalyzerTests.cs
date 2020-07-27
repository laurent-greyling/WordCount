using System;
using WordCount.Library.Services;
using Xunit;

namespace WordCount.Tests.Services
{
    public class WordFrequencyAnalyzerTests
    {
        private readonly string _phrase = "The sun shines over the lake";
        private readonly string _numericPhrase = "The sun shines over the lake 7, 7, 777, 777, 777777777, 777777777";
        private readonly string _guidPhrase = "The sun shines over the lake 64b0e55b-9cf0-41a0-ad7c-c37d2a48d5fb 64b0e55b-9cf0-41a0-ad7c-c37d2a48d5fb";
        private readonly string _charnumberPhrase = "The sun shines over the lake thr33 tim3s thr33";
        private readonly string _specialCharPhrase = "The sun shines over the lake - + = ) ( * & ^ % $ # @ ! ~ ` - + = ) ( * & ^ % $ # @ ! ~ `";
        private readonly string _word = "ThE";
        private readonly string _whitespaceWord = " ThE ";
        private readonly int _nthValue = 3;
        private readonly WordFrequencyAnalyzer Result;

        public WordFrequencyAnalyzerTests()
        {
            Result = new WordFrequencyAnalyzer();
        }

        [Fact]
        public void CalculateFrequencyForWord_Text_Null_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Result.CalculateFrequencyForWord(null, _word));
        }

        [Fact]
        public void CalculateFrequencyForWord_Text_Empty_Throws_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Result.CalculateFrequencyForWord("", _word));
        }

        [Fact]
        public void CalculateFrequencyForWord_Word_Null_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Result.CalculateFrequencyForWord(_phrase, null));
        }

        [Fact]
        public void CalculateFrequencyForWord_Word_Empty_Throws_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Result.CalculateFrequencyForWord(_phrase, ""));
        }

        [Fact]
        public void CalculateHighestFrequency_Text_Null_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Result.CalculateHighestFrequency(null));
        }

        [Fact]
        public void CalculateHighestFrequency_Text_Empty_Throws_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Result.CalculateHighestFrequency(""));
        }

        [Fact]
        public void CalculateMostFrequentNWords_Text_Null_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Result.CalculateMostFrequentNWords(null, _nthValue));
        }

        [Fact]
        public void CalculateMostFrequentNWords_Text_Empty_Throws_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Result.CalculateMostFrequentNWords("", _nthValue));
        }

        [Fact]
        public void Frequency_Of_Word_Return_Occurence_Of_Specified_Word()
        {
            var wordFrequency = Result.CalculateFrequencyForWord(_phrase, _word);

            Assert.Equal(2, wordFrequency);
        }

        [Fact]
        public void Frequency_Of_Word_Word_Has_Leading_Trailing_Whitespace_Return_Occurence_Of_Specified_Word()
        {
            var wordFrequency = Result.CalculateFrequencyForWord(_phrase, _whitespaceWord);

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

        [Fact]
        public void Most_Frequent_N_Returns_List_Of_N_Words_Numeric_Values_Excluded()
        {
            var wordFrequency = Result.CalculateMostFrequentNWords(_numericPhrase, _nthValue);

            Assert.Equal("the", wordFrequency[0].Word);
            Assert.Equal(2, wordFrequency[0].Frequency);
            Assert.Equal("lake", wordFrequency[1].Word);
            Assert.Equal(1, wordFrequency[1].Frequency);
            Assert.Equal("over", wordFrequency[2].Word);
            Assert.Equal(1, wordFrequency[2].Frequency);
        }

        [Fact]
        public void Most_Frequent_N_Returns_List_Of_N_Words_Guid_Values_Excluded()
        {
            var wordFrequency = Result.CalculateMostFrequentNWords(_guidPhrase, _nthValue);

            Assert.Equal("the", wordFrequency[0].Word);
            Assert.Equal(2, wordFrequency[0].Frequency);
            Assert.Equal("lake", wordFrequency[1].Word);
            Assert.Equal(1, wordFrequency[1].Frequency);
            Assert.Equal("over", wordFrequency[2].Word);
            Assert.Equal(1, wordFrequency[2].Frequency);
        }

        [Fact]
        public void Most_Frequent_N_Returns_List_Of_N_Words_NumerWords_Values_Excluded()
        {
            var wordFrequency = Result.CalculateMostFrequentNWords(_charnumberPhrase, _nthValue);

            Assert.Equal("the", wordFrequency[0].Word);
            Assert.Equal(2, wordFrequency[0].Frequency);
            Assert.Equal("lake", wordFrequency[1].Word);
            Assert.Equal(1, wordFrequency[1].Frequency);
            Assert.Equal("over", wordFrequency[2].Word);
            Assert.Equal(1, wordFrequency[2].Frequency);
        }

        [Fact]
        public void Most_Frequent_N_Returns_List_Of_N_Words_SpecialChar_Values_Excluded()
        {
            var wordFrequency = Result.CalculateMostFrequentNWords(_specialCharPhrase, _nthValue);

            Assert.Equal("the", wordFrequency[0].Word);
            Assert.Equal(2, wordFrequency[0].Frequency);
            Assert.Equal("lake", wordFrequency[1].Word);
            Assert.Equal(1, wordFrequency[1].Frequency);
            Assert.Equal("over", wordFrequency[2].Word);
            Assert.Equal(1, wordFrequency[2].Frequency);
        }
    }
}
