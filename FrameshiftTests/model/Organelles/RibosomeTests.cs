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

        private Ribosome r;
        private AACodon start; 
        private List<AACodon> stops;
        private Nucleobase[] seq;

        //initializes the 'default' ribosome to be used in testing
        // might have to change based on actual syntax
        public void Init() {
            start = AACodon.AUG;
            stops = new List<AACodon> 
            {
                AACodon.UGA,
                AACodon.UAA,
                AACodon.UAG
            }; 
            seq = FrameshiftUtil.StringSeq("AUGCCCUAA");

            r = new Ribosome(start, stops, new AAFactory(), 3);
        }
        
        [TestMethod()]
        public void TestScanFirstCodon()
        {
            Init();
            //Assert.AreEqual(r.Scan(seq, new List<AACodon> {start}, 0), 0);
            

        }

        [TestMethod()]
        public void TestX()
        {
            return;
        }
    }
}