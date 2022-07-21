using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LaserMark
{

    //共用的函數群
    public static class MPU
    {

        public static Boolean TOSrun = false;

        public static double MPU_int_numericUpDown1 = 0.045;

        public static Boolean wme1 = false;

        public static Boolean wme2 = false;

        public static Boolean wme3 = false;

        public static System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection("server=192.168.1.58;Initial Catalog=dbMES;Persist Security Info=True;User ID=sa;Password=aaa222!!!");

        //191120
        public static System.Data.DataTable Datatable_OCRini;

        public static System.Data.DataTable Datatable_ROYCE;

        public static Bitmap Mainimg;

        public static Graphics Graph;

        public static Boolean ProcessMainImgEnabled;

        public static TextBox TextBoxLog;

        public static DataGridView DataGridView_LaserSet;



        //ORC SCREEN
        //int screenLeft = SystemInformation.VirtualScreen.Left;
        //int screenTop = SystemInformation.VirtualScreen.Top;
        //int screenWidth = SystemInformation.VirtualScreen.Width;
        //int screenHeight = SystemInformation.VirtualScreen.Height;

        /// <summary>
        /// 設定系統自建資料欄位
        /// </summary>
        /// <param name="Arry_Str_Set"></param>
        /// <param name="StrDataColumnsName"></param>
        public static void SetDataColumn(ref String[] Arry_Str_Set, String StrDataColumnsName)
        {

            Arry_Str_Set = StrDataColumnsName.Split(',');

        }



        static string strErrorMessageLOG = "";



        public static void WriteErrorCode(String strSourceID, String strErrorMessage)
        {
            if (!File.Exists(System.Environment.CurrentDirectory + @"\log\error.txt"))
            {
                if (!Directory.Exists(System.Environment.CurrentDirectory + @"\log\"))
                {
                    Directory.CreateDirectory(System.Environment.CurrentDirectory + @"\log\");
                }

                using (System.IO.FileStream fs = System.IO.File.Create(System.Environment.CurrentDirectory + @"\log\error.txt"))
                {
                }
            }

            //重覆的錯誤，就不要再記錄了。
            if (strErrorMessage != strErrorMessageLOG)
            {

                //如果檔案太大就清空文字檔。
                FileInfo f = new FileInfo(System.Environment.CurrentDirectory + @"\log\error.txt");
                Boolean bool_OverWriter = true;

                if (f.Length > 50000)
                {
                    bool_OverWriter = false;
                }

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(System.Environment.CurrentDirectory + @"\log\error.txt", bool_OverWriter))
                {
                    file.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":" + strErrorMessage);
                }

                strErrorMessageLOG = strErrorMessage;

            }

        }


        public static void WriteLog(String strMessage)
        {
            if (!File.Exists(System.Environment.CurrentDirectory + @"\log\" + DateTime.Now.ToString("yyyyMMdd") + @".txt"))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(System.Environment.CurrentDirectory + @"\log\" + DateTime.Now.ToString("yyyyMMdd") + @".txt"))
                {
                }
            }

            FileInfo f = new FileInfo(System.Environment.CurrentDirectory + @"\log\" + DateTime.Now.ToString("yyyyMMdd") + @".txt");
            Boolean bool_OverWriter = true;

            if (f.Length > 50000)
            {
                bool_OverWriter = false;
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(System.Environment.CurrentDirectory + @"\log\" + DateTime.Now.ToString("yyyyMMdd") + @".txt", bool_OverWriter))
            {
                file.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":" + strMessage);
            }

        }

        /// <summary>
        /// 確認系統所使用的各項資料夾及檔案是否存在，在系統一開炲就先確認，減少系統中確認的相關動作
        /// </summary>
        public static void CreateDirectory()
        {
            //系統設定檔 \Config\sys.ini
            //解碼設定檔 \Config\OCR.int
            //錯誤記錄檔 \log\error.txt
            //系統訊息檔 \log/yyyyMMDD.txt

            try
            {
                if (!Directory.Exists(System.Environment.CurrentDirectory + @"\Config\"))
                {
                    Directory.CreateDirectory(System.Environment.CurrentDirectory + @"\Config\");
                }

                if (!Directory.Exists(System.Environment.CurrentDirectory + @"\log\"))
                {
                    Directory.CreateDirectory(System.Environment.CurrentDirectory + @"\log\");
                }

                if (!File.Exists(System.Environment.CurrentDirectory + @"\Config\sys.ini"))
                {
                    using (System.IO.FileStream fs = System.IO.File.Create(System.Environment.CurrentDirectory + @"\Config\sys.ini"))
                    {
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(System.Environment.CurrentDirectory + @"\Config\sys.ini", true))
                    {
                        file.WriteLine("MID:XXX");
                    }
                }
                if (!File.Exists(System.Environment.CurrentDirectory + @"\Config\OCR.ini"))
                {
                    using (System.IO.FileStream fs = System.IO.File.Create(System.Environment.CurrentDirectory + @"\Config\OCR.ini"))
                    {
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(System.Environment.CurrentDirectory + @"\Config\OCR.ini", true))
                    {
                        file.WriteLine("欄位名稱,20,40,80,100,120");
                    }
                }
                if (!File.Exists(System.Environment.CurrentDirectory + @"\log\error.txt"))
                {
                    using (System.IO.FileStream fs = System.IO.File.Create(System.Environment.CurrentDirectory + @"\log\error.txt"))
                    {
                    }
                }
                if (!File.Exists(System.Environment.CurrentDirectory + @"\log\" + DateTime.Now.ToString("yyyyMMdd") + @".txt"))
                {
                    using (System.IO.FileStream fs = System.IO.File.Create(System.Environment.CurrentDirectory + @"\log\" + DateTime.Now.ToString("yyyyMMdd") + @".txt"))
                    {
                    }
                }
            }
            catch (Exception ex)
            {

                WriteErrorCode("M0075", ex.Message);

            }

        }




        /// <summary>
        /// 設定系統自建資料表
        /// </summary>
        /// <param name="StrDataTableName"></param>
        /// <param name="DataTable_Set"></param>
        /// <param name="StrDataColumnsName"></param>
        public static void SetDataTable(String StrDataTableName, ref System.Data.DataTable DataTable_Set, String[] StrDataColumnsName, String[] StrColType)
        {

            try
            {
                //'先清空資料表
                DataTable_Set = new System.Data.DataTable();

                //'設定資料表名稱
                DataTable_Set.TableName = StrDataTableName;

                //'設定欄位名稱

                for (int i = 0; i <= StrDataColumnsName.Length - 1; i++)
                {

                    DataTable_Set.Columns.Add(StrDataColumnsName[i], Type.GetType(StrColType[i]));

                }

            }

            catch (Exception ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("MPU0060:Exception source: {0}", ex.Source);
            }

        }

        //委派用
        delegate void PrintHandler(TextBox tb, string text);
        delegate void PrintHandlerDGV(DataGridView GDV, string text);

        public static void ProcessMainImg(TextBox tb, string text)
        {
            //判斷這個TextBox的物件是否在同一個執行緒上
            if (MPU.TextBoxLog.InvokeRequired)
            {
                //當InvokeRequired為true時，表示在不同的執行緒上，所以進行委派的動作!!
                PrintHandler ph = new PrintHandler(ProcessMainImg);
                MPU.TextBoxLog.Invoke(ph, MPU.TextBoxLog, DateTime.Now.ToString("yyyyMMdd HH:mm:ss fff"));
            }
            else
            {
                //表示在同一個執行緒上了，所以可以正常的呼叫到這個TextBox物件
                MPU.TextBoxLog.Text = DateTime.Now.ToString("yyyyMMdd HH:mm:ss fff");
                //tb.Text = tb.Text + text + Environment.NewLine;
            }
        }


        public static void ProcessMainImg(DataGridView GDV, string text)
        {
            //判斷這個TextBox的物件是否在同一個執行緒上
            if (DataGridView_LaserSet.InvokeRequired)
            {
                //當InvokeRequired為true時，表示在不同的執行緒上，所以進行委派的動作!!
                PrintHandlerDGV ph = new PrintHandlerDGV(ProcessMainImg);
                MPU.TextBoxLog.Invoke(ph, DataGridView_LaserSet, DateTime.Now.ToString("yyyyMMdd HH:mm:ss fff"));
            }
            else
            {
                //表示在同一個執行緒上了，所以可以正常的呼叫到這個TextBox物件
                Datatable_OCRini.Rows[0]["DateTime"] = DateTime.Now.ToString("yyyyMMdd HH:mm:ss fff");
            }
        }

        /// <summary>
        /// 偵測螢幕畫面開始準備解碼
        /// </summary>
        public static void ProcessMainImg()
        {

            //Mainimg
            while (ProcessMainImgEnabled)
            {

                //(int i = 0; i < (dataGridView_LaserSet.Rows.Count - 1); i++)
                for (int i = 0; i < 50; i++)
                {
                    Application.DoEvents();
                    Thread.Sleep(10);
                }

                Mainimg = new Bitmap(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
                Graph = Graphics.FromImage(Mainimg);
                Graph.CopyFromScreen(SystemInformation.VirtualScreen.Left, SystemInformation.VirtualScreen.Top, 0, 0, Mainimg.Size);
                Graph.ReleaseHdc(Graph.GetHdc());

                //TEST
                //Mainimg = new Bitmap("S3_191211_0002.jpg");

                //判斷這個TextBox的物件是否在同一個執行緒上
                if (MPU.TextBoxLog.InvokeRequired)
                {
                    //當InvokeRequired為true時，表示在不同的執行緒上，所以進行委派的動作!!
                    PrintHandler ph = new PrintHandler(ProcessMainImg);
                    MPU.TextBoxLog.Invoke(ph, MPU.TextBoxLog, DateTime.Now.ToString("yyyyMMdd HH:mm:ss fff"));
                }
                else
                {
                    //表示在同一個執行緒上了，所以可以正常的呼叫到這個TextBox物件
                    MPU.TextBoxLog.Text = DateTime.Now.ToString("yyyyMMdd HH:mm:ss fff");
                    //tb.Text = tb.Text + text + Environment.NewLine;
                }


                //判斷這個DataGridView的物件是否在同一個執行緒上
                if (DataGridView_LaserSet.InvokeRequired)
                {
                    //當InvokeRequired為true時，表示在不同的執行緒上，所以進行委派的動作!!
                    PrintHandlerDGV ph = new PrintHandlerDGV(ProcessMainImg);
                    //MPU.TextBoxLog.Invoke(ph, DataGridView_LaserSet, DateTime.Now.ToString("yyyyMMdd HH:mm:ss fff"));
                    //
                    //for (int i = 0; i < (dataGridView_LaserSet.Rows.Count - 1); i++)
                    //{
                    //    Bitmap rawDatabmp = Resize(MPU.Mainimg.Clone(new Rectangle(
                    //        Convert.ToInt32(dataGridView_LaserSet.Rows[i].Cells["X"].Value),
                    //        Convert.ToInt32(dataGridView_LaserSet.Rows[i].Cells["Y"].Value),
                    //        Convert.ToInt32(dataGridView_LaserSet.Rows[i].Cells["Width"].Value),
                    //        Convert.ToInt32(dataGridView_LaserSet.Rows[i].Cells["Height"].Value)), MPU.Mainimg.PixelFormat), 2); 

                    //    dataGridView_LaserSet.Rows[i].Cells["rawDataImage"].Value = rawDatabmp;

                    //    Bitmap greyLevelbmp = Resize(rawDatabmp, 1); 

                    //    //灰階化
                    //    int temp = 0;

                    //    for (int y = 0; y < greyLevelbmp.Height; y++)
                    //        for (int x = 0; x < greyLevelbmp.Width; x++)
                    //        {
                    //            temp = (greyLevelbmp.GetPixel(x, y).R + greyLevelbmp.GetPixel(x, y).G + greyLevelbmp.GetPixel(x, y).B) / 3;
                    //            if (temp >= Convert.ToInt32(dataGridView_LaserSet.Rows[i].Cells["Threshold"].Value))
                    //            {
                    //                temp = 255;
                    //            }
                    //            else
                    //            {
                    //                temp = 0;
                    //            }
                    //            greyLevelbmp.SetPixel(x, y, Color.FromArgb(temp, temp, temp));
                    //        }

                    //    dataGridView_LaserSet.Rows[i].Cells["greyLevelImage"].Value = greyLevelbmp;  

                    //    Page page = ocr.Process(greyLevelbmp);
                    //    string str = page.GetText();//識別後的內容
                    //    page.Dispose();
                    //    dataGridView_LaserSet.Rows[i].Cells["Value"].Value = str;
                    //}

                }
                else
                {
                    //表示在同一個執行緒上了，所以可以正常的呼叫到這個TextBox物件
                    //Datatable_OCRini.Rows[0]["DateTime"] = DateTime.Now.ToString("yyyyMMdd HH:mm:ss fff"); 
                }



                //if (this.InvokeRequired)
                //{
                //    MethodInvoker mi = new MethodInvoker(this.UpdateUI);
                //    this.BeginInvoke(mi, null);
                //}
                //else
                //{
                //    testLogWrite("");
                //}

            }

        }

        public static void ProcessMainImg222()
        {
        }

        //記錄系統資訊
        public static void systemlog(String logmessage)
        {
            //try
            //{
            //    if (conn.State == System.Data.ConnectionState.Open)
            //    {
            //        //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("INSERT  INTO [dbo].[tb_errorcode] ([err_str],[err_type],[err_source],[systime]) VALUES('" + logmessage + "','',0,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "') ", conn);

            //        //cmd.ExecuteNonQuery();
            //    }
            //}
            //catch (Exception EX)
            //{
            //   // EX.Source

            //    if (EX.Source != null)
            //       // Console.WriteLine("MPU0082:Exception source: {0}", EX.Source);

            //}
        }
    }
}
