namespace cspClient
{
    partial class IdNumAddForm
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
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.idtype_combox = new CCWin.SkinControl.SkinComboBox();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.idNum_textBox = new CCWin.SkinControl.SkinTextBox();
            this.btn_idReader = new CCWin.SkinControl.SkinButton();
            this.btn_sure = new CCWin.SkinControl.SkinButton();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.skinLabel4 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel5 = new CCWin.SkinControl.SkinLabel();
            this.uname = new CCWin.SkinControl.SkinTextBox();
            this.telNum = new CCWin.SkinControl.SkinTextBox();
            this.SuspendLayout();
            // 
            // skinLabel1
            // 
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(12, 30);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(254, 31);
            this.skinLabel1.TabIndex = 0;
            this.skinLabel1.Text = "是否更新添加证件号？";
            // 
            // idtype_combox
            // 
            this.idtype_combox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.idtype_combox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.idtype_combox.FormattingEnabled = true;
            this.idtype_combox.Location = new System.Drawing.Point(136, 160);
            this.idtype_combox.Name = "idtype_combox";
            this.idtype_combox.Size = new System.Drawing.Size(121, 31);
            this.idtype_combox.TabIndex = 1;
            this.idtype_combox.WaterText = "";
            // 
            // skinLabel2
            // 
            this.skinLabel2.AutoSize = true;
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(19, 160);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(97, 27);
            this.skinLabel2.TabIndex = 2;
            this.skinLabel2.Text = "证件类型:";
            // 
            // skinLabel3
            // 
            this.skinLabel3.AutoSize = true;
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel3.Location = new System.Drawing.Point(19, 199);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(97, 27);
            this.skinLabel3.TabIndex = 3;
            this.skinLabel3.Text = "证件号码:";
            // 
            // idNum_textBox
            // 
            this.idNum_textBox.BackColor = System.Drawing.Color.Transparent;
            this.idNum_textBox.DownBack = null;
            this.idNum_textBox.Icon = null;
            this.idNum_textBox.IconIsButton = false;
            this.idNum_textBox.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.idNum_textBox.IsPasswordChat = '\0';
            this.idNum_textBox.IsSystemPasswordChar = false;
            this.idNum_textBox.Lines = new string[0];
            this.idNum_textBox.Location = new System.Drawing.Point(136, 197);
            this.idNum_textBox.Margin = new System.Windows.Forms.Padding(0);
            this.idNum_textBox.MaxLength = 32767;
            this.idNum_textBox.MinimumSize = new System.Drawing.Size(28, 28);
            this.idNum_textBox.MouseBack = null;
            this.idNum_textBox.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.idNum_textBox.Multiline = true;
            this.idNum_textBox.Name = "idNum_textBox";
            this.idNum_textBox.NormlBack = null;
            this.idNum_textBox.Padding = new System.Windows.Forms.Padding(5);
            this.idNum_textBox.ReadOnly = false;
            this.idNum_textBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.idNum_textBox.Size = new System.Drawing.Size(440, 36);
            // 
            // 
            // 
            this.idNum_textBox.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.idNum_textBox.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.idNum_textBox.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.idNum_textBox.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.idNum_textBox.SkinTxt.Multiline = true;
            this.idNum_textBox.SkinTxt.Name = "BaseText";
            this.idNum_textBox.SkinTxt.Size = new System.Drawing.Size(430, 26);
            this.idNum_textBox.SkinTxt.TabIndex = 0;
            this.idNum_textBox.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.idNum_textBox.SkinTxt.WaterText = "";
            this.idNum_textBox.TabIndex = 4;
            this.idNum_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.idNum_textBox.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.idNum_textBox.WaterText = "";
            this.idNum_textBox.WordWrap = true;
            // 
            // btn_idReader
            // 
            this.btn_idReader.BackColor = System.Drawing.Color.Transparent;
            this.btn_idReader.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btn_idReader.DownBack = null;
            this.btn_idReader.Location = new System.Drawing.Point(592, 197);
            this.btn_idReader.MouseBack = null;
            this.btn_idReader.Name = "btn_idReader";
            this.btn_idReader.NormlBack = null;
            this.btn_idReader.Size = new System.Drawing.Size(141, 36);
            this.btn_idReader.TabIndex = 5;
            this.btn_idReader.Text = "读卡器读取";
            this.btn_idReader.UseVisualStyleBackColor = false;
            this.btn_idReader.Click += new System.EventHandler(this.btn_idReader_Click);
            // 
            // btn_sure
            // 
            this.btn_sure.BackColor = System.Drawing.Color.Transparent;
            this.btn_sure.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btn_sure.DownBack = null;
            this.btn_sure.Location = new System.Drawing.Point(191, 387);
            this.btn_sure.MouseBack = null;
            this.btn_sure.Name = "btn_sure";
            this.btn_sure.NormlBack = null;
            this.btn_sure.Size = new System.Drawing.Size(141, 36);
            this.btn_sure.TabIndex = 6;
            this.btn_sure.Text = "确定";
            this.btn_sure.UseVisualStyleBackColor = false;
            this.btn_sure.Click += new System.EventHandler(this.btn_sure_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(457, 387);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(140, 36);
            this.btn_cancel.TabIndex = 7;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // skinLabel4
            // 
            this.skinLabel4.AutoSize = true;
            this.skinLabel4.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel4.BorderColor = System.Drawing.Color.White;
            this.skinLabel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel4.Location = new System.Drawing.Point(59, 104);
            this.skinLabel4.Name = "skinLabel4";
            this.skinLabel4.Size = new System.Drawing.Size(57, 27);
            this.skinLabel4.TabIndex = 8;
            this.skinLabel4.Text = "姓名:";
            // 
            // skinLabel5
            // 
            this.skinLabel5.AutoSize = true;
            this.skinLabel5.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel5.BorderColor = System.Drawing.Color.White;
            this.skinLabel5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel5.Location = new System.Drawing.Point(370, 104);
            this.skinLabel5.Name = "skinLabel5";
            this.skinLabel5.Size = new System.Drawing.Size(77, 27);
            this.skinLabel5.TabIndex = 9;
            this.skinLabel5.Text = "手机号:";
            // 
            // uname
            // 
            this.uname.BackColor = System.Drawing.Color.Transparent;
            this.uname.DownBack = null;
            this.uname.Icon = null;
            this.uname.IconIsButton = false;
            this.uname.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.uname.IsPasswordChat = '\0';
            this.uname.IsSystemPasswordChar = false;
            this.uname.Lines = new string[0];
            this.uname.Location = new System.Drawing.Point(119, 104);
            this.uname.Margin = new System.Windows.Forms.Padding(0);
            this.uname.MaxLength = 32767;
            this.uname.MinimumSize = new System.Drawing.Size(28, 28);
            this.uname.MouseBack = null;
            this.uname.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.uname.Multiline = true;
            this.uname.Name = "uname";
            this.uname.NormlBack = null;
            this.uname.Padding = new System.Windows.Forms.Padding(5);
            this.uname.ReadOnly = true;
            this.uname.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.uname.Size = new System.Drawing.Size(225, 36);
            // 
            // 
            // 
            this.uname.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.uname.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uname.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.uname.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.uname.SkinTxt.Multiline = true;
            this.uname.SkinTxt.Name = "BaseText";
            this.uname.SkinTxt.ReadOnly = true;
            this.uname.SkinTxt.Size = new System.Drawing.Size(215, 26);
            this.uname.SkinTxt.TabIndex = 0;
            this.uname.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.uname.SkinTxt.WaterText = "";
            this.uname.TabIndex = 10;
            this.uname.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uname.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.uname.WaterText = "";
            this.uname.WordWrap = true;
            // 
            // telNum
            // 
            this.telNum.BackColor = System.Drawing.Color.Transparent;
            this.telNum.DownBack = null;
            this.telNum.Icon = null;
            this.telNum.IconIsButton = false;
            this.telNum.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.telNum.IsPasswordChat = '\0';
            this.telNum.IsSystemPasswordChar = false;
            this.telNum.Lines = new string[0];
            this.telNum.Location = new System.Drawing.Point(473, 104);
            this.telNum.Margin = new System.Windows.Forms.Padding(0);
            this.telNum.MaxLength = 32767;
            this.telNum.MinimumSize = new System.Drawing.Size(28, 28);
            this.telNum.MouseBack = null;
            this.telNum.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.telNum.Multiline = true;
            this.telNum.Name = "telNum";
            this.telNum.NormlBack = null;
            this.telNum.Padding = new System.Windows.Forms.Padding(5);
            this.telNum.ReadOnly = true;
            this.telNum.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.telNum.Size = new System.Drawing.Size(225, 36);
            // 
            // 
            // 
            this.telNum.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.telNum.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.telNum.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.telNum.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.telNum.SkinTxt.Multiline = true;
            this.telNum.SkinTxt.Name = "BaseText";
            this.telNum.SkinTxt.ReadOnly = true;
            this.telNum.SkinTxt.Size = new System.Drawing.Size(215, 26);
            this.telNum.SkinTxt.TabIndex = 0;
            this.telNum.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.telNum.SkinTxt.WaterText = "";
            this.telNum.TabIndex = 11;
            this.telNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.telNum.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.telNum.WaterText = "";
            this.telNum.WordWrap = true;
            // 
            // IdNumAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(769, 526);
            this.Controls.Add(this.telNum);
            this.Controls.Add(this.uname);
            this.Controls.Add(this.skinLabel5);
            this.Controls.Add(this.skinLabel4);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_sure);
            this.Controls.Add(this.btn_idReader);
            this.Controls.Add(this.idNum_textBox);
            this.Controls.Add(this.skinLabel3);
            this.Controls.Add(this.skinLabel2);
            this.Controls.Add(this.idtype_combox);
            this.Controls.Add(this.skinLabel1);
            this.Name = "IdNumAddForm";
            this.Text = "证件号添加";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinComboBox idtype_combox;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private CCWin.SkinControl.SkinTextBox idNum_textBox;
        private CCWin.SkinControl.SkinButton btn_idReader;
        private CCWin.SkinControl.SkinButton btn_sure;
        private System.Windows.Forms.Button btn_cancel;
        private CCWin.SkinControl.SkinLabel skinLabel4;
        private CCWin.SkinControl.SkinLabel skinLabel5;
        private CCWin.SkinControl.SkinTextBox uname;
        private CCWin.SkinControl.SkinTextBox telNum;
    }
}