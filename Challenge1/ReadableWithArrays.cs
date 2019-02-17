namespace Challenge1
{
    public class ReadableWithArrays
    {
        /// <summary>
        /// This refactored version of Unreadable.Do() is also using arrays
        /// </summary>
        public void RemoveFirst(string match, ref string[] items)
        {
            if (items == null)
            {
                // Invalid input
                return;
            }

            var result = new string[items.Length - 1];
            bool hasMatch = false;
            for (int i = 0; i < items.Length; i++)
            {
                if (hasMatch)
                {
                    result[i - 1] = items[i];
                }
                else
                {
                    hasMatch = items[i] == match;

                    if (!hasMatch && i < items.Length - 1) // Fix the crash in Do()
                    {
                        result[i] = items[i];
                    }
                }
            }

            if(hasMatch)
            {
                // If we found a match, replace the source array with the modified
                // version, otherwise (if nothing was found) leave it as it was
                items = result;
            }
        }
    }
}