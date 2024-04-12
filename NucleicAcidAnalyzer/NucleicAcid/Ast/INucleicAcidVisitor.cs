namespace NucleicAcidAnalyzer.NucleicAcid.Ast
{
    public interface INucleicAcidVisitor
    {
        void Visit(INucleicAcid.Adenine @base);

        void Visit(INucleicAcid.Cytosine @base);

        void Visit(INucleicAcid.Guanine @base);

        void Visit(INucleicAcid.Thymine @base);

        void Visit(INucleicAcid.Uracil @base);

        void Visit(INucleicAcid.DeleteBase @base);

        void Visit(INucleicAcid.DnaTripletCode dnaTripletCode);

        void Visit(INucleicAcid.RnaTripletCode rnaTripletCode);

        void Visit(INucleicAcid.Dna dna);

        void Visit(INucleicAcid.Rna rna);
    }
}
