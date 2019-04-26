using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShemotechnika.Elements
{
    public delegate void SubscribeOnDisconnect(IElementBase element);

    // Компонент с произвольным количеством входов/выходов
    public interface IElementBase
    {
        List<IElementBase> ConnectedElements { get; set; }
        SubscribeOnDisconnect SubscribeOnDisconnect { get; set; }
    }

    public static class IElementBaseExtend
    {
        private static bool isRecursionConnectTo = false;
        private static bool IsRecursionConnectTo
        {
            get
            {
                isRecursionConnectTo = !isRecursionConnectTo;
                return !isRecursionConnectTo;
            }
        }
        public static void Disconnect(this IElementBase sender, IElementBase element)
        {
            sender.ConnectedElements.Remove(element);
            //if (!IsRecursionConnectTo) element.ConnectedElements.Remove(sender);
            //if (!IsRecursionConnectTo) element.Disconnect(sender);
        }
    }
}
