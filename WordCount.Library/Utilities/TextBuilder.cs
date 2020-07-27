using System.IO;
using System.Text;

namespace WordCount.Library.Utilities
{
    public static class TextBuilder
    {
        /// <summary>
        /// If we provide text and a file of large text, we append them and evaluate the entire string
        /// Will also return one or the other if only one was provided
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pathToTextFile"></param>
        /// <returns></returns>
        public static string TextToEvalate(string text, string pathToTextFile)
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
