using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace NucleicAcidAnalyzer.NucleicAcid.Parser.Tests
{
    using Ast;

    [TestClass]
    public class DnaParserTests
    {
        [TestMethod]
        public void ParseInputStringTest1()
        {
            string input = "TTTCCATAGGCTCCGCCCCCCTGACAAGCA";
            DnaParser parser = new DnaParser();
            INucleicAcid.Dna dna = (INucleicAcid.Dna) parser.Parse(input);
            Assert.AreEqual(10, dna.TripletCodes.Count);
            using (StringWriter writer = new StringWriter())
            {
                dna.Accept(new NucleicAcidWriter(writer));
                Assert.AreEqual(input, writer.ToString());
            }
        }

        [TestMethod]
        public void ParseInputStringTest2()
        {
            string input = "T-TCCATAGGCTCCGCCCCCCTGACAAGCA";
            DnaParser parser = new DnaParser();
            INucleicAcid.Dna dna = (INucleicAcid.Dna)parser.Parse(input);
            Assert.AreEqual(10, dna.TripletCodes.Count());
            using (StringWriter writer = new StringWriter())
            {
                dna.Accept(new NucleicAcidWriter(writer));
                Assert.AreEqual(input, writer.ToString());
            }
        }

        [TestMethod]
        public void ParseInputStringTest3()
        {
            string input = "TUTCCAUAGGCTCCGCCCCCCTGACAAGCA";
            DnaParser parser = new DnaParser();
            Assert.ThrowsException<NucleicAcidParseExpception>(() =>
            {
                parser.Parse(input);
            });
        }

        [TestMethod]

        public void ParseInputStringTest4()
        {
            string input = "UUUCCAUAGGCUCCGCCCCCCUGACAAGCA";
            DnaParser parser = new DnaParser();
            Assert.ThrowsException<NucleicAcidParseExpception>(() =>
            {
                parser.Parse(input);
            });
        }

        [TestMethod]

        public void ParseInputStringTest5()
        {
            string input = "T";
            DnaParser parser = new DnaParser();
            Assert.ThrowsException<NucleicAcidParseExpception>(() =>
            {
                parser.Parse(input);
            });
        }

        [TestMethod]

        public void ParseInputStringTest6()
        {
            string input = "TT";
            DnaParser parser = new DnaParser();
            Assert.ThrowsException<NucleicAcidParseExpception>(() =>
            {
                parser.Parse(input);
            });
        }

        [TestMethod]

        public void ParseInputStringTest7()
        {
            string input = "";
            DnaParser parser = new DnaParser();
            Assert.ThrowsException<NucleicAcidParseExpception>(() =>
            {
                parser.Parse(input);
            });
        }

        [TestMethod]
        public void ParseInputTextReaderTest()
        {
            string input = "TTTCCATAGGCTCCGCCCCCCTGACAAGCA";
            DnaParser parser = new DnaParser();
            using StringReader reader = new StringReader(input);
            INucleicAcid.Dna dna = (INucleicAcid.Dna) parser.Parse(reader);
            Assert.AreEqual(10, dna.TripletCodes.Count());
            using (StringWriter writer = new StringWriter())
            {
                dna.Accept(new NucleicAcidWriter(writer));
                Assert.AreEqual(input, writer.ToString());
            }
        }

        [TestMethod]
        public void ParseInputStreamTest()
        {
            string input = "TTTCCATAGGCTCCGCCCCCCTGACAAGCA";
            DnaParser parser = new DnaParser();
            using MemoryStream memory = new MemoryStream();
            memory.Write(Encoding.UTF8.GetBytes(input));
            memory.Position = 0;
            INucleicAcid.Dna dna = (INucleicAcid.Dna)parser.Parse(memory);
            Assert.AreEqual(10, dna.TripletCodes.Count());
            using (StringWriter writer = new StringWriter())
            {
                dna.Accept(new NucleicAcidWriter(writer));
                Assert.AreEqual(input, writer.ToString());
            }
        }
    }
}