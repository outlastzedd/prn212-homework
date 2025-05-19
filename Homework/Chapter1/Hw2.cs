// A utility to analyze text files and provide statistics
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Homework.Chapter1
{
    class Hw2
    {
        public static void Run(string[] args)
        {
            Console.WriteLine("File Analyzer - .NET Core");
            Console.WriteLine("This tool analyzes text files and provides statistics.");
            
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a file path as a command-line argument.");
                Console.WriteLine("Example: dotnet run myfile.txt");
                return;
            }
            
            string filePath = args[0];
            
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File '{filePath}' does not exist.");
                return;
            }
            
            try
            {
                Console.WriteLine($"Analyzing file: {filePath}");
                
                // Read the file content
                string content = File.ReadAllText(filePath);
                
                // TODO: Implement analysis functionality
                // 1. Count words
                int lineCount = File.ReadAllLines(filePath).Length;
                Console.WriteLine($"Number of lines: {lineCount}");
                // 2. Count characters (with and without whitespace)
                int charCount = content.Length;
                int charCountWithoutSpaces = content.Replace(" ", "").Length;
                Console.WriteLine($"Number of characters (with spaces): {charCount}");
                Console.WriteLine($"Number of characters (without spaces): {charCountWithoutSpaces}");
                // 3. Count sentences
                int sentenceCount = content.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
                Console.WriteLine($"Number of sentences: {sentenceCount}");
                // 4. Identify most common words
                var words = content.Split(new[] { ' ', '\n', '\r', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
                var wordCount = new Dictionary<string, int>();
                foreach (var word in words)
                {
                    var lowerWord = word.ToLower();
                    if (wordCount.ContainsKey(lowerWord))
                        wordCount[lowerWord]++;
                    else
                        wordCount[lowerWord] = 1;
                }
                var mostCommonWords = wordCount.OrderByDescending(kvp => kvp.Value).Take(10);
                Console.WriteLine("Most common words:");
                foreach (var kvp in mostCommonWords)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
                // 5. Average word length
                double averageWordLength = words.Average(w => w.Length);
                Console.WriteLine($"Average word length: {averageWordLength:F2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during file analysis: {ex.Message}");
            }
        }
    }
}