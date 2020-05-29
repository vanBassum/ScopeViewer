using ScopeViewer.Scope.Controls;
using STDLib.Misc;
using System;
using System.Drawing;

namespace ScopeViewer.Scope
{
    public class Trace : PropertySensitive
    {
        [TraceViewAttribute(Text = "", Width = 20)]
        public Pen Pen { get { return GetPar(Pens.Red); } set { SetPar(value); } }

        [TraceViewAttribute(Width = 20, Text = "")]
        public bool Visible { get { return GetPar(true); } set { SetPar(value); } }
        [TraceViewAttribute]
        public string Name { get { return GetPar("New Trace"); } set { SetPar(value); } }
        //[TraceViewAttribute]
        public string Unit { get { return GetPar(""); } set { SetPar(value); } }
        [TraceViewAttribute(Width = 50)]
        public double Scale { get { return GetPar(1f); } set { SetPar(value); } }
        [TraceViewAttribute(Width = 50)]
        public double Offset { get { return GetPar(0f); } set { SetPar(value); } }
        [TraceViewAttribute(Width = 50)]
        public int Layer { get { return GetPar(10); } set { SetPar(value); } }
        public ThreadedBindingList<PointD> Points { get; set; } = new ThreadedBindingList<PointD>();
        public DrawStyles DrawStyle { get; set; } = DrawStyles.Points;
        public DrawOptions DrawOption { get; set; } = DrawOptions.None;


        public Func<double, string> ToHumanReadable { get; set; } = (a) => a.ToHumanReadable(3);


        public enum DrawStyles
        {
            Points,
            Lines,
            NonInterpolatedLine,
            DiscreteSingal,
            //State,
        }

        [Flags]
        public enum DrawOptions
        {
            None = 0,
            ShowCrosses = 1,
            //ExtendBegin,
            //ExtendEnd,
        }
    }

   

}
