namespace NucleicAcidAnalyzer.NucleicAcid.Dna.Ast
{
    using NucleicAcid.Ast;

    public sealed class Dna : INucleicAcid.AbstractNucleicAcid
    {
        public override void Accept(INucleicAcidVisitor visitor)
        {
            if (visitor is not IDnaVisitor)
                throw new Exception("invalid IDnaVisitor");
            ((IDnaVisitor)visitor).Visit(this);
        }
    }
}
