namespace cspClient
{
    partial class DataQuery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_exportCSV = new CCWin.SkinControl.SkinButton();
            this.phoneNumber = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_query = new CCWin.SkinControl.SkinButton();
            this.skinDataGridView1 = new CCWin.SkinControl.SkinDataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pageControl1 = new cspClient.projCtl.PageControl();
            this.btn_reset = new CCWin.SkinControl.SkinButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skinDataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.skinDataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1398, 531);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btn_reset);
            this.panel1.Controls.Add(this.btn_exportCSV);
            this.panel1.Controls.Add(this.phoneNumber);
            this.panel1.Controls.Add(this.name);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_query);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1392, 86);
            this.panel1.TabIndex = 0;
            // 
            // btn_exportCSV
            // 
            this.btn_exportCSV.BackColor = System.Drawing.Color.Transparent;
            this.btn_exportCSV.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btn_exportCSV.DownBack = null;
            this.btn_exportCSV.Location = new System.Drawing.Point(20, 46);
            this.btn_exportCSV.MouseBack = null;
            this.btn_exportCSV.Name = "btn_exportCSV";
            this.btn_exportCSV.NormlBack = null;
            this.btn_exportCSV.Size = new System.Drawing.Size(101, 37);
            this.btn_exportCSV.TabIndex = 5;
            this.btn_exportCSV.Text = "生成CSV";
            this.btn_exportCSV.UseVisualStyleBackColor = false;
            this.btn_exportCSV.Click += new System.EventHandler(this.btn_exportCSV_Click);
            // 
            // phoneNumber
            // 
            this.phoneNumber.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.phoneNumber.Location = new System.Drawing.Point(388, 6);
            this.phoneNumber.Name = "phoneNumber";
            this.phoneNumber.Size = new System.Drawing.Size(193, 30);
            this.phoneNumber.TabIndex = 4;
            // 
            // name
            // 
            this.name.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.name.Location = new System.Drawing.Point(88, 6);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(193, 30);
            this.name.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(303, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "手机号:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "姓名:";
            // 
            // btn_query
            // 
            this.btn_query.BackColor = System.Drawing.Color.Transparent;
            this.btn_query.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btn_query.DownBack = null;
            this.btn_query.Location = new System.Drawing.Point(655, 9);
            this.btn_query.MouseBack = null;
            this.btn_query.Name = "btn_query";
            this.btn_query.NormlBack = null;
            this.btn_query.Size = new System.Drawing.Size(101, 37);
            this.btn_query.TabIndex = 0;
            this.btn_query.Text = "查询";
            this.btn_query.UseVisualStyleBackColor = false;
            this.btn_query.Click += new System.EventHandler(this.btn_query_Click);
            // 
            // skinDataGridView1
            // 
            this.skinDataGridView1.AllowUserToAddRows = false;
            this.skinDataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.skinDataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.skinDataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.skinDataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.skinDataGridView1.ColumnFont = null;
            this.skinDataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.skinDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.skinDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.skinDataGridView1.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.skinDataGridView1.DefaultCellStyle = dataGridViewCellStyle15;
            this.skinDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinDataGridView1.EnableHeadersVisualStyles = false;
            this.skinDataGridView1.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.skinDataGridView1.HeadFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinDataGridView1.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.skinDataGridView1.Location = new System.Drawing.Point(3, 95);
            this.skinDataGridView1.Name = "skinDataGridView1";
            this.skinDataGridView1.ReadOnly = true;
            this.skinDataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.skinDataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.skinDataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.skinDataGridView1.RowTemplate.Height = 23;
            this.skinDataGridView1.Size = new System.Drawing.Size(1392, 365);
            this.skinDataGridView1.TabIndex = 1;
            this.skinDataGridView1.TitleBack = null;
            this.skinDataGridView1.TitleBackColorBegin = System.Drawing.Color.White;
            this.skinDataGridView1.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pageControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 466);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1392, 44);
            this.panel2.TabIndex = 2;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.BackColor = System.Drawing.Color.White;
            this.pageControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(78)))), ((int)(((byte)(151)))));
            this.pageControl1.Location = new System.Drawing.Point(0, 0);
            this.pageControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 1;
            this.pageControl1.PageSize = 100;
            this.pageControl1.RecordCount = 0;
            this.pageControl1.Size = new System.Drawing.Size(953, 52);
            this.pageControl1.TabIndex = 0;
            // 
            // btn_reset
            // 
            this.btn_reset.BackColor = System.Drawing.Color.Transparent;
            this.btn_reset.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btn_reset.DownBack = null;
            this.btn_reset.Location = new System.Drawing.Point(799, 9);
            this.btn_reset.MouseBack = null;
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.NormlBack = null;
            this.btn_reset.Size = new System.Drawing.Size(101, 37);
            this.btn_reset.TabIndex = 6;
            this.btn_reset.Text = "重置";
            this.btn_reset.UseVisualStyleBackColor = false;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // DataQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1398, 531);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DataQuery";
            this.Text = "DataQuery";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skinDataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private CCWin.SkinControl.SkinButton btn_query;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox phoneNumber;
        private CCWin.SkinControl.SkinButton btn_exportCSV;
        private CCWin.SkinControl.SkinDataGridView skinDataGridView1;
        private System.Windows.Forms.Panel panel2;
        private projCtl.PageControl pageControl1;
        private CCWin.SkinControl.SkinButton btn_reset;
    }
}