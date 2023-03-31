namespace NucleicAcidAnalyzer.NucleicAcid.Transcriptor
{
    using NucleicAcid.Ast;
    using NucleicAcid.Dna.Ast;
    using NucleicAcid.Rna.Ast;

    public interface ITranscriptor<TInput, TOutput> : INucleicAcidVisitor where TInput : INucleicAcid where TOutput : INucleicAcid
    {
        TOutput Transcription(TInput input);
    }

    public abstract class AbstractTranscriptor<TInput, TOutput> : ITranscriptor<TInput, TOutput> where TInput : INucleicAcid where TOutput : INucleicAcid
    {
        protected readonly Stack<INucleicAcid> stack = new Stack<INucleicAcid>();

        public TOutput Transcription(TInput input)
        {
            input.Accept(this);
            INucleicAcid instance = stack.Pop();
            stack.Clear();
            return (TOutput)instance;
        }

        public abstract void Visit(INucleicAcid nucleicAcid);

        protected IEnumerable<INucleicAcid.BaseCode> VisitBaseCodeEnumerable(IEnumerable<INucleicAcid.BaseCode> enumerable)
        {
            foreach (INucleicAcid.BaseCode code in enumerable)
            {
                code.Accept(this);
                yield return (INucleicAcid.BaseCode) stack.Pop();                
            }
        }

        public void Visit(INucleicAcid.BaseCode code)
        {
            INucleicAcid.BaseCode baseCode = new INucleicAcid.BaseCode();
            for (int i = 0; i < 3; i++)
            {
                code.GetBase(i).Accept(this);
                INucleicAcid.IBase @base = (INucleicAcid.IBase)stack.Pop();
                baseCode.AddBase(i, @base);
            }
            stack.Push(baseCode);
        }

        public void Visit(INucleicAcid.Adenine @base)
        {
            stack.Push(new INucleicAcid.Uracil());
        }

        public void Visit(INucleicAcid.Cytosine @base)
        {
            stack.Push(new INucleicAcid.Guanine());
        }

        public void Visit(INucleicAcid.Guanine @base)
        {
            stack.Push(new INucleicAcid.Cytosine());
        }

        public void Visit(INucleicAcid.Thymine @base)
        {
            stack.Push(new INucleicAcid.Adenine());
        }

        public void Visit(INucleicAcid.Uracil @base)
        {
            stack.Push(new INucleicAcid.Adenine());
        }

        public void Visit(INucleicAcid.DeleteBase @base)
        {
            stack.Push(new INucleicAcid.DeleteBase());
        }
    }

    public sealed class RnaTranscriptor : AbstractTranscriptor<Dna, Rna>
    {
        public override void Visit(INucleicAcid nucleicAcid)
        {
            Rna rna = new Rna();
            IEnumerable<INucleicAcid.BaseCode> enumerable = ((Dna) nucleicAcid).AsEnumerable();
            rna.SetCodesEnumerable(VisitBaseCodeEnumerable(enumerable));
            stack.Push(rna);
        }
    }
}
