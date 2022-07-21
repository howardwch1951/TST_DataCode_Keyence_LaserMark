using System;
using System.IO;
using System.Windows.Forms;

namespace LaserMark
{
    class Default_Item
    {
        public static void Items_txt()
        {
            using (System.IO.FileStream fs = System.IO.File.Create(System.Environment.CurrentDirectory + @"\Language\Items.txt"))
            {
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(System.Environment.CurrentDirectory + @"\Language\Items.txt"))
            {
                //image
                file.WriteLine(
                "優先刻印方向" + "\r\n" +
                "Scan Direction" + "\r\n" +
                "列數" + "\r\n" +
                "Columns" + "\r\n" +
                "行數" + "\r\n" +
                "Rows" + "\r\n" +
                "列間隔" + "\r\n" +
                "Work space(C)" + "\r\n" +
                "行間隔" + "\r\n" +
                "Work space(R)" + "\r\n" +
                "刻印開始編號" + "\r\n" +
                "Marking PaletteNo" + "\r\n" +
                "基準位置X座標" + "\r\n" +
                "Ref.Pos X" + "\r\n" +
                "基準位置Y座標" + "\r\n" +
                "Ref.Pos Y" + "\r\n\r\n");

                //model
                file.WriteLine(
                "設定類別" + "\r\n" +
                "Setting Type" + "\r\n" +
                "移動方向(XY)" + "\r\n" +
                "Movement Direction XY" + "\r\n" +
                "刻印方向" + "\r\n" +
                "Marking Direction" + "\r\n" +
                "移動條件(XY)" + "\r\n" +
                "Movement Condition XY" + "\r\n" +
                "移動條件(Z)" + "\r\n" +
                "Movement Condition Z" + "\r\n" +
                "刻印時間" + "\r\n" +
                "Marking Time" + "\r\n" +
                "觸發延遲" + "\r\n" +
                "Trigger Delay" + "\r\n" +
                "符號器脈衝數" + "\r\n" +
                "Number of EncoderPulses" + "\r\n" +
                "最小工件間隔" + "\r\n" +
                "Minimum Workpiece Interval" + "\r\n" +
                "移動刻印開始位" + "\r\n" +
                "Movement Marking Start Pos" + "\r\n" +
                "移動刻印結束位" + "\r\n" +
                "Movement Marking End Pos" + "\r\n" +
                "連續刻印次數" + "\r\n" +
                "Cont Mark Rept" + "\r\n" +
                "連續刻印間隔" + "\r\n" +
                "Cont Mark Interval" + "\r\n" +
                "距離指示燈位置" + "\r\n" +
                "Distance Pointer Pos" + "\r\n" +
                "路徑掃描速度" + "\r\n" +
                "Approach Scan Speed" + "\r\n" +
                "優化掃描速度" + "\r\n" +
                "Optimized Scan Speed" + "\r\n" +
                "掃描優化標誌" + "\r\n" +
                "Scan Optimization Flag" + "\r\n" +
                "刻印順序標誌" + "\r\n" +
                "Marking Order Flag" + "\r\n\r\n");

                //line
                file.WriteLine(
                "信息組立體形狀" + "\r\n" +
                "Block 3D Shape" + "\r\n" +
                "信息組類別" + "\r\n" +
                "Block Type" + "\r\n" +
                "X座標(2D)" + "\r\n" +
                "Xcoordinate 2D" + "\r\n" +
                "Y座標(2D)" + "\r\n" +
                "Ycoordinate 2D" + "\r\n" +
                "Z座標(2D)" + "\r\n" +
                "Zcoordinate 2D" + "\r\n" +
                "光束點可變值" + "\r\n" +
                "Spot variable value" + "\r\n" +
                "刻印角度" + "\r\n" +
                "Block Angle" + "\r\n" +
                "字符角度" + "\r\n" +
                "Character Angle" + "\r\n" +
                "刻印標誌" + "\r\n" +
                "Marking Flag" + "\r\n" +
                "路徑長" + "\r\n" +
                "Approach" + "\r\n" +
                "字符間路徑長" + "\r\n" +
                "Approach between Characters" + "\r\n" +
                "掃描速度" + "\r\n" +
                "Scan Speed" + "\r\n" +
                "刻印功率" + "\r\n" +
                "Marking Power" + "\r\n" +
                "Q開關頻率" + "\r\n" +
                "QSW freq" + "\r\n" +
                "初始脈衝值" + "\r\n" +
                "Initial pulse application value" + "\r\n" +
                "初始脈衝時間" + "\r\n" +
                "Initial pulse application time" + "\r\n" +
                "線型" + "\r\n" +
                "Line Type" + "\r\n" +
                "字體編號" + "\r\n" +
                "FontNo" + "\r\n" +
                "字符高度" + "\r\n" +
                "Char Height" + "\r\n" +
                "字符寬度" + "\r\n" +
                "Char Width" + "\r\n" +
                "跳過交叉" + "\r\n" +
                "Skip Cross" + "\r\n" +
                "刻印線數" + "\r\n" +
                "Number of Lines" + "\r\n" +
                "粗線寬度" + "\r\n" +
                "Thick Line Width" + "\r\n" +
                "高速字符變更" + "\r\n" +
                "Target of Quick Change of Character" + "\r\n" +
                "平均配置標誌" + "\r\n" +
                "Regular Pitch Layout Flag" + "\r\n" +
                "字符間隔" + "\r\n" +
                "Character Pitch" + "\r\n" +
                "字符串信息" + "\r\n" +
                "Character String Information" + "\r\n\r\n");

                //logo
                file.Write(
                "信息組立體形狀" + "\r\n" +
                "Block 3D Shape" + "\r\n" +
                "信息組類別" + "\r\n" +
                "Block Type" + "\r\n" +
                "X座標(2D)" + "\r\n" +
                "Xcoordinate  2D" + "\r\n" +
                "Y座標(2D)" + "\r\n" +
                "Ycoordinate 2D" + "\r\n" +
                "Z座標(2D)" + "\r\n" +
                "Zcoordinate 2D" + "\r\n" +
                "光束點可變值" + "\r\n" +
                "Spot variable value" + "\r\n" +
                "刻印角度" + "\r\n" +
                "Block Angle" + "\r\n" +
                "標識寬度" + "\r\n" +
                "LogoSize Width" + "\r\n" +
                "標識高度" + "\r\n" +
                "LogoSize Height" + "\r\n" +
                "像素分辨率" + "\r\n" +
                "Resolution" + "\r\n" +
                "黑/白反轉" + "\r\n" +
                "Reverse B/W" + "\r\n" +
                "空白點" + "\r\n" +
                "Skipped dots" + "\r\n" +
                "濃度" + "\r\n" +
                "Concentration" + "\r\n" +
                "刻印標誌" + "\r\n" +
                "Marking Flag" + "\r\n" +
                "路徑長" + "\r\n" +
                "Approach" + "\r\n" +
                "字符間路徑長" + "\r\n" +
                "Approach between Characters" + "\r\n" +
                "掃描速度" + "\r\n" +
                "Scan Speed" + "\r\n" +
                "刻印功率" + "\r\n" +
                "Marking Power" + "\r\n" +
                "Q開關頻率" + "\r\n" +
                "QSW freq" + "\r\n" +
                "初始脈衝值" + "\r\n" +
                "Initial pulse application value" + "\r\n" +
                "初始脈衝時間" + "\r\n" +
                "Initial pulse application time" + "\r\n" +
                "圖檔" + "\r\n" +
                "Logo File" + "\r\n\r\n");

                //counter
                //logo
                file.Write(
                "計數器編號" + "\r\n" +
                "Counter#" + "\r\n" +
                "步驟" + "\r\n" +
                "Step" + "\r\n" +
                "計數器初始值" + "\r\n" +
                "Initial" + "\r\n" +
                "計數器開始值" + "\r\n" +
                "Start" + "\r\n" +
                "計數器最終值" + "\r\n" +
                "Final" + "\r\n" +
                "計數器刻印次數" + "\r\n" +
                "Mark Rept" + "\r\n" +
                "重設定時" + "\r\n" +
                "Reset Timing" + "\r\n" +
                "定時計算" + "\r\n" +
                "Count Timing" + "\r\n" +
                "進數" + "\r\n" +
                "Base");
            }
        }

