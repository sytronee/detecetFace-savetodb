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
using CommonModels;
using Npgsql;

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
            apiTimer.Start();

        }

        private static readonly HttpClient client = new HttpClient();
        private string sonKayitliVeriId = ""; // Aynı mesajı tekrar göstermemek için
        Dictionary<string,string> veriler = new Dictionary<string,string>();
        

        string connectingString = "server=localhost;port=5454;Database=detectedFace_db;user Id=postgres;password=123";
        void pushhdata()
        {
            var con = new NpgsqlConnection(connectingString);
            con.Open();
            string query = "INSERT INTO scanned_data (camera_id, detected_time, confidence, bbox_x, bbox_y, bbox_w, bbox_h) VALUES (@camid,@detectedTime, @conf, @x, @y, @w, @h)";

            var cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@camid", "camera-"+veriler["camid"]);
            cmd.Parameters.AddWithValue("@detectedTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@conf",  Convert.ToInt16(veriler["conf"]));
            cmd.Parameters.AddWithValue("@x", Convert.ToInt16(veriler["bbox_x"]));
            cmd.Parameters.AddWithValue("@y", Convert.ToInt16(veriler["bbox_y"]));
            cmd.Parameters.AddWithValue("@w", Convert.ToInt16(veriler["with"]));
            cmd.Parameters.AddWithValue("@h",Convert.ToInt16(veriler["height"]));
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
            apiTimer.Start();
        }

        private void pnl_ext_MouseEnter(object sender, EventArgs e) => pnl_ext.BackColor = Color.FromArgb(211, 163, 117);
        private void pnl_ext_MouseLeave(object sender, EventArgs e) => pnl_ext.BackColor = Color.FromArgb(201, 153, 107);
        private void pnl_ext_Click(object sender, EventArgs e) => Application.Exit();

        private void btn_scanFace_Click(object sender, EventArgs e)
        {
            apiTimer.Start();
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
                pctr_box_face.Image = null;
                richTextBox1.Text="";
                pythonProcess.Start();
                
                btn_scanFace.Enabled = false; // Script zaten çalışıyorsa tekrar basılmasın
                btn_scanFace.Text = "Scanning...";
                btn_submit.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Python başlatılamadı: " + ex.Message);
            }
        }
        private async void apiTimer_Tick(object sender, EventArgs e)
        {
            string url = "https://localhost:7110/api/detection/son-veri";

            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var veri = JsonConvert.DeserializeObject<DetectionData>(json);
                    veriler.Add("camid", veri.camera_id);
                    veriler.Add("conf", veri.confidence.ToString());
                   
                    veriler.Add("bbox_x", veri.bbox.x_degeri.ToString());
                    veriler.Add("bbox_y", veri.bbox.y_degeri.ToString());
                    veriler.Add("with", veri.bbox.width.ToString());
                    veriler.Add("height", veri.bbox.height.ToString());
                    // Yeni veri mi geldi kontrolü?
                    string id = veri.camera_id + veri.confidence.ToString();
                    if (id != sonKayitliVeriId)
                    {
                        sonKayitliVeriId = id;
                        
                        // Veriyi ekrana yazdır
                        this.Invoke(new Action(() => {
                            // Buraya kendi label ismini yaz
                            richTextBox1.Text = json.ToString();
                            lbl_conf.Text += " "+veri.confidence;
                            pctr_box_face.ImageLocation = Application.StartupPath + "/Snapshot.jpg";
                            
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                // API kapalıysa buraya düşer, uygulama donmaz
                System.Diagnostics.Debug.WriteLine("Hata: " + ex.Message);
            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            btn_savedb.Enabled = true;
            btn_scanFace.Enabled = true;
            btn_scanFace.Text = "Scan Face ";
        }
    }
}
