namespace NucleicAcidAnalyzer.NucleicAcid.Transcriptor
{
    using Ast;

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

        public virtual void Visit(INucleicAcid.Dna dna)
        {
            throw new NotImplementedException();
        }

        public void Visit(INucleicAcid.DnaTripletCode dnaTripletCode)
        {
            INucleicAcid.RnaTripletCode rnaTripletCode = new INucleicAcid.RnaTripletCode();
            for (int index = 0; index < 3; index++)
            {
                dnaTripletCode.GetBase(index).Accept(this);
                rnaTripletCode.AddBase(index, (INucleicAcid.IBase)stack.Pop());
            }
            stack.Push(rnaTripletCode);
        }

        public virtual void Visit(INucleicAcid.Rna rna)
        {
            throw new NotImplementedException();
        }

        public void Visit(INucleicAcid.RnaTripletCode rnaTripletCode)
        {
            INucleicAcid.DnaTripletCode dnaTripletCode = new INucleicAcid.DnaTripletCode();
            for (int index = 0; index < 3; index++)
            {
                rnaTripletCode.GetBase(index).Accept(this);
                dnaTripletCode.AddBase(index, (INucleicAcid.IBase)stack.Pop());
            }
            stack.Push(dnaTripletCode);
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

    public sealed class RnaTranscriptor : AbstractTranscriptor<INucleicAcid.Dna, INucleicAcid.Rna>
    {
        public override void Visit(INucleicAcid.Dna dna)
        {
            List<INucleicAcid.TripletCode> tripletCodes = new List<INucleicAcid.TripletCode>();
            foreach(INucleicAcid.TripletCode tripletCode in dna.TripletCodes)
            {
                tripletCode.Accept(this);
                tripletCodes.Add((INucleicAcid.TripletCode) stack.Pop());
            }
            stack.Push(new INucleicAcid.Rna(tripletCodes));
        }
    }
}
