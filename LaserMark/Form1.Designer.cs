
namespace LaserMark
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Model_config_button = new System.Windows.Forms.Button();
            this.Model_send_button = new System.Windows.Forms.Button();
            this.Model_delete_button = new System.Windows.Forms.Button();
            this.textBox_runcard = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Model = new System.Windows.Forms.TextBox();
            this.Exit_button = new System.Windows.Forms.Button();
            this.labelL1 = new System.Windows.Forms.Label();
            this.labelL2 = new System.Windows.Forms.Label();
            this.labelL3 = new System.Windows.Forms.Label();
            this.labelL4 = new System.Windows.Forms.Label();
            this.labelL5 = new System.Windows.Forms.Label();
            this.labelL6 = new System.Windows.Forms.Label();
            this.labelL10 = new System.Windows.Forms.Label();
            this.labelL11 = new System.Windows.Forms.Label();
            this.labelM1 = new System.Windows.Forms.Label();
            this.labelM2 = new System.Windows.Forms.Label();
            this.labelM3 = new System.Windows.Forms.Label();
            this.labelM4 = new System.Windows.Forms.Label();
            this.labelM5 = new System.Windows.Forms.Label();
            this.labelM6 = new System.Windows.Forms.Label();
            this.labelM10 = new System.Windows.Forms.Label();
            this.labelM11 = new System.Windows.Forms.Label();
            this.Language_button = new System.Windows.Forms.Button();
            this.label_system = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelM7 = new System.Windows.Forms.Label();
            this.labelL7 = new System.Windows.Forms.Label();
            this.labelM8 = new System.Windows.Forms.Label();
            this.labelL8 = new System.Windows.Forms.Label();
            this.labelM9 = new System.Windows.Forms.Label();
            this.labelL9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.IntegralHeight = false;
            this.comboBox1.Location = new System.Drawing.Point(15, 160);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(297, 34);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(15, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "Runcard";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Model_config_button
            // 
            this.Model_config_button.Font = new System.Drawing.Font("微軟正黑體", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Model_config_button.Location = new System.Drawing.Point(15, 280);
            this.Model_config_button.Margin = new System.Windows.Forms.Padding(2);
            this.Model_config_button.Name = "Model_config_button";
            this.Model_config_button.Size = new System.Drawing.Size(297, 35);
            this.Model_config_button.TabIndex = 2;
            this.Model_config_button.Text = "品別參數設定";
            this.Model_config_button.UseVisualStyleBackColor = true;
            this.Model_config_button.Click += new System.EventHandler(this.Model_config_button_Click);
            // 
            // Model_send_button
            // 
            this.Model_send_button.Font = new System.Drawing.Font("微軟正黑體", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Model_send_button.Location = new System.Drawing.Point(15, 240);
            this.Model_send_button.Margin = new System.Windows.Forms.Padding(0);
            this.Model_send_button.Name = "Model_send_button";
            this.Model_send_button.Size = new System.Drawing.Size(297, 35);
            this.Model_send_button.TabIndex = 3;
            this.Model_send_button.Text = "傳送刻印參數";
            this.Model_send_button.UseVisualStyleBackColor = true;
            this.Model_send_button.Click += new System.EventHandler(this.Model_send_button_Click);
            // 
            // Model_delete_button
            // 
            this.Model_delete_button.Font = new System.Drawing.Font("微軟正黑體", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Model_delete_button.Location = new System.Drawing.Point(15, 320);
            this.Model_delete_button.Margin = new System.Windows.Forms.Padding(2);
            this.Model_delete_button.Name = "Model_delete_button";
            this.Model_delete_button.Size = new System.Drawing.Size(297, 35);
            this.Model_delete_button.TabIndex = 5;
            this.Model_delete_button.Text = "刪除品別參數";
            this.Model_delete_button.UseVisualStyleBackColor = true;
            this.Model_delete_button.Click += new System.EventHandler(this.Model_delete_button_Click);
            // 
            // textBox_runcard
            // 
            this.textBox_runcard.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_runcard.Font = new System.Drawing.Font("微軟正黑體", 15F, System.Drawing.FontStyle.Bold);
            this.textBox_runcard.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_runcard.Location = new System.Drawing.Point(15, 30);
            this.textBox_runcard.MaxLength = 16;
            this.textBox_runcard.Name = "textBox_runcard";
            this.textBox_runcard.Size = new System.Drawing.Size(297, 34);
            this.textBox_runcard.TabIndex = 6;
            this.textBox_runcard.TabStop = false;
            this.textBox_runcard.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_runcard_KeyPress);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(15, 130);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(297, 35);
            this.label3.TabIndex = 8;
            this.label3.Text = "品別清單";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(15, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(297, 35);
            this.label2.TabIndex = 9;
            this.label2.Text = "品別";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_Model
            // 
            this.textBox_Model.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox_Model.Font = new System.Drawing.Font("微軟正黑體", 15F, System.Drawing.FontStyle.Bold);
            this.textBox_Model.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_Model.Location = new System.Drawing.Point(15, 95);
            this.textBox_Model.Name = "textBox_Model";
            this.textBox_Model.ReadOnly = true;
            this.textBox_Model.Size = new System.Drawing.Size(297, 34);
            this.textBox_Model.TabIndex = 10;
            // 
            // Exit_button
            // 
            this.Exit_button.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold);
            this.Exit_button.Location = new System.Drawing.Point(15, 647);
            this.Exit_button.Margin = new System.Windows.Forms.Padding(2);
            this.Exit_button.Name = "Exit_button";
            this.Exit_button.Size = new System.Drawing.Size(204, 35);
            this.Exit_button.TabIndex = 12;
            this.Exit_button.Text = "離開";
            this.Exit_button.UseVisualStyleBackColor = true;
            this.Exit_button.Click += new System.EventHandler(this.Exit_button_Click);
            // 
            // labelL1
            // 
            this.labelL1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelL1.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelL1.Location = new System.Drawing.Point(15, 366);
            this.labelL1.Name = "labelL1";
            this.labelL1.Size = new System.Drawing.Size(91, 20);
            this.labelL1.TabIndex = 13;
            this.labelL1.Text = "第一行文字";
            this.labelL1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelL2
            // 
            this.labelL2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelL2.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelL2.Location = new System.Drawing.Point(15, 391);
            this.labelL2.Name = "labelL2";
            this.labelL2.Size = new System.Drawing.Size(91, 20);
            this.labelL2.TabIndex = 23;
            this.labelL2.Text = "第二行文字";
            this.labelL2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelL3
            // 
            this.labelL3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelL3.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelL3.Location = new System.Drawing.Point(15, 416);
            this.labelL3.Name = "labelL3";
            this.labelL3.Size = new System.Drawing.Size(91, 20);
            this.labelL3.TabIndex = 24;
            this.labelL3.Text = "第三行文字";
            this.labelL3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelL4
            // 
            this.labelL4.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelL4.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelL4.Location = new System.Drawing.Point(15, 440);
            this.labelL4.Name = "labelL4";
            this.labelL4.Size = new System.Drawing.Size(91, 20);
            this.labelL4.TabIndex = 25;
            this.labelL4.Text = "第四行文字";
            this.labelL4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelL5
            // 
            this.labelL5.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelL5.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelL5.Location = new System.Drawing.Point(15, 466);
            this.labelL5.Name = "labelL5";
            this.labelL5.Size = new System.Drawing.Size(91, 20);
            this.labelL5.TabIndex = 26;
            this.labelL5.Text = "第五行文字";
            this.labelL5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelL6
            // 
            this.labelL6.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelL6.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelL6.Location = new System.Drawing.Point(15, 491);
            this.labelL6.Name = "labelL6";
            this.labelL6.Size = new System.Drawing.Size(91, 20);
            this.labelL6.TabIndex = 27;
            this.labelL6.Text = "第六行文字";
            this.labelL6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelL10
            // 
            this.labelL10.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelL10.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelL10.Location = new System.Drawing.Point(15, 591);
            this.labelL10.Name = "labelL10";
            this.labelL10.Size = new System.Drawing.Size(91, 20);
            this.labelL10.TabIndex = 28;
            this.labelL10.Text = "第十行文字";
            this.labelL10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelL11
            // 
            this.labelL11.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelL11.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelL11.Location = new System.Drawing.Point(15, 616);
            this.labelL11.Name = "labelL11";
            this.labelL11.Size = new System.Drawing.Size(91, 20);
            this.labelL11.TabIndex = 29;
            this.labelL11.Text = "標識";
            this.labelL11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelM1
            // 
            this.labelM1.AutoEllipsis = true;
            this.labelM1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelM1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelM1.Location = new System.Drawing.Point(112, 366);
            this.labelM1.Name = "labelM1";
            this.labelM1.Size = new System.Drawing.Size(200, 20);
            this.labelM1.TabIndex = 30;
            this.labelM1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelM2
            // 
            this.labelM2.AutoEllipsis = true;
            this.labelM2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelM2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelM2.Location = new System.Drawing.Point(112, 391);
            this.labelM2.Name = "labelM2";
            this.labelM2.Size = new System.Drawing.Size(200, 20);
            this.labelM2.TabIndex = 31;
            this.labelM2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelM3
            // 
            this.labelM3.AutoEllipsis = true;
            this.labelM3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelM3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelM3.Location = new System.Drawing.Point(112, 416);
            this.labelM3.Name = "labelM3";
            this.labelM3.Size = new System.Drawing.Size(200, 20);
            this.labelM3.TabIndex = 32;
            this.labelM3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelM4
            // 
            this.labelM4.AutoEllipsis = true;
            this.labelM4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelM4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelM4.Location = new System.Drawing.Point(112, 441);
            this.labelM4.Name = "labelM4";
            this.labelM4.Size = new System.Drawing.Size(200, 20);
            this.labelM4.TabIndex = 33;
            this.labelM4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelM5
            // 
            this.labelM5.AutoEllipsis = true;
            this.labelM5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelM5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelM5.Location = new System.Drawing.Point(112, 466);
            this.labelM5.Name = "labelM5";
            this.labelM5.Size = new System.Drawing.Size(200, 20);
            this.labelM5.TabIndex = 34;
            this.labelM5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelM6
            // 
            this.labelM6.AutoEllipsis = true;
            this.labelM6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelM6.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelM6.Location = new System.Drawing.Point(112, 491);
            this.labelM6.Name = "labelM6";
            this.labelM6.Size = new System.Drawing.Size(200, 20);
            this.labelM6.TabIndex = 35;
            this.labelM6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelM10
            // 
            this.labelM10.AutoEllipsis = true;
            this.labelM10.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelM10.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelM10.Location = new System.Drawing.Point(112, 591);
            this.labelM10.Name = "labelM10";
            this.labelM10.Size = new System.Drawing.Size(200, 20);
            this.labelM10.TabIndex = 36;
            this.labelM10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelM11
            // 
            this.labelM11.AutoEllipsis = true;
            this.labelM11.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelM11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelM11.Location = new System.Drawing.Point(112, 616);
            this.labelM11.Name = "labelM11";
            this.labelM11.Size = new System.Drawing.Size(200, 20);
            this.labelM11.TabIndex = 37;
            this.labelM11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Language_button
            // 
            this.Language_button.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Language_button.Location = new System.Drawing.Point(224, 647);
            this.Language_button.Name = "Language_button";
            this.Language_button.Size = new System.Drawing.Size(88, 35);
            this.Language_button.TabIndex = 38;
            this.Language_button.Text = "English";
            this.Language_button.UseVisualStyleBackColor = true;
            this.Language_button.Click += new System.EventHandler(this.Language_button_Click);
            // 
            // label_system
            // 
            this.label_system.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(183)))), ((int)(((byte)(246)))));
            this.label_system.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_system.ForeColor = System.Drawing.Color.White;
            this.label_system.Location = new System.Drawing.Point(15, 200);
            this.label_system.Name = "label_system";
            this.label_system.Size = new System.Drawing.Size(297, 35);
            this.label_system.TabIndex = 39;
            this.label_system.Text = "系統初始化中!";
            this.label_system.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelM7
            // 
            this.labelM7.AutoEllipsis = true;
            this.labelM7.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelM7.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelM7.Location = new System.Drawing.Point(112, 516);
            this.labelM7.Name = "labelM7";
            this.labelM7.Size = new System.Drawing.Size(200, 20);
            this.labelM7.TabIndex = 41;
            this.labelM7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelL7
            // 
            this.labelL7.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelL7.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelL7.Location = new System.Drawing.Point(15, 516);
            this.labelL7.Name = "labelL7";
            this.labelL7.Size = new System.Drawing.Size(91, 20);
            this.labelL7.TabIndex = 40;
            this.labelL7.Text = "第七行文字";
            this.labelL7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelM8
            // 
            this.labelM8.AutoEllipsis = true;
            this.labelM8.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelM8.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelM8.Location = new System.Drawing.Point(112, 541);
            this.labelM8.Name = "labelM8";
            this.labelM8.Size = new System.Drawing.Size(200, 20);
            this.labelM8.TabIndex = 43;
            this.labelM8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelL8
            // 
            this.labelL8.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelL8.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelL8.Location = new System.Drawing.Point(15, 541);
            this.labelL8.Name = "labelL8";
            this.labelL8.Size = new System.Drawing.Size(91, 20);
            this.labelL8.TabIndex = 42;
            this.labelL8.Text = "第八行文字";
            this.labelL8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelM9
            // 
            this.labelM9.AutoEllipsis = true;
            this.labelM9.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelM9.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelM9.Location = new System.Drawing.Point(112, 566);
            this.labelM9.Name = "labelM9";
            this.labelM9.Size = new System.Drawing.Size(200, 20);
            this.labelM9.TabIndex = 45;
            this.labelM9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelL9
            // 
            this.labelL9.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelL9.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelL9.Location = new System.Drawing.Point(15, 566);
            this.labelL9.Name = "labelL9";
            this.labelL9.Size = new System.Drawing.Size(91, 20);
            this.labelL9.TabIndex = 44;
            this.labelL9.Text = "第九行文字";
            this.labelL9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 691);
            this.Controls.Add(this.labelM9);
            this.Controls.Add(this.labelL9);
            this.Controls.Add(this.labelM8);
            this.Controls.Add(this.labelL8);
            this.Controls.Add(this.labelM7);
            this.Controls.Add(this.labelL7);
            this.Controls.Add(this.label_system);
            this.Controls.Add(this.Language_button);
            this.Controls.Add(this.labelM11);
            this.Controls.Add(this.labelM10);
            this.Controls.Add(this.labelM6);
            this.Controls.Add(this.labelM5);
            this.Controls.Add(this.labelM4);
            this.Controls.Add(this.labelM3);
            this.Controls.Add(this.labelM2);
            this.Controls.Add(this.labelM1);
            this.Controls.Add(this.labelL11);
            this.Controls.Add(this.labelL10);
            this.Controls.Add(this.labelL6);
            this.Controls.Add(this.labelL5);
            this.Controls.Add(this.labelL4);
            this.Controls.Add(this.labelL3);
            this.Controls.Add(this.labelL2);
            this.Controls.Add(this.labelL1);
            this.Controls.Add(this.Exit_button);
            this.Controls.Add(this.textBox_Model);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_runcard);
            this.Controls.Add(this.Model_delete_button);
            this.Controls.Add(this.Model_send_button);
            this.Controls.Add(this.Model_config_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "雷射刻印機";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button Model_config_button;
        private System.Windows.Forms.Button Model_send_button;
        private System.Windows.Forms.Button Model_delete_button;
        private System.Windows.Forms.TextBox textBox_runcard;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Model;
        private System.Windows.Forms.Button Exit_button;
        private System.Windows.Forms.Label labelL1;
        private System.Windows.Forms.Label labelL2;
        private System.Windows.Forms.Label labelL3;
        private System.Windows.Forms.Label labelL4;
        private System.Windows.Forms.Label labelL5;
        private System.Windows.Forms.Label labelL6;
        private System.Windows.Forms.Label labelL10;
        private System.Windows.Forms.Label labelL11;
        private System.Windows.Forms.Label labelM1;
        private System.Windows.Forms.Label labelM2;
        private System.Windows.Forms.Label labelM3;
        private System.Windows.Forms.Label labelM4;
        private System.Windows.Forms.Label labelM5;
        private System.Windows.Forms.Label labelM6;
        private System.Windows.Forms.Label labelM10;
        private System.Windows.Forms.Label labelM11;
        private System.Windows.Forms.Button Language_button;
        private System.Windows.Forms.Label label_system;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelM7;
        private System.Windows.Forms.Label labelL7;
        private System.Windows.Forms.Label labelM8;
        private System.Windows.Forms.Label labelL8;
        private System.Windows.Forms.Label labelM9;
        private System.Windows.Forms.Label labelL9;
    }
}

