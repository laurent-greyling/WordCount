using CommandLine;

namespace WordCount
{
    public class Options
    {
        /// <summary>
        /// This has a character limit, so good for small amounts of text
        /// </summary>
        [Option('t', "text", Required = false, HelpText = "The text that need to be analysed for word frequency")]
        public string TextToUse { get; set; }

        [Option('w', "word", Required = false, HelpText = "The specified word the frequency should be calculated for")]
        public string WordToUse { get; set; }

        [Option('n', "nth", Required = false, Default = 0, HelpText = "The most frequent 'n' words in the text")]
        public int WordOnAverageCount { get; set; }

        [Option('p', "path", Required = false, HelpText = "Path to file with text to be analysed")]
        public string PathToTextFile { get; set; }
    }
}
