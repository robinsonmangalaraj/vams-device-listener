namespace VamsDeviceListener
{
    partial class frmHttpListener
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHttpListener));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblStartDt = new System.Windows.Forms.Label();
            this.pbLogo1 = new System.Windows.Forms.PictureBox();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.lnkBtnDetails = new System.Windows.Forms.LinkLabel();
            this.stb1 = new System.Windows.Forms.StatusBar();
            this.stbStatus = new System.Windows.Forms.StatusBarPanel();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stbStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "ZK Device Listener";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(129, 58);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(128, 24);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(125, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(128, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.Color.White;
            this.txtStatus.ForeColor = System.Drawing.Color.Black;
            this.txtStatus.HideSelection = false;
            this.txtStatus.Location = new System.Drawing.Point(8, 75);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtStatus.Size = new System.Drawing.Size(514, 219);
            this.txtStatus.TabIndex = 61;
            this.txtStatus.Visible = false;
            // 
            // lblStartDt
            // 
            this.lblStartDt.AutoSize = true;
            this.lblStartDt.Location = new System.Drawing.Point(10, 55);
            this.lblStartDt.Name = "lblStartDt";
            this.lblStartDt.Size = new System.Drawing.Size(97, 17);
            this.lblStartDt.TabIndex = 62;
            this.lblStartDt.Text = "Running since ";
            // 
            // pbLogo1
            // 
            this.pbLogo1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLogo1.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo1.Image")));
            this.pbLogo1.Location = new System.Drawing.Point(360, 304);
            this.pbLogo1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbLogo1.Name = "pbLogo1";
            this.pbLogo1.Size = new System.Drawing.Size(139, 22);
            this.pbLogo1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbLogo1.TabIndex = 63;
            this.pbLogo1.TabStop = false;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(10, 7);
            this.PictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(37, 39);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 59;
            this.PictureBox1.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.Green;
            this.Label1.Location = new System.Drawing.Point(50, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(310, 29);
            this.Label1.TabIndex = 58;
            this.Label1.Text = "VAMS ZK Device Listener";
            // 
            // lnkBtnDetails
            // 
            this.lnkBtnDetails.AutoSize = true;
            this.lnkBtnDetails.Location = new System.Drawing.Point(442, 20);
            this.lnkBtnDetails.Name = "lnkBtnDetails";
            this.lnkBtnDetails.Size = new System.Drawing.Size(85, 17);
            this.lnkBtnDetails.TabIndex = 60;
            this.lnkBtnDetails.TabStop = true;
            this.lnkBtnDetails.Text = "Show Details";
            this.lnkBtnDetails.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkBtnDetails_LinkClicked);
            // 
            // stb1
            // 
            this.stb1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stb1.Location = new System.Drawing.Point(0, 343);
            this.stb1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.stb1.Name = "stb1";
            this.stb1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stbStatus});
            this.stb1.ShowPanels = true;
            this.stb1.Size = new System.Drawing.Size(535, 27);
            this.stb1.TabIndex = 64;
            this.stb1.Text = "StatusBar1";
            // 
            // stbStatus
            // 
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Text = "stbStatus";
            this.stbStatus.Width = 250;
            // 
            // frmHttpListener
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 370);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.lblStartDt);
            this.Controls.Add(this.pbLogo1);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.lnkBtnDetails);
            this.Controls.Add(this.stb1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmHttpListener";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HTTP LISTENER";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmHttpListener_FormClosing);
            this.Load += new System.EventHandler(this.frmHttpListener_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stbStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        internal System.Windows.Forms.TextBox txtStatus;
        internal System.Windows.Forms.Label lblStartDt;
        internal System.Windows.Forms.PictureBox pbLogo1;
        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.LinkLabel lnkBtnDetails;
        internal System.Windows.Forms.StatusBar stb1;
        internal System.Windows.Forms.StatusBarPanel stbStatus;
    }
}

