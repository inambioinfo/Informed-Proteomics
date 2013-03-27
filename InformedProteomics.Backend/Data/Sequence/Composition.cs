using InformedProteomics.Backend.Data.Biology;
using InformedProteomics.Backend.Utils;

namespace InformedProteomics.Backend.Data.Sequence
{
    public class Composition : IMolecule
    {
        public static readonly Composition Zero = new Composition(0, 0, 0, 0, 0);
		public static readonly Composition H2O = new Composition(0, 2, 0, 1, 0);
        public static readonly Composition NH3 = new Composition(0, 3, 1, 0, 0);
        public static readonly Composition NH2 = new Composition(0, 2, 1, 0, 0);
        public static readonly Composition OH = new Composition(0, 1, 0, 1, 0);
        public static readonly Composition CO = new Composition(1, 0, 0, 1, 0);
        public static readonly Composition Hydrogen = new Composition(0, 1, 0, 0, 0);

        public Composition(int c, int h, int n, int o, int s)
        {
            C = (short)c;
            H = (short)h;
            N = (short)n;
            O = (short)o;
            S = (short)s;
        }
        
        public Composition(Composition composition): this(composition.C, composition.H, 
            composition.N, composition.O, composition.S)
        {
            
        }

        #region Properties

        public short C { get; private set; }
        public short H { get; private set; }
        public short N { get; private set; }
        public short O { get; private set; }
        public short S { get; private set; }

        #endregion

        /// <summary>
        /// Gets the mono-isotopic mass
        /// </summary>
        public double GetMass()
        {
            return C*MassC + H*MassH + N*MassN + O*MassO + S*MassS;
        }

        /// <summary>
        /// Gets the m/z of ith isotope
        /// </summary>
        /// <param name="isotopeIndex">isotope index. 0 means mono-isotope, 1 means 2nd isotope, etc.</param>
        /// <returns></returns>
        public double GetIsotopeMass(int isotopeIndex)
        {
            return GetMass() + isotopeIndex * MassIsotope;
        }

        /// <summary>
        /// Gets the mono-isotopic nominal mass
        /// </summary>
        public int GetNominalMass()
        {
            return C*NominalMassC + H*NominalMassH + N*NominalMassN + O*NominalMassO + S*NominalMassS;
        }

        public Composition GetComposition()
        {
            return this;
        }

        public void AddComposition(Composition composition)
        {
            C += composition.C;
            H += composition.H;
            N += composition.N;
            O += composition.O;
            S += composition.S;
        }

        public float[] GetIsotopomerEnvelop()
        {
            throw new System.NotImplementedException();
        }

        public static Composition operator +(Composition c1, Composition c2)
        {
            return new Composition(c1.C + c2.C, c1.H + c2.H, c1.N + c2.N, c1.O + c2.O, c1.S + c2.S);
        }

        public static Composition operator -(Composition c1, Composition c2)
        {
            return new Composition(c1.C - c2.C, c1.H - c2.H, c1.N - c2.N, c1.O - c2.O, c1.S - c2.S);
        }

		public override string ToString()
		{
		    return "C" + C + "H" + H + "N" + N + "O" + O + "S" + S;
		}

        #region Masses of Atoms

        private static readonly double MassC = Atom.Get("C").Mass;
        private static readonly double MassH = Atom.Get("H").Mass;
        private static readonly double MassN = Atom.Get("N").Mass;
        private static readonly double MassO = Atom.Get("O").Mass;
        private static readonly double MassS = Atom.Get("S").Mass;
        private static readonly double MassIsotope = Atom.Get("C13").Mass - MassC;


        private static readonly int NominalMassC = Atom.Get("C").NominalMass;
        private static readonly int NominalMassH = Atom.Get("H").NominalMass;
        private static readonly int NominalMassN = Atom.Get("N").NominalMass;
        private static readonly int NominalMassO = Atom.Get("O").NominalMass;
        private static readonly int NominalMassS = Atom.Get("S").NominalMass;

        #endregion

    }
}
