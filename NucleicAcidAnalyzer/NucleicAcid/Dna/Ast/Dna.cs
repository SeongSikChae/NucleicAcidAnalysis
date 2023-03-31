namespace NucleicAcidAnalyzer.NucleicAcid.Dna.Ast
{
    using NucleicAcid.Ast;

    public sealed class Dna : INucleicAcid.AbstractNucleicAcid
    {
        public override void Accept(INucleicAcidVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
