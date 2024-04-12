using Antlr4.Runtime.Tree;

namespace NucleicAcidAnalyzer.NucleicAcid.Parser
{
    using Ast;

    public sealed class RnaParser : AbstractNucleicAcidParser
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
                case "U":
                    return new INucleicAcid.Uracil();
                case "-":
                    return new INucleicAcid.DeleteBase();
                default:
                    throw new NucleicAcidParseExpception($"unknown rna base : '{node.GetText()}'");
            }
        }
    }
}
