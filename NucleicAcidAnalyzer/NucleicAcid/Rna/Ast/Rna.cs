namespace NucleicAcidAnalyzer.NucleicAcid.Rna.Ast
{
    using NucleicAcid.Ast;

    public sealed class Rna : INucleicAcid.AbstractNucleicAcid
    {
        public override void Accept(INucleicAcidVisitor visitor)
        {
            if (visitor is not IRanVisitor)
                throw new Exception("invalid IRnaVisitor");
            ((IRanVisitor)visitor).Visit(this);
        }
    }
}
