using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShemotechnika.Elements.Other
{
   
    public class Branch     // Ветвь
    {
        public Node Input { get; set; }
        public Node Output { get; set; }

        public List<IElementBase> Elements { get; set; }

        public Branch (List<IElementBase> elements, Node node_1, Node node_2)
        {
            var battary = elements.First((i) => { if (i is Battary) return true; else return false; });
            if (battary == null)
            {
                Input = node_1;
                Output = node_2;
            }
            else
            {
                
            }
        }

    }
}
