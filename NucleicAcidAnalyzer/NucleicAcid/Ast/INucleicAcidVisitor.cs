namespace NucleicAcidAnalyzer.NucleicAcid.Ast
{
    public interface INucleicAcidVisitor
    {
        void Visit(INucleicAcid nucleicAcid);

        void Visit(INucleicAcid.BaseCode code);

        void Visit(INucleicAcid.Adenine @base);

        void Visit(INucleicAcid.Cytosine @base);

        void Visit(INucleicAcid.Guanine @base);

        void Visit(INucleicAcid.Thymine @base);

        void Visit(INucleicAcid.Uracil @base);

        void Visit(INucleicAcid.DeleteBase @base);
    }
}
