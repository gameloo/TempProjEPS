using LibraryShemotechnika.Elements.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShemotechnika.Elements
{
    // Компонент с произвольным количеством входов/выходов
    public interface IElementBase
    {
        List<Pin> Pins { get; }
    }

    public static class IElementBaseExtend
    {
        
    }
}
