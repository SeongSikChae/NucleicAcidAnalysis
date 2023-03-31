using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace NucleicAcidAnalyzer.NucleicAcid.Rna.Parser.Tests
{
    using NucleicAcid.Ast;
    using NucleicAcidAnalyzer.NucleicAcid.Rna.Ast;

    [TestClass]
    public class RnaParserTests
    {
        [TestMethod]
        public void ParseInputStringTest1()
        {
            INucleicAcid.DeleteBase deleteBase = new INucleicAcid.DeleteBase();

            string input = "UUUCCAUAGGCUCCGCCCCCCUGACAAGCA";
            RnaParser parser = new RnaParser();
            Ast.Rna rna = parser.Parse(input);
            Assert.AreEqual<int>(10, rna.AsEnumerable().Count());
            Assert.IsFalse(rna.AsEnumerable().Any(c =>
            {
                for (int i = 0; i < 3; i++)
                {
                    bool flag = c.GetBase(i).Value.Equals(deleteBase.Value);
                    if (flag)
                        return true;
                }
                return false;
            }));

            rna.SetCodesEnumerable(null);
            Assert.AreEqual(0, rna.AsEnumerable().Count());
        }

        [TestMethod]
        public void ParseInputStringTest2()
        {
            INucleicAcid.DeleteBase deleteBase = new INucleicAcid.DeleteBase();

            string input = "U-UCCAUAGGCUCCGCCCCCCUGACAAGCA";
            RnaParser parser = new RnaParser();
            Ast.Rna rna = parser.Parse(input);
            Assert.AreEqual<int>(10, rna.AsEnumerable().Count());
            Assert.IsTrue(rna.AsEnumerable().Any(c =>
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

            string input = "UTUCCATAGGCUCCGCCCCCCUGACAAGCA";
            RnaParser parser = new RnaParser();
            Assert.ThrowsException<Exception>(() =>
            {
                parser.Parse(input).AsEnumerable().ToList();
            });
        }

        [TestMethod]

        public void ParseInputStringTest4()
        {
            string input = "TTTCCATAGGCTCCGCCCCCCTGACAAGCA";
            RnaParser parser = new RnaParser();
            Assert.ThrowsException<Exception>(() =>
            {
                parser.Parse(input);
            });
        }

        [TestMethod]

        public void ParseInputStringTest5()
        {
            string input = "U";
            RnaParser parser = new RnaParser();
            Assert.ThrowsException<Exception>(() =>
            {
                parser.Parse(input).AsEnumerable().ToList();
            });
        }

        [TestMethod]

        public void ParseInputStringTest6()
        {
            string input = "UU";
            RnaParser parser = new RnaParser();
            Assert.ThrowsException<Exception>(() =>
            {
                parser.Parse(input).AsEnumerable().ToList();
            });
        }

        [TestMethod]

        public void ParseInputStringTest7()
        {
            string input = "";
            RnaParser parser = new RnaParser();
            Assert.ThrowsException<Exception>(() =>
            {
                parser.Parse(input);
            });
        }

        [TestMethod]
        public void ParseInputTextReaderTest()
        {
            INucleicAcid.DeleteBase deleteBase = new INucleicAcid.DeleteBase();

            string input = "UUUCCAUAGGCUCCGCCCCCCUGACAAGCA";
            RnaParser parser = new RnaParser();
            using StringReader reader = new StringReader(input);
            Ast.Rna rna = parser.Parse(reader);
            Assert.AreEqual<int>(10, rna.AsEnumerable().Count());
            Assert.IsFalse(rna.AsEnumerable().Any(c =>
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

            string input = "UUUCCAUAGGCUCCGCCCCCCUGACAAGCA";
            RnaParser parser = new RnaParser();
            using MemoryStream memory = new MemoryStream();
            memory.Write(Encoding.UTF8.GetBytes(input));
            memory.Position = 0;
            Ast.Rna rna = parser.Parse(memory);
            Assert.AreEqual<int>(10, rna.AsEnumerable().Count());
            Assert.IsFalse(rna.AsEnumerable().Any(c =>
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