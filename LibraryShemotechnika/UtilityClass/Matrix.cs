using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;
using MathNet.Numerics.LinearAlgebra.Solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShemotechnika.UtilityClass
{
    public class Matrix
    {
        public static double[] CalculateSystemOfLinearEquations(double[,] matrixSystem, double[] matrixRightSide)
        {
            var matrixA = DenseMatrix.OfArray(matrixSystem);
            var vectorB = new DenseVector(matrixRightSide);
            var iterationCountStopCriterion = new IterationCountStopCriterion<double>(1000);
            var residualStopCriterion = new ResidualStopCriterion<double>(1e-10);
            var monitor = new Iterator<double>(iterationCountStopCriterion, residualStopCriterion);
            var solver = new BiCgStab();
            return matrixA.SolveIterative(vectorB, solver, monitor).ToArray();
        }

    }
}
