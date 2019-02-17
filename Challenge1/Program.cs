using System;
using System.Collections.Generic;

namespace Challenge1
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1: What does Unreadable do?
            //    - It removes the first occurrence of the "element" in the "array" and crashes if none was found
            // 2: Refactor it for better readability
            //    - Please Find 3 different implementations in Readable_XXX classes

            var lookingForWord = "pizza";
            var testsCases = new List<string[]>
                {
                    new string[] { "a", "pizza", "b", "pizza", "c", "pizza" },
                    new string[] { "a", "b", "c", "pizza" },
                    new string[] { "a", "b", "c", "d" },
                    new string[] { "pizza", "b", "c", "d" }
                };

            // Implementation lambdas
            var implementations = new Dictionary<string, Func<string, string[], string[]>>
                {
                    { nameof(Unreadable),         (element, array) => { new Unreadable().Do(element, ref array); return array; } },
                    { nameof(ReadableWithArrays), (element, array) => { new ReadableWithArrays().RemoveFirst(element, ref array); return array; } },
                    { nameof(ReadableWithLists),  (element, array) => { new ReadableWithLists().RemoveFirst(element, ref array); return array; } },
                    { nameof(ReadableWithLinq),   (element, array) => { new ReadableWithLinq().RemoveFirst(element, ref array); return array; } },
                };

            // Tests for all of them :
            foreach(var test in testsCases)
            {
                Console.WriteLine($"Test: {string.Join(", ", test)}");

                foreach(var implementation in implementations)
                {
                    var implName = implementation.Key.PadRight(20, ' ');
                    var method = implementation.Value;

                    var tempArray = (string[])test.Clone();
                    try
                    {
                        tempArray = method(lookingForWord, tempArray);
                        Console.WriteLine($"\t{implName}: {string.Join(", ", tempArray)}");
                    }
                    catch(Exception)
                    {
                        Console.WriteLine($"\t{implName}: Crash");
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
