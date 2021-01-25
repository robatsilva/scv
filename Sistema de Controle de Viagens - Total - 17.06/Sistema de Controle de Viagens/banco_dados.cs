using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace Sistema_de_Controle_de_Viagens
{
    class banco_dados
    {
        private static StreamReader objReader = new StreamReader("conexao");
        private static string mstring = objReader.ReadToEnd();
        public static bool status_consulta = false;
        private static MySqlConnection oCn = new MySqlConnection(mstring); //MySQL
        
        public banco_dados()
        {
           
        }
        public static DataTable consulta(Timer timer1, string sql, string tabela)
        {
            try
            {
                if (oCn.State != ConnectionState.Open)
                    oCn.Open();
                MySqlDataAdapter oDA = new MySqlDataAdapter(sql, oCn);
                DataSet oDs = new DataSet();
                oDA.Fill(oDs, tabela);
                status_consulta = true;
                oCn.Close();
                return oDs.Tables[tabela];
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                status_consulta = false;
                timer1.Enabled = false;
                if (oCn.State == ConnectionState.Open)
                {

                    oCn.Close();
                    oCn.Open();

                    try
                    {

                        MySqlDataAdapter oDA = new MySqlDataAdapter("Insert Into Logs (Log, data) Values ('CCT II" + sql.Replace("'", "-") + " - " + e.Message.Replace("'", "-") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", oCn); ;
                        DataSet oDs = new DataSet();
                        oDA.Fill(oDs, tabela);
                        oCn.Close();
                    }
                    catch
                    {
                        oCn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Você não está conectado, Se a conexão não retornar reinicie o programa!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                timer1.Enabled = true;
                return null;
            }


        }
    }
}
