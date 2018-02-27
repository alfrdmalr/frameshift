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
        private List<IAminoAcid> pro;

        public NormalCell(int sequenceLength)
        {
            this.pol = new Polymerase();
            List<AACodon> stops = new List<AACodon>
            {
                AACodon.UGA,
                AACodon.UAA,
                AACodon.UAG
            };

            this.rib = new Ribosome(AACodon.AUG, stops, new AAFactory(), 3);
            this.rna = FrameshiftUtil.GenerateRNASequence(sequenceLength);
            //this.pro = this.rib.Translate(this.rna); 
            //commented out due to singleton implementation
        }

        public List<IAminoAcid> GetAminoAcids()
        {
            // faux-singleton
            if (this.pro == null)
            {
                this.pro = this.rib.Translate(this.rna);
            }

            return this.pro;
        }

        public Nucleobase[] GetDNA()
        {
            throw new NotImplementedException();
        }

        public Nucleobase[] GetRNA()
        {
            return this.rna;
        }
    }
}
