namespace NucleicAcidAnalyzer.NucleicAcid.Rna.Ast
{
    using NucleicAcid.Ast;

    public sealed class Rna : INucleicAcid.AbstractNucleicAcid
    {
        public override void Accept(INucleicAcidVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
