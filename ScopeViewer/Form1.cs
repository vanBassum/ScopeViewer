using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FRMLib.Scope;

namespace ScopeViewer
{
    public partial class Form1 : Form
    {
        ScopeController scope = new ScopeController();
        public Form1()
        {
            InitializeComponent();
            scopeView1.DataSource = scope;
            traceView1.DataSource = scope;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            /*
            Trace t = new Trace();
            t.DrawStyle = Trace.DrawStyles.Lines;
            t.Pen = Pens.Yellow;
            scope.Traces.Add(t);
            t.Points.Add(new PointD(0, -4));
            t.Points.Add(new PointD(1, -3));
            t.Points.Add(new PointD(2, 0));
            t.Points.Add(new PointD(3, 4));
            t.Points.Add(new PointD(4, 2));
            scopeView1.DrawAll();*/

        }
        Trace t;
        private void button2_Click(object sender, EventArgs e)
        {

            t = new Trace();
            t.DrawStyle = Trace.DrawStyles.DiscreteSingal;
            //t.DrawOption = Trace.DrawOptions.ShowCrosses;
            scope.Traces.Add(t);

            for (int i = 0; i <= 100; i++)
            {
                t.Points.Add(new PointD(((double)i)/10, 10 * Math.Sin(i * 2 * Math.PI / 100)));
            }

            //scopeView1.Settings.TimeScale = 100;

            scopeView1.DrawAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            scopeView1.AutoScaleHorizontal();
            scopeView1.AutoScaleTrace(t);
            scopeView1.DrawAll();
        }
    }
}
