namespace detectFace_ui
{
    partial class frm_main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnl_ctrlbox = new System.Windows.Forms.Panel();
            this.lbl_tag = new System.Windows.Forms.Label();
            this.pnl_ext = new System.Windows.Forms.Panel();
            this.lbl_ext = new System.Windows.Forms.Label();
            this.pctr_box_face = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_conf = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.apiTimer = new System.Windows.Forms.Timer(this.components);
            this.lbl_status = new System.Windows.Forms.Label();
            this.btn_savedb = new detectFace_ui.customTools.RoneButton();
            this.btn_scanFace = new detectFace_ui.customTools.RoneButton();
            this.btn_submit = new detectFace_ui.customTools.RoneButton();
            this.pnl_ctrlbox.SuspendLayout();
            this.pnl_ext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctr_box_face)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_ctrlbox
            // 
            this.pnl_ctrlbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(153)))), ((int)(((byte)(107)))));
            this.pnl_ctrlbox.Controls.Add(this.lbl_tag);
            this.pnl_ctrlbox.Controls.Add(this.pnl_ext);
            this.pnl_ctrlbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ctrlbox.Location = new System.Drawing.Point(0, 0);
            this.pnl_ctrlbox.Name = "pnl_ctrlbox";
            this.pnl_ctrlbox.Size = new System.Drawing.Size(1200, 41);
            this.pnl_ctrlbox.TabIndex = 0;
            this.pnl_ctrlbox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnl_ctrlbox_MouseDown);
            // 
            // lbl_tag
            // 
            this.lbl_tag.AutoSize = true;
            this.lbl_tag.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_tag.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(79)))), ((int)(((byte)(74)))));
            this.lbl_tag.Location = new System.Drawing.Point(21, 6);
            this.lbl_tag.Name = "lbl_tag";
            this.lbl_tag.Size = new System.Drawing.Size(148, 29);
            this.lbl_tag.TabIndex = 1;
            this.lbl_tag.Text = "Detect Face ";
            // 
            // pnl_ext
            // 
            this.pnl_ext.Controls.Add(this.lbl_ext);
            this.pnl_ext.Location = new System.Drawing.Point(1148, 0);
            this.pnl_ext.Name = "pnl_ext";
            this.pnl_ext.Size = new System.Drawing.Size(40, 40);
            this.pnl_ext.TabIndex = 0;
            this.pnl_ext.Click += new System.EventHandler(this.pnl_ext_Click);
            this.pnl_ext.MouseEnter += new System.EventHandler(this.pnl_ext_MouseEnter);
            this.pnl_ext.MouseLeave += new System.EventHandler(this.pnl_ext_MouseLeave);
            // 
            // lbl_ext
            // 
            this.lbl_ext.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_ext.AutoSize = true;
            this.lbl_ext.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_ext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(79)))), ((int)(((byte)(74)))));
            this.lbl_ext.Location = new System.Drawing.Point(9, 9);
            this.lbl_ext.Name = "lbl_ext";
            this.lbl_ext.Size = new System.Drawing.Size(22, 22);
            this.lbl_ext.TabIndex = 0;
            this.lbl_ext.Text = "X";
            this.lbl_ext.Click += new System.EventHandler(this.pnl_ext_Click);
            this.lbl_ext.MouseEnter += new System.EventHandler(this.pnl_ext_MouseEnter);
            this.lbl_ext.MouseLeave += new System.EventHandler(this.pnl_ext_MouseLeave);
            // 
            // pctr_box_face
            // 
            this.pctr_box_face.Location = new System.Drawing.Point(15, 37);
            this.pctr_box_face.Name = "pctr_box_face";
            this.pctr_box_face.Size = new System.Drawing.Size(460, 412);
            this.pctr_box_face.TabIndex = 2;
            this.pctr_box_face.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_submit);
            this.groupBox1.Controls.Add(this.lbl_conf);
            this.groupBox1.Controls.Add(this.pctr_box_face);
            this.groupBox1.Location = new System.Drawing.Point(26, 119);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(526, 561);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detected Face";
            // 
            // lbl_conf
            // 
            this.lbl_conf.AutoSize = true;
            this.lbl_conf.Location = new System.Drawing.Point(11, 469);
            this.lbl_conf.Name = "lbl_conf";
            this.lbl_conf.Size = new System.Drawing.Size(139, 20);
            this.lbl_conf.TabIndex = 3;
            this.lbl_conf.Text = "Confidence Value:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.richTextBox1);
            this.groupBox2.Location = new System.Drawing.Point(610, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(539, 449);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Values";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(233)))), ((int)(((byte)(230)))));
            this.richTextBox1.Location = new System.Drawing.Point(30, 37);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(483, 392);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // apiTimer
            // 
            this.apiTimer.Interval = 500;
            this.apiTimer.Tick += new System.EventHandler(this.apiTimer_Tick);
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(767, 614);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(0, 20);
            this.lbl_status.TabIndex = 7;
            // 
            // btn_savedb
            // 
            this.btn_savedb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(118)))), ((int)(((byte)(109)))));
            this.btn_savedb.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(118)))), ((int)(((byte)(109)))));
            this.btn_savedb.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_savedb.BorderRadius = 0;
            this.btn_savedb.BorderSize = 0;
            this.btn_savedb.Enabled = false;
            this.btn_savedb.FlatAppearance.BorderSize = 0;
            this.btn_savedb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_savedb.ForeColor = System.Drawing.Color.White;
            this.btn_savedb.Location = new System.Drawing.Point(610, 588);
            this.btn_savedb.Name = "btn_savedb";
            this.btn_savedb.Size = new System.Drawing.Size(150, 40);
            this.btn_savedb.TabIndex = 6;
            this.btn_savedb.Text = "Save Database";
            this.btn_savedb.TextColor = System.Drawing.Color.White;
            this.btn_savedb.UseVisualStyleBackColor = false;
            this.btn_savedb.Click += new System.EventHandler(this.btn_savedb_Click);
            // 
            // btn_scanFace
            // 
            this.btn_scanFace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(118)))), ((int)(((byte)(109)))));
            this.btn_scanFace.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(118)))), ((int)(((byte)(109)))));
            this.btn_scanFace.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_scanFace.BorderRadius = 0;
            this.btn_scanFace.BorderSize = 0;
            this.btn_scanFace.FlatAppearance.BorderSize = 0;
            this.btn_scanFace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_scanFace.ForeColor = System.Drawing.Color.White;
            this.btn_scanFace.Location = new System.Drawing.Point(26, 57);
            this.btn_scanFace.Name = "btn_scanFace";
            this.btn_scanFace.Size = new System.Drawing.Size(150, 40);
            this.btn_scanFace.TabIndex = 4;
            this.btn_scanFace.Text = "Scan Face";
            this.btn_scanFace.TextColor = System.Drawing.Color.White;
            this.btn_scanFace.UseVisualStyleBackColor = false;
            this.btn_scanFace.Click += new System.EventHandler(this.btn_scanFace_Click);
            // 
            // btn_submit
            // 
            this.btn_submit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(118)))), ((int)(((byte)(109)))));
            this.btn_submit.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(118)))), ((int)(((byte)(109)))));
            this.btn_submit.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_submit.BorderRadius = 0;
            this.btn_submit.BorderSize = 0;
            this.btn_submit.Enabled = false;
            this.btn_submit.FlatAppearance.BorderSize = 0;
            this.btn_submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_submit.ForeColor = System.Drawing.Color.White;
            this.btn_submit.Location = new System.Drawing.Point(15, 506);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(150, 40);
            this.btn_submit.TabIndex = 1;
            this.btn_submit.Text = "Submit";
            this.btn_submit.TextColor = System.Drawing.Color.White;
            this.btn_submit.UseVisualStyleBackColor = false;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // frm_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(233)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.btn_savedb);
            this.Controls.Add(this.btn_scanFace);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnl_ctrlbox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frm_main";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.pnl_ctrlbox.ResumeLayout(false);
            this.pnl_ctrlbox.PerformLayout();
            this.pnl_ext.ResumeLayout(false);
            this.pnl_ext.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctr_box_face)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_ctrlbox;
        private System.Windows.Forms.Panel pnl_ext;
        private System.Windows.Forms.Label lbl_ext;
        private System.Windows.Forms.Label lbl_tag;
        private System.Windows.Forms.PictureBox pctr_box_face;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_conf;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private customTools.RoneButton btn_submit;
        private customTools.RoneButton btn_scanFace;
        private System.Windows.Forms.Timer apiTimer;
        private customTools.RoneButton btn_savedb;
        private System.Windows.Forms.Label lbl_status;
    }
}

