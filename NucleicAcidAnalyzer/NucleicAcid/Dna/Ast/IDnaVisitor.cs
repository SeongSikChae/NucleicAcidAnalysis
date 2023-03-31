namespace NucleicAcidAnalyzer.NucleicAcid.Dna.Ast
{
    using NucleicAcid.Ast;

    public interface IDnaVisitor : INucleicAcidVisitor
    {
        void Visit(Dna dna);
    }
}
