using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LaserMark
{
    public partial class Tabpage_Counter : UserControl
    {
        string[,] button_name = new string[2, 2] { { "修改", "Revise" }, { "移除", "Remove" } };
        public Tabpage_Counter()
        {
            InitializeComponent();

            counter0_tittle1.Text = Var.Items_tittle[Convert.ToInt32(Var.English)][4][1];
            counter0_tittle2.Text = Var.Items_tittle[Convert.ToInt32(Var.English)][4][2];
            counter0_tittle3.Text = Var.Items_tittle[Convert.ToInt32(Var.English)][4][3];
            counter0_tittle4.Text = Var.Items_tittle[Convert.ToInt32(Var.English)][4][4];
            counter0_tittle5.Text = Var.Items_tittle[Convert.ToInt32(Var.English)][4][5];
            counter0_tittle6.Text = Var.Items_tittle[Convert.ToInt32(Var.English)][4][6];
            counter0_tittle7.Text = Var.Items_tittle[Convert.ToInt32(Var.English)][4][7];
            counter0_tittle8.Text = Var.Items_tittle[Convert.ToInt32(Var.English)][4][8];

            button0_change.Text = button_name[0, Convert.ToInt32(Var.English)];
            button0_remove.Text = button_name[1, Convert.ToInt32(Var.English)];

            Var.Counter_No++;
        }

        private void Tabpage_Counter_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("", (sender as Form).Name + "Tabpage_Counter_Load," + ex.Message);
            }
        }
        Label lable_value;
        //基本初始化計數器(手動新增)
        public void Counter_Set(int No)
        {
            try
            {
                this.Name += No.ToString();
                label_counter0.Text = "Counter# " + No.ToString();

                this.Controls.Find("tableLayoutPanel_counter0_1", true)[0].Name = "tableLayoutPanel_counter" + No.ToString() + "_1";
                this.Controls.Find("tableLayoutPanel_counter0_2", true)[0].Name = "tableLayoutPanel_counter" + No.ToString() + "_2";

                this.Controls.Find("panel_counter0_1", true)[0].Name = "panel_counter" + No.ToString() + "_1";

                lable_value = this.Controls.Find("label_counter0", true)[0] as Label;
                lable_value.Name = "label_counter" + No.ToString();

                for (int i = 1; i <= 8; i++)
                {
                    this.Controls.Find("counter0_tittle" + i.ToString(), true)[0].Name = "counter" + No.ToString() + "_tittle" + i.ToString();

                    lable_value = this.Controls.Find("counter0_value" + i.ToString(), true)[0] as Label;
                    lable_value.Name = "counter" + No.ToString() + "_value" + i.ToString();
                }

                this.Controls.Find("button0_change", true)[0].Name = "button" + No.ToString() + "_change";
                this.Controls.Find("button0_remove", true)[0].Name = "button" + No.ToString() + "_remove";
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("", "Counter_Set_(int No)," + ex.Message);
            }
        }
        
        //動態新增計數器(可填入值)
        public void Counter_Set(int No, TextBox[] value)
        {
            try
            {
                this.Name += No.ToString();

                this.Controls.Find("tableLayoutPanel_counter0_1", true)[0].Name = "tableLayoutPanel_counter" + No.ToString() + "_1";
                this.Controls.Find("tableLayoutPanel_counter0_2", true)[0].Name = "tableLayoutPanel_counter" + No.ToString() + "_2";

                this.Controls.Find("panel_counter0_1", true)[0].Name = "panel_counter" + No.ToString() + "_1";

                lable_value= this.Controls.Find("label_counter0", true)[0] as Label;
                lable_value.Name = "label_counter" + No.ToString();
                lable_value.Text = "Counter# " + value[0].Text;

                for (int i = 1; i <= 8; i++)
                {
                    this.Controls.Find("counter0_tittle" + i.ToString(), true)[0].Name = "counter" + No.ToString() + "_tittle" + i.ToString();

                    lable_value = this.Controls.Find("counter0_value" + i.ToString(), true)[0] as Label;
                    lable_value.Name = "counter" + No.ToString() + "_value" + i.ToString();
                    lable_value.Text = value[i].Text;
                }

                this.Controls.Find("button0_change", true)[0].Name = "button" + No.ToString() + "_change";
                this.Controls.Find("button0_remove", true)[0].Name = "button" + No.ToString() + "_remove";
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("", "Counter_Set(int No, TextBox[] value)," + ex.Message);
            }
        }

        //重新改變所有物件編號
        public void Counter_ReSet(int No, TextBox[] value)
        {
            try
            {
                this.Name = "Tabpage_Counter" + value[0].Text;

                this.Controls.Find("tableLayoutPanel_counter" + No.ToString() + "_1", true)[0].Name = "tableLayoutPanel_counter" + value[0].Text + "_1";
                this.Controls.Find("tableLayoutPanel_counter" + No.ToString() + "_2", true)[0].Name = "tableLayoutPanel_counter" + value[0].Text + "_2";

                this.Controls.Find("panel_counter" + No.ToString() + "_1", true)[0].Name = "panel_counter" + value[0].Text + "_1";

                lable_value = this.Controls.Find("label_counter" + No.ToString(), true)[0] as Label;
                lable_value.Name = "label_counter" + value[0].Text;
                lable_value.Text = "Counter# " + value[0].Text;

                for (int i = 1; i <= 8; i++)
                {
                    this.Controls.Find("counter" + No.ToString() + "_tittle" + i.ToString(), true)[0].Name = "counter" + value[0].Text + "_tittle" + i.ToString();

                    lable_value = this.Controls.Find("counter" + No.ToString() + "_value" + i.ToString(), true)[0] as Label;
                    lable_value.Name = "counter" + value[0].Text + "_value" + i.ToString();
                    lable_value.Text = value[i].Text;
                }

                this.Controls.Find("button" + No.ToString() + "_change", true)[0].Name = "button" + value[0].Text + "_change";
                this.Controls.Find("button" + No.ToString() + "_remove", true)[0].Name = "button" + value[0].Text + "_remove";
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("", "Counter_ReSet," + ex.Message);
            }
        }
        //傳送Tabpage_Counter的數值給Form5
        public void Counter_Get(string No)
        {
            try
            {
                for (int i = 1; i <= 8; i++)
                    Form1.form5.counter_value[i] = this.Controls.Find("counter" + No + "_value" + i.ToString(), true)[0].Text;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("", "Counter_Get" + ex.Message);
            }
        }

        private void button0_change_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.form5.counter_value[0] = (sender as Button).Name.Replace("button", "").Replace("_change", "");
                Counter_Get(Form1.form5.counter_value[0]);

                DialogResult result = Form1.form5.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Var.Counter_num[Convert.ToInt32(Form1.form5.counter_value[0])] = "0";
                    Label counter_No = this.Controls.Find("label_counter" + Form1.form5.counter_value[0], true)[0] as Label;


                    TextBox[] value = Form1.form5.Text_get();

                    Counter_ReSet(Convert.ToInt32(Form1.form5.counter_value[0]), value);
                    for (int i = 1; i <= 8; i++)
                        this.Controls.Find("counter" + value[0].Text + "_value" + i.ToString(), true)[0].Text = value[i].Text;

                    Var.Counter_num[Convert.ToInt32(value[0].Text)] = "1";
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("", (sender as Button).Name + "button0_change_Click," + ex.Message);
            }
        }

        private void button0_remove_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result;

                if (Var.English)
                    result = MessageBox.Show("Remove Counter# " + (sender as Button).Name.Replace("button", "").Replace("_remove", "") + "?", "Remove Counter Setting", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                else
                    result = MessageBox.Show("確定移除Counter# " + (sender as Button).Name.Replace("button", "").Replace("_remove", "") + "嗎?", "移除計數器設定", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    Var.Counter_No--;
                    Var.Counter_num[Convert.ToInt32(label_counter0.Text.Replace("Counter# ", ""))] = "0";
                    this.Dispose();

                    Form1.form2.Counter_AutoArrange();
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("", (sender as Button).Name + "_button0_remove_Click," + ex.Message);
            }
        }
    }
}
