using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge2.Utils
{
    public class ConsoleUtils
    {
        public static string ReadString(string prompt, Action onCancel = null, Func<string, bool> isValid = null)
        {
            if (string.IsNullOrEmpty(prompt))
            {
                throw new ArgumentException(nameof(prompt));
            }

            string input = string.Empty;
            bool accepted = false;
            bool canceled = false;

            while (!accepted && !canceled)
            {
                Console.Write($"{prompt} (Empty to cancel):");
                input = Console.ReadLine();

                canceled = string.IsNullOrEmpty(input);
                if (!canceled)
                {
                    if (isValid != null)
                    {
                        accepted = isValid.Invoke(input);
                    }
                    else
                    {
                        accepted = true;
                    }

                    if(!accepted)
                    {
                        Console.WriteLine("Invalid input, please try again");
                    }
                }
            }

            if (canceled)
            {
                onCancel?.Invoke();
                return null;
            }

            return input;
        }

        public static int ReadPositiveInt(string prompt, Action onCancel = null)
        {
            int inputInt = int.MinValue;

            var input = ReadString(
                prompt,
                onCancel,
                (s) => 
                {
                    if(!int.TryParse(s, out inputInt))
                    {
                        return false;
                    }
                    return inputInt > 0;
                });

            return inputInt;
        }
    }
}
