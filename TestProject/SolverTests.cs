using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

using tanks.Models;

namespace tanks.ModelsTests
{
    [TestClass]
    public class SolverTests
    {
        void TestCase(string name)
        {
            FlowDiagram fd = null;

            using (var stream = tanks.TestData.GetResourceStream(name))
                fd = XMLUtil.DeserializeFlowDiagram(stream);

            fd.Fix();

            {
                var ms = new MemoryStream();

                XMLUtil.SerializeFlowDiagram(ms, fd);
            }

            var fd1 = DTOUtil.CreateFromFD(fd, (type) => Activator.CreateInstance(type));

            tanks.TestCode.SolverTest(fd1);

            DTOUtil.UpdateFD(fd, fd1);
        }

        [TestMethod]
        public void SolverDoc1Test()
        {
            TestCase("TestData.doc1.xml");
        }

        [TestMethod]
        public void SolverDoc2Test()
        {
            TestCase("TestData.doc2.xml");
        }

        [TestMethod]
        public void SolverDoc3Test()
        {
            TestCase("TestData.doc3.xml");
        }

        [TestMethod]
        public void SolverDoc4Test()
        {
            TestCase("TestData.doc4.xml");
        }

        [TestMethod]
        public void SolverDoc5Test()
        {
            TestCase("TestData.doc5.xml");
        }

        [TestMethod]
        public void SolverDoc6Test()
        {
            TestCase("TestData.doc6.xml");
        }

        [TestMethod]
        public void SolverDoc6aTest()
        {
            TestCase("TestData.doc6a.xml");
        }

        [TestMethod]
        public void SolverDoc7Test()
        {
            TestCase("TestData.doc7.xml");
        }
    }
}
