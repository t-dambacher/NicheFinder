using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheFinder.Integration
{
    public static class Integrators
    {
        /// <summary>
        /// S'assure que les données initiales sont bien intégrées en BDD
        /// </summary>
        public static void EnsureIntegrated()
        {
            NounsIntegrator.EnsureIntegrated();
            NAF5Integrator.EnsureIntegrated();
        }
    }
}
