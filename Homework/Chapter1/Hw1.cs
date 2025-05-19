// A simple calculator app that takes commands from the command line
using System;

namespace Homework.Chapter1
{
    class Hw1
    {
        public static void Run(string[] args)
        {
            Console.WriteLine("Welcome to .NET Core Calculator!");
            Console.WriteLine("Available operations: add, subtract, multiply, divide");
            Console.WriteLine("Example usage: add 5 3");
            Console.WriteLine("Type 'exit' to quit");
            
            bool running = true;
            while (running)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(input))
                    continue;
                    
                string[] parts = input.Trim().Split(' ');
                
                if (parts[0].ToLower() == "exit")
                {
                    running = false;
                    continue;
                }
                
                if (parts.Length != 3)
                {
                    Console.WriteLine("Invalid input format. Please use: operation number1 number2");
                    continue;
                }
                
                // TODO: Implement parsing numbers and performing calculations
                // This is where you will add your code
                double result, num1 = Double.Parse(parts[1]), num2 = Double.Parse(parts[2]);
                switch (parts[0].ToLower())
                {
                    case "add":
                        result = num1 + num2;
                        Console.WriteLine("Result for " + num1 + " + " + num2 + ": " + result);
                        break;
                    case "subtract":
                        result = num1 - num2;
                        Console.WriteLine("Result for " + num1 + " - " + num2 + ": " + result);
                        break;
                    case "multiply":
                        result = num1 * num2;
                        Console.WriteLine("Result for " + num1 + " * " + num2 + ": " + result);
                        break;
                    case "divide":
                        result = num1 / num2;
                        Console.WriteLine("Result for " + num1 + " / " + num2 + ": " + result);
                        break;
                    default:
                        Console.WriteLine("Unknown operation: " + parts[0]);
                        continue;
                }
            }
        }
    }
}