using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using STDLib.Extentions;

namespace ScopeViewer.Scope.Controls
{
    public partial class ScopeView : UserControl
    {
        public ScopeViewSettings Settings { get; set; } = new ScopeViewSettings();
        public ScopeController DataSource { get; set; }
        
        int columns;
        int hPxPerSub;
        int thiswidth;
        int rows;
        int vPxPerSub;
        int thisheight;

        public ScopeView()
        {
            InitializeComponent();
            DrawAll();
        }

        private void Calculate()
        {
            columns = Settings.HorizontalDivisions;
            hPxPerSub = this.Width / columns;
            thiswidth = (int)(columns * hPxPerSub);
            rows = Settings.VerticalDivisions;
            vPxPerSub = this.Height / rows;
            thisheight = (int)(rows * vPxPerSub);
        }

        private void ScopeView_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                var parent = this.Parent;
                while (!(parent is Form)) parent = parent.Parent;
                var form = parent as Form;
                form.ResizeEnd += Form_ResizeEnd;
            }

            Settings.PropertyChanged += (a,b) => this.InvokeIfRequired(() => DrawBackground());
        }



        private void Form_ResizeEnd(object sender, EventArgs e)
        {
            DrawAll();
        }


        public void DrawAll()
        {
            Calculate();
            DrawBackground();
            DrawData();
            DrawForeground();
        }

        private void DrawBackground()
        {
            this.BackgroundImage = new Bitmap(this.Width, this.Height);
            using (Graphics g = Graphics.FromImage(this.BackgroundImage))
            {
                g.Clear(Settings.BackgroundColor);

                //Draw the horizontal lines
                for (int row = 1; row < rows + 1; row++)
                {
                    int y = (int)(row * vPxPerSub);
                    if (row % (Settings.VerticalDivisions / Settings.VerticalDivisions.LowestDiv()) == 0)
                        g.DrawLine(Settings.GridPen, 0, y, thiswidth, y);
                    else
                        g.DrawLine(Settings.GridSubPen, 0, y, thiswidth, y);
                }

                //Draw the vertical lines
                for (int i = 0; i < columns + 1; i++)
                {
                    int x = (int)(i * hPxPerSub);
                    if (i % (Settings.HorizontalDivisions / Settings.HorizontalDivisions.LowestDiv()) == 0)
                        g.DrawLine(Settings.GridPen, x, 0, x, thisheight);
                    else
                        g.DrawLine(Settings.GridSubPen, x, 0, x, thisheight);

                }
            }
        }

        private void DrawData()
        {
            pictureBox1.BackgroundImage = new Bitmap(this.Width, this.Height);
            using (Graphics g = Graphics.FromImage(pictureBox1.BackgroundImage))
            {
                if (DataSource == null)
                {
                    g.DrawString("No datasource bound", DefaultFont, Brushes.White, new Point(this.Width / 2 - 50, this.Height / 2));
                }
                else
                {
                    double pxPerUnits_hor = thiswidth / (Settings.TimeScale); // hPxPerSub * grid.Horizontal.SubDivs / (HorUnitsPerDivision /** grid.Horizontal.Divisions*/);


                    var sortedTraces = from trace in DataSource.Traces
                                       orderby trace.Layer descending
                                       select trace;



                    int traceNo = 0;
                    //Loop through plots
                    foreach (Trace trace in sortedTraces)  // (int traceIndex = 0; traceIndex < Scope.Traces.Count; traceIndex++)
                    {
                        Pen pen = trace.Pen;
                        Brush brush = new SolidBrush(pen.Color);

                        //Trace trace = Scope.Traces[traceIndex];
                        if (trace.Visible)
                        {
                            //Pen linePen = new Pen(trace.Colour);
                            double pxPerUnits_ver = thisheight / (Settings.VerticalDivisions * trace.Scale);// /** grid.Vertical.Divisions*/);
                                                                                                                       //Draw plot
                            int pointCnt = trace.Points.Count;
                            int inc = pointCnt / thiswidth;
                            if (inc < 1)
                                inc = 1;

                            try
                            {
                                Point p = Point.Empty;
                                Point pPrev = Point.Empty;



                                for (int i = 0; i < pointCnt; i += inc)
                                {

                                    double x = (float)(trace.Points[i].X + Settings.TimeOffset) * pxPerUnits_hor;
                                    double y = thisheight / 2 - (trace.Points[i].Y - trace.Offset) * pxPerUnits_ver * trace.Scale;

                                    p = new Point((int)x, (int)y);


                                    if (trace.DrawOption.HasFlag(Trace.DrawOptions.ShowCrosses))
                                        g.DrawCross(pen, p, 3);


                                    switch (trace.DrawStyle)
                                    {
                                        case Trace.DrawStyles.Points:
                                            g.Drawpoint(brush, p, 2);
                                            break;

                                        case Trace.DrawStyles.DiscreteSingal:
                                            g.Drawpoint(brush, p, 4);
                                            g.DrawLine(pen, new Point(p.X, thisheight / 2), p);
                                            break;

                                        case Trace.DrawStyles.Lines:
                                            if(!pPrev.IsEmpty)
                                                g.DrawLine(pen, p, pPrev);
                                            
                                            break;

                                        case Trace.DrawStyles.NonInterpolatedLine:
                                            if (!pPrev.IsEmpty)
                                            {
                                                Point between = new Point(p.X, pPrev.Y);
                                                g.DrawLine(pen, pPrev, between);
                                                g.DrawLine(pen, between, p);
                                            }
                                            break;
                                            
                                        default:
                                            g.DrawString($"Drawing of '{trace.DrawStyle}' is not supported yet.", Settings.Font, brush, new Point(0, traceNo * Settings.Font.Height));
                                            i = pointCnt;
                                            break;

                                    }

                                    pPrev = p;
                                }
                            }
                            catch(Exception ex)
                            {
                                g.DrawString(ex.Message, Settings.Font, brush, new Point(0, traceNo * Settings.Font.Height));
                            }
                        }

                        traceNo++;
                    }
                }
            }
        }

        private void DrawForeground()
        {
            if (DataSource != null)
            {

            }
        }
    }

}
