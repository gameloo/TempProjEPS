using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShemotechnika.Elements.Other
{
    public class Node : IElementBase
    {
        public List<Pin> Pins { get; private set; }

        public Node()
        {
            Pins = new List<Pin>(3) { new Pin(this), new Pin(this), new Pin(this) };
        }

        public Pin this[int index]
        {
            get
            {
                return Pins[index];
            }
        }

        public void Connect(Pin pin)
        {
            var unconnectedPin = Pins.First(i => i.ConnectedPin == null);
            if (unconnectedPin != null)
            {
                unconnectedPin.ConnectToPin(pin);
            }
            else
            {
                var newPin = new Pin(this);
                Pins.Add(newPin);
                newPin.ConnectToPin(pin);
            }
        }

        public void Disconnect(IElementBase element)
        {
            Pins.First(i => i.ConnectedPin.Element.Equals(element))?.Disconnect();
        }
    }
}
