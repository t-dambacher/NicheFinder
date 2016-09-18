using System;

namespace NicheFinder.Integration
{
    /// <summary>
    /// Classe de base d'intégration des données
    /// </summary>
    public abstract class Integrator
    {
        /// <summary>
        /// Priorité à appliquer à l'ordre d'utilisation de la classe
        /// </summary>
        public abstract Int32 Priority { get; }

        /// <summary>
        /// S'assure que les données gérées par la classe sont bien intégrés
        /// </summary>
        public void EnsureIntegrated()
        {
            if (!IsIntegrated())
                Integrate();
        }

        /// <summary>
        /// Vérifie si les données gérées par la classe ont déjà été integrée
        /// </summary>
        /// <returns></returns>
        public abstract Boolean IsIntegrated();

        /// <summary>
        /// Intégère les données gérées par la classe
        /// </summary>
        public abstract void Integrate();
    }
}
