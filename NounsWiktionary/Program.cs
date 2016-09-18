using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NounsWiktionary
{
    /// <summary>
    /// Programme permettant de parser et récupérer la liste des noms à partir d'un dumb du wiktionaire
    /// </summary>
    class Program
    {
        static void Main(String[] args)
        {
            try
            {
                // Récupération des paramètres de la ligne de commande
                Parameters parameters = Parameters.FromCommandLine(args);
                if (parameters == null)
                    throw new ArgumentException("Usage : ExtractNouns <input.xml> [output.txt]");

                // Parsing
                IEnumerable<String> frNouns = NounsParser.FromWikipediaFile(parameters.Input);
                if (frNouns == null || frNouns.Count() == 0)
                    throw new Exception("Aucune donnée récupérée");

                Console.WriteLine($"{frNouns.Count()} noms récupérés.");

                // Sauvegarde
                File.WriteAllText(
                    parameters.Output, String.Join(Environment.NewLine, frNouns), Encoding.UTF8
                );

                Console.WriteLine("OK");
            }
            catch (ArgumentException aEx)
            {
                Console.WriteLine(aEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur est survenue : " + ex.Message);
            }

            Console.ReadKey();
        }
    }
}
