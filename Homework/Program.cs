// See https://aka.ms/new-console-template for more information
using System;
using Homework.Chapter1;

namespace Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            string choice;
            Console.WriteLine("Select the program to run:");
            Console.WriteLine("1. Calculator");
            Console.WriteLine("2. File Analyzer");
            Console.WriteLine("3. Task Scheduler");
            Console.WriteLine("Type 'exit' to quit");
            Console.Write("Enter your choice: ");
            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Hw1.Run(args);
                    break;
                case "2":
                    string[] files = { "D:\\++ FPTU\\PRN212\\Homework\\Homework\\Chapter1\\example.txt" };
                    Hw2.Run(files);
                    break;
                case "3":
                    Hw3.Run(args);
                    break;
                case "exit":
                    Console.WriteLine("Exiting the program.");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                    break;
            }
        }
    }
}