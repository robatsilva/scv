﻿using System;
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
        DataTable odtImpar;
        DataTable odtPar;
        DataTable odtAux;

        DateTime dia;
        string linha;
        string sSqlImpar;
        string sSqlPar;
        string maquinista_entrada;
        string maquinista_saida;
        string mensagem;
        DateTime data_viagem;
        int sequencia;
        int codigo_local_vira;
        int left, atualizacao;
        Label sendoMovido;
        Panel sendoMovido_panel;
        int Tamanho;
        bool status_consulta = false;
        string versao = "1";
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

        private void inserePic(Info info, int width, int height, int left, int top, MouseEventHandler MouseClick)
        {
            Label pic = new Label();
            pic.Name = "sequencia " + info.sequencia.ToString();
            pic.Width = width;
            pic.Height = height;
            pic.Tag = info;

            pic.Left = left;
            pic.Top = top;
            pic.AllowDrop = true;

            pic.DragEnter += new DragEventHandler(pic_DragEnter);
            pic.DragDrop += new DragEventHandler(pic_DragDrop);
            pic.DragLeave += new EventHandler(pic_DragLeave);
            pic.MouseEnter += new EventHandler(pic_MouseEnter);
            pic.MouseLeave += new EventHandler(pic_MouseLeave);
            pic.MouseClick += new MouseEventHandler(MouseClick);
            panel1.Controls.Add(pic);
        }

        void pega_maquinista_vira(int codigo, string maquinista)
        {

            codigo_local_vira = codigo;
            string sql = "Select Maquinista, codigo_local_vira "
            + "From viras inner join locais_vira on locais_vira.codigo = viras.codigo_local_vira "
            + "where locais_vira.codigo_linha = " + linha
            + " and sequencia_local_vira = " + codigo_local_vira
            + " and hora_saida is null "
            + "order by sequencia";

            odtAux = consulta(sql, "viras");

            if (odtAux.Rows.Count > 0)
            {
                maquinista_entrada = odtAux.Rows[0]["Maquinista"].ToString();
                codigo_local_vira = int.Parse(odtAux.Rows[0]["codigo_local_vira"].ToString());
            }

            else
            {//senão houver maquinista no vira, volta o maquinista do trem
                maquinista_entrada = maquinista;
                maquinista_saida = "";
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
            texto.ForeColor = Color.White;
            texto.TextAlign = ContentAlignment.MiddleCenter;
            texto.Width = prefixo.Width;
            texto.Height = prefixo.Height / 3;

            texto.Text = info.prefixo;
            texto.Tag = info;
            texto.MouseMove += prefixo_MouseMove;
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
            texto2.MouseMove += prefixo_MouseMove;
            prefixo.Controls.Add(texto2);

            Label texto3 = new Label();
            texto3.Font = new Font("Arial Black", 8);
            texto3.TextAlign = ContentAlignment.TopCenter;
            texto3.Width = prefixo.Width;
            texto3.Height = prefixo.Height / 3;
            texto3.ForeColor = Color.Yellow;
            texto3.Text = odt.Rows[i]["Nome"].ToString();
            texto3.Top = prefixo.Height / 3;
            texto3.BackColor = Color.Transparent;
            texto3.MouseMove += prefixo_MouseMove;
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
            prefixo.ContextMenuStrip = cmsMenu;

            prefixo.MouseMove += new MouseEventHandler(prefixo_MouseMove);
            //prefixo.MouseDoubleClick += prefixo_MouseDoubleClick;
            panel1.Controls.Add(prefixo);

            //insere o pic atrás(no sentido de circulação) do panel prefixo
            inserePic(info, widthPic, prefixo.Height, prefixo.Left + leftPic, prefixo.Top, adicionarToolStripMenuItem_Click);

        }

        private void atualiza()
        {
            int define_top1;
            int define_top2;

            DataTable odt_aux;

            odt_aux = consulta("select * from linha where codigo = "+linha+"", "linha");

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
                for (int i = panel1.Controls.Count - 1; i >= 0; i--)
                    panel1.Controls[i].Dispose();

                for (int i = panel2.Controls.Count - 1; i >= 0; i--)
                    panel2.Controls[i].Dispose();
                panel1.Controls.Clear();
                panel2.Controls.Clear();

                GC.Collect();
                int posicao = panel1.Width - Tamanho;
                int controls = -1;
                if (odtImpar.Rows.Count > 1)
                    posicao = (((panel1.Width - 60 - (Tamanho * odtImpar.Rows.Count)) / (odtImpar.Rows.Count - 1)) + Tamanho);
                Panel prefixo = new Panel();
                for (int i = odtImpar.Rows.Count - 1; i >= 0; i--)
                {
                    carregaPanel(odtImpar, prefixo, posicao - Tamanho, Tamanho - posicao, i, define_top1, posicao);
                    left = left + posicao;                    
                }
                if (odtImpar.Rows.Count > 0) //insere o último Pic (label escondido)
                {
                    Label label = (Label)panel1.Controls[1]; //primeiro pic inserido
                    label.Width = 30; // tamanho das bordas laterais
                    label.Left = 0;
                    label.Name = "prefixo_impar";
                    controls = panel1.Controls.Count - 1; //último pic inserido
                    Info aux = (Info)panel1.Controls[controls].Tag;// tag do último pic inserido
                    Info info = new Info();
                    prefixo = (Panel)panel1.Controls[controls - 1]; //ultimo prefixo inserido
                    try
                    {
                        info.prefixo = (int.Parse(aux.prefixo) - 2).ToString();
                    }
                    catch
                    {
                        info.prefixo = "???";
                    }
                    info.sequencia = (aux.sequencia - 2);
                    inserePic(info, panel1.Width - prefixo.Left - Tamanho, prefixo.Height, prefixo.Left + Tamanho, define_top1, adicionarToolStripMenuItem_Click);
                    //evento no prefixo do vira
                    panel1.Controls[panel1.Controls.Count - 3].Name = "prefixo_impar";
                    panel1.Controls[panel1.Controls.Count - 3].Controls[0].MouseDoubleClick += prefixoImpar_MouseDoubleClick;
                    panel1.Controls[panel1.Controls.Count - 3].Controls[1].MouseDoubleClick += prefixoImpar_MouseDoubleClick;
                    panel1.Controls[panel1.Controls.Count - 3].Controls[2].MouseDoubleClick += prefixoImpar_MouseDoubleClick;
                }
                else
                {
                    int sequencia_aux;
                    odtAux = consulta("Select max(sequencia) From historico Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and (Sequencia mod 2) = 1", "historico");
                    try
                    {
                        sequencia_aux = int.Parse(odtAux.Rows[0][0].ToString()) + 2;
                    }
                    catch
                    {
                        sequencia_aux = -1;
                    }
                    inserePic(new Info(sequencia_aux), panel1.Width, panel1.Height / 2 - 20, 0, define_top1, adicionarPrefixoImparToolStripMenuItem_Click);
                }

                left = panel1.Width - Tamanho - 30;
                if (odtPar.Rows.Count > 1)
                    posicao = (((panel1.Width - 60 - (Tamanho * odtPar.Rows.Count)) / (odtPar.Rows.Count - 1)) + Tamanho);
                for (int i = odtPar.Rows.Count - 1; i >= 0; i--)
                {
                    carregaPanel(odtPar, prefixo, posicao - Tamanho, Tamanho, i, define_top2, posicao);
                    left = left - posicao;
                }
                if (odtPar.Rows.Count > 0)
                {
                    Label label = (Label)panel1.Controls[controls + 3];
                    label.Width = 30;
                    label.Name = "prefixo_par";
                    Info aux = (Info)panel1.Controls[panel1.Controls.Count - 2].Tag;
                    Info info = new Info();
                    prefixo = (Panel)panel1.Controls[panel1.Controls.Count - 2];
                    try
                    {
                        info.prefixo = (int.Parse(aux.prefixo) - 2).ToString();
                    }
                    catch
                    {
                        info.prefixo = "???";
                    }
                    info.sequencia = (aux.sequencia - 2);
                    inserePic(info, prefixo.Left, prefixo.Height, 0, prefixo.Top, adicionarToolStripMenuItem_Click);
                    //evento no prefixo do vira					
                    panel1.Controls[panel1.Controls.Count - 3].Name = "prefixo_par";
                    panel1.Controls[panel1.Controls.Count - 3].Controls[0].MouseDoubleClick += prefixoPar_MouseDoubleClick;
                    panel1.Controls[panel1.Controls.Count - 3].Controls[1].MouseDoubleClick += prefixoPar_MouseDoubleClick;
                    panel1.Controls[panel1.Controls.Count - 3].Controls[2].MouseDoubleClick += prefixoPar_MouseDoubleClick;

                }
                else
                {
                    int sequencia_aux;
                    odtAux = consulta("Select max(sequencia) From historico Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and (Sequencia mod 2) = 0", "historico");
                    try
                    {
                        sequencia_aux = int.Parse(odtAux.Rows[0][0].ToString()) + 2;
                    }
                    catch
                    {
                        sequencia_aux = 0;
                    }
                    inserePic(new Info(sequencia_aux), panel1.Width, panel1.Height / 2 - 20, 0, define_top2, adiiconarPrefixoParToolStripMenuItem_Click);
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
                    odt_aux2 = consulta("Select trens.Trem, trens.observacao, locais.local, trens.mensagem from Trens inner Join Locais on trens.local = locais.codigo where Linha = " + linha + " and Trens.local = " + odtAux.Rows[i][0].ToString() + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id and Trens.Linha = andamento.linha)", "Trens");

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
                //MessageBox.Show("Err CCT IIo na atualização!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II Atualização - " + e.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }


        }

        private void executaAdicionar(string msg)
        {
            try
            {

                string sql = "Update Trens Set Trens.observacao = '', Trens.mensagem = '" + msg + txt_obs.Text + "' where Trem = '" + cmbTrem.Text.ToUpper() + "'";

                consulta(sql, "Trens");

                sql = "Update Andamento Set Sequencia = (Sequencia + 2) where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and (sequencia mod 2) = (" + sequencia.ToString() + " mod 2) and sequencia > " + sequencia.ToString();

                consulta(sql, "Andamento");
                string destino;
                if (lbl_destino_diferenciado.Checked == true)
                    destino = "SIM";
                else
                    destino = "NÃO";

                sql = "INSERT INTO Andamento (Linha, Data, Prefixo, Id, Maquinista, Sequencia, Destino_Diferente) Values ('"
                    + linha + "', '"
                    + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "', '"
                    + txtPrefixo.Text + "', '"
                    + cmbTrem.Text.ToUpper() + "', '"
                    + maquinista_entrada + "' ,"
                    + (sequencia + 2).ToString() + ", '"
                    + destino + "')";
                consulta(sql, "andamento");


                if (status_consulta == true)
                {
                    consulta("update viras set proxima_escala = '" + txtPrefixo.Text + "', hora_saida = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where maquinista = '" + maquinista_entrada + "' and hora_saida is null", "viras");
                    if (status_consulta == true)
                    {
                        if (maquinista_saida != "" && codigo_local_vira != 5)
                        {
                            odtAux = consulta("select Max(sequencia) as sequencia from viras where codigo_local_vira = " + codigo_local_vira + " and hora_saida is null", "viras");
                            if (status_consulta == true)
                                consulta("Insert into viras (codigo_local_vira, codigo_linha, hora_entrada, maquinista, sequencia) values "
                                    + "(" + codigo_local_vira + ", " + linha + ", '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + maquinista_saida + "', " + odtAux.Rows[0]["sequencia"].ToString() + " + 1)", "viras");
                        }
                    }
                }
                maquinista_saida = "";
                maquinista_entrada = "";
                cmbTrem.Text = "";
                pnl_acao.Visible = false;
            }

            catch (Exception e)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo em Adicionar!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II adicionar - " + e.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }

        }

        private void executaEditar()
        {
            try
            {
                Info info = (Info)sendoMovido_panel.Tag;
                string destino;
                if (lbl_destino_diferenciado.Checked == true)
                    destino = "SIM";
                else
                    destino = "NÃO";
                string sql = "Update Andamento Set Prefixo = '" + txtPrefixo.Text + "', Destino_diferente = '" + destino + "' where Linha = " + linha + " and sequencia = " + sequencia.ToString();

                consulta(sql, "andamento");

                sql = "Update Trens Set Mensagem = '" + mensagem + txt_obs.Text + "' where trem = '" + cmbTrem.Text + "'";
                if (status_consulta == true)
                    consulta(sql, "trens");

                if (status_consulta == true)
                    consulta("update viras set proxima_escala = '" + txtPrefixo.Text + "' where proxima_escala = '" + info.prefixo + "' and maquinista = '" + maquinista_entrada + "' and hora_saida > '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "'", "viras");
                maquinista_entrada = "";
                pnl_acao.Visible = false;
            }
            catch (Exception e)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo ao Editar!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II editar - " + e.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void executaEditarLocal()
        {
            try
            {
                string sql = "Update Trens Set Trens.Local = '" + cmb_local.SelectedValue + "', trens.observacao = '" + txt_obs.Text + "', Trens.mensagem = '" + mensagem + "', where Trem = '" + cmbTrem.Text.ToUpper() + "'";

                consulta(sql, "trens");

                pnl_acao.Visible = false;
            }
            catch (Exception e)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo ao Editar Local!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II editar - " + e.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
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
                //MessageBox.Show("Err CCT IIo ao Editar Mensagem!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II editar - " + e.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void executaRecolher(string final_viagem, string local, string mensagem_trens, string mensagem_historico)
        {
            try
            {
                string sql = "Delete From Andamento where linha = " + linha + " and ID = '" + cmbTrem.Text.ToUpper() + "'";

                consulta(sql, "andamento");
                if (status_consulta == true)
                {
                    if (groupBox1.Text == "Recolher")
                        sql = "Update Trens Set Trens.Local = '" + local + "', trens.observacao = '" + txt_obs.Text + "', Trens.mensagem = '" + mensagem_trens + "' where Trem = '" + cmbTrem.Text.ToUpper() + "'";
                    else
                        sql = "Update Trens Set trens.observacao = '', Trens.mensagem = '" + mensagem_trens + txt_obs.Text + "' where Trem = '" + cmbTrem.Text.ToUpper() + "'";

                    consulta(sql, "Trens");
                    if (status_consulta == true)
                    {
                        sql = "INSERT INTO historico (Linha, Data, Prefixo, Trem, Maquinista, Sequencia, Final_viagem, Mensagem) Values ('"
                            + linha + "', '"
                            + data_viagem.ToString("yyyy-MM-dd HH:mm:ss") + "', '"
                            + txtPrefixo.Text + "', '"
                            + cmbTrem.Text.ToUpper() + "', '"
                            + maquinista_saida + "' ,"
                            + sequencia.ToString() + ", '"
                            + final_viagem + "', '"
                            + mensagem_historico + "')";

                        consulta(sql, "historico");
                        if (status_consulta == true)
                            if (maquinista_saida != "" && (groupBox1.Text == "Recolher" || codigo_local_vira == 5))
                            {
                                consulta("Insert into viras (codigo_local_vira, codigo_linha, hora_entrada, maquinista, sequencia) values "
                                    + "(5, " + linha + ", '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + maquinista_saida + "', 0)", "viras");
                                maquinista_saida = "";
                            }
                        pnl_acao.Visible = false;
                    }
                }
            }
            catch (Exception e)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo ao Recolher!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II recolher - " + e.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                 
                oCn = new MySqlConnection(mstring);
                maquinista_entrada = "";
                maquinista_saida = "";
                this.Width = Screen.PrimaryScreen.Bounds.Width;
                tabControl1.Width = this.Width - 15;
                panel1.Width = this.Width - 40;
                panel2.Width = this.Width - 40;
                Tamanho = panel1.Width / 13 - 10;
                panel1.Left = (this.Width - panel1.Width) / 2 - 5;
                
                dia = DateTime.Now;
                if (dia.Hour < 3)
                    dia = dia.AddDays(-1);

                
                cmbLinha.DisplayMember = "linha";
                cmbLinha.ValueMember = "codigo";
                cmbLinha.DataSource = consulta("Select * From linha", "linha");

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
                //MessageBox.Show("Err CCT IIo na conexão codigo 01!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II Load - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
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
            int sequencia_aux;
            string prefixo_aux;
            try
            {
                string msg_aux;
                msg_aux = mensagem;
                if (rdb_msg.Checked == false)
                    mensagem = "";

                if (groupBox1.Text == "Adicionar")
                {
                    if (txtPrefixo.Text != "" && int.Parse(txtPrefixo.Text) % 2 == sequencia % 2 && cmbTrem.Text != "")
                    {
                        odtAux = consulta("update Linha set atualizacao = (atualizacao + 1) where codigo = " + linha, "linha");
                        atualizacao = atualizacao + 1;
                        executaAdicionar(mensagem);
                    }
                    else
                        MessageBox.Show("Prefixo ou trem inválido!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                if (groupBox1.Text == "Recolher")
                {
                    ////////////////////////////////////////////
                    if (cmb_local.Text == "")
                    {
                        MessageBox.Show("Selecione um Local", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    ////////////////////////////////////////
                    executaRecolher(cmb_local.Text, cmb_local.SelectedValue.ToString(), mensagem, msg_aux);
                    atualizacao = atualizacao + 1;
                    odtAux = consulta("update Linha set atualizacao = (atualizacao + 1) where codigo = " + linha, "linha");

                }
                if (groupBox1.Text == "Virar")
                {
                    if (txtPrefixo.Text != "" && int.Parse(txtPrefixo.Text) % 2 == sequencia % 2)
                    {
                        sequencia_aux = sequencia;
                        prefixo_aux = txtPrefixo.Text;

                        Info info = (Info)sendoMovido.Tag;
                        sequencia = info.sequencia;
                        txtPrefixo.Text = info.prefixo;
                        executaRecolher(prefixo_aux, "", mensagem, msg_aux);
                        if (status_consulta == true)
                        {
                            sequencia = sequencia_aux;
                            txtPrefixo.Text = prefixo_aux;
                            executaAdicionar(mensagem);

                            odtAux = consulta("update Linha set atualizacao = (atualizacao + 1) where codigo = " + linha, "linha");
                            atualizacao = atualizacao + 1;
                        }
                    }
                    else
                        MessageBox.Show("Prefixo inválido", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                if (groupBox1.Text == "Editar")
                {
                    if (int.Parse(txtPrefixo.Text) % 2 == sequencia % 2)
                    {
                        executaEditar();
                        odtAux = consulta("update Linha set atualizacao = (atualizacao + 1) where codigo = " + linha, "linha");
                        atualizacao = atualizacao + 1;

                    }
                    else
                        MessageBox.Show("Prefixo inválido", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (groupBox1.Text == "Alterar Local")
                {
                    ////////////////////////////////////////////
                    if (cmb_local.Text == "")
                    {
                        MessageBox.Show("Selecione um Local", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    ////////////////////////////////////////
                    executaEditarLocal();
                    odtAux = consulta("update Linha set atualizacao = (atualizacao + 1) where codigo = " + linha, "linha");
                    atualizacao = atualizacao + 1;
                }

                if (groupBox1.Text == "Editar Mensagem")
                {
                    executaEditar();
                    odtAux = consulta("update Linha set atualizacao = (atualizacao + 1) where codigo = " + linha, "linha");
                    atualizacao = atualizacao + 1;
                }
                txt_obs.Text = "";
                lbl_destino_diferenciado.Checked = false;
                atualiza();
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo na conexão codigo 02!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II - Gravar" + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void adicionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Label aux = (Label)sender;

                Info info = (Info)aux.Tag;

                try
                {
                    txtPrefixo.Text = (int.Parse(info.prefixo) + 2).ToString("000");
                }
                catch
                {
                    txtPrefixo.Text = "???";
                }
                cmbTrem.DataSource = consulta("Select Trem From Trens where Linha = " + linha + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id) Order By Trem", "Trens");
                cmbTrem.DisplayMember = "Trem";

                sequencia = info.sequencia;
                maquinista_entrada = "";
                maquinista_saida = "";
                pnl_acao.Visible = true;
                groupBox1.Text = "Adicionar";
                lblLocal.Visible = false;
                cmb_local.Visible = false;
                lbl_obs.Text = "Obs.";
                lbl_obs.Visible = false;
                txt_obs.Visible = false;
                lbl_prefixo.Visible = true;
                txtPrefixo.Visible = true;
                txtPrefixo.Enabled = true;
                cmbTrem.Enabled = true;
                cmbTrem.Text = "";
                lbl_destino_diferenciado.Visible = true;
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo na conexão codigo 03!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II - Adicionar" + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void recolherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Info info = (Info)cmsMenu.SourceControl.Tag;
                sequencia = info.sequencia;


                //panel1.Controls[cmsMenu.SourceControl.Name.ToString()].BackColor = Color.Green;

                pnl_acao.Visible = true;
                groupBox1.Text = "Recolher";
                lblLocal.Visible = true;
                cmb_local.Visible = true;
                lbl_obs.Text = "Via/Pátio";
                lbl_obs.Visible = true;
                txt_obs.Visible = true;
                lbl_prefixo.Visible = true;
                txtPrefixo.Visible = true;
                txtPrefixo.Enabled = false;
                cmbTrem.Enabled = false;
                lbl_destino_diferenciado.Visible = false;
                txtPrefixo.Text = info.prefixo;
                cmbTrem.Text = info.trem;
                maquinista_saida = info.maquinista;
                data_viagem = info.data_viagem;

                cmb_local.DataSource = consulta("Select * From Locais", "Locais");
                cmb_local.DisplayMember = "Local";
                cmb_local.ValueMember = "codigo";
                cmb_local.Text = "";
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo ao recolher!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II recolher - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void alterarLocalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Label Trem = (Label)cms_recolhidos.SourceControl;

                cmbTrem.DataSource = consulta("Select Trem From Trens where Linha = " + linha + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id) Order By Trem", "Trens");
                cmbTrem.DisplayMember = "Trem";

                Trem.BackColor = Color.Green;
                pnl_acao.Visible = true;
                groupBox1.Text = "Alterar Local";
                lblLocal.Visible = true;
                cmb_local.Visible = true;
                lbl_obs.Text = "Via/Pátio";
                lbl_obs.Visible = true;
                txt_obs.Visible = true;
                lbl_prefixo.Visible = false;
                txtPrefixo.Visible = false;
                cmbTrem.Enabled = false;
                lbl_destino_diferenciado.Visible = false;
                cmbTrem.Text = Trem.Text;

                cmb_local.DataSource = consulta("Select * From Locais", "Locais");
                cmb_local.DisplayMember = "Local";
                cmb_local.ValueMember = "codigo";
                cmb_local.Text = "";

            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo ao Alterar Local!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II editar - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void editarMensagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Label Trem = (Label)cms_recolhidos.SourceControl;

                cmbTrem.DataSource = consulta("Select Trem From Trens where Linha = " + linha + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id) Order By Trem", "Trens");
                cmbTrem.DisplayMember = "Trem";

                Trem.BackColor = Color.Green;
                pnl_acao.Visible = true;
                groupBox1.Text = "Editar Mensagem";
                lblLocal.Visible = false;
                cmb_local.Visible = false;
                lbl_obs.Text = "Obs.";
                lbl_obs.Visible = false;
                txt_obs.Visible = false;
                lbl_prefixo.Visible = false;
                txtPrefixo.Visible = false;
                cmbTrem.Enabled = false;
                lbl_destino_diferenciado.Visible = false;
                cmbTrem.Text = Trem.Text;
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo ao Editar Mensagem!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II editar - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Info info = (Info)cmsMenu.SourceControl.Tag;
            if (MessageBox.Show("Este Prefixo não será armazenado no historico deseja continuar?", "Sistema de Controle de Viagens", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                consulta("Delete from andamento where Id = '" + info.trem + "'", "andamento");
                if (status_consulta == true)
                {
                    if (info.maquinista != "")
                        consulta("Insert into viras (codigo_local_vira, codigo_linha, hora_entrada, maquinista) values "
                            + "(5, " + linha + ", '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + info.maquinista + "')", "viras");

                    odtAux = consulta("update Linha set atualizacao = (atualizacao + 1) where codigo = " + linha, "linha");
                }
                atualizacao = atualizacao + 1;
            }
            atualiza();
        }

        private void adicionarPrefixoImparToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //sequencia = 1;
                odtPar = consulta("Select max(sequencia) From historico Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and (Sequencia mod 2) = 1", "historico");
                try
                {
                    sequencia = int.Parse(odtPar.Rows[0][0].ToString()) + 2;
                }
                catch
                {
                    sequencia = 1;
                }

                cmbTrem.DataSource = consulta("Select Trem From Trens where Linha = " + linha + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id) Order By Trem", "Trens");
                cmbTrem.DisplayMember = "Trem";


                txtPrefixo.Text = "001";
                maquinista_entrada = "";
                maquinista_saida = "";
                pnl_acao.Visible = true;
                groupBox1.Text = "Adicionar";
                lblLocal.Visible = false;
                cmb_local.Visible = false;
                lbl_obs.Text = "Obs.";
                lbl_obs.Visible = false;
                txt_obs.Visible = false;
                lbl_prefixo.Visible = true;
                txtPrefixo.Visible = true;
                txtPrefixo.Enabled = true;
                lbl_destino_diferenciado.Visible = true;

                cmbTrem.Enabled = true;
                cmbTrem.Text = "";
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo na conexão codigo 04!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II adicionar impar - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }


        private void adiiconarPrefixoParToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                odtPar = consulta("Select max(sequencia) From historico Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and (Sequencia mod 2) = 0", "historico");
                try
                {
                    sequencia = int.Parse(odtPar.Rows[0][0].ToString()) + 2;
                }
                catch
                {
                    sequencia = 0;
                }

                cmbTrem.DataSource = consulta("Select Trem From Trens where Linha = " + linha + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id) Order By Trem", "Trens");
                cmbTrem.DisplayMember = "Trem";

                txtPrefixo.Text = "002";

                maquinista_saida = "";
                maquinista_entrada = "";
                pnl_acao.Visible = true;
                groupBox1.Text = "Adicionar";
                lblLocal.Visible = false;
                cmb_local.Visible = false;
                lbl_obs.Text = "Obs.";
                lbl_obs.Visible = false;
                txt_obs.Visible = false;
                lbl_prefixo.Visible = true;
                txtPrefixo.Visible = true;
                txtPrefixo.Enabled = true;
                cmbTrem.Enabled = true;
                cmbTrem.Text = "";
                lbl_destino_diferenciado.Visible = true;

            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo na conexão codigo 05!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II adicionar par - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            pnl_acao.Visible = false;
            cmb_local.Text = "";

            cmbTrem.Text = "";
            txtPrefixo.Text = "";
            txt_obs.Text = "";
            atualiza();
        }

        private void Ok()
        {

            linha = cmbLinha.SelectedValue.ToString();
            
            pnl_acao.Visible = false;
            atualiza();
        }

        void prefixoImpar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                sendoMovido = (Label)sender;
                Info info = (Info)sendoMovido.Tag;
                cmbTrem.Text = info.trem;
                cmb_local.Text = "";
                maquinista_saida = info.maquinista;
                data_viagem = info.data_viagem;
                groupBox1.Text = "Virar";
                string maquinista = info.maquinista;

                odtPar = consulta("Select max(sequencia) From Andamento Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and (Sequencia mod 2) = 0", "Andamento");
                if (odtPar.Rows[0][0].ToString() == "")
                {
                    odtPar = consulta("Select max(sequencia) From historico Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and (Sequencia mod 2) = 0", "historico");
                    if (int.Parse(info.prefixo) >= 700)
                        odtImpar = consulta("Select max(Prefixo) From historico Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and prefixo >= 700 and (Sequencia mod 2) = 0", "historico");
                    else
                        odtImpar = consulta("Select max(Prefixo) From historico Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and prefixo < 700 and (Sequencia mod 2) = 0", "historico");

                    try
                    {
                        txtPrefixo.Text = (int.Parse(odtImpar.Rows[0][0].ToString()) + 2).ToString("000");
                    }
                    catch
                    {
                        txtPrefixo.Text = "002";
                    }

                    info = null;

                }
                else
                {
                    if (int.Parse(info.prefixo) >= 700)
                        odtImpar = consulta("Select max(Prefixo) From Andamento Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and prefixo >= 700 and (Sequencia mod 2) = 0", "Andamento");
                    else
                        odtImpar = consulta("Select max(Prefixo) From Andamento Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and prefixo < 700 and (Sequencia mod 2) = 0", "Andamento");


                    if (odtImpar.Rows[0][0].ToString() == "")
                        if (int.Parse(info.prefixo) >= 700)
                            odtImpar = consulta("Select max(Prefixo) From historico Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and prefixo >= 700 and (Sequencia mod 2) = 0", "historico");
                        else
                            odtImpar = consulta("Select max(Prefixo) From historico Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and prefixo < 700 and (Sequencia mod 2) = 0", "historico");

                    try
                    {
                        txtPrefixo.Text = (int.Parse(odtImpar.Rows[0][0].ToString()) + 2).ToString("000");
                    }
                    catch
                    {
                        txtPrefixo.Text = "002";
                    }
                }
                try
                {
                    sequencia = (int.Parse(odtPar.Rows[0][0].ToString()));
                }
                catch
                {
                    sequencia = 0;
                }

                pega_maquinista_vira(1, maquinista);

                sendoMovido.Parent.BackColor = Color.Green;
                pnl_acao.Visible = true;
                lblLocal.Visible = false;
                cmb_local.Visible = false;
                lbl_obs.Text = "Obs.";
                lbl_obs.Visible = false;
                txt_obs.Visible = false;
                lbl_prefixo.Visible = true;
                txtPrefixo.Visible = true;
                txtPrefixo.Enabled = true;
                cmbTrem.Enabled = false;
                lbl_destino_diferenciado.Visible = true;
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo na conexão codigo 06!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II prefixo impar Dbclik - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }

        }

        void prefixoPar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                sendoMovido = (Label)sender;
                Info info = (Info)sendoMovido.Tag;
                cmbTrem.Text = info.trem;
                cmb_local.Text = "";
                maquinista_saida = info.maquinista;
                groupBox1.Text = "Virar";
                data_viagem = info.data_viagem;
                string maquinista = info.maquinista;

                odtPar = consulta("Select max(sequencia) From Andamento Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and (Sequencia mod 2) = 1", "Andamento");
                if (odtPar.Rows[0][0].ToString() == "")
                {
                    odtPar = consulta("Select max(sequencia) From historico Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and (Sequencia mod 2) = 1", "historico");
                    if (int.Parse(info.prefixo) >= 700)
                        odtImpar = consulta("Select max(Prefixo) From historico Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and prefixo >= 700 and (Sequencia mod 2) = 1", "historico");
                    else
                        odtImpar = consulta("Select max(Prefixo) From historico Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and prefixo < 700 and (Sequencia mod 2) = 1", "historico");

                    try
                    {
                        txtPrefixo.Text = (int.Parse(odtImpar.Rows[0][0].ToString()) + 2).ToString("000");
                    }
                    catch
                    {
                        txtPrefixo.Text = "001";
                    }

                    info = null;

                }
                else
                {
                    if (int.Parse(info.prefixo) >= 700)
                        odtImpar = consulta("Select max(Prefixo) From Andamento Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and prefixo >= 700 and (Sequencia mod 2) = 1", "Andamento");
                    else
                        odtImpar = consulta("Select max(Prefixo) From Andamento Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and prefixo < 700 and (Sequencia mod 2) = 1", "Andamento");

                    if (odtImpar.Rows[0][0].ToString() == "")

                        if (int.Parse(info.prefixo) >= 700)
                            odtImpar = consulta("Select max(Prefixo) From historico Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and prefixo >= 700 and (Sequencia mod 2) = 1", "historico");
                        else
                            odtImpar = consulta("Select max(Prefixo) From historico Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and prefixo < 700 and (Sequencia mod 2) = 1", "historico");

                    try
                    {
                        txtPrefixo.Text = (int.Parse(odtImpar.Rows[0][0].ToString()) + 2).ToString("000");
                    }
                    catch
                    {
                        txtPrefixo.Text = "001";
                    }
                }
                try
                {
                    sequencia = (int.Parse(odtPar.Rows[0][0].ToString()));
                }
                catch
                {
                    sequencia = 1;
                }

                pega_maquinista_vira(0, maquinista);


                sendoMovido.Parent.BackColor = Color.Green;
                pnl_acao.Visible = true;
                lblLocal.Visible = false;
                cmb_local.Visible = false;
                lbl_obs.Text = "Obs.";
                lbl_obs.Visible = false;
                txt_obs.Visible = false;
                lbl_prefixo.Visible = true;
                txtPrefixo.Visible = true;
                txtPrefixo.Enabled = true;
                lbl_destino_diferenciado.Visible = true;
                cmbTrem.Enabled = false;
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo na conexão codigo 07!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II prefixo par dbclik - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        void editar()
        {
            try
            {
                Panel prefixo = (Panel)cmsMenu.SourceControl;
                sendoMovido_panel = (Panel)cmsMenu.SourceControl;
                Info info = (Info)prefixo.Tag;
                sequencia = info.sequencia;
                txtPrefixo.Text = info.prefixo;
                maquinista_entrada = info.maquinista;
                cmbTrem.DataSource = consulta("Select Trem From Trens where Linha = " + linha + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id) Order By Trem", "Trens");
                cmbTrem.DisplayMember = "Trem";

                sendoMovido_panel.BackColor = Color.Green;
                pnl_acao.Visible = true;
                groupBox1.Text = "Editar";
                lblLocal.Visible = false;
                cmb_local.Visible = false;
                lbl_obs.Text = "Obs.";
                lbl_obs.Visible = false;
                txt_obs.Visible = false;
                lbl_prefixo.Visible = true;
                txtPrefixo.Visible = true;
                txtPrefixo.Enabled = true;
                cmbTrem.Enabled = false;
                cmbTrem.Text = info.trem;
                lbl_destino_diferenciado.Visible = true;

            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo ao editar!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II editar - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void prefixo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                sendoMovido = (Label)sender;
                sendoMovido.DoDragDrop(sendoMovido.Tag, DragDropEffects.Link);
            }

        }


        void pic_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                Label pic = (Label)sender;
                Info info1 = (Info)sendoMovido.Tag;
                Info info2 = (Info)pic.Tag;
                if ((info1.sequencia % 2) != (info2.sequencia % 2))
                {
                    pic.Image = lblPic.Image;
                    e.Effect = DragDropEffects.Link;
                }
                else
                    e.Effect = DragDropEffects.None;
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo na conexão codigo 08!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II drag enter - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        void pic_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Label pic = (Label)sender;

                if (sendoMovido.Parent.Name == "prefixo_impar" && pic.Name == "prefixo_par")
                {
                    prefixoImpar_MouseDoubleClick(sendoMovido, null);
                    return;
                }
                if (sendoMovido.Parent.Name == "prefixo_par" && pic.Name == "prefixo_impar")
                {
                    prefixoPar_MouseDoubleClick(sendoMovido, null);
                    return;
                }

                Info info = (Info)pic.Tag;
                Info sendo_movido_info = (Info)sendoMovido.Tag;

                if (int.Parse(sendo_movido_info.prefixo) >= 700)
                    odtImpar = consulta("Select max(Prefixo) From Andamento Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and prefixo >= 700 and (Sequencia mod 2) = " + (info.sequencia % 2), "Andamento");
                else
                    odtImpar = consulta("Select max(Prefixo) From Andamento Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and prefixo < 700 and (Sequencia mod 2) = " + (info.sequencia % 2), "Andamento");


                if (odtImpar.Rows[0][0].ToString() == "")
                    if (int.Parse(sendo_movido_info.prefixo) >= 700)
                        odtImpar = consulta("Select max(Prefixo) From historico Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and prefixo >= 700 and (Sequencia mod 2) = " + (info.sequencia % 2), "historico");
                    else
                        odtImpar = consulta("Select max(Prefixo) From historico Where Linha = " + linha + " and Data = '" + dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and prefixo < 700 and (Sequencia mod 2) = " + (info.sequencia % 2), "historico");


                try
                {
                    txtPrefixo.Text = (int.Parse(odtImpar.Rows[0][0].ToString()) + 2).ToString("000");
                }
                catch
                {
                    if (info.sequencia % 2 == 0)
                        txtPrefixo.Text = "700";
                    else
                        txtPrefixo.Text = "701";
                }

                cmbTrem.Text = sendo_movido_info.trem;

                sequencia = info.sequencia;
                cmb_local.Text = "";
                maquinista_saida = sendo_movido_info.maquinista;
                data_viagem = sendo_movido_info.data_viagem;

                if (int.Parse(sendo_movido_info.prefixo) >= 700)
                    if (sendo_movido_info.sequencia % 2 == 0)
                        pega_maquinista_vira(2, sendo_movido_info.maquinista);
                    else
                        pega_maquinista_vira(3, sendo_movido_info.maquinista);
                else
                    codigo_local_vira = 5;

                sendoMovido.Parent.BackColor = Color.Green;
                groupBox1.Text = "Virar";
                pnl_acao.Visible = true;
                lblLocal.Visible = false;
                cmb_local.Visible = false;
                lbl_obs.Text = "Obs.";
                lbl_obs.Visible = false;
                txt_obs.Visible = false;
                lbl_prefixo.Visible = true;
                txtPrefixo.Visible = true;
                txtPrefixo.Enabled = true;
                cmbTrem.Enabled = false;

                lbl_destino_diferenciado.Visible = true;
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo na conexão codigo 09!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II  drag drop - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        void pic_DragLeave(object sender, EventArgs e)
        {
            Label pic = (Label)sender;
            pic.Image = null;
        }

        void pic_MouseLeave(object sender, EventArgs e)
        {
            Label pic = (Label)sender;
            pic.Image = null;
        }

        void pic_MouseEnter(object sender, EventArgs e)
        {
            Label pic = (Label)sender;
            pic.Image = lblPic.Image;
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

        private void txtPrefixo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || //Letras
                char.IsSymbol(e.KeyChar) || //Símbolos
                char.IsWhiteSpace(e.KeyChar) || //Espaço
                char.IsPunctuation(e.KeyChar)) //Pontuação
                e.Handled = true; //Não permitir
            if (e.KeyChar == 13)
                btnGravar.PerformClick();
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editar();
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

                odtAux = consulta("Select versao from versao","versao");
                if(odtAux.Rows.Count > 0)
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
                //MessageBox.Show("Err CCT IIo na conexão codigo 10!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err CCT II timer - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void cmbLinha_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void verifica_mensagem(object sender, EventArgs e)
        {
            try
            {
                odtAux = consulta("select mensagem from trens where trem = '" + cmbTrem.Text + "'", "trens");
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

        private void cmb_local_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbTrem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnGravar.PerformClick();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

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
