using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FRMLib;
using FRMLib.Scope;
using FRMLib.Scope.Controls;

namespace ScopeViewer
{
    public partial class Form1 : Form
    {
        CmdHandler cmdHandler = new CmdHandler();
        ScopeController scope = new ScopeController();
        Trace trace_xk;
        Trace trace_yk;

        public Form1()
        {
            InitializeComponent();
            scopeView1.DataSource = scope;
            traceView1.DataSource = scope;
            markerView1.DataSource = scope;
            mathView1.DataSource = scope;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            scope.Traces.Add(trace_xk = new Trace { Name = "xk", Pen = Pens.Red, Scale = 10, Offset = -20 });
            scope.Traces.Add(trace_yk = new Trace { Name = "yk", Pen = Pens.Yellow, Scale = 64, Offset = -255 });
            scope.HorizontalToHumanReadable = (a) => (a.ToString());
            scope.MathItems.Add(new MathItem());
            cmdHandler.EventRecieved += CmdHandler_EventRecieved;
            //cmdHandler.DataRecieved += (a, b) => richTextBox1.InvokeIfRequired(() => richTextBox1.AppendText(b));

            textBox1.Text = cmdHandler.SendCommand("DPIDTS");
            textBox2.Text = cmdHandler.SendCommand("DPIDP");
            textBox3.Text = cmdHandler.SendCommand("DPIDI");
            textBox4.Text = cmdHandler.SendCommand("DPIDD");
        }


        private void CmdHandler_EventRecieved(object sender, string e)
        {
            if (e.StartsWith("EPID")) HandlePIDEvent(e);
        }

        void HandlePIDEvent(string line)
        {
            int index = 0;
            double xk = 0;
            double yk = 0;
            Match m;
            bool suc = true;

            if (suc &= (m = Regex.Match(line, @"s: (\d+)")).Success)
                suc &= int.TryParse(m.Groups[1].Value, out index);

            if (suc |= (m = Regex.Match(line, @"xk: ([\d\.]+)")).Success)
                suc |= double.TryParse(m.Groups[1].Value, out xk);

            if (suc |= (m = Regex.Match(line, @"yk: ([\d\.]+)")).Success)
                suc |= double.TryParse(m.Groups[1].Value, out yk);

            if (suc)
            {
                trace_xk.Points.Add(index, xk);
                trace_yk.Points.Add(index, yk);
            }

            scopeView1.InvokeIfRequired(() => { scopeView1.FitHorizontalInXDivs(10); });
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmdHandler.SendCommand("CPIDTS" + textBox1.Text);
            textBox1.Text = cmdHandler.SendCommand("DPIDTS");
            cmdHandler.SendCommand("CSAV");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmdHandler.SendCommand("CPIDP" + textBox2.Text);
            textBox2.Text = cmdHandler.SendCommand("DPIDP");
            cmdHandler.SendCommand("CSAV");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cmdHandler.SendCommand("CPIDI" + textBox3.Text);
            textBox3.Text = cmdHandler.SendCommand("DPIDI");
            cmdHandler.SendCommand("CSAV");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cmdHandler.SendCommand("CPIDD" + textBox4.Text);
            textBox4.Text = cmdHandler.SendCommand("DPIDD");
            cmdHandler.SendCommand("CSAV");
        }

        
            
            
            
    }






    public class Phy
    {
        public event EventHandler<string> DataRecieved;

        SerialPort serial = new SerialPort();

        public Phy()
        {
            serial.DataReceived += Serial_DataReceived;
            serial.PortName = "COM16";
            serial.BaudRate = 9600;
            serial.Open();
        }

        string buf = "";
        private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            buf += serial.ReadExisting();

            int i = buf.IndexOf('\n');
            if (i != -1)
            {
                DataRecieved?.Invoke(this, buf.Substring(0, i + 1).Trim('\r', '\n'));
                buf = buf.Substring(i + 1);
            }
        }

        public void SendData(string data)
        {
            serial.WriteLine(data.Trim('\r', '\n'));
        }

    }



    public class CmdHandler
    {
        Phy phy = new Phy();
        public event EventHandler<string> EventRecieved;
        private SemaphoreSlim signal = new SemaphoreSlim(0, 1);
        string result = "";
        public event EventHandler<string> DataRecieved;
        public CmdHandler()
        {
            phy.DataRecieved += Phy_DataRecieved;
        }

        private void Phy_DataRecieved(object sender, string e)
        {
            DataRecieved?.Invoke(this, e);
            if (e.StartsWith("E"))
                EventRecieved?.Invoke(this, e);
            else
            {
                result = e;
                signal.Release();
            }
        }

        public string SendCommand(string cmd)
        {
            phy.SendData(cmd);
            signal.Wait();
            return result;
        }
    }
}
