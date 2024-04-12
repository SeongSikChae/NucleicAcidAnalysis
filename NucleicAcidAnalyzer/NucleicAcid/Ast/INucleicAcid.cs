using System.Collections.ObjectModel;

namespace NucleicAcidAnalyzer.NucleicAcid.Ast
{
    public interface INucleicAcid
    {
        void Accept(INucleicAcidVisitor visitor);

        public abstract class AbstractNucleicAcid(IList<TripletCode> tripletCodes) : INucleicAcid
        {
            public ReadOnlyCollection<TripletCode> TripletCodes { get; } = new ReadOnlyCollection<TripletCode>(tripletCodes);

            public abstract void Accept(INucleicAcidVisitor visitor);
        }

        public sealed class Dna(IList<TripletCode> tripletCodes) : AbstractNucleicAcid(tripletCodes)
        {
            public override void Accept(INucleicAcidVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        public sealed class Rna(IList<TripletCode> tripletCodes) : AbstractNucleicAcid(tripletCodes)
        {
            public override void Accept(INucleicAcidVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        public abstract class TripletCode : INucleicAcid 
        {
            public IBase GetBase(int index)
            {
                return bases[index];
            }

            private readonly IBase[] bases = new IBase[3];

            public void AddBase(int index, IBase @base)
            {
                bases[index] = @base;
            }

            public abstract void Accept(INucleicAcidVisitor visitor);
        }

        public sealed class DnaTripletCode : TripletCode
        {
            public override void Accept(INucleicAcidVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        public sealed class RnaTripletCode : TripletCode
        {
            public override void Accept(INucleicAcidVisitor visitor)
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
