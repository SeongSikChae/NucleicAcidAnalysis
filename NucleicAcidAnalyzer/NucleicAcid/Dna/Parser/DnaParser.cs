using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace NucleicAcidAnalyzer.NucleicAcid.Dna.Parser
{
    using NucleicAcid.Ast;
    using NucleicAcid.Compiler;
    using NucleicAcid.Dna.Ast;

    public sealed class DnaParser : NucleicAcidCompilerBaseVisitor<INucleicAcid>
    {
        public Dna Parse(string input)
        {
            return Parse(new AntlrInputStream(input));
        }

        public Dna Parse(TextReader reader)
        {
            return Parse(new AntlrInputStream(reader));
        }

        public Dna Parse(Stream stream)
        {
            return Parse(new AntlrInputStream(stream));
        }

        private Dna Parse(AntlrInputStream stream)
        {
            NucleicAcidCompilerLexer lexer = new NucleicAcidCompilerLexer(stream);
            CommonTokenStream cts = new CommonTokenStream(lexer);
            NucleicAcidCompilerParser parser = new NucleicAcidCompilerParser(cts);
            parser.BuildParseTree = true;
            NucleicAcidCompilerParser.NucleicAcidContext context = parser.nucleicAcid();
            INucleicAcid instance = context.Accept(this);
            if (instance is null)
                throw new Exception($"invalid dna : '{context.GetText()}'");
            return (Dna)instance;
        }

        public override Dna VisitDna([NotNull] NucleicAcidCompilerParser.DnaContext context)
        {
            Dna dna = new Dna();
            foreach (IParseTree tree in context.children)
            {
                INucleicAcid.BaseCode baseCode = (INucleicAcid.BaseCode)tree.Accept(this);
                dna.AddCode(baseCode);
            }
            return dna;
        }

        public override INucleicAcid VisitDnaCode([NotNull] NucleicAcidCompilerParser.DnaCodeContext context)
        {
            INucleicAcid.BaseCode code = new INucleicAcid.BaseCode();
            int index = 0;
            foreach (IParseTree tree in context.children)
            {
                INucleicAcid.IBase @base = (INucleicAcid.IBase)tree.Accept(this);
                if (@base is null)
                    throw new Exception($"invalid dna code : '{context.GetText()}'");
                code.AddBase(index++, @base);
            }
            return code;
        }

        public override INucleicAcid VisitDnaBase([NotNull] NucleicAcidCompilerParser.DnaBaseContext context)
        {
            IParseTree tree = context.GetChild(0);
            if (tree is null)
                throw new Exception($"invalid dna base : '{context.GetText()}'");
            return (INucleicAcid.IBase)tree.Accept(this);
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