        public static void Limit_txt()
        {
            using (System.IO.FileStream fs = System.IO.File.Create(System.Environment.CurrentDirectory + @"\Config\Limit.txt"))
            {
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(System.Environment.CurrentDirectory + @"\Config\Limit.txt"))
            {
                //image
                file.WriteLine(
                "0,5,1,0;0;0" + "\r\n" +
                "001,255,3,000;000;000" + "\r\n" +
                "001,255,3,000;000;000" + "\r\n" +
                "000.000,120.000,7,000.000;000.000;000.000" + "\r\n" +
                "000.000,120.000,7,000.000;000.000;000.000" + "\r\n" +
                "00001,65025,5,00000;00000;00000" + "\r\n" +
                "-060.000,0060.000,7,0000.000;-000.000;0000.000" + "\r\n" +
                "-060.000,0060.000,7,0000.000;-000.000;0000.000" + "\r\n");

                //model
                file.WriteLine(
                "0,4,1,0;0;0" + "\r\n" +
                "0,4,1,0;0;0" + "\r\n" +
                "0,7,1,0;0;0" + "\r\n" +
                "0,3,1,0;0;0" + "\r\n" +
                "0,5,1,0;0;0" + "\r\n" +
                "000.01,300.00,6,000.00;000.00;000.00" + "\r\n" +
                "0000.0,0009.9,3,0000.0;0000.0;0000.0" + "\r\n" +
                "0000,2000,4,0000;0000;0000" + "\r\n" +
                "0000.0,6500.0,6,0000.0;0000.0;0000.0" + "\r\n" +
                "-060.000,0060.000,7,0000.000;-000.000;0000.000" + "\r\n" +
                "-060.000,0060.000,7,0000.000;-000.000;0000.000" + "\r\n" +
                "00000,65535,5,00000;00000;00000" + "\r\n" +
                "0000.0,0009.9,3,0000.0;0000.0;0000.0" + "\r\n" +
                "-021.0,0021.0,4,0000.0;-000.0;0000.0" + "\r\n" +
                "00000,04000,4,00000;00000;00000" + "\r\n" +
                "00000,00000,1,00000;00000;00000" + "\r\n" +
                "2,2,1,0;0;0" + "\r\n" +
                "0,1,1,0;0;0" + "\r\n");

                //line
                file.WriteLine(
                "-01,099,2,000;-00;000" + "\r\n" +
                "000,034,2,000;000;000" + "\r\n" +
                "-060.000,0060.000,7,0000.000;-000.000;0000.000" + "\r\n" +
                "-060.000,0060.000,7,0000.000;-000.000;0000.000" + "\r\n" +
                "-021.00,0021.00,6,0000.00;-000.00;0000.00" + "\r\n" +
                "-210,0210,4,0000;-000;0000" + "\r\n" +
                "000.00,359.99,6,000.00;000.00;000.00" + "\r\n" +
                "000.00,360.00,6,000.00;000.00;000.00" + "\r\n" +
                "0,1,1,0;0;0" + "\r\n" +
                "0.00,5.00,4,0.00;0.00;0.00" + "\r\n" +
                "0.016,5.000,5,0.000;0.000;0.000" + "\r\n" +
                "00001,12000,5,00000;00000;00000" + "\r\n" +
                "000.0,100.0,5,000.0;000.0;000.0" + "\r\n" +
                "000,400,3,000;000;000" + "\r\n" +
                "000,099,2,000;000;000" + "\r\n" +
                "000,099,2,000;000;000" + "\r\n" +
                "00,01,1,00;00;00" + "\r\n" +
                "00,11,2,00;00;00" + "\r\n" +
                "000.100,120.000,7,000.000;000.000;000.000" + "\r\n" +
                "000.100,120.000,7,000.000;000.000;000.000" + "\r\n" +
                "00.000,10.000,6,00.000;00.000;00.000" + "\r\n" +
                "000,100,3,000;000;000" + "\r\n" +
                "00.000,05.000,6,00.000;00.000;00.000" + "\r\n" +
                "0,1,1,0;0;0" + "\r\n" +
                "0,1,1,0;0;0" + "\r\n" +
                "000.000,180.000,7,000.000;000.000;000.000" + "\r\n");

                //logo
                file.Write(
                "-01,099,2,000;-00;000" + "\r\n" +
                "-04,-01,2,000;-00;000" + "\r\n" +
                "-060.000,0060.000,8,0000.000;-000.000;0000.000" + "\r\n" +
                "-060.000,0060.000,8,0000.000;-000.000;0000.000" + "\r\n" +
                "-021.00,0021.00,6,0000.00;-000.00;0000.00" + "\r\n" +
                "-210,0210,4,0000;-000;0000" + "\r\n" +
                "000.00,359.99,6,000.00;000.00;000.00" + "\r\n" +
                "000.100,120.000,7,000.000;000.000;000.000" + "\r\n" +
                "000.100,120.000,7,000.000;000.000;000.000" + "\r\n" +
                "050,800,3,000;000;000" + "\r\n" +
                "0,1,1,0;0;0" + "\r\n" +
                "0,8,1,0;0;0" + "\r\n" +
                "0,8,1,0;0;0" + "\r\n" +
                "0,1,1,0;0;0" + "\r\n" +
                "0.00,5.00,4,0.00;0.00;0.00" + "\r\n" +
                "0.016,5.000,5,0.000;0.000;0.000" + "\r\n" +
                "00000,12000,5,00000;00000;00000" + "\r\n" +
                "000.0,100.0,5,000.0;000.0;000.0" + "\r\n" +
                "000,400,3,000;000;000" + "\r\n" +
                "000,100,3,000;000;000" + "\r\n" +
                "000,099,2,000;000;000" + "\r\n");

                //counter
                file.Write(
                "0,9,1,0;0;0" + "\r\n" +
                "0,10000,5,00000;00000;00000" + "\r\n" +
                "0,4294967295,10,0000000000;0000000000;0000000000" + "\r\n" +
                "0,4294967295,10,0000000000;0000000000;0000000000" + "\r\n" +
                "0,4294967295,10,0000000000;0000000000;0000000000" + "\r\n" +
                "0,4294967295,10,0000000000;0000000000;0000000000" + "\r\n" +
                "0,4,1,0;0;0" + "\r\n" +
                "0,1,1,0;0;0" + "\r\n" +
                "2,36,2,00;00;00");
            }
        }

