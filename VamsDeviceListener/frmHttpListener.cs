using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Owin.Hosting;
using AccessControlSDK;
using AccessControlSDK.EVENTS;
using AccessControlSDK.EVENTS.ZKBIO;
using AccessControlSDK.EVENTS.COMMON;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Web;

namespace VamsDeviceListener
{
    public partial class frmHttpListener : Form
    {

        #region Declaration

        private System.Timers.Timer tmrRestartApp;
        private System.Timers.Timer tmrStatusBar;
        private bool ignoreCase = true;
        Icon icon1 = Properties.Resources.favicon_light;
        Icon icon2 = Properties.Resources.favicon_grey;
        Int16 chkICO = 0;
                
        #region Delegate

        private delegate void delShowMsg(string Msg);
        private delegate void delUpdateStatusBar();

        #endregion //Delegate
                
        #endregion //Declaration

        #region Constructor

        public frmHttpListener()
        {
            InitializeComponent();
        }

        #endregion //Constructor

        #region Moving Form

        #region Declaration

        [DllImport("shell32.dll")]
        private static extern int SHAppBarMessage(int dwMessage, ref APPBARDATA pData);

        private const int ABM_GETTASKBARPOS = 0x5;
        private const int WM_SYSCOMMAND = 0x112;
        private const int SC_MINIMIZE = 0xF020;

        #endregion //Declaration

        #region ENUM

        enum ABEdge
        {
            ABE_LEFT = 0,
            ABE_TOP,
            ABE_RIGHT,
            ABE_BOTTOM
        }

        enum ABNotify
        {
            ABN_STATECHANGE = 0,
            ABN_POSCHANGED,
            ABN_FULLSCREENAPP,
            ABN_WINDOWARRANGE
        }

        enum ABMsg
        {
            ABM_NEW = 0,
            ABM_REMOVE = 1,
            ABM_QUERYPOS = 2,
            ABM_SETPOS = 3,
            ABM_GETSTATE = 4,
            ABM_GETTASKBARPOS = 5,
            ABM_ACTIVATE = 6,
            ABM_GETAUTOHIDEBAR = 7,
            ABM_SETAUTOHIDEBAR = 8,
            ABM_WINDOWPOSCHANGED = 9,
            ABM_SETSTATE = 10
        }


        #endregion //ENUM

        #region STRUCTURE

        struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public int uCallbackMessage;
            public ABEdge uEdge;
            public RECT rc;
            public IntPtr lParam;
        }

        #endregion //STRUCTURE

