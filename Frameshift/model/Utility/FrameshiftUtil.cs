using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frameshift

{
    /// <summary>
    /// utilities class for FRAMSHIFT
    /// </summary>
    public class FrameshiftUtil
    {
        /// <summary>
        /// Generates a new, random sequence of DNA bases
        /// </summary>
        /// <param name="len">desired length of sequence</param>
        /// <returns>Returns the new DNA sequence</returns>
        public static Nucleobase[] GenerateDNASequence(int len)
        {
            return GenerateSequence(len, SequenceType.DNA);
        }

        /// <summary>
        /// Generates a new, random sequence of RNA bases
        /// </summary>
        /// <param name="len">desired length of sequence</param>
        /// <returns>Returns the new RNA sequence</returns>
        public static Nucleobase[] GenerateRNASequence(int len)
        {
            return GenerateSequence(len, SequenceType.RNA);
        }

        /// <summary>
        /// Generates a random sequence of appropriate nucleobases
        /// based on the given length and specified type
        /// </summary>
        /// <param name="len">length of desired sequence</param>
        /// <param name="t">type of sequence</param>
        /// <returns>Returns the newly generated sequence</returns>
        private static Nucleobase[] GenerateSequence(int len, SequenceType t)
        {
            if (len < 1)
            {
                throw new ArgumentException("Sequence length must be greater than 0");
            }

            /*
            * base indices:
            * 0 T
            * 1 C
            * 2 G
            * 3 A
            * 4 U
            */

            int baseStart; // defines the index of the first base in the set of bases to build the sequence with
            int baseEnd; // defines the index of the last base " " "
            Nucleobase[] seq = new Nucleobase[len];
            Random randall = new Random();

            switch (t)
            {
                case SequenceType.DNA:
                    baseStart = 0;
                    baseEnd = 3;
                    break;
                case SequenceType.RNA:
                    baseStart = 1;
                    baseEnd = 4;
                    break;
                default:
                    throw new ArgumentException("sequence type must be either DNA or RNA");
            }

            for (int i = 0; i < len; i++)
            {
                seq[i] = (Nucleobase)randall.Next(baseStart, baseEnd + 1);
            }

            return seq;
        }

        /// <summary>
        /// Prints a given sequence of nucleobases to the console for viewing
        /// </summary>
        /// <param name="seq">sequence to be printed</param>
        public static void PrintSequence(Nucleobase[] seq)
        {
            foreach (Nucleobase n in seq)
            {
                Console.Write(n.ToString());
            }
        }

        /// <summary>
        /// Convenience method for testing and custom sequences.
        /// Converts the given string (if valid) into an array of nucleobases.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Nucleobase[] StringSeq(String s)
        {
            Nucleobase[] seq = new Nucleobase[s.Length];
            int i = 0;
            foreach (Char c in s)
            {
                if (Enum.TryParse(c.ToString(), out Nucleobase b))
                {
                    seq[i] = b;
                } else
                {
                    throw new ArgumentException("input for quick sequences must only " +
                        "contain characters which are valid elements of that sequence. Invalid" +
                        "character: " + c);
                }

                i++;
            }

            if (seq.Contains(Nucleobase.U) && seq.Contains(Nucleobase.T))
            {
                throw new ArgumentException("sequence should not contain both Thymine and Uracil");
            }

            return seq;
        }
    }
}
