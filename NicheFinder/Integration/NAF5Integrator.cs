using NicheFinder.DataBase;
using NicheFinder.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheFinder.Integration
{
    /// <summary>
    /// Classe d'intégration de la liste de référence des NAF5 dans la BDD
    /// </summary>
    public static class NAF5Integrator
    {
        /// <summary>
        /// S'assure que les NAF5 sont bien intégrés en BDD
        /// </summary>
        public static void EnsureIntegrated()
        {
            using (IDbContext ctx = NicheContext.Create())
            {
                if (ctx.Nouns.Any())
                    return;
            }

            IEnumerable<NAF5> nafs = ParseNAF5(@"C:\Users\W\Source\Repos\nichefinder\Data\liste_naf2008_n5.csv");
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
        static IEnumerable<NAF5> ParseNAF5(String naf5Files)
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
