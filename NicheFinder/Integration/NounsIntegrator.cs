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
    /// Classe d'intégration de la liste de référence des noms dans la BDD
    /// </summary>
    public class NounsIntegrator : Integrator
    {
        /// <summary>
        /// Priorité à appliquer à l'ordre d'utilisation de la classe
        /// </summary>
        public override Int32 Priority { get { return 0; } }

        /// <summary>
        /// Chemin d'accès au CSV contenant les NAF à intégrer
        /// </summary>
        private readonly String _nounPath = @"C:\Users\W\Source\Repos\nichefinder\Data\nouns.fr.txt";

        /// <summary>
        /// Vérifie si les données gérées par la classe ont déjà été integrée
        /// </summary>
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
            IEnumerable<Noun> nouns = ParseNouns(_nounPath);
            if (nouns == null || !nouns.Any())
                throw new InvalidOperationException("Impossible de trouver la liste des noms");

            // On découpe en sous-listes pour ne pas saturer le DbContext avec des objets en cache
            IEnumerable<IEnumerable<Noun>> groups = CreateGroups(nouns);
            foreach (IEnumerable<Noun> group in groups)
            {
                using (IDbContext ctx = NicheContext.Create())
                {
                    foreach (Noun noun in group)
                        ctx.Save(noun);

                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Division de la liste globale en sous listes
        /// </summary>
        private IEnumerable<IEnumerable<Noun>> CreateGroups(IEnumerable<Noun> nouns)
        {
            List<Noun> res = new List<Noun>();
            for (Int32 i = 0, total = nouns.Count(); i < total; ++i)
            {
                res.Add(nouns.ElementAt(i));

                if (i % 1000 == 0 || i == total)
                {
                    yield return res;
                    res = new List<Noun>();
                }

            }
        }

        /// <summary>
        /// Parse le ficher contant les noms
        /// </summary>
        private IEnumerable<Noun> ParseNouns(String nounsFile)
        {
            IList<Noun> res = new List<Noun>();

            using (StreamReader sr = new StreamReader(nounsFile))
            {
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    if (!String.IsNullOrWhiteSpace(line))
                        res.Add(line.Trim());
                }
            }

            return res;
        }
    }
}
