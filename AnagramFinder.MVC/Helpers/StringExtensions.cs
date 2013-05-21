using System.Collections;

namespace AnagramFinder.MVC.Helpers
{
    public static class StringExtensions
    {
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}