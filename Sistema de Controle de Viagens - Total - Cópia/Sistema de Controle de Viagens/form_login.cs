﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Principal;
namespace Sistema_de_Controle_de_Viagens
{
    public partial class form_login : Form
    {
        public form_login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "cco")
            {
                Form_cct1 frm_cct1 = new Form_cct1();
                frm_cct1.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Senha incorreta");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "cco")
            {
                Form_cct2 frm_cct2 = new Form_cct2();
                frm_cct2.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Senha incorreta");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "escala")
            {
                Form_escala frm_escala = new Form_escala();
                frm_escala.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Senha incorreta");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                Form_visualizacao frm_visualizacao = new Form_visualizacao();
                frm_visualizacao.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Senha incorreta");
        }

        private void form_login_Load(object sender, EventArgs e)
        {
            
        }
    }
}
