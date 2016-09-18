using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NicheFinder.Integration
{
    public class Integrators
    {
        /// <summary>
        /// S'assure que les données initiales sont bien intégrées en BDD
        /// </summary>
        public static void EnsureIntegrated()
        {
            foreach (Integrator i in GetIntegrators().OrderBy(i => i.Priority))
                i.EnsureIntegrated();
        }

        /// <summary>
        /// Renvoie l'ensemble des classes d'intégrations gérées dans l'application
        /// </summary>
        static IEnumerable<Integrator> GetIntegrators()
        {
            Type iIntegratorType = typeof(Integrator);
            return iIntegratorType.Assembly
                .GetTypes()
                .Where(t => iIntegratorType.IsAssignableFrom(t) && !t.IsAbstract && t.GetConstructor(Type.EmptyTypes) != null)
                .Select(t => (Integrator)Activator.CreateInstance(t))
                .ToList();
        }
    }
}
