using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace NucleicAcidAnalyzer.NucleicAcid.Dna.Parser
{
    using NucleicAcid.Ast;
    using NucleicAcid.Compiler;
    using NucleicAcid.Dna.Ast;
    using NucleicAcid.Parser;

    public sealed class DnaParser : AbstractNucleicAcidParser<Dna>
    {
        public override Dna VisitDna([NotNull] NucleicAcidCompilerParser.DnaContext context)
        {
            Dna dna = new Dna();
            dna.SetCodesEnumerable(VisitBaseCodeEnumerable(context.children));
            return dna;
        }

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
                    throw new Exception($"unknown dna base : '{node.GetText()}'");
            }
        }
    }
}
