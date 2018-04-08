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
            /*
            int length = 0;

            while (length < 1)
            {
                Console.WriteLine("Welcome to FRAMESHIFT!\n" +
                    "Enter the length you want for the cell's DNA sequence.\n" + 
                    "This seems to work best between 100 and 1000 base pairs, though it doesn't have to be.");
                var l = Console.ReadLine();
                int.TryParse(l, out length);
            }
            */
            
            ICell humanCell = new NormalCell();
            IFrameshiftController con = new ConsoleController(humanCell);
            con.RunSimulation();
        }

    }
}