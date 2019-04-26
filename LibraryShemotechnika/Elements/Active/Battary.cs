using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShemotechnika.Elements
{
    public class Battary
    {
        public double Voltage { get; set; }

        public List<IElementBase> ConnectedElements { get; set; }
    }
}
