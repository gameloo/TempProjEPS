using LibraryShemotechnika.Elements.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShemotechnika.Elements.Other
{

    public enum Direction
    {
        N1toN2,
        N2toN1
    }

    public class Branch     // Ветвь
    {
        public Node Node_1 { get; set; }
        public Node Node_2 { get; set; }
        public Direction Direction { get; set; } = Direction.N2toN1;

        public List<IElementBase> Elements { get; set; }

        public Branch(Node node)
        {
            Elements = new List<IElementBase>();
            Node_1 = node;
        }

        public void Add(IElementBase element)
        {
            Elements.Add(element);
            if (element is IActiveElement)
            {
                Direction = FindDirection(element);
            }
        }

        private Direction FindDirection(IElementBase element)
        {
            if (element is Node)
            {
                if (element.Equals(Node_1)) return Direction.N1toN2;
                else return Direction.N2toN1;
            }
            else return FindDirection(element.Pins[0].ConnectedPin.Element);
        }

        public DirectionAmperage DirectionAmperage(Node node)
        {
            if (node.Equals(Node_1))
            {
                if (Direction == Direction.N1toN2) return Other.DirectionAmperage.From;
                else return Other.DirectionAmperage.Towards;
            }
            else
            {
                if (Direction == Direction.N2toN1) return Other.DirectionAmperage.From;
                else return Other.DirectionAmperage.Towards;
            }
        }
    }
}
