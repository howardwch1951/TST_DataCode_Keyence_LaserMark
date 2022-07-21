using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LaserMark
{
    public partial class InputBox : Form
    {
        public InputBox()
        {
            InitializeComponent();
        }

        string Model_name = Var.Model;
        DataRow[] Model_row;

        private void button2_Click(object sender, EventArgs e)
        {
            Model_row = Var.Model_select.Select("Model='" + textBox1.Text + "'");

            Var.Rules = Access_data.Access_Select_Keyence_Rules("select * from Rules where Model='" + Var.Model + "' order by Block_No ASC;");
            if (textBox1.Text != "" && textBox1.Text != null)
                if (Model_row.Length == 0)
                {
                    Model_name = textBox1.Text;
                    for (int i = 0; i < Var.Rules.Rows.Count; i++)
                    {
                        Access_data.Access_Select_Keyence_Rules("insert into Rules (Model, Block_No, Rule_Code) values('" + Model_name + "','"
                                                                                                                         + Var.Rules.Rows[i]["Block_No"] + "','"
                                                                                                                         + Var.Rules.Rows[i]["Rule_Code"] + "');");
                    }
                }
                else
                    MessageBox.Show("品別名稱重複!", "名稱重複", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show("品別名稱不可為空!", "名稱錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public string GetMsg()
        {
            return Model_name;
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(Owner.Location.X + Owner.Width / 2 - this.Width / 2, Owner.Location.Y + Owner.Height / 2 - this.Height / 2);
            this.MaximizeBox = false;

            if (Var.English)
            {
                label1.Text = "Input new Model name";
                button2.Text = "Yes";
                button1.Text = "No";
            }
            else
            {
                label1.Text = "輸入新品別名稱";
                button2.Text = "確定";
                button1.Text = "取消";
            }

            textBox1.Focus();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //不可将以下字符用于标题中。
                //半角字符：“¥”“/”“:”“*”“?”“<”“>”“|”“'”“.”“,”“ ” （空格）
                //全角字符：“　” （空格）
                if ((e.KeyChar == 92 || e.KeyChar == 47 || e.KeyChar == 58 || e.KeyChar == 42 || e.KeyChar == 63 || e.KeyChar == 60 || e.KeyChar == 62 || e.KeyChar == 124 || e.KeyChar == 39 || e.KeyChar == 46 || e.KeyChar == 44 || e.KeyChar == 32 || e.KeyChar == 12288) && (sender as TextBox).Text.Length < 26)
                {
                    e.Handled = true;
                    toolTip1.ToolTipTitle = "不可包含下列任意字元:";
                    toolTip1.Show("\\  /:*?<　>|'.,", textBox1, new Point(textBox1.Location.X + textBox1.Width / 2, textBox1.Height), 2000);
                }

                if ((sender as TextBox).Text.Length >= 26 && e.KeyChar != 8)
                {
                    e.Handled = true;

                    toolTip1.ToolTipTitle = "標題名稱限制";
                    toolTip1.Show("標題名稱最多26個字", textBox1, new Point(textBox1.Location.X + textBox1.Width / 2, textBox1.Height), 2000);
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("t1", "textBox1_KeyPress," + ex.Message);
            }
        }

        private void InputBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            Var.Form3_Close = true;
        }
    }
}

