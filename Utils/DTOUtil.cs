
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace tanks.Models
{
	public static class DTOUtil
	{
        public static tanks.Models.FlowDiagram CreateFromFD(tanks.Models.FlowDiagram src, Func<Type,object> getInstance)
        {
            return CreateFromFlowDiagram(src, getInstance);
        }

        public static void UpdateFD(tanks.Models.FlowDiagram dst, tanks.Models.FlowDiagram src)
        {
            UpdateFlowDiagram(dst,src);
        }

        private static void Update_double_array(Double[] dst, Double[] src)
        {
            for(int i=0; i<src.Length; i++)
                dst[i]=src[i];
        }

        private static void UpdateCollection_double_array(Collection<Double[]> dst, Collection<Double[]> src)
        {
            for(int i=0; i<src.Count; i++)
            Update_double_array(dst[i],src[i]);
        }

        private static void UpdateCollection_double(Collection<double> dst, Collection<double> src)
        {
            for(int i=0; i<src.Count; i++)
                dst[i]=src[i];
        }

        private static void UpdateCollection_Component(Collection<tanks.Models.Component> dst, Collection<tanks.Models.Component> src)
        {
            for(int i=0; i<src.Count; i++)
                UpdateComponent(dst[i], src[i]);
        }

        private static void UpdateCollection_CalculationSystem(Collection<tanks.Models.CalculationSystem> dst, Collection<tanks.Models.CalculationSystem> src)
        {
            for(int i=0; i<src.Count; i++)
                UpdateCalculationSystem(dst[i], src[i]);
        }

        private static void UpdateCollection_ModelLink(Collection<tanks.Models.ModelLink> dst, Collection<tanks.Models.ModelLink> src)
        {
            for(int i=0; i<src.Count; i++)
            {
                switch(src[i].GetType().Name)
                {
                case "Signal":
                    UpdateSignal(dst[i] as tanks.Models.Signal, src[i] as tanks.Models.Signal);
                    break;
                case "Stream":
                    UpdateStream(dst[i] as tanks.Models.Stream, src[i] as tanks.Models.Stream);
                    break;
                default:
                        throw new Exception("fail");
                }
            }
        }

        private static void UpdateCollection_ModelObject(Collection<tanks.Models.ModelObject> dst, Collection<tanks.Models.ModelObject> src)
        {
            for(int i=0; i<src.Count; i++)
            {

                switch(src[i].GetType().Name)
                {
                case "ControlValve":
                    UpdateControlValve(dst[i] as tanks.Models.ControlValve, src[i] as tanks.Models.ControlValve);
                    break;
                case "FlowMeter":
                    UpdateFlowMeter(dst[i] as tanks.Models.FlowMeter, src[i] as tanks.Models.FlowMeter);
                    break;
                case "HeatExchanger":
                    UpdateHeatExchanger(dst[i] as tanks.Models.HeatExchanger, src[i] as tanks.Models.HeatExchanger);
                    break;
                case "LiquidLevelMeter":
                    UpdateLiquidLevelMeter(dst[i] as tanks.Models.LiquidLevelMeter, src[i] as tanks.Models.LiquidLevelMeter);
                    break;
                case "LiquidTank":
                    UpdateLiquidTank(dst[i] as tanks.Models.LiquidTank, src[i] as tanks.Models.LiquidTank);
                    break;
                case "Mixer":
                    UpdateMixer(dst[i] as tanks.Models.Mixer, src[i] as tanks.Models.Mixer);
                    break;
                case "PIDController":
                    UpdatePIDController(dst[i] as tanks.Models.PIDController, src[i] as tanks.Models.PIDController);
                    break;
                case "Pipe":
                    UpdatePipe(dst[i] as tanks.Models.Pipe, src[i] as tanks.Models.Pipe);
                    break;
                case "PressureFeed":
                    UpdatePressureFeed(dst[i] as tanks.Models.PressureFeed, src[i] as tanks.Models.PressureFeed);
                    break;
                case "PressureGauge":
                    UpdatePressureGauge(dst[i] as tanks.Models.PressureGauge, src[i] as tanks.Models.PressureGauge);
                    break;
                case "PressureProduct":
                    UpdatePressureProduct(dst[i] as tanks.Models.PressureProduct, src[i] as tanks.Models.PressureProduct);
                    break;
                case "Pump":
                    UpdatePump(dst[i] as tanks.Models.Pump, src[i] as tanks.Models.Pump);
                    break;
                case "Splitter":
                    UpdateSplitter(dst[i] as tanks.Models.Splitter, src[i] as tanks.Models.Splitter);
                    break;
                case "Strainer":
                    UpdateStrainer(dst[i] as tanks.Models.Strainer, src[i] as tanks.Models.Strainer);
                    break;
                case "Thermometer":
                    UpdateThermometer(dst[i] as tanks.Models.Thermometer, src[i] as tanks.Models.Thermometer);
                    break;
                default:
                        throw new Exception("fail");
                }
            }
        }

		private static void UpdateCalculationSystem(tanks.Models.CalculationSystem dst, tanks.Models.CalculationSystem src)
		{
                UpdateCollection_double_array(dst.SRKKIJ, src.SRKKIJ);
		}
		private static void UpdateComponent(tanks.Models.Component dst, tanks.Models.Component src)
		{
                dst.Id = src.Id;
                dst.Mw = src.Mw;
                dst.Tc = src.Tc;
                dst.Pc = src.Pc;
                dst.Zc = src.Zc;
                dst.Omega = src.Omega;
                dst.Higa = src.Higa;
                dst.Higb = src.Higb;
                dst.Higc = src.Higc;
                dst.Higd = src.Higd;
                dst.Hige = src.Hige;
                dst.Higf = src.Higf;
                dst.ZRA = src.ZRA;
		}
		private static void UpdateControlValve(tanks.Models.ControlValve dst, tanks.Models.ControlValve src)
		{
                dst.CV = src.CV;
                dst.R = src.R;
                dst.pos0 = src.pos0;
                dst.topen = src.topen;
                dst.tclose = src.tclose;
                dst.tlag = src.tlag;
                dst.Cff = src.Cff;
                dst.mv = src.mv;
                dst.pos = src.pos;
                dst.velosity = src.velosity;
                dst.X = src.X;
                dst.Y = src.Y;
                dst.Id = src.Id;
		}
		private static void UpdateFlowDiagram(tanks.Models.FlowDiagram dst, tanks.Models.FlowDiagram src)
		{
                UpdateCollection_Component(dst.Components, src.Components);
                UpdateCollection_ModelObject(dst.Items, src.Items);
                UpdateCollection_ModelLink(dst.Links, src.Links);
                UpdateCollection_CalculationSystem(dst.Systems, src.Systems);
                dst.Width = src.Width;
                dst.Height = src.Height;
		}
		private static void UpdateFlowMeter(tanks.Models.FlowMeter dst, tanks.Models.FlowMeter src)
		{
                dst.Fmax = src.Fmax;
                dst.Fmin = src.Fmin;
                dst.unit = src.unit;
                dst.t_unit = src.t_unit;
                dst.F = src.F;
                dst.X = src.X;
                dst.Y = src.Y;
                dst.Id = src.Id;
		}
		private static void UpdateHeatExchanger(tanks.Models.HeatExchanger dst, tanks.Models.HeatExchanger src)
		{
                dst.WHdes = src.WHdes;
                dst.DHdes = src.DHdes;
                dst.PdHdes = src.PdHdes;
                dst.WLdes = src.WLdes;
                dst.DLdes = src.DLdes;
                dst.PdLdes = src.PdLdes;
                dst.A = src.A;
                dst.U = src.U;
                dst.X = src.X;
                dst.Y = src.Y;
                dst.Id = src.Id;
		}
		private static void UpdateLiquidLevelMeter(tanks.Models.LiquidLevelMeter dst, tanks.Models.LiquidLevelMeter src)
		{
                dst.Ll = src.Ll;
                dst.Lh = src.Lh;
                dst.Lbase = src.Lbase;
                dst.unit = src.unit;
                dst.L = src.L;
                dst.X = src.X;
                dst.Y = src.Y;
                dst.Id = src.Id;
		}
		private static void UpdateLiquidTank(tanks.Models.LiquidTank dst, tanks.Models.LiquidTank src)
		{
                dst.T = src.T;
                dst.A = src.A;
                UpdateCollection_double(dst.u, src.u);
                dst.X = src.X;
                dst.Y = src.Y;
                dst.Id = src.Id;
		}
		private static void UpdateMixer(tanks.Models.Mixer dst, tanks.Models.Mixer src)
		{
                dst.X = src.X;
                dst.Y = src.Y;
                dst.Id = src.Id;
		}
		private static void UpdatePIDController(tanks.Models.PIDController dst, tanks.Models.PIDController src)
		{
                dst.PB = src.PB;
                dst.TI = src.TI;
                dst.TD = src.TD;
                dst.PVSPAN = src.PVSPAN;
                dst.PVBASE = src.PVBASE;
                dst.INCDEC = src.INCDEC;
                dst.MVSPAN = src.MVSPAN;
                dst.MVH = src.MVH;
                dst.MVL = src.MVL;
                dst.SV = src.SV;
                dst.PV = src.PV;
                dst.MV = src.MV;
                dst.CMOD = src.CMOD;
                dst.SVM = src.SVM;
                dst.MVM = src.MVM;
                dst.e = src.e;
                dst.E = src.E;
                dst.X = src.X;
                dst.Y = src.Y;
                dst.Id = src.Id;
		}
		private static void UpdatePipe(tanks.Models.Pipe dst, tanks.Models.Pipe src)
		{
                dst.Hdiff = src.Hdiff;
                dst.X = src.X;
                dst.Y = src.Y;
                dst.Id = src.Id;
		}
		private static void UpdatePressureFeed(tanks.Models.PressureFeed dst, tanks.Models.PressureFeed src)
		{
                UpdateCollection_double(dst.x, src.x);
                dst.P = src.P;
                dst.T = src.T;
                dst.X = src.X;
                dst.Y = src.Y;
                dst.Id = src.Id;
		}
		private static void UpdatePressureGauge(tanks.Models.PressureGauge dst, tanks.Models.PressureGauge src)
		{
                dst.Pl = src.Pl;
                dst.Ph = src.Ph;
                dst.unit = src.unit;
                dst.P = src.P;
                dst.X = src.X;
                dst.Y = src.Y;
                dst.Id = src.Id;
		}
		private static void UpdatePressureProduct(tanks.Models.PressureProduct dst, tanks.Models.PressureProduct src)
		{
                dst.P = src.P;
                dst.X = src.X;
                dst.Y = src.Y;
                dst.Id = src.Id;
		}
		private static void UpdatePump(tanks.Models.Pump dst, tanks.Models.Pump src)
		{
                dst.Hd = src.Hd;
                dst.Hs = src.Hs;
                dst.Vd = src.Vd;
                dst.dd = src.dd;
                dst.Wd = src.Wd;
                dst.Ws = src.Ws;
                dst.pos0 = src.pos0;
                dst.tlag = src.tlag;
                dst.mv = src.mv;
                dst.pos = src.pos;
                dst.X = src.X;
                dst.Y = src.Y;
                dst.Id = src.Id;
		}
		private static void UpdateSignal(tanks.Models.Signal dst, tanks.Models.Signal src)
		{
                dst.Id = src.Id;
                dst.From = src.From;
                dst.To = src.To;
                dst.Figures = src.Figures;
		}
		private static void UpdateSplitter(tanks.Models.Splitter dst, tanks.Models.Splitter src)
		{
                dst.X = src.X;
                dst.Y = src.Y;
                dst.Id = src.Id;
		}
		private static void UpdateStrainer(tanks.Models.Strainer dst, tanks.Models.Strainer src)
		{
                dst.Wd = src.Wd;
                dst.DP = src.DP;
                dst.dd = src.dd;
                dst.X = src.X;
                dst.Y = src.Y;
                dst.Id = src.Id;
		}
		private static void UpdateStream(tanks.Models.Stream dst, tanks.Models.Stream src)
		{
                dst.Type = src.Type;
                UpdateCollection_double(dst.x, src.x);
                dst.F = src.F;
                dst.P = src.P;
                dst.T = src.T;
                dst.RL = src.RL;
                dst.HL = src.HL;
                UpdateCollection_double(dst.XL, src.XL);
                dst.RV = src.RV;
                dst.HV = src.HV;
                UpdateCollection_double(dst.XV, src.XV);
                dst.Mw = src.Mw;
                dst.V = src.V;
                dst.Id = src.Id;
                dst.From = src.From;
                dst.To = src.To;
                dst.Figures = src.Figures;
		}
		private static void UpdateThermometer(tanks.Models.Thermometer dst, tanks.Models.Thermometer src)
		{
                dst.Tl = src.Tl;
                dst.Th = src.Th;
                dst.unit = src.unit;
                dst.T = src.T;
                dst.X = src.X;
                dst.Y = src.Y;
                dst.Id = src.Id;
		}

// ---------------------------------
    
        private static Double[] CreateFrom_double_array(Double[] src)
        {
            var dst = new Double[src.Length];

            for(int i=0; i<src.Length; i++)
                dst[i]=src[i];

            return dst;
        }

        private static Collection<Double[]> CreateFromCollection_double_array(Collection<Double[]> src, Func<Type,object> getInstance)
        {
            var dst = new Collection<Double[]>();

            for(int i=0; i<src.Count; i++)
                dst.Add(CreateFrom_double_array(src[i]));

            return dst;
        }

        private static Collection<double> CreateFromCollection_double(Collection<double> src, Func<Type,object> getInstance)
        {
            var dst = new Collection<double>();

            for(int i=0; i<src.Count; i++)
                dst.Add(src[i]);

            return dst;
        }

        private static Collection<tanks.Models.Component> CreateFromCollection_Component(Collection<tanks.Models.Component> src, Func<Type,object> getInstance)
        {
            var dst = new Collection<tanks.Models.Component>();

            for(int i=0; i<src.Count; i++)
                dst.Add(CreateFromComponent(src[i], getInstance));

            return dst;
        }

        private static Collection<tanks.Models.CalculationSystem> CreateFromCollection_CalculationSystem(Collection<tanks.Models.CalculationSystem> src, Func<Type,object> getInstance)
        {
            var dst = new Collection<tanks.Models.CalculationSystem>();

            for(int i=0; i<src.Count; i++)
                dst.Add(CreateFromCalculationSystem(src[i], getInstance));

            return dst;
        }

        private static Collection<tanks.Models.ModelLink> CreateFromCollection_ModelLink(Collection<tanks.Models.ModelLink> src, Func<Type,object> getInstance)
        {
            var dst = new Collection<tanks.Models.ModelLink>();

            for(int i=0; i<src.Count; i++)
            {
                tanks.Models.ModelLink link;

                switch(src[i].GetType().Name)
                {
                case "Signal":
                    link = CreateFromSignal(src[i] as tanks.Models.Signal, getInstance);
                    break;
                case "Stream":
                    link = CreateFromStream(src[i] as tanks.Models.Stream, getInstance);
                    break;
                default:
                        throw new Exception("fail");
                }
                dst.Add(link);
            }

            return dst;
        }

        private static Collection<tanks.Models.ModelObject> CreateFromCollection_ModelObject(Collection<tanks.Models.ModelObject> src, Func<Type,object> getInstance)
        {
            var dst = new Collection<tanks.Models.ModelObject>();

            for(int i=0; i<src.Count; i++)
            {
                tanks.Models.ModelObject obj;

                switch(src[i].GetType().Name)
                {
                case "ControlValve":
                    obj = CreateFromControlValve(src[i] as tanks.Models.ControlValve, getInstance);
                    break;
                case "FlowMeter":
                    obj = CreateFromFlowMeter(src[i] as tanks.Models.FlowMeter, getInstance);
                    break;
                case "HeatExchanger":
                    obj = CreateFromHeatExchanger(src[i] as tanks.Models.HeatExchanger, getInstance);
                    break;
                case "LiquidLevelMeter":
                    obj = CreateFromLiquidLevelMeter(src[i] as tanks.Models.LiquidLevelMeter, getInstance);
                    break;
                case "LiquidTank":
                    obj = CreateFromLiquidTank(src[i] as tanks.Models.LiquidTank, getInstance);
                    break;
                case "Mixer":
                    obj = CreateFromMixer(src[i] as tanks.Models.Mixer, getInstance);
                    break;
                case "PIDController":
                    obj = CreateFromPIDController(src[i] as tanks.Models.PIDController, getInstance);
                    break;
                case "Pipe":
                    obj = CreateFromPipe(src[i] as tanks.Models.Pipe, getInstance);
                    break;
                case "PressureFeed":
                    obj = CreateFromPressureFeed(src[i] as tanks.Models.PressureFeed, getInstance);
                    break;
                case "PressureGauge":
                    obj = CreateFromPressureGauge(src[i] as tanks.Models.PressureGauge, getInstance);
                    break;
                case "PressureProduct":
                    obj = CreateFromPressureProduct(src[i] as tanks.Models.PressureProduct, getInstance);
                    break;
                case "Pump":
                    obj = CreateFromPump(src[i] as tanks.Models.Pump, getInstance);
                    break;
                case "Splitter":
                    obj = CreateFromSplitter(src[i] as tanks.Models.Splitter, getInstance);
                    break;
                case "Strainer":
                    obj = CreateFromStrainer(src[i] as tanks.Models.Strainer, getInstance);
                    break;
                case "Thermometer":
                    obj = CreateFromThermometer(src[i] as tanks.Models.Thermometer, getInstance);
                    break;
                default:
                        throw new Exception("fail");
                }
                dst.Add(obj);
            }

            return dst;
        }

		private static tanks.Models.CalculationSystem CreateFromCalculationSystem(tanks.Models.CalculationSystem src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.CalculationSystem)) as tanks.Models.CalculationSystem;
                obj.SRKKIJ = CreateFromCollection_double_array(src.SRKKIJ, getInstance);
			return obj;
		}
		private static tanks.Models.Component CreateFromComponent(tanks.Models.Component src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.Component)) as tanks.Models.Component;
                obj.Id = src.Id;
                obj.Mw = src.Mw;
                obj.Tc = src.Tc;
                obj.Pc = src.Pc;
                obj.Zc = src.Zc;
                obj.Omega = src.Omega;
                obj.Higa = src.Higa;
                obj.Higb = src.Higb;
                obj.Higc = src.Higc;
                obj.Higd = src.Higd;
                obj.Hige = src.Hige;
                obj.Higf = src.Higf;
                obj.ZRA = src.ZRA;
			return obj;
		}
		private static tanks.Models.ControlValve CreateFromControlValve(tanks.Models.ControlValve src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.ControlValve)) as tanks.Models.ControlValve;
                obj.CV = src.CV;
                obj.R = src.R;
                obj.pos0 = src.pos0;
                obj.topen = src.topen;
                obj.tclose = src.tclose;
                obj.tlag = src.tlag;
                obj.Cff = src.Cff;
                obj.mv = src.mv;
                obj.pos = src.pos;
                obj.velosity = src.velosity;
                obj.X = src.X;
                obj.Y = src.Y;
                obj.Id = src.Id;
			return obj;
		}
		private static tanks.Models.FlowDiagram CreateFromFlowDiagram(tanks.Models.FlowDiagram src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.FlowDiagram)) as tanks.Models.FlowDiagram;
                obj.Components = CreateFromCollection_Component(src.Components, getInstance);
                obj.Items = CreateFromCollection_ModelObject(src.Items, getInstance);
                obj.Links = CreateFromCollection_ModelLink(src.Links, getInstance);
                obj.Systems = CreateFromCollection_CalculationSystem(src.Systems, getInstance);
                obj.Width = src.Width;
                obj.Height = src.Height;
			return obj;
		}
		private static tanks.Models.FlowMeter CreateFromFlowMeter(tanks.Models.FlowMeter src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.FlowMeter)) as tanks.Models.FlowMeter;
                obj.Fmax = src.Fmax;
                obj.Fmin = src.Fmin;
                obj.unit = src.unit;
                obj.t_unit = src.t_unit;
                obj.F = src.F;
                obj.X = src.X;
                obj.Y = src.Y;
                obj.Id = src.Id;
			return obj;
		}
		private static tanks.Models.HeatExchanger CreateFromHeatExchanger(tanks.Models.HeatExchanger src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.HeatExchanger)) as tanks.Models.HeatExchanger;
                obj.WHdes = src.WHdes;
                obj.DHdes = src.DHdes;
                obj.PdHdes = src.PdHdes;
                obj.WLdes = src.WLdes;
                obj.DLdes = src.DLdes;
                obj.PdLdes = src.PdLdes;
                obj.A = src.A;
                obj.U = src.U;
                obj.X = src.X;
                obj.Y = src.Y;
                obj.Id = src.Id;
			return obj;
		}
		private static tanks.Models.LiquidLevelMeter CreateFromLiquidLevelMeter(tanks.Models.LiquidLevelMeter src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.LiquidLevelMeter)) as tanks.Models.LiquidLevelMeter;
                obj.Ll = src.Ll;
                obj.Lh = src.Lh;
                obj.Lbase = src.Lbase;
                obj.unit = src.unit;
                obj.L = src.L;
                obj.X = src.X;
                obj.Y = src.Y;
                obj.Id = src.Id;
			return obj;
		}
		private static tanks.Models.LiquidTank CreateFromLiquidTank(tanks.Models.LiquidTank src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.LiquidTank)) as tanks.Models.LiquidTank;
                obj.T = src.T;
                obj.A = src.A;
                obj.u = CreateFromCollection_double(src.u, getInstance);
                obj.X = src.X;
                obj.Y = src.Y;
                obj.Id = src.Id;
			return obj;
		}
		private static tanks.Models.Mixer CreateFromMixer(tanks.Models.Mixer src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.Mixer)) as tanks.Models.Mixer;
                obj.X = src.X;
                obj.Y = src.Y;
                obj.Id = src.Id;
			return obj;
		}
		private static tanks.Models.PIDController CreateFromPIDController(tanks.Models.PIDController src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.PIDController)) as tanks.Models.PIDController;
                obj.PB = src.PB;
                obj.TI = src.TI;
                obj.TD = src.TD;
                obj.PVSPAN = src.PVSPAN;
                obj.PVBASE = src.PVBASE;
                obj.INCDEC = src.INCDEC;
                obj.MVSPAN = src.MVSPAN;
                obj.MVH = src.MVH;
                obj.MVL = src.MVL;
                obj.SV = src.SV;
                obj.PV = src.PV;
                obj.MV = src.MV;
                obj.CMOD = src.CMOD;
                obj.SVM = src.SVM;
                obj.MVM = src.MVM;
                obj.e = src.e;
                obj.E = src.E;
                obj.X = src.X;
                obj.Y = src.Y;
                obj.Id = src.Id;
			return obj;
		}
		private static tanks.Models.Pipe CreateFromPipe(tanks.Models.Pipe src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.Pipe)) as tanks.Models.Pipe;
                obj.Hdiff = src.Hdiff;
                obj.X = src.X;
                obj.Y = src.Y;
                obj.Id = src.Id;
			return obj;
		}
		private static tanks.Models.PressureFeed CreateFromPressureFeed(tanks.Models.PressureFeed src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.PressureFeed)) as tanks.Models.PressureFeed;
                obj.x = CreateFromCollection_double(src.x, getInstance);
                obj.P = src.P;
                obj.T = src.T;
                obj.X = src.X;
                obj.Y = src.Y;
                obj.Id = src.Id;
			return obj;
		}
		private static tanks.Models.PressureGauge CreateFromPressureGauge(tanks.Models.PressureGauge src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.PressureGauge)) as tanks.Models.PressureGauge;
                obj.Pl = src.Pl;
                obj.Ph = src.Ph;
                obj.unit = src.unit;
                obj.P = src.P;
                obj.X = src.X;
                obj.Y = src.Y;
                obj.Id = src.Id;
			return obj;
		}
		private static tanks.Models.PressureProduct CreateFromPressureProduct(tanks.Models.PressureProduct src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.PressureProduct)) as tanks.Models.PressureProduct;
                obj.P = src.P;
                obj.X = src.X;
                obj.Y = src.Y;
                obj.Id = src.Id;
			return obj;
		}
		private static tanks.Models.Pump CreateFromPump(tanks.Models.Pump src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.Pump)) as tanks.Models.Pump;
                obj.Hd = src.Hd;
                obj.Hs = src.Hs;
                obj.Vd = src.Vd;
                obj.dd = src.dd;
                obj.Wd = src.Wd;
                obj.Ws = src.Ws;
                obj.pos0 = src.pos0;
                obj.tlag = src.tlag;
                obj.mv = src.mv;
                obj.pos = src.pos;
                obj.X = src.X;
                obj.Y = src.Y;
                obj.Id = src.Id;
			return obj;
		}
		private static tanks.Models.Signal CreateFromSignal(tanks.Models.Signal src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.Signal)) as tanks.Models.Signal;
                obj.Id = src.Id;
                obj.From = src.From;
                obj.To = src.To;
                obj.Figures = src.Figures;
			return obj;
		}
		private static tanks.Models.Splitter CreateFromSplitter(tanks.Models.Splitter src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.Splitter)) as tanks.Models.Splitter;
                obj.X = src.X;
                obj.Y = src.Y;
                obj.Id = src.Id;
			return obj;
		}
		private static tanks.Models.Strainer CreateFromStrainer(tanks.Models.Strainer src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.Strainer)) as tanks.Models.Strainer;
                obj.Wd = src.Wd;
                obj.DP = src.DP;
                obj.dd = src.dd;
                obj.X = src.X;
                obj.Y = src.Y;
                obj.Id = src.Id;
			return obj;
		}
		private static tanks.Models.Stream CreateFromStream(tanks.Models.Stream src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.Stream)) as tanks.Models.Stream;
                obj.Type = src.Type;
                obj.x = CreateFromCollection_double(src.x, getInstance);
                obj.F = src.F;
                obj.P = src.P;
                obj.T = src.T;
                obj.RL = src.RL;
                obj.HL = src.HL;
                obj.XL = CreateFromCollection_double(src.XL, getInstance);
                obj.RV = src.RV;
                obj.HV = src.HV;
                obj.XV = CreateFromCollection_double(src.XV, getInstance);
                obj.Mw = src.Mw;
                obj.V = src.V;
                obj.Id = src.Id;
                obj.From = src.From;
                obj.To = src.To;
                obj.Figures = src.Figures;
			return obj;
		}
		private static tanks.Models.Thermometer CreateFromThermometer(tanks.Models.Thermometer src, Func<Type,object> getInstance)
		{
			var obj = getInstance(typeof(tanks.Models.Thermometer)) as tanks.Models.Thermometer;
                obj.Tl = src.Tl;
                obj.Th = src.Th;
                obj.unit = src.unit;
                obj.T = src.T;
                obj.X = src.X;
                obj.Y = src.Y;
                obj.Id = src.Id;
			return obj;
		}
	}
}