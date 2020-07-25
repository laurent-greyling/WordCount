using WordCount.Services;
using Xunit;

namespace WordCount.Tests.Services
{
    public class WordFrequencyAnalyzerTest
    {
        private string _phrase = "Hallo World, the wOrld is ours";
        private string _word = "world";
        private WordFrequencyAnalyzer Result;

        [Fact]
        public void Frequency_Of_Word_Return_Occurence_Of_Specified_Word() 
        {
            Result = new WordFrequencyAnalyzer();

            var wordFrequency = Result.CalculateFrequencyForWord(_phrase, _word);

            Assert.Equal(2, wordFrequency);
        }
    }
}
