using System.Windows.Forms;

namespace cspClient
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainPanel = new System.Windows.Forms.Panel();
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.btn_queryData = new System.Windows.Forms.Button();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.btn_sync = new System.Windows.Forms.Button();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.mainButton = new System.Windows.Forms.Button();
            this.loginout = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.skinMenuStrip1 = new CCWin.SkinControl.SkinMenuStrip();
            this.userMenuStrips = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStrip_changeConf = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSM = new System.Windows.Forms.ToolStripButton();
            this.toolStripSync = new System.Windows.Forms.ToolStripDropDownButton();
            this.menubtn_queryInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tools_aboutUs = new System.Windows.Forms.ToolStripButton();
            this.updateVersionBtn = new CCWin.SkinControl.SkinButton();
            this.ToolStrip_resetPwd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.MainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.skinMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.BackColor = System.Drawing.Color.Transparent;
            this.MainPanel.Controls.Add(this.skinLabel3);
            this.MainPanel.Controls.Add(this.btn_queryData);
            this.MainPanel.Controls.Add(this.skinLabel2);
            this.MainPanel.Controls.Add(this.btn_sync);
            this.MainPanel.Controls.Add(this.skinLabel1);
            this.MainPanel.Controls.Add(this.mainButton);
            this.MainPanel.ForeColor = System.Drawing.Color.Black;
            this.MainPanel.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MainPanel.Location = new System.Drawing.Point(1, 52);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Padding = new System.Windows.Forms.Padding(4);
            this.MainPanel.Size = new System.Drawing.Size(1381, 495);
            this.MainPanel.TabIndex = 0;
            // 
            // skinLabel3
            // 
            this.skinLabel3.AutoSize = true;
            this.skinLabel3.BackColor = System.Drawing.Color.White;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel3.ForeColor = System.Drawing.Color.Black;
            this.skinLabel3.Location = new System.Drawing.Point(297, 181);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(69, 20);
            this.skinLabel3.TabIndex = 5;
            this.skinLabel3.Text = "数据查询";
            // 
            // btn_queryData
            // 
            this.btn_queryData.BackColor = System.Drawing.Color.Transparent;
            this.btn_queryData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_queryData.FlatAppearance.BorderSize = 0;
            this.btn_queryData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_queryData.Location = new System.Drawing.Point(266, 56);
            this.btn_queryData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_queryData.Name = "btn_queryData";
            this.btn_queryData.Size = new System.Drawing.Size(143, 122);
            this.btn_queryData.TabIndex = 4;
            this.btn_queryData.UseVisualStyleBackColor = false;
            this.btn_queryData.Click += new System.EventHandler(this.btn_queryData_Click);
            // 
            // skinLabel2
            // 
            this.skinLabel2.AutoSize = true;
            this.skinLabel2.BackColor = System.Drawing.Color.White;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.ForeColor = System.Drawing.Color.Black;
            this.skinLabel2.Location = new System.Drawing.Point(512, 181);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(69, 20);
            this.skinLabel2.TabIndex = 3;
            this.skinLabel2.Text = "数据上传";
            // 
            // btn_sync
            // 
            this.btn_sync.BackColor = System.Drawing.Color.Transparent;
            this.btn_sync.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_sync.FlatAppearance.BorderSize = 0;
            this.btn_sync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_sync.Location = new System.Drawing.Point(484, 56);
            this.btn_sync.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_sync.Name = "btn_sync";
            this.btn_sync.Size = new System.Drawing.Size(143, 122);
            this.btn_sync.TabIndex = 2;
            this.btn_sync.UseVisualStyleBackColor = false;
            this.btn_sync.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // skinLabel1
            // 
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.White;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.ForeColor = System.Drawing.Color.Black;
            this.skinLabel1.Location = new System.Drawing.Point(92, 181);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(69, 20);
            this.skinLabel1.TabIndex = 1;
            this.skinLabel1.Text = "会务注册";
            // 
            // mainButton
            // 
            this.mainButton.BackColor = System.Drawing.Color.Transparent;
            this.mainButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mainButton.FlatAppearance.BorderSize = 0;
            this.mainButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mainButton.Location = new System.Drawing.Point(60, 56);
            this.mainButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mainButton.Name = "mainButton";
            this.mainButton.Size = new System.Drawing.Size(143, 122);
            this.mainButton.TabIndex = 0;
            this.mainButton.UseVisualStyleBackColor = true;
            this.mainButton.Click += new System.EventHandler(this.mainButton_Click_1);
            // 
            // loginout
            // 
            this.loginout.BackColor = System.Drawing.SystemColors.HotTrack;
            this.loginout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginout.FlatAppearance.BorderSize = 0;
            this.loginout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginout.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loginout.ForeColor = System.Drawing.Color.White;
            this.loginout.Location = new System.Drawing.Point(1259, 2);
            this.loginout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loginout.Name = "loginout";
            this.loginout.Size = new System.Drawing.Size(119, 41);
            this.loginout.TabIndex = 10000;
            this.loginout.Text = "注销";
            this.loginout.UseVisualStyleBackColor = false;
            this.loginout.Click += new System.EventHandler(this.loginout_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(1, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1381, 45);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 124F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel1.Controls.Add(this.skinMenuStrip1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.loginout, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.updateVersionBtn, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1381, 45);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // skinMenuStrip1
            // 
            this.skinMenuStrip1.Arrow = System.Drawing.Color.Black;
            this.skinMenuStrip1.Back = System.Drawing.Color.White;
            this.skinMenuStrip1.BackRadius = 4;
            this.skinMenuStrip1.BackRectangle = new System.Drawing.Rectangle(10, 10, 10, 10);
            this.skinMenuStrip1.Base = System.Drawing.SystemColors.HotTrack;
            this.skinMenuStrip1.BaseFore = System.Drawing.Color.White;
            this.skinMenuStrip1.BaseForeAnamorphosis = false;
            this.skinMenuStrip1.BaseForeAnamorphosisBorder = 4;
            this.skinMenuStrip1.BaseForeAnamorphosisColor = System.Drawing.Color.White;
            this.skinMenuStrip1.BaseHoverFore = System.Drawing.Color.White;
            this.skinMenuStrip1.BaseItemAnamorphosis = true;
            this.skinMenuStrip1.BaseItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.BaseItemBorderShow = true;
            this.skinMenuStrip1.BaseItemDown = ((System.Drawing.Image)(resources.GetObject("skinMenuStrip1.BaseItemDown")));
            this.skinMenuStrip1.BaseItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.BaseItemMouse = ((System.Drawing.Image)(resources.GetObject("skinMenuStrip1.BaseItemMouse")));
            this.skinMenuStrip1.BaseItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.BaseItemRadius = 4;
            this.skinMenuStrip1.BaseItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinMenuStrip1.BaseItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinMenuStrip1.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.skinMenuStrip1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinMenuStrip1.Fore = System.Drawing.Color.Black;
            this.skinMenuStrip1.HoverFore = System.Drawing.Color.White;
            this.skinMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.skinMenuStrip1.ItemAnamorphosis = true;
            this.skinMenuStrip1.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.ItemBorderShow = true;
            this.skinMenuStrip1.ItemHover = System.Drawing.SystemColors.GradientActiveCaption;
            this.skinMenuStrip1.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.ItemRadius = 4;
            this.skinMenuStrip1.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userMenuStrips});
            this.skinMenuStrip1.Location = new System.Drawing.Point(1056, 0);
            this.skinMenuStrip1.Name = "skinMenuStrip1";
            this.skinMenuStrip1.Padding = new System.Windows.Forms.Padding(4);
            this.skinMenuStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinMenuStrip1.Size = new System.Drawing.Size(200, 45);
            this.skinMenuStrip1.SkinAllColor = true;
            this.skinMenuStrip1.TabIndex = 2;
            this.skinMenuStrip1.Text = "skinMenuStrip1";
            this.skinMenuStrip1.TitleAnamorphosis = true;
            this.skinMenuStrip1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.skinMenuStrip1.TitleRadius = 4;
            this.skinMenuStrip1.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // userMenuStrips
            // 
            this.userMenuStrips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userMenuStrips.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStrip_changeConf});
            this.userMenuStrips.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userMenuStrips.ForeColor = System.Drawing.Color.Black;
            this.userMenuStrips.Name = "userMenuStrips";
            this.userMenuStrips.Size = new System.Drawing.Size(81, 37);
            this.userMenuStrips.Text = "用户名";
            // 
            // ToolStrip_changeConf
            // 
            this.ToolStrip_changeConf.Name = "ToolStrip_changeConf";
            this.ToolStrip_changeConf.Size = new System.Drawing.Size(164, 26);
            this.ToolStrip_changeConf.Text = "切换会议";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSM,
            this.toolStripSync,
            this.toolStripButton2,
            this.tools_aboutUs});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(932, 45);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(52, 42);
            this.toolStripButton1.Text = "首页";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSM
            // 
            this.toolStripSM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripSM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSM.Name = "toolStripSM";
            this.toolStripSM.Size = new System.Drawing.Size(90, 42);
            this.toolStripSM.Text = "会务注册";
            this.toolStripSM.ToolTipText = "会务注册";
            this.toolStripSM.Click += new System.EventHandler(this.toolStripSM_Click);
            // 
            // toolStripSync
            // 
            this.toolStripSync.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menubtn_queryInfo});
            this.toolStripSync.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripSync.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSync.Name = "toolStripSync";
            this.toolStripSync.Size = new System.Drawing.Size(100, 42);
            this.toolStripSync.Text = "数据查询";
            // 
            // menubtn_queryInfo
            // 
            this.menubtn_queryInfo.Name = "menubtn_queryInfo";
            this.menubtn_queryInfo.Size = new System.Drawing.Size(162, 28);
            this.menubtn_queryInfo.Text = "登记信息";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(90, 42);
            this.toolStripButton2.Text = "数据上传";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // tools_aboutUs
            // 
            this.tools_aboutUs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tools_aboutUs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tools_aboutUs.Name = "tools_aboutUs";
            this.tools_aboutUs.Size = new System.Drawing.Size(90, 42);
            this.tools_aboutUs.Text = "关于我们";
            this.tools_aboutUs.Visible = false;
            // 
            // updateVersionBtn
            // 
            this.updateVersionBtn.BackColor = System.Drawing.Color.Transparent;
            this.updateVersionBtn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.updateVersionBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updateVersionBtn.DownBack = null;
            this.updateVersionBtn.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.updateVersionBtn.Location = new System.Drawing.Point(935, 2);
            this.updateVersionBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.updateVersionBtn.MouseBack = null;
            this.updateVersionBtn.Name = "updateVersionBtn";
            this.updateVersionBtn.NormlBack = null;
            this.updateVersionBtn.Size = new System.Drawing.Size(118, 41);
            this.updateVersionBtn.TabIndex = 4;
            this.updateVersionBtn.Text = "更新版本";
            this.updateVersionBtn.UseVisualStyleBackColor = false;
            this.updateVersionBtn.Visible = false;
            this.updateVersionBtn.Click += new System.EventHandler(this.updateVersionBtn_Click);
            // 
            // ToolStrip_resetPwd
            // 
            this.ToolStrip_resetPwd.Name = "ToolStrip_resetPwd";
            this.ToolStrip_resetPwd.Size = new System.Drawing.Size(164, 26);
            this.ToolStrip_resetPwd.Text = "密码重置";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 6);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 6);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1384, 544);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MainPanel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "会务注册";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.skinMenuStrip1.ResumeLayout(false);
            this.skinMenuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion


        public System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button loginout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public CCWin.SkinControl.SkinLabel skinLabel1;
        private System.Windows.Forms.ToolStripMenuItem userMenuStrips;
        private System.Windows.Forms.ToolStripMenuItem ToolStrip_changeConf;
        private System.Windows.Forms.ToolStripMenuItem ToolStrip_resetPwd;
        public CCWin.SkinControl.SkinLabel skinLabel2;


        public System.Windows.Forms.Button mainButton;
        public System.Windows.Forms.Button btn_sync;
        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripSM;
        public System.Windows.Forms.ToolStripButton toolStripButton1;
        private CCWin.SkinControl.SkinButton updateVersionBtn;
        private ToolStripButton tools_aboutUs;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        //private Button btn_username;
        private ToolStripButton toolStripButton2;
        public Button btn_queryData;
        public CCWin.SkinControl.SkinLabel skinLabel3;
        private CCWin.SkinControl.SkinMenuStrip skinMenuStrip1;
        private ToolStripDropDownButton toolStripSync;
        private ToolStripMenuItem menubtn_queryInfo;
    }
}