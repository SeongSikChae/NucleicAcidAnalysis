using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace NucleicAcidAnalyzer.NucleicAcid.Rna.Parser
{
    using NucleicAcid.Ast;
    using NucleicAcid.Compiler;
    using NucleicAcid.Rna.Ast;
    using NucleicAcid.Parser;

    public sealed class RnaParser : AbstractNucleicAcidParser<Rna>
    {
        public override Rna VisitRna([NotNull] NucleicAcidCompilerParser.RnaContext context)
        {
            Rna rna = new Rna();
            rna.SetCodesEnumerable(VisitBaseCodeEnumerable(context.children));
            return rna;
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
                case "U":
                    return new INucleicAcid.Uracil();
                case "-":
                    return new INucleicAcid.DeleteBase();
                default:
                    throw new Exception($"unknown rna base : '{node.GetText()}'");
            }
        }
    }
}
