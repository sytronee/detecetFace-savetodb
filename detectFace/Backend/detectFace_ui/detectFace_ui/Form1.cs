using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.Json;
using Newtonsoft.Json;
using detectFace_ui.models;




namespace detectFace_ui
{
    public partial class frm_main : Form
    {
        //koseleri yuvarlama
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        public frm_main()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

        }




        //borderdan suruklemenin importları
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);

        //focus için gerekli 
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool FlashWindow(IntPtr hwnd, bool bInvert);
       

        private void pnl_ctrlbox_MouseDown(object sender, MouseEventArgs e)
        {
            //Borderlardan sürüklemek için 
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
            //
        }

        private void pnl_ext_MouseEnter(object sender, EventArgs e) => pnl_ext.BackColor = Color.FromArgb(211, 163, 117);
        private void pnl_ext_MouseLeave(object sender, EventArgs e) => pnl_ext.BackColor = Color.FromArgb(201, 153, 107);
        private void pnl_ext_Click(object sender, EventArgs e) => Application.Exit();

        private void btn_scanFace_Click(object sender, EventArgs e)
        {
            
            try
            {
                System.Diagnostics.Process pythonProcess = new System.Diagnostics.Process();
                DirectoryInfo dir = new DirectoryInfo(Application.StartupPath);

                dir = dir.Parent.Parent.Parent.Parent.Parent;

                string scriptPath = Path.Combine(
                    dir.FullName,
                    "PythonScript",
                    "detectFace.py"
                );
                string python= Path.Combine(
                    dir.FullName,
                    "PythonScript",
                    "venv",
                    "Scripts",
                    "python.exe"
                );
                pythonProcess.StartInfo.FileName= python;
                pythonProcess.StartInfo.Arguments = scriptPath;
                pythonProcess.StartInfo.UseShellExecute = true;
                pythonProcess.StartInfo.CreateNoWindow = false; // Görünür olması için false

                pythonProcess.Start();
                apiTimer.Start();
                btn_scanFace.Enabled = false; // Script zaten çalışıyorsa tekrar basılmasın
                btn_scanFace.Text = "Scanning...";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Python başlatılamadı: " + ex.Message);
            }
        }
      
    }
}
