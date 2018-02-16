using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frameshift
{
    /// <summary>
    /// Abstract class representing an amino acid generator. This class defines
    /// default amino acid/codon relations.
    /// </summary>
    public class AAFactory : IAAFactory
    {
        public IAminoAcid BuildAA(AACodon codon)
        {
            switch(codon)
            {
                case AACodon.UUU:
                case AACodon.UUC:
                    //phenylalanine
                    return new AminoAcid(codon, "Phenylalanine", "Phe", 'F');
                case AACodon.UUA:
                case AACodon.UUG:
                case AACodon.CUU:
                case AACodon.CUC:
                case AACodon.CUA:
                case AACodon.CUG:
                    //leucine
                    return new AminoAcid(codon, "Leucine", "Leu", 'L');
                case AACodon.UCU:
                case AACodon.UCC:
                case AACodon.UCA:
                case AACodon.UCG:
                case AACodon.AGU:
                case AACodon.AGC:
                    //serine
                    return new AminoAcid(codon, "Serine", "Ser", 'S');
                case AACodon.UAU:
                case AACodon.UAC:
                    //tyrosine;
                    return new AminoAcid(codon, "Tyrosine", "Tyr", 'T');
                case AACodon.UGU:
                case AACodon.UGC:
                    //cysteine
                    return new AminoAcid(codon, "Cysteine", "Cys", 'C');
                case AACodon.UGG:
                    //tryptophan;
                    return new AminoAcid(codon, "Tryptophan", "Trp", 'W');
                case AACodon.CCU:
                case AACodon.CCC:
                case AACodon.CCA:
                case AACodon.CCG:
                    //proline
                    return new AminoAcid(codon, "Proline", "Pro", 'P');
                case AACodon.CAU:
                case AACodon.CAC:
                    //histidine
                    return new AminoAcid(codon, "Histidine", "His", 'H');
                case AACodon.CAA:
                case AACodon.CAG:
                    //glutamine
                    return new AminoAcid(codon, "Glutamine", "Gln", 'Q');
                case AACodon.CGU:
                case AACodon.CGC:
                case AACodon.CGA:
                case AACodon.CGG:
                case AACodon.AGA:
                case AACodon.AGG:
                    //arginine
                    return new AminoAcid(codon, "Arginine", "Arg", 'R');
                case AACodon.AUU:
                case AACodon.AUC:
                case AACodon.AUA:
                    //isoleucine
                    return new AminoAcid(codon, "Isoleucine", "Ile", 'I');
                case AACodon.AUG:
                    //met / start
                    return new AminoAcid(codon, "Methionine", "Met", 'M');
                case AACodon.ACU:
                case AACodon.ACC:
                case AACodon.ACA:
                case AACodon.ACG:
                    //threonine
                    return new AminoAcid(codon, "Threonine", "Thr", 'T');
                case AACodon.AAU:
                case AACodon.AAC:
                //asparagine
                    return new AminoAcid(codon, "Asparagine", "Asn", 'N');
                case AACodon.AAA:
                case AACodon.AAG:
                    //lycine
                    return new AminoAcid(codon, "Lycine", "Lys", 'K');
                case AACodon.GUU:
                case AACodon.GUC:
                case AACodon.GUA:
                case AACodon.GUG:
                    //valine
                    return new AminoAcid(codon, "Valine", "Val", 'V');
                case AACodon.GCU:
                case AACodon.GCC:
                case AACodon.GCA:
                case AACodon.GCG:
                    //alanine
                    return new AminoAcid(codon, "Alanine", "Ala", 'A');
                case AACodon.GAU:
                case AACodon.GAC:
                    //aspartic acid
                    return new AminoAcid(codon, "Apartic Acid", "Asp", 'D');
                case AACodon.GAA:
                case AACodon.GAG:
                    //glutamic acid
                    return new AminoAcid(codon, "Glutamic Acid", "Glu", 'E');
                case AACodon.GGU:
                case AACodon.GGC:
                case AACodon.GGA:
                case AACodon.GGG:
                    //glycine;
                    return new AminoAcid(codon, "Glycine", "Gly", 'G');
                case AACodon.UAA:
                case AACodon.UAG:
                case AACodon.UGA:
                    //stop 
                    return new AminoAcid(codon, "STOP", "END", '_');
                default: return null;
            }
        }
    }
}
