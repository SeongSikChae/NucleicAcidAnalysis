namespace NucleicAcidAnalyzer.NucleicAcid.Rna.Ast
{
    using NucleicAcid.Ast;

    public interface IRanVisitor : INucleicAcidVisitor
    {
        void Visit(Rna rna);
    }
}
