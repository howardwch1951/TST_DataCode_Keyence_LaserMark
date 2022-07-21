using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LaserMark
{
    public partial class Tabpage_Line : UserControl
    {
        private static int No;
        Label[] Column_l = new Label[27];
        TextBox[] Text_l = new TextBox[27];
        Label[] LabelOL_l = new Label[27];
        Label[] LabelL_l = new Label[27];

        public Tabpage_Line()
        {
            InitializeComponent();

            No ++;

            for (int i = 0; i < 27; i++)
            {
                Column_l[i] = this.Controls.Find("columnL1_" + (i + 1).ToString(), true)[0] as Label;
                Column_l[i].Name = "columnL" + No + "_" + (i + 1);

                Text_l[i] = this.Controls.Find("textBoxL1_" + (i + 1).ToString(), true)[0] as TextBox;
                Text_l[i].Name = "textBoxL" + No + "_" + (i + 1);
                Text_l[i].AutoSize = false;
                Text_l[i].Height = 23;
                Text_l[i].Font = new Font("微軟正黑體", (float)11, FontStyle.Bold);

                LabelOL_l[i] = this.Controls.Find("labelOL1_" + (i + 1).ToString(), true)[0] as Label;
                LabelOL_l[i].Name = "labelOL" + No + "_" + (i + 1);

                LabelL_l[i] = this.Controls.Find("labelL1_" + (i + 1).ToString(), true)[0] as Label;
                LabelL_l[i].Name = "labelL" + No + "_" + (i + 1);
            }
        }
        public void Tabpage_Line_Refresh(int k)
        {
            No = k;
            
            for (int i = 0; i < 27; i++)
            {
                Column_l[i] = this.Controls.Find("columnL1_" + (i + 1).ToString(), true)[0] as Label;
                Column_l[i].Name = "columnL" + No + "_" + (i + 1);

                Text_l[i] = this.Controls.Find("textBoxL1_" + (i + 1).ToString(), true)[0] as TextBox;
                Text_l[i].Name = "textBoxL" + No + "_" + (i + 1);
                Text_l[i].AutoSize = false;
                Text_l[i].Height = 23;
                Text_l[i].Font = new Font("微軟正黑體", (float)11, FontStyle.Bold);

                LabelOL_l[i] = this.Controls.Find("labelOL1_" + (i + 1).ToString(), true)[0] as Label;
                LabelOL_l[i].Name = "labelOL" + No + "_" + (i + 1);

                LabelL_l[i] = this.Controls.Find("labelL1_" + (i + 1).ToString(), true)[0] as Label;
                LabelL_l[i].Name = "labelL" + No + "_" + (i + 1);
            }
        }
    }
}
