using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
using COL.MassLib;
using CSNamedPipe;
namespace TestApp
{
    public partial class frmNamePipeReader : Form
    {
        string FileName = @"d:\Fetuin_400ng_ETD_100RA_062212.raw";
        List<Task> allTsk = new List<Task>();
        public frmNamePipeReader()
        {
            InitializeComponent();
        }
        COL.MassLib.RawReader raw;
        private void button1_Click(object sender, EventArgs e)
        {
            //if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                
                
               raw = new COL.MassLib.RawReader(@"d:\Fetuin_400ng_ETD_100RA_062212.raw", "raw");
 
               //for (int i = 100; i <= 110; i++)
               //{

               //        raw.ReadScan(i);
               //        Console.WriteLine(i.ToString());


               //}
                Thread ReadThread = new Thread(Read);
               //backgroundWorker1.RunWorkerAsync();
                //COL.MassLib.GetHCDInfo gHCD = new COL.MassLib.GetHCDInfo(@"d:\Fetuin_400ng_ETD_100RA_062212.raw", "raw",312);
                //COL.MassLib.HCDInfo HCD = gHCD.HCDInfo();
                //textBox1.Text = sc.ScanHeader;
                ReadThread.Name = "ReadingThread@Form";
                ReadThread.Start();


            }
        }
        private void Read()
        {
            for (int i = 311; i <= 315; i++)
            {
               Task tsk1= Task.Factory.StartNew(() =>
                {
                    try
                    {
                        raw.ReadScan(i);
                        raw.GetScanDescription(i);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    textBox1.SetPropertyThreadSafe(() => textBox1.Text, i.ToString());
                });
               Task.WaitAll(tsk1);

            }

            Console.WriteLine("Done Reading");
        }

        private void btnServerNamedPipe_Click(object sender, EventArgs e)
        {

            COL.MassLib.RawReader raw = new COL.MassLib.RawReader(@"D:\Fetuin_400ng_ETD_100RA_062212.raw", "raw");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process GlypIDReader = new Process();
            GlypIDReader.StartInfo.FileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\GlypIDWrapper.exe";
            GlypIDReader.StartInfo.Arguments = "\"" + FileName + "\" " + "Raw" + " R " + "100" + "  " +
                                               "3.0" + " " +
                                                "5.0" + " " +
                                                "5.0" + " " +
                                                "10";

            GlypIDReader.Start();
            GlypIDReader.WaitForExit();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i < 500; i++)
            {
                Console.WriteLine(raw.ReadScan(i).ScanHeader);
                
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Process GlypIDReader = new Process();
            GlypIDReader.StartInfo.FileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\GlypIDWrapper.exe";
            GlypIDReader.StartInfo.Arguments = "\"" + FileName + "\" " + "Raw" + " R " + "100" + "  " +
                                               "3.0" + " " +
                                                "5.0" + " " +
                                                "5.0" + " " +
                                                "10";

            GlypIDReader.Start();

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void btnConsoleInput_Click(object sender, EventArgs e)
        {
            RawReader raw = new RawReader(@"E:\Dropbox\@Paper\MyPaper\GlycanSeq_AC\Data\H1N1\P_Trypsin_IU_090710_HCD.RAW", "r");
            DateTime Start = DateTime.Now;  
            for (int i = 2007; i <= 2010; i++)
            {

                raw.ReadScan(i);
                Console.WriteLine(i.ToString());
            }
           
            double End = DateTime.Now.Subtract(Start).TotalSeconds;
            
            Console.WriteLine(End);
        }

        byte[] test = new byte[3];
        private void button2_Click_1(object sender, EventArgs e)
        {


            Thread s = new Thread(StartServer);
            s.Start();
    
        }
        private void StartServer()
        {
            //NativeNamedPipeServer.Run(@"\\.\pipe\MSScan", out test);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte[] SendData = new byte[3] { 1, 2, 3 };
            string responseMsg = "";
            NativeNamedPipeClient.Run(@"\\.\pipe\MSScan", SendData, out responseMsg);
            Console.WriteLine(responseMsg);
        }
    }
    static class ExtHelper
    {
        private delegate void SetPropertyThreadSafeDelegate<TResult>(Control @this, Expression<Func<TResult>> property, TResult value);

        public static void SetPropertyThreadSafe<TResult>(this Control @this, Expression<Func<TResult>> property, TResult value)
        {
            var propertyInfo = (property.Body as MemberExpression).Member as PropertyInfo;

            if (propertyInfo == null ||
                !@this.GetType().IsSubclassOf(propertyInfo.ReflectedType) ||
                @this.GetType().GetProperty(propertyInfo.Name, propertyInfo.PropertyType) == null)
            {
                throw new ArgumentException("The lambda expression 'property' must reference a valid property on this Control.");
            }

            if (@this.InvokeRequired)
            {
                @this.Invoke(new SetPropertyThreadSafeDelegate<TResult>(SetPropertyThreadSafe), new object[] { @this, property, value });
            }
            else
            {
                @this.GetType().InvokeMember(propertyInfo.Name, BindingFlags.SetProperty, null, @this, new object[] { value });
            }
        }
    }
}
