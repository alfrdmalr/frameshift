using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frameshift
{
    /// <summary>
    /// Represents a 'normal' cell, as in a human or bird or something.
    /// Uses the standard conserved set of amino acids.
    /// </summary>
    public class NormalCell : ICell
    {
        private Polymerase pol; //technically unecessary since it's static currently but hey
        private Ribosome rib;
        private Nucleobase[] rna;
        private Nucleobase[] dna;
        private List<IAminoAcid> pro;

        public NormalCell(int sequenceLength)
        {

            if (sequenceLength < 0)
            {
                throw new ArgumentException("Illegal sequence length");
            }

            List<AACodon> stops = new List<AACodon>
            {
                AACodon.UGA,
                AACodon.UAA,
                AACodon.UAG
            };

            this.rib = new Ribosome(AACodon.AUG, stops, new AAFactory(), 3);
            this.dna = FrameshiftUtil.GenerateDNASequence(sequenceLength);
            this.rna = Polymerase.Transcribe(this.dna);
            this.pro = this.rib.Translate(this.rna);
        }

        /// <summary>
        /// creates a new cell with no genetic information. 
        /// Initializes the 'normal' machinery.
        /// </summary>
        public NormalCell()
        {
            List<AACodon> stops = new List<AACodon>
            {
                AACodon.UGA,
                AACodon.UAA,
                AACodon.UAG
            };

            this.pol = new Polymerase();
            this.rib = new Ribosome(AACodon.AUG, stops, new AAFactory(), 3);
            this.dna = new Nucleobase[0];
            this.rna = new Nucleobase[0];
            this.pro = new List<IAminoAcid>();
        }

        public List<IAminoAcid> GetAminoAcids()
        {
            return this.pro;
        }

        public List<IAminoAcid> SimpleTranslate()
        {
            return this.rib.BlindTranslate(this.rna, 0);
        }

        public Nucleobase[] GetDNA()
        {
            return this.dna;
        }

        public Nucleobase[] GetRNA()
        {
            return this.rna;
        }

        public void ChangeSequence(int len)
        {
            if (len < 0)
            {
                throw new ArgumentException("Could not change sequence: illegal sequence length");
            }
            this.WipeCell();

            this.dna = FrameshiftUtil.GenerateDNASequence(len);
            this.rna = Polymerase.Transcribe(this.dna);
            this.pro = this.rib.Translate(this.rna);
        }

        /// <summary>
        /// clears any genetic information about the cell. Machinery is untouched.
        /// </summary>
        private void WipeCell()
        {
            this.pro = null;
            this.rna = null;
            this.dna = null;
        }
    }
}
