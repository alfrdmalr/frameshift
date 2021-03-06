﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Frameshift
{
    /// <summary>
    /// handles translation from RNA to amino acids
    /// </summary>
    public class Ribosome
    {
        private AACodon startCodon;
        private List<AACodon> stopCodons;
        private IAAFactory factory;
        private int codonLength;

        /// <summary>
        /// Constructor for Ribosome instances.
        /// </summary>
        /// <param name="startCodon">The codon accepted as the start codon.</param>
        /// <param name="stopCodons">Set of codons accepted as the stop codon.</param>
        /// <param name="factory">Rules for generating AminoAcids from codons</param>
        /// <param name="codonLength">length of an invidual codon; should be consistent
        /// with start/stop codons</param>
        public Ribosome(AACodon startCodon, List<AACodon> stopCodons, IAAFactory factory, int codonLength)
        {
            this.startCodon = startCodon;
            this.stopCodons = stopCodons;
            this.factory = factory;
            this.codonLength = codonLength;
        }

        /// <summary>
        /// Translates the given rna sequence into the corresponding protein sequence.
        /// Translates between pairs of START and STOP codons; in the case that a strand does
        /// not have a final stop codon, the remainder of the strand will be translated.
        /// <param name="seq">The rna sequence serving as the template </param>
        /// <returns>Returns the list of amino acids generated by the sequence</returns>
        /// </summary>
        public List<IAminoAcid> Translate(Nucleobase[] seq)
        {
            Boolean keepTranslating = true;
            List<IAminoAcid> protein = new List<IAminoAcid>();
            int start = 0;
            int stop = 0;
            while (keepTranslating)
            {
                try
                {
                    start = Scan(seq, startCodon, stop);
                    stop = Scan(seq, stopCodons, start + this.codonLength, this.codonLength);
                }
                catch (CodonNotFoundException)
                {
                    keepTranslating = false;
                    break;
                }
                catch (ArgumentException)
                {
                    keepTranslating = false;
                    break;
                }

                var rf = CreateReadingFrame(seq, start, stop);
                List<IAminoAcid> polypeptide =  TranslateReadingFrame(rf);
                
                protein.AddRange(polypeptide);
            }
            return protein;
        }

        /// <summary>
        /// Translates the given sequence "blindly," i.e. without searching for start and
        /// stop codons. Any remainder nucleotides will be truncated and not reflected in the resultant
        /// polypeptide.
        /// </summary>
        /// <param name="seq">Sequence to blindly translate.</param>
        /// <returns>Returns a polypeptide via a list of amino acids</returns>
        public List<IAminoAcid> BlindTranslate(Nucleobase[] seq, int startIndex)
        {
            int stopIndex = seq.Length - (seq.Length % this.codonLength) - this.codonLength;
            var rf = CreateReadingFrame(seq, 0, stopIndex);
            var polypeptide = TranslateReadingFrame(rf);
            return polypeptide;
        }


        /// <summary>
        /// Scans the given sequence for a codon in the provided list, returning the
        /// index of the first base in the found codon
        /// </summary>
        /// <param name="seq">sequence to scan</param>
        /// <param name="cod">set of codons to scan for</param>
        /// <param name="beginScan">index at which to begin scanning for the specified codon</param>
        /// <returns>Returns the index of the first nucleobase in a matching sequence</returns>
        private int Scan(Nucleobase[] seq, List<AACodon> cod, int beginScan, int granularity)
        {
            if (this.codonLength >= seq.Length - beginScan)
            {
                throw new ArgumentException("given codon is longer than the provided sequence");
            } else if (beginScan < 0 || beginScan > seq.Length)
            {
                throw new ArgumentOutOfRangeException("scan must begin at a valid index. Attempted scan start: " +
                    beginScan + ". Sequence length: " + seq.Length + ".");
            }

            for (int i = beginScan; i < seq.Length - this.codonLength + 1; i += granularity)
            {
                var tempCodon = AACodonExtension.BuildAACodon(seq, i, i + this.codonLength);
                if (cod.Contains(tempCodon))
                {
                    return i;
                }
            }
            throw new CodonNotFoundException();
        }

        /// <summary>
        /// Overload for scan, used when there's only a single possible codon to scan for.
        /// uses the default granularity of 1
        /// </summary>
        private int Scan(Nucleobase[] seq, AACodon cod, int beginScan)
        {
            return Scan(seq, new List<AACodon> {cod}, beginScan, 1);
        }

        /// <summary>
        /// Creates triplet groups of nucleobases along the specified range. the
        /// range must be of appropriate length, i.e. a multiple of the codon length.
        /// </summary>
        /// <param name="seq">RNA sequence to group</param>
        /// <param name="startIndex">index of the first nucleobase in the first codon</param>
        /// <param name="stopIndex">index of the first nucleobase in the last codon</param>
        /// <returns>Returns list of AACodons</returns>
        private List<AACodon> CreateReadingFrame(Nucleobase[] seq, int startIndex, int stopIndex)
        {
            if (stopIndex < startIndex + this.codonLength) {
                throw new ArgumentException("stop index must be at least " + this.codonLength +
                    "greater than start index.");
            }
            int rangeLength = stopIndex - startIndex;
            if (rangeLength % 3 != 0) {
                throw new ArgumentException("the length of the reading frame must"
                + "be a multiple of " + this.codonLength + " -- in other words, " +
                "the reading frame must contain a whole number of codons.");
            }

            List<AACodon> triplets = new List<AACodon>();
            int lengthDiff = seq.Length - this.codonLength; //readability

            for (int i = startIndex; i < stopIndex + this.codonLength; i += this.codonLength)
            {
                triplets.Add(AACodonExtension.BuildAACodon(seq, i, i + this.codonLength));
            }

            return triplets;
        }

        /// <summary>
        /// Translates the given sequence of AACodons into the corresponding
        /// amino acids.
        /// </summary>
        /// <param name="seq">sequence of codon triplets</param>
        /// <returns>Returns amino acid sequence</returns>
        private List<IAminoAcid> TranslateReadingFrame(List<AACodon> seq)
        {
            List<IAminoAcid> polypeptide = new List<IAminoAcid>();
            //use the factory to determine which amino acids get made.
            // at any time, if seq = stopCodon quit.
            foreach (AACodon c in seq)
            {
                polypeptide.Add(factory.BuildAA(c));
            }
            return polypeptide;
        }
    }
}