        public static void LimitName_txt()
        {
            using (System.IO.FileStream fs = System.IO.File.Create(System.Environment.CurrentDirectory + @"\Config\LimitName.txt"))
            {
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(System.Environment.CurrentDirectory + @"\Config\LimitName.txt"))
            {
                //image
                file.WriteLine(
                "0   /   5" + "\r\n" +
                "001   /   255" + "\r\n" +
                "001   /   255" + "\r\n" +
                "000.000   /   120.000" + "\r\n" +
                "000.000   /   120.000" + "\r\n" +
                "00001   /   65025" + "\r\n" +
                "-60.000   /   60.000  " + "\r\n" +
                "-60.000   /   60.000  " + "\r\n");

                //model
                file.WriteLine(
                "0   /   4" + "\r\n" +
                "0   /   4" + "\r\n" +
                "0   /   7" + "\r\n" +
                "0   /   3" + "\r\n" +
                "0   /   5" + "\r\n" +
                "000.01   /   300.00" + "\r\n" +
                "0.0   /   9.9" + "\r\n" +
                "0010   /   2000" + "\r\n" +
                "0000.1   /   6500.0" + "\r\n" +
                "-60.000   /   60.000  " + "\r\n" +
                "-60.000   /   60.000  " + "\r\n" +
                "00000   /   65535" + "\r\n" +
                "0.0   /   9.9" + "\r\n" +
                "-21.0   /   21.0  " + "\r\n" +
                "0000   /   4000" + "\r\n" +
                "0" + "\r\n" +
                "2" + "\r\n" +
                "0   /   1" + "\r\n");

                //line
                file.WriteLine(
                "-1   /   99" + "\r\n" +
                "00   /   34" + "\r\n" +
                "-60.000   /   60.000  " + "\r\n" +
                "-60.000   /   60.000  " + "\r\n" +
                "-21.00   /   21.00  " + "\r\n" +
                "-210   /   210  " + "\r\n" +
                "000.00   /   359.99" + "\r\n" +
                "000.00   /   360.00" + "\r\n" +
                "0   /   1" + "\r\n" +
                "0.00   /   5.00" + "\r\n" +
                "0.016   /   5.000" + "\r\n" +
                "00001   /   12000" + "\r\n" +
                "000.0   /   100.0" + "\r\n" +
                "000   /   400" + "\r\n" +
                "00   /   99" + "\r\n" +
                "00   /   99" + "\r\n" +
                "0   /   1" + "\r\n" +
                "00   /   11" + "\r\n" +
                "000.100   /   120.000" + "\r\n" +
                "000.100   /   120.000" + "\r\n" +
                "00.000   /   10.000" + "\r\n" +
                "000   /   100" + "\r\n" +
                "0.000   /   5.000" + "\r\n" +
                "0   /   1" + "\r\n" +
                "0   /   1" + "\r\n" +
                "000.000   /   180.000" + "\r\n" +
                "最長為127個字符" + "\r\n");

                //logo
                file.Write(
                "-1   /   99" + "\r\n" +
                "-4   /   -1" + "\r\n" +
                "-60.000   /   60.000  " + "\r\n" +
                "-60.000   /   60.000  " + "\r\n" +
                "-21.00   /   21.00  " + "\r\n" +
                "-210   /   210  " + "\r\n" +
                "000.00   /   359.99" + "\r\n" +
                "000.100   /   120.000" + "\r\n" +
                "000.100   /   120.000" + "\r\n" +
                "  50   /   800" + "\r\n" +
                "0   /   1" + "\r\n" +
                "0   /   8" + "\r\n" +
                "0   /   8" + "\r\n" +
                "0   /   1" + "\r\n" +
                "0.00   /   5.00" + "\r\n" +
                "0.016   /   5.000" + "\r\n" +
                "00000   /   12000" + "\r\n" +
                "000.0   /   100.0" + "\r\n" +
                "000   /   400" + "\r\n" +
                "000   /   100" + "\r\n" +
                "00   /   99" + "\r\n");

                //counter
                file.Write(
                "0   /   9" + "\r\n" +
                "         0   /   10000" + "\r\n" +
                "                   0  /  4294967295" + "\r\n" +
                "                   0  /  4294967295" + "\r\n" +
                "                   0  /  4294967295" + "\r\n" +
                "                   0  /  4294967295" + "\r\n" +
                "0   /   4" + "\r\n" +
                "0   /   1" + "\r\n" +
                "  2   /   36");
            }
        }

