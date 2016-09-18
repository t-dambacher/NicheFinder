using NicheFinder.DataBase;
using NicheFinder.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NicheFinder.Integration
{
    /// <summary>
    /// Classe d'intégration de la liste de référence des NAF5 dans la BDD
    /// </summary>
    public class NAF5Integrator : Integrator
    {
        /// <summary>
        /// Priorité à appliquer à l'ordre d'utilisation de la classe
        /// </summary>
        public override Int32 Priority { get { return 0; } }

        /// <summary>
        /// Chemin d'accès au CSV contenant les NAF à intégrer
        /// </summary>
        private readonly String _nafPath = @"C:\Users\W\Source\Repos\nichefinder\Data\liste_naf2008_n5.csv";

        /// <summary>
        /// Vérifie si les données gérées par la classe ont déjà été integrée
        /// </summary>
        /// <returns></returns>
        public override Boolean IsIntegrated()
        {
            using (IDbContext ctx = NicheContext.Create())
            {
                return ctx.Nouns.Any();
            }
        }

        /// <summary>
        /// Intégère les données gérées par la classe
        /// </summary>
        public override void Integrate()
        {
            IEnumerable<NAF5> nafs = ParseNAF5(this._nafPath);
            if (nafs == null || !nafs.Any())
                throw new InvalidOperationException("Impossible de trouver la liste des noms");

            using (IDbContext ctx = NicheContext.Create())
            {
                foreach (NAF5 naf in nafs)
                    ctx.Save(naf);

                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Parse le ficher contant les NAF5
        /// </summary>
        private IEnumerable<NAF5> ParseNAF5(String naf5Files)
        {
            IList<NAF5> res = new List<NAF5>();

            using (StreamReader sr = new StreamReader(naf5Files))
            {
                sr.ReadLine();  // On zap la 1ere ligne qui contient l'en-tete des colonnes

                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        String[] splited = line.Split(';');
                        if (splited.Length == 2)
                            res.Add(new NAF5(splited[0], splited[1]));
                    }
                }
            }

            return res;
        }
    }
}
