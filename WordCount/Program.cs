using CommandLine;
using System;
using System.IO;
using System.Text;
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
                    try
                    {
                        if (string.IsNullOrWhiteSpace(o.TextToUse) && string.IsNullOrWhiteSpace(o.WordToUse) && string.IsNullOrWhiteSpace(o.WordToUse) && string.IsNullOrWhiteSpace(o.PathToTextFile))
                        {
                            throw new Exception("No valid arguments for evaluation provided");
                        }

                        var textToEvaluate = TextToEvalate(o.TextToUse, o.PathToTextFile);

                        if (string.IsNullOrWhiteSpace(textToEvaluate))
                        {
                            throw new Exception("No text provided to evaluate");
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Starting process");
                        Console.ForegroundColor = ConsoleColor.White;

                        if (!string.IsNullOrWhiteSpace(textToEvaluate) && !string.IsNullOrWhiteSpace(o.WordToUse))
                        {
                            var wordFrequency = wordFrequencyAnalyzer.CalculateFrequencyForWord(textToEvaluate, o.WordToUse);
                            Console.WriteLine($"The word '{o.WordToUse}' occurred '{wordFrequency}' times");
                        }

                        if (!string.IsNullOrWhiteSpace(textToEvaluate))
                        {
                            var highestFrequency = wordFrequencyAnalyzer.CalculateHighestFrequency(textToEvaluate);
                            Console.WriteLine($"The highest frequency of a word in this paragraph is '{highestFrequency}' times");
                        }

                        if (!string.IsNullOrWhiteSpace(textToEvaluate) && o.WordOnAverageCount > 0)
                        {
                            var wordsOnNthOccurence = wordFrequencyAnalyzer.CalculateMostFrequentNWords(textToEvaluate, o.WordOnAverageCount);
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

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Process successfully completed");                   
                });

            Console.ReadKey();
        }

        /// <summary>
        /// If we provide text and a file of large text, we append them and evaluate the entire string
        /// Will also return one or the other if only one was provided
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pathToTextFile"></param>
        /// <returns></returns>
        private static string TextToEvalate(string text, string pathToTextFile)
        {
            if (string.IsNullOrWhiteSpace(text) && string.IsNullOrWhiteSpace(pathToTextFile))
            {
                return string.Empty;
            }

            var textToEvaluate = new StringBuilder();
            textToEvaluate.Append(text);

            //If there is a text file and we do not append a space to the initial string, the words will
            //be against each other and not counted. This is to allow so they are seen as independent
            textToEvaluate.Append(" ");

            if (!string.IsNullOrWhiteSpace(pathToTextFile))
            {
                var textInFile = File.ReadAllText(pathToTextFile);
                textToEvaluate.Append(textInFile);
            }
            
            return textToEvaluate.ToString();
        }
    }
}
