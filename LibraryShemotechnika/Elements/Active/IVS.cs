using System;
using System.Collections.Generic;
using LibraryShemotechnika.Elements.Other;

namespace LibraryShemotechnika.Elements.Active
{
    // Ideal voltage source
    public class IVS : IElementBase
    {
        public List<Pin> Pins { get; private set; }
    }
}
