using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LaserMark
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        private string[,] tittle_name = new string[2, 4] { { "項目", "新設值", "原始值", "下限  /  上限" }, { "Items", "New", "Origin", "Min  /  Max" } };
        private string[,] button_name = new string[2, 2] { { "確定", "取消" }, { "Ok", "Cancel" } };

        public string[] counter_value = new string[9];

        Label[] Column_l = new Label[9];
        TextBox[] Text_l = new TextBox[9];
        Label[] LabelOL_l = new Label[9];
        Label[] LabelL_l = new Label[9];

        DataRow[] counter;
        private void Form5_Load(object sender, EventArgs e)
        {
            try
            {
                this.SetDesktopLocation(Owner.Location.X + Owner.Width / 2 - this.Width / 2, Owner.Location.Y + Owner.Height / 2 - this.Height / 2);
                this.MaximizeBox = false;

                tittle1.Text = tittle_name[Convert.ToInt32(Var.English), 0];
                tittle2.Text = tittle_name[Convert.ToInt32(Var.English), 1];
                tittle3.Text = tittle_name[Convert.ToInt32(Var.English), 2];
                tittle4.Text = tittle_name[Convert.ToInt32(Var.English), 3];

                button_change.Text = button_name[Convert.ToInt32(Var.English),0];
                button_exit.Text = button_name[Convert.ToInt32(Var.English), 1];

                if (Var.Counter_OldValue[Convert.ToInt32(counter_value[0])] != "" && Var.Counter_OldValue[Convert.ToInt32(counter_value[0])] != null)
                    counter = Var.Model_counter.Select("Counter_No=" + Var.Counter_OldValue[Convert.ToInt32(counter_value[0])]);

                for (int i = 1; i <= 9; i++)
                {
                    Column_l[i - 1] = this.Controls.Find("columnC" + i.ToString(), true)[0] as Label;
                    Column_l[i - 1].Text = Var.Items_tittle[Convert.ToInt32(Var.English)][4][i - 1];

                    Column_l[i - 1].Font = Text_font(Var.English, Column_l[i - 1].Text.Replace("(", "").Replace(")", "").Length);

                    Text_l[i - 1] = this.Controls.Find("textBoxC" + i.ToString(), true)[0] as TextBox;
                    Text_l[i - 1].AutoSize = false;
                    Text_l[i - 1].Height = 23;
                    Text_l[i - 1].Font = new Font("微軟正黑體", (float)11, FontStyle.Bold);
                    Text_l[i - 1].KeyPress += new KeyPressEventHandler(Num_key_C);
                    Text_l[i - 1].TextChanged += new EventHandler(Num_limit_C);
                    Text_l[i - 1].MaxLength = Convert.ToInt32(Var.Limit[4][i - 1][2]);
                    Text_l[i - 1].Text = Convert.ToInt64(counter_value[i - 1]).ToString();

                    LabelOL_l[i - 1] = this.Controls.Find("labelOC" + i.ToString(), true)[0] as Label;
                    LabelOL_l[i - 1].Text = "";
                    if (counter != null && counter.Length > 0) LabelOL_l[i - 1].Text = Convert.ToInt64(counter[0][i]).ToString();

                    LabelL_l[i - 1] = this.Controls.Find("labelC" + i.ToString(), true)[0] as Label;
                    LabelL_l[i - 1].Text = Var.Limitname[4][i - 1];
                }

                this.ActiveControl = tittle1;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("F5L", "Form5_Load," + ex.Message);
            }
        }

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

        //計數器設定輸入數值判斷
        private void Num_key_C(object sender, KeyPressEventArgs e)
        {
            try
            {
                //只能輸入數字"0~9"和"-"和"backspace鍵"
                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 45 || e.KeyChar == 8)
                    e.Handled = false;
                //限制部分能輸入"."
                else if (e.KeyChar == 46 && Var.Limit[4][Convert.ToInt32((sender as TextBox).Name.Replace("textBox", "").Substring(1)) - 1][3].IndexOf(".") > 0)
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("NKC", "Num_key_C," + ex.Message);
            }
        }

        //計數器設定數值判斷
        private void Num_limit_C(object sender, EventArgs e)
        {
            try
            {
                if ((sender as TextBox).Text != "" && Double.TryParse((sender as TextBox).Text, out double i))
                {
                    //正常double數值
                    if (Convert.ToDouble((sender as TextBox).Text) > Convert.ToDouble(Var.Limit[4][Convert.ToInt32((sender as TextBox).Name.Replace("textBox", "").Substring(1)) - 1][1]))
                        (sender as TextBox).BackColor = Color.Red;
                    else if (Convert.ToDouble((sender as TextBox).Text) < Convert.ToDouble(Var.Limit[4][Convert.ToInt32((sender as TextBox).Name.Replace("textBox", "").Substring(1)) - 1][0]))
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
                    if (this.Controls.Find("labelO" + (sender as TextBox).Name.Replace("textBox", ""), true)[0].Text != "")
                        (sender as TextBox).BackColor = Color.Red;
                    else
                        (sender as TextBox).BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("N1", "Num_limit_C" + ex.Message);
            }
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("F5C", "Form5_FormClosing," + ex.Message);
            }
        }
        //傳送Form5的數值給Tabpage_Counter
        public TextBox[] Text_get()
        {
            try
            {
                Text_l[2].Text = Text_l[3].Text;
                return Text_l;
            }
            catch (Exception  ex)
            {
                MPU.WriteErrorCode("", "Text_get," + ex.Message);
                return new TextBox[9];
            }
        }

        private void button_change_Click(object sender, EventArgs e)
        {
            try
            {
                bool illegal = false;

                for (int i = 0; i < 10; i++)
                {
                    if (Var.Counter_num[Convert.ToInt32(Text_l[0].Text)] == "1" && counter_value[0] != Text_l[0].Text)
                    {
                        Text_l[0].BackColor = Color.Red;
                        MessageBox.Show("計數器已重複", "編號重複", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        break;
                    }
                }


                for (int i = 0; i <= 8; i++)
                {
                    if (Text_l[i].BackColor == Color.Red)
                    {
                        illegal = true;

                        //將焦點移至該textbox
                        this.ActiveControl = Text_l[i];
                        Text_l[i].SelectAll();
                        break;
                    }
                }

                if (!illegal)
                {
                    if (LabelOL_l[0].Text != "")
                    {
                        Var.Counter_OldValue[Convert.ToInt32(LabelOL_l[0].Text)] = "";
                        Var.Counter_OldValue[Convert.ToInt32(Text_l[0].Text)] = LabelOL_l[0].Text;
                    }


                    this.DialogResult = DialogResult.OK;
                    this.Hide();
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("BC", "button_change_Click," + ex.Message);
            }
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                
                this.Hide();
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("BE", "button_exit_Click," + ex.Message);
            }
        }

        private void textBoxC7_Enter(object sender, EventArgs e)
        {
            try
            {
                if (Var.English)
                {
                    toolTip1.ToolTipTitle = "Value:";
                    toolTip1.Show("0：Sensor\r\n1：Forcibly\r\n2：Power On\r\n3：Chang Prog\r\n4：Daily\r\n", textBoxC7, new Point(25, 25));
                }
                else
                {
                    toolTip1.ToolTipTitle = "設定值:";
                    toolTip1.Show("0：傳感器\r\n1：強制\r\n2：電源接通時\r\n3：切換程序時\r\n4：日期更換時\r\n", textBoxC7, new Point(30, 25));
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("LME", "labelC7_MouseEnter," + ex.Message);
            }
        }

        private void textBoxC7_Leave(object sender, EventArgs e)
        {
            try
            {
                toolTip1.Hide(textBoxC7);
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("LME", "textBoxC7_Leave," + ex.Message);
            }
        }

        private void textBoxC8_Enter(object sender, EventArgs e)
        {
            try
            {
                if (Var.English)
                {
                    toolTip1.ToolTipTitle = "Value:";
                    toolTip1.Show("0：Sensor\r\n1：Each Marking", textBoxC8, new Point(25, 25));
                }
                else
                {
                    toolTip1.ToolTipTitle = "設定值:";
                    toolTip1.Show("0：傳感器\r\n1：每次刻印", textBoxC8, new Point(30, 25));
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("LME", "labelC8_MouseEnter," + ex.Message);
            }
        }

        private void textBoxC8_Leave(object sender, EventArgs e)
        {
            try
            {
                toolTip1.Hide(textBoxC8);
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("LME", "textBoxC8_Leave," + ex.Message);
            }
        }
    }
}
