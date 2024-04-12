using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NucleicAcidAnalyzer.NucleicAcid.Transcriptor.Tests
{
    using Ast;
    using NucleicAcidAnalyzer.NucleicAcid.Parser.Tests;
    using Parser;
    using System.Diagnostics;

    [TestClass]
    public class RnaTranscriptorTests
    {
        [TestMethod]
        public void TranscriptionTest1()
        {
            using FileStream fs = new FileStream("TestData/DNASequence1.txt", FileMode.Open, FileAccess.Read);

            DnaParser parser = new DnaParser();
            INucleicAcid.Dna dna = (INucleicAcid.Dna) parser.Parse(fs);

            RnaTranscriptor transcriptor = new RnaTranscriptor();
            INucleicAcid.Rna rna = transcriptor.Transcription(dna);

            using (StringWriter writer = new StringWriter())
            {
                rna.Accept(new NucleicAcidWriter(writer));
                Trace.WriteLine(writer);
            }
        }
    }
}