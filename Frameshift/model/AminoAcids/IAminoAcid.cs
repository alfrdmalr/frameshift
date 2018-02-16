using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frameshift
{
    /// <summary>
    /// Behavior for all amino acids, including custom definitions.
    /// </summary>
    public interface IAminoAcid
    {
        /// <summary>
        /// Returns the codon that generated this amino acid. If this amino
        /// acid was constructed independently of a DNA sequence, it should
        /// assign itself a potential codon.
        /// </summary>
        /// <returns>Returns source codon.</returns>
        AACodon GetSourceCodon();

        /// <summary>
        /// Returns the full name of this amino acid.
        /// </summary>
        /// <returns>Returns full name.</returns>
        String GetFullName();

        /// <summary>
        /// Returns the 3-letter abbreviation for this amino acid.
        /// </summary>
        /// <returns>Returns abbreviated name.</returns>
        String GetShortName();

        /// <summary>
        /// Returns the 1-letter symbol abbreviation for this amino acid.
        /// </summary>
        /// <returns>Returns amino acid symbol.</returns>
        Char GetSymbol();
        
    }
}
