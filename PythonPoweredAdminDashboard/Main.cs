using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json;

namespace PythonPoweredAdminDashboard
{
    
    public partial class Main : Form
    {
        Process process;
        public Main()
        {
            ProcessStartInfo pi = new ProcessStartInfo();
            pi.FileName=Application.StartupPath + "\\server\\Python27\\pythonw.exe";
            pi.Arguments = Application.StartupPath + "\\server\\wsgi\\main.py";
            pi.CreateNoWindow = true;
            InitializeComponent();
            process = Process.Start(pi);
            timer1.Interval = 2000;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            byte[] data = { };

            var jsdata = new JSONdata
            {
                username = "Eko",
                company_name = "CDI",
                company_address = "Maesan"
            };

            String postdata = JsonConvert.SerializeObject(jsdata);

            System.Text.Encoding a = System.Text.Encoding.UTF8;

            data = a.GetBytes(postdata);

            webBrowser1.Navigate("http://localhost:7465", "_self", data, "Content-Type: application/x-www-form-urlencoded" + Environment.NewLine);
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string url = e.Url.ToString();
            if (url.StartsWith("app:"))
            {
                string context = url.Split(':')[1];

                if (context.Equals("company_profile"))
                {
                    MessageBox.Show("You have a great company there!");
                }
                e.Cancel = true;
            }
        }
    }

    class JSONdata
    {
        public string username;
        public string company_name;
        public string company_address;
    }

}
