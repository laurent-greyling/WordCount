namespace WordCount.Services
{
    public interface IWordFrequency
    {
        string Word { get; }
        int Frequency { get; }
    }
}
