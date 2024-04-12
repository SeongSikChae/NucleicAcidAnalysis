using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace NucleicAcidAnalyzer.NucleicAcid.Parser.Tests
{
    using Ast;

    [TestClass]
    public class RnaParserTests
    {
        [TestMethod]
        public void ParseInputStringTest1()
        {
            string input = "UUUCCAUAGGCUCCGCCCCCCUGACAAGCA";
            RnaParser parser = new RnaParser();
            INucleicAcid.Rna rna = (INucleicAcid.Rna)parser.Parse(input);
            Assert.AreEqual(10, rna.TripletCodes.Count());
            using (StringWriter writer = new StringWriter())
            {
                rna.Accept(new NucleicAcidWriter(writer));
                Assert.AreEqual(input, writer.ToString());
            }
        }

        [TestMethod]
        public void ParseInputStringTest2()
        {
            string input = "U-UCCAUAGGCUCCGCCCCCCUGACAAGCA";
            RnaParser parser = new RnaParser();
            INucleicAcid.Rna rna = (INucleicAcid.Rna)parser.Parse(input);
            Assert.AreEqual(10, rna.TripletCodes.Count());
            using (StringWriter writer = new StringWriter())
            {
                rna.Accept(new NucleicAcidWriter(writer));
                Assert.AreEqual(input, writer.ToString());
            }
        }

        [TestMethod]
        public void ParseInputStringTest3()
        {
            INucleicAcid.DeleteBase deleteBase = new INucleicAcid.DeleteBase();

            string input = "UTUCCATAGGCUCCGCCCCCCUGACAAGCA";
            RnaParser parser = new RnaParser();
            Assert.ThrowsException<NucleicAcidParseExpception>(() =>
            {
                parser.Parse(input);
            });
        }

        [TestMethod]

        public void ParseInputStringTest4()
        {
            string input = "TTTCCATAGGCTCCGCCCCCCTGACAAGCA";
            RnaParser parser = new RnaParser();
            Assert.ThrowsException<NucleicAcidParseExpception>(() =>
            {
                parser.Parse(input);
            });
        }

        [TestMethod]

        public void ParseInputStringTest5()
        {
            string input = "U";
            RnaParser parser = new RnaParser();
            Assert.ThrowsException<NucleicAcidParseExpception>(() =>
            {
                parser.Parse(input);
            });
        }

        [TestMethod]

        public void ParseInputStringTest6()
        {
            string input = "UU";
            RnaParser parser = new RnaParser();
            Assert.ThrowsException<NucleicAcidParseExpception>(() =>
            {
                parser.Parse(input);
            });
        }

        [TestMethod]

        public void ParseInputStringTest7()
        {
            string input = "";
            RnaParser parser = new RnaParser();
            Assert.ThrowsException<NucleicAcidParseExpception>(() =>
            {
                parser.Parse(input);
            });
        }

        [TestMethod]
        public void ParseInputTextReaderTest()
        {
            string input = "UUUCCAUAGGCUCCGCCCCCCUGACAAGCA";
            RnaParser parser = new RnaParser();
            using StringReader reader = new StringReader(input);
            INucleicAcid.Rna rna = (INucleicAcid.Rna)parser.Parse(reader);
            Assert.AreEqual(10, rna.TripletCodes.Count());
            using (StringWriter writer = new StringWriter())
            {
                rna.Accept(new NucleicAcidWriter(writer));
                Assert.AreEqual(input, writer.ToString());
            }
        }

        [TestMethod]
        public void ParseInputStreamTest()
        {
            string input = "UUUCCAUAGGCUCCGCCCCCCUGACAAGCA";
            RnaParser parser = new RnaParser();
            using MemoryStream memory = new MemoryStream();
            memory.Write(Encoding.UTF8.GetBytes(input));
            memory.Position = 0;
            INucleicAcid.Rna rna = (INucleicAcid.Rna)parser.Parse(memory);
            Assert.AreEqual(10, rna.TripletCodes.Count());
            using (StringWriter writer = new StringWriter())
            {
                rna.Accept(new NucleicAcidWriter(writer));
                Assert.AreEqual(input, writer.ToString());
            }
        }
    }
}