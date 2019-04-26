using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShemotechnika.Elements.Other
{
    public class Node: IElementBase
    {
        public List<Branch> Branches { get; set; }

        public List<IElementBase> ConnectedElements { get; set; }

        public SubscribeOnDisconnect SubscribeOnDisconnect { get; set; }

        public Node()
        {
            ConnectedElements = new List<IElementBase>();
        }
    }
}
