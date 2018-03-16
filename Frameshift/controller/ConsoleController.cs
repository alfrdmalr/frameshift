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
            Console.WriteLine("Welcome to FRAMESHIFT!");
            
            Console.WriteLine("For now, your cell is probably pretty boring. Try changing the DNA sequence! \n" + 

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
            String[] inputs = i.Trim().Split(' ');
            switch (inputs[0].ToLower())
            {
                case "help":
                    Console.WriteLine("Commands:\n" +
                        "sequence - allows user to change dna sequence" +
                        "\nprotein [type] - displays " +
                        "amino acid sequence(s) of the cell based on the type:\n" +
                        "\t full: full protein name\n\t short: " +
                        "abbreviated protein name\n\t symbol: protein symbol" +
                        "\nignore [type] - displays amino acid sequence without checking for" +
                        " start/stop codons." +
                        "\ndna - displays dna sequence" +
                        "\nrna - displays rna sequence" +
                        "\nreport - displays a summary of the cell" + 
                        "\nhelp - displays list of commands" +
                        "\nend - quits the simulation");
                    return true;
                case "dna":
                    this.PrintSequence(this.cell.GetDNA());
                    return true;
                case "rna":
                    this.PrintSequence(this.cell.GetRNA());
                    return true;
                case "sequence":
                    if (inputs.Length > 2)
                    {
                        this.SequenceRoutineHandler(inputs[1] + " " + inputs[2]);
                    } else if (inputs.Length > 1)
                    {
                        this.SequenceRoutineHandler(inputs[1]);
                    } else
                    {
                        this.SequenceRoutineHelper();
                    }
                    return true;
                case "protein":
                    if (inputs.Length > 1)
                    {
                        ProteinPrintHelper(true, inputs[1]);
                    } else
                    {
                        ProteinPrintHelper(true, "prompt");
                    }
                    return true;
                case "ignore":
                    if (inputs.Length > 1)
                    {
                        ProteinPrintHelper(false, inputs[1]);
                    } else
                    {
                        ProteinPrintHelper(false, "prompt");
                    }
                    return true;
                case "report":
                    this.GenerateBasicReport();
                    return true;
                case "end":
                    return false;
                default:
                    Console.WriteLine("Command \'" + inputs[0] + "\' not recognized. Type \'help\' for a list of commands.");
                    return true;
            }
        }

        private void PrintSequence(Nucleobase[] seq)
        {
            foreach (Nucleobase n in seq)
            {
                Console.Write(n.ToString());
            }
            Console.Write("\n");
        }

        /// <summary>
        /// Helps to print the appropriate protein sequence based on the input.
        /// </summary>
        /// <param name="normal"></param>
        /// <param name="type"></param>
        private void ProteinPrintHelper(Boolean normal, String type)
        {
            if (type.ToLower().Equals("prompt"))
            {
                Console.WriteLine("How do you want to display the protein? (full, short, symbol)");
                ProteinPrintHelper(normal, Console.ReadLine());
                return;
            }

            if (normal)
            {
                PrintProtein(type, this.cell.GetAminoAcids());
            } else
            {
                try
                {
                    PrintProtein(type, this.cell.SimpleTranslate());
                    Console.WriteLine("Note that there is at least one start/stop codon missing!");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Unable to perform a basic translate. " +
                        "Does the cell have a valid RNA sequence?.");
                }
            }
        }

        private void PrintProtein(string type, List<IAminoAcid> protein)
        {
            if (protein.Count < 1)
            {
                Console.WriteLine("Something is wrong with this cell's protein.\n" +
                "you can always type \'ignore\' followed by the display type " +
                        "(\'full\', \'short,\' etc.) to view the full sequence of amino acids. " +
                        "If that fails, you can double check the RNA sequence with 'rna', or change it with 'sequence'");
                return;
            }

            StringBuilder sb = new StringBuilder();
            int pNum = 1;
            Boolean newProtein = false;
            Boolean hyphen;
            if (type.ToLower().Equals("symbol"))
            {
                hyphen = false;
            } else
            {
                hyphen = true;
            }

            sb.Append(String.Format("P{0}: ", pNum));

            foreach (IAminoAcid aa in protein)
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
                        Console.WriteLine("Invalid command: " + type);
                        return;
                }

                if (aa.GetFullName().Equals("STOP"))
                {
                    sb.Append("\n\n");
                    pNum++;
                    newProtein = true;
                }
                else if (hyphen)
                {
                    sb.Append("-");
                }
            }
            sb.Remove(sb.Length - 1, 1); // removes the last hyphen
            Console.WriteLine(sb.ToString());
        }

        private void SequenceRoutineHandler(String i)
        {
            var inputs = i.Trim().Split(' ');

            switch(inputs[0])
            {
                case "custom":
                    Console.WriteLine("not implemented yet, sorry!");
                    break;
                case "random":
                    Random randall = new Random();
                    var rint = randall.Next(200, 2000);
                    this.cell.ChangeSequence(rint);
                    Console.WriteLine("DNA successfully synthesized! (length " + rint + ")");
                    break;
                case "length":
                    int l = 0;
                    if (inputs.Length > 1)
                    {
                        Int32.TryParse(inputs[1], out l);
                    }
                    while (l < 1)
                    {
                        Console.WriteLine("How long would you like the random DNA sequence to be?");
                        Int32.TryParse(Console.ReadLine(), out l);
                    }
                    this.cell.ChangeSequence(l);
                    Console.WriteLine("DNA successfully synthesized! (length " + l + ")");
                    break;
                default:
                    Console.WriteLine("Unrecognized command: " + i);
                    break;
            }
        }

        //overload
        private void SequenceRoutineHelper()
        {
            Console.WriteLine("How would you like to change the cell's DNA sequence?\n" +
                "'custom' - allows user to input custom sequence\n" +
                "'random' - generates random DNA sequence of random length\n" +
                "'length' - specify length of random DNA sequence");
            SequenceRoutineHandler(Console.ReadLine());
        }

        private void GenerateBasicReport()
        {
            int one = this.cell.GetDNA().Length;
            var protein = this.cell.GetAminoAcids();
            var twoQuery =
                from a in protein
                where !a.GetFullName().Equals("STOP")
                select a;
            int two = twoQuery.Count();
            int numProteins = protein.Count - two;

            Console.WriteLine("|| CELL SUMMARY ||");
            Console.WriteLine("Nucleotide Sequence Length: {0}\n" +
                "Amino Acid Count: {1}\nProtein Count: {2}", one, two, numProteins);

        }
    }
}