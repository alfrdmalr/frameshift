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
    public class AACodonExtensionTests
    {
        
        [TestMethod()]
        public void TestBuildAAStart()
        {
            var seq = FrameshiftUtil.StringSeq("AUG");
            Assert.AreEqual(AACodon.AUG, AACodonExtension.BuildAACodon(seq, 0, 3));
        }

        [TestMethod()]
        public void TestBuildAAStop()
        {
            var seq = FrameshiftUtil.StringSeq("UAG");
            Assert.AreEqual(AACodon.UAG, AACodonExtension.BuildAACodon(seq, 0, 3));
        }


        /// <summary>
        /// tests to ensure that the specified range is read and not just the
        /// first triplet found
        /// </summary>
        [TestMethod()]
        public void TestBuildAAMiddle()
        {
            var seq = FrameshiftUtil.StringSeq("UGGAGUCCCGA");
            Assert.AreEqual(AACodon.CCC, AACodonExtension.BuildAACodon(seq, 6, 9));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBuildAAFailTooShort()
        {
            var seq = FrameshiftUtil.StringSeq("GA");
            AACodonExtension.BuildAACodon(seq, 0, 3);
        }

        /// <summary>
        /// tests to make sure that an error is thrown when an invalid
        /// sequence is present (e.g. some unrecognized base)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBuildAAFailInvalidSequence()
        {
            var seq = FrameshiftUtil.StringSeq("GAT");
            AACodonExtension.BuildAACodon(seq, 0, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBuildAAFailStartIndexOOB()
        {
            var seq = FrameshiftUtil.StringSeq("UGCACC");
            AACodonExtension.BuildAACodon(seq, -1, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBuildAAFailStartIndexOOB2()
        {
            var seq = FrameshiftUtil.StringSeq("UGCACC");
            AACodonExtension.BuildAACodon(seq, 14, 16);
        }

        /// <summary>
        /// can't start AFTER the stop index
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBuildAAFailStopIndexOOB()
        {
            var seq = FrameshiftUtil.StringSeq("UGCACC");
            AACodonExtension.BuildAACodon(seq, 0, -4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBuildAAFailStopIndexOOB2()
        {
            var seq = FrameshiftUtil.StringSeq("UGCACC");
            AACodonExtension.BuildAACodon(seq, 0, 13);
        }
    }
}