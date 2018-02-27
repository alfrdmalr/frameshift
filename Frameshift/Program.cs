using Frameshift.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frameshift
{

    // main class
    class Program
    {

        static void Main(string[] args)
        {

            int length = 0;

            while (length < 1)
            {
                Console.WriteLine("Enter the length of the sequence you wish to generate.");
                var l = Console.ReadLine();
                int.TryParse(l, out length);
            }
            
            ICell humanCell = new NormalCell(length);
            IFrameshiftController con = new ConsoleController(humanCell);
            con.RunSimulation();

            /*
            Console.WriteLine("\nResultant amino acid sequence:");
            foreach (IAminoAcid aa in protein)
            {
                Console.Write(aa.GetShortName() + ".");
            }
            */

            //Console.ReadLine();
        }

    }
}
