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


namespace Sistema_de_Controle_de_Viagens
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string mstring = @"Server=cp03487; Database=scv; Uid=root; Pwd='';";
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
        string versao = "1";
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
                        MySqlDataAdapter oDA = new MySqlDataAdapter("Insert Into Logs (Log, data) Values ('CCT I" + sql.Replace("'", "-") + " - " + e.Message.Replace("'", "-") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", oCn); ;
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
            info.data_viagem = Convert.ToDateTime(odt.Rows[i]["andamento.data"].ToString());

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
            texto.MouseDoubleClick += prefixo_MouseDoubleClick;
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
            texto2.MouseDoubleClick += prefixo_MouseDoubleClick;
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
            texto3.MouseDoubleClick += prefixo_MouseDoubleClick;
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
            prefixo.MouseDoubleClick += prefixo_MouseDoubleClick;
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

                    for (int i = panel2.Controls.Count - 1; i >= 0; i--)
                        panel2.Controls[i].Dispose();
                    panel1.Controls.Clear();
                    panel2.Controls.Clear();

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

                    odtAux = consulta("select distinct trens.local, Locais.local, Locais.codigo from Trens inner Join Locais on trens.local = locais.codigo where Trens.Linha = " + cmbLinha.SelectedValue + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id and Trens.Linha = andamento.linha) order by locais.codigo", "Trens");
                    left = 10;
                    DataTable odt_aux2;
                    

                    //for para colocar as estações
                    for (int i = 0; i < odtAux.Rows.Count; i++)
                    {
                        Panel local = new Panel();
                        local.Width = 95;
                        local.Height = panel2.Height - 10;
                        local.Top = 0;
                        local.Left = left;
                        local.BorderStyle = BorderStyle.Fixed3D;
                        local.AutoScroll = true;
                        //odt_aux2 = consulta("Select trens.Trem, trens.observacao, locais.local, trens.mensagem from Trens inner Join Locais on trens.local = locais.codigo where Linha = " + cmbLinha.SelectedValue + " and Trens.local = " + odtAux.Rows[i][0].ToString() + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id and Trens.Linha = " + cmbLinha.SelectedValue + ")", "Trens");
                        odt_aux2 = consulta("Select trens.Trem, trens.observacao, locais.local, trens.mensagem from Trens inner Join Locais on trens.local = locais.codigo where Linha = " + cmbLinha.SelectedValue + " and Trens.local = " + odtAux.Rows[i][0].ToString() + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id and Trens.Linha = andamento.linha)", "Trens");

                        //dentro de cada estação, os trens
                        for (int n = 0; n < odt_aux2.Rows.Count; n++)
                        {
                            local.Name = odt_aux2.Rows[n][0].ToString();
                            ToolTip yourToolTip = new ToolTip();
                            //The below are optional, of course,
                            yourToolTip.IsBalloon = true;
                            yourToolTip.ShowAlways = true;

                            
                            Label texto = new Label();

                            texto.Name = odt_aux2.Rows[n][0].ToString();
                            texto.Font = new Font("Arial Black", 14);
                            texto.TextAlign = ContentAlignment.MiddleCenter;
                            texto.Width = 90;
                            //texto.Height = local.Height / 20 + 10;
                            texto.Top = n * 40 + 30;
                            texto.ForeColor = Color.White;
                            texto.Text = odt_aux2.Rows[n][0].ToString();
                            texto.BackColor = Color.Transparent;
                            texto.ContextMenuStrip = cms_recolhidos;

                            if (odt_aux2.Rows[n]["mensagem"].ToString() != "")
                            {
                                yourToolTip.SetToolTip(texto, odt_aux2.Rows[n][1].ToString() + " " + odt_aux2.Rows[n]["mensagem"].ToString());
                                texto.BackColor = Color.AntiqueWhite;
                                texto.ForeColor = Color.Black;
                            }
                            else
                                yourToolTip.SetToolTip(texto, odt_aux2.Rows[n][1].ToString());
                            
                            local.Controls.Add(texto);

                        }
                        left += local.Width + 20;

                        Label texto1 = new Label();

                        texto1.Name = odtAux.Rows[i][1].ToString();
                        texto1.Font = new Font("Arial Black", 14);
                        texto1.TextAlign = ContentAlignment.MiddleCenter;
                        texto1.Width = 90;
                        texto1.Height = prefixo.Height / 5 + 10;
                        texto1.Top = 0;
                        texto1.ForeColor = Color.Red;
                        texto1.Text = odtAux.Rows[i][1].ToString();
                        texto1.BackColor = Color.Transparent;
                        local.Controls.Add(texto1);
                        panel2.Controls.Add(local);

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

       private void executaEditar()
	    {
		    try
		    {
			    string sql = "Update Trens Set Mensagem = '" + mensagem + txt_obs.Text + "' where trem = '" + cmbTrem.Text + "'";

                consulta(sql, "trens");

			    pnl_acao.Visible = false;
		    }
		    catch (Exception e)
		    {
                timer1.Enabled = false;
			    //MessageBox.Show("Err CCT Io ao Editar!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT I editar - " + e.Message + "', '" + DateTime.Now + "')", "Log");
                
                timer1.Enabled = true;
		    }
	    }

        private void executaEditarLocal()
        {
            try
            {
                string sql = "Update Trens Set Trens.Local = '" + cmb_local.SelectedValue + "', trens.observacao = '" + txt_obs.Text + "', Trens.mensagem = '" + mensagem + "', Trens.Data = DateValue('" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "') where Trem = '" + cmbTrem.Text.ToUpper() + "'";

                consulta(sql, "trens");

                pnl_acao.Visible = false;
            }
            catch (Exception e)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT Io ao Editar Local!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT I editar - " + e.Message + "', '" + DateTime.Now + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void executaEditarMensagem()
        {
            try
            {
                string sql = "Update Trens Set Mensagem = '" + mensagem + txt_obs.Text + "' where trem = '" + cmbTrem.Text + "'";

                consulta(sql, "trens");

                pnl_acao.Visible = false;
            }
            catch (Exception e)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT Io ao Editar Mensagem!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT I editar - " + e.Message + "', '" + DateTime.Now + "')", "Log");
                timer1.Enabled = true;
            }
        }
	

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                oCn = new MySqlConnection(mstring);
                this.Width = Screen.PrimaryScreen.Bounds.Width;
                tabControl1.Width = this.Width-15; 
                panel1.Width = this.Width - 40;
                panel2.Width = this.Width - 40;
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
        }


        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                string msg_aux;
                msg_aux = mensagem;
                if (rdb_msg.Checked == false)
                    mensagem = "";
       
                if (groupBox1.Text == "Editar Mensagem")
                {
                    executaEditar();
                    odtAux = consulta("update Linha set atualizacao = (atualizacao + 1) where codigo = " + linha, "linha");
                    atualizacao = atualizacao + 1;
                }
                txt_obs.Text = "";

                atualiza();
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT Io na conexão codigo 02!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT I - Gravar" + ex.Message + "', '" + DateTime.Now + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void prefixo_MouseDoubleClick(object sender, EventArgs e)
        {
            try
            {
                Label Trem = (Label)sender;
                Info info = (Info)Trem.Tag;

                cmbTrem.DataSource = consulta("Select Trem From Trens where Linha = " + cmbLinha.SelectedValue + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id) Order By Trem", "Trens");
                cmbTrem.DisplayMember = "Trem";

                Trem.Parent.BackColor = Color.Green;
                pnl_acao.Visible = true;
                groupBox1.Text = "Editar Mensagem";
                lblLocal.Visible = false;
                cmb_local.Visible = false;
                lbl_obs.Text = "Obs.";
                lbl_obs.Visible = true;
                txt_obs.Visible = true;
                lbl_prefixo.Visible = true;
                txtPrefixo.Visible = true;
                cmbTrem.Enabled = false;
                txtPrefixo.Text = info.prefixo;
                cmbTrem.Text = info.trem;
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT Io ao Editar Mensagem!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT I editar - " + ex.Message + "', '" + DateTime.Now + "')", "Log");
                timer1.Enabled = true;
            }
        }

         private void btnCancelar_Click(object sender, EventArgs e)
        {
            pnl_acao.Visible = false;
            cmb_local.Text = "";
            
            cmbTrem.Text = "";
            txtPrefixo.Text = "";
            atualiza();
        }

        private void Ok()
        {
            linha = cmbLinha.SelectedValue.ToString();
            //dia = dateTimePicker1.Value.Date;
            //sSqlImpar = "SELECT * from andamento Where Linha = " + linha + " and Data = DateValue('" + dia.Date + "') and (Sequencia mod 2) = 1 order by sequencia";
            //sSqlPar = "SELECT * from andamento Where Linha = " + linha + " and Data = DateValue('" + dia.Date + "') and (Sequencia mod 2) = 0 order by sequencia";
            pnl_acao.Visible = false;
            atualiza();
        }
 		
        private void cmbLinha_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ok();
        }

        private void cmb_local_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnGravar.PerformClick();
            e.Handled = true;
        }

        private void pnl_acao_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                pnl_acao.Location = new Point(Form.MousePosition.X - this.Location.X - 10, Form.MousePosition.Y - this.Location.Y - 30);
                
        }
        private void pnl_acao_VisibleChanged(object sender, EventArgs e)
        {
            if (pnl_acao.Visible == true)
            {
                dia = DateTime.Now;

                if (dia.Hour < 3)
                    dia = dia.AddDays(-1);
                panel1.Enabled = false;
                btnGravar.Focus();

                txtPrefixo.Focus();
                //Cursor.Position = new Point(this.Location.X + pnl_acao.Location.X + btnGravar.Location.X + 60, this.Location.Y + pnl_acao.Location.Y + btnGravar.Location.Y + 60);
            }
            else
            {
                panel1.Enabled = true;
            }
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
                        if (pnl_acao.Visible == true)
                        {
                            timer1.Enabled = false;
                            MessageBox.Show("Uma atualização foi feita por outro usuário, favor, repita a operação", "Sistema de Controle de Viagens");
                            pnl_acao.Visible = false;
                            timer1.Enabled = true;
                        }
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

        private void verifica_mensagem(object sender, EventArgs e)
        {
            odtAux = consulta("select mensagem from trens where trem = '" + cmbTrem.Text + "'", "trens");
            try
            {
                if (odtAux.Rows.Count > 0 && odtAux.Rows[0][0].ToString() != "")
                {
                    mensagem = odtAux.Rows[0][0].ToString() + " ";
                    rdb_msg.Visible = true;
                    rdb_msg.Text = "Manter mensagem: " + mensagem;
                    rdb_msg.Checked = true;

                }

                else
                {
                    mensagem = "";
                    rdb_msg.Visible = false;
                    rdb_msg.Text = "";
                    rdb_msg.Checked = true;
                }
            }

            catch
            {
                mensagem = "";
                rdb_msg.Visible = false;
                rdb_msg.Text = "";
                rdb_msg.Checked = true;
            }
        }

        private void txt_obs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnGravar.PerformClick();
        }

        private void cmbTrem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnGravar.PerformClick();
        }

        private void editarMensagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Label Trem = (Label)cms_recolhidos.SourceControl;

                cmbTrem.DataSource = consulta("Select Trem From Trens where Linha = " + cmbLinha.SelectedValue + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id) Order By Trem", "Trens");
                cmbTrem.DisplayMember = "Trem";

                Trem.BackColor = Color.Green;
                pnl_acao.Visible = true;
                groupBox1.Text = "Editar Mensagem";
                lblLocal.Visible = false;
                cmb_local.Visible = false;
                lbl_obs.Text = "Obs.";
                lbl_obs.Visible = true;
                txt_obs.Visible = true;
                lbl_prefixo.Visible = false;
                txtPrefixo.Visible = false;
                cmbTrem.Enabled = false;
                cmbTrem.Text = Trem.Text;
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT Io ao Editar Mensagem!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT I editar - " + ex.Message + "', '" + DateTime.Now + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btn_novo_id_Click(object sender, EventArgs e)
        {
            if (txt_novo_id.Text == "" || txt_novo_id.Text.Length < 3)
            {
                MessageBox.Show("Digite um Id válido", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(radioButton1.Checked == true)
                consulta("insert into trens values (" + linha + ", '" + txt_novo_id.Text.ToUpper() + "', 1, null, null, null)", "trens");
            else
                consulta("update trens set linha = " + linha + " where trem = '"+txt_novo_id.Text.ToUpper()+"'", "trens");
            MessageBox.Show("Trem incluido com sucesso", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
            atualiza();
        }

     }

    class Info
    {
        public string prefixo;
        public string trem;
        public string maquinista;
        public DateTime data_viagem;
        public string mensagem;
        public int sequencia;

        public Info()
        {
            prefixo = "???";
            trem = "";
            maquinista = "";
            mensagem = "";
            sequencia = 0;
        }

        public Info(int sequencia)
        {
            prefixo = "???";
            trem = "";
            maquinista = "";
            mensagem = "";
            this.sequencia = sequencia;
        }
    }
}
