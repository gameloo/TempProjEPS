using LibraryShemotechnika.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Solvers;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;
using LibraryShemotechnika.Elements.Other;

namespace LibraryShemotechnika
{
    public class Shema
    {
        bool flag;

        public Shema()
        {
            var resistor_1 = new Resistor() { TESTSTRING = "R1" };
            var resistor_2 = new Resistor() { TESTSTRING = "R2" };
            var resistor_3 = new Resistor() { TESTSTRING = "R3" };
            var resistor_4 = new Resistor() { TESTSTRING = "R4" };
            var node_1 = new Node();

            resistor_1.Pins[0].ConnectToPin(resistor_2.Pins[1]);
            node_1.Connect(resistor_3.Pins[0]);
            node_1.Disconnect(resistor_3);
            resistor_1[0].Disconnect();
        }


        private void Test_1()
        {
            double G1 = Math.Pow(25, -1);
            double G2 = Math.Pow(22, -1);
            double G3 = Math.Pow(42, -1);
            double G4 = Math.Pow(35, -1);
            double G5 = Math.Pow(51, -1);
            double G6 = Math.Pow(10, -1);
            double G7 = Math.Pow(47, -1);

            double E1 = 50.0;
            double E2 = 100.0;

            var matrixA = new[,]
                {
                    { (G1 + G6 + G7), (-G1 - G6),0 },
                    { (-G1 - G6), (G1+G3+G4+G6), (-G4) },
                    { 0, (-G4), (G2 + G4 + G5) }
                };

            var vectorB = new[] { (-E1 * G1), (E1 * G1), (E2 * G2) };

            var result = UtilityClass.Matrix.CalculateSystemOfLinearEquations(matrixA, vectorB);
        }

        public void Start()
        {
            flag = true;
            while (flag)
            {
                Console.WriteLine(DateTime.UtcNow);
                Thread.Sleep(100);
            }

        }

        public void Stop()
        {
            flag = false;
        }


        public static Direction DirectionAmperage(IElementBase sender, IElementBase targetElement)
        {

            return Direction.From;
        }
    }

    public enum Direction   // Направление
    {
        To,     // К
        From,   // От
        Havent  // Не имеет
    }

}
