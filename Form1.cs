using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerpointWifiRemoteServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bunifuTextBox1.Text = GetLocalIPAddress()+ ":"+bunifuHTTPServerComponent1.port;
            RenderQR();
        }

        private void bunifuTextBox1_TextChange(object sender, EventArgs e)
        {
            RenderQR();
        }

        private void RenderQR()
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(bunifuTextBox1.Text.Trim(), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            img_barcode.Image = qrCode.GetGraphic(20);
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            MessageBox.Show("No network adapters with an IPv4 address in the system!");
            Application.Exit();
            return null;
        }

        private void bunifuHTTPServerComponent1_onDataPosted(object sender, EventArgs e)
        {
            Bunifu.Util.HTTP.BunifuHttpProcessor p = (Bunifu.Util.HTTP.BunifuHttpProcessor)sender;
            p.outputStream.Write(p.Data);
        }
    }
}
