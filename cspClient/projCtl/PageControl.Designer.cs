namespace cspClient.projCtl
{
    partial class PageControl
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
        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGo = new System.Windows.Forms.Button();
            this.txtPageNum = new System.Windows.Forms.TextBox();
            this.lblCurrentPage = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.lblSept = new System.Windows.Forms.Label();
            this.lblPageCount = new System.Windows.Forms.Label();
            this.txtPageSize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLast = new System.Windows.Forms.Button();
            this.linkFirst = new System.Windows.Forms.Button();
            this.linkPrevious = new System.Windows.Forms.Button();
            this.linkNext = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGo
            // 
            this.btnGo.BackColor = System.Drawing.Color.Transparent;
            this.btnGo.ForeColor = System.Drawing.Color.Black;
            this.btnGo.Location = new System.Drawing.Point(960, 16);
            this.btnGo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(68, 34);
            this.btnGo.TabIndex = 48;
            this.btnGo.Text = "跳转";
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // txtPageNum
            // 
            this.txtPageNum.Location = new System.Drawing.Point(902, 18);
            this.txtPageNum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPageNum.Name = "txtPageNum";
            this.txtPageNum.Size = new System.Drawing.Size(42, 28);
            this.txtPageNum.TabIndex = 46;
            this.txtPageNum.TextChanged += new System.EventHandler(this.txtPageNum_TextChanged);
            this.txtPageNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPageNum_KeyPress);
            // 
            // lblCurrentPage
            // 
            this.lblCurrentPage.AutoSize = true;
            this.lblCurrentPage.ForeColor = System.Drawing.Color.Red;
            this.lblCurrentPage.Location = new System.Drawing.Point(292, 23);
            this.lblCurrentPage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentPage.Name = "lblCurrentPage";
            this.lblCurrentPage.Size = new System.Drawing.Size(17, 18);
            this.lblCurrentPage.TabIndex = 49;
            this.lblCurrentPage.Text = "1";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.ForeColor = System.Drawing.Color.Red;
            this.lblTotalCount.Location = new System.Drawing.Point(420, 25);
            this.lblTotalCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(35, 18);
            this.lblTotalCount.TabIndex = 51;
            this.lblTotalCount.Text = "100";
            // 
            // lblSept
            // 
            this.lblSept.AutoSize = true;
            this.lblSept.Location = new System.Drawing.Point(318, 23);
            this.lblSept.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSept.Name = "lblSept";
            this.lblSept.Size = new System.Drawing.Size(17, 18);
            this.lblSept.TabIndex = 56;
            this.lblSept.Text = "/";
            // 
            // lblPageCount
            // 
            this.lblPageCount.AutoSize = true;
            this.lblPageCount.ForeColor = System.Drawing.Color.Red;
            this.lblPageCount.Location = new System.Drawing.Point(344, 24);
            this.lblPageCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPageCount.Name = "lblPageCount";
            this.lblPageCount.Size = new System.Drawing.Size(17, 18);
            this.lblPageCount.TabIndex = 57;
            this.lblPageCount.Text = "1";
            // 
            // txtPageSize
            // 
            this.txtPageSize.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtPageSize.Location = new System.Drawing.Point(81, 19);
            this.txtPageSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Size = new System.Drawing.Size(46, 28);
            this.txtPageSize.TabIndex = 46;
            this.txtPageSize.Text = "100";
            this.txtPageSize.TextChanged += new System.EventHandler(this.txtPageSize_TextChanged);
            this.txtPageSize.Leave += new System.EventHandler(this.txtPageSize_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(462, 24);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 21);
            this.label5.TabIndex = 249;
            this.label5.Text = "条记录";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(146, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 21);
            this.label3.TabIndex = 246;
            this.label3.Text = "条   当前页：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(22, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 21);
            this.label2.TabIndex = 245;
            this.label2.Text = "每页";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(386, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 21);
            this.label1.TabIndex = 244;
            this.label1.Text = "共";
            // 
            // linkLast
            // 
            this.linkLast.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLast.Location = new System.Drawing.Point(819, 17);
            this.linkLast.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.linkLast.Name = "linkLast";
            this.linkLast.Size = new System.Drawing.Size(66, 34);
            this.linkLast.TabIndex = 243;
            this.linkLast.Text = ">>|";
            this.linkLast.UseVisualStyleBackColor = true;
            this.linkLast.Click += new System.EventHandler(this.linkLast_Click);
            // 
            // linkFirst
            // 
            this.linkFirst.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkFirst.Location = new System.Drawing.Point(558, 17);
            this.linkFirst.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.linkFirst.Name = "linkFirst";
            this.linkFirst.Size = new System.Drawing.Size(66, 34);
            this.linkFirst.TabIndex = 242;
            this.linkFirst.Text = "|<<";
            this.linkFirst.UseVisualStyleBackColor = true;
            this.linkFirst.Click += new System.EventHandler(this.linkFirst_Click);
            // 
            // linkPrevious
            // 
            this.linkPrevious.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkPrevious.Location = new System.Drawing.Point(652, 17);
            this.linkPrevious.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.linkPrevious.Name = "linkPrevious";
            this.linkPrevious.Size = new System.Drawing.Size(51, 34);
            this.linkPrevious.TabIndex = 241;
            this.linkPrevious.Text = "<";
            this.linkPrevious.UseVisualStyleBackColor = true;
            this.linkPrevious.Click += new System.EventHandler(this.linkPrevious_Click);
            // 
            // linkNext
            // 
            this.linkNext.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkNext.Location = new System.Drawing.Point(736, 17);
            this.linkNext.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.linkNext.Name = "linkNext";
            this.linkNext.Size = new System.Drawing.Size(51, 34);
            this.linkNext.TabIndex = 240;
            this.linkNext.Text = ">";
            this.linkNext.UseVisualStyleBackColor = true;
            this.linkNext.Click += new System.EventHandler(this.linkNext_Click);
            // 
            // PageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkLast);
            this.Controls.Add(this.linkFirst);
            this.Controls.Add(this.linkPrevious);
            this.Controls.Add(this.linkNext);
            this.Controls.Add(this.lblPageCount);
            this.Controls.Add(this.lblSept);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.lblCurrentPage);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.txtPageSize);
            this.Controls.Add(this.txtPageNum);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(78)))), ((int)(((byte)(151)))));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "PageControl";
            this.Size = new System.Drawing.Size(1046, 76);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.TextBox txtPageNum;
        private System.Windows.Forms.Label lblCurrentPage;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label lblSept;
        private System.Windows.Forms.Label lblPageCount;
        private System.Windows.Forms.TextBox txtPageSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button linkLast;
        private System.Windows.Forms.Button linkFirst;
        private System.Windows.Forms.Button linkPrevious;
        private System.Windows.Forms.Button linkNext;
    }
}
