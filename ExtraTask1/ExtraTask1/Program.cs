using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtraTask1
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "ExtraTask1";
            Console.ForegroundColor = ConsoleColor.Green;

            //dictionary containing letters of the English alphabet as keys
            //each element of the dictionary has a value in the form of a linked list
            var namesDic = new Dictionary<char, LinkedList<string>>()
            {
                { 'a', new LinkedList<string>() }, { 'b', new LinkedList<string>() },
                { 'c', new LinkedList<string>() }, { 'd', new LinkedList<string>() },
                { 'e', new LinkedList<string>() }, { 'f', new LinkedList<string>() },
                { 'g', new LinkedList<string>() }, { 'h', new LinkedList<string>() },
                { 'i', new LinkedList<string>() }, { 'j', new LinkedList<string>() },
                { 'k', new LinkedList<string>() }, { 'l', new LinkedList<string>() },
                { 'm', new LinkedList<string>() }, { 'n', new LinkedList<string>() },
                { 'o', new LinkedList<string>() }, { 'p', new LinkedList<string>() },
                { 'q', new LinkedList<string>() }, { 'r', new LinkedList<string>() },
                { 's', new LinkedList<string>() }, { 't', new LinkedList<string>() },
                { 'u', new LinkedList<string>() }, { 'v', new LinkedList<string>() },
                { 'w', new LinkedList<string>() }, { 'x', new LinkedList<string>() },
                { 'y', new LinkedList<string>() }, { 'z', new LinkedList<string>() }
            };

            Console.WriteLine("Please, enter names separated by commas:");
            string text = Console.ReadLine();

            var words = text.Split(new char[] { ',' });

            var inputNames = new Queue<string>();

            foreach (var word in words)
            {
                inputNames.Enqueue(word.Trim(new char[] { '"', ' ' }));
            }

            //add names to linked lists according to their first characters
            foreach (var name in inputNames)
            {
                var firstChar = name.ToLower().FirstOrDefault();

                foreach (var keyValuePair in namesDic)
                {
                    if (keyValuePair.Key == firstChar)
                    {
                        keyValuePair.Value.AddLast(name);
                        break;
                    }
                }
            }

            Console.WriteLine("\nOutput:");
            foreach (var keyValuePair in namesDic)
            {
                //sort strings in linked lists alphabetically
                if (keyValuePair.Value.Count > 1)
                {
                    var tempLinkedList = new LinkedList<string>(keyValuePair.Value);
                    keyValuePair.Value.Clear();
                    var orderedEnumerable = tempLinkedList.OrderBy(x => x);
                    foreach (var str in orderedEnumerable)
                        keyValuePair.Value.AddLast(str);
                }

                //build output strings
                var namesString = new StringBuilder();
                foreach (var name in keyValuePair.Value)
                {
                    if (name == keyValuePair.Value.Last.Value)
                    {
                        namesString.Append($"\"{name}\"");
                    }
                    else
                    {
                        namesString.Append($"\"{name}\", ");
                    }
                }

                Console.WriteLine($"{keyValuePair.Key}: {namesString}");
            }

            Console.ReadLine();
        }
    }
}
