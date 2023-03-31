using Antlr4.Runtime;

namespace NucleicAcidAnalyzer.NucleicAcid.Parser
{
    using Antlr4.Runtime.Misc;
    using Antlr4.Runtime.Tree;
    using NucleicAcid.Ast;
    using NucleicAcid.Compiler;

    public interface INucleicAcidParser<R> where R : INucleicAcid
    {
        R Parse(string input);

        R Parse(TextReader reader);

        R Parse(Stream stream);

        R Parse(AntlrInputStream stream);
    }

    public abstract class AbstractNucleicAcidParser<R> : NucleicAcidCompilerBaseVisitor<INucleicAcid>, INucleicAcidParser<R> where R : INucleicAcid
    {
        public R Parse(string input)
        {
            return Parse(new AntlrInputStream(input));
        }

        public R Parse(TextReader reader)
        {
            return Parse(new AntlrInputStream(reader));
        }

        public R Parse(Stream stream)
        {
            return Parse(new AntlrInputStream(stream));
        }

        public R Parse(AntlrInputStream stream)
        {
            NucleicAcidCompilerLexer lexer = new NucleicAcidCompilerLexer(stream);
            CommonTokenStream cts = new CommonTokenStream(lexer);
            NucleicAcidCompilerParser parser = new NucleicAcidCompilerParser(cts);
            parser.BuildParseTree = true;
            NucleicAcidCompilerParser.NucleicAcidContext context = parser.nucleicAcid();
            INucleicAcid instance = context.Accept(this);
            if (instance is null)
                throw new Exception($"invalid {typeof(R)} : '{context.GetText()}'");
            return (R)instance;
        }

        protected IEnumerable<INucleicAcid.BaseCode> VisitBaseCodeEnumerable(IList<IParseTree> children)
        {
            foreach (IParseTree tree in children)
            {
                yield return (INucleicAcid.BaseCode)tree.Accept(this);
            }
        }

        public override INucleicAcid VisitDnaCode([NotNull] NucleicAcidCompilerParser.DnaCodeContext context)
        {
            return VisitBaseCode(context);
        }

        public override INucleicAcid VisitRnaCode([NotNull] NucleicAcidCompilerParser.RnaCodeContext context)
        {
            return VisitBaseCode(context);
        }

        private INucleicAcid VisitBaseCode(ParserRuleContext context)
        {
            INucleicAcid.BaseCode code = new INucleicAcid.BaseCode();
            int index = 0;
            foreach (IParseTree tree in context.children)
            {
                INucleicAcid.IBase @base = (INucleicAcid.IBase)tree.Accept(this);
                if (@base is null)
                    throw new Exception($"invalid base code : '{context.GetText()}'");
                code.AddBase(index++, @base);
            }
            return code;
        }

        public override INucleicAcid VisitDnaBase([NotNull] NucleicAcidCompilerParser.DnaBaseContext context)
        {
            return VisitBase(context);
        }

        public override INucleicAcid VisitRnaBase([NotNull] NucleicAcidCompilerParser.RnaBaseContext context)
        {
            return VisitBase(context);
        }

        private INucleicAcid VisitBase(ParserRuleContext context)
        {
            IParseTree tree = context.GetChild(0);
            if (tree is null)
                throw new Exception($"invalid base : '{context.GetText()}'");
            return (INucleicAcid.IBase)tree.Accept(this);
        }
    }
}
