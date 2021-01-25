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


namespace Sistema_de_Controle_de_Viagens
{
    public partial class Form_visualizacao : Form
    {
        public Form_visualizacao()
        {
            InitializeComponent();
        }
        string mstring;
        MySqlConnection oCn; //MySQL
        //string mstring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\masterpixftp\scv\luz\SCV.accdb";
        //string mstring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../../../SCV.accdb";
        //OleDbConnection oCn;
        
        DataTable odtImpar;
        DataTable odtPar;
        DataTable odtAux;

        DateTime dia;
        string linha;
        string sSqlImpar;
        string sSqlPar;
        string mensagem;
        int left, atualizacao;
        int Tamanho;
        string versao = "2";
        bool status_consulta = false;

        private void corPrefixo(Panel prefixo)
        {
            prefixo.Controls[0].BackColor = Color.Green;
            prefixo.Controls[1].BackColor = Color.Green;
            prefixo.Controls[2].BackColor = Color.Green;

            
        }

        private DataTable consulta(string sql, string tabela)
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
                status_consulta = false;
                timer1.Enabled = false;
                if (oCn.State == ConnectionState.Open)
                {

                    oCn.Close();
                    oCn.Open();

                    try
                    {
                        MessageBox.Show(e.Message);
                        MySqlDataAdapter oDA = new MySqlDataAdapter("Insert Into Logs (Log, data) Values ('Veiw " + sql.Replace("'", "-") + " - " + e.Message.Replace("'", "-") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", oCn); ;
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

        private void carregaPanel(DataTable odt, Panel prefixo, int widthPic, int leftPic, int i, int top, int posicao)
        {
            Info info = new Info();
            prefixo = new Panel();
           
            info.prefixo = int.Parse(odt.Rows[i]["prefixo"].ToString()).ToString("000");
            info.trem = odt.Rows[i]["id"].ToString();
            info.maquinista = odt.Rows[i]["maquinista"].ToString();
            info.sequencia = int.Parse(odt.Rows[i]["Sequencia"].ToString());
            info.mensagem = odt.Rows[i]["mensagem"].ToString();
            info.data_viagem = Convert.ToDateTime(odt.Rows[i]["data"].ToString());

            prefixo.Name = "prefixo" + odt.Rows[i]["id"].ToString();
            
            prefixo.Tag = info;
            prefixo.Font = new Font("Arial Black", 13);
            //prefixo.BackColor = Color.White;
            prefixo.Left = left;
            prefixo.Top = top;
            prefixo.Width = Tamanho;
            prefixo.Height = panel1.Height / 2 - 20; //subtrai 20 para espaçar por igual nas bordas laterais

            Label texto = new Label();
            texto.Name = "texto1";
            texto.Font = new Font("Arial Black", 16);
            texto.TextAlign = ContentAlignment.MiddleCenter;
            texto.Width = prefixo.Width;
            texto.Height = prefixo.Height / 3;
            texto.ForeColor = Color.White;
            texto.Text = info.prefixo;
            texto.BackColor = Color.Transparent;
            texto.Tag = info;
            
            prefixo.Controls.Add(texto);
            
            Label texto2 = new Label();
            texto2.Font = new Font("Arial Black", 16);
            texto2.TextAlign = ContentAlignment.TopCenter;
            texto2.Width = prefixo.Width;
            texto2.Height = prefixo.Height / 3;
            texto2.ForeColor = Color.Red;
            texto2.Text = info.trem;
            texto2.Top = prefixo.Height / 3 * 2;
            texto2.BackColor = Color.Transparent;
            texto2.Tag = info;
            
            prefixo.Controls.Add(texto2);
            
            Label texto3 = new Label();
            texto3.Font = new Font("Arial Black", 9);
            texto3.TextAlign = ContentAlignment.TopCenter;
            texto3.Width = prefixo.Width;
            texto3.Height = prefixo.Height / 3;
            texto3.ForeColor = Color.Yellow;
            texto3.Text = info.maquinista;
            texto3.Top = prefixo.Height / 3;
            texto3.BackColor = Color.Transparent;
            texto3.Tag = info;
            
            prefixo.Controls.Add(texto3);
            //prefixo.BackgroundImage = Image.FromFile(@"..\..\frente.jpg");
            //prefixo.BackgroundImageLayout = ImageLayout.Stretch;
            prefixo.BorderStyle = BorderStyle.Fixed3D;
            if (info.mensagem.Trim() != "")
            {
                ToolTip yourToolTip = new ToolTip();
                //The below are optional, of course,
                yourToolTip.IsBalloon = true;
                yourToolTip.ShowAlways = true;
                prefixo.BackColor = Color.FromArgb(205, 201, 201);
                texto.ForeColor = Color.Black;
                yourToolTip.SetToolTip(texto, info.mensagem);
                yourToolTip.SetToolTip(texto2, info.mensagem);
                yourToolTip.SetToolTip(texto3, info.mensagem);
            }

            if (odt.Rows[i]["Destino_Diferente"].ToString() == "SIM")
            {
                texto.BackColor = Color.FromArgb(70, 155, 255);
                texto.ForeColor = Color.Black;
            }
            //if (odt.Rows[i]["Destino_Diferente"].ToString() == "SIM" && int.Parse(info.prefixo) < 700)
            //{
            //    texto.BackColor = Color.FromArgb(70, 155, 255);
            //    texto.ForeColor = Color.Black;
            //}

            //else if (odt.Rows[i]["Destino_Diferente"].ToString() == "SIM" || int.Parse(info.prefixo) >= 700)
            //{
            //    texto.BackColor = Color.Transparent;
            //    texto.ForeColor = Color.FromArgb(70, 155, 255);
            //}

            else
            {
                texto.BackColor = Color.Transparent;

            }
            
            panel1.Controls.Add(prefixo);
			
        }

        private void atualiza()
        {
            int define_top1;
            int define_top2;

            DataTable odt_aux;

            odt_aux = consulta("select * from linha where codigo = " + linha + "", "linha");

            if (odt_aux.Rows[0]["prefixo_cima"].ToString() == "Impar")
            {
                define_top1 = 10;
                define_top2 = panel1.Height - 60 - (panel1.Height / 2 - 60);
            }
            else
            {
                define_top1 = panel1.Height - 60 - (panel1.Height / 2 - 60);
                define_top2 = 10;
            }

            sSqlImpar = "SELECT * from (andamento Left Join Trens on andamento.id = trens.trem) left join maquinistas on maquinistas.matricula = andamento.maquinista Where andamento.Linha = " + linha + " and (Sequencia mod 2) = 1 order by sequencia";
            sSqlPar = "SELECT * from (andamento Left Join Trens on andamento.id = trens.trem) left join maquinistas on maquinistas.matricula = andamento.maquinista Where andamento.Linha = " + linha + "  and (Sequencia mod 2) = 0 order by sequencia";
                try
                {
                    System.Threading.Thread.Sleep(50);
                    odtImpar = consulta(sSqlImpar, "andamento");
                    odtPar = consulta(sSqlPar, "andamento");
                    left = 30;
                    for (int i = panel1.Controls.Count -1; i>=0; i--)
                        panel1.Controls[i].Dispose();

                    panel1.Controls.Clear();

                    GC.Collect();
                    int posicao = panel1.Width - Tamanho;
                    if (odtImpar.Rows.Count > 1)
                        posicao = (((panel1.Width - 60 - (Tamanho * odtImpar.Rows.Count)) / (odtImpar.Rows.Count - 1)) + Tamanho);
                    Panel prefixo = new Panel();
                    for (int i = odtImpar.Rows.Count - 1; i >= 0; i--)
                    {
                        carregaPanel(odtImpar, prefixo, posicao - Tamanho, Tamanho - posicao, i, define_top1, posicao);
                        left = left + posicao;             
                    }

                    left = panel1.Width - Tamanho - 30;
                    if (odtPar.Rows.Count > 1)
                        posicao = (((panel1.Width - 60 - (Tamanho * odtPar.Rows.Count)) / (odtPar.Rows.Count - 1)) + Tamanho);
                    for (int i = odtPar.Rows.Count - 1; i >= 0; i--)
                    {
                        carregaPanel(odtPar, prefixo, posicao - Tamanho, Tamanho, i, define_top2, posicao);
                        left = left - posicao;
                    }
                   
                    lbl_circulacao.Text = "Circulando: " + (odtImpar.Rows.Count + odtPar.Rows.Count).ToString();
                    odtImpar = null;
                    odtPar = null;
                }
                catch (Exception e)
                {
                    timer1.Enabled = false;
                    //MessageBox.Show("Err CCT Io na atualização!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    consulta("Insert Into Logs (Log, data) Values ('Err CCT I Atualização - " + e.Message + "', '" + DateTime.Now + "')", "Log");
                    timer1.Enabled = true;
                }

            
        }

    
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                StreamReader objReader = new StreamReader("conexao");
                mstring = objReader.ReadToEnd();
                oCn = new MySqlConnection(mstring);
                this.Width = Screen.PrimaryScreen.Bounds.Width;
                tabControl1.Width = this.Width-15; 
                panel1.Width = this.Width - 40;
                
                dataGridView1.Width = this.Width - 40;
                Tamanho = panel1.Width / 13 - 10;
                panel1.Left = (this.Width - panel1.Width) / 2 - 5;

                dia = DateTime.Now;
                if (dia.Hour < 3)
                    dia = dia.AddDays(-1);

                
                cmbLinha.DisplayMember = "Linha";
                cmbLinha.ValueMember = "Codigo";
                cmbLinha.DataSource = consulta("Select * From Linha", "Linha");
                
                linha = cmbLinha.SelectedValue.ToString();
                
                panel1.Focus();
                panel1.Visible = true;
                consulta("Update Linha set Atualizacao = 0", "Linha");
                atualizacao = 0;
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT Io na conexão codigo 01!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT I Load - " + ex.Message + "', '" + DateTime.Now + "')", "Log");
                timer1.Enabled = true;

            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            oCn.Close();
            oCn.Dispose();
            Environment.Exit(0);
        }


        private void Ok()
        {
            linha = cmbLinha.SelectedValue.ToString();
            //dia = dateTimePicker1.Value.Date;
            //sSqlImpar = "SELECT * from andamento Where Linha = " + linha + " and Data = DateValue('" + dia.Date + "') and (Sequencia mod 2) = 1 order by sequencia";
            //sSqlPar = "SELECT * from andamento Where Linha = " + linha + " and Data = DateValue('" + dia.Date + "') and (Sequencia mod 2) = 0 order by sequencia";
            atualiza();
        }
 		
        private void cmbLinha_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ok();
            dateTimePicker1_ValueChanged(new object(), new EventArgs());
        }

          

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            try
            {
                dia = DateTime.Now;

                if (dia.Hour < 3)
                    dia = dia.AddDays(-1);
                odtAux = consulta("Select atualizacao from linha where codigo = " + linha, "linha");
                if (odtAux.Rows.Count > 0)
                    if (atualizacao != int.Parse(odtAux.Rows[0][0].ToString()))
                    {                      
                        atualizacao = int.Parse(odtAux.Rows[0][0].ToString());
                        atualiza();

                    }
                odtAux = consulta("Select versao from versao", "versao");
                if (odtAux.Rows.Count > 0)
                    if (odtAux.Rows[0]["versao"].ToString() != versao)
                    {
                        timer1.Enabled = false;
                        MessageBox.Show("Essa versão do aplicativo não é a mais atual, favor abrir a nova versão!", "Sistema de Controle de Viagens");
                        this.Close();
                    }
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT Io na conexão codigo 10!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT I timer - " + ex.Message + "', '" + DateTime.Now + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void cmbLinha_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

    
        private void ToCsV(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "Linha " + cmbLinha.Text + " - " + dateTimePicker1.Value.ToShortDateString() + "\r\n";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i <= dGV.RowCount - 1; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DataTable odt_aux = consulta("select distinct trem from historico where Linha = " + linha + " and data = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' Order by trem", "historico");
            DataTable odt_aux1;
            int j, i;
            dataGridView1.Columns.Clear();
            // varre as linhas...
            for (j = 0; j < odt_aux.Rows.Count; j++)
            {
                dataGridView1.Columns.Add(odt_aux.Rows[j][0].ToString(), odt_aux.Rows[j][0].ToString());
                odt_aux1 = consulta("select prefixo, final_viagem from historico where (trem like '%" + txt_filtro.Text + "%' or prefixo like '%" + txt_filtro.Text + "%') and trem = '" + odt_aux.Rows[j][0].ToString() + "' and Linha = " + linha + " and data = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' Order by trem", "historico");
                for (i = 0; i < odt_aux1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows.Count <= i + 1)
                        dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[j].Value = odt_aux1.Rows[i][0].ToString();
                    if (odt_aux1.Rows[i]["final_viagem"].ToString() == "Excluído")
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Red;
                }
            }
            oCn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Linha " + cmbLinha.Text + " - " + dateTimePicker1.Value.ToShortDateString().Replace('/', '.');
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //ToCsV(dataGridView1, @"c:\export.xls");
                ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printDGV.Print_DataGridView(dataGridView1, "Linha " + cmbLinha.Text + " - " + dateTimePicker1.Value.ToShortDateString());
        }

        private void txt_filtro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                dateTimePicker1_ValueChanged(null, null);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            dateTimePicker1_ValueChanged(null, null);
        }

     }

}
