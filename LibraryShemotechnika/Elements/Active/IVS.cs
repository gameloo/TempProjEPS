using System;
using System.Collections.Generic;
using LibraryShemotechnika.Elements.Interfaces;
using LibraryShemotechnika.Elements.Other;

namespace LibraryShemotechnika.Elements.Active
{
    // Ideal voltage source
    public class IVS : IActiveElement
    {
        public double Voltage { get; set; }
        public List<Pin> Pins { get; private set; } // pin[0] -  pin[1] +

        public double E
        {
            get
            {
                return Voltage;
            }
        }

        public IVS()
        {
            Pins = new List<Pin>(2) { new Pin(this), new Pin(this) };
        }

        public Pin this[int index]
        {
            get
            {
                return Pins[index];
            }
        }
    }
}
