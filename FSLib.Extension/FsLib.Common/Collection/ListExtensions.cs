using System.Collections.Generic;

namespace System.Collection
{
    public static class ListExtensions
    {
        public static void ForEach()
        {
            List<string> list = new List<string>();
            list.ForEach(s =>
            {
                s= s + 1;
            });
        }
    }
}