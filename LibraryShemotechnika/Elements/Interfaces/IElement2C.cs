using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShemotechnika.Elements.Interfaces
{
    // Компонент с 2 коннекторами
    public interface IElement2C : IElementBase
    {
        IElementBase ConnectedElementOnInput { get; set; }
        IElementBase ConnectedElementOnOutput { get; set; }
    }


    public static class IElement2CExtend
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

        /// <summary>
        /// Подключить вход данного элемента к выходу компонента element
        /// </summary>
        /// <param name="element">Компонент, к выходу которого будет подключен данный элемент</param>
        public static void CleatOutputConnectWithElement(this IElement2C sender, IElement2C element) // swap?
        {
            if (!IsRecursionConnectTo)
            {
                sender.Disconnect((IElement2C)sender.ConnectedElementOnOutput);
                element.Disconnect((IElement2C)element.ConnectedElementOnInput);
                element.CleatInputConnectWithElement(sender);
            }
            sender.ConnectedElementOnOutput = element;
        }
        public static void CleatOutputConnectWithElement(this IElement2C sender, IElementBase element) // swap?
        {
            element.ConnectedElements.Add(sender);
            sender.ConnectedElementOnOutput = element;
        }
        public static void CleatInputConnectWithElement(this IElement2C sender, IElement2C element) // this swap?
        {
            if (!IsRecursionConnectTo)
            {
                sender.Disconnect((IElement2C)sender.ConnectedElementOnInput);
                element.Disconnect((IElement2C)element.ConnectedElementOnOutput);
                element.CleatOutputConnectWithElement(sender);
            }
            sender.ConnectedElementOnInput = element;
        }
        public static void Disconnect(this IElement2C sender, IElement2C element) // Возможно возникновение ошибки, если элемент будет подключен сам к себе (закольцован?) 
        {
            sender.ConnectedElements.Insert(sender.ConnectedElements.IndexOf(element), null);
            sender.ConnectedElements.Remove(element);
            if (element != null)
            {
                element.ConnectedElements.Insert(element.ConnectedElements.IndexOf(sender), null);
                element.ConnectedElements.Remove(sender);
            }
        }
        public static void Disconnect(this IElement2C sender, IElementBase element)
        {
            sender.ConnectedElements.Insert(sender.ConnectedElements.IndexOf(element), null);
            sender.ConnectedElements.Remove(element);
            element.ConnectedElements.Remove(sender);
        }
    }
}