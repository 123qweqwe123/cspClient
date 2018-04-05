namespace cspClient
{
    partial class LoginForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.skinProgressBar1 = new CCWin.SkinControl.SkinProgressBar();
            this.processInfo = new CCWin.SkinControl.SkinLabel();
            this.skinButton1 = new CCWin.SkinControl.SkinButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(196, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(196, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "密  码：";
            // 
            // username
            // 
            this.username.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.username.Location = new System.Drawing.Point(333, 92);
            this.username.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(215, 34);
            this.username.TabIndex = 3;
            // 
            // password
            // 
            this.password.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.password.Location = new System.Drawing.Point(333, 150);
            this.password.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(215, 34);
            this.password.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(459, 256);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 38);
            this.button2.TabIndex = 6;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // skinProgressBar1
            // 
            this.skinProgressBar1.Back = null;
            this.skinProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.skinProgressBar1.BarBack = null;
            this.skinProgressBar1.BarRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.skinProgressBar1.ForeColor = System.Drawing.Color.DarkGreen;
            this.skinProgressBar1.Location = new System.Drawing.Point(0, 351);
            this.skinProgressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.skinProgressBar1.Name = "skinProgressBar1";
            this.skinProgressBar1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinProgressBar1.Size = new System.Drawing.Size(761, 19);
            this.skinProgressBar1.TabIndex = 7;
            // 
            // processInfo
            // 
            this.processInfo.AutoSize = true;
            this.processInfo.BackColor = System.Drawing.Color.Transparent;
            this.processInfo.BorderColor = System.Drawing.Color.White;
            this.processInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.processInfo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.processInfo.Location = new System.Drawing.Point(0, 331);
            this.processInfo.Name = "processInfo";
            this.processInfo.Size = new System.Drawing.Size(111, 20);
            this.processInfo.TabIndex = 8;
            this.processInfo.Text = "程序开始启动...";
            // 
            // skinButton1
            // 
            this.skinButton1.BackColor = System.Drawing.Color.Transparent;
            this.skinButton1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton1.DownBack = null;
            this.skinButton1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinButton1.Location = new System.Drawing.Point(200, 256);
            this.skinButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.skinButton1.MouseBack = null;
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = null;
            this.skinButton1.Size = new System.Drawing.Size(109, 38);
            this.skinButton1.TabIndex = 5;
            this.skinButton1.Text = "登录";
            this.skinButton1.UseVisualStyleBackColor = false;
            this.skinButton1.Click += new System.EventHandler(this.skinButton1_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(761, 370);
            this.Controls.Add(this.processInfo);
            this.Controls.Add(this.skinProgressBar1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.skinButton1);
            this.Controls.Add(this.password);
            this.Controls.Add(this.username);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox password;
        private CCWin.SkinControl.SkinButton skinButton1;
        private System.Windows.Forms.Button button2;
        private CCWin.SkinControl.SkinProgressBar skinProgressBar1;
        private CCWin.SkinControl.SkinLabel processInfo;
    }
}