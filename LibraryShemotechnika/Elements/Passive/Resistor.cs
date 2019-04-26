using LibraryShemotechnika.Elements.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShemotechnika.Elements
{
    public class Resistor : IElement2C
    {
        public string TESTSTRING { get; set; }

        public List<IElementBase> ConnectedElements { get; set; }
        public IElementBase ConnectedElementOnInput
        {
            get
            {
                return ConnectedElements[0];
            }
            set
            {
                ConnectedElements[0] = value;
            }
        }
        public IElementBase ConnectedElementOnOutput
        {
            get
            {
                return ConnectedElements[1];
            }
            set
            {
                ConnectedElements[1] = value;
            }
        }

        public double Resistance { get; set; }
        public Resistor()
        {
            ConnectedElements = new List<IElementBase>();
            ConnectedElements.AddRange(new IElementBase[]{ null, null });
        }

    }
}
