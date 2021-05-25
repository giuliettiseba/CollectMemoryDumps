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

        private string selectedProcess;

        String[] milestoneProcesses = {
                                    "VideoOS.Administration",
                                    "VideoOS.DataCollector.Service",
                                    "VideoOS.Event.Server",
                                    "VideoOS.LogServer",
                                    "VideoOS.MobileServer.Service",
                                    "VideoOS.Recorder.Service",
                                    "VideoOS.Server.Service",

                                    "VideoOS.OnvifGateway.OnvifServer",
                                    "VideoOS.OnvifGateway.RtspServer",

                                    "VideoOS.Event.Server.TrayController",
                                    "VideoOS.MobileServer.TrayController",
                                    "VideoOS.Recorder.Service.TrayController",
                                    "VideoOS.Server.Service.TrayController",
                                    "VideoOS.OnvifGateway.TrayManager",

                                     };

        public main()
        {

            InitializeComponent();
            ShowMilestoneProcesses();
        }

        private void ShowMilestoneProcesses()
        {
            foreach (String item in milestoneProcesses)
            {
                listBoxMilestoneProcesses.Items.Add(item);
            }
            listBoxMilestoneProcesses.SelectedIndex = 0;
        }


        private void ListBox_MilestoneProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedProcess = (String)listBoxMilestoneProcesses.SelectedItem;
        }

        private void Button_Collect_Clicked(object sender, EventArgs e)
        {

            RadioButton r = groupBox_Options.Controls.OfType<RadioButton>()
                           .FirstOrDefault(n => n.Checked);

            String _selection = r.Text;

            string _selectionMade;


            string call_process = "";
            int out_no;
            if (textBox_PID.Text != "")
                if (int.TryParse(textBox_PID.Text, out out_no))
                    call_process = out_no.ToString();
                else MessageBox.Show("PID must be a integer number");

            else call_process = " -w " + selectedProcess + ".exe";

            if (call_process != "")
            {

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
                        RunTimer(call_process, 10);
                        _selectionMade = "runTimer";
                        break;

                    case "After 30 minutes":
                        RunTimer(call_process, 30);
                        _selectionMade = "runTimer";
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

                    case "Custom Arguments":
                        _selectionMade = textBox_CustomArguments.Text;
                        break;
                  
                    default:
                        throw new Exception("switch _selection fail: _selection: " + _selection);
                        //break;

                }

                if (_selectionMade != "runTimer")
                {
                    String[] arguments = { _selectionMade, call_process };
                    CallProcdum(arguments);
                }
            }
        }


        const string procdump_fileName = "procdump.exe"; // -ma";

        private void CallProcdum(string[] _arguments)
        {
            try
            {

                var myProcess = new System.Diagnostics.Process();
                myProcess.StartInfo.FileName = procdump_fileName;
                myProcess.StartInfo.Arguments = "-accepteula " + _arguments[0] + ' ' + _arguments[1];

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
                    // continuos watch of the procdump procress. 
                }

            } while (!myProcess.WaitForExit(1000));

            // is PID or name
            int _pid = 0;
            Process myStartedProcess;
            if (int.TryParse(StripTargetProcess(myProcess.StartInfo.Arguments), out _pid))
                myStartedProcess = Process.GetProcessById(_pid);
            else
            {
                Process[] localByName = Process.GetProcessesByName(StripTargetProcess(myProcess.StartInfo.Arguments));              /// get the target processs. 
                myStartedProcess = localByName.FirstOrDefault();
            }
            if (myStartedProcess != null)

                ProcessThreadReport(myStartedProcess);

            DeletePanelSafe(_panel);
        }

        private void RunTimer(string _selectedProcess, int v)
        {
            Task t = new Task(() => StartTimer(_selectedProcess, v));
            t.Start();
        }

        private void StartTimer(string _selectedProcess, int v) // v = minutes 
        {

            Process myProcess = _selectedProcess.Contains(".exe") ?
                Process.GetProcessesByName(_selectedProcess.Substring(0, _selectedProcess.LastIndexOf(".exe"))).FirstOrDefault() :
                Process.GetProcessById(int.Parse(_selectedProcess));

            if (myProcess != null && !myProcess.HasExited)
            {
                // PrintOutput("Start" + DateTime.Now.ToString("yyMMdd_HHmmss"));

                Panel _timerPanel = AddTimerPanel(myProcess);
                DateTime now = DateTime.Now;
                while (((v * 60) - (DateTime.Now.Subtract(now).Seconds + DateTime.Now.Subtract(now).Minutes * 60 + DateTime.Now.Subtract(now).Hours * 60 * 60)) > 0)
                {
                    // _timerPanel.Controls["TextBox"].Text =
                    UpdateTimerSafe(_timerPanel, ((v * 60) - (DateTime.Now.Subtract(now).Seconds + DateTime.Now.Subtract(now).Minutes * 60 + DateTime.Now.Subtract(now).Hours * 60 * 60)).ToString());
                    Thread.Sleep(1000);
                }

                String[] arguments = { "", _selectedProcess + ".exe" };
                CallProcdum(arguments);
                DeletePanelSafe(_timerPanel);

                // PrintOutput("End" + DateTime.Now.ToString("yyMMdd_HHmmss"));
            }
            else PrintOutput("TIMER DID NOT START - Start the process first");
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
            if (!_myProcess.HasExited)
                label1.Text = _myProcess.ProcessName;


            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

            panel1.Controls.Add(label1);
            panel1.Controls.Add(textBox1);

            AddPanelSafe(panel1);
            return panel1;

        }


        private Panel AddTimerPanel(Process _myProcess)
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
            textBox1.Location = new System.Drawing.Point(300, 6);
            textBox1.Name = "TextBox";
            textBox1.Text = "0";
            textBox1.Size = new System.Drawing.Size(400, 20);
            textBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(14, 9);
            label1.Name = "Timer";
            label1.Size = new System.Drawing.Size(280, 13);
            label1.TabIndex = 1;
            label1.Text = "Timer: " + _myProcess.ProcessName;


            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

            panel1.Controls.Add(label1);
            panel1.Controls.Add(textBox1);

            AddPanelSafe(panel1);
            return panel1;

        }


        private void ProcessThreadReport(Process _myProcess)
        {
           

            using (StreamWriter writer = new StreamWriter(_myProcess.ProcessName + ".exe_" + DateTime.Now.ToString("yyMMdd_HHmmss_fff") + "_Theads.csv"))
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
            // example -c 90 - s 1 - w 1234


            // int startCutting = arguments.IndexOf("VideoOS.");

            int startCutting = arguments.LastIndexOf("-w") + 3;
            int endCutting = arguments.Contains(".exe") ? arguments.IndexOf(".exe") - startCutting : arguments.Length - startCutting;

            return arguments.Substring(startCutting, endCutting);
        }


        /*Auxiliary handler for the memorydump process.*/
        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            PrintOutput(e.Data);
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
            if (this.flowLayoutPanel1.InvokeRequired)
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
            if (this.flowLayoutPanel1.InvokeRequired)
            {
                SetPanelDeleteCallback d = new SetPanelDeleteCallback(DeletePanelSafe);
                this.Invoke(d, new object[] { _panel });
            }
            else
            {
                flowLayoutPanel1.Controls.Remove(_panel);
            }
        }


        delegate void UpdateTimerCallback(Panel _panel, String str);

        private void UpdateTimerSafe(Panel _panel, String str)
        {
            if (_panel.InvokeRequired)
            {
                UpdateTimerCallback d = new UpdateTimerCallback(UpdateTimerSafe);
                this.Invoke(d, new object[] { _panel, str });
            }
            else
            {
                _panel.Controls["TextBox"].Text = str;
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

        private void textBox_PID_Enter(object sender, EventArgs e)
        {
            listBoxMilestoneProcesses.ClearSelected();
        }

        private void listBoxMilestoneProcesses_Enter(object sender, EventArgs e)
        {
            textBox_PID.Text = "";
        }
    }



}

