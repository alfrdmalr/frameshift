using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frameshift
{
    // enum describing "codon" sequences of rna that form amino acids
    public enum AACodon
    {
        UUU, UUC, UUA, UUG, UCU, UCC, UCA, UCG,
        UAU, UAC, UAA, UAG, UGU, UGC, UGA, UGG,
        CUU, CUC, CUA, CUG, CCU, CCC, CCA, CCG,
        CAU, CAC, CAA, CAG, CGU, CGC, CGA, CGG,
        AUU, AUC, AUA, AUG, ACU, ACC, ACA, ACG,
        AAU, AAC, AAA, AAG, AGU, AGC, AGA, AGG,
        GUU, GUC, GUA, GUG, GCU, GCC, GCA, GCG,
        GAU, GAC, GAA, GAG, GGU, GGC, GGA, GGG
    }

    /// <summary>
    /// extension class to provide methods for the 'Codon' enum
    /// </summary>
    public static class AACodonExtension
    {
        private static int CODON_LENGTH = 3;
        /// <summary>
        /// Given an array of nucleobases, returns the appropriate amino acid
        /// </summary>
        /// <param name="seq">Base triplet to convert to a codon</param>
        /// <param name="start">codon start index (inclusive)</param>
        /// <param name="end">codon start index (exclusive)</param>
        public static AACodon BuildAACodon(Nucleobase[] seq, int start, int end) 
        {
            if (start > end || start > seq.Length || start < 0 || end > seq.Length || end < 0)
            {
                throw new ArgumentException(String.Format("Illegal indices. Start: {0}, End: {1}," +
                    " Sequence Length: {2}", start, end, seq.Length));
            } else if (end - start != CODON_LENGTH)
            {
                throw new ArgumentException("No codon of length " + seq.Length + " exist; Nucleobase" +
                    "sequence should be 3 bases long.");
            }

            var s = new StringBuilder();
            for (int i = start; i < end; i++)
            {
                s.Append(seq[i].ToString());
            }

            if (Enum.TryParse(s.ToString(), out AACodon newAA))
            {
                return newAA;
            }
            
            // should only ever happen if Thymine is present
            throw new ArgumentException("no codon found corresponding to \'" + s + "\'");
        }

        /// <summary>
        /// Given an amino acid codon, returns a sequence of 
        /// nucleobases corresponding to that codon
        /// </summary>
        /// <param name="codon">Codon to break down</param>
        /// <returns>Array of appropriate bases</returns>
        public static Nucleobase[] BreakAACodon(AACodon codon)
        {
            return FrameshiftUtil.StringSeq(codon.ToString()); //TODO
        }
    }
}
