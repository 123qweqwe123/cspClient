namespace cspClient
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.btnSetPrint = new CCWin.SkinControl.SkinButton();
            this.btnPrePrint = new CCWin.SkinControl.SkinButton();
            this.btnPrient = new CCWin.SkinControl.SkinButton();
            this.qrCodeGraphicControl1 = new Gma.QrCodeNet.Encoding.Windows.Forms.QrCodeGraphicControl();
            this.qrCodeImgControl1 = new Gma.QrCodeNet.Encoding.Windows.Forms.QrCodeImgControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.skinButton1 = new CCWin.SkinControl.SkinButton();
            this.label1 = new System.Windows.Forms.Label();
            this.skinComboBox1 = new CCWin.SkinControl.SkinComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.skinButton2 = new CCWin.SkinControl.SkinButton();
            ((System.ComponentModel.ISupportInitialize)(this.qrCodeImgControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // btnSetPrint
            // 
            this.btnSetPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnSetPrint.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnSetPrint.DownBack = null;
            this.btnSetPrint.Location = new System.Drawing.Point(216, 425);
            this.btnSetPrint.MouseBack = null;
            this.btnSetPrint.Name = "btnSetPrint";
            this.btnSetPrint.NormlBack = null;
            this.btnSetPrint.Size = new System.Drawing.Size(95, 35);
            this.btnSetPrint.TabIndex = 0;
            this.btnSetPrint.Text = "打印设置";
            this.btnSetPrint.UseVisualStyleBackColor = false;
            this.btnSetPrint.Click += new System.EventHandler(this.btnSetPrint_Click);
            // 
            // btnPrePrint
            // 
            this.btnPrePrint.BackColor = System.Drawing.Color.Transparent;
            this.btnPrePrint.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnPrePrint.DownBack = null;
            this.btnPrePrint.Location = new System.Drawing.Point(362, 425);
            this.btnPrePrint.MouseBack = null;
            this.btnPrePrint.Name = "btnPrePrint";
            this.btnPrePrint.NormlBack = null;
            this.btnPrePrint.Size = new System.Drawing.Size(95, 35);
            this.btnPrePrint.TabIndex = 1;
            this.btnPrePrint.Text = "打印预览";
            this.btnPrePrint.UseVisualStyleBackColor = false;
            this.btnPrePrint.Click += new System.EventHandler(this.btnPrePrint_Click);
            // 
            // btnPrient
            // 
            this.btnPrient.BackColor = System.Drawing.Color.Transparent;
            this.btnPrient.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnPrient.DownBack = null;
            this.btnPrient.Location = new System.Drawing.Point(515, 425);
            this.btnPrient.MouseBack = null;
            this.btnPrient.Name = "btnPrient";
            this.btnPrient.NormlBack = null;
            this.btnPrient.Size = new System.Drawing.Size(95, 35);
            this.btnPrient.TabIndex = 2;
            this.btnPrient.Text = "打印";
            this.btnPrient.UseVisualStyleBackColor = false;
            this.btnPrient.Click += new System.EventHandler(this.btnPrient_Click);
            // 
            // qrCodeGraphicControl1
            // 
            this.qrCodeGraphicControl1.ErrorCorrectLevel = Gma.QrCodeNet.Encoding.ErrorCorrectionLevel.M;
            this.qrCodeGraphicControl1.Location = new System.Drawing.Point(252, 208);
            this.qrCodeGraphicControl1.Name = "qrCodeGraphicControl1";
            this.qrCodeGraphicControl1.QuietZoneModule = Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules.Two;
            this.qrCodeGraphicControl1.Size = new System.Drawing.Size(193, 179);
            this.qrCodeGraphicControl1.TabIndex = 3;
            // 
            // qrCodeImgControl1
            // 
            this.qrCodeImgControl1.ErrorCorrectLevel = Gma.QrCodeNet.Encoding.ErrorCorrectionLevel.M;
            this.qrCodeImgControl1.Image = ((System.Drawing.Image)(resources.GetObject("qrCodeImgControl1.Image")));
            this.qrCodeImgControl1.Location = new System.Drawing.Point(1080, 208);
            this.qrCodeImgControl1.Name = "qrCodeImgControl1";
            this.qrCodeImgControl1.QuietZoneModule = Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules.Two;
            this.qrCodeImgControl1.Size = new System.Drawing.Size(279, 227);
            this.qrCodeImgControl1.TabIndex = 4;
            this.qrCodeImgControl1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(252, 62);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(305, 25);
            this.textBox1.TabIndex = 5;
            // 
            // skinButton1
            // 
            this.skinButton1.BackColor = System.Drawing.Color.Transparent;
            this.skinButton1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton1.DownBack = null;
            this.skinButton1.Location = new System.Drawing.Point(1204, 156);
            this.skinButton1.MouseBack = null;
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = null;
            this.skinButton1.Size = new System.Drawing.Size(95, 35);
            this.skinButton1.TabIndex = 6;
            this.skinButton1.Text = "测试";
            this.skinButton1.UseVisualStyleBackColor = false;
            this.skinButton1.Click += new System.EventHandler(this.skinButton1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(97, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "登记身份证号：";
            // 
            // skinComboBox1
            // 
            this.skinComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.skinComboBox1.FormattingEnabled = true;
            this.skinComboBox1.Items.AddRange(new object[] {
            "会议1",
            "会议2",
            "会议3"});
            this.skinComboBox1.Location = new System.Drawing.Point(252, 115);
            this.skinComboBox1.Name = "skinComboBox1";
            this.skinComboBox1.Size = new System.Drawing.Size(121, 26);
            this.skinComboBox1.TabIndex = 8;
            this.skinComboBox1.WaterText = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(97, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "参与会议选择：";
            // 
            // skinButton2
            // 
            this.skinButton2.BackColor = System.Drawing.Color.Transparent;
            this.skinButton2.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton2.DownBack = null;
            this.skinButton2.Location = new System.Drawing.Point(839, 27);
            this.skinButton2.MouseBack = null;
            this.skinButton2.Name = "skinButton2";
            this.skinButton2.NormlBack = null;
            this.skinButton2.Size = new System.Drawing.Size(95, 35);
            this.skinButton2.TabIndex = 10;
            this.skinButton2.Text = "测试";
            this.skinButton2.UseVisualStyleBackColor = false;
            this.skinButton2.Click += new System.EventHandler(this.skinButton2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1357, 534);
            this.Controls.Add(this.skinButton2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.skinComboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.skinButton1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.qrCodeImgControl1);
            this.Controls.Add(this.qrCodeGraphicControl1);
            this.Controls.Add(this.btnPrient);
            this.Controls.Add(this.btnPrePrint);
            this.Controls.Add(this.btnSetPrint);
            this.Name = "Form1";
            this.Text = "会议签到";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.qrCodeImgControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private CCWin.SkinControl.SkinButton btnSetPrint;
        private CCWin.SkinControl.SkinButton btnPrePrint;
        private CCWin.SkinControl.SkinButton btnPrient;
        private Gma.QrCodeNet.Encoding.Windows.Forms.QrCodeGraphicControl qrCodeGraphicControl1;
        private Gma.QrCodeNet.Encoding.Windows.Forms.QrCodeImgControl qrCodeImgControl1;
        private System.Windows.Forms.TextBox textBox1;
        private CCWin.SkinControl.SkinButton skinButton1;
        private System.Windows.Forms.Label label1;
        private CCWin.SkinControl.SkinComboBox skinComboBox1;
        private System.Windows.Forms.Label label2;
        private CCWin.SkinControl.SkinButton skinButton2;
    }
}

