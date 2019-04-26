using LibraryShemotechnika.Elements.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShemotechnika.Elements.Other
{
    public class Pin
    {
        public IElementBase Element { get; private set; }
        public Pin ConnectedPin { get; private set; }

        public Pin (IElementBase element)
        {
            Element = element;
        }

        private static bool isRecursion = false;
        private static bool IsRecursion
        {
            get
            {
                isRecursion = !isRecursion;
                return !isRecursion;
            }
        }
        public void ConnectToPin(Pin pin)
        {
            if (pin.ConnectedPin != null) throw new PinAlreadyUsedException(pin);
            if (!IsRecursion) pin.ConnectToPin(this);
            ConnectedPin = pin;
        }
        public void Disconnect()
        {
            if (!IsRecursion) ConnectedPin?.Disconnect();
            ConnectedPin = null;
        }

    }
}
