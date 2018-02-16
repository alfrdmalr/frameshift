using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frameshift
{
    /// <summary>
    /// Class representing a generic amino acid.
    /// </summary>
    public class AminoAcid : IAminoAcid
    {
        AACodon source;
        Char symbol;
        String fullName;
        String abbrev;

        /// <summary>
        /// Basic constructor for an amino acid.
        /// </summary>
        /// <param name="src">Codon used to generate this amino acid</param>
        /// <param name="i">Index of this AA in the protein sequence</param>
        /// <param name="name">Full name of the AA</param>
        /// <param name="abb">Abbreviated name</param>
        /// <param name="s">Shorthand symbol</param>
        public AminoAcid(AACodon src, String name, String abb, Char s)
        {
            this.source = src;
            this.symbol = s;
            this.fullName = name;
            this.abbrev = abb;

        }

        public string GetFullName()
        {
            return this.fullName;
        }

        public string GetShortName()
        {
            return this.abbrev;
        }

        public AACodon GetSourceCodon()
        {
            return this.source;
        }

        public char GetSymbol()
        {
            return this.symbol;
        }
    }
}
