using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CollectMemoryDumps
{
    public partial class main : Form
    {
        bool filterMilestone;

        private List<Process> data = new List<Process>();
        private BindingSource bs = new BindingSource();
        private string selectedProcess;

        String[] milestoneProcesses = {
                                    "VideoOS.Recorder.Service.TrayController",
                                    "VideoOS.LogServer",
                                    "VideoOS.Administration",
                                    "VideoOS.Server.Service",
                                    "VideoOS.Event.Server.TrayController",
                                    "VideoOS.Server.Service.TrayController",
                                    "VideoOS.OnvifGateway.OnvifServer",
                                    "VideoOS.Recorder.Service",
                                    "VideoOS.Administration",
                                    "VideoOS.Server.Service.TrayController",
                                    "VideoOS.OnvifGateway.RtspServer",
                                    "VideoOS.OnvifGateway.TrayManager",
                                    "VideoOS.Administration",
                                    "VideoOS.MobileServer.TrayController",
                                    "VideoOS.MobileServer.TrayController",
                                    "VideoOS.DataCollector.Service",
                                    "VideoOS.Recorder.Service.TrayController",
                                    "VideoOS.MobileServer.Service",
                                    "VideoOS.Event.Server",
                                    "VideoOS.Event.Server.TrayController",
                                     };




        public main()
        {

            InitializeComponent();
            ShowMilestoneProcesses();
            //           ShowRunningProcesses();       // Dump live process 



        }

        private Panel AddPanel(Process _myProcess)
        {

            System.Windows.Forms.Panel panel1 = new System.Windows.Forms.Panel();
            System.Windows.Forms.TextBox textBox1 = new System.Windows.Forms.TextBox();
            System.Windows.Forms.Label label1 = new System.Windows.Forms.Label();
            // 
            // panel1
            // 
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel1.Location = new System.Drawing.Point(353, 44);
            panel1.Name = _myProcess.Id.ToString();
            panel1.Size = new System.Drawing.Size(480, 35);
            panel1.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(69, 6);
            textBox1.Name = "TextBox";
            textBox1.Text = _myProcess.StartInfo.Arguments;
            textBox1.Size = new System.Drawing.Size(400, 20);
            textBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(14, 9);
            label1.Name = "Process";
            label1.Size = new System.Drawing.Size(35, 13);
            label1.TabIndex = 1;
            label1.Text = _myProcess.ProcessName;


            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

            panel1.Controls.Add(label1);
            panel1.Controls.Add(textBox1);

            AddPanelSafe(panel1);
            return panel1;

        }

        private void ShowMilestoneProcesses()
        {
            foreach (String item in milestoneProcesses)
            {
                listBoxMilestoneProcesses.Items.Add(item);
            }
            listBoxMilestoneProcesses.SelectedIndex = 0;
        }



        private void listBox_processes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Select a live process 

            // selectedProcess = (Process)listBox_MilestoneProcesses.SelectedItem;
            //textBox1.Text = selectedProcess.ProcessName;
            //            selectedProcess.

        }



        private void listBox_MilestoneProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedProcess = (String)listBoxMilestoneProcesses.SelectedItem;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox_Output.Text += "Starting..." + Environment.NewLine;

            String _selection = "";
            foreach (RadioButton r in groupBox1.Controls)
            {
                if (r.Checked)
                    _selection = r.Text;
            }

            string _selectionMade;

            switch (_selection)
            {
                case "Instant":
                    _selectionMade = "";
                    break;

                case "Exception":
                    _selectionMade = "-e 1";
                    break;

                case "Memory 500 Mb":
                    _selectionMade = "-m 500 ";
                    break;

                case "Memory 1000 Mb":
                    _selectionMade = "-m 1000 ";
                    break;

                case "After 10 minutes":
                    _selectionMade = RunTimer(10); ;
                    break;

                case "After 30 minutes":
                    _selectionMade = RunTimer(10); ;
                    break;

                case "CPU 50%":
                    _selectionMade = "-c 50 -s 1";
                    break;

                case "CPU 75%":
                    _selectionMade = "-c 70 -s 1";
                    break;

                case "CPU 90%":
                    _selectionMade = "-c 90 -s 1";
                    break;

                default:
                    throw new Exception("switch _selection fail: _selection: " + _selection);
                    //break;
            }


            String[] arguments = { _selectionMade, " -w " + selectedProcess + ".exe" };


            CallProcdum(arguments);


        }


        const string procdump_fileName = "procdump.exe"; // -ma";

        private void CallProcdum(string[] _arguments)
        {
            try
            {

                var myProcess = new System.Diagnostics.Process();
                myProcess.StartInfo.FileName = procdump_fileName;
                myProcess.StartInfo.Arguments = _arguments[0] + ' ' + _arguments[1];

                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.RedirectStandardOutput = true;
                myProcess.OutputDataReceived += p_OutputDataReceived;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();

                Task t = new Task(() => MonitorProcess(myProcess));
                t.Start();

                myProcess.BeginOutputReadLine();

            }
            catch (Win32Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        List<Process> runningProcess = new List<Process>();
        private void MonitorProcess(Process myProcess)
        {
            runningProcess.Add(myProcess);
            Panel _panel = AddPanel(myProcess);
            do
            {
                if (!myProcess.HasExited)
                {
                    // continuos whatch of the procdump procress. 
                }

            } while (!myProcess.WaitForExit(1000));




            Process[] localByName = Process.GetProcessesByName(StripTargetProcess(myProcess.StartInfo.Arguments));              /// get the target processs. 

            ProcessThreadReport(localByName.FirstOrDefault());


            DeletePanelSafe(_panel);

        }

        private void ProcessThreadReport(Process _myProcess)
        {
            using (StreamWriter writer = new StreamWriter(_myProcess.ProcessName + ".exe_" + DateTime.Now.ToString("yyMMdd_HHmmss") + "_Theads.csv"))
            {
                writer.WriteLine(_myProcess.StartInfo.FileName);
                List<Dictionary<string, string>> processTreadInfoList = new List<Dictionary<string, string>>();
                writer.WriteLine("Id,Total processor time, Responding, Name,Physical memory usage,Base priority, User processor time, Privileged processor time,  Paged system memory size, Paged memory size");
                String[] _processInfo = {
                    _myProcess.Id.ToString(),
                    _myProcess.TotalProcessorTime.ToString(),
                    _myProcess.Responding.ToString(),
                    _myProcess.ProcessName,
                    _myProcess.WorkingSet64.ToString(),
                    _myProcess.BasePriority.ToString(),
                    _myProcess.PriorityClass.ToString(),
                    _myProcess.UserProcessorTime.ToString(),
                    _myProcess.PrivilegedProcessorTime.ToString(),
                    _myProcess.PagedSystemMemorySize64.ToString(),
                    _myProcess.PagedMemorySize64.ToString(),
                    };
                writer.WriteLine(String.Join(",", _processInfo));
                /* 
                writer.WriteLine($"----------------------------------------------------------");
                writer.WriteLine($"  Name                      : {_myProcess.ProcessName}");
                writer.WriteLine($"  Physical memory usage     : {_myProcess.WorkingSet64}");
                writer.WriteLine($"  Base priority             : {_myProcess.BasePriority}");
                writer.WriteLine($"  Priority class            : {_myProcess.PriorityClass}");
                writer.WriteLine($"  User processor time       : {_myProcess.UserProcessorTime}");
                writer.WriteLine($"  Privileged processor time : {_myProcess.PrivilegedProcessorTime}");
                writer.WriteLine($"  Total processor time      : {_myProcess.TotalProcessorTime}");
                writer.WriteLine($"  Paged system memory size  : {_myProcess.PagedSystemMemorySize64}");
                writer.WriteLine($"  Paged memory size         : {_myProcess.PagedMemorySize64}");
                writer.WriteLine($"----------------------------------------------------------");
                */

                List<String[]> _threadInfoList = new List<string[]>();
                
                writer.WriteLine("ID, TotalProcessorTime(sec),ThreadState, WaitReason,StartAddress ");
                foreach (ProcessThread _processThread in _myProcess.Threads)
                {

                    String[] output = new string[5];
                    output[0] = _processThread.Id.ToString();
                    output[1] = _processThread.TotalProcessorTime.ToString();
                    output[2] = _processThread.ThreadState.ToString();
                    if (_processThread.ThreadState == System.Diagnostics.ThreadState.Wait)
                        output[3] = _processThread.WaitReason.ToString();
                    else output[3] = "";
                    output[4] = _processThread.StartAddress.ToString();

                    /*writer.WriteLine($"----------------------------------------------------------");
                    writer.WriteLine($"  ID                       : {_processThread.Id.ToString()}");
                    writer.WriteLine($"  TotalProcessorTime(Sec)  : {_processThread.TotalProcessorTime.TotalSeconds}");
                    writer.WriteLine($"  UserProcessorTime(Sec)   : {_processThread.UserProcessorTime.TotalSeconds}");
                    writer.WriteLine($"  PrivilegedProcTime(Sec)  : {_processThread.PrivilegedProcessorTime.TotalSeconds}");
                    writer.WriteLine($"  State                    : { _processThread.ThreadState}");
                    if (_processThread.ThreadState == System.Diagnostics.ThreadState.Wait)
                        writer.WriteLine($"  WaitReason               : { _processThread.WaitReason}");
                    writer.WriteLine($"  Start Address            : {_processThread.StartAddress}");
                    */
                    _threadInfoList.Add(output);
                }
                _threadInfoList = _threadInfoList.OrderByDescending(arr => arr[1]).ToList();

                foreach (String[] _threadInfo in _threadInfoList)
                {
                    writer.WriteLine(String.Join(",", _threadInfo));
                }
        
            }
        }






        private String StripTargetProcess(string arguments)
        {
            // example -c 90 - s 1 - w VideoOS.Recorder.Service.TrayController.exe

            int startCutting = arguments.IndexOf("VideoOS.");
            int endCutting = arguments.IndexOf(".exe") - startCutting;
            return arguments.Substring(startCutting, endCutting);
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            PrintOutput(e.Data);
        }


        private string RunTimer(int v)
        {

            // is a live process?
            // get process
            // getTimer 
            // start thread 

            throw new NotImplementedException();
        }




        // Safe calls 

        delegate void SetTextCallback(string text);

        private void PrintOutput(string text)
        {
            if (this.textBox_Output.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(PrintOutput);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox_Output.Text += text + Environment.NewLine;

                this.textBox_Output.SelectionStart = this.textBox_Output.TextLength;
                this.textBox_Output.ScrollToCaret();
            }
        }



        delegate void SetPanelCallback(Panel _panel);

        private void AddPanelSafe(Panel _panel)
        {
            if (this.textBox_Output.InvokeRequired)
            {
                SetPanelCallback d = new SetPanelCallback(AddPanelSafe);
                this.Invoke(d, new object[] { _panel });
            }
            else
            {
                flowLayoutPanel1.Controls.Add(_panel);
            }
        }


        delegate void SetPanelDeleteCallback(Panel _panel);

        private void DeletePanelSafe(Panel _panel)
        {
            if (this.textBox_Output.InvokeRequired)
            {
                SetPanelDeleteCallback d = new SetPanelDeleteCallback(DeletePanelSafe);
                this.Invoke(d, new object[] { _panel });
            }
            else
            {
                flowLayoutPanel1.Controls.Remove(_panel);
            }
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Process proc in runningProcess)
            {
                if (!proc.HasExited)
                    proc.Kill();

            }
        }


    }



}

