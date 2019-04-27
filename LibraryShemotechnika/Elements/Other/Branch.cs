using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShemotechnika.Elements.Other
{
   
    public class Branch     // Ветвь
    {
        

        public Node Node_1 { get; set; }
        public Node Node_2 { get; set; }

        List<IElementBase> Elements { get; set; }

        public Branch(Node node)
        {
            Elements = new List<IElementBase>();
            Node_1 = node;
        }

        public void Add(IElementBase element)
        {
            Elements.Add(element);
        }
    }
}
