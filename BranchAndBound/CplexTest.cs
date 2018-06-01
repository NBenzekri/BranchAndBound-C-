//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using ILOG.Concert;
//using ILOG.CPLEX;

//namespace BranchAndBound
//{
//    class CplexTest
//    {
//        public static void Main(string[] args)
//        {
//            try {
//                Cplex cplex = new Cplex();
//                double[] lb = { 0.0, 0.0, 0.0 };
//                double[] ub = { 40.0, System.Double.MaxValue, System.Double.MaxValue };
//                INumVar[] x = cplex.NumVarArray(3, lb, ub);
//                var[0] = x;
//                double[] objvals = { 1.0, 2.0, 3.0 };
//                cplex.Add(cplex.Maximize(cplex.ScalProd(x, objvals)));
//                rng[0] = newIRange[2];
//                rng[0][0] = cplex.AddRange(-System.Double.MaxValue, 20.0);
//                rng[0][1] = cplex.AddRange(-System.Double.MaxValue, 30.0);
//                rng[0][0].Expr = cplex.Sum(cplex.Prod(-1.0, x[0]), cplex.Prod(1.0, x[1]), cplex.Prod(1.0, x[2]));
//                rng[0][1].Expr = cplex.Sum(cplex.Prod(1.0, x[0]), cplex.Prod(-3.0, x[1]), cplex.Prod(1.0, x[2]));
//                x[0].Name = "x1";
//                x[1].Name = "x2";
//                x[2].Name = "x3";
//                rng[0][0].Name = "c1";
//                rng[0][0].Name = "c2";
//                cplex.ExportModel("example.lp");
//                if (cplex.Solve()) { double[] x = cplex.GetValues(var[0]);
//                    double[] dj = cplex.GetReducedCosts(var[0]);
//                    double[] pi = cplex.GetDuals(rng[0]);
//                    double[] slack = cplex.GetSlacks(rng[0]);
//                    cplex.Output().WriteLine("Solutionstatus=" + cplex.GetStatus());
//                    cplex.Output().WriteLine("Solutionvalue=" + cplex.ObjValue);
//                    intnvars = x.Length;
//                    for (intj = 0; j < nvars; ++j) {
//                        cplex.Output().WriteLine("Variable" + j + ":Value=" + x[j] + "Reducedcost=" + dj[j]);
//                    }
//                    intncons = slack.Length;
//                    for (inti = 0; i < ncons; ++i) {
//                        cplex.Output().WriteLine("Constraint" + i + ":Slack=" + slack[i] + "Pi=" + pi[i]);
//                    }
//                }
//                cplex.End();
//            } catch (ILOG.Concert.Exceptione) {
//                System.Console.WriteLine("Concertexception'" + e + "'caught");
//            }
//        }

//    }
//}
