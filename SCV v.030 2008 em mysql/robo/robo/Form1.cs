using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;


namespace robo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string mstring, sql, prefixo_line = "";
            int prefixo_banco, prefixo_txt = 0;

            MySqlConnection oCn; //MySQL
            StreamReader objReader = new StreamReader("conexao");
            mstring = objReader.ReadLine();
            oCn = new MySqlConnection(mstring);
            oCn.Open();
            sql = "select prefixo from andamento where linha = 1 and sequencia % 2 = 1 order by sequencia desc limit 1";
            MySqlDataAdapter oDA = new MySqlDataAdapter(sql, oCn);
            DataSet oDs = new DataSet();
            oDA.Fill(oDs, "andamento");
            DataTable andamento = oDs.Tables["andamento"];


            //esse é o código para pegar o arquivo, quando em produção usá-lo
            sql = "select pasta from linha where codigo = 1";
            oDA = new MySqlDataAdapter(sql, oCn);
            oDs = new DataSet();
            oDA.Fill(oDs, "linha");
            DataTable table_linha = oDs.Tables["linha"];

            DirectoryInfo dir = new DirectoryInfo(table_linha.Rows[0]["pasta"].ToString());
            FileInfo[] files = dir.GetFiles();
            string file = (from f in dir.GetFiles()
                           orderby f.LastWriteTime descending
                           select f).First().Name;
            prefixo_banco = int.Parse(andamento.Rows[0]["prefixo"].ToString());
            objReader = new StreamReader(file);

            while (prefixo_line != null)
            {
                prefixo_line = objReader.ReadLine();
                if(prefixo_line != null)
                    if (prefixo_line.Length > 17)
                    {
                        int.TryParse(prefixo_line.Substring(14, 3), out prefixo_txt);
                        if ((prefixo_txt) == prefixo_banco + 2)
                        {
                            MessageBox.Show(prefixo_banco.ToString() + " " + prefixo_line.Substring(14, 3));
                        }
                    }
            }


            oCn.Close();
        }
    }
}
