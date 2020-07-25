using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordCount.Services;
using Xunit;

namespace WordCount.Tests
{
    public class Class1
    {
        private Mock<IWordFrequencyAnalyzer> _mockedFrequencyAnalyser;
        private string _phrase = "Hallo World, the wOrld is ours";
        private string _word = "world";
        private WordFrequencyAnalyzer Result;

        public Class1()
        {
            _mockedFrequencyAnalyser = new Mock<IWordFrequencyAnalyzer>();
            _mockedFrequencyAnalyser.Setup(x => x.CalculateFrequencyForWord(_phrase, _word)).Returns(It.IsAny<int>());

            Result = new WordFrequencyAnalyzer();
        }

        [Fact]
        public void x() 
        {
            var c = Result.CalculateFrequencyForWord(_phrase, _word);

            Assert.Equal(2, c);
        }
    }
}
