using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using tanks.Models;

namespace tanks.ViewModels
{
    public class FractionInfo : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public double x { get; set; }
        public double XL { get; set; }
        public double XV { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void UpdateDisplay()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("x"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("XL"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("XV"));
        }
    }

    public class StreamInfo : INotifyPropertyChanged
    {
        public string From { get; set; }
        public string To { get; set; }

        public double H { get; set; }
        public double ro { get; set; }

        public double P { get; set; }
        public double F { get; set; }
        public double T { get; set; }

        public double RV { get; set; }
        public double HV { get; set; }
        public double HL { get; set; }
        public double Mw { get; set; }
        public double V { get; set; }
        public ObservableCollection<FractionInfo> Fractions { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void UpdateDisplay()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("F"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("P"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("T"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ro"));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RV"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HV"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HL"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Mw"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("V"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Fractions"));
            foreach (var fr in Fractions) fr.UpdateDisplay();

        }
    }

    public class RuntimeData : INotifyPropertyChanged
    {
        public ObservableCollection<StreamInfo> Items {get;set;} = new ObservableCollection<StreamInfo>();
        public StreamInfo SelectedItem {get;set;}

    public event PropertyChangedEventHandler PropertyChanged;
        public void buildData(FlowDiagram fd)
        {
            for (int i = 0; i < fd.Links.Count; i++)
            {
                var stream = fd.Links[i] as Models.Stream;

                if (stream == null) continue;

                var si = new StreamInfo
                {
                    From = stream.From,
                    To = stream.To,
                    P = stream.P,
                    F = stream.F,
                    T = stream.T,
                    H = stream.HL * (1.0 - stream.RV) + stream.HV * stream.RV,
                    ro = stream.Mw / stream.V,

                RV = stream.RV,
                HV = stream.HV,
                    HL = stream.HL,
                    Mw = stream.Mw,
                    V = stream.V,

                    Fractions= new ObservableCollection<FractionInfo>()
                };
                for (int j = 0; j < fd.Components.Count; j++)
                {
                    si.Fractions.Add(new FractionInfo
                    {
                        Name = fd.Components[j].Id,
                        x = stream.x[j],
                        XL = stream.XL[j],
                        XV = stream.XV[j],
                    });
                }

                Items.Add(si);
            }
        }
        public void UpdateData(FlowDiagram fd)
        {
            int j = 0;
            for (int i = 0; i < fd.Links.Count; i++)
            {
                var stream = fd.Links[i] as Models.Stream;

                if (stream == null) continue;

                Items[j].P = stream.P;
                Items[j].F = stream.F;
                Items[j].T = stream.T;
                Items[j].H = stream.HL * (1.0 - stream.RV) + stream.HV * stream.RV; 
                Items[j].ro = stream.Mw / stream.V;

                Items[j].RV = stream.RV;
                Items[j].HV = stream.HV;
                Items[j].HL = stream.HL;
                Items[j].Mw = stream.Mw;
                Items[j].V = stream.V;

                for (int k = 0; k < fd.Components.Count; k++)
                {
                    Items[j].Fractions[k].x = stream.x[k];
                    Items[j].Fractions[k].XL = stream.XL[k];
                    Items[j].Fractions[k].XV = stream.XV[k];
                }

                Items[j].UpdateDisplay();
                j++;
            }
        }
    }
}
