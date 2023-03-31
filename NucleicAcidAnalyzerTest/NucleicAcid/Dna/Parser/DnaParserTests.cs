using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NucleicAcidAnalyzer.NucleicAcid.Dna.Parser.Tests
{
    using NucleicAcid.Ast;
    using System.Text;

    [TestClass]
    public class DnaParserTests
    {
        [TestMethod]
        public void ParseInputStringTest1()
        {
            INucleicAcid.DeleteBase deleteBase = new INucleicAcid.DeleteBase();

            string input = "TTTCCATAGGCTCCGCCCCCCTGACAAGCA";
            DnaParser parser = new DnaParser();
            Ast.Dna dna = parser.Parse(input);
            Assert.AreEqual<int>(10, dna.AsEnumerable().Count());
            Assert.IsFalse(dna.AsEnumerable().Any(c =>
            {
                for (int i = 0; i < 3; i++)
                {
                    bool flag = c.GetBase(i).Value.Equals(deleteBase.Value);
                    if (flag)
                        return true;
                }
                return false;
            }));
        }

        [TestMethod]
        public void ParseInputStringTest2()
        {
            INucleicAcid.DeleteBase deleteBase = new INucleicAcid.DeleteBase();

            string input = "T-TCCATAGGCTCCGCCCCCCTGACAAGCA";
            DnaParser parser = new DnaParser();
            Ast.Dna dna = parser.Parse(input);
            Assert.AreEqual<int>(10, dna.AsEnumerable().Count());
            Assert.IsTrue(dna.AsEnumerable().Any(c =>
            {
                for (int i = 0; i < 3; i++)
                {
                    bool flag = c.GetBase(i).Value.Equals(deleteBase.Value);
                    if (flag)
                        return true;
                }
                return false;
            }));
        }

        [TestMethod]
        public void ParseInputStringTest3()
        {
            INucleicAcid.DeleteBase deleteBase = new INucleicAcid.DeleteBase();

            string input = "TUTCCAUAGGCTCCGCCCCCCTGACAAGCA";
            DnaParser parser = new DnaParser();
            Assert.ThrowsException<Exception>(() =>
            {
                parser.Parse(input);
            });
        }

        [TestMethod]

        public void ParseInputStringTest4()
        {
            string input = "UUUCCAUAGGCUCCGCCCCCCUGACAAGCA";
            DnaParser parser = new DnaParser();
            Assert.ThrowsException<Exception>(() =>
            {
                parser.Parse(input);
            });
        }

        [TestMethod]

        public void ParseInputStringTest5()
        {
            string input = "T";
            DnaParser parser = new DnaParser();
            Assert.ThrowsException<Exception>(() =>
            {
                parser.Parse(input);
            });
        }

        [TestMethod]

        public void ParseInputStringTest6()
        {
            string input = "TT";
            DnaParser parser = new DnaParser();
            Assert.ThrowsException<Exception>(() =>
            {
                parser.Parse(input);
            });
        }

        [TestMethod]

        public void ParseInputStringTest7()
        {
            string input = "";
            DnaParser parser = new DnaParser();
            Assert.ThrowsException<Exception>(() =>
            {
                parser.Parse(input);
            });
        }

        [TestMethod]
        public void ParseInputTextReaderTest()
        {
            INucleicAcid.DeleteBase deleteBase = new INucleicAcid.DeleteBase();

            string input = "TTTCCATAGGCTCCGCCCCCCTGACAAGCA";
            DnaParser parser = new DnaParser();
            using StringReader reader = new StringReader(input);
            Ast.Dna dna = parser.Parse(reader);
            Assert.AreEqual<int>(10, dna.AsEnumerable().Count());
            Assert.IsFalse(dna.AsEnumerable().Any(c =>
            {
                for (int i = 0; i < 3; i++)
                {
                    bool flag = c.GetBase(i).Value.Equals(deleteBase.Value);
                    if (flag)
                        return true;
                }
                return false;
            }));
        }

        [TestMethod]
        public void ParseInputStreamTest()
        {
            INucleicAcid.DeleteBase deleteBase = new INucleicAcid.DeleteBase();

            string input = "TTTCCATAGGCTCCGCCCCCCTGACAAGCA";
            DnaParser parser = new DnaParser();
            using MemoryStream memory = new MemoryStream();
            memory.Write(Encoding.UTF8.GetBytes(input));
            memory.Position = 0;
            Ast.Dna dna = parser.Parse(memory);
            Assert.AreEqual<int>(10, dna.AsEnumerable().Count());
            Assert.IsFalse(dna.AsEnumerable().Any(c =>
            {
                for (int i = 0; i < 3; i++)
                {
                    bool flag = c.GetBase(i).Value.Equals(deleteBase.Value);
                    if (flag)
                        return true;
                }
                return false;
            }));
        }
    }
}