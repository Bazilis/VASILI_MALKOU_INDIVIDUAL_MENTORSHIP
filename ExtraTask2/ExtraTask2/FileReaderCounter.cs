using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExtraTask2
{
    internal class FileReaderCounter
    {
        private readonly static string _basePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;

        public static void ReadAndCountFile1()
        {
            string[] lines = File.ReadAllLines(Path.Combine(_basePath, "StringLines1.txt"));

            string[] stringNumbers = lines[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            string operation = lines[1];

            var doubleNumbers = new List<double>();

            foreach (string stringNumber in stringNumbers)
            {
                doubleNumbers.Add(Convert.ToDouble(stringNumber));
            }

            string resultString = string.Join(operation, stringNumbers);

            double operationResult = operation switch
            {
                "+" => doubleNumbers.Aggregate((x, y) => x + y),
                "-" => doubleNumbers.Aggregate((x, y) => x - y),
                "*" => doubleNumbers.Aggregate((x, y) => x * y),
                "/" => doubleNumbers.Aggregate((x, y) => x / y),
                _ => default
            };

            Console.WriteLine($"Data from file 1 => {resultString} = {operationResult} (multi-threaded)");
        }

        public static void ReadAndCountFile2()
        {
            string[] lines = File.ReadAllLines(Path.Combine(_basePath, "StringLines2.txt"));

            string[] stringNumbers = lines[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            string operation = lines[1];

            var doubleNumbers = new List<double>();

            foreach (string stringNumber in stringNumbers)
            {
                doubleNumbers.Add(Convert.ToDouble(stringNumber));
            }

            string resultString = string.Join(operation, stringNumbers);

            double operationResult = operation switch
            {
                "+" => doubleNumbers.Aggregate((x, y) => x + y),
                "-" => doubleNumbers.Aggregate((x, y) => x - y),
                "*" => doubleNumbers.Aggregate((x, y) => x * y),
                "/" => doubleNumbers.Aggregate((x, y) => x / y),
                _ => default
            };

            Console.WriteLine($"Data from file 2 => {resultString} = {operationResult} (multi-threaded)");
        }
    }
}
