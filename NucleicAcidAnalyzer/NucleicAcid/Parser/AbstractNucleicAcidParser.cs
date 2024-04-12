using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace NucleicAcidAnalyzer.NucleicAcid.Parser
{
    using Ast;
    using Compiler;

    public abstract class AbstractNucleicAcidParser : NucleicAcidBaseVisitor<INucleicAcid>
    {
        public INucleicAcid Parse(string input)
        {
            return Parse(new AntlrInputStream(input));
        }

        public INucleicAcid Parse(TextReader reader)
        {
            return Parse(new AntlrInputStream(reader));
        }

        public INucleicAcid Parse(Stream stream)
        {
            return Parse(new AntlrInputStream(stream));
        }

        public INucleicAcid Parse(AntlrInputStream stream)
        {
            NucleicAcidLexer lexer = new NucleicAcidLexer(stream);
            CommonTokenStream cts = new CommonTokenStream(lexer);
            NucleicAcidParser parser = new NucleicAcidParser(cts);
            parser.BuildParseTree = true;
            NucleicAcidParser.NucleicAcidContext context = parser.nucleicAcid();
            INucleicAcid instance = context.Accept(this);
            if (instance is null)
                throw new NucleicAcidParseExpception($"invalid nucleicAcid : '{context.GetText()}'");
            return instance;
        }

        public override INucleicAcid VisitDna([NotNull] NucleicAcidParser.DnaContext context)
        {
            List<INucleicAcid.TripletCode> tripletCodes = new List<INucleicAcid.TripletCode>();
            INucleicAcid.Dna dna = new INucleicAcid.Dna(tripletCodes);
            NucleicAcidParser.DnaTripletCodeContext[] dnaTripletCodeContexts = context.dnaTripletCode();
            foreach (NucleicAcidParser.DnaTripletCodeContext dnaTripletCodeContext in dnaTripletCodeContexts)
                tripletCodes.Add((INucleicAcid.TripletCode)dnaTripletCodeContext.Accept(this));
            return dna;
        }

        public override INucleicAcid VisitDnaTripletCode([NotNull] NucleicAcidParser.DnaTripletCodeContext context)
        {
            INucleicAcid.DnaTripletCode dnaTripletCode = new INucleicAcid.DnaTripletCode();
            NucleicAcidParser.DnaBaseContext[] dnaBaseContexts = context.dnaBase();
            int index = 0;
            foreach (NucleicAcidParser.DnaBaseContext dnaBaseContext in dnaBaseContexts)
                dnaTripletCode.AddBase(index++, (INucleicAcid.IBase)dnaBaseContext.Accept(this));
            return dnaTripletCode;
        }

        public override INucleicAcid VisitDnaBase([NotNull] NucleicAcidParser.DnaBaseContext context)
        {
            if (context.ChildCount > 0)
                return context.GetChild<ITerminalNode>(0).Accept(this);
            throw new NucleicAcidParseExpception($"dna base parser error : '{context.GetText()}'");
        }

        public override INucleicAcid VisitRna([NotNull] NucleicAcidParser.RnaContext context)
        {
            List<INucleicAcid.TripletCode> tripletCodes = new List<INucleicAcid.TripletCode>();
            INucleicAcid.Rna rna = new INucleicAcid.Rna(tripletCodes);
            NucleicAcidParser.RnaTripletCodeContext[] rnaTripletCodeContexts = context.rnaTripletCode();
            foreach (NucleicAcidParser.RnaTripletCodeContext rnaTripletCodeContext in rnaTripletCodeContexts)
                tripletCodes.Add((INucleicAcid.TripletCode)rnaTripletCodeContext.Accept(this));
            return rna;
        }

        public override INucleicAcid VisitRnaTripletCode([NotNull] NucleicAcidParser.RnaTripletCodeContext context)
        {
            INucleicAcid.RnaTripletCode rnaTripletCode = new INucleicAcid.RnaTripletCode();
            NucleicAcidParser.RnaBaseContext[] rnaBaseContexts = context.rnaBase();
            int index = 0;
            foreach (NucleicAcidParser.RnaBaseContext rnaBaseContext in rnaBaseContexts)
                rnaTripletCode.AddBase(index++, (INucleicAcid.IBase)rnaBaseContext.Accept(this));
            return rnaTripletCode;
        }

        public override INucleicAcid VisitRnaBase([NotNull] NucleicAcidParser.RnaBaseContext context)
        {
            if (context.ChildCount > 0)
                return context.GetChild<ITerminalNode>(0).Accept(this);
            throw new NucleicAcidParseExpception($"rna base parser error : '{context.GetText()}'");
        }
    }
}
