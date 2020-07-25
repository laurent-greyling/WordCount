using CommandLine;
using System;
using WordCount.Factories;

namespace WordCount
{
    class Program
    {
        static void Main(string[] args)
        {
            var wordFrequencyAnalyzer = WordFrequencyAnalyzerFactory.WordFrequencyAnalyzer();

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    if (string.IsNullOrWhiteSpace(o.TextToUse) && string.IsNullOrWhiteSpace(o.WordToUse) && string.IsNullOrWhiteSpace(o.WordToUse))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("No valid arguments for evaluation provided ");
                        return; 
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Starting process");
                    Console.ForegroundColor = ConsoleColor.White;

                    try
                    {
                        if (!string.IsNullOrWhiteSpace(o.TextToUse) && !string.IsNullOrWhiteSpace(o.WordToUse))
                        {
                            var wordFrequency = wordFrequencyAnalyzer.CalculateFrequencyForWord(o.TextToUse, o.WordToUse);
                            Console.WriteLine($"The word '{o.WordToUse}' occurred '{wordFrequency}' times");
                        }

                        if (!string.IsNullOrWhiteSpace(o.TextToUse))
                        {
                            var highestFrequency = wordFrequencyAnalyzer.CalculateHighestFrequency(o.TextToUse);
                            Console.WriteLine($"The highest frequency of a word in this paragraph is '{highestFrequency}' times");
                        }

                        if (!string.IsNullOrWhiteSpace(o.TextToUse) && o.WordOnAverageCount > 0)
                        {
                            var wordsOnNthOccurence = wordFrequencyAnalyzer.CalculateMostFrequentNWords(o.TextToUse, o.WordOnAverageCount);
                            Console.WriteLine($"Words that occured n={o.WordOnAverageCount} times:");

                            foreach (var item in wordsOnNthOccurence)
                            {
                                Console.WriteLine($"{item.Word} : {item.Frequency}");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"ERROR: {e}");
                        return;
                    }                   
                });

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Process successfully completed");
            Console.ReadKey();
        }
    }
}
