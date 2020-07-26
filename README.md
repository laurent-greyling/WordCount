# WordCount

This is a small application to count the frequency of words appearing.

It will:

- Show the occurence of a specified word.
- Show the frequency of the word that occured the most
- Show a list of words based on N value given. 
  - The words will appear in alphabetical order based on highest to lowest frequency

There are several components to this project 

- WordCount Console
- WordCount Library
- WordCount Tests

## Console App
The console app uses the `WordCount.Library` to evaluate the above mentioned frequency of word(s).

To run the console app you need to provide it the following arguments:

- `-t` or `--text` | _string_: The text that need to be analysed for word frequency. This is meant for a small evaluation set
- `w` or `--word` | _string_: The specified word the frequency should be calculated for
- `n` or `--nth` | _int_: The most frequent 'n' words in the text
- `p` or `--path` | _string_: Path to file with text to be analysed. For large body of text

__Note__ if you provide both `t` and `p`, the application will concat them and evaluate it in it's entirety.

### Run the app
Running the app can happen in two ways:

1. When you have it open in visual studio, 
   - right click on the app `WordCount`:
   - Make it the startup project
   - Go to properties > debug
   - In the `Command Line Arguments` set the following
   ```
   -w <word to evaluate> -t <text to evaluate> -n <n> -p <path to text file>
   ```
   - Press F5

2. When running from a `command line prompt`
   - Navigate to where `WordCount.exe` lives (after release build)
   - Run
   ```
   WordCount.exe -w <word to evaluate> -t <text to evaluate> -n <n> -p <path to text file>
   ```

__Note__ in this project a large txt file is included named `IpsumLargeString.txt`.

The output should look something like this:

![image](https://user-images.githubusercontent.com/17876815/88465060-146dbc00-cec0-11ea-8a76-bacd87ed5f15.png)

## Library
A library used by Console app and tests, holding the implementation of:

- CalculateFrequencyForWord
- CalculateHighestFrequency
- CalculateMostFrequentNWords

To use this library in another project, you can build the .dll via release build and implement in another project. Can use the Console app as a guide.

## Tests
The `WordCount.Tests` is a small application that tests the implementation of the `WordCount.Library`. It uses `XUnit` as a testing framework.

It tests the following:
- Most_Frequent_N_Returns_List_Of_N_Words_With_Frequency
- Highest_Frequency_Of_Word_Return_Count_Of_Highest_Occurence_Of_Word
- Frequency_Of_Word_Return_Occurence_Of_Specified_Word
- CalculateMostFrequentNWords_Text_Empty_Throws_ArgumentException
- CalculateMostFrequentNWords_Text_Null_Throws_ArgumentNullException
- CalculateHighestFrequency_Text_Empty_Throws_ArgumentException
- CalculateHighestFrequency_Text_Null_Throws_ArgumentNullException
- CalculateFrequencyForWord_Word_Empty_Throws_ArgumentException
- CalculateFrequencyForWord_Word_Null_Throws_ArgumentNullException
- CalculateFrequencyForWord_Text_Empty_Throws_ArgumentException
- CalculateFrequencyForWord_Text_Null_Throws_ArgumentNullException
- Most_Frequent_N_Returns_List_Of_N_Words_Numeric_Values_Excluded
- Most_Frequent_N_Returns_List_Of_N_Words_Guid_Values_Excluded
- Most_Frequent_N_Returns_List_Of_N_Words_NumerWords_Values_Excluded

## 3rd Party Libraries used in this project
- `XUnit` => Unit testing framework
- `CommandLineParser` => to pass command line options easily