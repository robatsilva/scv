using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sistema_de_Controle_de_Viagens
{
    public partial class frm_acesso : Form
    {
        public frm_acesso()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("CCT I");
            comboBox1.Items.Add("CCT II");
            comboBox1.Items.Add("Escalante");
            comboBox1.Items.Add("Visualização");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            Form1 frm = new Form1(comboBox1.Text);
            frm.Show();
            
        }

        private void txt_senha_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
