﻿using WordCount.Library.Services;

namespace WordCount.Factories
{
    public static class WordFrequencyAnalyzerFactory
    {
        public static IWordFrequencyAnalyzer WordFrequencyAnalyzer() 
        {
            return new WordFrequencyAnalyzer();
        }
    }
}
