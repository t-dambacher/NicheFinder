using NicheFinder.DataBase;
using System;
using System.Linq;

namespace NicheFinder.Integration
{
    /// <summary>
    /// Classe d'intégration des descriptions associées aux NAF
    /// </summary>
    public class NAFDescriptionsIntegrator : Integrator
    {
        /// <summary>
        /// Priorité à appliquer à l'ordre d'utilisation de la classe
        /// </summary>
        public override Int32 Priority { get { return 1; } }

        /// <summary>
        /// Vérifie si les données gérées par la classe ont déjà été integrée
        /// </summary>
        public override Boolean IsIntegrated()
        {
            using (IDbContext ctx = NicheContext.Create())
            {
                return ctx.NAF5Descriptions.Any();
            }
        }

        /// <summary>
        /// Intégère les données gérées par la classe
        /// </summary>
        public override void Integrate()
        {
            // todo tda
        }
    }
}
