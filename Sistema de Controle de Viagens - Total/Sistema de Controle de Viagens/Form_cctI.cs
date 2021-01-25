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
    public partial class Form_cctI : Form
    {
        public Form_cctI()
        {
            InitializeComponent();
        }
        
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
                
        private void corPrefixo(Panel prefixo)
        {
            prefixo.Controls[0].BackColor = Color.Green;
            prefixo.Controls[1].BackColor = Color.Green;
            prefixo.Controls[2].BackColor = Color.Green;

            
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

            //if (odt.Rows[i]["Destino_Diferente"].ToString() == "SIM")
            //{
            //    texto.BackColor = Color.FromArgb(70, 155, 255);
            //    texto.ForeColor = Color.Black;
            //}
            if (odt.Rows[i]["Destino_Diferente"].ToString() == "SIM" && int.Parse(info.prefixo) < 700)
            {
                texto.BackColor = Color.FromArgb(70, 155, 255);
                texto.ForeColor = Color.Black;
            }

            else if (odt.Rows[i]["Destino_Diferente"].ToString() == "SIM" || int.Parse(info.prefixo) >= 700)
            {
                texto.BackColor = Color.Transparent;
                texto.ForeColor = Color.FromArgb(70, 155, 255);
            }

            else
            {
                texto.BackColor = Color.Transparent;

            }
            prefixo.MouseDoubleClick += prefixo_MouseDoubleClick;
            panel1.Controls.Add(prefixo);
			
        }

        private void atualiza()
        {
            int define_top1;
            int define_top2;

            DataTable odt_aux;

            odt_aux = banco_dados.consulta(timer1, "select * from linha where codigo = " + linha + "", "linha");

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
                    odtImpar = banco_dados.consulta(timer1, sSqlImpar, "andamento");
                    odtPar = banco_dados.consulta(timer1, sSqlPar, "andamento");
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

                    odtAux = banco_dados.consulta(timer1, "select distinct trens.local, Locais.local, Locais.codigo from Trens inner Join Locais on trens.local = locais.codigo where Trens.Linha = " + cmbLinha.SelectedValue + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id and Trens.Linha = andamento.linha) order by locais.codigo", "Trens");
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
                        //odt_aux2 = banco_dados.consulta(timer1, "Select trens.Trem, trens.observacao, locais.local, trens.mensagem from Trens inner Join Locais on trens.local = locais.codigo where Linha = " + cmbLinha.SelectedValue + " and Trens.local = " + odtAux.Rows[i][0].ToString() + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id and Trens.Linha = " + cmbLinha.SelectedValue + ")", "Trens");
                        odt_aux2 = banco_dados.consulta(timer1, "Select trens.Trem, trens.observacao, locais.local, trens.mensagem from Trens inner Join Locais on trens.local = locais.codigo where Linha = " + cmbLinha.SelectedValue + " and Trens.local = " + odtAux.Rows[i][0].ToString() + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id and Trens.Linha = andamento.linha)", "Trens");

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
                    banco_dados.consulta(timer1, "Insert Into Logs (Log, data) Values ('Err CCT I Atualização - " + e.Message + "', '" + DateTime.Now + "')", "Log");
                    timer1.Enabled = true;
                }

            
        }

       private void executaEditar()
	    {
		    try
		    {
			    string sql = "Update Trens Set Mensagem = '" + mensagem + txt_obs.Text + "' where trem = '" + cmbTrem.Text + "'";

                banco_dados.consulta(timer1, sql, "trens");

			    pnl_acao.Visible = false;
		    }
		    catch (Exception e)
		    {
                timer1.Enabled = false;
			    //MessageBox.Show("Err CCT Io ao Editar!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                banco_dados.consulta(timer1, "Insert Into Logs (Log, data) Values ('Err CCT I editar - " + e.Message + "', '" + DateTime.Now + "')", "Log");
                
                timer1.Enabled = true;
		    }
	    }

        private void executaEditarLocal()
        {
            try
            {
                string sql = "Update Trens Set Trens.Local = '" + cmb_local.SelectedValue + "', trens.observacao = '" + txt_obs.Text + "', Trens.mensagem = '" + mensagem + "', Trens.Data = DateValue('" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "') where Trem = '" + cmbTrem.Text.ToUpper() + "'";

                banco_dados.consulta(timer1, sql, "trens");

                pnl_acao.Visible = false;
            }
            catch (Exception e)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT Io ao Editar Local!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                banco_dados.consulta(timer1, "Insert Into Logs (Log, data) Values ('Err CCT I editar - " + e.Message + "', '" + DateTime.Now + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void executaEditarMensagem()
        {
            try
            {
                string sql = "Update Trens Set Mensagem = '" + mensagem + txt_obs.Text + "' where trem = '" + cmbTrem.Text + "'";

                banco_dados.consulta(timer1, sql, "trens");

                pnl_acao.Visible = false;
            }
            catch (Exception e)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT Io ao Editar Mensagem!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                banco_dados.consulta(timer1, "Insert Into Logs (Log, data) Values ('Err CCT I editar - " + e.Message + "', '" + DateTime.Now + "')", "Log");
                timer1.Enabled = true;
            }
        }
	

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.Width = Screen.PrimaryScreen.Bounds.Width;
                tabControl1.Width = this.Width-15; 
                panel1.Width = this.Width - 40;
                panel2.Width = this.Width - 40;
                dataGridView1.Width = this.Width - 40;
                Tamanho = panel1.Width / 13 - 10;
                panel1.Left = (this.Width - panel1.Width) / 2 - 5;

                dia = DateTime.Now;
                if (dia.Hour < 3)
                    dia = dia.AddDays(-1);

                
                cmbLinha.DisplayMember = "Linha";
                cmbLinha.ValueMember = "Codigo";
                cmbLinha.DataSource = banco_dados.consulta(timer1, "Select * From Linha", "Linha");
                
                linha = cmbLinha.SelectedValue.ToString();
                
                panel1.Focus();
                panel1.Visible = true;
                banco_dados.consulta(timer1, "Update Linha set Atualizacao = 0", "Linha");
                atualizacao = 0;
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT Io na conexão codigo 01!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                banco_dados.consulta(timer1, "Insert Into Logs (Log, data) Values ('Err CCT I Load - " + ex.Message + "', '" + DateTime.Now + "')", "Log");
                timer1.Enabled = true;

            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
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
                    odtAux = banco_dados.consulta(timer1, "update Linha set atualizacao = (atualizacao + 1) where codigo = " + linha, "linha");
                    atualizacao = atualizacao + 1;
                }
                txt_obs.Text = "";

                atualiza();
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT Io na conexão codigo 02!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                banco_dados.consulta(timer1, "Insert Into Logs (Log, data) Values ('Err CCT I - Gravar" + ex.Message + "', '" + DateTime.Now + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void prefixo_MouseDoubleClick(object sender, EventArgs e)
        {
            try
            {
                Label Trem = (Label)sender;
                Info info = (Info)Trem.Tag;

                cmbTrem.DataSource = banco_dados.consulta(timer1, "Select Trem From Trens where Linha = " + cmbLinha.SelectedValue + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id) Order By Trem", "Trens");
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
                banco_dados.consulta(timer1, "Insert Into Logs (Log, data) Values ('Err CCT I editar - " + ex.Message + "', '" + DateTime.Now + "')", "Log");
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
            dateTimePicker1_ValueChanged(new object(), new EventArgs());
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
                odtAux = banco_dados.consulta(timer1, "Select atualizacao from linha where codigo = " + linha, "linha");
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
                odtAux = banco_dados.consulta(timer1, "Select versao from versao", "versao");
                if (odtAux.Rows.Count > 0)
                    if (odtAux.Rows[0]["versao"].ToString() != scv.versao)
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
                banco_dados.consulta(timer1, "Insert Into Logs (Log, data) Values ('Err CCT I timer - " + ex.Message + "', '" + DateTime.Now + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void cmbLinha_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void verifica_mensagem(object sender, EventArgs e)
        {
            odtAux = banco_dados.consulta(timer1, "select mensagem from trens where trem = '" + cmbTrem.Text + "'", "trens");
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

                cmbTrem.DataSource = banco_dados.consulta(timer1, "Select Trem From Trens where Linha = " + cmbLinha.SelectedValue + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id) Order By Trem", "Trens");
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
                banco_dados.consulta(timer1, "Insert Into Logs (Log, data) Values ('Err CCT I editar - " + ex.Message + "', '" + DateTime.Now + "')", "Log");
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
                banco_dados.consulta(timer1, "insert into trens values (" + linha + ", '" + txt_novo_id.Text.ToUpper() + "', 1, null, null)", "trens");
            else
                banco_dados.consulta(timer1, "update trens set linha = " + linha + " where trem = '"+txt_novo_id.Text.ToUpper()+"'", "trens");
            MessageBox.Show("Trem incluido com sucesso", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
            atualiza();
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
            DataTable odt_aux = banco_dados.consulta(timer1, "select distinct trem from historico where Linha = " + linha + " and data = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' Order by trem", "historico");
            DataTable odt_aux1;
            int j, i;
            dataGridView1.Columns.Clear();
            // varre as linhas...
            for (j = 0; j < odt_aux.Rows.Count; j++)
            {
                dataGridView1.Columns.Add(odt_aux.Rows[j][0].ToString(), odt_aux.Rows[j][0].ToString());
                odt_aux1 = banco_dados.consulta(timer1, "select prefixo, final_viagem from historico where (trem like '%" + txt_filtro.Text + "%' or prefixo like '%" + txt_filtro.Text + "%') and trem = '" + odt_aux.Rows[j][0].ToString() + "' and Linha = " + linha + " and data = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' Order by trem", "historico");
                for (i = 0; i < odt_aux1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows.Count <= i + 1)
                        dataGridView1.Rows.Add();

                    dataGridView1.Rows[i].Cells[j].Value = odt_aux1.Rows[i][0].ToString();
                    if (odt_aux1.Rows[i]["final_viagem"].ToString() == "Excluído")
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Red;
                }
            }            
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

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            dateTimePicker1_ValueChanged(null, null);
        }

     }

}
