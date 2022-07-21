using DateCode_Dll;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LaserMark
{
    public partial class TST_DateCode_Form : Form
    {
        public TST_DateCode_Form()
        {
            InitializeComponent();
        }

        private static Label[] Current_Label = new Label[11];
        private static Label[] Rule_Label = new Label[10];
        private static Button[] SetBtn = new Button[10];
        private static int Current_Label_Len;
        private static int Rule_Label_Len;

        public static TextBox Model_Name_TextBox;        
        public static DateTimePicker ENG_DateTimePicker;
        public static TextBox Error_Msg_TextBox;
        private static List<String> Msg = new List<String>();

        private static TST_DateCode_Setting TST_Setting = new TST_DateCode_Setting();

        //---//
        private static int[] Rules_Block_No;
        private static String[] Rules;
        private static String[][] Rules_Split;        
        private static int Rules_Have_Value_Count;
                                                        
        private static String[] Btn_Block_No;

        private static int Rule_Count;
        private static int Value_Count;
        private static int Exist_Count;
        private static int Diff_Count;
        private static String Rule_Combine;

        private static String Check_Result;
        private static String Block_Num;
        private static String Date_Code;
        //---//

        private void TST_DateCode_Form_Load(object sender, EventArgs e)
        {
            try
            {
                //---設置物件預設值---//                
                this.SetDesktopLocation(Owner.Location.X + Owner.Width, Owner.Location.Y);
                Current_Label_Len = Current_Label.Length;
                Rule_Label_Len = Rule_Label.Length;

                DateCode.Current_Value = new String[Current_Label_Len];
                DateCode.Rule_Value = new String[Rule_Label_Len];

                Model_Name_TextBox = this.Controls.Find("textBox1", true)[0] as TextBox;
                Error_Msg_TextBox = this.Controls.Find("textBox2", true)[0] as TextBox;
                Error_Msg_TextBox.ShortcutsEnabled = false;

                ENG_DateTimePicker = this.Controls.Find("ENG_Time", true)[0] as DateTimePicker;
                ENG_DateTimePicker.Visible = false;

                for (int i = 0; i < Current_Label.Length; i++)
                {
                    Current_Label[i] = this.Controls.Find("label_Current" + (i + 1).ToString(), true)[0] as Label;
                    Current_Label[i].Text = "";
                }

                for (int i = 0; i < Rule_Label.Length; i++)
                {
                    Rule_Label[i] = this.Controls.Find("label_Rule" + (i + 1).ToString(), true)[0] as Label;
                    Rule_Label[i].Text = "";
                }

                for (int i = 0; i < SetBtn.Length; i++)
                {
                    SetBtn[i] = this.Controls.Find("button_Set" + (i + 1).ToString(), true)[0] as Button;
                    SetBtn[i].Enabled = false;
                }         

                if (Var.Ethernet == true && Var.str_RUNcard != "ENG" && !String.IsNullOrEmpty(Var.Model))
                {
                    label3.Text = RunCard_Date(Var.str_RUNcard);
                }
                else if (Var.Ethernet == false && Var.str_RUNcard != "ENG" && !String.IsNullOrEmpty(Var.Model))
                {
                    label3.ForeColor = Color.Red;
                    label3.Text = "網路連線失敗，無法設定規則！";
                }
                else if (Var.str_RUNcard == "ENG" && !String.IsNullOrEmpty(Var.Model))
                {                    
                    label3.Text = "";
                    ENG_Time.Visible = true;
                }                
                else
                {
                    label3.ForeColor = Color.Red;
                    label3.Text = "此Model不存在!";
                }

                Model_Name_TextBox.Text = Var.Model;                
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("FL", "TST_DateCode_Form_Load," + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Model_Name_TextBox.Text) && Var.Form3_Close == false)
            {
                if (Var.str_RUNcard != "ENG")
                {
                    TST_DateCode_Form_Update();                    
                }
                else
                {
                    ENG_DateTimePicker.Value = System.DateTime.Now;
                }
            }
        }

        private void ENG_Date_ValueChanged(object sender, EventArgs e)
        {
            DataTable Table;
            String TST_Week;
            try
            {
                if (Var.Ethernet)
                {
                    Table = Form1.ExecuteDataTable("select PM04 from pm_dc_file @olmes1 where to_char(pm03,'yyyymmdd') = to_char('" + ENG_DateTimePicker.Value.ToString("yyyyMMdd") + "')");
                }
                else
                {
                    Table = new DataTable();
                }

                if (Table.Rows.Count > 0)
                {
                    TST_Week = Table.Rows[0][0].ToString();
                }
                else
                {
                    TST_Week = "00";
                }

                Var.Date = ENG_DateTimePicker.Value.ToString("yyyyMMdd") + TST_Week;
                Var.Today_Date = ENG_DateTimePicker.Value.ToString("yyyyMMdd HHmm ") + (Convert.ToInt32(ENG_DateTimePicker.Value.DayOfWeek) + 1 > 6 ? 0 : Convert.ToInt32(ENG_DateTimePicker.Value.DayOfWeek) + 1);
                DateCode.DateRefresh(Var.Date, Var.Today_Date);
                TST_DateCode_Form_Update();
                MsgError();
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("", "ENG_Date_ValueChanged," + ex.Message);
            }
        }

        public static void TST_DateCode_Form_Update()
        {
            try
            {
                Reset_Value();
                Load_Form_Label();
                Auto_Adjust_Rule();
                Execute_Rule();
                Set_Button();
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("", "Update_Label_Keyence," + ex.Message);
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            try
            {
                Var.Button_Num = Convert.ToInt32(Regex.Replace((sender as Button).Name, "[^0-9]", ""));                
                TST_Setting.ShowDialog();
                MsgError();
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("", "button_Click," + ex.Message);
            }
        }

        private static void Reset_Value()
        {
            Rules_Block_No = new int[Rule_Label_Len];
            Rules = new String[Rule_Label_Len];
            Rules_Split = new String[Rule_Label_Len][];
            Rules_Have_Value_Count = 0;
            Btn_Block_No = new String[Rule_Label_Len];
            Diff_Count = 0;
            Rule_Combine = "";
            Check_Result = "";
            Block_Num = "";
            Date_Code = "";
            Var.Result = "";
            Msg.Clear();

            for (int i = 0; i < Current_Label.Length; i++)
            {
                Current_Label[i].BackColor = SystemColors.Control;
                Current_Label[i].Text = "";
                DateCode.Current_Value[i] = "";
            }

            for (int i = 0; i < Rule_Label.Length; i++)
            {
                Rule_Label[i].BackColor = SystemColors.Control;
                Rule_Label[i].Text = "";
                DateCode.Rule_Value[i] = "";
            }
        }

        private static void Load_Form_Label()
        {
            //---Current_Label---//
            Var.Access_select_Keyence = null;
            Var.Access_select_Keyence = Access_data.Access_Select("select Block_No,CharacterString_Information from Block where Model='" + Model_Name_TextBox.Text + "' order by Block_No ASC");
            for (int i = 0; i < Var.Access_select_Keyence.Rows.Count; i++)
            {
                Current_Label[Convert.ToInt32(Var.Access_select_Keyence.Rows[i]["Block_No"].ToString())].Text = Var.Access_select_Keyence.Rows[i]["CharacterString_Information"].ToString();
                DateCode.Current_Value[Convert.ToInt32(Var.Access_select_Keyence.Rows[i]["Block_No"].ToString())] = Var.Access_select_Keyence.Rows[i]["CharacterString_Information"].ToString();

                if (!string.IsNullOrEmpty(Current_Label[Convert.ToInt32(Var.Access_select_Keyence.Rows[i]["Block_No"].ToString())].Text))
                {
                    Current_Label[Convert.ToInt32(Var.Access_select_Keyence.Rows[i]["Block_No"].ToString())].BackColor = Color.White;
                    Btn_Block_No[i] = Var.Access_select_Keyence.Rows[i]["Block_No"].ToString();
                }
            }

            //---Logo---//
            Var.Access_select_Keyence = null;
            Var.Access_select_Keyence = Access_data.Access_Select("select File from Logo where Model='" + Model_Name_TextBox.Text + "'");
            Current_Label[10].Text = Var.Access_select_Keyence.Rows[0]["File"].ToString();
            if (!string.IsNullOrEmpty(Current_Label[10].Text))
            {
                Current_Label[10].BackColor = Color.White;
            }

            //---Rule_Label---//
            Var.Access_select_Keyence = null;
            Var.Access_select_Keyence = Access_data.Access_Select_Keyence_Rules("select Block_No,Rule_Code from Rules where Model='" + Model_Name_TextBox.Text + "' order by Block_No ASC");
            for (int i = 0; i < Var.Access_select_Keyence.Rows.Count; i++)
            {
                Rule_Label[Convert.ToInt32(Var.Access_select_Keyence.Rows[i]["Block_No"].ToString())].Text = Var.Access_select_Keyence.Rows[i]["Rule_Code"].ToString();
                DateCode.Rule_Value[Convert.ToInt32(Var.Access_select_Keyence.Rows[i]["Block_No"].ToString())] = Var.Access_select_Keyence.Rows[i]["Rule_Code"].ToString();

                if (!string.IsNullOrEmpty(Rule_Label[Convert.ToInt32(Var.Access_select_Keyence.Rows[i]["Block_No"].ToString())].Text))
                {
                    Rule_Label[Convert.ToInt32(Var.Access_select_Keyence.Rows[i]["Block_No"].ToString())].BackColor = Color.White;
                }

                // 取出Rules的Block_No、Rule_Code，另外儲存用來切割
                Rules_Block_No[i] = Convert.ToInt32(Var.Access_select_Keyence.Rows[i]["Block_No"]);
                Rules[Rules_Block_No[i]] = Var.Access_select_Keyence.Rows[i]["Rule_Code"].ToString();
            }            
        }

        private static void Set_Button()
        {
            for (int k = 0; k < SetBtn.Length; k++)
            {
                if (Array.Exists(Btn_Block_No, element => element == k.ToString()))
                {
                    String Narrow = Strings.StrConv(Current_Label[k].Text, VbStrConv.Narrow, 0);
                    
                    if ((Var.Ethernet == true && Var.str_RUNcard != "ENG") || Var.str_RUNcard == "ENG")
                    {
                        if (Narrow.Substring(0, 1) != "%")
                        {
                            SetBtn[k].Enabled = true;
                        }
                        else
                        {
                            SetBtn[k].Enabled = false;
                        }
                    }
                    else
                    {
                        SetBtn[k].Enabled = false;
                    }
                }
                else
                {
                    SetBtn[k].Enabled = false;
                }
            }
        }

        public static void Auto_Adjust_Rule()
        {
            // 計算Rules有值的數量
            Rules_Have_Value_Count = 0;
            
            for (int i = 0; i < Rules.Length; i++)
            {
                if (Rules[i] != null)
                {
                    Rules_Have_Value_Count++;
                }
            }

            //主程式改字Rule改長度//
            if (Var.Form2_Close == true)
            {
                for (int i = 0; i < Rules_Have_Value_Count; i++)
                {
                    Rules_Split[Rules_Block_No[i]] = Rules[Rules_Block_No[i]].Split(',');
                    Rule_Count = Rules_Split[Rules_Block_No[i]].Length;
                    Value_Count = DateCode.Current_Value[Rules_Block_No[i]].Length;
                    Diff_Count = 0;
                    Exist_Count = 0;
                    Rule_Combine = "";

                    for (int j = 0; j < Rules_Split[Rules_Block_No[i]].Length; j++)
                    {
                        if (Array.Exists(DateCode.Diff_Rule, element => element == Rules_Split[Rules_Block_No[i]][j]))
                        {
                            Diff_Count++;
                        }
                    }

                    if (DateCode.Current_Value[Rules_Block_No[i]].Length > Rules_Split[Rules_Block_No[i]].Length + Diff_Count)
                    {
                        Rule_Combine = Rules[Rules_Block_No[i]];
                        for (int k = 0; k < DateCode.Current_Value[Rules_Block_No[i]].Length - (Rules_Split[Rules_Block_No[i]].Length + Diff_Count); k++)
                        {
                            Rule_Combine = Rule_Combine + ",";
                        }
                    }
                    else if (DateCode.Current_Value[Rules_Block_No[i]].Length < Rules_Split[Rules_Block_No[i]].Length + Diff_Count)
                    {
                        if (Diff_Count == 0)
                        {
                            if (DateCode.Current_Value[Rules_Block_No[i]].Length == 0)
                            {
                                Rule_Combine = "";
                            }
                            else
                            {
                                for (int k = 0; k < DateCode.Current_Value[Rules_Block_No[i]].Length; k++)
                                {
                                    Rule_Combine = Rule_Combine + Rules_Split[Rules_Block_No[i]][k].ToString() + ",";
                                }
                                Rule_Combine = Rule_Combine.Substring(0, Rule_Combine.Length - 1);
                            }
                        }
                        else
                        {
                            for (int k = 0; k < Rules_Split[Rules_Block_No[i]].Length + Diff_Count - DateCode.Current_Value[Rules_Block_No[i]].Length; k++)
                            {
                                if (Array.Exists(DateCode.Diff_Rule, element => element == Rules_Split[Rules_Block_No[i]][Rules_Split[Rules_Block_No[i]].Length - k + Exist_Count - 1]))
                                {
                                    Exist_Count++;
                                }
                                Rule_Combine = "";
                                if (Rule_Count + Diff_Count - Value_Count > 0 && Exist_Count < 2)
                                {
                                    for (int n = 0; n < Rule_Count - 1; n++)
                                    {
                                        Rule_Combine = Rule_Combine + Rules_Split[Rules_Block_No[i]][n].ToString() + ",";
                                    }
                                    if (Exist_Count == 0)
                                    {
                                        Rule_Combine = Rule_Combine.Substring(0, Rule_Combine.Length - 1);
                                    }
                                    Rule_Count--;
                                }
                                else
                                {
                                    for (int n = 0; n < Rule_Count; n++)
                                    {
                                        Rule_Combine = Rule_Combine + Rules_Split[Rules_Block_No[i]][n].ToString() + ",";
                                    }
                                    Rule_Combine = Rule_Combine.Substring(0, Rule_Combine.Length - 1);
                                    Exist_Count = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        Rule_Combine = Rules[Rules_Block_No[i]];
                    }

                    if (String.IsNullOrEmpty(String.Concat(Rule_Combine.Split(','))))
                    {
                        Rule_Combine = "";
                    }

                    Rule_Label[Rules_Block_No[i]].Text = Rule_Combine;
                    DateCode.Rule_Value[Rules_Block_No[i]] = Rule_Combine;
                    Rules[Rules_Block_No[i]] = Rule_Combine;

                    Var.Access_select = Access_data.Access_Select_Keyence_Rules("select Model,Block_No from Rules where Model='" + Var.Model + "' and Block_No = '" + Rules_Block_No[i] + "';");
                    if (Var.Access_select.Rows.Count == 0)
                    {
                        Var.Access_select = Access_data.Access_Select_Keyence_Rules("INSERT INTO Rules (Model, Block_No, Rule_Code) VALUES('" + Var.Model + "', '" + Rules_Block_No[i].ToString() + "', '" + Rule_Combine + "');");
                    }
                    else
                    {
                        Var.Access_select = Access_data.Access_Select_Keyence_Rules("UPDATE Rules SET Rule_Code = '" + Rule_Combine + "' WHERE Model = '" + Var.Model + "' and Block_No = '" + Rules_Block_No[i] + "';");
                    }
                    Access_data.Access_InsertUpdateDelete_Log("INSERT INTO Rules_Log VALUES('" + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "', '" + Var.Model + "', '" + Rules_Block_No[i] + "', '" + Rule_Combine + "', '" + DateCode.Current_Value[Rules_Block_No[i]] + "');");
                }
            }
        }

        public static void Execute_Rule()
        {
            // 計算Rules有值的數量
            Rules_Have_Value_Count = 0;

            for (int i = 0; i < Rules.Length; i++)
            {
                if (Rules[i] != null)
                {
                    Rules_Have_Value_Count++;
                }
            }
            
            //---切割Rule，執行Rules_Code中的各別副程式---//
            if (((Var.Ethernet == true && Var.str_RUNcard != "ENG") || Var.str_RUNcard == "ENG") && Var.Form2_Close == false)
            {
                Rules_Split = new String[Rule_Label_Len][];
                for (int i = 0; i < Rules_Have_Value_Count; i++)
                {
                    Check_Result = "";
                    DateCode.Rule_k = 0;
                    DateCode.Y_03_Count = 0;
                    if (!String.IsNullOrEmpty(String.Concat(Rules[Rules_Block_No[i]].Split(','))))
                    {
                        DateCode.Y_03_Count = Rules_Block_No[i];
                        Rules_Split[Rules_Block_No[i]] = Rules[Rules_Block_No[i]].Split(',');
                        if (Rules_Split[Rules_Block_No[i]].Length > Current_Label[Rules_Block_No[i]].Text.Length)
                        {
                            MessageBox.Show(Message_Num(Rules_Block_No[i] + 1) + "行文字與設定規則長度不符，請檢查規則");
                            Var.Result = "";
                        }
                        else
                        {
                            for (int j = 0; j < Rules_Split[Rules_Block_No[i]].Length; j++)
                            {
                                if (Rules_Split[Rules_Block_No[i]][j] != "")
                                {
                                    Check_Result = DateCode.Run_Rule(Rules_Split[Rules_Block_No[i]][j]);
                                    if (Array.Exists(DateCode.Diff_Rule, element => element == Rules_Split[Rules_Block_No[i]][j]))
                                    {
                                        if (Check_Result == "@")
                                        {
                                            Var.Result = Var.Result + Current_Label[Rules_Block_No[i]].Text.Substring(DateCode.Rule_k, 2);
                                            AddMsgError(Rules_Block_No[i] + 1, j + 1, Rules_Split[Rules_Block_No[i]][j]);
                                        }
                                        else
                                        {
                                            Var.Result = Var.Result + Check_Result;
                                        }
                                        DateCode.Rule_k = DateCode.Rule_k + 2;
                                    }
                                    else
                                    {                                        
                                        if (Check_Result == "@")
                                        {
                                            Var.Result = Var.Result + Current_Label[Rules_Block_No[i]].Text.Substring(DateCode.Rule_k, 1);
                                            AddMsgError(Rules_Block_No[i] + 1, j + 1, Rules_Split[Rules_Block_No[i]][j]);                                            
                                        }
                                        else
                                        {
                                            Var.Result = Var.Result + Check_Result;                                            
                                        }
                                        DateCode.Rule_k++;
                                    }
                                }
                                else
                                {
                                    Var.Result = Var.Result + Current_Label[Rules_Block_No[i]].Text.Substring(DateCode.Rule_k, 1);
                                    DateCode.Rule_k++;
                                }
                            }

                            Var.Result = Strings.StrConv(Var.Result, VbStrConv.Wide, 0);
                            Current_Label[Rules_Block_No[i]].Text = Var.Result;
                            DateCode.Current_Value[Rules_Block_No[i]] = Var.Result;
                            Date_Code = Var.Result;
                            Block_Num = Rules_Block_No[i].ToString();
                            Var.Result = "";

                            //---//

                            if (Strings.StrConv(Date_Code, VbStrConv.Narrow, 0).Substring(0, 1) == "%")
                            {
                                Date_Code = Strings.StrConv(Date_Code, VbStrConv.Narrow, 0);
                                Access_data.Access_InsertUpdateDelete("Update [Block] set " +
                                                                        "Block.CharacterString_Information='" + Date_Code + "'" +
                                                                        " where Model = '" + Var.Model + "' and Block_No='" + Block_Num + "';");
                            }
                            else
                            {
                                Access_data.Access_InsertUpdateDelete("Update [Block] set " +
                                                                        "Block.CharacterString_Information='" + Date_Code + "'" +
                                                                        " where Model = '" + Var.Model + "' and Block_No='" + Block_Num + "';");
                            }
                        }
                    }
                    else
                    {
                        Var.Result = "";
                    }
                }                
            }
            Form1.Update_Label();
            Var.Form2_Close = false;
        }

        public static void MsgError()
        {
            if (Msg.Count > 0)
            {
                Error_Msg_TextBox.Visible = true;
                Error_Msg_TextBox.ForeColor = Color.Red;
                Error_Msg_TextBox.Text = String.Join("\r\n", Msg.ToArray());
            }
            else
            {
                Error_Msg_TextBox.Visible = false;
            }
        }

        public static void AddMsgError(int i , int j, String rule)
        {
            Msg.Add("＊" + Message_Num(i) + "行" + Message_Num(j) + "個規則(" + rule + ")\r\n　" + DateCode.RuleErrMsg);
        }

        private static string Message_Num(int i)
        {
            try
            {
                switch (i)
                {
                    case 1:
                        return "第一";
                    case 2:
                        return "第二";
                    case 3:
                        return "第三";
                    case 4:
                        return "第四";
                    case 5:
                        return "第五";
                    case 6:
                        return "第六";
                    case 7:
                        return "第七";
                    case 8:
                        return "第八";
                    case 9:
                        return "第九";
                    case 10:
                        return "第十";
                    default:
                        return "";
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("", "Message_Num," + ex.Message);

                return "";
            }
        }

        private void TST_DateCode_Form_Shown(object sender, EventArgs e)
        {
            MsgError();

            if (Var.Ethernet == false)
            {
                MessageBox.Show("網路連線失敗，請檢查網路狀況", "網路異常！");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        #region RunCard_Date
        public static String RunCard_Date(String RunCard)
        {
            DataTable Table = new DataTable();

            try
            {
                Table = Form1.ExecuteDataTable("SELECT to_char(shm15,'yyyy/mm/dd') FROM sfa_file,shm_file WHERE shm012 = sfa01 and sfa05 > 0  and shm01 ='" + RunCard + "'");

                return Table.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("Table", "RunCard_Date," + ex.Message);

                return "資料庫回傳值錯誤!";
            }
        }
        #endregion
    }
}
