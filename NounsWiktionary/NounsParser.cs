using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NounsWiktionary
{
    /// <summary>
    /// Classe de parsing du fichier wikipedia / wiktionaire
    /// </summary>
    internal static class NounsParser
    {
        /// <summary>
        /// Parse la liste des noms du dump xml wikipedia pour en récupérer la liste des noms qui y sont décris
        /// </summary>
        public static IEnumerable<String> FromWikipediaFile(String wikiXmlDumpPath)
        {
            IList<String> res = new List<String>();

            using (StreamReader sr = new StreamReader(wikiXmlDumpPath, Encoding.UTF8))
            {
                Boolean tryFoundNoun = false;
                Int32 count = 0, lineNumber = 0;
                String line = null;

                try
                {
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();

                        if (line != null)
                        {
                            if (tryFoundNoun)
                            {
                                if (line.StartsWith(@"'''") && line.Length > 3)
                                {
                                    Int32 endIndex = line.IndexOf(@"'''", 3);
                                    if (endIndex >= 3)
                                    {
                                        String word = line.Substring(3, endIndex - 3);
                                        if (!word.Contains(@"{{") && !word.Contains("[[") && !word.StartsWith(".") && !word.Contains("&lt;") && !String.IsNullOrWhiteSpace(word))
                                            res.Add(word.ToLower().Trim());
                                        tryFoundNoun = false;
                                    }
                                    else
                                        count++;
                                }
                                else
                                    count++;

                                tryFoundNoun = count <= 5;
                            }

                            if (line.Contains(@"nom|fr"))
                            {
                                tryFoundNoun = true;
                                count = 0;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new ParseException(lineNumber, line, ex);
                }
            }

            return res.Distinct().OrderBy(s => s).ToList();
        }

        static String RemoveDiacritics(this String me)
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