        #region Overides

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_SYSCOMMAND && m.WParam.ToInt32() == SC_MINIMIZE)
            {
                AnimateWindow(true);
                return;
            }

            base.WndProc(ref m);
        }

        #endregion //Overides

        #endregion //Moving Form

        #region Manage UI

        /// <summary>
        /// Displays the message to the control
        /// </summary>
        /// <param name="Msg"></param>
        public void DisplayMsg(string Msg)
        {
            ShowStatus(txtStatus, Msg);
            Application.DoEvents();
        }

        /// <summary>
        /// Animates the Notify Icon to check for active
        /// </summary>
        private void AnimateNotifyIcon()
        {

            if (chkICO == 1)
            {
                this.Icon = icon2;
                notifyIcon1.Icon = icon2;
                chkICO = 0;
            }
            else
            {
                this.Icon = icon1;
                notifyIcon1.Icon = this.Icon;
                chkICO = 1;
            }

            Application.DoEvents();
        }

        /// <summary>
        /// Updates the Status Bar
        /// </summary>
        private void UpdateStausBar()
        {
            delUpdateStatusBar dlg = new delUpdateStatusBar(AnimateNotifyIcon);
            this.Invoke(dlg);
        }

        /// <summary>
        /// Shows the Status in the textbox
        /// </summary>
        /// <param name="T"></param>
        /// <param name="Msg"></param>
        private void ShowStatus(TextBox T, string Msg)
        {
            if (T.Text.Length > 32000)
                T.Text = "";
            T.Text += System.Convert.ToString(DateTime.Now) + "-->" + Msg;
            T.Text += Environment.NewLine;
        }

        #endregion //Manage UI

        #region Form Events

        private void frmHttpListener_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.WindowsShutDown)
            {
                if (ignoreCase)
                {                    
                    AnimateWindow(true);
                    e.Cancel = true;
                }
                else
                {
                    if (Properties.Settings.Default.WriteInfoLog)
                    {
                        Logs.WriteLog("Engine Stopped at " + DateTime.Now.ToString("dd MMM yyyy hh:mm:ss tt"));
                    }
                                        
                }
            }
        }

        private void frmHttpListener_Load(object sender, EventArgs e)
        {

            if (Properties.Settings.Default.WriteInfoLog)
            {
                Logs.WriteLog("Engine Started at " + DateTime.Now.ToString("dd MMM yyyy hh:mm:ss tt"));
            }

            lblStartDt.Text = "Running since " + DateTime.Now.ToString();
            this.lnkBtnDetails.Visible = true;
            this.txtStatus.Visible = false;
            base.Height = 100;
            lblStartDt.Location = new Point(6, 58);

            this.Location = new Point(0, 0);
            this.StartPosition = FormStartPosition.Manual;

            this.Show();

            this.lnkBtnDetails_LinkClicked(this, null);

            //Initializes the ZkAPI
            Utilities.accCtrl = new AccessController();
            Utilities.accCtrl.sdkApiWriteLog = Properties.Settings.Default.WriteInfoLog.ToString();
            Utilities.accCtrl.apiTmInterval = Properties.Settings.Default.TmIntervalInMin;
            Utilities.accCtrl.evCommandUpload += zkApiCs_evCommandUpload;
            Utilities.accCtrl.evDataUpload += zkApiCs_evDataUpload;
            Utilities.accCtrl.evDisplayMessage += zkApiCs_evDisplayMessage;
            Utilities.accCtrl.evRealTime += zkApiCs_evRealTime;
            Utilities.accCtrl.evEmpLog += zkApiCs_evEmpLog;
            
            Initialize();

            stbStatus.Text = "Ready";

            UpdateStausBar();
            
            // start self host service
            WebApp.Start<VamsDeviceListener.Startup>(Properties.Settings.Default.BaseAddress);

        }
        
        #endregion //Form Events

        #region Control Events

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            AnimateWindow(false);
        }

        private void lnkBtnDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.txtStatus.Visible = !this.txtStatus.Visible;

            if (this.txtStatus.Visible)
            {
                this.lnkBtnDetails.Text = "Hide Details";
                base.Height = 340;
                lblStartDt.Location = new Point(6, 44);
            }
            else
            {
                this.lnkBtnDetails.Text = "Show Details";
                base.Height = 125;
                lblStartDt.Location = new Point(6, 58);
            }
        }

        #endregion //Control Events

        #region Methods & Functions

        /// <summary>
        /// Updates the form to system tray
        /// </summary>
        /// <param name="ToTray"></param>
        private void AnimateWindow(bool ToTray)
        {
            // get the screen dimensions
            Rectangle screenRect = Screen.GetBounds(this.Location);

            // figure out where the taskbar is (and consequently the tray)
            Point destPoint = new Point();
            APPBARDATA BarData = new APPBARDATA();
            BarData.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(BarData);
            SHAppBarMessage(Convert.ToInt32(ABMsg.ABM_GETTASKBARPOS), ref BarData);
            switch (BarData.uEdge)
            {
                case ABEdge.ABE_BOTTOM:
                case ABEdge.ABE_RIGHT:
                    {
                        // Tray is to the Bottom Right
                        destPoint = new Point(screenRect.Width, screenRect.Height);
                        break;
                    }
                case ABEdge.ABE_LEFT:
                    {
                        // Tray is to the Bottom Left
                        destPoint = new Point(0, screenRect.Height);
                        break;
                    }

                case ABEdge.ABE_TOP:
                    {
                        // Tray is to the Top Right
                        destPoint = new Point(screenRect.Width, 0);
                        break;
                    }
            }

            // setup our loop based on the direction
            float a, b, s;
            if (ToTray)
            {
                a = 0;
                b = 1;
                s = 0.05F;
            }
            else
            {
                a = 1;
                b = 0;
                s = -0.05F;
            }

            Point curPoint;

            // "animate" the window
            Size curSize;
            Point startPoint = this.Location;
            int dWidth = destPoint.X - startPoint.X;
            int dHeight = destPoint.Y - startPoint.Y;
            int startWidth = this.Width;
            int startHeight = this.Height;
            float i;
            for (i = a; i <= b; i += s)
            {
                curPoint = new Point(Convert.ToInt32(startPoint.X + i * dWidth), Convert.ToInt32(startPoint.Y + i * dHeight));
                curSize = new Size(Convert.ToInt32((1 - i) * startWidth), Convert.ToInt32((1 - i) * startHeight));
                ControlPaint.DrawReversibleFrame(new Rectangle(curPoint, curSize), this.BackColor, FrameStyle.Thick);
                System.Threading.Thread.Sleep(15);
                ControlPaint.DrawReversibleFrame(new Rectangle(curPoint, curSize), this.BackColor, FrameStyle.Thick);
            }

            if (ToTray)
            {
                // hide the form and show the notifyicon
                this.Hide();
                notifyIcon1.Visible = true;
            }
            else
            {
                // hide the notifyicon and show the form
                notifyIcon1.Visible = false;
                this.Show();
            }
        }

        /// <summary>
        /// Initializes the form
        /// </summary>
        private void Initialize()
        {
            StartTimer();
            StartProcess();
        }
        
        /// <summary>
        /// Starts the timer 
        /// </summary>
        private void StartTimer()
        {

            tmrStatusBar = new  System.Timers.Timer(1000);
            tmrStatusBar.Elapsed += tmrStatusBar_Elasped;
            tmrStatusBar.AutoReset = true;
            tmrStatusBar.Enabled = true;
            tmrStatusBar.Start();

            tmrRestartApp = new System.Timers.Timer(60 * 1000);
            tmrRestartApp.Elapsed += tmrRestartApp_Elapsed;
            tmrRestartApp.AutoReset = true;
            tmrRestartApp.Enabled = true;
            tmrRestartApp.Start();

        }

        /// <summary>
        /// Timer event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrStatusBar_Elasped(object sender, System.Timers.ElapsedEventArgs e) 
        {
            UpdateStausBar();
        }

        /// <summary>
        /// Timer event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrRestartApp_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            string strDate = string.Empty;
            DateTime dte = new DateTime();
            bool bln = false;

            try
            {

                tmrRestartApp.Enabled = false;
                tmrRestartApp.Stop();

                Utilities.isRestarted4Today = false;

                if (File.Exists(Environment.CurrentDirectory + @"\RestartedExe.txt"))
                {
                    string dteStr = string.Empty;
                    dteStr = File.ReadAllText(Environment.CurrentDirectory + @"\RestartedExe.txt");

                    if (DateTime.Now.ToString("yyyy-MMM-dd") == dteStr)
                    {
                        Utilities.isRestarted4Today = true;
                    }
                   
                }

                strDate = DateTime.Now.ToString("yyyy-MMM-dd") + " " + Properties.Settings.Default.RestartApplicationTime.ToString();

                bln = DateTime.TryParseExact(strDate, "yyyy-MMM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dte);

                if (!bln) {
                    Logs.WriteLog("tmrRestartApp_Elapsed : Invalid date format not able to convert");
                    return;
                }

                if (DateTime.Now >= dte)
                {
                    if (!Utilities.isRestarted4Today)
                    {
                        File.WriteAllText(Environment.CurrentDirectory + @"\RestartedExe.txt", DateTime.Now.ToString("yyyy-MMM-dd"));
                        this.Invoke(new Action(() => Application.Restart()));
                        this.Invoke(new Action(() => Application.ExitThread()));
                    }
                }
                
            }
            catch (Exception exp)
            {
                Logs.WriteLog("tmrRestartApp_Elapsed : " + exp.ToString());
            }
            finally {
                strDate = null;
                tmrRestartApp.Enabled = true;
                tmrRestartApp.Start();
            }

        }

        /// <summary>
        /// Starts the listening process
        /// </summary>
        private void StartProcess()
        {

            sdkResponseObj zkRespObj = new sdkResponseObj();
            List<sdkEventsApiDetails> zkBSApiDetList = new List<sdkEventsApiDetails>();
            sdkEventsApiDetails zkEventApiDet = new sdkEventsApiDetails();

            string Url = string.Empty;
            string strm = string.Empty;
            string str = string.Empty;
            WebRequest httpWR = null;
            JsonSerializer serializer = new JsonSerializer();

            wsEvents.wsDeviceEvents wsAPI = new wsEvents.wsDeviceEvents();

            try
            {

                if (string.Compare(Properties.Settings.Default.ListernerFor, "BS", true) == 0)
                {

                    if (Properties.Settings.Default.IsRestAPI)
                    {

                        Url = Properties.Settings.Default.WebApiUrl + @"GetApiDetails?access_token=" +
                        Properties.Settings.Default.WebApiToken;

                        httpWR = HttpWebRequest.Create(Url);
                        httpWR.Method = "GET";
                        httpWR.ContentType = "application/json";

                        using (WebResponse tResponse = httpWR.GetResponse())
                        {
                            using (Stream dataStreamResponse = tResponse.GetResponseStream())
                            {
                                using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    string sResponseFromServer = tReader.ReadToEnd();
                                    str = sResponseFromServer;
                                }
                            }
                        }

                        if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
                        {
                            DisplayMsg("StartProcess : No API Details found to process.");
                        }
                        else
                        {
                            zkBSApiDetList = JsonConvert.DeserializeObject<List<sdkEventsApiDetails>>(str);

                            Utilities.accCtrl.apiCallEventList = new sdkCallEventList();
                            Utilities.accCtrl.apiCallEventList.apiEventDetList = zkBSApiDetList;
                            Utilities.accCtrl.apiTmInterval = Properties.Settings.Default.TmIntervalInMin;
                            Utilities.accCtrl.sdkApiWriteLog = Properties.Settings.Default.WriteInfoLog.ToString();

                            zkRespObj = Utilities.accCtrl.StartEventListener();

                            if (zkRespObj.code != "SUC")
                            {
                                if (zkRespObj.hasError)
                                {
                                    DisplayMsg("StartProcess : " + zkRespObj.errorMessage);
                                }
                                else if (zkRespObj.showInfoMessage)
                                {
                                    DisplayMsg("StartProcess : " + zkRespObj.infoMessage);
                                }
                            }
                            else
                            {
                                DisplayMsg(zkRespObj.infoMessage);
                            }

                        }

                    }   
                    else
                    {

                        wsEvents.sdkCallEventList zkApiDet = wsAPI.GetApiDetails();

                        str = JsonConvert.SerializeObject(zkApiDet.apiEventDetList);
                        
                        zkBSApiDetList =  JsonConvert.DeserializeObject<List<sdkEventsApiDetails>>(str);

                        Utilities.accCtrl.apiCallEventList = new sdkCallEventList();
                        Utilities.accCtrl.apiCallEventList.apiEventDetList = zkBSApiDetList;
                        Utilities.accCtrl.apiTmInterval = Properties.Settings.Default.TmIntervalInMin;
                        Utilities.accCtrl.sdkApiWriteLog = Properties.Settings.Default.WriteInfoLog.ToString();

                        zkRespObj = Utilities.accCtrl.StartEventListener();

                        if (zkRespObj.code != "SUC")
                        {
                            if (zkRespObj.hasError)
                            {
                                DisplayMsg("StartProcess : " + zkRespObj.errorMessage);
                            }
                            else if (zkRespObj.showInfoMessage)
                            {
                                DisplayMsg("StartProcess : " + zkRespObj.infoMessage);
                            }
                        }
                        else
                        {
                            DisplayMsg(zkRespObj.infoMessage);
                        }

                    }

                }
                else
                {

                    DisplayMsg("StartProcess : No Speed Face Device Functionality Implemented.");

                }



            }
            catch (Exception exp)
            {
                DisplayMsg("StartProcess : " + exp.ToString());
            }
            finally
            {
                zkRespObj = null;
            }

        }

        #endregion //Methods & Functions

        #region Context Menu

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnimateWindow(false);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ignoreCase = false;
            Application.Exit();
        }

        #endregion //Context Menu

        #region Events

        private void zkApiCs_evCommandUpload(object sender, CommandUploadEventArgs e)
        {
            sdkBSEventsCommandResult sdkCr = new sdkBSEventsCommandResult();
            //wsEvents.zkCommandResult wsZKCR = new wsSF.zkCommandResult();
            string Url = string.Empty;
            string strm = string.Empty;
            string str = string.Empty;
            WebRequest httpWR = null;
            JsonSerializer serializer = new JsonSerializer();
            Byte[] byteArray = null;

            try
            {                

                if (Properties.Settings.Default.IsRestAPI)
                {

                    sdkCr.command = e.command;
                    sdkCr.commandId = e.commandId;
                    sdkCr.deviceSN = e.deviceSN;
                    sdkCr.returnId = e.returnId;
                    sdkCr.returnMessage = e.returnMessage;
                    sdkCr.pin = e.pin;

                    Url = Properties.Settings.Default.WebApiUrl + @"PushCommandResultEvents?access_token=" +
                    Properties.Settings.Default.WebApiToken;

                    httpWR = HttpWebRequest.Create(Url);
                    httpWR.Method = "POST";
                    httpWR.ContentType = "application/json";

                    strm = JsonConvert.SerializeObject(sdkCr);

                    byteArray = Encoding.UTF8.GetBytes(strm);

                    httpWR.ContentLength = byteArray.Length;

                    using (Stream dataStream = httpWR.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);

                        using (WebResponse tResponse = httpWR.GetResponse())
                        {
                            using (Stream dataStreamResponse = tResponse.GetResponseStream())
                            {
                                using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    str = sResponseFromServer;
                                }
                            }
                        }
                    }

                }
                else
                {

                    //wsSF.wsCorporate wsAPI = new wsSF.wsCorporate();

                    //wsZKCR.command = sdkCr.command;
                    //wsZKCR.commandId = sdkCr.commandId;
                    //wsZKCR.deviceSN = sdkCr.deviceSN;
                    //wsZKCR.pin = sdkCr.pin;
                    //wsZKCR.returnId = sdkCr.returnId;
                    //wsZKCR.returnMessage = sdkCr.returnMessage;

                    //wsAPI.PushCommandResultEvents(wsZKCR);

                }
                

            }
            catch (Exception exp)
            {
                Logs.WriteLog("accCtrl_evCommandUpload : " + exp.ToString());
            }
            finally
            {
                sdkCr = null;
                //wsZKCR = null;
                Url = null;
                strm = null;
                str = null;
                httpWR = null;
                serializer = null;
                byteArray = null;
            }

        }

        private void zkApiCs_evDataUpload(object sender, DataUploadEventArgs e)
        {

            sdkBSEventsDataUploadResult sdkDur = new sdkBSEventsDataUploadResult();
            //wsSF.zkDataUpload wsZKDU = new wsSF.zkDataUpload();
            string Url = string.Empty;
            string strm = string.Empty;
            string str = string.Empty;
            WebRequest httpWR = null;
            JsonSerializer serializer = new JsonSerializer();
            Byte[] byteArray = null;

            try
            {
                
                if (Properties.Settings.Default.IsRestAPI)
                {

                    sdkDur.pin = e.pin;
                    sdkDur.cardNo = e.cardNo;
                    sdkDur.deviceSN = e.deviceSN;
                    sdkDur.disable = e.disable;
                    sdkDur.endTime = e.endTime;
                    sdkDur.group = e.group;
                    sdkDur.name = e.name;
                    sdkDur.password = e.password;
                    sdkDur.privilege = e.privilege;
                    sdkDur.startTime = e.startTime;
                    sdkDur.uploadId = e.uploadId;
                    sdkDur.verify = e.verify;
                    sdkDur.photo = e.photo;
                    sdkDur.photoSize = e.photoSize;

                    Url = Properties.Settings.Default.WebApiUrl + @"PushDataUploadEvents?access_token=" +
                    Properties.Settings.Default.WebApiToken;

                    httpWR = HttpWebRequest.Create(Url);
                    httpWR.Method = "POST";
                    httpWR.ContentType = "application/json";

                    strm = JsonConvert.SerializeObject(sdkDur);

                    byteArray = Encoding.UTF8.GetBytes(strm);

                    httpWR.ContentLength = byteArray.Length;

                    using (Stream dataStream = httpWR.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);

                        using (WebResponse tResponse = httpWR.GetResponse())
                        {
                            using (Stream dataStreamResponse = tResponse.GetResponseStream())
                            {
                                using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    str = sResponseFromServer;
                                }
                            }
                        }
                    }

                }
                else
                {

                    //wsSF.wsCorporate wsAPI = new wsSF.wsCorporate();

                    //wsZKDU.cardNo = sdkDur.cardNo;
                    //wsZKDU.deviceSN = sdkDur.deviceSN;
                    //wsZKDU.disable = sdkDur.disable;
                    //wsZKDU.endTime = sdkDur.endTime;
                    //wsZKDU.group = sdkDur.group;
                    //wsZKDU.name = sdkDur.name;
                    //wsZKDU.password = sdkDur.password;
                    //wsZKDU.photo = sdkDur.photo;
                    //wsZKDU.photoSize = sdkDur.photoSize;
                    //wsZKDU.pin = sdkDur.pin;
                    //wsZKDU.privilege = sdkDur.privilege;
                    //wsZKDU.startTime = sdkDur.startTime;
                    //wsZKDU.uploadId = sdkDur.uploadId;
                    //wsZKDU.verify = sdkDur.verify;

                    //wsAPI.PushDataUploadEvents(wsZKDU);

                }
                

            }
            catch (Exception exp)
            {
                Logs.WriteLog("accCtrl_evDataUpload : " + exp.ToString());
            }
            finally
            {
                sdkDur = null;
                Url = null;
                strm = null;
                str = null;
                httpWR = null;
                serializer = null;
                byteArray = null;
            }

        }

        private void zkApiCs_evDisplayMessage(object sender, DisplayMessageEventArgs e)
        {
                        
            string Url = string.Empty;
            string strm = string.Empty;
            string str = string.Empty;
            WebRequest httpWR = null;
            JsonSerializer serializer = new JsonSerializer();
            Byte[] byteArray = null;

            try
            {

                if (Properties.Settings.Default.IsRestAPI)
                {

                    Url = Properties.Settings.Default.WebApiUrl + @"PushDisplayMessageEvents?access_token=" +
                    Properties.Settings.Default.WebApiToken;

                    httpWR = HttpWebRequest.Create(Url);
                    httpWR.Method = "POST";
                    httpWR.ContentType = "application/json";

                    byteArray = Encoding.UTF8.GetBytes(e.message);

                    httpWR.ContentLength = byteArray.Length;

                    using (Stream dataStream = httpWR.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);

                        using (WebResponse tResponse = httpWR.GetResponse())
                        {
                            using (Stream dataStreamResponse = tResponse.GetResponseStream())
                            {
                                using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    str = sResponseFromServer;
                                }
                            }
                        }
                    }

                }
                else
                {

                    //wsSF.wsCorporate wsAPI = new wsSF.wsCorporate();
                    //wsAPI.PushDisplayMessageEvents(e.message);

                }
                

                DisplayMsg(e.message);

            }
            catch (Exception exp)
            {
                Logs.WriteLog("accCtrl_evDisplayMessage : " + exp.ToString());
            }
            finally
            {
                Url = null;
                strm = null;
                str = null;
                httpWR = null;
                serializer = null;
                byteArray = null;
            }
        }

        private void zkApiCs_evRealTime(object sender, RealTimeEventArgs e)
        {

            sdkBSEventsRealTimeData sdkRTD = new sdkBSEventsRealTimeData();
            wsEvents.sdkBSEventsRealTimeData wsSdkRTD = new wsEvents.sdkBSEventsRealTimeData();
            string Url = string.Empty;
            string strm = string.Empty;
            string str = string.Empty;
            WebRequest httpWR = null;
            JsonSerializer serializer = new JsonSerializer();
            Byte[] byteArray = null;

            try
            {
                               

                if (Properties.Settings.Default.IsRestAPI)
                {

                    sdkRTD.accZone = e.accZone;
                    sdkRTD.areaName = e.areaName;
                    sdkRTD.cardNo = e.cardNo;
                    sdkRTD.deptName = e.deptName;
                    sdkRTD.devName = e.devName;
                    sdkRTD.devSn = e.devSn;
                    sdkRTD.eventName = e.eventName;
                    sdkRTD.eventNumber = e.eventNumber;
                    sdkRTD.eventPointName = e.eventPointName;
                    sdkRTD.eventTime = e.eventTime;
                    sdkRTD.name = e.name;
                    sdkRTD.id = e.id;
                    sdkRTD.inoutStatus = e.inoutStatus;
                    sdkRTD.lastName = e.lastName;
                    sdkRTD.logId = e.logId;
                    sdkRTD.maskFlag = e.maskFlag;
                    sdkRTD.photoPath = e.photoPath;
                    sdkRTD.pin = e.pin;
                    sdkRTD.readerName = e.readerName;
                    sdkRTD.status = e.status;
                    sdkRTD.temperature = e.temperature;
                    sdkRTD.verifyModeName = e.verifyModeName;
                    sdkRTD.eventFrom = e.eventFrom;
                    sdkRTD.eventResult = e.eventResult;
                    sdkRTD.eventScannedFrom = e.eventScannedFrom;
                    sdkRTD.uniqueId = e.uniqueId;

                    Url = Properties.Settings.Default.WebApiUrl + @"PushRealTimeEvents?access_token=" +
                                        Properties.Settings.Default.WebApiToken;

                    httpWR = HttpWebRequest.Create(Url);
                    httpWR.Method = "POST";
                    httpWR.ContentType = "application/json";

                    strm = JsonConvert.SerializeObject(sdkRTD);

                    if (Properties.Settings.Default.WriteInfoLog)
                    {
                        Logs.WriteLog("Push Real Time Result : " + strm);
                    }

                    byteArray = Encoding.UTF8.GetBytes(strm);

                    httpWR.ContentLength = byteArray.Length;

                    using (Stream dataStream = httpWR.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);

                        using (WebResponse tResponse = httpWR.GetResponse())
                        {
                            using (Stream dataStreamResponse = tResponse.GetResponseStream())
                            {
                                using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    str = sResponseFromServer;
                                }
                            }
                        }
                    }
                }
                else
                {

                    wsEvents.wsDeviceEvents wsAPI = new wsEvents.wsDeviceEvents();

                    wsSdkRTD.accZone = e.accZone;
                    wsSdkRTD.areaName = e.areaName;
                    wsSdkRTD.cardNo = e.cardNo;
                    wsSdkRTD.deptName = e.deptName;
                    wsSdkRTD.devName = e.devName;
                    wsSdkRTD.devSn = e.devSn;
                    wsSdkRTD.eventName = e.eventName;
                    wsSdkRTD.eventNumber = e.eventNumber;
                    wsSdkRTD.eventPointName = e.eventPointName;
                    wsSdkRTD.eventTime = e.eventTime;
                    wsSdkRTD.id = e.id;
                    wsSdkRTD.inoutStatus = e.inoutStatus;
                    wsSdkRTD.lastName = e.lastName;
                    wsSdkRTD.logId = e.logId;
                    wsSdkRTD.maskFlag = e.maskFlag;
                    wsSdkRTD.name = e.name;
                    wsSdkRTD.photoPath = e.photoPath;
                    wsSdkRTD.pin = e.pin;
                    wsSdkRTD.readerName = e.readerName;
                    wsSdkRTD.status = e.status;
                    wsSdkRTD.temperature = e.temperature;
                    wsSdkRTD.verifyModeName = e.verifyModeName;
                    wsSdkRTD.eventFrom = e.eventFrom;
                    wsSdkRTD.eventResult = e.eventResult;

                    wsAPI.PushRealTimeEvents(wsSdkRTD);

                    if (Properties.Settings.Default.WriteInfoLog)
                    {
                        strm = JsonConvert.SerializeObject(wsSdkRTD);
                        Logs.WriteLog("Push Real Time Result at : " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") + " : " + strm);
                    }

                }
                

            }
            catch (Exception exp)
            {
                Logs.WriteLog("accCtrl_evRealTime : " + exp.ToString());
            }
            finally
            {
                sdkRTD = null;
               // wsZkRTD = null;
                Url = null;
                strm = null;
                str = null;
                httpWR = null;
                serializer = null;
                byteArray = null;
            }

        }

        private void zkApiCs_evEmpLog(object sender, EmployeeLogEventArgs e)
        {

            sdkBSEventsEmployeeLogData sdkELD = new sdkBSEventsEmployeeLogData();
            //wsSF.zkBSEmployeeLogData wsZkELD = new wsSF.zkBSEmployeeLogData();
            string Url = string.Empty;
            string strm = string.Empty;
            string str = string.Empty;
            WebRequest httpWR = null;
            JsonSerializer serializer = new JsonSerializer();
            Byte[] byteArray = null;

            try
            {

                if (string.IsNullOrEmpty(e.pin) || string.IsNullOrWhiteSpace(e.pin)) {
                    return;
                }

                if (string.Compare(e.verifyModeName, "acc_verify_mode_onlyface", true) != 0) {
                    return;
                }
                               
                    
                if (Properties.Settings.Default.IsRestAPI)
                {

                    sdkELD.accZone = e.accZone;
                    sdkELD.areaName = e.areaName;
                    sdkELD.cardNo = e.cardNo;
                    sdkELD.deptName = e.deptName;
                    sdkELD.devName = e.devName;
                    sdkELD.devSn = e.devSn;
                    sdkELD.eventName = e.eventName;
                    sdkELD.eventPointName = e.eventPointName;
                    sdkELD.eventTime = e.eventTime;
                    sdkELD.id = e.id;
                    sdkELD.lastName = e.lastName;
                    sdkELD.logId = e.logId;
                    sdkELD.name = e.name;
                    sdkELD.pin = e.pin;
                    sdkELD.readerName = e.readerName;
                    sdkELD.verifyModeName = e.verifyModeName;

                    Url = Properties.Settings.Default.WebApiUrl + @"PushEmployeeLogEvents?access_token=" +
                                        Properties.Settings.Default.WebApiToken;

                    httpWR = HttpWebRequest.Create(Url);
                    httpWR.Method = "POST";
                    httpWR.ContentType = "application/json";

                    strm = JsonConvert.SerializeObject(sdkELD);

                    byteArray = Encoding.UTF8.GetBytes(strm);

                    httpWR.ContentLength = byteArray.Length;

                    using (Stream dataStream = httpWR.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);

                        using (WebResponse tResponse = httpWR.GetResponse())
                        {
                            using (Stream dataStreamResponse = tResponse.GetResponseStream())
                            {
                                using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    str = sResponseFromServer;
                                }
                            }
                        }
                    }
                }
                else
                {

                    //wsSF.wsCorporate wsApi = new wsSF.wsCorporate();

                    //wsZkELD.accZone = e.accZone;
                    //wsZkELD.areaName = e.areaName;
                    //wsZkELD.cardNo = e.cardNo;
                    //wsZkELD.deptName = e.deptName;
                    //wsZkELD.devName = e.devName;
                    //wsZkELD.devSn = e.devSn;
                    //wsZkELD.eventName = e.eventName;
                    //wsZkELD.eventPointName = e.eventPointName;
                    //wsZkELD.eventTime = e.eventTime;
                    //wsZkELD.id = e.id;
                    //wsZkELD.lastName = e.lastName;
                    //wsZkELD.logId = e.logId;
                    //wsZkELD.name = e.name;
                    //wsZkELD.pin = e.pin;
                    //wsZkELD.readerName = e.readerName;
                    //wsZkELD.verifyModeName = e.verifyModeName;

                    //wsApi.PushEmployeeLogEvents(wsZkELD);

                }


            }
            catch (Exception exp)
            {
                Logs.WriteLog("accCtrl_evEmpLog : " + exp.ToString());
            }
            finally
            {
                sdkELD = null;
                //wsZkELD = null;
                Url = null;
                strm = null;
                str = null;
                httpWR = null;
                serializer = null;
                byteArray = null;
            }

        }

        #endregion //Events

    }
}
