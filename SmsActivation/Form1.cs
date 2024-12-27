using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualBasic.ApplicationServices;
using OpenPop.Pop3;
using static System.Net.Mime.MediaTypeNames;

namespace SmsActivation
{
    public partial class fmMain : Form
    {
        #region Managing Procentia VRM
        private Process? _remoteDesctopProc;
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lp1, string lp2);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);



        // Import EnumChildWindows from User32.dll
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool EnumChildWindows(IntPtr hWndParent, EnumChildProc lpEnumFunc, IntPtr lParam);

        // Delegate for EnumChildWindows callback
        private delegate bool EnumChildProc(IntPtr hwnd, IntPtr lParam);

        // Import GetClassName from User32.dll
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        // Import GetFocus from User32.dll
        [DllImport("user32.dll")]
        private static extern IntPtr GetFocus();

        // Import GetWindowThreadProcessId from User32.dll
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // Import GetCurrentThreadId from Kernel32.dll
        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();

        // Import AttachThreadInput from User32.dll
        [DllImport("user32.dll")]
        private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        // Import SendMessage from User32.dll
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        // BM_CLICK is a predefined constant for the click message
        private const uint BM_CLICK = 0x00F5;

        [DllImport("user32.dll")]
        private static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        public const uint WM_CLOSE = 0x0010;

        #endregion

        private bool isListening;
        private bool isConnectingToVM;

        public bool IsListening
        {
            get
            {
                return isListening;
            }

            set
            {
                isListening = value;
                btStartListening.Enabled = !isListening;
                btStopListening.Enabled = isListening;
            }

        }

        public fmMain()
        {
            InitializeComponent();
        }

 
        private void btStopListening_Click(object sender, EventArgs e)
        {
            IsListening = false;
        }


        private async Task Listen()
        {
            var config = System.Configuration.ConfigurationManager.AppSettings;

            DateTime startCheckDate = DateTime.Now.AddMinutes(-(int)txtListenAgoMinutes.Value);
            await Task.Delay(1000);
            string password = config["emailPassword"];
            string mail = config["email"];

            string code = string.Empty;
            IsListening = true;
            DateTime statListenAt = DateTime.Now;

            while (IsListening)
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
                        txtCode.Text = code;
                        SaveTextToClipboard(code);
                        if (isConnectingToVM)
                        {
                            var dialogWindow = FindWindow(null, "");
                            var smsCodKeys = GetKeys(code);
                            SendKeys.SendWait(smsCodKeys);
                            SendEnter();
                            isConnectingToVM = false;
                        }
                        //WindowState = FormWindowState.Minimized;
                        //IsListening = false;
                    }
                }

                await Task.Delay(Convert.ToInt32(config["listenDelay"]));
                if ((DateTime.Now - statListenAt).Seconds >= 30)
                {
                    IsListening = false;
                }
            }
        }

        private async void btStartListening_Click(object sender, EventArgs e)
        {
            IsListening = true;
            await Listen();
        }

        private void btSavePasswordToClipboard_Click(object sender, EventArgs e)
        {
            txtCode.Text = string.Empty;
            var config = System.Configuration.ConfigurationManager.AppSettings;
            string password = config["amdarisPassword"];
            SaveTextToClipboard(password);
        }

        private async void btCurrentProjectPassordToClipboad_Click(object sender, EventArgs e)
        {
            isConnectingToVM = false;
            txtCode.Text = string.Empty;
            var config = System.Configuration.ConfigurationManager.AppSettings;
            string password = config["currentProjectPassword"];
            SaveTextToClipboard(password);
            await Listen();
        }

        private void SaveTextToClipboard(string text)
        {
            if (InvokeRequired)
                Invoke(() => { _saveTextToClipboard(text); });
            else
                _saveTextToClipboard(text);
        }

        private void _saveTextToClipboard(string text)
        {
            System.Windows.Forms.Clipboard.Clear();
            System.Windows.Forms.Clipboard.SetText(text);
        }

        private void btStartProcentiaVM_Click(object sender, EventArgs e)
        {
            OpenVM();
        }

        private void SendEnter()
        {
            SendKeys.SendWait("{ENTER}");
        }

        private void SendTab()
        {
            SendKeys.SendWait("{TAB}");
        }

        private void SendTabs(int tabNumber)
        {
            for (int i = 0; i < tabNumber; i++)
            {
                SendTab();
            }
        }

        private void StartRemoteDesctop()
        {
            _remoteDesctopProc = new Process();
            _remoteDesctopProc.StartInfo.FileName = "C:\\Users\\pavel.martiniuc\\AppData\\Local\\Apps\\Remote Desktop\\msrdcw.exe";
            _remoteDesctopProc.StartInfo.Arguments = "-n";
            _remoteDesctopProc.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            _remoteDesctopProc.Start();
        }

        private async void ConnectToVM_Click(object sender, EventArgs e)
        {
            OpenVM();
            await Task.Delay(4000);
            var config = System.Configuration.ConfigurationManager.AppSettings;
            string password = config["currentProjectPassword"];
            string passwordkeys = GetKeys(password);
            //System.Windows.Forms.Clipboard.Clear();
            //System.Windows.Forms.Clipboard.SetText(password);
            IntPtr dialogWindow = FindWindow(null, "");
            int tryCount = 0;
            while (tryCount < 10)
            {
                tryCount++;
                dialogWindow = FindWindow(null, "");
                if (dialogWindow != IntPtr.Zero)
                {
                    break;
                }
                await Task.Delay(200);
            }
            await Task.Delay(4000);
            if (dialogWindow == IntPtr.Zero)
            {
                return;
            }
            
            // Set password
            SendKeys.SendWait(passwordkeys);
            SendEnter();

            await Task.Delay(3000);
            dialogWindow = FindWindow(null, "");
            
            // Authorize by Sms
            SendTab();
            SendEnter();

            await Task.Delay(2000);
            dialogWindow = FindWindow(null, "");

            // Authorize by Sms
            SendTab();
            SendTab();
            SendEnter();
            isConnectingToVM = true;
            await Task.Delay(5000);
            await Listen();
        }

        private async void OpenVM()
        {
            IntPtr remoteDesctopWindow = FindWindow(null, "Remote Desktop");
            if (remoteDesctopWindow != IntPtr.Zero)
            {
                PostMessage(remoteDesctopWindow, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                await Task.Delay(300);
            
            }
            StartRemoteDesctop();
            remoteDesctopWindow = FindWindow(null, "Remote Desktop");
            if (remoteDesctopWindow == IntPtr.Zero)
            {
                int tryCount = 0;

                while (tryCount < 10)
                {
                    tryCount++;
                    remoteDesctopWindow = FindWindow(null, "Remote Desktop");
                    if (remoteDesctopWindow != IntPtr.Zero)
                    {
                        break;
                    }

                    await Task.Delay(200);
                }
            }
            SetForegroundWindow(remoteDesctopWindow);
            await Task.Delay(2000);
            SendTabs(7);
            SendEnter();
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

        private List<IntPtr> GetAllButtons(IntPtr parentHandle)
        {
            List<IntPtr> buttonHandles = new List<IntPtr>();

            // Callback function for each child window
            EnumChildProc callback = (hwnd, lParam) =>
            {
                // Get the class name of the child window
                StringBuilder className = new StringBuilder(256);
                GetClassName(hwnd, className, className.Capacity);

                // Check if it's a button
                if (className.ToString() == "Button")
                {
                    buttonHandles.Add(hwnd);
                }

                return true; // Continue enumeration
            };

            // Enumerate all child windows
            EnumChildWindows(parentHandle, callback, IntPtr.Zero);

            return buttonHandles;
        }

        private string GetControlClassName(IntPtr hWnd)
        {
            const int MaxClassNameLength = 256;
            StringBuilder className = new StringBuilder(MaxClassNameLength);

            // Call GetClassName
            int result = GetClassName(hWnd, className, className.Capacity);

            if (result > 0)
            {
                return className.ToString();
            }

            // If the result is 0, an error occurred
            return null;
        }

        public static IntPtr GetControlByName(IntPtr parentHandle, string windowName)
        {
            IntPtr controlHandle = IntPtr.Zero;
            while ((controlHandle = FindWindowEx(parentHandle, controlHandle, null, windowName)) != IntPtr.Zero)
            {
                // You can return the first match or continue searching
                return controlHandle;
            }
            return IntPtr.Zero;
        }
    }
}
