using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frameshift
{
    /// <summary>
    /// Defines behavior for an Amino Acid Factory. Classes implementing
    /// this interface can define custom amino acid/codon relationships
    /// based on the BuildAA method.
    /// </summary>
    public interface IAAFactory
    {
        /// <summary>
        /// Constructs an amino acid based on the given codon.
        /// </summary>
        /// <param name="codon"></param>
        /// <returns></returns>
        IAminoAcid BuildAA(AACodon codon);
    }
}
