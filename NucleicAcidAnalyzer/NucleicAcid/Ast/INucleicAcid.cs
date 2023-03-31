namespace NucleicAcidAnalyzer.NucleicAcid.Ast
{
    public interface INucleicAcid
    {
        void Accept(INucleicAcidVisitor visitor);

        public abstract class AbstractNucleicAcid : INucleicAcid
        {
            protected IEnumerable<BaseCode>? codesEnumerable;

            public void SetCodesEnumerable(IEnumerable<BaseCode> codesEnumerable)
            {
                this.codesEnumerable = codesEnumerable;
            }

            public IEnumerable<BaseCode> AsEnumerable()
            {
                return codesEnumerable is not null ? codesEnumerable : Enumerable.Empty<BaseCode>();
            }

            public abstract void Accept(INucleicAcidVisitor visitor);
        }

        public sealed class BaseCode : INucleicAcid
        {
            private readonly IBase[] bases = new IBase[3];

            public IBase GetBase(int index)
            {
                return bases[index];
            }

            public void AddBase(int index, IBase @base)
            {
                bases[index] = @base;
            }

            public void Accept(INucleicAcidVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        public interface IBase : INucleicAcid
        {
            string Value { get; }
        }

        public sealed class Adenine : IBase
        {
            public string Value => "A";

            public void Accept(INucleicAcidVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        public sealed class Cytosine : IBase
        {
            public string Value => "C";

            public void Accept(INucleicAcidVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        public sealed class Guanine : IBase
        {
            public string Value => "G";

            public void Accept(INucleicAcidVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        public sealed class Thymine : IBase
        {
            public string Value => "T";

            public void Accept(INucleicAcidVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        public sealed class Uracil : IBase
        {
            public string Value => "U";

            public void Accept(INucleicAcidVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        public sealed class DeleteBase : IBase
        {
            public string Value => "-";

            public void Accept(INucleicAcidVisitor visitor)
            {
                visitor.Visit(this);
            }
        }
    }
}
