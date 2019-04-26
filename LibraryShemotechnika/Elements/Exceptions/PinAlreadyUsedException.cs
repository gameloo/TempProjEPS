using LibraryShemotechnika.Elements.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShemotechnika.Elements.Exceptions
{
    public  class PinAlreadyUsedException : Exception
    {
        public Pin AlreadyUsedPin { get; private set; }

        public PinAlreadyUsedException(Pin pin, string optionalmsg = "msg"): base(optionalmsg)
        {
            AlreadyUsedPin = pin;
        }
    }
}
