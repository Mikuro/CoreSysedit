using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tanks.ViewModels;

namespace tanks
{
    public class MainWindowVM
    {
        public ObservableCollection<BaseSheetVM> Sheets { get; set; }
        public MainWindowVM()
        {
            Sheets = new ObservableCollection<BaseSheetVM>();
        }

        public void DoLoad(string name)
        {
            var pathname = System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar + name;

            var noextname = System.IO.Path.GetFileNameWithoutExtension(name);

            Models.FlowDiagram fd1;

            using (var fs = new System.IO.FileStream(pathname, System.IO.FileMode.Open))
            {
                fd1 = Models.XMLUtil.DeserializeFlowDiagram(fs);
            }

            fd1.Fix();

            var dict = new Dictionary<Type, Type> {
                { typeof(Models.CalculationSystem), typeof(ViewModels.CalculationSystem)    },
                { typeof(Models.Component),         typeof(ViewModels.Component)            },
                { typeof(Models.ControlValve),      typeof(ViewModels.ControlValve)         },
                { typeof(Models.FlowDiagram),       typeof(ViewModels.FlowDiagram)          },
                { typeof(Models.FlowMeter),         typeof(ViewModels.FlowMeter)            },
                { typeof(Models.HeatExchanger),     typeof(ViewModels.HeatExchanger)        },
                { typeof(Models.LiquidLevelMeter),  typeof(ViewModels.LiquidLevelMeter)     },
                { typeof(Models.LiquidTank),        typeof(ViewModels.LiquidTank)           },
                { typeof(Models.Mixer),             typeof(ViewModels.Mixer)                },
                { typeof(Models.PIDController),     typeof(ViewModels.PIDController)        },
                { typeof(Models.Pipe),              typeof(ViewModels.Pipe)                 },
                { typeof(Models.PressureFeed),      typeof(ViewModels.PressureFeed)         },
                { typeof(Models.PressureGauge),     typeof(ViewModels.PressureGauge)        },
                { typeof(Models.PressureProduct),   typeof(ViewModels.PressureProduct)      },
                { typeof(Models.Pump),              typeof(ViewModels.Pump)                 },
                { typeof(Models.Signal),            typeof(ViewModels.Signal)               },
                { typeof(Models.Splitter),          typeof(ViewModels.Splitter)             },
                { typeof(Models.Strainer),          typeof(ViewModels.Strainer)             },
                { typeof(Models.Stream),            typeof(ViewModels.Stream)               },
                { typeof(Models.Thermometer),       typeof(ViewModels.Thermometer)          },
            };

            var fd = Models.DTOUtil.CreateFromFD(fd1, (type) => Activator.CreateInstance(dict[type])) as FlowDiagram;

            Sheets.Add(new FlowSheet { flowDiagram = fd, DocName = noextname });
        }

        public void DoLoad(string[] args)
        {
            foreach (var name in args)
                DoLoad(name);

            foreach (var sht in Sheets)
                sht.Initialize();

            foreach (var sht in Sheets)
                sht.Start();
        }

        public void Stop()
        {
            foreach (var sht in Sheets)
                sht.Stop();
        }

        public void Done()
        {
            foreach (var sht in Sheets)
                sht.Done();
        }
    }
}
