using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LaserMark
{
    class Keyence
    {
        private static string Marking_232Error = "";
        private static string Marking_232Command = "";

        private static byte[] receive = new byte[500];

        private static string K_re;

        public static bool Keyence_promgramName()
        {
            try
            {
                byte[] buff1;
                K_re = "";
                Marking_232Command = "";
                Marking_232Error = "";

                buff1 = Encoding.Default.GetBytes("\x02" + "F5,900" + "\x03");

                Var.serialPort.Write(buff1, 0, buff1.Length);
                K_re = Keyence_read();

                if (K_re != "timeout" && K_re != "fail")
                {
                    Marking_232Command = K_re.Substring(1, 2);
                    Marking_232Error = K_re.Substring(4, 1);

                    if (Marking_232Error == "0")
                    {
                        if (Marking_232Command == "F5" && K_re.Substring(6) != "")
                        {
                            return true;
                        }
                        else
                        {
                            MPU.WriteErrorCode("1", K_re);
                            MessageBox.Show(K_re);

                            return false;
                        }
                    }
                    else if (Marking_232Error == "1" && K_re.Substring(6, 4) == "S021")
                    {
                        MPU.WriteErrorCode("2", K_re);

                        return Keyence_create();
                    }
                    else
                    {
                        MPU.WriteErrorCode("F5", K_re);
                        MessageBox.Show(K_re);

                        return false;
                    }
                }
                else if (K_re == "timeout")
                {
                    MessageBox.Show(K_re, "Timeout");

                    return false;
                }
                else if (K_re == "fail")
                {
                    MessageBox.Show(K_re, "Fail");

                    return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("Kp", "Keyence_promgramName," + ex.Message);

                return false;
            }
        }

        private static bool Keyence_create()
        {
            try
            {
                if (Keyence_send("XT,900"))
                    if (Keyence_send("K0,900,0,0,0,6,0,0,000.50,0000.0,0000,0000.0,0060.000,-060.000,00,00001,0000.0,0000.0,00000,00000,2,1"))
                        if (Keyence_send("K2,900,0,099,000,-046.800,0033.100,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01000,045.0,100,000,000,01,00,001.000,000.700,00.001,000,00.100,0,0,001.000,test"))
                            if (Keyence_send("G4,900,NEW"))
                                return Keyence_send("YE");
                
                return false;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("C1", "Keyence_create," + ex.Message);
                MessageBox.Show(ex.Message, "初始化失敗");

                return false;
            }
        }
        public static bool Keyence_send(string command)
        {
            try
            {
                byte[] buff1;
                K_re = "";
                Marking_232Command = "";
                Marking_232Error = "";


                buff1 = Encoding.GetEncoding("Shift-JIS").GetBytes(String.Concat("\x02", command, "\x03"));
                
                Var.serialPort.Write(buff1, 0, buff1.Length);
                K_re = Keyence_read();

                if (K_re != "timeout" && K_re != "fail")
                {
                    Marking_232Command = K_re.Substring(1, 2);
                    Marking_232Error = K_re.Substring(4, 1);

                    if (Marking_232Command == command.Substring(0, 2) && Marking_232Error == "0")
                    {
                        return true;
                    }
                    else if (Marking_232Command == command.Substring(0, 2) && Marking_232Error == "1")
                    {
                        MPU.WriteErrorCode("2", K_re);

                        if (Marking_232Command == "K2" || Marking_232Command == "G6" || Marking_232Command == "K3" || Marking_232Command == "F7")
                            MessageBox.Show(Keyence_Command_Multiline_Error(command.Substring(0, 8)) + Keyence_Command_Error(Marking_232Command) + " , " + Keyence_Error(K_re.Substring(6, 4)), "參數錯誤 " + Marking_232Command + " : " + K_re.Substring(6, 4));
                        else
                            MessageBox.Show(Keyence_Command_Error(Marking_232Command) + " , " + Keyence_Error(K_re.Substring(6, 4)), "參數錯誤 " + Marking_232Command + " : " + K_re.Substring(6, 4));

                        if (Marking_232Command != "XT" && Marking_232Command != "XI")
                            Keyence_send("XI");

                        return false;
                    }
                    else
                    {
                        MPU.WriteErrorCode("1", K_re);
                        MessageBox.Show(K_re);

                        return false;
                    }
                }
                else if (K_re == "timeout")
                {
                    MessageBox.Show(K_re, "Timeout");

                    return false;
                }
                else if (K_re == "fail")
                {
                    MessageBox.Show(K_re, "Fail");

                    return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("KS", "Keyence_send," + ex.Message);
                MessageBox.Show(ex.Message);

                return false;
            }
        }

        public static string Keyence_sendforread(string command)
        {
            try
            {
                byte[] buff1;
                K_re = "";
                Marking_232Command = "";
                Marking_232Error = "";

                buff1 = Encoding.Default.GetBytes(String.Concat("\x02", command, "\x03"));

                Var.serialPort.Write(buff1, 0, buff1.Length);
                K_re = Keyence_read().Trim('\x02', '\x03');

                return K_re;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("KS", "Keyence_sendforread," + ex.Message);
                MessageBox.Show(ex.Message);

                return "fail";
            }
        }

        private static string Keyence_read()
        {
            try
            {
                int timeout = 0;

                string Reply = "";
                int length = 0;
                int Total_length = 0;

                while (true)
                {
                    timeout++;
                    Thread.Sleep(80);
                    //System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff"));
                    
                    if ((length = Var.serialPort.BytesToRead) > 0)
                    {
                        Total_length += Var.serialPort.BytesToRead;

                        Var.serialPort.Read(receive, 0, Var.serialPort.BytesToRead);
                        Reply += Encoding.GetEncoding("Shift-JIS").GetString(receive, 0, Total_length);

                        Array.Clear(receive, 0, 500);
                        if (Reply != "" && Reply.Substring(0, 1) == "\x02" && Reply.Substring(Reply.Length - 1, 1) == "\x03")
                        {
                            //MPU.WriteLog(Reply);
                            return Reply;
                        }
                    }
                    
                    if (timeout >= 50)
                        return "timeout";

                    Application.DoEvents();
                    //System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff"));
                }
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("KR", "Keyence_read," + ex.Message);
                MessageBox.Show(ex.Message);

                return "fail";
            }
        }

        //指令錯誤時,指令名稱
        public static string Keyence_Command_Error(string command)
        {
            try
            {
                switch (command)
                {
                    case "XT":
                        return "程序No.新建失敗";
                    case "G4":
                        return "標題設定失敗";
                    case "K0":
                        return "通用條件設定失敗";
                    case "K2":
                        return "文字設定失敗";
                    case "G8":
                        return "底座設定失敗";
                    case "G6":
                        return "計數器設定失敗";
                    case "YE":
                        return "程序No.儲存失敗";
                    case "F9":
                        return "底座設定讀取失敗";
                    case "K1":
                        return "通用設定讀取失敗";
                    case "K3":
                        return "文字設定讀取失敗";
                    case "F7":
                        return "計數器設定讀取失敗";
                    default:
                        return "超過現有指令範圍";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
        }
        //同指令多行時,判斷哪一行指令錯誤
        public static string Keyence_Command_Multiline_Error(string command)
        {
            try
            {
                string[] command_line = command.Split(',');


                switch (command_line[0])
                {
                    case "K2":
                        return "文字行" + command_line[2] + " ";
                    case "G6":
                        return "計數器" + command_line[2] + " ";
                    case "K3":
                        return "文字行" + command_line[2] + " ";
                    case "F7":
                        return "計數器" + command_line[2] + " ";
                    default:
                        return "超過現有指令範圍";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
        }
        //錯誤代號
        public static string Keyence_Error(string error)
        {
            try
            {
                switch (error)
                {
                    case "S000":
                        return "程序內容非法";
                    case "S001":
                        return "設定儲存器已滿";
                    case "S002":
                        return "內部儲存卡已滿";
                    case "S003":
                        return "外部儲存卡已滿";
                    case "S004":
                        return "外部儲存卡未插入";
                    case "S005":
                        return "外部儲存卡無法辨識";
                    case "S006":
                        return "優先權錯誤";
                    case "S008":
                        return "無文件錯誤";
                    case "S009":
                        return "忙碌錯誤";
                    case "S010":
                        return "無刻印信息組";
                    case "S011":
                        return "Logo標誌或自定義字符數量超出範圍";
                    case "S012":
                        return "優化非法錯誤";
                    case "S014":
                        return "當前設定操作錯誤";
                    case "S015":
                        return "Logo標誌或自定義字符文件操作錯誤";
                    case "S016":
                        return "測試刻印不可執行";
                    case "S018":
                        return "條形碼或2維碼程序內容非法";
                    case "S019":
                        return "所有設定恢復錯誤";
                    case "S020":
                        return "數據長度錯誤";
                    case "S021":
                        return "程序NO.未註冊";
                    case "S022":
                        return "信息組編號未註冊";
                    case "S024":
                        return "命令非法";
                    case "S025":
                        return "校驗碼錯誤";
                    case "S026":
                        return "格式化錯誤";
                    case "S027":
                        return "命令無法識別";
                    case "S028":
                        return "回應數據長度錯誤";
                    case "S029":
                        return "刻印內容請求錯誤";
                    case "S030":
                        return "組編號未註冊";
                    case "S050":
                        return "高速字符變更設定錯誤";
                    case "S051":
                        return "樣本刻印不可執行";
                    case "S052":
                        return "檢測激光不可執行";
                    case "S060":
                        return "信息組種類程序內容非法";
                    case "S061":
                        return "信息組座標程序內容非法";
                    case "S062":
                        return "字符大小程序內容非法";
                    case "S063":
                        return "字符配置程序內容非法";
                    case "S064":
                        return "字符詳細程序內容非法";
                    case "S065":
                        return "刻印條件程序內容非法";
                    case "S066":
                        return "條形碼或2維碼程序內容非法";
                    case "S067":
                        return "連續刻印程序內容非法";
                    case "S068":
                        return "移動或刻印方向程序內容非法";
                    case "S069":
                        return "線條件程序內容非法";
                    case "S070":
                        return "圖像信息程序內容非法";
                    case "S071":
                        return "圖像工件信息程序內容非法";
                    case "S072":
                        return "字符串程序內容非法";
                    case "S073":
                        return "個別計數器程序內容非法";
                    case "S074":
                        return "通用計數器程序內容非法";
                    case "S075":
                        return "預設信息程序內容非法";
                    case "S076":
                        return "系統信息程序內容非法";
                    case "S077":
                        return "字體替換信息程序內容非法";
                    case "S078":
                        return "字體縮放信息程序內容非法";
                    case "S079":
                        return "字體跳過交叉寬度信息程序內容非法";
                    case "S080":
                        return "Logo標誌或自定義字符緩衝器信息程序內容非法";
                    case "S081":
                        return "當前值信息程序內容非法";
                    case "S082":
                        return "3維系統信息程序內容非法";
                    case "S083":
                        return "3維設定信息程序內容非法";
                    case "S084":
                        return "操作限制錯誤";
                    case "S086":
                        return "Wobble程序內容不正確";
                    case "S090":
                        return "註冊錯誤條形碼";
                    case "S091":
                        return "條形碼與2維碼連接設定錯誤";
                    case "S092":
                        return "條形碼註冊狀態非法";
                    default:
                        return error + "其它錯誤，超過錯誤代碼範圍";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
        }
    }
}
