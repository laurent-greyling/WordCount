using CommandLine;

namespace WordCount
{
    public class Options
    {
        [Option('t', "text", Required = false, HelpText = "The text that need to be analysed for word frequency")]
        public string TextToUse { get; set; }

        [Option('w', "word", Required = false, HelpText = "The specified word the frequency should be calculated for")]
        public string WordToUse { get; set; }

        [Option('n', "nth", Required = false, Default = 0, HelpText = "The most frequent 'n' words in the text")]
        public int WordOnAverageCount { get; set; }
    }
}
