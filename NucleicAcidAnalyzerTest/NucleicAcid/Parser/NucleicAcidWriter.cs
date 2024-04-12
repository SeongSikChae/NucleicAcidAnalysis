namespace NucleicAcidAnalyzer.NucleicAcid.Parser.Tests
{
    using Ast;

    internal class NucleicAcidWriter(TextWriter writer) : INucleicAcidVisitor
    {
        public void Visit(INucleicAcid.Adenine @base)
        {
            writer.Write(@base.Value);
        }

        public void Visit(INucleicAcid.Cytosine @base)
        {
            writer.Write(@base.Value);
        }

        public void Visit(INucleicAcid.Guanine @base)
        {
            writer.Write(@base.Value);
        }

        public void Visit(INucleicAcid.Thymine @base)
        {
            writer.Write(@base.Value);
        }

        public void Visit(INucleicAcid.Uracil @base)
        {
            writer.Write(@base.Value);
        }

        public void Visit(INucleicAcid.DeleteBase @base)
        {
            writer.Write(@base.Value);
        }

        public void Visit(INucleicAcid.DnaTripletCode tripletCode)
        {
            for (int index = 0; index < 3; index++)
                tripletCode.GetBase(index).Accept(this);
        }

        public void Visit(INucleicAcid.RnaTripletCode tripletCode)
        {
            for (int index = 0; index < 3; index++)
                tripletCode.GetBase(index).Accept(this);
        }

        public void Visit(INucleicAcid.Dna dna)
        {
            foreach (INucleicAcid.TripletCode tripletCode in dna.TripletCodes)
                tripletCode.Accept(this);
        }

        public void Visit(INucleicAcid.Rna rna)
        {
            foreach (INucleicAcid.TripletCode tripletCode in rna.TripletCodes)
                tripletCode.Accept(this);
        }
    }
}
