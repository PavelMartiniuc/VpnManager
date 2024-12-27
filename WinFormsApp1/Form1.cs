using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using OpenPop.Pop3;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {

        private const int WM_CLOSE = 16;
        private const int BN_CLICKED = 0x0001;

        const int WM_GETTEXT = 0x000D;
        const int WM_GETTEXTLENGTH = 0x000E;

        private bool firstStart = false;
        bool _searchingPassword = false;

        const int SW_RESTORE = 9;

        //const int WM_COMMAND = 0x0111;
        //const int BN_CLICKED = 0;

        private bool _isConnected;
        private string _smsCode = string.Empty;
        private bool _isEnteringSms = false;

        public bool IsConnected
        {
            get
            {
                bool isConnected = GetConnectButtonText() == "Disconnect";
                btConnect.Enabled = !isConnected;
                btDisconnect.Enabled = isConnected;
                return isConnected;
            }
        }

        Process? _ciscoProcess = null;

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr handle, int nCmdShow);
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool IsIconic(IntPtr handle);


        ///// <summary>
        ///// The FindWindow API
        ///// </summary>
        ///// <param name="lpClassName">the class name for the window to search for</param>
        ///// <param name="lpWindowName">the name of the window to search for</param>
        ///// <returns></returns>
        //[DllImport("User32.dll")]
        //public static extern Int32 FindWindow(String lpClassName, String lpWindowName);

        /// <summary>
        /// The SendMessage API
        /// </summary>
        /// <param name="hWnd">handle to the required window</param>
        /// <param name="msg">the system/Custom message to send</param>
        /// <param name="wParam">first message parameter</param>
        /// <param name="lParam">second message parameter</param>
        /// <returns></returns>
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int SendMessage(int hWnd, int msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "GetWindowText", CharSet = CharSet.Auto)]
        static extern IntPtr GetWindowCaption(IntPtr hwnd, StringBuilder lpString, int maxCount);


        private const int SW_SHOWMAXIMIZED = 3;

        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr FindWindowExW(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass = "", string lpszWindow = "");

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetClassName(IntPtr hWnd, StringBuilder lpszClass, int maxCount);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lp1, string lp2);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        static extern int SendMessage3(IntPtr hwndControl, uint Msg, int wParam, StringBuilder strBuffer); // get text

        [DllImport("user32.dll", EntryPoint = "SendMessage",
          CharSet = CharSet.Auto)]
        static extern int SendMessage4(IntPtr hwndControl, uint Msg, int wParam, int lParam);  // text length

        [DllImport("user32.dll", EntryPoint = "SetFocus",
         CharSet = CharSet.Auto)]
        static extern int SetFocus(IntPtr hwndControl);  // text length

        static int GetTextBoxTextLength(IntPtr hTextBox)
        {
            // helper for GetTextBoxText
            uint WM_GETTEXTLENGTH = 0x000E;
            int result = SendMessage4(hTextBox, WM_GETTEXTLENGTH,
              0, 0);
            return result;
        }

        static string GetTextBoxText(IntPtr hTextBox)
        {
            uint WM_GETTEXT = 0x000D;
            int len = GetTextBoxTextLength(hTextBox);
            if (len <= 0) return null;  // no text
            StringBuilder sb = new StringBuilder(len + 1);
            SendMessage3(hTextBox, WM_GETTEXT, len + 1, sb);
            return sb.ToString();
        }

        static void SetTextBoxText(IntPtr hTextBox, string text)
        {
            uint WM_SETTEXT = 0x000C;
            StringBuilder sb = new StringBuilder(text.Length + 1);
            sb.Append(text);
            SendMessage3(hTextBox, WM_SETTEXT, text.Length + 1, sb);
        }

        public static void BringProcessToFront(Process process)
        {
            if (process == null)
                return;
            IntPtr handle = process.MainWindowHandle;
            if (IsIconic(handle))
            {
                ShowWindow(handle, SW_RESTORE);
            }

            SetForegroundWindow(handle);
        }

        private IntPtr GetConnectButton()
        {
            if (_ciscoProcess == null)
                return IntPtr.Zero;
            return GetFirstButton(_ciscoProcess.MainWindowHandle);
        }

        private void ClickOnButton(IntPtr button)
        {
            int BM_CLICK = 0x00F5;
            SendMessage(button, BM_CLICK, IntPtr.Zero, IntPtr.Zero);
        }

        private async void TrackConnectedStatus()
        {
            while (true)
            {
                bool connected = IsConnected;
                await Task.Delay(10000);
            }
        }

        private string GetConnectButtonText()
        {
            return GetControlText(GetConnectButton());
        }

        private List<IntPtr> GetAllChildrenWindowHandles(IntPtr hParent, int maxCount)
        {
            List<IntPtr> result = new List<IntPtr>();
            int ct = 0;
            IntPtr prevChild = IntPtr.Zero;
            IntPtr currChild = IntPtr.Zero;
            while (true && ct < maxCount)
            {
                currChild = FindWindowEx(hParent, prevChild, null, null);
                if (currChild == IntPtr.Zero) break;
                result.Add(currChild);
                prevChild = currChild;
                ++ct;
            }
            return result;
        }


        private IntPtr GetFirstEditInComboBox(IntPtr mainWindowHandle)
        {
            var children = GetAllChildrenWindowHandles(mainWindowHandle, 200);

            foreach (var child in children)
            {
                //var text = GetTextBoxText(child);
                var combo = FindWindowEx(child, IntPtr.Zero, "ComboBox", null);
                if (combo == IntPtr.Zero)
                    continue;
                var edit = FindWindowEx(combo, IntPtr.Zero, "Edit", null);
                if (edit == IntPtr.Zero)
                    continue;
                return edit;
            }

            return IntPtr.Zero;
        }

        private IntPtr GetFirstButton(IntPtr mainWindowHandle)
        {
            var children = GetAllChildrenWindowHandles(mainWindowHandle, 200);

            foreach (var child in children)
            {
                //var text = GetTextBoxText(child);
                var button = FindWindowEx(child, IntPtr.Zero, "Button", null);
                if (button == IntPtr.Zero)
                    continue;
                return button;
            }

            return IntPtr.Zero;
        }

        private IntPtr GetFirstListItem(IntPtr mainWindowHandle)
        {
            var children = GetAllChildrenWindowHandles(mainWindowHandle, 200);

            foreach (var child in children)
            {
                //var text = GetTextBoxText(child);
                var combo = FindWindowEx(child, IntPtr.Zero, "ComboBox", null);
                if (combo == IntPtr.Zero)
                    continue;
                var edit = FindWindowEx(combo, IntPtr.Zero, "ListItem", null);
                if (edit == IntPtr.Zero)
                    continue;
                return edit;
            }

            return IntPtr.Zero;
        }

        private async void Connect()
        {
            if (IsConnected)
                return;

            _searchingPassword = false;
            _smsCode = string.Empty;
            StartCiscoVpn();
            //if (firstStart)
            //    await Task.Delay(500);

            _ciscoProcess = GetCiscoProcess();

            // rem
            SetForegroundWindow(_ciscoProcess.MainWindowHandle);

            if (firstStart && Convert.ToBoolean(ConfigurationManager.AppSettings["autoConnect"]))
            {
                await Task.Delay(10000);
                firstStart = false;
            }

            BringProcessToFront(_ciscoProcess);
            SetForegroundWindow(_ciscoProcess.MainWindowHandle);
            
            
            //SetForegroundWindow(GetConnectButton());
            //SetForegroundWindow(serverTextBox);
            //SetTextBoxText(serverTextBox, ConfigurationManager.AppSettings["vpnServer"]);
            //await Task.Delay(500);
            //SetForegroundWindow(GetConnectButton());

                

            //var listItem = GetFirstListItem(_ciscoProcess.MainWindowHandle);
            //var text = GetControlText(listItem);
            //await Task.Delay(1000);

            //await Task.Delay(1000);
            //ClickOnButton(GetConnectButton());
            //SetForegroundWindow(GetConnectButton());
            SendKeys.SendWait("{ENTER}");
            IntPtr connectWindow = IntPtr.Zero;
            _searchingPassword = true;
            do
            {
                if (!_searchingPassword)
                    return;
                await Task.Delay(1000);
                connectWindow = FindWindow(null, "Cisco AnyConnect | remote-shj.petrofac.com");
            }
            while (connectWindow == IntPtr.Zero);

            if (!_searchingPassword)
                return;

            SetForegroundWindow(connectWindow);

            var passwordKeys = GetKeys(System.Configuration.ConfigurationManager.AppSettings["vpnPassword"]);

            SendKeys.SendWait(passwordKeys);
            SendKeys.SendWait("{ENTER}");
            _searchingPassword = false;
            gbSMS.Enabled = true;
            _isEnteringSms = true;
            txtSms.Focus();
            await StartListeningEmailsWithCode();
        }

        private async void Disconnect()
        {
            if (!IsConnected)
                return;
            BringProcessToFront(_ciscoProcess);
            //await Task.Delay(2000);
            //BringProcessToFront(_ciscoProcess);
            SendKeys.SendWait("{ENTER}");
            this.KeyPreview = false;

            _isEnteringSms = false;
        }

        //private string GetButtonText(IntPtr parentWindow)
        //{
        //    var buttonPtr = FindWindowEx(parentWindow, IntPtr.Zero, "Button", null);

        //    //Alloc memory for the buffer that recieves the text
        //    var Hndl = Marshal.AllocHGlobal(200);

        //    // Send The WM_GETTEXT Message
        //    SendMessage(buttonPtr, WM_GETTEXT, 200, Hndl);

        //   //copy the characters from the unmanaged memory to a managed string
        //    var result = Marshal.PtrToStringUni(Hndl);

        //    return result ?? "NotFound";
        //}

        private string GetButtonText(IntPtr button)
        {
            var result = GetControlText(button);
            return result ?? "NotFound";
        }

        private void StartCiscoVpn()
        {
            firstStart = false;
            if (_ciscoProcess != null)
                return;
            _ciscoProcess = new Process();
            _ciscoProcess.StartInfo.FileName = "C:\\Program Files (x86)\\Cisco\\Cisco AnyConnect Secure Mobility Client\\vpnui.exe";
            _ciscoProcess.StartInfo.Arguments = "-n";
            _ciscoProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            _ciscoProcess.Start();
            btStopCisoVpn.Enabled = _ciscoProcess != null;
            firstStart = true;
        }

        private void StopCiscoVpn()
        {
            if (_ciscoProcess == null)
                return;
            _ciscoProcess.Kill(true);
            _ciscoProcess = null;
            btStopCisoVpn.Enabled = _ciscoProcess != null;
        }

        private Process? GetCiscoProcess()
        {
            var processes = Process.GetProcessesByName("vpnui");
            if (processes.Any())
                return processes[0];
            return null;
        }

        private void CloseCiscoVpn()
        {
            if (_ciscoProcess == null)
                return;
            _ciscoProcess.Close();
            _ciscoProcess = null;
        }

        private string GetKeys(string inputString)
        {
            var outputKeys = string.Empty;
            foreach (char key in inputString)
            {
                outputKeys += "{" + key + "}";
            }
            return outputKeys;
        }


        public Form1()
        {
            InitializeComponent();
            _ciscoProcess = GetCiscoProcess();
            btStopCisoVpn.Enabled = _ciscoProcess != null;
            this.KeyPreview = true;
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
           Connect();
        }

        private void btDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void bt1_Click(object sender, EventArgs e)
        {
            var digit = (sender as Button).Text;
            _smsCode += digit;
            txtSms.Text = _smsCode;
        }

        private void btConfirmSMS_Click(object sender, EventArgs e)
        {
            ConfirmSms();
        }

        private void ConfirmSms()
        {
            var smskeys = GetKeys(_smsCode);
            var connectWindow = FindWindow(null, "Cisco AnyConnect | remote-shj.petrofac.com");
            SetForegroundWindow(connectWindow);
            SendKeys.SendWait(smskeys);
            SendKeys.SendWait("{ENTER}");
            txtSms.Text = _smsCode;
            _isEnteringSms = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _smsCode = string.Empty;
            txtSms.Text = _smsCode;
        }

        public string GetControlText(IntPtr hWnd)
        {
            // Get the size of the string required to hold the window title (including trailing null.) 
            Int32 titleSize = SendMessage(hWnd, WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
            // If titleSize is 0, there is no title so return an empty string (or null)
            if (titleSize == 0)
                return String.Empty;
            StringBuilder title = new StringBuilder(titleSize + 1);
            SendMessage(hWnd, (int)WM_GETTEXT, title.Capacity, title);
            return title.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TrackConnectedStatus();
            btConnect.PerformClick();
        }

        private void txtSms_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSms.Text))
                return;
            if (e.KeyCode == Keys.Enter)
                ConfirmSms();
        }

        private void btSetConnected_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbSetConnected.Checked)
            //{
            //    IsConnected = true;
            //}
            //else
            //{
            //    IsConnected = false;
            //}
        }

        private void btStopCisco_Click(object sender, EventArgs e)
        {
            StopCiscoVpn();
        }

        private async Task StartListeningEmailsWithCode()
        {
            var config = System.Configuration.ConfigurationManager.AppSettings;

            DateTime startCheckDate = DateTime.Now;
            await Task.Delay(1000);
            string password = config["emailPassword"];
            string mail = config["email"];

            string code = string.Empty;
            do
            {
                var client = new Pop3Client();
                client.Connect(config["smtpServer"], Convert.ToInt32(config["smtpPort"]), true);
                client.Authenticate(mail, password);

                var count = client.GetMessageCount();
                var message = client.GetMessage(count);
                string subject = message.Headers.Subject;
                string from = message.Headers.From.Address;

                if (subject.Contains("pavel_vpn_manager") && from == mail)
                {
                    var body = message.FindFirstPlainTextVersion().GetBodyAsText();
                    int slashIndex = body.IndexOf("/");
                    DateTime messageDate = DateTime.Parse(body.Substring(0, slashIndex), new CultureInfo("ru-RU"));
                    if (messageDate >= startCheckDate)
                    {
                        code = body.Substring(slashIndex + 1);
                        _smsCode = code;
                        txtSms.Invoke(() => txtSms.Text = code);
                        await Task.Delay(5000);
                        btConfirmSMS.PerformClick();
                    }
                }

                await Task.Delay(5000);
            }
            while (string.IsNullOrEmpty(code));

        }
    }
}
