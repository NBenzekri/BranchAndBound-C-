using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BranchAndBound
{
    class Program
    {
        //Methods
        //ERO for Matrix Operations, only need add multiple: r1 --> r1 + nr2
        public static double[,] ero(double[,] input, int r1, double n, int r2)
        {
            int cols = input.GetLength(1);
            for (int i = 0; i < cols; i++)
            {
                input[r1, i] += n * input[r2, i];
            }
            return input;
        }
        //::Pivot about some element @ (x,y)
        public static void pivot(double[,] input, int x, int y)
        {
            int m = input.GetLength(0);
            int n = input.GetLength(1);
            double pivot = input[x, y];
            for (int i = 0; i < n; i++)
            {
                input[x, i] = input[x, i] / pivot;
            }
            for (int i = 0; i < m; i++)
            {
                if (i == x) { }
                else { ero(input, i, -input[i, y], x); }
            }
        }
        //Return the pivot element by the ratio test (the basis column with a variable in this row is leaving variable)
        public static int[] ratioTest(double[,] input)
        {
            int m = input.GetLength(0);
            int n = input.GetLength(1);
            int rowIndex;
            int colIndex = 0;
            double[] ratMin = new double[m - 1];
            //find better way to do this later
            for (int i = 0; i < ratMin.Length; i++) { ratMin[i] = 9999999999; }
            for (int i = 1; i < n - 1; i++)
            {
                //apply Bland's Rule to prevent cycling?
                //first issue with new tableau, need to account for
                //negative and inf ratios, or cycling occurs
                if (input[0, i] < 0)
                {
                    for (int j = 1; j < m; j++)
                    {
                        ratMin[j - 1] = input[j, n - 1] / input[j, i];
                        if (input[j, n - 1] / input[j, i] < 0)
                        {
                            ratMin[j - 1] = 999999999999;
                        }

                        //Console.WriteLine("elem [j]: " + ratMin[j]);
                    }
                    colIndex = i;
                    break;
                }
            }
            //give me row with minimum value as determined by ratio test
            //foreach(double elem in ratMin){Write(elem +" ");}
            double min = ratMin.Min();
            rowIndex = Array.IndexOf(ratMin, min) + 1;
            int[] pivElem = new int[] { rowIndex, colIndex };
            return pivElem;
        }
        //pass in return from getObjective method
        public static bool isDone(double[,] input)
        {
            double[] objective = getObjective(input);
            foreach (double elem in objective)
            {
                if (elem < 0) { return false; }
            }
            return true;
        }
        //return array of objective function (i.e. "max cTx" returns cT)
        public static double[] getObjective(double[,] input)
        {
            int n = input.GetLength(1);
            double[] objective = new double[n - 2];
            for (int i = 1; i < n - 1; i++)
            {
                objective[i - 1] = input[0, i];
            }
            //foreach(double elem in objective){Write(elem + " ");}
            return objective;
        }
        //Return an array indexing every column: if not a basis give value -1,
        //if a basis column then the column index is the value of element in array
        public static int[] getBasis(double[,] input)
        {
            int m = input.GetLength(0);
            int n = input.GetLength(1);
            int[] bases = new int[n - 2];
            for (int i = 0; i < bases.Length; i++)
            {
                bases[i] = -1;
            }
            //end rows and columns are not part of basis matrix
            for (int c = 1; c < n - 1; c++)
            {
                double[] column = new double[m - 1];
                for (int r = 0; r < m - 1; r++)
                {
                    column[r] = input[r + 1, c];
                }
                int sum = 0;
                //Write("Column {0} consists of elements ", c);
                foreach (double elem in column)
                {
                    //incase 2 +1 = 1, but can't be pivot
                    //Write("{0} ", elem);
                    if (elem <= 1 && elem >= 1) { sum++; }
                    else if (elem <= 0 && elem >= 0) { }
                    else
                    {
                        sum--;
                    }
                }
                //Console.WriteLine();
                //for some reason sum == 1 does not work...possibly rounding error?
                if (sum <= 1 && sum >= 1)
                {
                    for (int i = 0; i < column.Length; i++)
                    {
                        if (column[i] == 1) { bases[c - 1] = i + 1; }
                    }
                }
            }
            return bases;
        }
        //for first attempt assume a basis exists
        //need to work on faster methods
        public static void print(double[,] mat)
        {   for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    Console.Write(Convert.ToString(mat[i, j]).PadLeft(6));
                }
                Console.WriteLine();
            }
        }
        public static void Main(string[] args)
        {
            ///Test Tableau----------------------------------------------------------------------------
            //int rows = 3; int cols = 7;
            double[,] tabx = new double[,]{
                            { 1,-2,-3,0,0, 0},
                            { 0, 1, 2,1,0,20},
                            { 0, 5, 5,0,1,60}};
            double[,] taby = new double[,]{
                            {1,0,3,1,0,0},
                            {0,1,2,-2,0,2},
                            {0,0,1,3,1,5}};
            double[,] tab1 = new double[,]{
                            { 1,-2,-3,0,0, 0, 0},
                            { 0, 1, 1,1,0, 0, 6},
                            { 0, 2, 1,0,1, 0,10},
                            { 0, -1,1,0,0, 1, 4}};
            double[,] tab = new double[,]{
                            { 1, 2, 1,-1, 0, 0, 0, 0},
                            { 0,-2, 1, 1, 1, 0, 0, 1},
                            { 0, 1,-1, 0, 0, 1, 0, 2},
                            { 0, 2,-3,-1, 0, 0, 1, 6}};

            ///----------------------------------------------------------------------------------------
            /*print(tab);
			Console.WriteLine(isDone(tab));
			//ero(tab, 1, 0.5, 2);
			//print(tab);
			pivot(tab, 2, 1);
			print(tab);
			Console.WriteLine(isDone(tab));
			pivot(tab, 1, 2);
			print(tab);
			Console.WriteLine(isDone(tab));*/
            //ratioTest(tab);

            //:::Algorithm::://
            uint count = 1;
            while (isDone(taby) == false)
            {
                //Print Step
                Console.WriteLine("Iteration " + count);
                count++;
                print(taby);
                //Perform Ratio Test
                int[] piv = ratioTest(taby);
                //Find leaving basis
                int[] basisArray = getBasis(taby);
                //Write("Basis Variable in Column: ");
                //foreach(int basis in basisArray){string str = basis > 0? "-" + basis.ToString() + "-" : "-X-"; Write(str);}
                Console.WriteLine();
                //int leaveVarIndex = Array.IndexOf(basisArray, piv[0]);
                //Console.WriteLine("Exit variable x[{0}]" , leaveVarIndex);
                //Console.WriteLine();
                pivot(taby, piv[0], piv[1]);
            }
            Console.WriteLine("FINAL TABLEAU");
            print(tabx);
            Console.WriteLine();
            Console.WriteLine("Algorithm termination after {0} iterations, with an optimized value of {1}.", count, taby[0, taby.GetLength(1) - 1]);
            Console.WriteLine("To obtain this, use the following variable assignments on x:");
            int[] solBases = getBasis(taby);
            double[] solution = new double[taby.GetLength(1) - 2];
            for (int i = 0; i < solution.Length; i++) { solution[i] = 0; }
            count = 0;
            foreach (int elem in solBases)
            {
                if (elem > 0) { solution[count] = taby[elem, taby.GetLength(1) - 1]; }
                count++;
            }
            for (int i = 0; i < solution.Length; i++) { Console.WriteLine("x{0} = {1}", i + 1, solution[i]); }
            Console.WriteLine();

        }
    }
}
