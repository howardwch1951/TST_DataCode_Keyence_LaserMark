using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace LaserMark
{
    class Access_data
    {
        private static string Access_strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Profile_Keyence\SystemProfile\Model.mdb";
        private static string Access_strConn_Keyence_Rules = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Profile_Keyence\SystemProfile\Keyence_Rule.mdb";
        private static string Access_strConn_Keyence_Log = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Profile_Keyence\SystemProfile\Log.mdb";

        public static bool Access_InsertUpdateDelete(string SQL)
        {
            try
            {
                using (OleDbConnection Model_mdb = new OleDbConnection(Access_strConn))
                {
                    Model_mdb.Open();

                    using (OleDbCommand cm = new OleDbCommand(SQL, Model_mdb))
                    {
                        cm.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("A1", "Access_InsertUpdateDelete," + SQL + "," + ex.Message);

                if (ex.Message.IndexOf("重複") > 0)
                    MessageBox.Show("重複產品名稱", "存檔失敗");

                return false;
            }
        }

        public static bool Access_InsertUpdateDelete_Log(string SQL)
        {
            try
            {
                using (OleDbConnection Model_mdb = new OleDbConnection(Access_strConn_Keyence_Log))
                {
                    Model_mdb.Open();

                    using (OleDbCommand cm = new OleDbCommand(SQL, Model_mdb))
                    {
                        cm.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("A1", "Access_InsertUpdateDelete_Log," + SQL + "," + ex.Message);

                if (ex.Message.IndexOf("重複") > 0)
                    MessageBox.Show("重複產品名稱", "存檔失敗");

                return false;
            }
        }

        public static DataTable Access_Select(string Sql)
        {
            try
            {
                DataTable ds = new DataTable();

                using (OleDbConnection Model_mdb = new OleDbConnection(Access_strConn))
                {
                    Model_mdb.Open();

                    using (OleDbDataAdapter da = new OleDbDataAdapter(Sql, Access_strConn))
                    {
                        da.Fill(ds);
                    }
                }

                return ds;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("A1", "Access_Select," + Sql + "," + ex.Message);

                return new DataTable();
            }
        }

        public static DataTable Access_Select_Keyence_Rules(string Sql)
        {
            try
            {
                DataTable ds = new DataTable();

                using (OleDbConnection Model_mdb = new OleDbConnection(Access_strConn_Keyence_Rules))
                {
                    Model_mdb.Open();

                    using (OleDbDataAdapter da = new OleDbDataAdapter(Sql, Access_strConn_Keyence_Rules))
                    {
                        da.Fill(ds);
                    }
                }

                return ds;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("A1", "Access_Select_Keyence_Rules," + Sql + "," + ex.Message);

                return new DataTable();
            }
        }

        public static void Access_Insert_FormDatatable(string tittle, DataTable select_all, DataTable select_counter)
        {
            try
            {
                string model_sql;

                model_sql = "Insert into [Model] Values('" + tittle + "'";
                for (int i = 1; i < 21; i++)
                    model_sql += ",'" + select_all.Rows[0][i].ToString() + "'";
                model_sql += ");";

                Access_InsertUpdateDelete(model_sql);

                string block_sql;


                for (int i = 0; i < select_all.Rows.Count; i++)
                {
                    block_sql = "Insert into [Block] Values('" + tittle + "'";
                    for (int j = 22; j < 51; j++)
                        block_sql += ",'" + select_all.Rows[i][j].ToString() + "'";
                    block_sql += ");";

                    Access_InsertUpdateDelete(block_sql);
                }


                string image_sql;

                image_sql = "Insert into [Image] Values('" + tittle + "'";
                for (int i = 52; i < 60; i++)
                    image_sql += ",'" + select_all.Rows[0][i].ToString() + "'";
                image_sql += ");";

                Access_InsertUpdateDelete(image_sql);


                string logo_sql;

                logo_sql = "Insert into [Logo] Values('" + tittle + "'";
                for (int i = 61; i < 85; i++)
                    logo_sql += ",'" + select_all.Rows[0][i].ToString() + "'";
                logo_sql += ");";

                Access_InsertUpdateDelete(logo_sql);


                if (select_counter.Rows.Count > 0)
                {
                    string counter_sql;

                    for (int i = 0; i < select_counter.Rows.Count; i++)
                    {
                        counter_sql = "Insert into [Counter] Values('" + tittle + "'";
                        for (int j = 1; j < 10; j++)
                            counter_sql += ",'" + select_counter.Rows[i][j].ToString() + "'";
                        counter_sql += ");";

                        Access_InsertUpdateDelete(counter_sql);
                    }
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("2", "Access_Insert," + ex.Message);
            }
        }

        public static void Access_Insert_Log(string tittle, DataTable select_all, DataTable select_counter)
        {
            try
            {
                String Time = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                string model_sql;

                model_sql = "Insert into [Model_Log] Values('" + Time + "'" + ",'" + tittle + "'";
                for (int i = 1; i < 21; i++)
                    model_sql += ",'" + select_all.Rows[0][i].ToString() + "'";
                model_sql += ");";

                Access_InsertUpdateDelete_Log(model_sql);

                string block_sql;


                for (int i = 0; i < select_all.Rows.Count; i++)
                {
                    block_sql = "Insert into [Block_Log] Values('" + Time + "'" + ",'" + tittle + "'";
                    for (int j = 22; j < 51; j++)
                        block_sql += ",'" + select_all.Rows[i][j].ToString() + "'";
                    block_sql += ");";

                    Access_InsertUpdateDelete_Log(block_sql);
                }


                string image_sql;

                image_sql = "Insert into [Image_Log] Values('" + Time + "'" + ",'" + tittle + "'";
                for (int i = 52; i < 60; i++)
                    image_sql += ",'" + select_all.Rows[0][i].ToString() + "'";
                image_sql += ");";

                Access_InsertUpdateDelete_Log(image_sql);


                string logo_sql;

                logo_sql = "Insert into [Logo_Log] Values('" + Time + "'" + ",'" + tittle + "'";
                for (int i = 61; i < 85; i++)
                    logo_sql += ",'" + select_all.Rows[0][i].ToString() + "'";
                logo_sql += ");";

                Access_InsertUpdateDelete_Log(logo_sql);


                if (select_counter.Rows.Count > 0)
                {
                    string counter_sql;

                    for (int i = 0; i < select_counter.Rows.Count; i++)
                    {
                        counter_sql = "Insert into [Counter_Log] Values('" + Time + "'" + ",'" + tittle + "'";
                        for (int j = 1; j < 10; j++)
                            counter_sql += ",'" + select_counter.Rows[i][j].ToString() + "'";
                        counter_sql += ");";

                        Access_InsertUpdateDelete_Log(counter_sql);
                    }
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("2", "Access_Insert," + ex.Message);
            }
        }

        public static void Access_Insert(string tittle, string block_num, TextBox[] block)
        {
            string[] block_string = new string[block.Length];

            block_string = Num_Format(block);

            Access_InsertUpdateDelete("Insert into [Block] Values(" +
                                                                    "'" + tittle + "'," +
                                                                    "'" + block_num + "'," +
                                                                    "'" + block_string[0] + "'," +
                                                                    "'" + block_string[1] + "'," +
                                                                    "'" + block_string[2] + "'," +
                                                                    "'" + block_string[3] + "'," +
                                                                    "'" + block_string[4] + "'," +
                                                                    "'" + block_string[5] + "'," +
                                                                    "'" + block_string[6] + "'," +
                                                                    "'" + block_string[7] + "'," +
                                                                    "'" + block_string[8] + "'," +
                                                                    "'" + block_string[9] + "'," +
                                                                    "'" + block_string[10] + "'," +
                                                                    "'00000'," +
                                                                    "'" + block_string[11] + "'," +
                                                                    "'" + block_string[12] + "'," +
                                                                    "'" + block_string[13] + "'," +
                                                                    "'" + block_string[14] + "'," +
                                                                    "'" + block_string[15] + "'," +
                                                                    "'" + block_string[16] + "'," +
                                                                    "'" + block_string[17] + "'," +
                                                                    "'" + block_string[18] + "'," +
                                                                    "'" + block_string[19] + "'," +
                                                                    "'" + block_string[20] + "'," +
                                                                    "'" + block_string[21] + "'," +
                                                                    "'" + block_string[22] + "'," +
                                                                    "'" + block_string[23] + "'," +
                                                                    "'" + block_string[24] + "'," +
                                                                    "'" + block_string[25] + "'," +
                                                                    "'" + block_string[26] + "')");
        }

        public static void Access_Insert(string tittle, TextBox[] counter)
        {
            string[] counter_string = new string[counter.Length];

            counter_string = Num_Format(counter);

            Access_InsertUpdateDelete("Insert into [Counter] Values(" +
                                                                    "'" + tittle + "'," +
                                                                    "'" + counter_string[0] + "'," +
                                                                    "'" + counter_string[1] + "'," +
                                                                    "'" + counter_string[2] + "'," +
                                                                    "'" + counter_string[3] + "'," +
                                                                    "'" + counter_string[4] + "'," +
                                                                    "'" + counter_string[5] + "'," +
                                                                    "'" + counter_string[6] + "'," +
                                                                    "'" + counter_string[7] + "'," +
                                                                    "'" + counter_string[8] + "')");
        }

        public static void Access_Insert(string tittle, TextBox[] model, TextBox[] block, TextBox[] image)
        {
            Access_InsertUpdateDelete("Insert into [Model] Values(" +
                                                                    "'" + tittle + "'," +
                                                                    "'" + model[0].Text + "'," +
                                                                    "'" + model[1].Text + "'," +
                                                                    "'0'," +
                                                                    "'" + model[2].Text + "'," +
                                                                    "'" + model[3].Text + "'," +
                                                                    "'" + model[4].Text + "'," +
                                                                    "'" + model[5].Text + "'," +
                                                                    "'" + model[6].Text + "'," +
                                                                    "'" + model[7].Text + "'," +
                                                                    "'" + model[8].Text + "'," +
                                                                    "'" + model[9].Text + "'," +
                                                                    "'00'," +
                                                                    "'" + model[10].Text + "'," +
                                                                    "'" + model[11].Text + "'," +
                                                                    "'" + model[12].Text + "'," +
                                                                    "'" + model[13].Text + "'," +
                                                                    "'" + model[14].Text + "'," +
                                                                    "'" + model[15].Text + "'," +
                                                                    "'" + model[16].Text + "'," +
                                                                    "'" + model[17].Text + "')");


            Access_InsertUpdateDelete("Insert into [Block] Values(" +
                                                                    "'" + tittle + "'," +
                                                                    "'" + block[0].Text + "'," +
                                                                    "'" + block[1].Text + "'," +
                                                                    "'" + block[2].Text + "'," +
                                                                    "'" + block[3].Text + "'," +
                                                                    "'" + block[4].Text + "'," +
                                                                    "'" + block[5].Text + "'," +
                                                                    "'" + block[6].Text + "'," +
                                                                    "'" + block[7].Text + "'," +
                                                                    "'" + block[8].Text + "'," +
                                                                    "'" + block[9].Text + "'," +
                                                                    "'" + block[10].Text + "'," +
                                                                    "'00000'," +
                                                                    "'" + block[11].Text + "'," +
                                                                    "'" + block[12].Text + "'," +
                                                                    "'" + block[13].Text + "'," +
                                                                    "'" + block[14].Text + "'," +
                                                                    "'" + block[15].Text + "'," +
                                                                    "'" + block[16].Text + "'," +
                                                                    "'" + block[17].Text + "'," +
                                                                    "'" + block[18].Text + "'," +
                                                                    "'" + block[19].Text + "'," +
                                                                    "'" + block[20].Text + "'," +
                                                                    "'" + block[21].Text + "'," +
                                                                    "'" + block[22].Text + "'," +
                                                                    "'" + block[23].Text + "'," +
                                                                    "'" + block[24].Text + "'," +
                                                                    "'" + block[25].Text + "'," +
                                                                    "'" + block[26].Text + "'," +
                                                                    "'" + block[27].Text + "')");


            Access_InsertUpdateDelete("Insert into [Image] Values(" +
                                                                    "'" + tittle + "'," +
                                                                    "'" + image[0].Text + "'," +
                                                                    "'" + image[1].Text + "'," +
                                                                    "'" + image[2].Text + "'," +
                                                                    "'" + image[3].Text + "'," +
                                                                    "'" + image[4].Text + "'," +
                                                                    "'" + image[5].Text + "'," +
                                                                    "'" + image[6].Text + "'," +
                                                                    "'" + image[7].Text + "')");
        }
        public static void Access_Update(string tittle, string block_num, TextBox[] block)
        {
            string[] block_string = new string[block.Length];

            block_string = Num_Format(block);

            Access_InsertUpdateDelete("Update [Block] set " +
                                                            "Block_No='" + block_num + "'," +
                                                            "Block_3DShape='" + block_string[0] + "'," +
                                                            "Block_Type='" + block_string[1] + "'," +
                                                            "Xcoordinate_2D='" + block_string[2] + "'," +
                                                            "Ycoordinate_2D='" + block_string[3] + "'," +
                                                            "Zcoordinate_2D='" + block_string[4] + "'," +
                                                            "Spot_variablevalue='" + block_string[5] + "'," +
                                                            "Block_Angle='" + block_string[6] + "'," +
                                                            "Character_Angle='" + block_string[7] + "'," +
                                                            "Marking_Flag='" + block_string[8] + "'," +
                                                            "Approach='" + block_string[9] + "'," +
                                                            "Approach_betweenCharacters='" + block_string[10] + "'," +
                                                            "Fixed_value='00000'," +
                                                            "Scan_Speed='" + block_string[11] + "'," +
                                                            "Marking_Power='" + block_string[12] + "'," +
                                                            "Q_switchfrequency='" + block_string[13] + "'," +
                                                            "Initial_pulseapplicationvalue='" + block_string[14] + "'," +
                                                            "Initial_pulseapplicationtime='" + block_string[15] + "'," +
                                                            "Line_Type='" + block_string[16] + "'," +
                                                            "FontNo='" + block_string[17] + "'," +
                                                            "Character_Height='" + block_string[18] + "'," +
                                                            "Character_Width='" + block_string[19] + "'," +
                                                            "Skip_Cross='" + block_string[20] + "'," +
                                                            "Number_ofLines='" + block_string[21] + "'," +
                                                            "Thick_LineWidth='" + block_string[22] + "'," +
                                                            "TargetofQuick_ChangeofCharacter='" + block_string[23] + "'," +
                                                            "RegularPitch_LayoutFlag='" + block_string[24] + "'," +
                                                            "Character_Pitch='" + block_string[25] + "'," +
                                                            "CharacterString_Information='" + block_string[26] + "'" +
                                                            " where Model = '" + tittle + "' and Block_No='" + block_num + "';");
        }

        public static void Access_Update(string tittle, DataTable select_all, TextBox[] model, TextBox[] image, TextBox[] logo, string logo_name)
        {
            string[] model_string = new string[model.Length];
            string[] image_string = new string[image.Length];
            string[] logo_string = new string[logo.Length];

            model_string = Num_Format(model);
            image_string = Num_Format(image);
            if (logo[0].Text != "" && logo_name != "")
            {
                logo_string = Num_Format(logo);
                logo_name = Logo_Format(Convert.ToDouble(logo[1].Text), logo_name);
            }


            Access_InsertUpdateDelete("Update [Model] set " +
                                                            "Setting_Type='" + model_string[0] + "'," +
                                                            "Movement_Direction_XY='" + model_string[1] + "'," +
                                                            "Fixed_value='0'," +
                                                            "Marking_Direction='" + model_string[2] + "'," +
                                                            "Movement_Condition_XY='" + model_string[3] + "'," +
                                                            "Movement_Condition_Z='" + model_string[4] + "'," +
                                                            "Marking_Time='" + model_string[5] + "'," +
                                                            "Trigger_Delay='" + model_string[6] + "'," +
                                                            "Number_of_EncoderPulses='" + model_string[7] + "'," +
                                                            "Minimum_Workpiece_Interval='" + model_string[8] + "'," +
                                                            "MovementMarking_StartPosition='" + model_string[9] + "'," +
                                                            "MovementMarking_EndPosition='" + model_string[10] + "'," +
                                                            "Fixed_value1='00'," +
                                                            "ContMarkRept='" + model_string[11] + "'," +
                                                            "ContMarkInterval='" + model_string[12] + "'," +
                                                            "Distance_PointerPosition='" + model_string[13] + "'," +
                                                            "Approach_ScanSpeed='" + model_string[14] + "'," +
                                                            "Optimized_ScanSpeed='" + model_string[15] + "'," +
                                                            "Scan_OptimizationFlag='" + model_string[16] + "'," +
                                                            "Marking_OrderFlag='" + model_string[17] + "'" +
                                                            " where Model = '" + tittle + "';");


            Access_InsertUpdateDelete("Update [Image] set " +
                                                            "Scan_Direction='" + image_string[0] + "'," +
                                                            "Number_ofColumns='" + image_string[1] + "'," +
                                                            "Number_ofRows='" + image_string[2] + "'," +
                                                            "Column_Pitch='" + image_string[3] + "'," +
                                                            "Row_Pitch='" + image_string[4] + "'," +
                                                            "MarkingStart_PaletteNo='" + image_string[5] + "'," +
                                                            "ReferencePosition_Xcoordinate='" + image_string[6] + "'," +
                                                            "ReferencePosition_Ycoordinate='" + image_string[7] + "'" +
                                                            " where Model = '" + tittle + "';");

            Access_InsertUpdateDelete("Update [Logo] set " +
                                                            "Block_No='" + Var.Access_select.Rows[0]["Logo.Block_No"].ToString() + "'," +
                                                            "Block_3DShape='" + logo_string[0] + "'," +
                                                            "Block_Type='" + logo_string[1] + "'," +
                                                            "Xcoordinate_2D='" + logo_string[2] + "'," +
                                                            "Ycoordinate_2D='" + logo_string[3] + "'," +
                                                            "Zcoordinate_2D='" + logo_string[4] + "'," +
                                                            "Spot_variablevalue='" + logo_string[5] + "'," +
                                                            "Block_Angle='" + logo_string[6] + "'," +
                                                            "LogoSize_Width='" + logo_string[7] + "'," +
                                                            "LogoSize_Height='" + logo_string[8] + "'," +
                                                            "Resolution='" + logo_string[9] + "'," +
                                                            "Reverse_B_W='" + logo_string[10] + "'," +
                                                            "Skipped_dots='" + logo_string[11] + "'," +
                                                            "Concentration='" + logo_string[12] + "'," +
                                                            "Marking_Flag='" + logo_string[13] + "'," +
                                                            "Approach='" + logo_string[14] + "'," +
                                                            "Approach_betweenCharacters='" + logo_string[15] + "'," +
                                                            "Fixed_value='0'," +
                                                            "Scan_Speed='" + logo_string[16] + "'," +
                                                            "Marking_Power='" + logo_string[17] + "'," +
                                                            "Q_switchfrequency='" + logo_string[18] + "'," +
                                                            "Initial_pulseapplicationvalue='" + logo_string[19] + "'," +
                                                            "Initial_pulseapplicationtime='" + logo_string[20] + "'," +
                                                            "File='" + logo_name + "'" +
                                                            " where Model = '" + tittle + "';");
        }

        public static void Access_Update(string tittle, TextBox[] counter)
        {
            string[] counter_string = new string[counter.Length];

            counter_string = Num_Format(counter);

            Access_InsertUpdateDelete("Update [Counter] set " +
                                                            "Step='" + counter_string[1] + "'," +
                                                            "Initial='" + counter_string[2] + "'," +
                                                            "Start='" + counter_string[3] + "'," +
                                                            "Final='" + counter_string[4] + "'," +
                                                            "MarkRept='" + counter_string[5] + "'," +
                                                            "Reset_Timing='" + counter_string[6] + "'," +
                                                            "Count_Timing='" + counter_string[7] + "'," +
                                                            "Base='" + counter_string[8] + "'" +
                                                            " where Model = '" + tittle + "' and Counter_No='" + counter_string[0] + "';");
        }

        private static string[] Num_Format(TextBox[] textbox)
        {
            try
            {
                string[] text_string = new string[textbox.Length];

                switch (textbox[0].Name.Replace("textBox", "").Substring(0, 1))
                {
                    case "T":
                        for (int i = 0; i < textbox.Length; i++)
                            text_string[i] = Convert.ToDouble(textbox[i].Text).ToString(Var.Limit[0][Convert.ToInt32(textbox[i].Name.Replace("textBox", "").Substring(1)) - 1][3]);
                        break;
                    case "U":
                        for (int i = 0; i < textbox.Length; i++)
                            text_string[i] = Convert.ToDouble(textbox[i].Text).ToString(Var.Limit[1][Convert.ToInt32(textbox[i].Name.Replace("textBox", "").Substring(1)) - 1][3]);
                        break;
                    case "L":
                        if (textbox[0].Name.Replace("textBox", "").IndexOf("L11") == -1)
                        {
                            //存數字
                            for (int i = 0; i < textbox.Length - 1; i++)
                                text_string[i] = Convert.ToDouble(textbox[i].Text).ToString(Var.Limit[2][Convert.ToInt32(textbox[i].Name.Substring(textbox[i].Name.IndexOf("_") + 1)) - 1][3]);
                            //存字串
                            text_string[textbox.Length - 1] = textbox[textbox.Length - 1].Text;
                        }
                        else
                        {
                            //存Logo
                            for (int i = 0; i < textbox.Length; i++)
                            {
                                if (Convert.ToDouble(textbox[1].Text) == -3 && (i == 7 || i == 8))
                                    text_string[i] = Var.Access_select.Rows[0][i + 62].ToString();
                                else if ((Convert.ToDouble(textbox[1].Text) == -1 || Convert.ToDouble(textbox[1].Text) == -2 || Convert.ToDouble(textbox[1].Text) == -4) && (i == 9 || i == 10 || i == 11 || i == 12))
                                    text_string[i] = Var.Access_select.Rows[0][i + 62].ToString();
                                else
                                    text_string[i] = Convert.ToDouble(textbox[i].Text).ToString(Var.Limit[3][Convert.ToInt32(textbox[i].Name.Substring(textbox[i].Name.IndexOf("_") + 1)) - 1][3]);
                            }
                        }
                        break;
                    case "C":
                        for (int i = 0; i < textbox.Length; i++)
                            text_string[i] = Convert.ToDouble(textbox[i].Text).ToString(Var.Limit[4][Convert.ToInt32(textbox[i].Name.Replace("textBox", "").Substring(1)) - 1][3]);
                        break;
                }

                return text_string;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("N123", "Num_Format," + ex.Message);

                string[] text_string = new string[textbox.Length];
                for (int i = 0; i < textbox.Length; i++)
                {
                    text_string[i] = textbox[i].Text;
                }

                return text_string;
            }
        }

        private static string Logo_Format(double block_type, string logo)
        {
            try
            {
                switch (block_type)
                {
                    case -1:
                        logo = "%L<" + logo + ">";
                        break;
                    case -2:
                        logo = "%K<" + logo + ">";
                        break;
                    case -3:
                        logo = "%T<" + logo + ">";
                        break;
                    case -4:
                        logo = "%Z<" + logo + ">";
                        break;
                }

                return logo;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("LM", "Logo_Format," + ex.Message);
                return logo;
            }
        }
        public static void Access_Delete_counter(string tittle, int counter_no)
        {
            Access_InsertUpdateDelete("DELETE Counter.* FROM [Counter] WHERE [Counter].Model= '" + tittle + "' and [Counter].Counter_No = '" + counter_no + "';");
        }
        public static void Access_Delete(string tittle, int block_no)
        {
            Access_InsertUpdateDelete("DELETE Block.* FROM [Block] WHERE [Block].Model= '" + tittle + "' and [Block].Block_No = '" + block_no + "';");
        }
        public static void Access_Delete(string tittle)
        {
            Access_InsertUpdateDelete("DELETE Model.*, Image.*, Logo.* FROM(Model INNER JOIN[Image] ON Model.Model = Image.Model) INNER JOIN Logo ON Image.Model = Logo.Model WHERE [Model].Model=[Image].Model And [Model].Model=[Image].Model And [Model].Model=[Logo].Model And [Model].Model= '" + tittle + "';");
            Access_InsertUpdateDelete("DELETE Block.* FROM [Block] WHERE [Block].Model= '" + tittle + "';");
            Access_InsertUpdateDelete("DELETE Counter.* FROM [Counter] WHERE [Counter].Model= '" + tittle + "';");
            Access_Select_Keyence_Rules("delete from Rules where Model = '" + tittle + "'");
        }
        //DELETE [Model].*,[Block].*,[Image].*,[Logo].* FROM((Model INNER JOIN Block ON Model.Model = Block.Model) INNER JOIN[Image] ON Block.Model = Image.Model) INNER JOIN Logo ON Image.Model = Logo.Model WHERE[Model].Model=[Image].Model and[Model].Model=[Image].Model and[Model].Model=[Logo].Model and[Model].Model='3225';
        //((Model INNER JOIN Block ON Model.Model = Block.Model) INNER JOIN [Image] ON Block.Model = Image.Model) INNER JOIN Logo ON Image.Model = Logo.Model
        //(Model INNER JOIN [Image] ON Model.Model = Image.Model) INNER JOIN Logo ON Image.Model = Logo.Model
        //DELETE Model.*, Image.*, Logo.*, Model.Model FROM(Model INNER JOIN[Image] ON Model.Model = Image.Model) INNER JOIN Logo ON Image.Model = Logo.Model WHERE(((Model.Model)=[Image].[Model] And (Model.Model)=[Image].[Model] And (Model.Model)=[Logo].[Model] And (Model.Model)= '3225'));
        public static void Access_Value(DataTable select_all, int index, TextBox[] block_t, Label[] block_l)
        {
            try
            {
                int b = 0;

                for (int i = 0; i < 28; i++)
                {
                    if (i == 11) i++;
                    if (i == 27)
                    {
                        block_t[b].Text = select_all.Rows[index][i + 23].ToString();
                        block_l[b].Text = block_t[b].Text;
                    }
                    else
                    {
                        block_t[b].Text = Convert.ToDouble(select_all.Rows[index][i + 23]).ToString();
                        block_l[b].Text = block_t[b].Text;
                    }

                    b++;
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("A2", "Access_Value_b," + ex.Message);
            }
        }

        public static void Access_Value(DataTable select_all, TextBox[] model_t, Label[] model_l, TextBox[] image_t, Label[] image_l, TextBox[] logo_t, Label[] logo_l, ComboBox logo_file)
        {
            try
            {
                int m = 0;

                for (int i = 0; i < select_all.Columns.Count; i++)
                    if (i >= 0 && i <= 19)
                    {
                        if (i == 2 || i == 12) i++;
                        model_t[m].Text = Convert.ToDouble(select_all.Rows[0][i + 1]).ToString();
                        model_l[m].Text = model_t[m].Text;

                        m++;
                    }
                    else if (i >= 51 && i <= 58)
                    {
                        image_t[i - 51].Text = Convert.ToDouble(select_all.Rows[0][i + 1]).ToString();
                        image_l[i - 51].Text = image_t[i - 51].Text;
                    }
                    else if (i >= 61 && i <= 82)
                    {
                        if (i == 61) m = 0;
                        if (i == 77) i++;
                        if (select_all.Rows[0][i + 1].ToString() != "")
                        {
                            logo_t[m].Text = Convert.ToDouble(select_all.Rows[0][i + 1]).ToString();
                            logo_l[m].Text = logo_t[m].Text;

                            if (m == 16 || m == 17 || m == 18)
                            {
                                if (Var.str_UserID == "Runcard")
                                {
                                    logo_t[m].BackColor = Color.FromArgb(195, 195, 195);
                                    logo_t[m].Enabled = false;
                                }
                                else
                                {
                                    logo_t[m].Enabled = true;
                                    logo_t[m].BackColor = SystemColors.Window;
                                }
                            }
                        }

                        m++;
                    }

                logo_file.SelectedIndex = -1;
                logo_file.Text = select_all.Rows[0][84].ToString().Replace("%L", "").Replace("%K", "").Replace("%T", "").Replace("%Z", "").Trim('<', '>');
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("A2", "Access_Value," + ex.Message);
            }
        }

        public static void Access_Value_counter(DataTable select_all, TextBox[] counter_t)
        {
            try
            {
                for (int i = 0; i < select_all.Rows.Count; i++)
                {

                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("A2", "Access_Value_counter," + ex.Message);
            }
        }

        public static void Access_MarkingString(DataTable select_all)
        {
            try
            {
                if (Var.Marking_string[0] != null)
                {
                    for (int i = 0; i < Var.Marking_string.Length; i++)
                    {
                        Var.Marking_string[i].Text = "";
                        Var.Marking_string[i].BackColor = SystemColors.Control;
                    }

                    for (int i = 0; i < select_all.Rows.Count; i++)
                    { 
                        if (select_all.Rows[i]["Block.Block_No"].ToString() != null && select_all.Rows[i]["Block.Block_No"].ToString() != "")
                        {
                            //Var.Marking_string[Convert.ToInt32(select_all.Rows[i]["Block.Block_No"].ToString())].Text = Strings.StrConv(select_all.Rows[i]["CharacterString_Information"].ToString(), VbStrConv.Narrow, 0).Replace(" ","　");
                            //Var.Marking_string[Convert.ToInt32(select_all.Rows[i]["Block.Block_No"].ToString())].Text = select_all.Rows[i]["CharacterString_Information"].ToString();
                            Var.Marking_string[Convert.ToInt32(select_all.Rows[i]["Block.Block_No"].ToString())].Text = Special_characters(select_all.Rows[i]["CharacterString_Information"].ToString());
                            Var.Marking_string[Convert.ToInt32(select_all.Rows[i]["Block.Block_No"].ToString())].BackColor = Color.White;
                        }
                    }                    

                    if (select_all.Rows[0]["File"].ToString() != "")
                    {
                        Var.Marking_string[Var.Marking_string.Length - 1].Text = select_all.Rows[0]["File"].ToString().Replace("%L", "").Replace("%K", "").Replace("%T", "").Replace("%Z", "").Trim('<', '>');
                        Var.Marking_string[Var.Marking_string.Length - 1].BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("A3", "Access_MarkingString," + ex.Message);
            }
        }

        //特殊字元不轉型
        private static string Special_characters(string text)
        {
            try
            {
                string text_s = "";
                string Jis = "";
                string ascii = "";
                char ascii_byte;

                for (int i = 0; i < text.Length; i++)
                {
                    text_s = text.Substring(i, 1);
                    ascii = Strings.StrConv(text_s, VbStrConv.Narrow, 0);
                    ascii_byte = Convert.ToChar(ascii);
                    //0~9 A~Z a~z
                    if ((ascii_byte >= 48 && ascii_byte <= 57) || (ascii_byte >= 65 && ascii_byte <= 90) || (ascii_byte >= 97 && ascii_byte <= 122))
                        Jis += ascii;
                    else
                        Jis += text_s;
                }

                return Jis;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("SC", "Special_characters," + ex.Message);

                return text;
            }
        }
    }
}
