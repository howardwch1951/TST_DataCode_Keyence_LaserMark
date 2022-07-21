using DateCode_Dll;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace LaserMark
{
    public partial class TST_DateCode_Setting : Form
    {
        public TST_DateCode_Setting()
        {
            InitializeComponent();
        }

        private static Label[] Form2_Label;
        private static Label[] Space_Label;
        private static ComboBox[] Form2_ComboBox;
        private static Button[] Tab_Button = new Button[6];
        private static Button[] Border;
        private static int Form2_ComboBox_Count;
        private static int Loaded_Count;
        int k;
        private static bool S_Exist;
        private static String Check_Result;

        private void TST_DateCode_Setting_Load(object sender, EventArgs e)
        {
            try
            {
                this.Owner = Var.TST_Form;
                //每次進入都是新的Rule，參數恢復預設值
                String[] Rules_split;
                Form2_ComboBox_Count = 0;
                Loaded_Count = 1;
                k = 0;//處理Rule控制兩個ComboBox時的參數
                S_Exist = true;

                //設定Title，Form位置
                this.Text = Form_tittle(Var.Button_Num);
                this.SetDesktopLocation(Owner.Location.X - (this.Width - Owner.Width), Owner.Location.Y);

                // 設定DataGridView的格式
                Reset_Datagridview(1);

                Border = new Button[DateCode.Current_Value[Var.Button_Num - 1].Length];

                // 將分類用的按鈕存入陣列
                //Array.Clear(Tab_Button, 0, Tab_Button.Length);
                for (int i = 0; i < Tab_Button.Length; i++)
                {
                    Tab_Button[i] = this.Controls.Find("tab_btn" + (i + 1).ToString(), true)[0] as Button;
                }
                Tab_Button[0].BackColor = Color.DarkGray;
                Tab_Button[0].Focus();

                // 動態生成ComboBoxc和Label，並存入ComboBox和Label的陣列
                Dynamic_Label_and_ComboBox();

                // 在ComboBox的第一個選項加入一個空白選項
                for (int i = 0; i < DateCode.Current_Value[Var.Button_Num - 1].Length; i++)
                {
                    Form2_ComboBox[i].Items.Add("");
                }

                // 將規則編號存入ComboBox
                for (int i = 0; i < DateCode.Current_Value[Var.Button_Num - 1].Length; i++)
                {
                    for (int j = 0; j < Var.Rules_Table.Tables["Table1"].Rows.Count; j++)
                    {
                        Form2_ComboBox[i].Items.Add(Var.Rules_Table.Tables["Table1"].Rows[j][0].ToString());
                    }
                }

                //切分Rule，放入ComboBox
                for (int i = 0; i < DateCode.Current_Value[Var.Button_Num - 1].Length; i++)
                {
                    if (!String.IsNullOrEmpty(DateCode.Rule_Value[Var.Button_Num - 1]))
                    {
                        Rules_split = DateCode.Rule_Value[Var.Button_Num - 1].Split(',');

                        if (Array.Exists(DateCode.Diff_Rule, element => element == Rules_split[k]))
                        {
                            Form2_ComboBox[i + 1].Enabled = false;
                            Form2_ComboBox[i + 1].Text = "";
                            Form2_ComboBox[i].Text = Rules_split[k];
                            k++;
                            i++;
                        }
                        else
                        {
                            Form2_ComboBox[i].Text = Rules_split[k];
                            k++;
                        }
                    }
                }

                //切分Current_Value，放入Label
                for (int i = 0; i < DateCode.Current_Value[Var.Button_Num - 1].Length; i++)
                {
                    Form2_Label[i].Text = DateCode.Current_Value[Var.Button_Num - 1].Substring(i, 1);
                    Var.Label_Text[i] = DateCode.Current_Value[Var.Button_Num - 1].Substring(i, 1);

                    Form2_ComboBox_Count++; // 計算有幾個ComboBox開啟
                }

                for (int i = 0; i < Form2_Label.Length; i++)
                {
                    S_Exist = S_Exist && Array.Exists(DateCode.Special_Char, element => element == Form2_Label[i].Text);
                }

                if (S_Exist)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Tab_Button[i].Enabled = false;
                    }

                    Tab_Btn_Click(Tab_Button[5], EventArgs.Empty);
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Tab_Button[i].Enabled = true;
                    }
                    Tab_Btn_Click(Tab_Button[0], EventArgs.Empty);
                }

                for (int i = 0; i < DateCode.Current_Value[Var.Button_Num - 1].Length; i++)
                {
                    if (!string.IsNullOrEmpty(Form2_ComboBox[i].Text))
                    {
                        Dynamic_Border(i);
                    }
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("", "TST_DateCode_Setting_Load," + ex.Message);
            }

            Loaded_Count--;
        }

        #region 設定DataGridView的格式
        private void Reset_Datagridview(int Table_Num)
        {
            //765-75-630=60 => 第一行
            dataGridView1.DataSource = Var.Rules_Table.Tables["Table" + (Table_Num)];
            dataGridView1.Columns[0].Width = 85;
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[0].HeaderText = "規則編號";
            if (dataGridView1.Rows.Count < 7)
            {
                dataGridView1.Columns[1].Width = 614;
            }
            else
            {
                dataGridView1.Columns[1].Width = 598;
            }

            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].HeaderText = "規則說明";
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", 12);
            dataGridView1.DefaultCellStyle.Font = new Font("微軟正黑體", 14);

            dataGridView1.FirstDisplayedScrollingRowIndex = 0;
            dataGridView1.ClearSelection();
        }
        #endregion

        #region 動態生成ComboBoxc和Label，並存入ComboBox和Label的陣列
        private void Dynamic_Label_and_ComboBox()
        {
            panel1.Controls.Clear();
            Form2_ComboBox = new ComboBox[DateCode.Current_Value[Var.Button_Num - 1].Length];
            Form2_Label = new Label[DateCode.Current_Value[Var.Button_Num - 1].Length];
            Var.Label_Text = new String[DateCode.Current_Value[Var.Button_Num - 1].Length];
            Space_Label = new Label[1];
            // 計算ComboBox和Label起始位置的x座標
            double x;
            bool isExceed = false;
            if ((DateCode.Current_Value[Var.Button_Num - 1].Length * 64 - 4) < panel1.Width)
            {
                if (DateCode.Current_Value[Var.Button_Num - 1].Length % 2 != 0)
                {
                    x = 349.5 - (64 * (double)(DateCode.Current_Value[Var.Button_Num - 1].Length / 2) + 25.5);
                }
                else
                {
                    x = 349.5 - (57.5 + 64 * (double)(DateCode.Current_Value[Var.Button_Num - 1].Length / 2 - 1));
                }
            }
            else
            {
                isExceed = true;
                x = 13;
            }

            for (int i = 0; i < DateCode.Current_Value[Var.Button_Num - 1].Length; i++)
            {
                Form2_ComboBox[i] = new ComboBox
                {
                    Name = "comboBox" + (i + 1).ToString(),
                    Font = new Font("微軟正黑體", 9),
                    Location = new Point(((int)x + i * 64), 86),
                    Size = new Size(51, 24),
                    ImeMode = ImeMode.Disable,
                    IntegralHeight = false,
                    MaxDropDownItems = 10
                };
                // 設定ComboBox的事件
                Form2_ComboBox[i].SelectedIndexChanged += comboBox_SelectedIndexChanged;
                Form2_ComboBox[i].KeyPress += comboBox_KeyPress;
                panel1.Controls.Add(Form2_ComboBox[i]);

                Form2_Label[i] = new Label
                {
                    Name = "label" + (i + 1).ToString(),
                    Font = new Font("微軟正黑體", 39),
                    Location = new Point(((int)x + i * 64), 9),
                    Size = new Size(51, 74),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = ""
                };
                panel1.Controls.Add(Form2_Label[i]);

                if (isExceed)
                {
                    Space_Label[0] = new Label
                    {
                        Name = "label1",
                        Location = new Point(((int)x + (DateCode.Current_Value[Var.Button_Num - 1].Length) * 64), 9),
                        Size = new Size(0, 105),
                        Text = ""
                    };
                    panel1.Controls.Add(Space_Label[0]);
                }

                this.Controls.Add(panel1);
            }
        }
        #endregion

        #region 動態生成有規則時的外框
        private void Dynamic_Border(int position)
        {
            if (!string.IsNullOrEmpty(Form2_ComboBox[position].Text))
            {
                if (Array.Exists(DateCode.Diff_Rule, element => element == Form2_ComboBox[position].Text))
                {
                    Border[position] = new Button
                    {
                        Name = "Border" + (position + 1).ToString(),
                        Location = new Point((Form2_Label[position].Left - 4), 6),
                        Size = new Size(123, 107),
                        Text = "",
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.Transparent,
                        TabStop = false
                    };
                }
                else
                {
                    Border[position] = new Button
                    {
                        Name = "Border" + (position + 1).ToString(),
                        Location = new Point((Form2_Label[position].Left - 4), 6),
                        Size = new Size(59, 107),
                        Text = "",
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.Transparent,
                        TabStop = false
                    };
                }
                Border[position].MouseDown += Border_Remove_Focus;
                Border[position].FlatAppearance.MouseDownBackColor = Color.Transparent;
                Border[position].FlatAppearance.MouseOverBackColor = Color.Transparent;
                Border[position].FlatAppearance.BorderColor = Color.Red;
                Border[position].FlatAppearance.BorderSize = 2;
                Border[position].SendToBack();
                panel1.Controls.Add(Border[position]);

                this.Controls.Add(panel1);
            }
        }
        #endregion

        #region 移除動態生成的外框
        private void Remove_Dynamic_Border(int position)
        {
            if (Border != null)
            {
                panel1.Controls.Remove(Border[position]);
            }
        }
        #endregion

        #region 儲存按鈕
        private void Save_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                String Rule_combine = "";
                String Modify_Value = "";

                for (int i = 0; i < DateCode.Current_Value[Var.Button_Num - 1].Length; i++)
                {
                    Modify_Value += Form2_Label[i].Text;
                }

                for (int i = 0; i < Form2_ComboBox_Count; i++)
                {
                    if (Array.Exists(DateCode.Diff_Rule, element => element == Form2_ComboBox[i].Text))
                    {
                        Rule_combine = Rule_combine + Form2_ComboBox[i].Text;
                    }
                    else
                    {
                        Rule_combine = Rule_combine + Form2_ComboBox[i].Text + ",";
                    }
                }

                Rule_combine = Rule_combine.Substring(0, Rule_combine.Length - 1);

                if (String.IsNullOrEmpty(String.Concat(Rule_combine.Split(','))))
                {
                    Rule_combine = "";
                }

                Var.Access_select = Access_data.Access_Select_Keyence_Rules("select Model,Block_No from Rules where Model='" + Var.Model + "' and Block_No = '" + (Var.Button_Num - 1) + "';");
                if (Var.Access_select.Rows.Count == 0)
                {
                    Var.Access_select = Access_data.Access_Select_Keyence_Rules("INSERT INTO Rules (Model, Block_No, Rule_Code) VALUES('" + Var.Model + "', '" + (Var.Button_Num - 1).ToString() + "', '" + Rule_combine + "');");
                }
                else
                {
                    Var.Access_select = Access_data.Access_Select_Keyence_Rules("UPDATE Rules SET Rule_Code = '" + Rule_combine + "' WHERE Model = '" + Var.Model + "' and Block_No = '" + (Var.Button_Num - 1) + "';");
                }
                Access_data.Access_InsertUpdateDelete_Log("INSERT INTO Rules_Log VALUES('" + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "', '" + Var.Model + "', '" + (Var.Button_Num - 1).ToString() + "', '" + DateCode.Rule_Value[Var.Button_Num - 1] + "', '" + DateCode.Current_Value[Var.Button_Num - 1] + "');");
                MPU.WriteLog("儲存Log => 產品：" + Var.Model + "、Block No：" + (Var.Button_Num - 1) + "、修改前規則：" + DateCode.Rule_Value[Var.Button_Num - 1] + "、修改後規則：" + Rule_combine + "、修改前參數值：" + DateCode.Current_Value[Var.Button_Num - 1] + "、修改後參數值：" + Modify_Value);

                TST_DateCode_Form.TST_DateCode_Form_Update();                

                this.Close();
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("", "save_btn_Click," + ex.Message);
            }
        }
        #endregion

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Check_Result = "";
                Var.ComboBox_Num = Convert.ToInt32(Regex.Replace((sender as ComboBox).Name, "[^0-9]", "")) - 1;
                DateCode.Y_03_Count = Var.Button_Num - 1;
                DateCode.Rule_k = Var.ComboBox_Num;

                if (Loaded_Count <= 0)
                {
                    // 如果輸入的規則不是空的
                    if (!String.IsNullOrEmpty((sender as ComboBox).Text))
                    {
                        Check_Result = DateCode.Run_Rule((sender as ComboBox).Text);
                        // 如果是影響兩位數的規則                        
                        if (Array.Exists(DateCode.Diff_Rule, element => element == (sender as ComboBox).Text))
                        {
                            // 如果不是最後一個參數
                            if (Var.ComboBox_Num + 1 < Form2_ComboBox_Count)
                            {
                                // 如果下一個參數是空值
                                if (Form2_ComboBox[Var.ComboBox_Num + 1].Text == "")
                                {
                                    if (Check_Result == "@")
                                    {
                                        MessageBox.Show(DateCode.RuleErrMsg);                                        
                                        Form2_Label[Var.ComboBox_Num].Text = Var.Label_Text[Var.ComboBox_Num];
                                        Form2_Label[Var.ComboBox_Num + 1].Text = Var.Label_Text[Var.ComboBox_Num + 1];
                                        Form2_ComboBox[Var.ComboBox_Num].Text = "";
                                    }
                                    else
                                    {
                                        Form2_Label[Var.ComboBox_Num].Text = Check_Result.Substring(0, 1);
                                        Form2_Label[Var.ComboBox_Num + 1].Text = Check_Result.Substring(1, 1);
                                        Form2_ComboBox[Var.ComboBox_Num + 1].Enabled = false;
                                    }                                    
                                    Remove_Dynamic_Border(Var.ComboBox_Num);
                                    Dynamic_Border(Var.ComboBox_Num);
                                }
                                // 如果下一個參數不是空值
                                else
                                {
                                    MessageBox.Show("下一個參數的規則不是空值，無法設定二位數規則!");
                                    Form2_ComboBox[Var.ComboBox_Num].Text = "";
                                    Remove_Dynamic_Border(Var.ComboBox_Num);
                                }
                            }
                            // 如果是最後一個參數
                            else
                            {
                                MessageBox.Show("最後一個參數，無法設定二位數規則!");
                                Form2_ComboBox[Var.ComboBox_Num].Text = "";
                                Remove_Dynamic_Border(Var.ComboBox_Num);
                            }
                        }
                        // 如果不是影響兩位數的規則
                        else
                        {
                            // 如果不是最後一個參數
                            if (Var.ComboBox_Num + 1 < Form2_ComboBox_Count)
                            {
                                // 如果下一個規則被鎖定
                                if (Form2_ComboBox[Var.ComboBox_Num + 1].Enabled == false)
                                {
                                    if (Check_Result == "@")
                                    { 
                                        MessageBox.Show(DateCode.RuleErrMsg);
                                        Form2_Label[Var.ComboBox_Num].Text = Var.Label_Text[Var.ComboBox_Num];
                                        Form2_ComboBox[Var.ComboBox_Num].Text = "";
                                    }
                                    else
                                    {                        
                                        Form2_Label[Var.ComboBox_Num].Text = Check_Result;
                                    }
                                    Form2_ComboBox[Var.ComboBox_Num + 1].Enabled = true;
                                    Form2_Label[Var.ComboBox_Num + 1].Text = Var.Label_Text[Var.ComboBox_Num + 1];
                                    Remove_Dynamic_Border(Var.ComboBox_Num);
                                    Dynamic_Border(Var.ComboBox_Num);
                                }
                                // 如果下一個規則沒有被鎖定
                                else
                                {
                                    if (Check_Result == "@")
                                    {
                                        MessageBox.Show(DateCode.RuleErrMsg);                                        
                                        Form2_Label[Var.ComboBox_Num].Text = Var.Label_Text[Var.ComboBox_Num];
                                        Form2_ComboBox[Var.ComboBox_Num].Text = "";
                                    }
                                    else
                                    {
                                        Form2_Label[Var.ComboBox_Num].Text = Check_Result;
                                    }
                                    Remove_Dynamic_Border(Var.ComboBox_Num);
                                    Dynamic_Border(Var.ComboBox_Num);
                                }
                            }
                            // 如果是最後一個參數
                            else
                            {
                                if (Check_Result == "@")
                                {
                                    MessageBox.Show(DateCode.RuleErrMsg);                                    
                                    Form2_Label[Var.ComboBox_Num].Text = Var.Label_Text[Var.ComboBox_Num];
                                    Form2_ComboBox[Var.ComboBox_Num].Text = "";
                                }
                                else
                                {
                                    Form2_Label[Var.ComboBox_Num].Text = Check_Result;
                                }
                                Remove_Dynamic_Border(Var.ComboBox_Num);
                                Dynamic_Border(Var.ComboBox_Num);
                            }
                        }
                    }
                    // 如果輸入的規則是空的
                    else
                    {
                        // 如果不是最後一個參數
                        if (Var.ComboBox_Num + 1 < Form2_ComboBox_Count)
                        {
                            // 如果下一個規則被鎖定
                            if (Form2_ComboBox[Var.ComboBox_Num + 1].Enabled == false)
                            {
                                Form2_Label[Var.ComboBox_Num].Text = Var.Label_Text[Var.ComboBox_Num];
                                Form2_ComboBox[Var.ComboBox_Num + 1].Enabled = true;
                                Form2_Label[Var.ComboBox_Num + 1].Text = Var.Label_Text[Var.ComboBox_Num + 1];
                            }
                            // 如果下一個規則沒有被鎖定
                            else
                            {
                                Form2_Label[Var.ComboBox_Num].Text = Var.Label_Text[Var.ComboBox_Num];
                            }
                            Remove_Dynamic_Border(Var.ComboBox_Num);
                        }
                        // 如果是最後一個參數
                        else
                        {
                            Form2_Label[Var.ComboBox_Num].Text = Var.Label_Text[Var.ComboBox_Num];
                        }
                        Remove_Dynamic_Border(Var.ComboBox_Num);
                    }
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("", "comboBox_SelectedIndexChanged," + ex.Message);
            }
        }

        private void Tab_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                int tab_btn_Num = 0;
                // 保留字串內的數字
                tab_btn_Num = Convert.ToInt32(Regex.Replace((sender as Button).Name, "[^0-9]", ""));

                //panel1.Focus();
                this.ActiveControl = null;

                //被點選的按鈕設為灰底，其餘按鈕恢復為預設顏色
                Tab_Button[tab_btn_Num - 1].BackColor = Color.DarkGray;
                for (int i = 0; i < Tab_Button.Length; i++)
                {
                    if (i != (tab_btn_Num - 1))
                    {
                        Tab_Button[i].BackColor = default(Color);
                    }
                }

                //讀取目前分類的DataGridView資料
                Reset_Datagridview(tab_btn_Num);

                // 清空ComboBox的內容
                for (int i = 0; i < DateCode.Current_Value[Var.Button_Num - 1].Length; i++)
                {
                    Form2_ComboBox[i].Items.Clear();
                }

                // 在ComboBox的第一個選項加入一個空白選項
                for (int i = 0; i < DateCode.Current_Value[Var.Button_Num - 1].Length; i++)
                {
                    Form2_ComboBox[i].Items.Add("");
                }

                // 將目前分類的規則編號存入ComboBox
                for (int i = 0; i < Var.Rules_Table.Tables["Table" + (tab_btn_Num)].Rows.Count; i++)
                {
                    for (int j = 0; j < DateCode.Current_Value[Var.Button_Num - 1].Length; j++)
                    {
                        Form2_ComboBox[j].Items.Add(Var.Rules_Table.Tables["Table" + (tab_btn_Num)].Rows[i][0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("", "Tab_Btn_Click," + ex.Message);
            }
        }

        private string Form_tittle(int i)
        {
            try
            {
                switch (i)
                {
                    case 1:
                        return "第一行文字設定";
                    case 2:
                        return "第二行文字設定";
                    case 3:
                        return "第三行文字設定";
                    case 4:
                        return "第四行文字設定";
                    case 5:
                        return "第五行文字設定";
                    case 6:
                        return "第六行文字設定";
                    case 7:
                        return "第七行文字設定";
                    case 8:
                        return "第八行文字設定";
                    case 9:
                        return "第九行文字設定";
                    case 10:
                        return "第十行文字設定";
                    default:
                        return "";
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("", "Form_tittle," + ex.Message);

                return "";
            }
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Cancel_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Border_Remove_Focus(object sender, EventArgs e)
        {
            panel1.Focus();
        }

        private void TST_DateCode_Setting_Shown(object sender, EventArgs e)
        {
            dataGridView1.FirstDisplayedScrollingRowIndex = 0;
            dataGridView1.ClearSelection();
        }
    }
}
