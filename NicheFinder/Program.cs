using NicheFinder.Integration;
using System;

namespace NicheFinder
{
    /// <summary>
    /// Classe principal
    /// </summary>
    class Program
    {
        static void Main(String[] args)
        {
            try
            {
                Integrators.EnsureIntegrated();

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
