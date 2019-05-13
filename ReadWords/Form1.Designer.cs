namespace ReadWords
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Button1 = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.openFile1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.checkCat = new System.Windows.Forms.CheckedListBox();
            this.btnTitleToPost = new System.Windows.Forms.Button();
            this.lblPostImg = new System.Windows.Forms.Label();
            this.btnPost = new System.Windows.Forms.Button();
            this.webPost = new System.Windows.Forms.WebBrowser();
            this.textPost = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textTitle = new System.Windows.Forms.TextBox();
            this.btnPreparePost = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textPathPost = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textNew = new System.Windows.Forms.TextBox();
            this.textOld = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textUname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textHost = new System.Windows.Forms.TextBox();
            this.listHost = new System.Windows.Forms.ListBox();
            this.listWidth = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textFileName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelDrop = new System.Windows.Forms.Label();
            this.btnRU = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textFolderAT = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtPostImage = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(12, 520);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(145, 41);
            this.Button1.TabIndex = 0;
            this.Button1.Text = "Start !!!";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(342, 81);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(281, 110);
            this.textBox.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(593, 520);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(145, 41);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // openFile1
            // 
            this.openFile1.FileName = "openFileDialog1";
            this.openFile1.Filter = "Файлы документов (*.txt; *.doc; *.docx; *.rtf)|*.txt;*.doc;*.docx;*.rtf";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 301);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(205, 44);
            this.button2.TabIndex = 5;
            this.button2.Text = "Чтение word";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 6);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(330, 290);
            this.listBox1.TabIndex = 9;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(342, 6);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(281, 69);
            this.textBox1.TabIndex = 10;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1178, 502);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtPostImage);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.checkCat);
            this.tabPage1.Controls.Add(this.btnTitleToPost);
            this.tabPage1.Controls.Add(this.btnPost);
            this.tabPage1.Controls.Add(this.webPost);
            this.tabPage1.Controls.Add(this.textPost);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.textTitle);
            this.tabPage1.Controls.Add(this.btnPreparePost);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.textPathPost);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.textNew);
            this.tabPage1.Controls.Add(this.textOld);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.textPath);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.textPassword);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.textUname);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textHost);
            this.tabPage1.Controls.Add(this.listHost);
            this.tabPage1.Controls.Add(this.listWidth);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textFileName);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.btnRU);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.listBox2);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.textFolderAT);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1170, 476);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ресайз/загрузка на FTP";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(272, 144);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 34;
            this.label9.Text = "Рубрики:";
            // 
            // checkCat
            // 
            this.checkCat.CheckOnClick = true;
            this.checkCat.FormattingEnabled = true;
            this.checkCat.Location = new System.Drawing.Point(269, 160);
            this.checkCat.Name = "checkCat";
            this.checkCat.Size = new System.Drawing.Size(223, 169);
            this.checkCat.TabIndex = 33;
            // 
            // btnTitleToPost
            // 
            this.btnTitleToPost.Location = new System.Drawing.Point(719, 23);
            this.btnTitleToPost.Name = "btnTitleToPost";
            this.btnTitleToPost.Size = new System.Drawing.Size(27, 23);
            this.btnTitleToPost.TabIndex = 32;
            this.btnTitleToPost.Text = "V";
            this.btnTitleToPost.UseVisualStyleBackColor = true;
            this.btnTitleToPost.Click += new System.EventHandler(this.BtnTitleToPost_Click);
            // 
            // lblPostImg
            // 
            this.lblPostImg.AutoSize = true;
            this.lblPostImg.Location = new System.Drawing.Point(201, 520);
            this.lblPostImg.Name = "lblPostImg";
            this.lblPostImg.Size = new System.Drawing.Size(58, 13);
            this.lblPostImg.TabIndex = 31;
            this.lblPostImg.Text = "post image";
            // 
            // btnPost
            // 
            this.btnPost.Enabled = false;
            this.btnPost.Location = new System.Drawing.Point(1072, 447);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(91, 23);
            this.btnPost.TabIndex = 30;
            this.btnPost.Text = "(3) post";
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.BtnPost_Click);
            // 
            // webPost
            // 
            this.webPost.Location = new System.Drawing.Point(724, 313);
            this.webPost.MinimumSize = new System.Drawing.Size(20, 20);
            this.webPost.Name = "webPost";
            this.webPost.Size = new System.Drawing.Size(439, 100);
            this.webPost.TabIndex = 29;
            // 
            // textPost
            // 
            this.textPost.Location = new System.Drawing.Point(719, 52);
            this.textPost.Multiline = true;
            this.textPost.Name = "textPost";
            this.textPost.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textPost.Size = new System.Drawing.Size(444, 255);
            this.textPost.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(716, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Title:";
            // 
            // textTitle
            // 
            this.textTitle.Location = new System.Drawing.Point(746, 6);
            this.textTitle.Multiline = true;
            this.textTitle.Name = "textTitle";
            this.textTitle.Size = new System.Drawing.Size(418, 40);
            this.textTitle.TabIndex = 26;
            // 
            // btnPreparePost
            // 
            this.btnPreparePost.Enabled = false;
            this.btnPreparePost.Location = new System.Drawing.Point(606, 447);
            this.btnPreparePost.Name = "btnPreparePost";
            this.btnPreparePost.Size = new System.Drawing.Size(103, 23);
            this.btnPreparePost.TabIndex = 12;
            this.btnPreparePost.Text = "(2) prepare Post";
            this.btnPreparePost.UseVisualStyleBackColor = true;
            this.btnPreparePost.Click += new System.EventHandler(this.BtnPreparePost_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(503, 400);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "remote Path to Upload (php):";
            // 
            // textPathPost
            // 
            this.textPathPost.Location = new System.Drawing.Point(500, 416);
            this.textPathPost.Name = "textPathPost";
            this.textPathPost.Size = new System.Drawing.Size(209, 20);
            this.textPathPost.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(271, 340);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "replace old/new:";
            // 
            // textNew
            // 
            this.textNew.Location = new System.Drawing.Point(268, 401);
            this.textNew.Multiline = true;
            this.textNew.Name = "textNew";
            this.textNew.Size = new System.Drawing.Size(224, 35);
            this.textNew.TabIndex = 22;
            // 
            // textOld
            // 
            this.textOld.Location = new System.Drawing.Point(268, 356);
            this.textOld.Multiline = true;
            this.textOld.Name = "textOld";
            this.textOld.Size = new System.Drawing.Size(224, 39);
            this.textOld.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(503, 340);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "remote Path (jpg):";
            // 
            // textPath
            // 
            this.textPath.Location = new System.Drawing.Point(500, 356);
            this.textPath.Multiline = true;
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(210, 39);
            this.textPath.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(503, 297);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "password:";
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(500, 313);
            this.textPassword.Name = "textPassword";
            this.textPassword.PasswordChar = '*';
            this.textPassword.Size = new System.Drawing.Size(210, 20);
            this.textPassword.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(503, 254);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "name:";
            // 
            // textUname
            // 
            this.textUname.Location = new System.Drawing.Point(500, 270);
            this.textUname.Name = "textUname";
            this.textUname.Size = new System.Drawing.Size(210, 20);
            this.textUname.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(503, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "host:";
            // 
            // textHost
            // 
            this.textHost.Location = new System.Drawing.Point(500, 227);
            this.textHost.Name = "textHost";
            this.textHost.Size = new System.Drawing.Size(210, 20);
            this.textHost.TabIndex = 13;
            // 
            // listHost
            // 
            this.listHost.FormattingEnabled = true;
            this.listHost.Location = new System.Drawing.Point(500, 134);
            this.listHost.Name = "listHost";
            this.listHost.Size = new System.Drawing.Size(210, 69);
            this.listHost.TabIndex = 12;
            this.listHost.SelectedIndexChanged += new System.EventHandler(this.ListHost_SelectedIndexChanged);
            // 
            // listWidth
            // 
            this.listWidth.FormattingEnabled = true;
            this.listWidth.Location = new System.Drawing.Point(500, 33);
            this.listWidth.Name = "listWidth";
            this.listWidth.Size = new System.Drawing.Size(210, 95);
            this.listWidth.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(496, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Имя файла:";
            // 
            // textFileName
            // 
            this.textFileName.Location = new System.Drawing.Point(569, 6);
            this.textFileName.Name = "textFileName";
            this.textFileName.Size = new System.Drawing.Size(141, 20);
            this.textFileName.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelDrop);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(488, 122);
            this.panel1.TabIndex = 7;
            this.panel1.Click += new System.EventHandler(this.Panel1_Click);
            this.panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.Panel1_DragDrop);
            this.panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.Panel1_DragEnter);
            this.panel1.DragLeave += new System.EventHandler(this.Panel1_DragLeave);
            // 
            // labelDrop
            // 
            this.labelDrop.Location = new System.Drawing.Point(1, 52);
            this.labelDrop.Name = "labelDrop";
            this.labelDrop.Size = new System.Drawing.Size(484, 14);
            this.labelDrop.TabIndex = 0;
            this.labelDrop.Text = "Перетащите сюда папку и/или файлы. Щелчок - выбор папки.";
            this.labelDrop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRU
            // 
            this.btnRU.Location = new System.Drawing.Point(389, 447);
            this.btnRU.Name = "btnRU";
            this.btnRU.Size = new System.Drawing.Size(103, 23);
            this.btnRU.TabIndex = 6;
            this.btnRU.Text = "(1) Resize+Upload";
            this.btnRU.UseVisualStyleBackColor = true;
            this.btnRU.Click += new System.EventHandler(this.BtnRU_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.DimGray;
            this.pictureBox1.Location = new System.Drawing.Point(6, 367);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(163, 103);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(6, 160);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(257, 134);
            this.listBox2.TabIndex = 4;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.ListBox2_SelectedIndexChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 297);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(256, 64);
            this.textBox2.TabIndex = 3;
            // 
            // textFolderAT
            // 
            this.textFolderAT.Location = new System.Drawing.Point(6, 134);
            this.textFolderAT.Name = "textFolderAT";
            this.textFolderAT.Size = new System.Drawing.Size(257, 20);
            this.textFolderAT.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.listBox1);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1170, 476);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Обработка Word";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1170, 476);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtPostImage
            // 
            this.txtPostImage.Location = new System.Drawing.Point(719, 436);
            this.txtPostImage.Multiline = true;
            this.txtPostImage.Name = "txtPostImage";
            this.txtPostImage.Size = new System.Drawing.Size(347, 33);
            this.txtPostImage.TabIndex = 35;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 573);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.lblPostImg);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.OpenFileDialog openFile1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textFolderAT;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnRU;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelDrop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textFileName;
        private System.Windows.Forms.ListBox listWidth;
        private System.Windows.Forms.ListBox listHost;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textUname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textHost;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textNew;
        private System.Windows.Forms.TextBox textOld;
        private System.Windows.Forms.Button btnPreparePost;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textPathPost;
        private System.Windows.Forms.TextBox textPost;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textTitle;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Label lblPostImg;
        private System.Windows.Forms.Button btnTitleToPost;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckedListBox checkCat;
        private System.Windows.Forms.WebBrowser webPost;
        private System.Windows.Forms.TextBox txtPostImage;
    }
}

