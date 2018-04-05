namespace AutoUpdate
{
    partial class AutoUpdate
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
            this.skinButton1 = new CCWin.SkinControl.SkinButton();
            this.progressBar = new CCWin.SkinControl.SkinProgressBar();
            this.skinPictureBox1 = new CCWin.SkinControl.SkinPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.skinPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // skinButton1
            // 
            this.skinButton1.BackColor = System.Drawing.Color.Transparent;
            this.skinButton1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton1.DownBack = null;
            this.skinButton1.Location = new System.Drawing.Point(348, 164);
            this.skinButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.skinButton1.MouseBack = null;
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = null;
            this.skinButton1.Size = new System.Drawing.Size(173, 42);
            this.skinButton1.TabIndex = 0;
            this.skinButton1.Text = "正在更新......";
            this.skinButton1.UseVisualStyleBackColor = false;
            this.skinButton1.Click += new System.EventHandler(this.skinButton1_Click);
            // 
            // progressBar
            // 
            this.progressBar.Back = null;
            this.progressBar.BackColor = System.Drawing.Color.Transparent;
            this.progressBar.BarBack = null;
            this.progressBar.BarRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.progressBar.ForeColor = System.Drawing.Color.Red;
            this.progressBar.Location = new System.Drawing.Point(218, 72);
            this.progressBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar.Name = "progressBar";
            this.progressBar.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.progressBar.Size = new System.Drawing.Size(468, 27);
            this.progressBar.TabIndex = 1;
            // 
            // skinPictureBox1
            // 
            this.skinPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.skinPictureBox1.ErrorImage = null;
            this.skinPictureBox1.Image = global::AutoUpdate.Properties.Resources.update;
            this.skinPictureBox1.InitialImage = null;
            this.skinPictureBox1.Location = new System.Drawing.Point(-4, 18);
            this.skinPictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.skinPictureBox1.Name = "skinPictureBox1";
            this.skinPictureBox1.Size = new System.Drawing.Size(216, 278);
            this.skinPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.skinPictureBox1.TabIndex = 2;
            this.skinPictureBox1.TabStop = false;
            // 
            // AutoUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(697, 315);
            this.Controls.Add(this.skinPictureBox1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.skinButton1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AutoUpdate";
            this.Text = "系统自动更新";
            ((System.ComponentModel.ISupportInitialize)(this.skinPictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinButton skinButton1;
        private CCWin.SkinControl.SkinProgressBar progressBar;
        private CCWin.SkinControl.SkinPictureBox skinPictureBox1;
    }
}

