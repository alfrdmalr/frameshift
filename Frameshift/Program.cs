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
        static int length = 0;

        // main program loop
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the length of the sequence you wish to generate.");
            var l = Console.ReadLine();
            int.TryParse(l, out length);

            ICell humanCell = new NormalCell(length);
            var protein = humanCell.GetAminoAcids();

            Console.WriteLine("\nResultant amino acid sequence:");
            foreach (IAminoAcid aa in protein)
            {
                Console.Write(aa.GetShortName() + ".");
            }

            Console.ReadLine();
        }

    }
}
