using Antlr4.Runtime.Tree;

namespace NucleicAcidAnalyzer.NucleicAcid.Parser
{
    using Ast;

    public sealed class DnaParser : AbstractNucleicAcidParser
    {
        public override INucleicAcid.IBase VisitTerminal(ITerminalNode node)
        {
            switch (node.GetText())
            {
                case "A":
                    return new INucleicAcid.Adenine();
                case "C":
                    return new INucleicAcid.Cytosine();
                case "G":
                    return new INucleicAcid.Guanine();
                case "T":
                    return new INucleicAcid.Thymine();
                case "-":
                    return new INucleicAcid.DeleteBase();
                default:
                    throw new NucleicAcidParseExpception($"unknown dna base : '{node.GetText()}'");
            }
        }
    }
}
