namespace 抓取信息
{
    partial class frmNewEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewEdit));
            this.linkNew = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.txtDes = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKeys = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtCon = new WinHtmlControl.Editor();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // linkNew
            // 
            this.linkNew.AutoSize = true;
            this.linkNew.Location = new System.Drawing.Point(352, 147);
            this.linkNew.Name = "linkNew";
            this.linkNew.Size = new System.Drawing.Size(71, 12);
            this.linkNew.TabIndex = 36;
            this.linkNew.TabStop = true;
            this.linkNew.Text = "about:blank";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(305, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 35;
            this.label6.Text = "原网址";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(686, 37);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 34;
            this.btnNext.Text = "后一条";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(605, 37);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(75, 23);
            this.btnLast.TabIndex = 33;
            this.btnLast.Text = "前一条";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(686, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 32;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(605, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 31;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 30;
            this.label5.Text = "日期";
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(69, 171);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(230, 21);
            this.txtDate.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "来源";
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(69, 144);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(230, 21);
            this.txtFrom.TabIndex = 27;
            // 
            // txtDes
            // 
            this.txtDes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDes.Location = new System.Drawing.Point(69, 64);
            this.txtDes.Name = "txtDes";
            this.txtDes.Size = new System.Drawing.Size(723, 74);
            this.txtDes.TabIndex = 26;
            this.txtDes.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "介绍";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "关键字";
            // 
            // txtKeys
            // 
            this.txtKeys.Location = new System.Drawing.Point(69, 37);
            this.txtKeys.Name = "txtKeys";
            this.txtKeys.Size = new System.Drawing.Size(390, 21);
            this.txtKeys.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "标题";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(69, 10);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(390, 21);
            this.txtTitle.TabIndex = 21;
            // 
            // txtCon
            // 
            this.txtCon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCon.BodyHtml = null;
            this.txtCon.BodyText = null;
            this.txtCon.DocumentText = resources.GetString("txtCon.DocumentText");
            this.txtCon.EditorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtCon.EditorForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtCon.FontName = null;
            this.txtCon.FontSize = WinHtmlControl.FontSize.NA;
            this.txtCon.Location = new System.Drawing.Point(26, 198);
            this.txtCon.Name = "txtCon";
            this.txtCon.Size = new System.Drawing.Size(766, 359);
            this.txtCon.TabIndex = 37;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(405, 174);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 39;
            this.label7.Text = "作者";
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(450, 171);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(230, 21);
            this.txtAuthor.TabIndex = 38;
            // 
            // frmNewEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 582);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.linkNew);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.txtDes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKeys);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtCon);
            this.Name = "frmNewEdit";
            this.Text = "frmNewEdit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNewEdit_FormClosing);
            this.Load += new System.EventHandler(this.frmNewEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkNew;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtFrom;
        public System.Windows.Forms.RichTextBox txtDes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtKeys;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtTitle;
        public WinHtmlControl.Editor txtCon;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtAuthor;
    }
}