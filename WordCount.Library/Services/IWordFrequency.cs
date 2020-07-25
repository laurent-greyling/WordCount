namespace WordCount.Library.Services
{
    /// <summary>
    /// Hold the word and the frequency the word occurs
    /// </summary>
    public interface IWordFrequency
    {
        /// <summary>
        /// A word in a sentence
        /// </summary>
        string Word { get; }

        /// <summary>
        /// Frequency of a specific word
        /// </summary>
        int Frequency { get; }
    }
}
