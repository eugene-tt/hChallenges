using System.Linq;

namespace Challenge1
{
    public class ReadableWithLinq
    {       
        /// <summary>
        /// This is a LINQ version. It takes all elements of the array preceding the matching one,
        /// then appends all the elements following it.
        /// </summary>
        public void RemoveFirst(string match, ref string[] items)
        {
            if(items == null)
            {
                // Invalid input
                return;
            }

            var matchFoundAt = int.MaxValue;
            items = items
                .TakeWhile((item, i) => {
                    var isMatch = item == match;
                    matchFoundAt = isMatch ? i : int.MaxValue;
                    return !isMatch;
                })
                .Concat(items.SkipWhile((item, i) => i <= matchFoundAt))
                .ToArray();
        }
    }
}
