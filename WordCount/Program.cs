using CommandLine;
using System;
using WordCount.Factories;
using WordCount.Library.Utilities;

namespace WordCount
{
    class Program
    {
        /// <summary>
        /// To run optimally:
        /// To Debug
        /// Right click on the project WordCount
        /// Make it the startup project
        /// Go to properties > debug
        /// In the Command Line Arguments set the following
        /// -w 'word to evaluate' -t 'text to evaluate' -n 'n' -p 'path to text file'
        /// Press F5
        /// 
        /// To Run Release
        /// Navigate to where `WordCount.exe` lives (after release build)
        /// Run - WordCount.exe -w 'word to evaluate' -t 'text to evaluate' -n 'n' -p 'path to text file'
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var wordFrequencyAnalyzer = WordFrequencyAnalyzerFactory.WordFrequencyAnalyzer();

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options =>
                {
                    try
                    {
                        options = AppOptions(options);

                        var textToEvaluate = TextBuilder.TextToEvalate(options.TextToUse, options.PathToTextFile);

                        if (string.IsNullOrWhiteSpace(textToEvaluate))
                        {
                            throw new Exception("No text provided to evaluate");
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Starting process");
                        Console.ForegroundColor = ConsoleColor.White;

                        if (!string.IsNullOrWhiteSpace(textToEvaluate) && !string.IsNullOrWhiteSpace(options.WordToUse))
                        {
                            var wordToUse = options.WordToUse.Trim();
                            if (Ensure.IsNotValidWord(wordToUse))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"The requested word '{wordToUse}' is invalid, a word should be between A-Z or a-z");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else 
                            {
                                var wordFrequency = wordFrequencyAnalyzer.CalculateFrequencyForWord(textToEvaluate, wordToUse);
                                Console.WriteLine($"The word '{wordToUse}' occurred '{wordFrequency}' times");
                            }                            
                        }

                        if (!string.IsNullOrWhiteSpace(textToEvaluate))
                        {
                            var highestFrequency = wordFrequencyAnalyzer.CalculateHighestFrequency(textToEvaluate);
                            Console.WriteLine($"The highest frequency of a word in this paragraph is '{highestFrequency}' times");
                        }

                        if (!string.IsNullOrWhiteSpace(textToEvaluate) && options.WordOnAverageCount > 0)
                        {
                            var wordsOnNthOccurence = wordFrequencyAnalyzer.CalculateMostFrequentNWords(textToEvaluate, options.WordOnAverageCount);
                            Console.WriteLine($"Words that occured n={options.WordOnAverageCount} times:");

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
        /// Help text to show if user didn't read the document provided
        /// </summary>
        private static void HelpText()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=> To optimally run/debug this app");

#if DEBUG
            Console.WriteLine("=> Right click on the project WordCount");
            Console.WriteLine("=> Make it the startup project");
            Console.WriteLine("=> Go to properties > debug");
            Console.WriteLine("=> In the Command Line Arguments set the following");
            Console.WriteLine("=> -w <word to evaluate> -t <text to evaluate> -n <n> -p <path to text file>");
            Console.WriteLine("=> Press F5");
#else
            Console.WriteLine("=> Navigate to where `WordCount.exe` lives (after release build)");
            Console.WriteLine("=> Run - WordCount.exe -w <word to evaluate> -t <text to evaluate> -n <n> -p <path to text file>");
#endif            
        }

        /// <summary>
        /// If no commandline options were given, allow user to enter text free hand
        /// Add these free hand options to the actual options of the app.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        private static Options AppOptions(Options options)
        {
            var isOptionsNull = Ensure.IsCommandArgumentsNotNull(options.TextToUse,
                            options.WordToUse,
                            options.WordOnAverageCount,
                            options.PathToTextFile);

            if (isOptionsNull)
            {
                HelpText();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Please provide the text you want analysed:");
                options.TextToUse = Console.ReadLine();
                Console.WriteLine("Please enter the word you want the frequency of:");
                options.WordToUse = Console.ReadLine();
                Console.WriteLine("Please enter the 'n' number:");
                try
                {
                    options.WordOnAverageCount = int.Parse(Console.ReadLine());
                }
                catch
                {
                    //if no valid int value given default to 0, nothing will be displayed                   
                    options.WordOnAverageCount = 0;
                }

                Console.WriteLine("Please enter path to text file (if any exist):");
                options.PathToTextFile = Console.ReadLine();

                isOptionsNull = Ensure.IsCommandArgumentsNotNullOrEmptyString(options.TextToUse,
                options.WordToUse,
                options.PathToTextFile);

                if (isOptionsNull)
                {
                    throw new Exception("No valid arguments for evaluation provided");
                }
            }

            return options;
        }
    }
}
