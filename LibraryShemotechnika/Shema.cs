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
using LibraryShemotechnika.Elements.Active;
using LibraryShemotechnika.Elements.Interfaces;

namespace LibraryShemotechnika
{
    public class Shema
    {
        bool flag;

        public Shema()
        {
            var resistor_1 = new Resistor() { TESTSTRING = "R1", R = 25 };
            var resistor_2 = new Resistor() { TESTSTRING = "R2", R = 22 };
            var resistor_3 = new Resistor() { TESTSTRING = "R3", R = 42 };
            var resistor_4 = new Resistor() { TESTSTRING = "R4", R = 35 };
            var resistor_5 = new Resistor() { TESTSTRING = "R5", R = 51 };
            var resistor_6 = new Resistor() { TESTSTRING = "R6", R = 10 };
            var resistor_7 = new Resistor() { TESTSTRING = "R7", R = 47 };
            var ivs_1 = new IVS() { Voltage = 50 };
            var ivs_2 = new IVS() { Voltage = 100 };
            var node_1 = new Node() { Node_name = "Node_1" };
            var node_2 = new Node() { Node_name = "Node_2" };
            var node_3 = new Node() { Node_name = "Node_3" };
            var node_4 = new Node() { Node_name = "Node_4" };

            ivs_1[1].ConnectToPin(resistor_1[0]);
            node_1.Connect(ivs_1[0]);
            node_1.Connect(resistor_6[0]);
            node_1.Connect(resistor_7[0]);

            node_2.Connect(resistor_1[1]);
            node_2.Connect(resistor_6[1]);
            node_2.Connect(resistor_3[1]);
            node_2.Connect(resistor_4[0]);

            node_3.Connect(resistor_4[1]);
            node_3.Connect(resistor_5[1]);
            node_3.Connect(resistor_2[1]);

            ivs_2[1].ConnectToPin(resistor_2[0]);
            node_4.Connect(ivs_2[0]);
            node_4.Connect(resistor_7[1]);
            node_4.Connect(resistor_3[0]);
            node_4.Connect(resistor_5[0]);

            var nodes = new List<Node>();
            var branches = new List<Branch>();
            FindBranchesAndNodes(node_1, nodes, branches);

            var fi = FindNodesPotential(nodes, branches);
        }

        /// <summary>
        /// Поиск узлов и ветвей в схеме
        /// </summary>
        /// <param name="node">Любой узел в схеме</param>
        /// <param name="retunedListNodes"></param>
        /// <param name="retunedListBranches"></param>
        private void FindBranchesAndNodes(Node node, List<Node> retunedListNodes, List<Branch> retunedListBranches)
        {
            var listPassedElements = new List<IElementBase>(); 
            BruteForseScheme(node, retunedListNodes, listPassedElements, retunedListBranches);
        }

        /* (Начало)
         * Обход всей схемы
         *  1. Выбирается произвольный узел
         *  2. Перебираюся все входящие в узел подключеные элементы:
         *  если элемент еще не встречался, то создается новая ветвь началом которой
         *  считается данный узел а первым элементом ветви становится данный элемент, иначе - пропускается.
         *  Уточнение: новые ветви создаются с каждым найденым элементом
         *  3. Форимруются ветви:
         *  в ветвь добавляются все следующие элементы за первым, вплоть до узла, который будет считаться концом ветви 
         *  4. Если узел еще не встречался, то переходим к шагу 2.
        */
        private void BruteForseScheme(Node node, List<Node> passedNodes, List<IElementBase> passedElements, List<Branch> branches)
        {
            passedNodes.Add(node);

            foreach (var pin in node.Pins)
            {
                if (!passedElements.Any(i => i.Equals(pin.ConnectedPin.Element)))
                {
                    var branch = new Branch(node);
                    branches.Add(branch);
                    var nextNode = FillBranches(branch, node, pin.ConnectedPin.Element, passedElements);
                    if (!passedNodes.Any(i => i.Equals(nextNode)))
                    {
                        BruteForseScheme(nextNode, passedNodes, passedElements, branches);
                    }
                }
            }
        }

        private Node FillBranches(Branch branch, IElementBase previousElement, IElementBase element, List<IElementBase> passedElements)
        {
            branch.Add(element);
            passedElements.Add(element); // мб сократить кол во вызовов add

            foreach (var pin in element.Pins)
            {
                if (!previousElement.Equals(pin.ConnectedPin.Element))
                {
                    if (pin.ConnectedPin.Element is Node)
                    {
                        branch.Node_2 = pin.ConnectedPin.Element as Node;
                        return pin.ConnectedPin.Element as Node;
                    }
                    else
                    {
                        return FillBranches(branch, element, pin.ConnectedPin.Element, passedElements);
                    }
                }
            }
            throw new InvalidOperationException(); // Схема не замкнута?
        }
        /*
         *  (Конец)
         *  Обход всей схемы
        */

        /* (Начало)
         * Поиск потенциалов узлов
        */
        private double[] FindNodesPotential(List<Node> nodes, List<Branch> branches)
        {
            var matrixA = new double[nodes.Count - 1, nodes.Count - 1];
            var matrixB = new double[nodes.Count - 1];
            var nodesFhiZero = nodes.Last();
            foreach (var node in nodes)
            {
                if (node.Equals(nodesFhiZero)) break;

                var connectedBranches = branches.FindAll((i) => i.Node_1.Equals(node) || i.Node_2.Equals(node));
                foreach (var branch in connectedBranches)
                {
                    var multipler = branch.DirectionAmperage(node) == DirectionAmperage.Towards ? 1.0 : -1.0;

                    var activeElement = branch.Elements.FirstOrDefault(i => i is IActiveElement);
                    if (activeElement != null)
                    {
                        foreach (var element in branch.Elements)
                        {
                            if (element is IPassiveElement)
                            {
                                matrixB[nodes.IndexOf(node)] += multipler * (activeElement as IActiveElement).E * (element as IPassiveElement).G;
                            }
                        }
                    }

                    foreach (var element in branch.Elements)
                    {
                        if (element is IPassiveElement)
                        {
                            if (!branch.Node_1.Equals(nodesFhiZero))
                                matrixA[nodes.IndexOf(node), nodes.IndexOf(branch.Node_1)] += branch.Direction == Direction.N1toN2 ? -1 * multipler * (element as IPassiveElement).G : multipler * (element as IPassiveElement).G;
                            if (!branch.Node_2.Equals(nodesFhiZero))
                                matrixA[nodes.IndexOf(node), nodes.IndexOf(branch.Node_2)] += branch.Direction == Direction.N2toN1 ? -1 * multipler * (element as IPassiveElement).G :  multipler * (element as IPassiveElement).G;
                        }
                    }
                }
            }

            return UtilityClass.Matrix.CalculateSystemOfLinearEquations(matrixA, matrixB);
        }
        /* (Конец)
         * Поиск потенциалов узлов
        */

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

    }

}
