using LibraryShemotechnika.Elements.Interfaces;
using LibraryShemotechnika.Elements.Other;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShemotechnika.Elements
{
    public class Resistor : IPassiveElement
    {
        public string TESTSTRING { get; set; }
        public List<Pin> Pins { get; private set; }

        public double R { get; set; } = 1;
        public double G
        {
            get
            {
                return 1 / R;
            }
        }

        public Resistor()
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
