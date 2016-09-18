using System;

namespace NicheFinder.Utils
{
    public static class StringNormalizer
    {
        public static String RemoveDiacritics(this String me)
        {
            Char[] buffer = new Char[me.Length];
            int index = 0;
            foreach (Char c in me)
            {
                if (_lookup[c])
                {
                    buffer[index] = c;
                    index++;
                }
            }

            return new String(buffer, 0, index);
        }

        static readonly Boolean[] _lookup = InitLookup();

        static Boolean[] InitLookup()
        {
            Boolean[] res = new Boolean[65536];
            for (Char c = '0'; c <= '9'; c++)
                res[c] = true;
            for (Char c = 'A'; c <= 'Z'; c++)
                res[c] = true;
            for (Char c = 'a'; c <= 'z'; c++)
                res[c] = true;

            res['.'] = true;
            res['_'] = true;

            return res;
        }
    }
}
