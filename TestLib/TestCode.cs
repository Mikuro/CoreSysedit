using System;
using System.Collections.Generic;
using System.Globalization;
using tanks.Models;
using tanks.Models.Solver;

namespace tanks
{
    public static class TestCode
    {
        static double LogAbs(double x)
        {
            double r = Math.Log10(Math.Abs(x) + 1e-20);

            return r;
        }
/*
        static double[] matmulsub(double[][] A, double[] x, double[] b)
        {
            double[] r = new double[A.Length];

            for (int j = 0; j < r.Length; j++)
            {
                double s = 0;

                for (int i = 0; i < x.Length; i++)
                {
                    s += A[j][i] * x[i];
                }

                r[j] = s - b[j];
            }
            return r;
        }
*/
        public static void SolverTest(FlowDiagram fd)
        {
            SRKSolver.ProcessLinks(fd);
            Flash.fugcnt = 0;
            Flash.funcnt = 0;
            SRKSolver.SolveStatic(fd);
            DCSSolver.SolveInstrument(fd);
            DCSSolver.OneStep(fd);

            var ld = new Dictionary<string, List<ProcessUnit>>();

            foreach (var item in fd.Items)
            {
                var unit = item as tanks.Models.ProcessUnit;
                if (unit == null) continue;

                List<ProcessUnit> list;

                if (!ld.ContainsKey(unit.GetType().Name))
                {
                    list = new List<ProcessUnit>();
                    ld[unit.GetType().Name] = list;
                }
                else
                    list = ld[unit.GetType().Name];

                list.Add(unit);
            }
            foreach (var key in ld.Keys)
            {
                Console.WriteLine("{0}", key);
                switch (key)
                {
                    case "PressureFeed":
                        break;
                    case "HeatExchanger":
                        foreach (var unit in ld[key])
                        {
                            {
                                double dP = (unit as HeatExchanger).f1.P -
                                    (unit as HeatExchanger).p1.P;

                                double c = ((unit as HeatExchanger).PdHdes *
                                        (unit as HeatExchanger).f1.V *
                                        (unit as HeatExchanger).DHdes *
                                        (unit as HeatExchanger).f1.Mw) /
                                        ((unit as HeatExchanger).WHdes * (unit as HeatExchanger).WHdes);

                                double f = Math.Sqrt(Math.Abs(dP / c)) * Math.Sign(dP / c);

                                Console.WriteLine("{0} is {1}", unit.Id,
                                    LogAbs(((unit as HeatExchanger).f1.F - f)));
                            }
                            {
                                double dP = (unit as HeatExchanger).f2.P -
                                    (unit as HeatExchanger).p2.P;

                                double c = ((unit as HeatExchanger).PdLdes *
                                        (unit as HeatExchanger).f2.V *
                                        (unit as HeatExchanger).DLdes *
                                        (unit as HeatExchanger).f2.Mw) /
                                        ((unit as HeatExchanger).WLdes * (unit as HeatExchanger).WLdes);

                                double f = Math.Sqrt(Math.Abs(dP / c)) * Math.Sign(dP / c);

                                Console.WriteLine("{0} is {1}", unit.Id,
                                    LogAbs(((unit as HeatExchanger).f2.F - f)));
                            }
                            double LMTD;
                            double Th1 = (unit as HeatExchanger).f1.T;
                            double Th2 = (unit as HeatExchanger).p1.T;
                            double Tc1 = (unit as HeatExchanger).f2.T;
                            double Tc2 = (unit as HeatExchanger).p2.T;
                            double Hh1 = (unit as HeatExchanger).f1.HL * (unit as HeatExchanger).f1.RL
                                + (unit as HeatExchanger).f1.HV * (unit as HeatExchanger).f1.RV;
                            double Hh2 = (unit as HeatExchanger).p1.HL * (unit as HeatExchanger).p1.RL
                                + (unit as HeatExchanger).p1.HV * (unit as HeatExchanger).p1.RV;
                            double Hc1 = (unit as HeatExchanger).f2.HL * (unit as HeatExchanger).f2.RL
                                + (unit as HeatExchanger).f2.HV * (unit as HeatExchanger).f2.RV;
                            double Hc2 = (unit as HeatExchanger).p2.HL * (unit as HeatExchanger).p2.RL
                                + (unit as HeatExchanger).p2.HV * (unit as HeatExchanger).p2.RV;

                            if (((Th1 - Tc2) / (Th2 - Tc1)) == 1.0)
                                LMTD = ((Th1 - Tc2) + (Th2 - Tc1)) / 2;
                            else
                                LMTD = ((Th1 - Tc2) - (Th2 - Tc1)) / Math.Log((Th1 - Tc2) / (Th2 - Tc1));

                            double Qi = 3.6 * (unit as HeatExchanger).U * (unit as HeatExchanger).A * LMTD / 1000;
                            double Qih = (unit as HeatExchanger).f1.F * (Hh1 - Hh2);
                            double Qic = (unit as HeatExchanger).f2.F * (Hc2 - Hc1);
                            Console.WriteLine("{0} | {1} {2} {3}", unit.Id, LogAbs(Qi - Qih), LogAbs(Qih - Qic), LogAbs(Qi - Qic));
                        }
                        break;
                    case "Splitter":
                        foreach (var unit in ld[key])
                        {
                            double ftot = 0;

                            if ((unit as Splitter).p1 != null)
                                ftot += (unit as Splitter).p1.F;
                            if ((unit as Splitter).p2 != null)
                                ftot += (unit as Splitter).p2.F;
                            if ((unit as Splitter).p3 != null)
                                ftot += (unit as Splitter).p3.F;

                            Console.WriteLine("{0} | {1}", unit.Id,
                                LogAbs(((unit as Splitter).f1.F - ftot) /*/ (ftot + 1.0)*/));
                        }
                        break;
                    case "ControlValve":
                        foreach (var unit in ld[key])
                        {
                            double f;
                            if ((unit as ControlValve).pos < (unit as ControlValve).pos0)
                            {
                                f = 0;
                            }
                            else
                            {

                                double dP = (unit as ControlValve).f1.P -
                                    (unit as ControlValve).p1.P;

                                double cv = (unit as ControlValve).CV * Math.Pow(
                                    (unit as ControlValve).R, ((unit as ControlValve).pos - 1.0));

                                double c = 2.74 * cv *
                                    (unit as ControlValve).Cff / Math.Sqrt(
                                        (unit as ControlValve).f1.V *
                                        (unit as ControlValve).f1.Mw);

                                f = c * Math.Sqrt(Math.Abs(dP)) * Math.Sign(dP);
                            }

                            Console.WriteLine("{0} is {1}", unit.Id,
                                LogAbs(((unit as ControlValve).f1.F - f)));
                        }
                        break;
                    case "LiquidTank":
                        foreach (var unit in ld[key])
                        {
                            double tu = 0.0;
                            for (int i = 0; i < (unit as LiquidTank).u.Count; i++)
                            {
                                tu += (unit as LiquidTank).u[i] * fd.Components[i].Mw;
                            }

                            double dP = (9.80665E-3) * tu / (unit as LiquidTank).A;

                            Console.WriteLine("{0} is {1}", unit.Id,
                                LogAbs(((unit as LiquidTank).p1.P - (unit as LiquidTank).fe.P - dP) /*/ (dP + 1.0)*/));
                        }
                        break;
                    case "Pipe":
                        foreach (var unit in ld[key])
                        {
                            double dP = (9.80665E-3) * (unit as Pipe).Hdiff *
                                (unit as Pipe).f1.Mw / (unit as Pipe).f1.V;

                            Console.WriteLine("{0} is {1}", unit.Id,
                                LogAbs(((unit as Pipe).p1.P - (unit as Pipe).f1.P + dP) /*/ (dP + 1.0)*/));
                        }
                        break;
                    case "Mixer":
                        foreach (var unit in ld[key])
                        {
                            double ftot = 0;

                            if ((unit as Mixer).f1 != null)
                                ftot += (unit as Mixer).f1.F;
                            if ((unit as Mixer).f2 != null)
                                ftot += (unit as Mixer).f2.F;
                            if ((unit as Mixer).f3 != null)
                                ftot += (unit as Mixer).f3.F;

                            Console.WriteLine("{0} | {1}", unit.Id,
                                LogAbs(((unit as Mixer).p1.F - ftot) /*/ (ftot + 1.0)*/));
                        }
                        break;
                    case "Pump":
                        foreach (var unit in ld[key])
                        {
                            double alpha = ((unit as Pump).Hs - (unit as Pump).Hd) /
                                ((unit as Pump).Vd * (unit as Pump).Vd);

                            double H0 = (unit as Pump).pos * (unit as Pump).pos * (unit as Pump).Hs;

                            double V = (unit as Pump).f1.F * (unit as Pump).f1.V;
                            double dP = (9.80665E-3) * (H0 - alpha * V * V) *
                                (unit as Pump).f1.Mw / (unit as Pump).f1.V;

                            Console.WriteLine("{0} is {1}", unit.Id,
                                LogAbs(((unit as Pump).p1.P - (unit as Pump).f1.P - dP)/*/(dP+1.0)*/));
                        }
                        break;
                    case "PressureProduct":
                        break;
                }
            }

            foreach (var lnk in fd.Links)
            {
                tanks.Models.Stream str = lnk as tanks.Models.Stream;

                if (str == null) continue;

                Console.WriteLine("link {0} {1} P={2} F={3} T={4} RV={5} Mw={6} po={7}",
                str.From,
                str.To,
                str.P.ToString("0.0##", CultureInfo.InvariantCulture),
                str.F.ToString("0.0##", CultureInfo.InvariantCulture),
                str.T.ToString("0.0##", CultureInfo.InvariantCulture),
                str.RV.ToString("0.0##", CultureInfo.InvariantCulture),
                str.Mw.ToString("0.0##", CultureInfo.InvariantCulture),
                (str.Mw / str.V).ToString("0.0##", CultureInfo.InvariantCulture)
                );
            }
            Console.WriteLine("");
        }

    }
}
