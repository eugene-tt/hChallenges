using System;
using System.Linq;

namespace Challenge1
{

    public class Unreadable
    {
        /// <summary>
        /// Scans array <paramref name="array"/> for the first occurrence of
        /// <paramref name="element"/> and removes it
        /// </summary>
        /// <param name="element">String to find in the array</param>
        /// <param name="array">Array to scan for the matching string</param>
        /// <remarks>The result is written back to the source array</remarks>
        /// <remarks>The function crashes when the source array doesn't include the element</remarks>
        public void Do(string element, ref string[] array)
        {
            // Parameter
            string x = element;
            string[] a = array;

            // Logic
            string[] b = new string[a.Length - 1];
            bool flag = false;
            for (int i = 0; i < a.Length; i++)
            {
                if (flag)
                    b[i - 1] = a[i];
                else
                {
                    flag = a[i] == x;

                    if (!flag)
                        b[i] = a[i];
                }
            }
            array = b;
        }
    }
}
