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

           
            
            ICell humanCell = new NormalCell();
            IFrameshiftController con = new ConsoleController(humanCell);
            con.RunSimulation();
        }

    }
}