using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frameshift
{
    /// <summary>
    /// Handles transcription from DNA to RNA
    /// </summary>
    public class Polymerase
    {
        /// <summary>
        /// transcribes the given dna sequence into rna, replacing 
        /// instances of T with U. Preserves the initial strand
        /// </summary>
        /// <param name="d">DNA strand to be transcribed</param>
        /// <returns>new RNA strand</returns>
        public static Nucleobase[] Transcribe(Nucleobase[] d)
        {
            Nucleobase[] r = new Nucleobase[d.Length];
            for (int i = 0; i < d.Length; i++)
            {
                if (d[i].Equals(Nucleobase.T))
                {
                    r[i] = Nucleobase.U;
                } else
                {
                    r[i] = d[i];
                }
            }
            return r;
        }
    }
}