        public static void Logo_txt()
        {
            using (System.IO.FileStream fs = System.IO.File.Create(System.Environment.CurrentDirectory + @"\Config\Logo.txt"))
            {
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(System.Environment.CurrentDirectory + @"\Config\Logo.txt"))
            {
                //logo
                file.Write(
                "\r\n" +
                "ESD" + "\r\n" +
                "B3031" + "\r\n" +
                "宏觀微科技Marking2" + "\r\n" +
                "bB3031-Logo" + "\r\n" +
                "bB3031-Logo-N" + "\r\n" +
                "bB3031-Logo1" + "\r\n" +
                "bB3031-Logo2" + "\r\n" +
                "1-1" + "\r\n" +
                "2-1" + "\r\n" +
                "10-1" + "\r\n" +
                "聖誕樹雪花test-2" + "\r\n" +
                "聖誕樹雪花test-1" + "\r\n" +
                "Lasermarking-1" + "\r\n" +
                "PA-ITRT-MARK-1" + "\r\n" +
                "雷射測試圖形R2007-2" + "\r\n" +
                "雷射測試圖形R2007-1" + "\r\n" +
                "雷射025" + "\r\n" +
                "雷射050");
            }
        }
        static string[] logo;
        public static string[] Logo_file()
        {
            try
            {
                if (!File.Exists(System.Environment.CurrentDirectory + @"\Config\Logo.txt")) Default_Item.Logo_txt();

                //Logo圖檔名稱
                if (File.Exists(System.Environment.CurrentDirectory + @"\Config\Logo.txt"))
                {
                    string line;
                    using (System.IO.StreamReader file = new System.IO.StreamReader(System.Environment.CurrentDirectory + @"\Config\Logo.txt"))
                    {
                        line = file.ReadToEnd();

                        if (line != "")
                            logo = line.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                        else
                            logo = new string[0];

                        Application.DoEvents();
                    }
                }

                return logo;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("LF", "Logo_file," + ex.Message);

                return new string[0];
            }
        }

        //新增圖檔
        public static bool Logo_txt_write(string Logo_name)
        {
            try
            {
                if (!File.Exists(System.Environment.CurrentDirectory + @"\Config\Logo.txt"))
                    using (System.IO.FileStream fs = System.IO.File.Create(System.Environment.CurrentDirectory + @"\Config\Logo.txt"))
                    {
                    }

                bool same = false;
                using (System.IO.StreamReader file = new System.IO.StreamReader(System.Environment.CurrentDirectory + @"\Config\Logo.txt"))
                {
                    string line;

                    while ((line = file.ReadLine()) != null)
                    {
                        if (line != "" && line.Equals(Logo_name))
                        {
                            same = true;
                            break;
                        }

                        Application.DoEvents();
                    }
                }

                if (!same)
                    using (System.IO.StreamWriter file = File.AppendText(System.Environment.CurrentDirectory + @"\Config\Logo.txt"))
                    {
                        //新增沒有的LOGO
                        file.Write("\r\n" + Logo_name);
                    }

                return same;
            }
            catch (Exception ex)
            {
                MPU.WriteErrorCode("LW", "Logo_txt_write," + ex.Message);

                return true;
            }
        }
    }
}
