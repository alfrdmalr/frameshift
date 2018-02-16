using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frameshift
{
    public class NormalCell : ICell
    {
        private Polymerase pol; //technically unecessary since it's static but hey
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
            // these lines are commented out because of a change in the ribosome, 
            //TODO
            this.rib = new Ribosome(AACodon.AUG, stops, new AAFactory(), 3);
            this.rna = FrameshiftUtil.GenerateRNASequence(sequenceLength);
            this.pro = this.rib.Translate(this.rna);
        }

        public List<IAminoAcid> GetAminoAcids()
        {
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
