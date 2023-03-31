using Microsoft.VisualStudio.TestTools.UnitTesting;
using NucleicAcidAnalyzer.NucleicAcid.Dna.Parser;

namespace NucleicAcidAnalyzer.NucleicAcid.Transcriptor.Tests
{
    [TestClass]
    public class RnaTranscriptorTests
    {
        [TestMethod]
        public void TranscriptionTest1()
        {
            using FileStream fs = new FileStream("TestData\\DNASequence1.txt", FileMode.Open, FileAccess.Read);

            DnaParser parser = new DnaParser();
            Dna.Ast.Dna dna = parser.Parse(fs);

            RnaTranscriptor transcriptor = new RnaTranscriptor();
            Rna.Ast.Rna rna = transcriptor.Transcription(dna);
            rna.AsEnumerable().ToList();
        }

        [TestMethod]
        public void TranscriptionTest2()
        {
            using FileStream fs = new FileStream("TestData\\DNASequence1.txt", FileMode.Open, FileAccess.Read);

            DnaParser parser = new DnaParser();
            Dna.Ast.Dna dna = parser.Parse(fs);
            dna.SetCodesEnumerable(null);

            RnaTranscriptor transcriptor = new RnaTranscriptor();
            Rna.Ast.Rna rna = transcriptor.Transcription(dna);
            rna.AsEnumerable().ToList();
        }
    }
}