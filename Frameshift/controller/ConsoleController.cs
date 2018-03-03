using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frameshift.controller
{
    /// <summary>
    /// Basic controller whose view is console output.
    /// </summary>
    class ConsoleController : IFrameshiftController
    {

        ICell cell;

        /// <summary>
        /// Public constructor for a controller.
        /// </summary>
        /// <param name="model"></param>
        public ConsoleController(ICell model)
        {
            this.cell = model;
        }

        public void RunSimulation()
        {
            Boolean simulate = true;

            Console.WriteLine("Cell synthesized succesfully!\n" + 
                "type \'help\' for a list of commands");

            // main loop
            while (simulate)
            {
                var i = Console.ReadLine();
                simulate = HandleInput(i);
            }
        }

        // does something based on the input
        // returns whether or not the simulation should continue
        private Boolean HandleInput(String i)
        {
            switch (i.ToLower())
            {
                case "help":
                    Console.WriteLine("Commands:\nprotein [type]: displays " +
                        "amino acid sequence(s) of the cell based on the type:\n" +
                        "\tfull: full protein name\n\tshort: " +
                        "abbreviated protein name\n\tsymbol: protein symbol" +
                        "\ndna\tdisplays dna sequence" +
                        "\nrna\tdisplays rna sequence" +
                        "\nhelp\tdisplays list of commands" +
                        "\nend\tquits the simulation");
                    return true;
                case "dna":
                    foreach (Nucleobase n in this.cell.GetDNA())
                    {
                        Console.Write(n.ToString());
                    }
                    Console.Write("\n");
                    return true;
                case "rna":
                    foreach (Nucleobase n in this.cell.GetRNA())
                    {
                        Console.Write(n.ToString());
                    }
                    Console.Write("\n");
                    return true;
                case "end":
                    return false;
                case "protein full":
                    PrintProtein("full");
                    return true;
                case "protein short":
                    PrintProtein("short");
                    return true;
                case "protein symbol":
                    PrintProtein("symbol");
                    return true;
                default:
                    Console.WriteLine("Command \'" + i + "\' not recognized. Type \'help\' for a list of commands.");
                    return true;
            }
        }

        private void PrintProtein(string type)
        {
            StringBuilder sb = new StringBuilder();
            int pNum = 1;
            Boolean newProtein = false;
            sb.Append(String.Format("P{0}: ", pNum));
            foreach (IAminoAcid aa in this.cell.GetAminoAcids())
            {
                if (newProtein)
                {
                    sb.Append(String.Format("P{0}: ", pNum));
                    newProtein = false;
                }

                switch (type.ToLower())
                {
                    case "full":
                        sb.Append(aa.GetFullName());
                        break;
                    case "short":
                        sb.Append(aa.GetShortName());
                        break;
                    case "symbol":
                        sb.Append(aa.GetSymbol());
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        return;
                }

                if (aa.GetFullName().Equals("STOP"))
                {
                    sb.Append("\n\n");
                    pNum++;
                    newProtein = true;
                }
                else
                {
                    sb.Append("-");
                }
            }
            sb.Remove(sb.Length - 1, 1); // removes the last hyphen
            Console.WriteLine(sb.ToString());
        }
    }
}
