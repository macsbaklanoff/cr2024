using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cr2024
{
    public partial class Form2 : Form
    {
        Form1 form;
        public Form2(Form1 form1)
        {
            InitializeComponent();
            form = form1;
            listBox1.Items.Add("");
            foreach (char c in form.textBox1.Text)
            {
                listBox1.Items.Add(c);
            }
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите символ");
                return;
            }
            form.changedcell(listBox1.SelectedItem.ToString());
            this.Close();
        }
    }
}
