using Microsoft.VisualStudio.TestTools.UnitTesting;
using Frameshift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frameshift.Tests
{
    [TestClass()]
    public class RibosomeTests
    {
        int length = 30;
        Nucleobase[] seq;
        Nucleobase[] start;

        private void InitVals()
        {
            seq = FrameshiftUtil.GenerateRNASequence(length);
            start = FrameshiftUtil.StringSeq("AUG");
        }


        [TestMethod()]
        public void TestScanRegularCase()
        {
            InitVals();
            Nucleobase[] seq1 = FrameshiftUtil.StringSeq("CGAUGCC");
            // start codon found at index 2
            Assert.AreEqual(2, Ribosome.Scan(seq1, start)); 
        }

        [TestMethod()]
        public void TestScanFirst()
        {
            InitVals();
            Nucleobase[] seq1 = FrameshiftUtil.StringSeq("AUGCCGU");
            Assert.AreEqual(0, Ribosome.Scan(seq1, start));
        }

        [TestMethod()]
        public void TestScanLast()
        {
            InitVals();
            Nucleobase[] seq1 = FrameshiftUtil.StringSeq("CCGAUG");
            Assert.AreEqual(3, Ribosome.Scan(seq1, start));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestScanNoStart()
        {
            InitVals();
            Nucleobase[] seq1 = FrameshiftUtil.StringSeq("CGAUCUAGUCGUAGCGU");
            Ribosome.Scan(seq1, start);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestScanShortSequence()
        {
            InitVals();
            Nucleobase[] seq1 = FrameshiftUtil.StringSeq("CG");
            Ribosome.Scan(seq1, start);
        }
    }
}