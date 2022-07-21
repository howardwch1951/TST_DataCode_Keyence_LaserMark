using System;
using System.Data;
using System.IO.Ports;
using System.Windows.Forms;

namespace LaserMark
{
    class Var
    {
        public static SerialPort serialPort;
        public static string connStr = "User Id=tst;Password=tst;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.32)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=topprod)));";
        public static string strCon = @"Data Source=192.168.1.58;Database=dbMES;Uid=sa;Pwd=aaa222!!!;";

        public static string str_DeviceNO;
        public static string str_UserID;
        public static string str_RUNcard;
        public static string str_marking;

        //----學弟新增的--//
        public static string Result;
        public static String[] Label_Text;
        public static int Button_Num;
        public static int ComboBox_Num;        
        public static DataTable Access_select_Keyence = new DataTable();
        public static DataTable Insert_Log = new DataTable();
        public static DataTable Rules = new DataTable();
        public static DataSet Rules_Table = new DataSet();
        public static string Date = "";
        public static string Today_Date = "";
        public static bool Form2_Close = false;
        public static bool Form3_Close = false;
        public static TST_DateCode_Form TST_Form = new TST_DateCode_Form();
        //------//


        public static DataTable Model_select = new DataTable();
        public static DataTable Access_select = new DataTable();
        public static DataTable Model_counter = new DataTable();

        public static string[] profile = new string[200];
        public static bool Ethernet = true;
        public static bool English = true;
        public static bool Initialization = true;
        public static string recipe;
        public static string Model;
        public static string[][][] Items_tittle = new string[2][][];
        public static string[][][] Limit=new string [5][][];
        public static string[][] Limitname = new string[5][];
        public static Label[] Marking_string = new Label[11];
        public static int Counter_No = 0;
        public static string[] Counter_num = new string[10];
        public static string[] Counter_OldValue = new string[10];

        //防止進入change事件,導致關閉視窗慢
        public static bool clear = false;
        //不規則陣列初始化
        public static void array3_initialize()
        {
            try
            {
                Var.Items_tittle[0] = new string[5][];
                Var.Items_tittle[1] = new string[5][];

                Var.Items_tittle[0][0] = new string[8];
                Var.Items_tittle[0][1] = new string[18];
                Var.Items_tittle[0][2] = new string[27];
                Var.Items_tittle[0][3] = new string[22];
                Var.Items_tittle[0][4] = new string[9];

                Var.Items_tittle[1][0] = new string[8];
                Var.Items_tittle[1][1] = new string[18];
                Var.Items_tittle[1][2] = new string[27];
                Var.Items_tittle[1][3] = new string[22];
                Var.Items_tittle[1][4] = new string[9];


                Var.Limit[0] = new string[8][];
                Var.Limit[1] = new string[18][];
                Var.Limit[2] = new string[26][];
                Var.Limit[3] = new string[21][];
                Var.Limit[4] = new string[9][];

                for (int i = 0; i < Var.Limit.Length; i++)
                    for (int j = 0; j < Var.Limit[i].Length; j++)
                        Var.Limit[i][j] = new string[4];


                Var.Limitname[0] = new string[8];
                Var.Limitname[1] = new string[18];
                Var.Limitname[2] = new string[27];
                Var.Limitname[3] = new string[21];
                Var.Limitname[4] = new string[9];
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("a3", "array3_initialize," + ex.Message);
            }
        }
    }
}
