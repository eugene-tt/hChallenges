using System.Collections.Generic;

namespace Challenge1
{
    public class ReadableWithLists
    {
        /// <summary>
        /// This refactored version of Unreadable.Do() is using a List<string>
        /// </summary>
        public void RemoveFirst(string match, ref string[] items)
        {
            if (items == null)
            {
                // Invalid input
                return;
            }

            var result = new List<string>();
            var matchFound = false;
            foreach(var item in items)
            {
                bool takeThisElement = matchFound || item != match;
                if(takeThisElement)
                {
                    result.Add(item);
                }
                else
                {
                    matchFound = true;
                }
            }

            items = result.ToArray();
        }
    }
}