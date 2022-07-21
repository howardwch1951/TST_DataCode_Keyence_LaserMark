using DateCode_Dll;
using Microsoft.VisualBasic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LaserMark
{
    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();
        }
        //protected override void WndProc(ref Message m)
        //{
        //    //WM_SYSCOMMAND	0x112  SC_CLOSE 0xF060  SC_MINIMIZE 0xF020  SC_RESTORE 0xF120
        //    if (m.Msg != 0x0112 || (m.WParam == (IntPtr)0xF060 || m.WParam == (IntPtr)0xF020 || m.WParam == (IntPtr)0xF120))
        //    {
        //        base.WndProc(ref m);
        //    }
        //}

        bool Initialization_state = false;
        System.Threading.Timer timer_backup;
        //學弟新增
        private static ComboBox Model_ComboBox;
        private static Button Model_send_btn;
        private static Button Model_config_btn;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {                
                //學弟新增
                Model_ComboBox = this.Controls.Find("comboBox1", true)[0] as ComboBox;
                Model_send_btn = this.Controls.Find("Model_send_button", true)[0] as Button;
                Model_config_btn = this.Controls.Find("Model_config_button", true)[0] as Button;
                Model_ComboBox.Click += new EventHandler(comboBox1_Click);
                comboBox1.MouseWheel += new MouseEventHandler(comboBox1_MouseWheel);
                //

                MPU.CreateDirectory();
                Var.array3_initialize();
                Var.str_DeviceNO = "Laser #3";

                //System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff"));
                timer1.Start();

                //載入設定檔
                txt_load();
                Load_Excel();

                //使用程式載入所有名稱
                Language_button.PerformClick();

                for (int i = 0; i <= 9; i++)
                {
                    Var.Counter_num[i] = "0";
                    Var.Counter_OldValue[i] = "";
                }

                textBox_runcard.BackColor = SystemColors.Window;
                textBox_runcard.Enabled = false;
                Model_send_button.Enabled = false;
                Model_config_button.Enabled = false;
                Model_delete_button.Enabled = false;

                form2.Owner = this;
                form4.Owner = this;
                form5.Owner = form2;                

                //---創建Rule---//
                if (!File.Exists(@"C:\Profile_Keyence\SystemProfile\Keyence_Rule.mdb"))
                {
                    Directory.CreateDirectory(@"C:\Profile_Keyence\SystemProfile\");
                    File.Copy(@".\Keyence_Rule.mdb", @"C:\Profile_Keyence\SystemProfile\Keyence_Rule.mdb");
                }

                //所有產品的設定檔
                if (!File.Exists(@"C:\Profile_Keyence\SystemProfile\Model.mdb"))
                {
                    Directory.CreateDirectory(@"C:\Profile_Keyence\SystemProfile\");
                    File.Copy(@".\Model.mdb", @"C:\Profile_Keyence\SystemProfile\Model.mdb");
                }

                //Log檔
                if (!File.Exists(@"C:\Profile_Keyence\SystemProfile\Log.mdb"))
                {
                    Directory.CreateDirectory(@"C:\Profile_Keyence\SystemProfile\");
                    File.Copy(@".\Log.mdb", @"C:\Profile_Keyence\SystemProfile\Log.mdb");
                }

                this.SetDesktopLocation(0, 0);
                this.MaximizeBox = false;

                //主畫面刻印文字Label
                for (int i = 0; i < Var.Marking_string.Length; i++)
                    Var.Marking_string[i] = this.Controls.Find("labelM" + (i + 1).ToString(), true)[0] as Label;

                Var.Model_select = Access_data.Access_Select("select Model from Model order by Model ASC");
                //for (int i = 0; i < Var.Model_select.Rows.Count; i++)
                //    comboBox1.Items.Add(Var.Model_select.Rows[i][0].ToString());

                ComboBox C_logo = form2.Controls.Find("comboBox11_Logo", true)[0] as ComboBox;
                string[] Logo = Default_Item.Logo_file();
                for (int i = 0; i < Logo.Length; i++)
                    C_logo.Items.Add(Logo[i]);
                
                //個別加入數值判斷事件和上下限名稱
                for (int i = 1; i <= 8; i++)
                {
                    TextBox text_T = form2.Controls.Find("textBoxT" + i, true)[0] as TextBox;

                    text_T.KeyPress += new KeyPressEventHandler(Num_key_T);
                    text_T.TextChanged += new EventHandler(Num_limit_T);
                    text_T.MaxLength = Convert.ToInt32(Var.Limit[0][i - 1][2]);
                    text_T.AutoSize = false;
                    text_T.Height = 23;
                    text_T.Font = new Font("微軟正黑體", (float)11, FontStyle.Bold);

                    form2.Controls.Find("labelT" + i, true)[0].Text = Var.Limitname[0][i - 1];
                }

                for (int i = 1; i <= 18; i++)
                {
                    TextBox text_U = form2.Controls.Find("textBoxU" + i, true)[0] as TextBox;

                    text_U.KeyPress += new KeyPressEventHandler(Num_key_U);
                    text_U.TextChanged += new EventHandler(Num_limit_U);
                    text_U.MaxLength = Convert.ToInt32(Var.Limit[1][i - 1][2]);
                    text_U.AutoSize = false;
                    text_U.Height = 23;
                    text_U.Font = new Font("微軟正黑體", (float)11, FontStyle.Bold);

                    form2.Controls.Find("labelU" + i, true)[0].Text = Var.Limitname[1][i - 1];
                }

                for (int i = 1; i <= 10; i++)
                {
                    for (int j = 1; j < 27; j++)
                    {
                        TextBox text_L = form2.Controls.Find("textBoxL" + i + "_" + j, true)[0] as TextBox;

                        text_L.KeyPress += new KeyPressEventHandler(Num_key_L);
                        text_L.TextChanged += new EventHandler(Num_limit_L);
                        text_L.MaxLength = Convert.ToInt32(Var.Limit[2][j - 1][2]);

                        form2.Controls.Find("labelL" + i + "_" + j, true)[0].Text = Var.Limitname[2][j - 1];
                    }

                    TextBox text_L_string = form2.Controls.Find("textBoxL" + i + "_27", true)[0] as TextBox;

                    text_L_string.KeyPress += new KeyPressEventHandler(Jis_string);
                    text_L_string.TextChanged += new EventHandler(Num_limit_string);
                    text_L_string.Leave += new EventHandler(Num_counter_string);
                    text_L_string.MaxLength = 127;

                    form2.Controls.Find("labelL" + i + "_27", true)[0].Text = Var.Limitname[2][26];
                }

                for (int i = 1; i <= 21; i++)
                {
                    TextBox text_Logo = form2.Controls.Find("textBoxL11_" + i, true)[0] as TextBox;

                    text_Logo.KeyPress += new KeyPressEventHandler(Num_key_Logo);
                    text_Logo.TextChanged += new EventHandler(Num_limit_Logo);
                    text_Logo.MaxLength = Convert.ToInt32(Var.Limit[3][i - 1][2]);
                    text_Logo.AutoSize = false;
                    text_Logo.Height = 23;
                    text_Logo.Font = new Font("微軟正黑體", (float)11, FontStyle.Bold);

                    form2.Controls.Find("labelL11_" + i, true)[0].Text = Var.Limitname[3][i - 1];
                }


                if (ComOpen())
                {
                    if (!Keyence.Keyence_promgramName())
                    {
                        Var.Initialization = false;

                        if (Var.English)
                            MessageBox.Show("Program Registered Error", "Initialization failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            MessageBox.Show("機台程序註冊失敗", "初始化失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    Var.Initialization = false;

                timer1.Stop();
                Initialization_state = true;
                if (!Var.Initialization)
                {
                    label_system.BackColor = Color.Red;
                    label_system.ForeColor = Color.White;

                    label_system.Text = "參數檢視模式";
                }
                else
                {
                    //使雷刻機台畫面切換至No.900
                    Keyence.Keyence_send("GA,900");

                    label_system.BackColor = Color.DarkSeaGreen;
                    label_system.ForeColor = Color.White;

                    label_system.Text = "系統 OK ";
                }

                timer_backup = new System.Threading.Timer(new System.Threading.TimerCallback(FileDailyBackup), null, 0, 1000);

                textBox_runcard.Enabled = true;
                Model_delete_button.Enabled = true;

                //System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff"));
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("F1", "Form1_Load," + ex.Message);
            }
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                this.TopMost = true;
                this.TopMost = false;
                this.Activate();
                this.ActiveControl = textBox_runcard;
            }
            catch(Exception ex)
            {
                MPU.WriteErrorCode("F1", "Form1_Shown," + ex.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                form2.Dispose();
                form4.Dispose();
                this.Dispose();
                
                System.Environment.Exit(Environment.ExitCode);
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("FF", "Form1_FormClosing," + ex.Message);
            }
        }

        public bool ComOpen()
        {
            try
            {
                Var.serialPort = new SerialPort("COM4", 38400, Parity.None, 8, StopBits.One);

                Var.serialPort.Open();


                return Var.serialPort.IsOpen;
            }
            catch (TimeoutException e)
            {
                MPU.WriteErrorCode("ComOpen", "TimeoutException : " + e.ToString());

                if (Var.English)
                    MessageBox.Show("Serial port connection timeout", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("雷射刻印機連線逾時", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);


                return false;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("RS232", "ComOpen," + ex.Message);

                if (Var.English)
                    MessageBox.Show("Connecting Laser Marker fail,Please check RS232 port & Laser Masrker can be romote contral!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("雷射刻印機連線失敗，請檢查RS232接頭是否連接或雷刻機是否為遠端控制狀態!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);


                return false;
            }
        }

        //底座設定輸入數值判斷
        private void Num_key_T(object sender, KeyPressEventArgs e)
        {
            try
            {
                //只能輸入數字"0~9"和"-"和"backspace鍵"
                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 45 || e.KeyChar == 8)
                    e.Handled = false;
                //限制部分能輸入"."
                else if (e.KeyChar == 46 && Var.Limit[0][Convert.ToInt32((sender as TextBox).Name.Replace("textBox", "").Substring(1)) - 1][3].IndexOf(".") > 0)
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("NKT", "Num_key_T," + ex.Message);
            }
        }

        //底座設定數值判斷
        private void Num_limit_T(object sender, EventArgs e)
        {
            try
            {
                if ((sender as TextBox).Text != "" && Double.TryParse((sender as TextBox).Text, out double i))
                {
                    //正常double數值
                    if (Convert.ToDouble((sender as TextBox).Text) > Convert.ToDouble(Var.Limit[0][Convert.ToInt32((sender as TextBox).Name.Replace("textBox", "").Substring(1)) - 1][1]))
                        (sender as TextBox).BackColor = Color.Red;
                    else if (Convert.ToDouble((sender as TextBox).Text) < Convert.ToDouble(Var.Limit[0][Convert.ToInt32((sender as TextBox).Name.Replace("textBox", "").Substring(1)) - 1][0]))
                        (sender as TextBox).BackColor = Color.Red;
                    else
                        (sender as TextBox).BackColor = Color.White;
                }
                else if ((sender as TextBox).Text != "" && !Double.TryParse((sender as TextBox).Text, out double j))
                {
                    //非正常double數值
                    (sender as TextBox).BackColor = Color.Red;
                }
                else
                {
                    if (form2.Controls.Find("labelO" + (sender as TextBox).Name.Replace("textBox", ""), true)[0].Text != "")
                        (sender as TextBox).BackColor = Color.Red;
                    else
                        (sender as TextBox).BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("N1", "Num_limit_T" + ex.Message);
            }
        }


        //通用設定輸入數值判斷
        private void Num_key_U(object sender, KeyPressEventArgs e)
        {
            try
            {
                //只能輸入數字"0~9"和"-"和"backspace鍵"
                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 45 || e.KeyChar == 8)
                    e.Handled = false;
                //限制部分能輸入"."
                else if (e.KeyChar == 46 && Var.Limit[1][Convert.ToInt32((sender as TextBox).Name.Replace("textBox", "").Substring(1)) - 1][3].IndexOf(".") > 0)
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("NKU", "Num_key_U," + ex.Message);
            }
        }

        //通用設定數值判斷
        private void Num_limit_U(object sender, EventArgs e)
        {
            try
            {
                if ((sender as TextBox).Text != "" && Double.TryParse((sender as TextBox).Text, out double i))
                {
                    //正常double數值
                    if (Convert.ToDouble((sender as TextBox).Text) > Convert.ToDouble(Var.Limit[1][Convert.ToInt32((sender as TextBox).Name.Replace("textBox", "").Substring(1)) - 1][1]))
                        (sender as TextBox).BackColor = Color.Red;
                    else if (Convert.ToDouble((sender as TextBox).Text) < Convert.ToDouble(Var.Limit[1][Convert.ToInt32((sender as TextBox).Name.Replace("textBox", "").Substring(1)) - 1][0]))
                        (sender as TextBox).BackColor = Color.Red;
                    else
                        (sender as TextBox).BackColor = Color.White;
                }
                else if ((sender as TextBox).Text != "" && !Double.TryParse((sender as TextBox).Text, out double j))
                {
                    //非正常double數值
                    (sender as TextBox).BackColor = Color.Red;
                }
                else
                {
                    if (form2.Controls.Find("labelO" + (sender as TextBox).Name.Replace("textBox", ""), true)[0].Text != "")
                        (sender as TextBox).BackColor = Color.Red;
                    else
                        (sender as TextBox).BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("N1", "Num_limit_U" + ex.Message);
            }
        }

        //文字設定輸入數值判斷
        private void Num_key_L(object sender, KeyPressEventArgs e)
        {
            try
            {
                //只能輸入數字"0~9"和"-"和"backspace鍵"
                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 45 || e.KeyChar == 8)
                    e.Handled = false;
                //限制部分能輸入"."
                else if (e.KeyChar == 46 && Var.Limit[2][Convert.ToInt32((sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("_") + 1)) - 1][3].IndexOf(".") > 0)
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("NKL", "Num_key_L," + ex.Message);
            }
        }

        //文字設定數值判斷
        private void Num_limit_L(object sender, EventArgs e)
        {
            try
            {
                if ((sender as TextBox).Text != "" && Double.TryParse((sender as TextBox).Text, out double i) && (sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("_") + 1) != "27")
                {
                    //非正常double數值
                    if (Convert.ToDouble((sender as TextBox).Text) > Convert.ToDouble(Var.Limit[2][Convert.ToInt32((sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("_") + 1)) - 1][1]))
                        (sender as TextBox).BackColor = Color.Red;
                    else if (Convert.ToDouble((sender as TextBox).Text) < Convert.ToDouble(Var.Limit[2][Convert.ToInt32((sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("_") + 1)) - 1][0]))
                        (sender as TextBox).BackColor = Color.Red;
                    else
                        (sender as TextBox).BackColor = Color.White;

                    //該頁空的就變紅
                    TextBox T = new TextBox();
                    for (int t = 1; t <= 27; t++)
                    {
                        T = form2.Controls.Find("textBoxL" + (sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("L") + 1, (sender as TextBox).Name.IndexOf("_") - ((sender as TextBox).Name.IndexOf("L") + 1)) + "_" + t, true)[0] as TextBox;

                        if (T.Text == "")
                            T.BackColor = Color.Red;
                    }
                }
                else if ((sender as TextBox).Text != "" && !Double.TryParse((sender as TextBox).Text, out double j) && (sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("_") + 1) != "27")
                {
                    //非正常double數值
                    (sender as TextBox).BackColor = Color.Red;
                }
                else
                {
                    if (form2.Controls.Find("labelO" + (sender as TextBox).Name.Replace("textBox", ""), true)[0].Text != "")
                        (sender as TextBox).BackColor = Color.Red;
                    else
                    {
                        if (!Var.clear)
                        {
                            (sender as TextBox).BackColor = Color.Red;

                            //該頁全空就變白
                            for (int y = 1; y <= 27; y++)
                            {
                                TextBox T = new TextBox();
                                T = form2.Controls.Find("textBoxL" + (sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("L") + 1, (sender as TextBox).Name.IndexOf("_") - ((sender as TextBox).Name.IndexOf("L") + 1)) + "_" + y, true)[0] as TextBox;
                                if (T.Text != "") break;

                                if (y == 27)
                                    for (int k = 1; k <= 27; k++)
                                    {
                                        T = form2.Controls.Find("textBoxL" + (sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("L") + 1, (sender as TextBox).Name.IndexOf("_") - ((sender as TextBox).Name.IndexOf("L") + 1)) + "_" + k, true)[0] as TextBox;
                                        T.BackColor = Color.White;
                                    }
                            }
                        }
                    }                
                }


                if ((sender as TextBox).BackColor == Color.Red)
                {
                    //該頁全空就變白
                    for (int y = 1; y <= 27; y++)
                    {
                        TextBox T = new TextBox();
                        T = form2.Controls.Find("textBoxL" + (sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("L") + 1, (sender as TextBox).Name.IndexOf("_") - ((sender as TextBox).Name.IndexOf("L") + 1)) + "_" + y, true)[0] as TextBox;
                        if (T.Text != "") break;

                        if (y == 27)
                            for (int k = 1; k <= 27; k++)
                            {
                                T = form2.Controls.Find("textBoxL" + (sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("L") + 1, (sender as TextBox).Name.IndexOf("_") - ((sender as TextBox).Name.IndexOf("L") + 1)) + "_" + k, true)[0] as TextBox;
                                T.BackColor = Color.White;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("N1", "Num_limit_L" + ex.Message);
            }
        }

        //刻印字串輸入判斷(全形)
        private void Jis_string(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar >= 33 && e.KeyChar <= 126)
                    e.KeyChar += Convert.ToChar(65248);
                else if (e.KeyChar == 32)
                    e.KeyChar = Convert.ToChar(12288);
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("N1", "Jis_string" + ex.Message);
            }
        }

        //刻印字串字數判斷
        private void Num_limit_string(object sender, EventArgs e)
        {
            try
            {
                //(sender as TextBox).Text = Encoding.GetEncoding("Shift-JIS").GetString(Encoding.Convert(Encoding.GetEncoding("ASCII"), Encoding.GetEncoding("Shift-JIS"), Encoding.Default.GetBytes((sender as TextBox).Text)));
                //Console.WriteLine(Encoding.GetEncoding("Shift-JIS").GetString(Encoding.Convert(Encoding.GetEncoding("ASCII"), Encoding.GetEncoding("Shift-JIS"), Encoding.Default.GetBytes((sender as TextBox).Text))));
                //(sender as TextBox).Text = Strings.StrConv((sender as TextBox).Text, VbStrConv.Wide, 0);
                if ((sender as TextBox).Text != "" && (sender as TextBox).Text.Length < 512)
                {
                    (sender as TextBox).BackColor = Color.White;

                    //該頁空的就變紅
                    TextBox T = new TextBox();
                    for (int t = 1; t <= 27; t++)
                    {
                        T = form2.Controls.Find("textBoxL" + (sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("L") + 1, (sender as TextBox).Name.IndexOf("_") - ((sender as TextBox).Name.IndexOf("L") + 1)) + "_" + t, true)[0] as TextBox;

                        if (T.Text == "")
                            T.BackColor = Color.Red;
                    }
                }
                else
                {
                    if (form2.Controls.Find("labelO" + (sender as TextBox).Name.Replace("textBox", ""), true)[0].Text != "")
                        (sender as TextBox).BackColor = Color.Red;
                    else
                    {
                        if (!Var.clear)
                        {
                            (sender as TextBox).BackColor = Color.Red;

                            //該頁全空就變白
                            for (int y = 1; y <= 27; y++)
                            {
                                TextBox T = new TextBox();
                                T = form2.Controls.Find("textBoxL" + (sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("L") + 1, (sender as TextBox).Name.IndexOf("_") - ((sender as TextBox).Name.IndexOf("L") + 1)) + "_" + y, true)[0] as TextBox;
                                if (T.Text != "") break;

                                if (y == 27)
                                    for (int k = 1; k <= 27; k++)
                                    {
                                        T = form2.Controls.Find("textBoxL" + (sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("L") + 1, (sender as TextBox).Name.IndexOf("_") - ((sender as TextBox).Name.IndexOf("L") + 1)) + "_" + k, true)[0] as TextBox;
                                        T.BackColor = Color.White;
                                    }
                            }
                        }
                    }
                }

                
                if ((sender as TextBox).BackColor == Color.Red)
                {
                    //該頁全空就變白
                    for (int y = 1; y <= 27; y++)
                    {
                        TextBox T = new TextBox();
                        T = form2.Controls.Find("textBoxL" + (sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("L") + 1, (sender as TextBox).Name.IndexOf("_") - ((sender as TextBox).Name.IndexOf("L") + 1)) + "_" + y, true)[0] as TextBox;
                        if (T.Text != "") break;

                        if (y == 27)
                            for (int k = 1; k <= 27; k++)
                            {
                                T = form2.Controls.Find("textBoxL" + (sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("L") + 1, (sender as TextBox).Name.IndexOf("_") - ((sender as TextBox).Name.IndexOf("L") + 1)) + "_" + k, true)[0] as TextBox;
                                T.BackColor = Color.White;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("N1", "Num_limit_string" + ex.Message);
            }
        }

        //判斷是否第一個字是%,則全轉半形
        private void Num_counter_string(object sender, EventArgs e)
        {
            try
            {
                if ((sender as TextBox).Text != "")
                    if ((sender as TextBox).Text.Substring(0, 1) == "％" || (sender as TextBox).Text.Substring(0, 1) == "%")
                    {
                        (sender as TextBox).Text = Strings.StrConv((sender as TextBox).Text, VbStrConv.Narrow, 0);
                    }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("",""+ex.Message);
            }
        }

        //標識設定輸入數值判斷
        private void Num_key_Logo(object sender, KeyPressEventArgs e)
        {
            try
            {
                //只能輸入數字"0~9"和"-"和"backspace鍵"
                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 45 || e.KeyChar == 8)
                    e.Handled = false;
                //限制部分能輸入"."
                else if (e.KeyChar == 46 && Var.Limit[3][Convert.ToInt32((sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("_") + 1)) - 1][3].IndexOf(".") > 0)
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("NKL", "Num_key_Logo," + ex.Message);
            }
        }

        //標識設定數值判斷
        private void Num_limit_Logo(object sender, EventArgs e)
        {
            try
            {
                if ((sender as TextBox).Text != "" && Double.TryParse((sender as TextBox).Text, out double i) && (sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("_") + 1) != "27")
                {
                    //非正常double數值
                    if (Convert.ToDouble((sender as TextBox).Text) > Convert.ToDouble(Var.Limit[3][Convert.ToInt32((sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("_") + 1)) - 1][1]))
                        (sender as TextBox).BackColor = Color.Red;
                    else if (Convert.ToDouble((sender as TextBox).Text) < Convert.ToDouble(Var.Limit[3][Convert.ToInt32((sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("_") + 1)) - 1][0]))
                        (sender as TextBox).BackColor = Color.Red;
                    else
                        (sender as TextBox).BackColor = Color.White;

                    //該頁空的就變紅
                    TextBox T = new TextBox();
                    for (int t = 1; t <= 21; t++)
                    {
                        T = form2.Controls.Find("textBoxL" + (sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("L") + 1, (sender as TextBox).Name.IndexOf("_") - ((sender as TextBox).Name.IndexOf("L") + 1)) + "_" + t, true)[0] as TextBox;

                        if (T.Text == "")
                            T.BackColor = Color.Red;
                    }
                }
                else if ((sender as TextBox).Text != "" && !Double.TryParse((sender as TextBox).Text, out double j))
                {
                    //非正常double數值
                    (sender as TextBox).BackColor = Color.Red;
                }
                else
                {
                    if (form2.Controls.Find("labelO" + (sender as TextBox).Name.Replace("textBox", ""), true)[0].Text != "")
                        (sender as TextBox).BackColor = Color.Red;
                    else
                    {
                        if (!Var.clear)
                        {
                            (sender as TextBox).BackColor = Color.Red;

                            //該頁全空就變白
                            ComboBox C = form2.Controls.Find("comboBox11_Logo", true)[0] as ComboBox;
                            if (C.Text == "")
                                for (int y = 1; y <= 21; y++)
                                {
                                    TextBox T = new TextBox();
                                    T = form2.Controls.Find("textBoxL" + (sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("L") + 1, (sender as TextBox).Name.IndexOf("_") - ((sender as TextBox).Name.IndexOf("L") + 1)) + "_" + y, true)[0] as TextBox;
                                    if (T.Text != "") break;

                                    if (y == 27)
                                        for (int k = 1; k <= 21; k++)
                                        {
                                            T = form2.Controls.Find("textBoxL" + (sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("L") + 1, (sender as TextBox).Name.IndexOf("_") - ((sender as TextBox).Name.IndexOf("L") + 1)) + "_" + k, true)[0] as TextBox;
                                            T.BackColor = Color.White;
                                        }
                                }
                        }
                    }
                }


                if ((sender as TextBox).BackColor == Color.Red)
                {
                    //該頁全空就變白
                    ComboBox C = form2.Controls.Find("comboBox11_Logo", true)[0] as ComboBox;
                    if (C.Text == "")
                        for (int y = 1; y <= 21; y++)
                        {
                            TextBox T = new TextBox();
                            T = form2.Controls.Find("textBoxL" + (sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("L") + 1, (sender as TextBox).Name.IndexOf("_") - ((sender as TextBox).Name.IndexOf("L") + 1)) + "_" + y, true)[0] as TextBox;
                            if (T.Text != "") break;

                            if (y == 21)
                                for (int k = 1; k <= 21; k++)
                                {
                                    T = form2.Controls.Find("textBoxL" + (sender as TextBox).Name.Substring((sender as TextBox).Name.IndexOf("L") + 1, (sender as TextBox).Name.IndexOf("_") - ((sender as TextBox).Name.IndexOf("L") + 1)) + "_" + k, true)[0] as TextBox;
                                    T.BackColor = Color.White;
                                }
                        }
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("N1", "Num_limit_Logo" + ex.Message);
            }
        }

        string K0_Parameter = "K0,900,";
        string K2_Parameter = "K2,900,";
        string G8_Parameter = "G8,900,";
        string K2_Logo_Parameter = "K2,900,";
        string G6_Parameter = "G6,900,";

        bool send_command = false;

        private void Model_send_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Var.serialPort.IsOpen)
                {
                    send_command = true;

                    textBox_runcard.ReadOnly = true;
                    textBox_runcard.KeyPress -= textBox_runcard_KeyPress;
                    comboBox1.Enabled = false;
                    Model_send_button.Enabled = false;
                    Model_config_button.Enabled = false;
                    Model_delete_button.Enabled = false;
                    timer1.Start();
                    label_system.Text = "參數傳送中";
                    label_system.BackColor = Color.Red;
                    label_system.ForeColor = Color.Black;

                    if (Keyence.Keyence_send("GA,900"))
                    {
                        if (Keyence.Keyence_send("XT,900"))
                        {
                            if (Keyence.Keyence_send("G4,900," + Var.Model))
                            {
                                K0_Parameter = "K0,900,";
                                for (int i = 1; i <= 20; i++)
                                {
                                    K0_Parameter += Var.Access_select.Rows[0][i].ToString();

                                    if (i != 20) K0_Parameter += ",";
                                }

                                if (Keyence.Keyence_send(K0_Parameter))
                                {
                                    bool K2_All = true;
                                    for (int i = 0; i < Var.Access_select.Rows.Count; i++)
                                    {
                                        if (Var.Access_select.Rows[i]["Block.Block_No"].ToString() != "")
                                        {
                                            K2_Parameter = "K2,900,";
                                            for (int j = 22; j <= 50; j++)
                                            {
                                                K2_Parameter += Var.Access_select.Rows[i][j].ToString();

                                                if (j != 50) K2_Parameter += ",";
                                            }

                                            if (!Keyence.Keyence_send(K2_Parameter))
                                            {
                                                K2_All = false;
                                                break;
                                            }
                                        }
                                    }

                                    K2_Logo_Parameter = "K2,900,";
                                    if (Var.Access_select.Rows[0][63].ToString() != "")
                                    {
                                        if (Var.Access_select.Rows[0][63].ToString() == "-01" || Var.Access_select.Rows[0][63].ToString() == "-02" || Var.Access_select.Rows[0][63].ToString() == "-04")
                                        {
                                            for (int i = 61; i <= 84; i++)
                                            {
                                                if (i != 71 && i != 72 && i != 73 && i != 74)
                                                {
                                                    K2_Logo_Parameter += Var.Access_select.Rows[0][i].ToString();

                                                    if (i != 84) K2_Logo_Parameter += ",";
                                                }
                                            }
                                        }
                                        else if (Var.Access_select.Rows[0][63].ToString() == "-03")
                                        {
                                            for (int i = 61; i <= 84; i++)
                                            {
                                                if (i != 69 && i != 70)
                                                {
                                                    K2_Logo_Parameter += Var.Access_select.Rows[0][i].ToString();

                                                    if (i != 84) K2_Logo_Parameter += ",";
                                                }
                                            }
                                        }
                                        if (!Keyence.Keyence_send(K2_Logo_Parameter)) K2_All = false;
                                    }

                                    if (K2_All)
                                    {
                                        G8_Parameter = "G8,900,";
                                        for (int i = 52; i <= 59; i++)
                                        {
                                            G8_Parameter += Var.Access_select.Rows[0][i].ToString();

                                            if (i != 59) G8_Parameter += ",";
                                        }
                                        if (Keyence.Keyence_send(G8_Parameter))
                                        {
                                            Var.str_marking = "";

                                            for (int i = 0; i < Var.Marking_string.Length; i++)
                                                Var.str_marking += Var.Marking_string[i].Text + ";";

                                            if (Var.Model_counter.Rows.Count > 0)
                                            {
                                                bool G6_All = true;

                                                for (int i = 0; i < Var.Model_counter.Rows.Count; i++)
                                                {
                                                    G6_Parameter = "G6,900,";
                                                    for (int j = 1; j < Var.Model_counter.Columns.Count; j++)
                                                    {
                                                        G6_Parameter += Var.Model_counter.Rows[i][j].ToString();

                                                        if (j != Var.Model_counter.Columns.Count - 1) G6_Parameter += ",";
                                                    }

                                                    if (!Keyence.Keyence_send(G6_Parameter))
                                                    {
                                                        G6_All = false;
                                                        break;
                                                    }
                                                }

                                                if (G6_All)
                                                {
                                                    DBupdate();
                                                    Keyence.Keyence_send("YE");

                                                    Send_Success();
                                                }
                                                else
                                                    Send_Fail();
                                            }
                                            else
                                            {
                                                DBupdate();
                                                Keyence.Keyence_send("YE");

                                                Send_Success();
                                            }
                                        }
                                        else
                                            Send_Fail();
                                    }
                                    else
                                        Send_Fail();
                                }
                                else
                                    Send_Fail();
                            }
                            else
                                Send_Fail();
                        }
                        else
                            Send_Fail();
                    }
                    else
                        Send_Fail();


                    textBox_runcard.ReadOnly = false;
                    textBox_runcard.KeyPress += textBox_runcard_KeyPress;
                    comboBox1.Enabled = true;
                    Model_send_button.Enabled = true;
                    Model_config_button.Enabled = true;
                    Model_delete_button.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("M1", "Model_send_button_Click," + ex.Message);
                MessageBox.Show(ex.Message, "傳送失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);

                textBox_runcard.ReadOnly = false;
                textBox_runcard.KeyPress += textBox_runcard_KeyPress;
                comboBox1.Enabled = true;
                Model_send_button.Enabled = true;
                Model_config_button.Enabled = true;
                Model_delete_button.Enabled = true;
                Send_Fail();
            }
        }

        //傳送成功
        private void Send_Success()
        {
            try
            {
                send_command = false;
                timer1.Stop();
                label_system.Text = "參數傳送結束";
                label_system.BackColor = Color.DarkSeaGreen;
                label_system.ForeColor = Color.White;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("SS", "Send_Success," + ex.Message);
            }
        }
        //傳送失敗
        private void Send_Fail()
        {
            try
            {
                send_command = false;
                timer1.Stop();
                label_system.Text = "參數傳送失敗";
                label_system.BackColor = Color.Red;
                label_system.ForeColor = Color.White;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("SS", "Send_Fail," + ex.Message);
            }
        }

        public static Form2 form2 = new Form2();
        public static Form4 form4 = new Form4();
        public static Form5 form5 = new Form5();

        private void Model_config_button_Click(object sender, EventArgs e)
        {
            if (Var.TST_Form.IsDisposed == false)
            {
                Var.TST_Form.Close();
            }

            form2.ShowDialog();            
            comboBox1.Text = Var.Model;

            if (Var.TST_Form.IsDisposed == true)
            {               
                Var.Form2_Close = true;
                Var.Form3_Close = false;
                Var.TST_Form = new TST_DateCode_Form();
                Var.TST_Form.Owner = this;
                Var.TST_Form.Show();
            }
        }

        private void Model_delete_button_Click(object sender, EventArgs e)
        {
            if (Var.TST_Form.IsDisposed == false)
            {
                Var.TST_Form.Close();
            }
            form4.ShowDialog();
        }

        private void Exit_button_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }

        DataRow[] model_row;

        public void AddComboBoxItems(string model)
        {
            try
            {
                model_row = Var.Model_select.Select("Model like '" + model + "%'");

                if (model_row.Length > 0)
                {
                    comboBox1.Items.Clear();
                    for (int i = 0; i < model_row.Length; i++)
                    {
                        comboBox1.Items.Add(model_row[i]["Model"]);
                        comboBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    for (int i = 0; i < Var.Model_select.Rows.Count; i++)
                        comboBox1.Items.Add(Var.Model_select.Rows[i][0].ToString());

                    MPU.WriteErrorCode("F123", "電腦內無檔案," + textBox_runcard.Text + "," + textBox_Model.Text);

                    if (Var.English)
                        MessageBox.Show("There is no " + textBox_Model.Text + " file in the computer !\r\n" + "Please engineering staff to add files !", "RunCard Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("電腦內無 " + textBox_Model.Text + " 檔案！\r\n" + "請工程人員新增檔案！", "RunCard錯誤！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("Form1", textBox_runcard.Text + "AddComboBoxItems :" + ex.ToString());
                MessageBox.Show("AddComboBoxItems : 載入下拉式選單出錯!");
            }
        }
        private void comboBox1_MouseWheel(object sender, MouseEventArgs e)
        {            
            ((HandledMouseEventArgs)e).Handled = true;                          
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {            
            try
            {
                Var.Model = Model_ComboBox.Text;                

                if (!Var.Form3_Close)
                {
                    foreach (Form f in this.OwnedForms)
                    {
                        if (f.Name == "TST_DateCode_Form")
                        {
                            TST_DateCode_Form.Model_Name_TextBox.Text = Model_ComboBox.Text;
                        }                        
                    }

                    if (Var.TST_Form.IsDisposed == true)
                    {
                        Var.TST_Form = new TST_DateCode_Form();
                        Var.TST_Form.Owner = this;
                        Var.TST_Form.Show();
                    }
                } 
                comboBox1_Click(comboBox1, new EventArgs());
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("CS", "comboBox1_SelectedIndexChanged," + ex.Message);
            }
        }

        public static void Update_Label()
        {
            try
            {                
                Var.Access_select = null;
                Var.Access_select = Access_data.Access_Select("SELECT Model.*, Block.*, Image.*, Logo.* FROM((Model LEFT OUTER JOIN[Image] ON Model.Model = Image.Model)LEFT OUTER JOIN Block ON Model.Model = Block.Model) LEFT OUTER JOIN Logo ON Model.Model = Logo.Model WHERE Model.Model = Image.Model And Model.Model = Logo.Model And Model.Model = '" + Var.Model + "'");

                Var.Model_counter = null;
                Var.Model_counter = Access_data.Access_Select("SELECT Counter.* from [Counter] where Model='" + Var.Model + "' order by Counter_No ASC");

                Access_data.Access_MarkingString(Var.Access_select);

                if (Var.Initialization) Model_send_btn.Enabled = true;
                Model_config_btn.Enabled = true;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("CS", "comboBox1_SelectedIndexChanged," + ex.Message);
            }
        }


        private void comboBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if ((sender as ComboBox).Text != "" && (sender as ComboBox).Items.Count > 10 && !UserId_Regex_ENG.IsMatch(textBox_runcard.Text))
                {
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add(Var.Model);
                    comboBox1.Text = Var.Model;
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("CC", "comboBox1_Click," + ex.Message);
            }
        }

        public static DataTable ExecuteDataTable(string sql, params OracleParameter[] parameters)
        {
            DataTable datatable = new DataTable();
            try
            {
                using (OracleConnection conn = new OracleConnection(Var.connStr))
                {
                    conn.Open();
                    Var.Ethernet = true;

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.Parameters.AddRange(parameters);
                        OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                        adapter.Fill(datatable);
                        return datatable;
                    }
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("Form1", "ExecuteDataTable," + ex.Message.ToString());

                Var.Ethernet = false;
                return datatable;
            }
        }

        DataTable view_runcard;
        DataTable DateCode_Table;
        DataTable Today_Table;
        public static string Barcode_runcard = "";
        //前3碼為[A-Z] [-] {9個數字} [-] {2個數字}
        private static Regex Runcard_regex = new Regex(@"^[A-Z]{3}[\-]\d{9}[\-]\d{2}");
        private static Regex UserId_Regex_ENG = new Regex(@"^ENG$");

        private void textBox_runcard_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if ((e.KeyChar >= (Char)48 && e.KeyChar <= (Char)57 || e.KeyChar == (Char)13 || e.KeyChar == (Char)8 || e.KeyChar == (Char)45) || (e.KeyChar >= (Char)65 && e.KeyChar <= (Char)90) || (e.KeyChar >= (Char)97 && e.KeyChar <= (Char)122))
            {
                e.Handled = false;

                if (Var.TST_Form.IsDisposed == false)
                {
                    Var.TST_Form.Close();
                }
                Model_config_btn.Enabled = false;
                textBox_Model.Text = "";
                comboBox1.Items.Clear();

                for (int i = 0; i < Var.Marking_string.Length; i++)
                {
                    Var.Marking_string[i].Text = "";
                    Var.Marking_string[i].BackColor = Color.White;
                }

                 Var.Date = "";
                 Var.Today_Date = "";
            }
            else
            {
                e.Handled = true;
            }

            if (Barcode_runcard == "")
                textBox_runcard.Text = "";

            if (Barcode_runcard.Length < 16 && e.KeyChar != 13)
                Barcode_runcard = Barcode_runcard + e.KeyChar;
            else if (e.KeyChar == 13)
            {
                textBox_runcard.Text = Barcode_runcard;
                Barcode_runcard = "";
            }

            try
            {
                if (textBox_runcard.Text.Length == 16 && e.KeyChar == 13)
                {
                    textBox_runcard.Text = textBox_runcard.Text.ToUpper();
                    if (Runcard_regex.IsMatch(textBox_runcard.Text))
                    {
                        comboBox1.Items.Clear();
                        Var.recipe = "";
                        Var.str_RUNcard = "";
                       
                        view_runcard = ExecuteDataTable("SELECT sfa01,sfa03,shm01 Runcard,shm05 FROM sfa_file,shm_file WHERE shm012 = sfa01 and sfa05 > 0 and shm01 ='" + textBox_runcard.Text + "'");
                        DateCode_Table = ExecuteDataTable("SELECT to_char(shm15,'yyyymmdd')||to_char(wk_manu(shm15),'FM00') FROM sfa_file,shm_file WHERE shm012 = sfa01 and sfa05 > 0  and shm01 = '" + textBox_runcard.Text + "'");
                        Today_Table = ExecuteDataTable("select to_char(sysdate,'yyyymmdd hh24Mi D') from pm_dc_file @olmes1 where to_char(pm03,'yyyy/mm/dd') = to_char(sysdate,'yyyy/mm/dd')");

                        if (view_runcard.Rows.Count > 0)
                        {
                            Var.Date = DateCode_Table.Rows[0][0].ToString(); //Date = 2022053122
                            Var.Today_Date = Today_Table.Rows[0][0].ToString();//Today_Date = 20220531 1446 3
                            DateCode.DateRefresh(Var.Date, Var.Today_Date);

                            textBox_Model.Text = view_runcard.Rows[0][3].ToString();
                            Var.recipe = textBox_Model.Text;
                            Var.str_RUNcard = textBox_runcard.Text;
                            Var.str_UserID = "Runcard";

                            AddComboBoxItems(textBox_Model.Text);
                        }
                        else
                        {
                            if (Var.English)
                            {
                                if (Var.Ethernet)
                                    MessageBox.Show("No data on this RunCard！", "RunCard Error！");
                                else
                                {
                                    comboBox1.Items.Clear();

                                    for (int i = 0; i < Var.Model_select.Rows.Count; i++)
                                        comboBox1.Items.Add(Var.Model_select.Rows[i][0].ToString());

                                    MessageBox.Show("Ethernet connected failed ,Please manual select files！", "Ethernet Error！");
                                }
                            }
                            else
                            {
                                if (Var.Ethernet)
                                    MessageBox.Show("此RunCard無資料！", "RunCard錯誤！");
                                else
                                {
                                    Var.str_UserID = "Runcard";

                                    comboBox1.Items.Clear();

                                    for (int i = 0; i < Var.Model_select.Rows.Count; i++)
                                        comboBox1.Items.Add(Var.Model_select.Rows[i][0].ToString());

                                    MessageBox.Show("網路連線失敗,請手動選擇檔案！", "網路異常！");
                                }
                            }
                        }
                    }
                }
                else if (UserId_Regex_ENG.IsMatch(textBox_runcard.Text) && e.KeyChar == 13)
                {
                    Var.str_RUNcard = textBox_runcard.Text;
                    Var.str_UserID = "ENG";


                    if (Var.Ethernet == false)
                    {
                        MessageBox.Show("網路連線失敗,請手動選擇檔案！", "網路異常！");
                    }

                    DateCode.DateRefresh(Var.Date, Var.Today_Date);

                    comboBox1.Items.Clear();
                    Var.Model_select = Access_data.Access_Select("select Model from Model order by Model ASC");
                    for (int i = 0; i < Var.Model_select.Rows.Count; i++)
                        comboBox1.Items.Add(Var.Model_select.Rows[i][0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("textBox_runcard_KeyPress : " + ex.ToString());
                MPU.WriteErrorCode("L233", "textBox_runcard_KeyPress : " + ex.Message.ToString());
            }
        }

        private Label[] tittle_l = new Label[4];
        private string[,] tittle_name = new string[2, 4] { { "項目", "新設值", "原始值", "下限  /  上限" }, { "Items", "New", "Origin", "Min  /  Max" } };
        private Label[] Column_l = new Label[27];
        private Label Max_sting;
        private string[,] Max_sting_name = new string[2, 1] { { "最長為127個字符" }, { "Maximum 127 characters" } };

        private void Language_button_Click(object sender, EventArgs e)
        {
            try
            {
                //預設中文(Var.English = true)
                if (!Var.English)
                {
                    //轉英文
                    Var.English = true;
                    label2.Text = "Model";
                    label3.Text = "Model List";
                    Model_send_button.Text = "Send marking config";
                    Model_config_button.Text = "Model Config Setting";
                    Model_delete_button.Text = "Delete Model Config";
                    labelL1.Text = "Line 1";
                    labelL2.Text = "Line 2";
                    labelL3.Text = "Line 3";
                    labelL4.Text = "Line 4";
                    labelL5.Text = "Line 5";
                    labelL6.Text = "Line 6";
                    labelL7.Text = "Line 7";
                    labelL8.Text = "Line 8";
                    labelL9.Text = "Line 9";
                    labelL10.Text = "Line 10";
                    labelL11.Text = "Logo";
                    Exit_button.Text = "Exit";
                    Language_button.Text = "中文";

                    Label label_Model_Config = form2.Controls.Find("label1", true)[0] as Label;
                    label_Model_Config.Text = "Model Config";
                    Label label_Model_Name = form2.Controls.Find("label2", true)[0] as Label;
                    label_Model_Name.Text = "Model Name";
                    Button button_New = form2.Controls.Find("button1", true)[0] as Button;
                    button_New.Text = "New";
                    Button button_Save = form2.Controls.Find("button2", true)[0] as Button;
                    button_Save.Text = "Save";
                    Button button_Read = form2.Controls.Find("button4", true)[0] as Button;
                    button_Read.Text = "Read";
                    Button button_Exit = form2.Controls.Find("button3", true)[0] as Button;
                    button_Exit.Text = "Exit";

                    TabControl tap = form2.Controls.Find("tabControl1", true)[0] as TabControl;
                    tap.TabPages[0].Text = " Tray";
                    tap.TabPages[1].Text = "Common";
                    tap.TabPages[2].Text = "Line 1";
                    tap.TabPages[3].Text = "Line 2";
                    tap.TabPages[4].Text = "Line 3";
                    tap.TabPages[5].Text = "Line 4";
                    tap.TabPages[6].Text = "Line 5";
                    tap.TabPages[7].Text = "Line 6";
                    tap.TabPages[8].Text = "Line 7";
                    tap.TabPages[9].Text = "Line 8";
                    tap.TabPages[10].Text = "Line 9";
                    tap.TabPages[11].Text = "Line 10";
                    tap.TabPages[12].Text = " Logo";
                    tap.TabPages[13].Text = "Counter";
                }
                else
                {
                    //轉中文
                    Var.English = false;
                    label2.Text = "品別";
                    label3.Text = "品別清單";
                    Model_send_button.Text = "傳送刻印參數";
                    Model_config_button.Text = "品別參數設定";
                    Model_delete_button.Text = "刪除品別參數";
                    labelL1.Text = "第一行文字";
                    labelL2.Text = "第二行文字";
                    labelL3.Text = "第三行文字";
                    labelL4.Text = "第四行文字";
                    labelL5.Text = "第五行文字";
                    labelL6.Text = "第六行文字";
                    labelL7.Text = "第七行文字";
                    labelL8.Text = "第八行文字";
                    labelL9.Text = "第九行文字";
                    labelL10.Text = "第十行文字";
                    labelL11.Text = "Logo";
                    Exit_button.Text = "離開";
                    Language_button.Text = "English";

                    Label label_Model_Config = form2.Controls.Find("label1", true)[0] as Label;
                    label_Model_Config.Text = "品別參數設定";
                    Label label_Model_Name = form2.Controls.Find("label2", true)[0] as Label;
                    label_Model_Name.Text = "品別名稱";
                    Button button_New = form2.Controls.Find("button1", true)[0] as Button;
                    button_New.Text = "新增參數";
                    Button button_Save = form2.Controls.Find("button2", true)[0] as Button;
                    button_Save.Text = "確認儲存";
                    Button button_Read = form2.Controls.Find("button4", true)[0] as Button;
                    button_Read.Text = "讀取資料";
                    Button button_Exit = form2.Controls.Find("button3", true)[0] as Button;
                    button_Exit.Text = "離開";

                    TabControl tap = form2.Controls.Find("tabControl1", true)[0] as TabControl;
                    tap.TabPages[0].Text = "底座設定";
                    tap.TabPages[1].Text = "通用設定";
                    tap.TabPages[2].Text = "第一行";
                    tap.TabPages[3].Text = "第二行";
                    tap.TabPages[4].Text = "第三行";
                    tap.TabPages[5].Text = "第四行";
                    tap.TabPages[6].Text = "第五行";
                    tap.TabPages[7].Text = "第六行";
                    tap.TabPages[8].Text = "第七行";
                    tap.TabPages[9].Text = "第八行";
                    tap.TabPages[10].Text = "第九行";
                    tap.TabPages[11].Text = "第十行";
                    tap.TabPages[12].Text = " Logo";
                    tap.TabPages[13].Text = "計數器";
                }

                //標題
                for (int i = 1; i <= 100; i++)
                {
                    tittle_l[(i - 1) % 4] = form2.Controls.Find("tittle" + i.ToString(), true)[0] as Label;
                    tittle_l[(i - 1) % 4].Text = tittle_name[Convert.ToInt32(Var.English), (i - 1) % 4];
                }

                Array.Clear(Column_l, 0, Column_l.Length);
                //底座設定
                for (int i = 1; i <= 8; i++)
                {
                    Column_l[i - 1] = form2.Controls.Find("columnT" + i.ToString(), true)[0] as Label;
                    Column_l[i - 1].Text = Var.Items_tittle[Convert.ToInt32(Var.English)][0][i - 1];

                    Column_l[i - 1].Font = Text_font(Var.English, Column_l[i - 1].Text.Replace("(", "").Replace(")", "").Length);
                }

                Array.Clear(Column_l, 0, Column_l.Length);
                //通用設定
                for (int i = 1; i <= 18; i++)
                {
                    Column_l[i - 1] = form2.Controls.Find("columnU" + i.ToString(), true)[0] as Label;
                    Column_l[i - 1].Text = Var.Items_tittle[Convert.ToInt32(Var.English)][1][i - 1];

                    Column_l[i - 1].Font = Text_font(Var.English, Column_l[i - 1].Text.Replace("(", "").Replace(")", "").Length);
                }

                Array.Clear(Column_l, 0, Column_l.Length);
                //文字行
                for (int i = 1; i <= 10; i++)
                {
                    for (int j = 1; j < 27; j++)
                    {
                        Column_l[j - 1] = form2.Controls.Find("columnL" + i.ToString() + "_" + j.ToString(), true)[0] as Label;
                        Column_l[j - 1].Text = Var.Items_tittle[Convert.ToInt32(Var.English)][2][j - 1];

                        Column_l[j - 1].Font = Text_font(Var.English, Column_l[j - 1].Text.Replace("(", "").Replace(")", "").Length);
                    }

                    Column_l[26] = form2.Controls.Find("columnL" + i.ToString() + "_27", true)[0] as Label;
                    Column_l[26].Text = Var.Items_tittle[Convert.ToInt32(Var.English)][2][26];

                    Column_l[26].Font = new Font("微軟正黑體", (float)11.25, FontStyle.Bold);

                    Max_sting = form2.Controls.Find("labelL" + i.ToString() + "_27", true)[0] as Label;
                    Max_sting.Text = Max_sting_name[Convert.ToInt32(Var.English), 0];
                }

                Array.Clear(Column_l, 0, Column_l.Length);
                //標識
                for (int i = 1; i <= 22; i++)
                {
                    Column_l[i - 1] = form2.Controls.Find("columnL11_" + i.ToString(), true)[0] as Label;
                    Column_l[i - 1].Text = Var.Items_tittle[Convert.ToInt32(Var.English)][3][i - 1];

                    Column_l[i - 1].Font = Text_font(Var.English, Column_l[i - 1].Text.Replace("(", "").Replace(")", "").Length);
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("LBC", "Language_button_Click," + ex.Message);
            }
        }

        //設定標題字體
        private Font Text_font(bool English, int text_length)
        {
            try
            {
                if (English)
                {
                    if (text_length < 12)
                        return new Font("微軟正黑體", (float)9.25, FontStyle.Bold);
                    else
                        return new Font("微軟正黑體", (float)6.75, FontStyle.Bold);
                }
                else
                {
                    if (text_length < 5)
                        return new Font("微軟正黑體", (float)11.25, FontStyle.Bold);
                    else
                        return new Font("微軟正黑體", (float)9, FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("T2", "Text_font," + ex.Message);

                return new Font("微軟正黑體", (float)9.25, FontStyle.Bold);
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            try
            {
                if (UserId_Regex_ENG.IsMatch(textBox_runcard.Text))
                    //判斷是否有刪除或是加入model
                    if (comboBox1.Items.Count > 0 && Var.Model_select.Rows.Count > comboBox1.Items.Count)
                    {
                        for (int i = 0; i < Var.Model_select.Rows.Count; i++)
                            if (comboBox1.FindStringExact(Var.Model_select.Rows[i][0].ToString()) == -1)
                            {
                                comboBox1.Items.Clear();

                                for (int j = 0; j < Var.Model_select.Rows.Count; j++)
                                    comboBox1.Items.Add(Var.Model_select.Rows[j][0].ToString());

                                break;
                            }
                    }
                    else if (comboBox1.Items.Count > 0 && Var.Model_select.Rows.Count < comboBox1.Items.Count)
                    {
                        for (int j = 0; j < comboBox1.Items.Count; j++)
                            if (Var.Model_select.Select("Model='" + comboBox1.Items[j].ToString() + "'").Length == 0)
                                comboBox1.Items.Remove(comboBox1.Items[j].ToString());
                    }

                if (comboBox1.Text == "")
                {
                    Model_config_button.Enabled = false;
                    Model_send_button.Enabled = false;

                    for (int i = 0; i < Var.Marking_string.Length; i++)
                        Var.Marking_string[i].Text = "";
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("O1", "Form1_Activated," + ex.Message);
            }
        }

        private void txt_load()
        {
            try
            {
                if (!File.Exists(System.Environment.CurrentDirectory + @"\Language\Items.txt")) Default_Item.Items_txt();
                if (!File.Exists(System.Environment.CurrentDirectory + @"\Config\Limit.txt")) Default_Item.Limit_txt();
                if (!File.Exists(System.Environment.CurrentDirectory + @"\Config\LimitName.txt")) Default_Item.LimitName_txt();


                //標題
                if (File.Exists(System.Environment.CurrentDirectory + @"\Language\Items.txt"))
                {
                    string line;
                    using (System.IO.StreamReader file = new System.IO.StreamReader(System.Environment.CurrentDirectory + @"\Language\Items.txt"))
                    {
                        int i = 0;
                        int j = 0;
                        int k = 0;

                        while ((line = file.ReadLine()) != null)
                        {
                            if (line != "" && i % 2 == 0)
                            {
                                Var.Items_tittle[0][j][k] = line;
                            }
                            else if (line != "" && i % 2 == 1)
                            {
                                Var.Items_tittle[1][j][k] = line;
                                k++;
                            }
                            else if ((line == "" || line == "\r") && i % 2 == 1)
                            {
                                j++;
                                k = 0;
                            }

                            i++;

                            Application.DoEvents();
                        }
                    }
                }

                //上下限數值
                if (File.Exists(System.Environment.CurrentDirectory + @"\Config\Limit.txt"))
                {
                    string line;
                    using (System.IO.StreamReader file = new System.IO.StreamReader(System.Environment.CurrentDirectory + @"\Config\Limit.txt"))
                    {
                        int i = 0;
                        int j = 0;

                        while ((line = file.ReadLine()) != null)
                        {
                            if (line != "")
                            {
                                Var.Limit[i][j] = line.Split(',');
                                j++;
                            }
                            else if (line == "" || line == "\r")
                            {
                                i++;
                                j = 0;
                            }

                            Application.DoEvents();
                        }
                    }
                }

                //上下限顯示
                if (File.Exists(System.Environment.CurrentDirectory + @"\Config\LimitName.txt"))
                {
                    string line;
                    using (System.IO.StreamReader file = new System.IO.StreamReader(System.Environment.CurrentDirectory + @"\Config\LimitName.txt"))
                    {
                        int i = 0;
                        int j = 0;

                        while ((line = file.ReadLine()) != null)
                        {
                            if (line != "")
                            {
                                Var.Limitname[i][j] = line;
                                j++;
                            }
                            else if (line == "" || line == "\r")
                            {
                                i++;
                                j = 0;
                            }

                            Application.DoEvents();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("M1", "txt_load," + ex.Message);
            }
        }


        //標題閃爍
        bool t = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!Initialization_state)
                    if (t)
                    {
                        label_system.BackColor = Color.FromArgb(118, 165, 235);
                        label_system.ForeColor = Color.White;
                    }
                    else
                    {
                        label_system.BackColor = SystemColors.Control;
                        label_system.ForeColor = Color.FromArgb(118, 165, 235);
                    }


                if (send_command)
                    if (t)
                    {
                        label_system.BackColor = Color.Red;
                        label_system.ForeColor = Color.Black;
                    }
                    else
                    {
                        label_system.BackColor = Color.LightGoldenrodYellow;
                        label_system.ForeColor = Color.Black;
                    }

                t = !t;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("TT", "timer1_Tick," + ex.Message);
            }
        }


        private static Regex BackUp_Regex = new Regex(@"^Laser #3_\d{4}(-\d{2}){2}_Backup$");
        private void FileDailyBackup(object State)
        {
            try
            {
                if (int.Parse(DateTime.Now.ToString("HHmmss")) == 000000)
                {
                    string Backup_path = @"\\192.168.1.101\saw\雷射刻印機\" + Var.str_DeviceNO + "_" + DateTime.Now.ToString("yyyy-MM-dd") + "_Backup";
                    DirectoryInfo dir = new DirectoryInfo(Backup_path + @"\..\");

                    //確認備分資料夾是否存在
                    foreach (DirectoryInfo di in dir.GetDirectories())
                    {
                        if (BackUp_Regex.IsMatch(di.Name))
                        {
                            foreach (FileInfo fi in di.GetFiles())
                            {
                                fi.Delete();
                            }
                            di.Delete();
                            
                            break;
                        }
                    }

                    dir = new DirectoryInfo(@"C:\Profile_Keyence\SystemProfile");
                    Directory.CreateDirectory(Backup_path);

                    foreach (FileInfo fi in dir.GetFiles("*.mdb"))
                    {
                        File.Copy(fi.FullName, Backup_path + "\\" + fi.Name, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("FD", "FileDailyBackup," + ex.Message);
            }
        }

        public static void DBupdate()
        {
            try
            {
                //上傳資料全部
                SqlConnection connection_Spool = new SqlConnection(Var.strCon);
                connection_Spool.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection_Spool;
                        command.CommandType = CommandType.Text;

                        command.CommandText = @"INSERT INTO [dbo].[tb_EAP_RecipeLog]
                                                                            ([Did]
                                                                            ,[UserID]
                                                                            ,[RunCard]
                                                                            ,[Model]
                                                                            ,[testRecipeID]
                                                                            ,[laserRecipeID]
                                                                            ,[laserRecipeBody]
                                                                            ,[systime])
                                                                            VALUES
                                                                            ('" + Var.str_DeviceNO +
                                                                            "','" + "" +
                                                                            "','" + Var.str_RUNcard +
                                                                            "','" + Var.recipe +
                                                                            "','" + "" +
                                                                            "','" + Var.Model +
                                                                            "','" + Var.str_marking +
                                                                            "','" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "')";

                        
                        int recordsAffected;
                        recordsAffected = command.ExecuteNonQuery();
                    }
                }
                catch (Exception EXSQL)
                {
                    //傳遞失敗表示連線仍然有問題，因此連線判定false

                    MPU.WriteErrorCode("F0170", "DBupdate," + EXSQL.Message);
                }

                //全部傳遞完畢要初始化Spool暫存器旗標
                connection_Spool.Close();
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("F0046", "DBupdate_open," + ex.Message);
            }
        }

        #region 設定並讀取規則Excel
        private void Load_Excel()
        {
            try
            {
                String fileName = "./Date_Code_Rules.xls";
                DataTable dt;
                IWorkbook workbook = null; //新建IWorkbook對象 
                FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                if (fileName.IndexOf(".xlsx") > 0) // 2007版本 
                {
                    //workbook = new XSSFWorkbook(fileStream); //xlsx數據讀入workbook 
                }
                else if (fileName.IndexOf(".xls") > 0) // 2003版本 
                {
                    workbook = new HSSFWorkbook(fileStream); //xls數據讀入workbook 
                }

                for (int k = 1; k < 7; k++)
                {
                    dt = new DataTable();
                    ISheet sheet = workbook.GetSheetAt(k); //獲取第一個工作表 
                    IRow row;// = sheet.GetRow(0); //新建當前工作表行數據 
                    row = sheet.GetRow(0); //row讀入頭部
                    if (row != null)
                    {
                        for (int m = 0; m < row.LastCellNum; m++) //表頭 
                        {
                            string cellValue = row.GetCell(m).ToString(); //獲取i行j列數據 
                            dt.Columns.Add(cellValue);
                        }
                    }
                    for (int i = 1; i <= sheet.LastRowNum; i++) //對工作表每一行 
                    {
                        System.Data.DataRow dr = dt.NewRow();
                        row = sheet.GetRow(i); //row讀入第i行數據 
                        if (row != null)
                        {
                            for (int j = 0; j < row.LastCellNum; j++) //對工作表每一列 
                            {
                                string cellValue = row.GetCell(j).ToString(); //獲取i行j列數據 
                                dr[j] = cellValue;
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                    Var.Rules_Table.Tables.Add(dt);                    
                }
                fileStream.Close();
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("", "LoadExcel," + ex.Message);
            }
        }
        #endregion       
    }
}
