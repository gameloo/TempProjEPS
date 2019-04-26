using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShemotechnika.Elements.Other
{

    public enum Direction
    {
        Unknown,
        Towards,         // Навстречу
        From        // От
    }
    public class DirectionAmperage
    {
        public IElementBase InputApmerageElement { get; set; }
        public IElementBase OutputApmerageElement { get; set; }

        public Direction GetDirectionAmperage(IElementBase element) // Надо пересмотреть поиск элемента
        {
            if (InputApmerageElement.Equals(element)) return Direction.Towards;
            if (OutputApmerageElement.Equals(element)) return Direction.From;
            return Direction.Unknown;
        }
    }
}
