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
using Npgsql;
using commonModels;
using static commonModels.Class1;

namespace detectFace_ui
{
    public partial class frm_main : Form
    {
        //koseleri yuvarlama
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public frm_main()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private static readonly HttpClient client = new HttpClient();
        private string sonKayitliVeriId = "";
        Dictionary<string, string> veriler = new Dictionary<string, string>();
        private bool apiTimerCalisiyor = false;
        private DirectoryInfo pythonScriptKlasoru;
        string durum = "";
        // Windows'ta çalışan UI için:
        string connectingString = "Host=localhost;Database=CameraDb;Username=postgres;Password=123;";
        void pushhdata()
        {
            try
            {
                using (var con = new NpgsqlConnection(connectingString))
                {
                    con.Open();

                    // Veritabanı sütun isimlerine göre sorgu güncellendi
                    string query = "INSERT INTO scanneddata (cameraid, detectedtime, confidence, bbox_x, bbox_y, bbox_w, bbox_h) " +
                                   "VALUES (@camid, @detectedTime, @conf, @x, @y, @w, @h)";

                    using (var cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@camid", "camera-" + veriler["camid"]);
                        cmd.Parameters.AddWithValue("@detectedTime", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"));

                        // Sadece confidence double geliyorsa onu çeviriyoruz
                        cmd.Parameters.AddWithValue("@conf", Convert.ToDouble(veriler["conf"].Replace('.', ',')));

                        // Diğerleri zaten int, doğrudan kısa (short/Int16) olarak ekliyoruz
                        cmd.Parameters.AddWithValue("@x", Convert.ToInt16(veriler["bbox_x"]));
                        cmd.Parameters.AddWithValue("@y", Convert.ToInt16(veriler["bbox_y"]));
                        cmd.Parameters.AddWithValue("@w", Convert.ToInt16(veriler["with"]));
                        cmd.Parameters.AddWithValue("@h", Convert.ToInt16(veriler["height"]));

                        cmd.ExecuteNonQuery();
                    }
                }
                durum = "Saved successfully.";
            }
            catch (Exception ex)
            {
                durum = "An error has occurred: \n" + ex.Message;
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private DirectoryInfo PythonScriptKlasorunuBul()
        {
            DirectoryInfo dir = new DirectoryInfo(Application.StartupPath);

            while (dir != null)
            {
                string pythonScriptPath = Path.Combine(dir.FullName, "PythonScript");
                string detectFacePath = Path.Combine(pythonScriptPath, "detectFace.py");

                if (Directory.Exists(pythonScriptPath) && File.Exists(detectFacePath))
                    return new DirectoryInfo(pythonScriptPath);

                dir = dir.Parent;
            }

            throw new DirectoryNotFoundException("PythonScript klasörü bulunamadı. Başlangıç yolu: " + Application.StartupPath);
        }

        private DirectoryInfo PythonScriptKlasorunuGetir()
        {
            if (pythonScriptKlasoru == null || !pythonScriptKlasoru.Exists)
                pythonScriptKlasoru = PythonScriptKlasorunuBul();

            return pythonScriptKlasoru;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

        private void pnl_ctrlbox_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void pnl_ext_MouseEnter(object sender, EventArgs e) => pnl_ext.BackColor = Color.FromArgb(211, 163, 117);
        private void pnl_ext_MouseLeave(object sender, EventArgs e) => pnl_ext.BackColor = Color.FromArgb(201, 153, 107);
        private void pnl_ext_Click(object sender, EventArgs e) => Application.Exit();

        private void btn_scanFace_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process pythonProcess = new System.Diagnostics.Process();
                DirectoryInfo pythonScriptDir = PythonScriptKlasorunuGetir();

                string scriptPath = Path.Combine(
                    pythonScriptDir.FullName,
                    "detectFace.py"
                );

                string python = Path.Combine(
                    pythonScriptDir.FullName,
                    "venv",
                    "Scripts",
                    "python.exe"
                );

                if (!File.Exists(python))
                    throw new FileNotFoundException("Python exe bulunamadı.", python);

                if (!File.Exists(scriptPath))
                    throw new FileNotFoundException("detectFace.py bulunamadı.", scriptPath);

                pythonProcess.StartInfo.FileName = python;
                pythonProcess.StartInfo.Arguments = "\"" + scriptPath + "\"";
                pythonProcess.StartInfo.WorkingDirectory = pythonScriptDir.FullName;
                pythonProcess.StartInfo.UseShellExecute = false;
                pythonProcess.StartInfo.CreateNoWindow = false;

                if (pctr_box_face.Image != null)
                {
                    pctr_box_face.Image.Dispose();
                    pctr_box_face.Image = null;

                }
                if (btn_savedb.Enabled == true)
                {
                    btn_savedb.Enabled = false;
                    lbl_status.Text = "";
                }
                pythonProcess.Start();
                apiTimer.Start();
                btn_scanFace.Enabled = false;
                btn_scanFace.Text = "Scanning...";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Python başlatılamadı: " + ex.Message);
            }
        }

        private async void apiTimer_Tick(object sender, EventArgs e)
        {
            if (apiTimerCalisiyor)
                return;

            apiTimerCalisiyor = true;
            apiTimer.Stop();

            string url = "http://localhost:8080/api/detection/son-veri";

            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var veri = JsonConvert.DeserializeObject<DetectionData>(json);

                    if (veri == null || veri.bbox == null)
                        return;

                    veriler["camid"] = veri.camera_id;
                    veriler["conf"] = veri.confidence.ToString();
                    veriler["bbox_x"] = veri.bbox.x_degeri.ToString();
                    veriler["bbox_y"] = veri.bbox.y_degeri.ToString();
                    veriler["with"] = veri.bbox.width.ToString();
                    veriler["height"] = veri.bbox.height.ToString();

                    string id = veri.camera_id + veri.confidence.ToString();

                    if (id != sonKayitliVeriId)
                    {
                        sonKayitliVeriId = id;

                        this.Invoke(new Action(() =>
                        {
                            string snapshotPath = Path.Combine(PythonScriptKlasorunuGetir().FullName, "Snapshot.jpg");
                            btn_submit.Enabled = true;
                            richTextBox1.Text = json.ToString();
                            lbl_conf.Text = "Confidence Value: " + veri.confidence;
                            pctr_box_face.ImageLocation = snapshotPath;

                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Hata: " + ex.Message);
            }
            finally
            {
                apiTimerCalisiyor = false;
                apiTimer.Start();
            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            btn_savedb.Enabled = true;
            btn_scanFace.Enabled = true;
            btn_scanFace.Text = "Scan Face ";
        }

        private void btn_savedb_Click(object sender, EventArgs e)
        {
            btn_submit.Enabled = false;
            pushhdata();
            lbl_status.Text = durum;
            richTextBox1.Text = "";
            lbl_conf.Text = "Confidence Value: ";
        }

       
    }
}