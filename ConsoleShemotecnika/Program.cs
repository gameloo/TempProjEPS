using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibraryShemotechnika;

namespace ConsoleShemotecnika
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainObj = new Shema();
            Task.Run(() => mainObj.Start());
            Thread.Sleep(5000);
            Console.WriteLine("write stop");
            switch (Console.ReadLine())
            {
                case "stop":
                    {
                        mainObj.Stop();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Ok");
                        break;
                    }
            }
            Console.WriteLine("End?");
            Console.ReadKey();
        }
    }
}
