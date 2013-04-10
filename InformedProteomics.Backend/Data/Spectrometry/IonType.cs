﻿using System.Diagnostics;
using InformedProteomics.Backend.Data.Biology;
using InformedProteomics.Backend.Data.Sequence;

namespace InformedProteomics.Backend.Data.Spectrometry
{
    public class IonType
    {
        public string Name { get; private set; }
        public Composition OffsetComposition { get; private set; }
        public int Charge { get; private set; }
        public bool IsPrefixIon { get; private set; }

        private readonly double _offsetMass;    // duplication but stored for performance

        public IonType(string name, Composition offsetComposition,
                       int charge, bool isPrefixIon)
        {
            Name = name;
            OffsetComposition = offsetComposition;
            Charge = charge;
            IsPrefixIon = isPrefixIon;
            _offsetMass = offsetComposition.GetMass();
        }

        public double GetMz(double cutMass)
        {
            return (cutMass + _offsetMass + Charge * Constants.H) / Charge;
        }

        public double GetMz(Composition prefixComposition)
        {
            Debug.Assert(prefixComposition != null, "prefixComposition must not be null");
            return GetMz(prefixComposition.GetMass());
        }

        public Ion GetIon(Composition cutComposition)
        {
            return new Ion(cutComposition + OffsetComposition, Charge);
        }

        public override string ToString()
        {
            return Name + "," + OffsetComposition + "," + _offsetMass + 
                "," + Charge + "," + IsPrefixIon;
        }

    }
  
}