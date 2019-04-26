using LibraryShemotechnika.Elements.Other;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShemotechnika.Elements
{
    public class Resistor : IElementBase
    {
        public string TESTSTRING { get; set; }
        public List<Pin> Pins { get; private set; }

        public Resistor()
        {
            Pins = new List<Pin>(2) { new Pin(this), new Pin(this) };          
        }
    }
}
