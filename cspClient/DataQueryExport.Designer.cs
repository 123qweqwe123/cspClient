namespace cspClient
{
    partial class DataQueryExport
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
            this.cb_v = new System.Windows.Forms.CheckBox();
            this.cb_l = new System.Windows.Forms.CheckBox();
            this.cb_w = new System.Windows.Forms.CheckBox();
            this.skinButton1 = new CCWin.SkinControl.SkinButton();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cb_v
            // 
            this.cb_v.AutoSize = true;
            this.cb_v.Checked = true;
            this.cb_v.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_v.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_v.Location = new System.Drawing.Point(103, 58);
            this.cb_v.Name = "cb_v";
            this.cb_v.Size = new System.Drawing.Size(71, 24);
            this.cb_v.TabIndex = 0;
            this.cb_v.Text = "来宾";
            this.cb_v.UseVisualStyleBackColor = true;
            // 
            // cb_l
            // 
            this.cb_l.AutoSize = true;
            this.cb_l.Checked = true;
            this.cb_l.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_l.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_l.Location = new System.Drawing.Point(262, 58);
            this.cb_l.Name = "cb_l";
            this.cb_l.Size = new System.Drawing.Size(71, 24);
            this.cb_l.TabIndex = 1;
            this.cb_l.Text = "讲师";
            this.cb_l.UseVisualStyleBackColor = true;
            // 
            // cb_w
            // 
            this.cb_w.AutoSize = true;
            this.cb_w.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_w.Location = new System.Drawing.Point(425, 58);
            this.cb_w.Name = "cb_w";
            this.cb_w.Size = new System.Drawing.Size(111, 24);
            this.cb_w.TabIndex = 2;
            this.cb_w.Text = "工作人员";
            this.cb_w.UseVisualStyleBackColor = true;
            // 
            // skinButton1
            // 
            this.skinButton1.BackColor = System.Drawing.Color.Transparent;
            this.skinButton1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton1.DownBack = null;
            this.skinButton1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinButton1.Location = new System.Drawing.Point(168, 128);
            this.skinButton1.MouseBack = null;
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = null;
            this.skinButton1.Size = new System.Drawing.Size(115, 35);
            this.skinButton1.TabIndex = 3;
            this.skinButton1.Text = "确定";
            this.skinButton1.UseVisualStyleBackColor = false;
            this.skinButton1.Click += new System.EventHandler(this.skinButton1_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(399, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 35);
            this.button1.TabIndex = 4;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DataQueryExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 188);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.skinButton1);
            this.Controls.Add(this.cb_w);
            this.Controls.Add(this.cb_l);
            this.Controls.Add(this.cb_v);
            this.Name = "DataQueryExport";
            this.Text = "选择导出类型";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox cb_v;
        public System.Windows.Forms.CheckBox cb_l;
        public System.Windows.Forms.CheckBox cb_w;
        private CCWin.SkinControl.SkinButton skinButton1;
        private System.Windows.Forms.Button button1;
    }
}