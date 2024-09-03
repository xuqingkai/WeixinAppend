using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Com.Youlaiyouqu.WeixinAppend
{
    public partial class MainForm : Form
    {
        const string WECHAT_NAME = "WeChat";

        const string WECHAT_EXE_NAME = "WeChat.exe";

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;                             //最左坐标
            public int Top;                             //最上坐标
            public int Right;                           //最右坐标
            public int Bottom;                        //最下坐标
        }

        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);

        [DllImportAttribute("user32.dll", EntryPoint = "MoveWindow")]
        public static extern bool MoveWindow(System.IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint Flags);

        public MainForm()
        {
            InitializeComponent();
        }

        private int weixinNumber = 0;

        private void FormMultiInstance_Load(object sender, EventArgs e)
        {
            weixinNumber = Math.Abs(Convert.ToInt32(ConfigurationManager.AppSettings["WeixinNumber"]));

            if (weixinNumber >=1 && Process.GetProcessesByName(WECHAT_NAME).Length >= 1)
            {
                weixinNumber = 1;
            }
            numericUpDown.Value = weixinNumber;


            buttonStartWeixin.Text = "启 动(0)";

            timerWeixinAppend.Start();
            if (weixinNumber >= 1) 
            {
                startWeixin();
            }
            
        }

        private void FormMultiInstance_Close(object sender, FormClosedEventArgs e)
        {
            timerWeixinAppend.Stop();
        }

        private void startWeixin()
        {
            clearListThread();

            string appPath = PathUtil.FindInstallPathFromRegistry(WECHAT_NAME) + Path.DirectorySeparatorChar + WECHAT_EXE_NAME;
            if (!File.Exists(appPath))
            {
                MessageBox.Show("检测到未安装微信，可能是注册表信息丢失，建议重新下载并安装一次。继续下一步可能出现异常。");
            }

            weixinNumber = Convert.ToInt32(numericUpDown.Value);
            for (int i = 0; i < weixinNumber; i++)
            {
                listThreadStartInfo.Add(new ProcessStartInfo(appPath));
            }
            checkListThreadStartInfo();
        }

        private List<Thread> listThread = new List<Thread>();

        private void clearListThread()
        {
            if (listThread.Count > 0)
            {
                listThread.ForEach(delegate (Thread thread)
                {
                    thread.Abort();
                });
                listThread.Clear();
            }

            if (listThreadStartInfo.Count > 0)
            {
                listThreadStartInfo.Clear();
            }
        }

        List<ProcessStartInfo> listThreadStartInfo = new List<ProcessStartInfo>();

        private void checkListThreadStartInfo()
        {
            if (listThreadStartInfo.Count > 0)
            {
                ProcessStartInfo thread = listThreadStartInfo[0];
                new Thread(() =>
                {
                    Process.Start(thread);
                    listThreadStartInfo.RemoveAt(0);
                }).Start();
            }
        }

        private List<WechatProcess> listWechatProcess = new List<WechatProcess>();

        private void TryWeixinAppend(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName(WECHAT_NAME);
            buttonStartWeixin.Text = "启 动(" + processes.Length + ")";
            if (processes.Length <= 0)
            {
                buttonStartWeixin.Enabled = true;
                return;
            }

            // 添加新进程
            foreach (Process p in processes)
            {
                int i = 0;
                for (i = 0; i < listWechatProcess.Count; i++)
                {
                    WechatProcess wechatProcess = listWechatProcess[i];
                    if (wechatProcess.Proc.Id == p.Id)
                    {
                        break;
                    }
                }
                if (i == listWechatProcess.Count)
                {
                    listWechatProcess.Add(new WechatProcess(p));
                }
            }

            // 关闭所有存在互斥句柄的进程
            int num = 0;
            for (int i = listWechatProcess.Count - 1; i >= 0; i--)
            {
                WechatProcess wechatProcess = listWechatProcess[i];
                if (!wechatProcess.MutexClosed)
                {
                    wechatProcess.MutexClosed = ProcessUtil.CloseMutexHandle(wechatProcess.Proc);
                    Console.WriteLine("进程：" + wechatProcess.Proc.Id + ",关闭互斥句柄：" + wechatProcess.MutexClosed);

                    if (wechatProcess.MutexClosed)
                    {
                        checkListThreadStartInfo();
                    }
                }
                else
                {
                    if (wechatProcess.Proc.HasExited)
                    {
                        // 移除不存在的线程
                        listWechatProcess.RemoveAt(i);
                    }
                    else
                    {
                        num++;
                    }
                }
            }
            if (weixinNumber <= listWechatProcess.Count) 
            {
                if (weixinNumber >= 2)
                {
                    weixinLayoutSort();
                }
                else if (weixinNumber >= 1)
                {
                    //Environment.Exit(0);
                    this.Close();
                }
            }
        }

        private bool isSortRunning = false;

        private void weixinLayoutSort()
        {
            Process[] processes1 = Process.GetProcessesByName(WECHAT_NAME);
            if (processes1.Length == 0)
            {
                return;
            }

            int realAppNum = Math.Max(weixinNumber, processes1.Length);

            if (isSortRunning) return;
            isSortRunning = true;

            if (listThread.Count > 0)
            {
                listThread.ForEach(delegate (Thread thread)
                {
                    thread.Abort();
                });
                listThread.Clear();
            }

            Thread startThread = new Thread(() =>
            {
                int maxW = 0;
                int maxH = 0;
                int i = 0;
                Hashtable hashtable = new Hashtable();
                bool flag = false;
                while (true)
                {
                    Thread.Sleep(1000 / 30);
                    Process[] processes = Process.GetProcessesByName(WECHAT_NAME);
                    if (processes.Length >= realAppNum)
                    {
                        for (i = 0; i < processes.Length; i++)
                        {
                            Process process = processes[i];
                            IntPtr awin = process.MainWindowHandle;    //获取当前窗口句柄
                            RECT rect = new RECT();
                            GetWindowRect(awin, ref rect);
                            int width = rect.Right - rect.Left;                     //窗口的宽度
                            int height = rect.Bottom - rect.Top;                   //窗口的高度

                            if (width > 0 && height > 0)
                            {
                                if (!hashtable.ContainsKey(process.Id))
                                {
                                    hashtable.Add(process.Id, process);
                                    maxW = width;
                                    maxH = height;
                                    if (hashtable.Count >= realAppNum)
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                        }

                        int screenW = SystemInformation.VirtualScreen.Width;
                        int screenH = SystemInformation.VirtualScreen.Height;
                        int itemW = maxW + 5;
                        int itemH = maxH + 5;
                        int column = screenW / itemW;
                        int left = (screenW - (hashtable.Count >= column? column : hashtable.Count) * itemW) / 2;
                        int row = screenH / itemH;
                        int top = (screenH - row * itemH) / 2;

                        i = 0;
                        foreach (DictionaryEntry entry in hashtable)
                        {
                            if (i >= column * row)
                            {
                                i = 0;
                            }
                            Process process = (Process)entry.Value;
                            int initX = (i % column) * itemW + left;
                            int initY = (i / column) * itemH + top;

                            SetWindowPos(process.MainWindowHandle, 0, initX, initY, 0, 0, 1); //简单解释下就是第一个参数为你要控制的窗口，最后一个参数为控制位置生效还是大小生效等
                            i += 1;
                        }

                        if (flag)
                        {
                            isSortRunning = false;
                            //Environment.Exit(0);
                            
                            Action<bool> AsyncUIDelegate = delegate (bool enable) {
                                this.Text = row + "/" + top;
                                //this.Close();
                            };
                            buttonStartWeixin.Invoke(AsyncUIDelegate, new object[] { true });
                            break;
                        }
                    }
                }
            });
            startThread.Start();
            listThread.Add(startThread);
        }

        private void buttonStartWeixin_Click(object sender, EventArgs e)
        {
            timerWeixinAppend.Start();
            startWeixin();
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            timerWeixinAppend.Stop();
            weixinNumber = Convert.ToInt32(numericUpDown.Value);
        }
    }
}
