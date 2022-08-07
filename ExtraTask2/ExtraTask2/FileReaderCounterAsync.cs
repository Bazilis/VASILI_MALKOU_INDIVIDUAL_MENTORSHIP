using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExtraTask2
{
    internal class FileReaderCounterAsync
    {
        private readonly static string _basePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;

        public async static Task ReadAndCountFile1Async()
        {
            string[] lines = await File.ReadAllLinesAsync(Path.Combine(_basePath, "StringLines1.txt"));

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

            Console.WriteLine($"Data from file 1 => {resultString} = {operationResult} (async)");
        }

        public async static Task ReadAndCountFile2Async()
        {
            string[] lines = await File.ReadAllLinesAsync(Path.Combine(_basePath, "StringLines2.txt"));

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

            Console.WriteLine($"Data from file 2 => {resultString} = {operationResult} (async)");
        }
    }
}
