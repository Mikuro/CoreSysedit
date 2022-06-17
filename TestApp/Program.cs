using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using tanks.Models;
using tanks.Models.Solver;

using System.Xml;
using System.Xml.Linq;

namespace tanks
{
    class Program
    {
        static void Main(string[] args)
        {
            FlowDiagram fd1;

            using (var fs = new FileStream(args[0], FileMode.Open))
                fd1 = XMLUtil.DeserializeFlowDiagram(fs);

            fd1.Fix();


            tanks.TestCode.SolverTest(fd1);
        }
    }
}
