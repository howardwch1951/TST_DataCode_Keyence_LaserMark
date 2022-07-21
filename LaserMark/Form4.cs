using System;
using System.Windows.Forms;

namespace LaserMark
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(Owner.Location.X + Owner.Width, Owner.Location.Y + 100);
            this.MaximizeBox = false;

            if (Var.English)
            {
                label1.Text = "Delete Model name";
                button1.Text = "Yes";
                button2.Text = "No";
            }
            else
            {
                label1.Text = "選擇要刪除的品別名稱";
                button1.Text = "確定";
                button2.Text = "取消";
            }

            comboBox1.Items.Clear();
            AddComboBoxItems();
        }
        public void AddComboBoxItems()
        {
            try
            {
                for (int i = 0; i < Var.Model_select.Rows.Count; i++)
                    comboBox1.Items.Add(Var.Model_select.Rows[i][0].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Form4_AddComboBoxItems : " + ex.ToString());
                MPU.WriteErrorCode("Form4", "Form4_AddComboBoxItems : " + ex.Message.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定要刪除該品項參數?", "刪除確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                String Delete_Model = comboBox1.Text;

                Access_data.Access_Delete(Delete_Model);
                Var.Model_select = Access_data.Access_Select("select Model from Model order by Model ASC");
                comboBox1.Items.Remove(Delete_Model);

                ComboBox C1 = Owner.Controls.Find("Combobox1", true)[0] as ComboBox;
                C1.Items.Remove(Delete_Model);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
