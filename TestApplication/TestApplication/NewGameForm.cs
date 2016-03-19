using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApplication
{
    public partial class NewGameForm : Form
    {
        public NewGameForm()
        {
            InitializeComponent();
            labelErr.Text = "";
            comboBox1.SelectedIndex = 0;           
        }

        MainForm mf;

        public string getPath()
        {
            if (comboBox1.SelectedIndex == 0)
                return Application.StartupPath + @"\images\fruits\";
            else //(comboBox1.SelectedIndex == 1)
                return Application.StartupPath + @"\images\flovers\";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mf = this.Owner as MainForm;
            if (mf.checkNM((int)numericUpDown1.Value, (int)numericUpDown2.Value))
            {
                mf.setNMPath((int)numericUpDown1.Value, (int)numericUpDown2.Value, getPath());
                this.Close();
            }
            else
            {
                labelErr.Text = "Количество элементов в поле должно быть четным!";
            }
        }
    }
}
