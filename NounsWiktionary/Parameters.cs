using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NounsWiktionary
{
    /// <summary>
    /// Classe contenant les paramètres d'appels de l'application
    /// </summary>
    internal class Parameters
    {
        /// <summary>
        /// Chemin XML d'entrée
        /// </summary>
        public String Input { get; private set; }

        /// <summary>
        /// Chemin de sortie
        /// </summary>
        public String Output { get; private set; }

        /// <summary>
        /// Construit une nouvelle instance de la classe
        /// </summary>
        private Parameters(String input, String output)
        {
            this.Input = input;
            this.Output = output;
        }


        /// <summary>
        /// Vérifie et récupère les paramètres de la ligne de commande
        /// </summary>
        public static Parameters FromCommandLine(String[] args)
        {
            if (args == null || args.Length == 0 || args.Length > 2)
                return null;

            String input = args[0];
            if (!File.Exists(input))
                throw new ArgumentException($"Le fichier '{input}' n'existe pas.");

            String output = String.Empty;
            if (args.Length == 2)
            {
                String outputDirectory = Path.GetDirectoryName(output);
                if (!Directory.Exists(outputDirectory))
                    throw new ArgumentException($"Le dossier {outputDirectory} n'existe pas.");

                output = args[1];
            }
            else
                output = Path.ChangeExtension(input, ".txt");

            return new Parameters(input, output);
        }
    }
}
