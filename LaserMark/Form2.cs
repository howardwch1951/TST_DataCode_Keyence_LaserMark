using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace LaserMark
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        TextBox[] Model_text = new TextBox[18];
        TextBox[] Block_text = new TextBox[27];
        TextBox[] Image_text = new TextBox[8];
        TextBox[] Logo_text = new TextBox[21];
        TextBox[] Counter_text = new TextBox[9];
        
        Label[] Model_label = new Label[18];
        Label[] Block_label = new Label[27];
        Label[] Image_label = new Label[8];
        Label[] Logo_label = new Label[21];

        Label[] Logo_column = new Label[21];
        Label[] Logo_labelL = new Label[21];

        ComboBox Logo_file = new ComboBox();

        private void Form2_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(Owner.Location.X, Owner.Location.Y);
            this.MaximizeBox = false;
            //System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff"));
            if (!Var.Initialization) button4.Enabled = false;
            label2.Text = Var.Model;

            //8
            for (int i = 0; i < 8; i++)
            {
                Image_label[i] = this.Controls.Find("labelOT" + (i + 1).ToString(), true)[0] as Label;
                Image_text[i] = this.Controls.Find("textBoxT" + (i + 1).ToString(), true)[0] as TextBox;
            }

            //18
            for (int i = 0; i < 18; i++)
            {
                Model_label[i] = this.Controls.Find("labelOU" + (i + 1).ToString(), true)[0] as Label;
                Model_text[i] = this.Controls.Find("textBoxU" + (i + 1).ToString(), true)[0] as TextBox;
            }

            ////27
            //for (int i = 1; i < 8; i++)
            //    for (int j = 0; j < 27; j++)
            //    {
            //        Block_label[j] = this.Controls.Find("labelOL" + i + "_" + (j + 1).ToString(), true)[0] as Label;
            //        Block_text[j] = this.Controls.Find("textBoxL" + i + "_" + (j + 1).ToString(), true)[0] as TextBox;
            //    }

            //21
            for (int i = 0; i < 21; i++)
            {
                Logo_column[i] = this.Controls.Find("columnL11_" + (i + 1).ToString(), true)[0] as Label;
                Logo_labelL[i] = this.Controls.Find("labelL11_" + (i + 1).ToString(), true)[0] as Label;

                Logo_label[i] = this.Controls.Find("labelOL11_" + (i + 1).ToString(), true)[0] as Label;
                Logo_text[i] = this.Controls.Find("textBoxL11_" + (i + 1).ToString(), true)[0] as TextBox;
            }

            //9
            for (int i = 0; i < 9; i++)
            {
                Counter_text[i] = new TextBox();
                Counter_text[i].Name = "textBoxC" + (i + 1).ToString();
            }

            //System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff"));
            Logo_file = this.Controls.Find("comboBox11_Logo", true)[0] as ComboBox;

            tabControl1.SelectedIndex = 0;
            this.ActiveControl = tabControl1.TabPages[0];
        }
        private void Form2_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                //System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff"));
                Model_setting();
                //System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff"));

                if (Logo_text[1].Text != "") Text_show(Logo_text[1].Text, Logo_column, Logo_text, Logo_label, Logo_labelL);

                this.Enabled = true;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("FS", "Form2_Shown," + ex.Message);
            }
        }
        //新增
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                InputBox inputBox = new InputBox();
                inputBox.Owner = this;
                DialogResult dr = inputBox.ShowDialog();

                if (dr == DialogResult.OK && Var.Model != inputBox.GetMsg())
                {
                    Var.Model = inputBox.GetMsg();
                    label2.Text = Var.Model;
                    Access_data.Access_Insert_FormDatatable(Var.Model, Var.Access_select, Var.Model_counter);

                    Var.Model_select = Access_data.Access_Select("select Model from Model order by Model ASC");
                    Var.Access_select = Access_data.Access_Select("SELECT Model.*, Block.*, Image.*, Logo.* FROM((Model LEFT OUTER JOIN [Image] ON Model.Model = Image.Model)LEFT OUTER JOIN Block ON Model.Model = Block.Model) LEFT OUTER JOIN Logo ON Model.Model = Logo.Model WHERE Model.Model = Image.Model And Model.Model = Logo.Model And Model.Model = '" + Var.Model + "'");
                    Var.Model_counter = Access_data.Access_Select("SELECT Counter.* from [Counter] where Model='" + Var.Model + "' order by Counter_No ASC");

                    ComboBox C1 = Owner.Controls.Find("Combobox1", true)[0] as ComboBox;
                    C1.Items.Clear();
                    C1.Items.Add(Var.Model);
                    C1.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("Form2", "button1_Click新增參數 : " + ex.ToString());
                MessageBox.Show("button1_Click新增參數錯誤");
            }
        }
        //儲存
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (label2.Text == "品別名稱" || label2.Text == "Model Name")
                {
                    if (Var.English)
                        MessageBox.Show("Create new Model please press 'New' button");
                    else
                        MessageBox.Show("新建立參數請選擇新增參數");
                }
                else
                {
                    bool illegal = false;

                    //檢查是否有錯誤的textbox
                    for (int i = 0; i < tabControl1.TabCount; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                for (int j = 0; j < Image_text.Length; j++)
                                    if (Image_text[j].BackColor == Color.Red)
                                    {
                                        illegal = true;
                                        tabControl1.SelectedIndex = i;
                                        //將焦點移至該textbox
                                        this.ActiveControl = Image_text[j];
                                        Image_text[j].SelectAll();
                                        break;
                                    }
                                break;
                            case 1:
                                for (int j = 0; j < Model_text.Length; j++)
                                    if (Model_text[j].BackColor == Color.Red)
                                    {
                                        illegal = true;
                                        tabControl1.SelectedIndex = i;
                                        //將焦點移至該textbox
                                        this.ActiveControl = Model_text[j];
                                        Model_text[j].SelectAll();
                                        break;
                                    }
                                break;
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                                for (int j = 0; j < 27; j++)
                                {
                                    Block_text[j] = new TextBox();
                                    Block_text[j] = this.Controls.Find("textboxL" + (i - 1) + "_" + (j + 1).ToString(), true)[0] as TextBox;

                                    if (Block_text[j].BackColor == Color.Red)
                                    {
                                        illegal = true;
                                        tabControl1.SelectedIndex = i;
                                        //將焦點移至該textbox
                                        this.ActiveControl = Block_text[j];
                                        Block_text[j].SelectAll();
                                        break;
                                    }
                                }
                                break;
                            case 12:
                                for (int j = 0; j < Logo_text.Length; j++)
                                {
                                    if (Logo_text[j].BackColor == Color.Red)
                                    {
                                        if (Logo_text[1].Text != "" && Convert.ToDouble(Logo_text[1].Text) == -3 && (j == 7 || j == 8))
                                            continue;
                                        else if (Logo_text[1].Text != "" && (Convert.ToDouble(Logo_text[1].Text) == -1 || Convert.ToDouble(Logo_text[1].Text) == -2 || Convert.ToDouble(Logo_text[1].Text) == -4) && (j == 9 || j == 10 || j == 11 || j == 12))
                                            continue;

                                        illegal = true;
                                        tabControl1.SelectedIndex = i;
                                        //將焦點移至該textbox
                                        this.ActiveControl = Logo_text[j];
                                        Logo_text[j].SelectAll();
                                        break;
                                    }

                                    if (j < Logo_text.Length - 1)
                                    {
                                        if (Logo_file.Text == "")
                                        {
                                            for (int u = 0; u < Logo_text.Length; u++)
                                            {
                                                if (Logo_text[u].Text != "" && Logo_text[u].Visible)
                                                {
                                                    illegal = true;
                                                    tabControl1.SelectedIndex = i;
                                                    //將焦點移至該combobox
                                                    this.ActiveControl = Logo_file;
                                                    Logo_file.SelectAll();
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                        }

                        if (illegal) break;
                    }



                    if (illegal == false)
                    {
                        Access_data.Access_Insert_Log(Var.Model, Var.Access_select, Var.Model_counter);
                        Access_data.Access_Update(Var.Model, Var.Access_select, Model_text, Image_text, Logo_text, Logo_file.Text);

                        for (int i = 0; i < 10; i++)
                        {
                            for (int j = 0; j < 27; j++)
                            {
                                Block_text[j] = new TextBox();
                                Block_text[j] = this.Controls.Find("textboxL" + (i + 1) + "_" + (j + 1).ToString(), true)[0] as TextBox;
                            }
                            if (Block_text[26].Text != "" && Block_text[26].Text != null)
                                if (Var.Access_select.Select("Block.Block_No='" + i + "'").Length > 0)
                                    Access_data.Access_Update(Var.Model, i.ToString(), Block_text);
                                else
                                    Access_data.Access_Insert(Var.Model, i.ToString(), Block_text);
                            else if (Block_text[26].Text == "" || Block_text[26].Text == null)
                                if (Var.Access_select.Select("Block.Block_No='" + i + "'").Length > 0)
                                    Access_data.Access_Delete(Var.Model, i);
                        }

                        for (int i = 0; i < 10; i++)
                        {
                            if (Var.Counter_num[i] == "1")
                            {
                                Counter_text[0].Text = this.Controls.Find("label_counter" + i.ToString(), true)[0].Text.Replace("Counter# ", "");

                                for (int j = 1; j < 9; j++)
                                {
                                    Counter_text[j].Text = this.Controls.Find("counter" + i.ToString() + "_value" + j.ToString(), true)[0].Text;
                                }

                                if (Var.Model_counter.Rows.Count > 0)
                                    if (Var.Model_counter.Select("Counter_No=" + i.ToString()).Length > 0)
                                        Access_data.Access_Update(Var.Model, Counter_text);
                                    else
                                        Access_data.Access_Insert(Var.Model, Counter_text);
                                else
                                    Access_data.Access_Insert(Var.Model, Counter_text);
                            }
                            else if (Var.Model_counter.Select("Counter_No=" + i.ToString()).Length > 0)
                                Access_data.Access_Delete_counter(Var.Model, i);
                        }

                        Var.Access_select = null;
                        Var.Access_select = Access_data.Access_Select("SELECT Model.*, Block.*, Image.*, Logo.* FROM((Model LEFT OUTER JOIN [Image] ON Model.Model = Image.Model)LEFT OUTER JOIN Block ON Model.Model = Block.Model) LEFT OUTER JOIN Logo ON Model.Model = Logo.Model WHERE Model.Model = Image.Model And Model.Model = Logo.Model And Model.Model = '" + Var.Model + "'");

                        Var.Model_counter = null;
                        Var.Model_counter = Access_data.Access_Select("SELECT Counter.* from [Counter] where Model='" + Var.Model + "' order by Counter_No ASC");

                        Model_setting();

                        tabControl1.SelectedIndex = 0;
                        this.ActiveControl = tabControl1.TabPages[0];

                        if (Var.English)
                            MessageBox.Show("Update config success");
                        else
                            MessageBox.Show("成功更新參數");
                    }
                    else
                    {
                        if (Var.English)
                            MessageBox.Show("Update config fail，Plase rechecked the value！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            MessageBox.Show("更新參數失敗，請重新確認數值！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }                
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("Form2", "button2_Click修改參數 : " + ex.ToString());
                MessageBox.Show("button2_Click修改參數錯誤" + ex.ToString());
            }
        }
        //讀取
        string respon = "";
        System.Threading.Timer timer_flash;
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                tabControl1.Enabled = false;
                timer_flash = new System.Threading.Timer(new System.Threading.TimerCallback(read_flash), null, 0, 500);

                respon = "";
                respon = Keyence.Keyence_sendforread("F9,900");
                //底座設定
                if (respon.Substring(0, 4) == "F9,0")
                {
                    string[] text_T = respon.Replace("F9,0,", "").Split(',');

                    for (int i = 0; i < text_T.Length; i++)
                        Image_text[i].Text = Convert.ToDouble(text_T[i]).ToString();

                    respon = "";
                    respon = Keyence.Keyence_sendforread("K1,900");
                    //通用設定
                    if (respon.Substring(0, 4) == "K1,0")
                    {
                        string[] text_U = respon.Replace("K1,0,", "").Split(',');

                        int k = 0;
                        for (int i = 0; i < text_U.Length; i++)
                        {
                            if (i == 2 || i == 12) i++;
                            Model_label[k].Text = Convert.ToDouble(text_U[i]).ToString();

                            k++;
                        }

                        //先清空Logo設定
                        for (int i = 0; i < 21; i++)
                        {
                            //視權限開放
                            if (i == 16 || i == 17 || i == 18)
                            {
                                Logo_text[i].Enabled = true;
                                Logo_text[i].BackColor = Color.White;
                            }

                            Logo_text[i].Text = "";
                        }
                            
                        Logo_file.SelectedIndex = 0;

                        for (int j = 0; j < Var.Marking_string.Length - 1; j++)
                        {
                            respon = "";
                            respon = Keyence.Keyence_sendforread("K3,900," + j);
                            //文字設定
                            if (respon.Substring(0, 4) == "K3,0")
                            {
                                string[] text_L = respon.Replace("K3,0,", "").Split(',');

                                //一般文字
                                if (text_L[1].IndexOf("-") == -1)
                                {
                                    k = 0;
                                    for (int i = 0; i < text_L.Length; i++)
                                    {
                                        if (i == 11) i++;
                                        Block_text[k] = this.Controls.Find("textBoxL" + (j + 1) + "_" + (k + 1).ToString(), true)[0] as TextBox;

                                        if (k != 26)
                                            Block_text[k].Text = Convert.ToDouble(text_L[i]).ToString();
                                        else
                                            Block_text[k].Text = text_L[i];
                                        
                                        //視權限開放
                                        if (k == 11 || k == 12 || k == 13)
                                            if (Var.str_UserID == "Runcard")
                                            {
                                                Block_text[k].BackColor = Color.FromArgb(195, 195, 195);
                                                Block_text[k].Enabled = false;
                                            }
                                            else
                                            {
                                                Block_text[k].Enabled = true;
                                                Block_text[k].BackColor = Color.White;
                                            }


                                            k++;
                                    }
                                }
                                //圖片
                                else
                                {
                                    //清空原先該行數值
                                    for (int i = 0; i < Block_text.Length; i++)
                                    {
                                        Block_text[i] = this.Controls.Find("textBoxL" + (j + 1) + "_" + (i + 1).ToString(), true)[0] as TextBox;

                                        //視權限開放
                                        if (i == 11 || i == 12 || i == 13)
                                        {
                                            Block_text[i].Enabled = true;
                                            Block_text[i].BackColor = Color.White;
                                        }

                                        Block_text[i].Text = "";
                                    }

                                    if (Convert.ToDouble(text_L[1]) == -1 || Convert.ToDouble(text_L[1]) == -2 || Convert.ToDouble(text_L[1]) == -4)
                                    {
                                        k = 0;
                                        for (int i = 0; i < text_L.Length - 1; i++)
                                        {
                                            if (i == 12) i++;
                                            if (k == 9) k += 4;
                                            Logo_text[k].Text = Convert.ToDouble(text_L[i]).ToString();

                                            //視權限開放
                                            if (k == 16 || k == 17 || k == 18)
                                                if (Var.str_UserID == "Runcard")
                                                {
                                                    Logo_text[k].BackColor = Color.FromArgb(195, 195, 195);
                                                    Logo_text[k].Enabled = false;
                                                }
                                                else
                                                {
                                                    Logo_text[k].Enabled = true;
                                                    Logo_text[k].BackColor = Color.White;
                                                }

                                            k++;
                                        }
                                    }
                                    else if (Convert.ToDouble(text_L[1]) == -4)
                                    {
                                        k = 0;
                                        for (int i = 0; i < text_L.Length - 1; i++)
                                        {
                                            if (i == 16) i++;
                                            Logo_text[k].Text = Convert.ToDouble(text_L[i]).ToString();

                                            //視權限開放
                                            if (k == 16 || k == 17 || k == 18)
                                                if (Var.str_UserID == "Runcard")
                                                {
                                                    Logo_text[k].BackColor = Color.FromArgb(195, 195, 195);
                                                    Logo_text[k].Enabled = false;
                                                }
                                                else
                                                {
                                                    Logo_text[k].Enabled = true;
                                                    Logo_text[k].BackColor = Color.White;
                                                }

                                            k++;
                                        }
                                    }


                                    Text_show(text_L[1], Logo_column, Logo_text, Logo_label, Logo_labelL);

                                    string Logo_name = text_L[text_L.Length - 1].Replace("%L", "").Replace("%K", "").Replace("%T", "").Replace("%Z", "").Trim('<', '>');
                                    if (!Default_Item.Logo_txt_write(Logo_name))
                                    {
                                        Logo_file.Items.Clear();

                                        string[] Logo = Default_Item.Logo_file();
                                        for (int i = 0; i < Logo.Length; i++)
                                            Logo_file.Items.Add(Logo[i]);
                                    }

                                    Logo_file.Text = Logo_name;
                                }
                            }
                            else if (respon == "K3,1,S022")
                            {
                                for (int i = 0; i < Block_text.Length; i++)
                                {
                                    Block_text[i] = this.Controls.Find("textBoxL" + (j + 1) + "_" + (i + 1).ToString(), true)[0] as TextBox;

                                    //視權限開放
                                    if (i == 11 || i == 12 || i == 13)
                                    {
                                        Block_text[i].Enabled = true;
                                        Block_text[i].BackColor = Color.White;
                                    }

                                    Block_text[i].Text = "";
                                }
                            }
                            else
                            {
                                MessageBox.Show(Keyence.Keyence_Command_Error(respon.Substring(2)) + " , " + Keyence.Keyence_Error(respon.Substring(5)), "參數錯誤 " + respon.Substring(2) + " : " + respon.Substring(5));

                                break;
                            }
                        }

                        //先清空計數器物件
                        for (int i = 0; i < 10; i++)
                        {
                            if (Var.Counter_num[i] == "1")
                            {
                                this.Controls.Find("Tabpage_Counter" + i.ToString(), true)[0].Dispose();
                                Var.Counter_num[i] = "0";
                            }

                            //Var.Counter_OldValue[i] = "";
                        }
                        Var.Counter_No = 0;

                        for (int i = 0; i < 2; i++)
                        {
                            respon = "";
                            respon = Keyence.Keyence_sendforread("F7,900," + i);
                            //計數器設定
                            if (respon.Substring(0, 4) == "F7,0")
                            {
                                string[] text_C = respon.Replace("F7,0,", "").Split(',');

                                Counter_text[0].Text = i.ToString();
                                for (int j = 0; j < text_C.Length; j++)
                                    Counter_text[j + 1].Text = Convert.ToDouble(text_C[j]).ToString();

                                Tabpage_Counter counter = new Tabpage_Counter();
                                counter.Counter_Set(i, Counter_text);
                                tabPage14.Controls.Add(counter);

                                Var.Counter_num[i] = "1";

                                Counter_AutoArrange();
                            }
                            else
                            {
                                MessageBox.Show(Keyence.Keyence_Command_Error(respon.Substring(2)) + " , " + Keyence.Keyence_Error(respon.Substring(5)), "參數錯誤 " + respon.Substring(2) + " : " + respon.Substring(5));

                                break;
                            }
                        }

                        MessageBox.Show("讀取結束", "讀取機台參數");
                    }
                    else
                    {
                        MessageBox.Show(Keyence.Keyence_Command_Error(respon.Substring(2)) + " , " + Keyence.Keyence_Error(respon.Substring(5)), "參數錯誤 " + respon.Substring(2) + " : " + respon.Substring(5));
                    }
                }
                else if (respon == "F9,1,S021")
                {
                    MessageBox.Show("程序NO.未註冊", "參數錯誤 S021");
                }
                else
                {
                    MPU.WriteErrorCode("bc4", "button4_Click," + respon);
                    if (respon == "timeout")
                        MessageBox.Show("連線超時", "Timeout");
                    else if (respon == "fail")
                        MessageBox.Show("讀取失敗", "Fail");
                    else
                        MessageBox.Show(Keyence.Keyence_Command_Error(respon.Substring(2)) + " , " + Keyence.Keyence_Error(respon.Substring(5)), "參數錯誤 " + respon.Substring(2) + " : " + respon.Substring(5));
                }


                timer_flash.Change(-1, -1);
                timer_flash.Dispose();
                
                tabControl1.Enabled = true;
                label2.BackColor = SystemColors.ControlLightLight;
                tabControl1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("bc4", "button4_Click," + ex.Message);

                timer_flash.Change(-1, -1);
                timer_flash.Dispose();

                tabControl1.Enabled = true;
                label2.BackColor = SystemColors.ControlLightLight;

                MessageBox.Show("請重新傳輸","傳輸失敗");
            }
        }
        //離開
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Access_data.Access_MarkingString(Var.Access_select);
                this.Close();
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("F1", "button3_Click," + ex.Message);
            }
        }
        //將所有物件或變數清空
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Var.clear = true;
                //System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff"));
                //8
                for (int i = 0; i < 8; i++)
                {
                    Image_label[i].Text = "";
                    Image_text[i].Text = "";
                }

                //18
                for (int i = 0; i < 18; i++)
                {
                    Model_label[i].Text = "";
                    Model_text[i].Text = "";
                }

                //27
                for (int i = 1; i <= 10; i++)
                    for (int j = 0; j < 27; j++)
                    {
                        Block_label[j] = this.Controls.Find("labelOL" + i + "_" + (j + 1).ToString(), true)[0] as Label;
                        Block_label[j].Text = "";

                        Block_text[j] = this.Controls.Find("textBoxL" + i + "_" + (j + 1).ToString(), true)[0] as TextBox;

                        //視權限開放
                        if (j == 11 || j == 12 || j == 13)
                        { 
                            Block_text[j].Enabled = true;
                            Block_text[j].BackColor = Color.White;
                        }
                        Block_text[j].Text = "";
                    }
                Logo_file.Text = "";

                //21
                for (int i = 0; i < 21; i++)
                {
                    Logo_label[i].Text = "";
                    Logo_text[i].Text = "";

                    //視權限開放
                    if (i == 16 || i == 17 || i == 18)
                    {
                        Logo_text[i].Enabled = true;
                        Logo_text[i].BackColor = SystemColors.Window;
                    }
                }

                //9
                for (int i = 0; i < 10; i++)
                {
                    if (Var.Counter_num[i] == "1")
                    {
                        this.Controls.Find("Tabpage_Counter" + i.ToString(), true)[0].Dispose();
                        Var.Counter_num[i] = "0";
                    }

                    Var.Counter_OldValue[i] = "";
                }
                Var.Counter_No = 0;


                Var.clear = false;
                //System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff"));
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("FCED", "Form2_FormClosed," + ex.Message);
            }
        }

        public void Model_setting()
        {
            try
            {
                if (Var.Access_select != null && Var.Access_select.Rows.Count > 0)
                    Access_data.Access_Value(Var.Access_select, Model_text, Model_label, Image_text, Image_label, Logo_text, Logo_label, Logo_file);

                for (int i = 0; i < Var.Access_select.Rows.Count; i++)
                {
                    if (Var.Access_select.Rows[i]["Block.Block_No"].ToString() != null && Var.Access_select.Rows[i]["Block.Block_No"].ToString() != "")
                    {
                        int No = Convert.ToInt32(Var.Access_select.Rows[i]["Block.Block_No"].ToString());

                        for (int j = 0; j < 27; j++)
                        {
                            Block_text[j] = this.Controls.Find("textboxL" + (No + 1) + "_" + (j + 1).ToString(), true)[0] as TextBox;

                            Block_label[j] = this.Controls.Find("labelOL" + (No + 1) + "_" + (j + 1).ToString(), true)[0] as Label;


                            Application.DoEvents();
                        }

                        Access_data.Access_Value(Var.Access_select, i, Block_text, Block_label);

                        //視權限開放
                        if (Var.str_UserID == "Runcard")
                        {
                            Block_text[11].BackColor = Color.FromArgb(195,195,195);
                            Block_text[11].Enabled = false;
                            Block_text[12].BackColor = Color.FromArgb(195, 195, 195);
                            Block_text[12].Enabled = false;
                            Block_text[13].BackColor = Color.FromArgb(195, 195, 195);
                            Block_text[13].Enabled = false;
                        }
                        else
                        {
                            Block_text[11].Enabled = true;
                            Block_text[11].BackColor = Color.White;
                            Block_text[12].Enabled = true;
                            Block_text[12].BackColor = Color.White;
                            Block_text[13].Enabled = true;
                            Block_text[13].BackColor = Color.White;
                        }
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    if (Var.Counter_num[i] == "1")
                    {
                        this.Controls.Find("Tabpage_Counter" + i.ToString(), true)[0].Dispose();
                        Var.Counter_num[i] = "0";
                    }

                    //初始化,數值是尚未更改時
                    Var.Counter_OldValue[i] = i.ToString();
                }
                Var.Counter_No = 0;

                if (Var.Model_counter != null && Var.Model_counter.Rows.Count > 0)
                {
                    for (int i = 0; i < Var.Model_counter.Rows.Count; i++)
                    {
                        for (int j = 1; j < Var.Model_counter.Columns.Count; j++)
                            Counter_text[j - 1].Text = Convert.ToDouble(Var.Model_counter.Rows[i][j]).ToString();

                        Tabpage_Counter counter = new Tabpage_Counter();
                        counter.Counter_Set(Convert.ToInt32(Var.Model_counter.Rows[i]["Counter_No"].ToString()), Counter_text);
                        tabPage14.Controls.Add(counter);

                        Var.Counter_num[Convert.ToInt32(Var.Model_counter.Rows[i]["Counter_No"].ToString())] = "1";
                    }

                    Counter_AutoArrange();
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("M2", "Model_setting," + ex.Message);

                this.Enabled = true;
            }
        }
        //Logo -1,-2,-4 與 -3 動態更換參數設定顯示
        private void Text_show(string num, Label[] logo_c, TextBox[] logo_t, Label[] logo_ol, Label[] logo_l)
        {
            try
            {
                if (Convert.ToDouble(num) == -1 || Convert.ToDouble(num) == -2 || Convert.ToDouble(num) == -4)
                {
                    for (int i = 13; i < 19; i++)
                    {
                        logo_c[i].Visible = true;
                        logo_t[i].Visible = true;
                        logo_ol[i].Visible = true;
                        logo_l[i].Visible = true;

                        logo_c[i].Location = new Point(6, 25 + ((i - 4) * 30));
                        logo_t[i].Location = new Point(112, 25 + ((i - 4) * 30));
                        logo_ol[i].Location = new Point(218, 25 + ((i - 4) * 30));
                        logo_l[i].Location = new Point(324, 25 + ((i - 4) * 30));

                        logo_t[i].TabStop = true;
                        logo_t[i].TabIndex = i + 1;
                    }
                    for (int i = 19; i < 21; i++)
                    {
                        logo_c[i].Visible = true;
                        logo_t[i].Visible = true;
                        logo_ol[i].Visible = true;
                        logo_l[i].Visible = true;

                        logo_c[i].Location = new Point(505, 25 + ((i - 19) * 30));
                        logo_t[i].Location = new Point(611, 25 + ((i - 19) * 30));
                        logo_ol[i].Location = new Point(717, 25 + ((i - 19) * 30));
                        logo_l[i].Location = new Point(823, 25 + ((i - 19) * 30));

                        logo_t[i].TabStop = true;
                        logo_t[i].TabIndex = i + 1;
                    }

                    logo_c[7].Visible = true;
                    logo_t[7].Visible = true;
                    logo_ol[7].Visible = true;
                    logo_l[7].Visible = true;
                    logo_c[8].Visible = true;
                    logo_t[8].Visible = true;
                    logo_ol[8].Visible = true;
                    logo_l[8].Visible = true;

                    logo_t[7].TabStop = true;
                    logo_t[8].TabStop = true;
                    logo_t[7].TabIndex = 12;
                    logo_t[8].TabIndex = 13;

                    for (int i = 9; i < 13; i++)
                    {
                        logo_c[i].Visible = false;
                        logo_t[i].Visible = false;
                        logo_ol[i].Visible = false;
                        logo_l[i].Visible = false;

                        logo_t[i].TabStop = false;
                    }
                }
                else if (Convert.ToDouble(num) == -3)
                {
                    for (int i = 9; i < 17; i++)
                    {
                        logo_c[i].Visible = true;
                        logo_t[i].Visible = true;
                        logo_ol[i].Visible = true;
                        logo_l[i].Visible = true;

                        logo_c[i].Location = new Point(6, 25 + ((i - 2) * 30));
                        logo_t[i].Location = new Point(112, 25 + ((i - 2) * 30));
                        logo_ol[i].Location = new Point(218, 25 + ((i - 2) * 30));
                        logo_l[i].Location = new Point(324, 25 + ((i - 2) * 30));

                        logo_t[i].TabStop = true;
                        logo_t[i].TabIndex = i + 3;
                    }
                    for (int i = 17; i < 21; i++)
                    {
                        logo_c[i].Visible = true;
                        logo_t[i].Visible = true;
                        logo_ol[i].Visible = true;
                        logo_l[i].Visible = true;

                        logo_c[i].Location = new Point(505, 25 + ((i - 17) * 30));
                        logo_t[i].Location = new Point(611, 25 + ((i - 17) * 30));
                        logo_ol[i].Location = new Point(717, 25 + ((i - 17) * 30));
                        logo_l[i].Location = new Point(823, 25 + ((i - 17) * 30));

                        logo_t[i].TabStop = true;
                        logo_t[i].TabIndex = i + 3;
                    }


                    logo_c[7].Visible = false;
                    logo_t[7].Visible = false;
                    logo_ol[7].Visible = false;
                    logo_l[7].Visible = false;
                    logo_c[8].Visible = false;
                    logo_t[8].Visible = false;
                    logo_ol[8].Visible = false;
                    logo_l[8].Visible = false;

                    logo_t[7].TabStop = false;
                    logo_t[8].TabStop = false;
                }

                //圖片檔跟著最後一個label跑
                columnL11_22.Location = new Point(logo_c[20].Location.X, logo_c[20].Location.Y + 30);
                comboBox11_Logo.Location = new Point(logo_t[20].Location.X, logo_c[20].Location.Y + 30);
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("", "text_show," + ex.Message);
            }
        }

        private void textBoxL11_2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Double.TryParse(textBoxL11_2.Text + e.KeyChar.ToString(), out double i))
                    if (Convert.ToDouble(textBoxL11_2.Text + e.KeyChar.ToString()) == -1 || Convert.ToDouble(textBoxL11_2.Text + e.KeyChar.ToString()) == -2 || Convert.ToDouble(textBoxL11_2.Text + e.KeyChar.ToString()) == -4)
                    {
                        Text_show(textBoxL11_2.Text + e.KeyChar.ToString(), Logo_column, Logo_text, Logo_label, Logo_labelL);
                    }
                    else if (Convert.ToDouble(textBoxL11_2.Text + e.KeyChar.ToString()) == -3)
                    {
                        Text_show(textBoxL11_2.Text + e.KeyChar.ToString(), Logo_column, Logo_text, Logo_label, Logo_labelL);
                    }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("k8", "textBoxL11_2_KeyPress" + ex.Message);
            }
        }

        private void comboBox11_Logo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox11_Logo.Text != "")
                {
                    for (int i = 0; i < Logo_text.Length; i++)
                    {
                        if (Logo_text[i].Text == "" && Logo_text[i].Visible)
                            Logo_text[i].BackColor = Color.Red;
                    }
                }
                else if (comboBox11_Logo.Text == "")
                {
                    for (int i = 0; i < Logo_text.Length; i++)
                    {
                        if (Logo_text[i].Text != "") break;

                        if (i == Logo_text.Length - 1)
                            for (int j = 0; j < Logo_text.Length; j++)
                                Logo_text[j].BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("C8", "comboBox11_Logo_SelectedIndexChanged," + ex.Message);
            }
        }
        bool t = false;
        private void read_flash(object state)
        {
            try
            {
                if (t)
                {
                    label2.BackColor = Color.Red;
                    label2.ForeColor = Color.Black;
                }
                else
                {
                    label2.BackColor = Color.LightGoldenrodYellow;
                    label2.ForeColor = Color.Black;
                }

                t = !t;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("TT", "timer1_Tick" + ex.Message);
            }
        }

        //　＋　按鈕,新增計數器按鈕
        private void button_counter_new_Click(object sender, EventArgs e)
        {
            try
            {
                if (Var.Counter_No < 10)
                {
                    for (int i = 0; i <= 9; i++)
                        if (Var.Counter_num[i] == "0")
                        {
                            Tabpage_Counter counter = new Tabpage_Counter();
                            counter.Counter_Set(i);
                            tabPage14.Controls.Add(counter);
                            counter.Location = new Point(1, 55 + ((Var.Counter_No - 1) * 92));

                            Var.Counter_num[i] = "1";
                            break;
                        }
                }
                else
                {
                    if (Var.English)
                        MessageBox.Show("Maximum number of Counter", "Counter");
                    else
                        MessageBox.Show("已達計數器上限", "計數器");
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("ex", "button_counter_new_Click," + ex.Message);
            }
        }

        //Counter介面的物件自動排列
        public void Counter_AutoArrange()
        {
            int i = 0;

            tabPage14.VerticalScroll.Value = 0;

            foreach(Control c in tabPage14.Controls)
            {
                if (c is Tabpage_Counter)
                {
                    c.Location = new Point(1, 55 + (i * 92));

                    i++;
                }
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
    }
}
