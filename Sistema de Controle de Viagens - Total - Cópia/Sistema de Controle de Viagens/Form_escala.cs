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
    public partial class Form_escala : Form
    {
        public Form_escala()
        {
            InitializeComponent();
        }
        //string mstring = @"Server=cp03487;Port=3306;Database=svc;Uid=root; Pwd='';";
        string mstring;
        MySqlConnection oCn; //MySQL
        DataTable odtImpar;
        DataTable odtPar;
        DataTable odtAux;

        Label sendo_movido;
        DateTime dia;
        string linha;
        string sSqlImpar;
        string trem, prefixo;
        string sSqlPar;
        int left, atualizacao;
        int Tamanho;
        int codigo_local_vira;
        string versao = "2";
        bool status_consulta = false;

        Maquinista maquinista = new Maquinista();

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
                //MessageBox.Show("Ocorreu um Err escalao no Banco de Dados!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (oCn.State == ConnectionState.Open)
                {

                    ////////////////////////////////////////////////////////////////////////////////////////////
                    oCn.Close();
                    oCn.Open();
                    ////////////////////////////////////////////////////////////////////////////////////////////
                    try
                    {
                        MessageBox.Show(e.Message);
                        MySqlDataAdapter oDA = new MySqlDataAdapter("Insert Into Logs (Log, data) Values ('Escala" + sql.Replace("'", "-") + " - " + e.Message.Replace("'", "-") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", oCn); ;
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

        private void carrega_vira(int sequencia_vira, int height)
        {
            odtAux = consulta("select codigo, local_vira, sequencia_local_vira from locais_vira where codigo_linha = " + linha + " and (Sequencia_local_vira mod 2) = " + sequencia_vira + " and tela = 'VIRA'", "Locais_vira");
            if (sequencia_vira == 0)
                left = 10;
            else
                left = panel3.Width - 35 * sequencia_vira + (-sequencia_vira * 200);
            DataTable odt_aux2;


            //for para colocar os locais de vira
            for (int i = 0; i < odtAux.Rows.Count; i++)
            {
                Panel local = cria_local_maquinista(odtAux, i, height);


                string sql = "Select nome, data_hora_entrada, matricula, sequencia, destacado, obs, Tv ";
                sql = sql + "from (viras ";
                sql = sql + "inner join maquinistas on maquinistas.matricula = viras.maquinista) ";
                sql = sql + "inner join maquinistas_registros on viras.maquinista = maquinistas_registros.matricula_maquinista ";
                sql = sql + "where viras.codigo_linha = " + linha + " ";
                sql = sql + "and viras.codigo_local_vira = " + odtAux.Rows[i]["codigo"].ToString() + " ";
                sql = sql + "and viras.hora_saida Is Null ";
                sql = sql + "and maquinistas_registros.Data_hora_saida Is Null order by viras.sequencia";
                odt_aux2 = consulta(sql, "Viras");


                //define sujestão de prefixo ao vira
                DataTable odt_aux3, odt_aux4;
                string prefixo_sujerido_externo;
                string prefixo_sujerido_interno;

                if (odtAux.Rows[i]["sequencia_local_vira"].ToString() == "0")
                {
                    odt_aux3 = consulta("select max(prefixo) as prefixo from andamento where linha = " + linha + " and prefixo < 700 and sequencia mod 2 = 1", "andamento");
                    try
                    { prefixo_sujerido_externo = odt_aux3.Rows[0]["prefixo"].ToString(); }
                    catch
                    { prefixo_sujerido_externo = null; }


                    odt_aux3 = consulta("select max(prefixo) as prefixo from andamento where linha = " + linha + " and prefixo >= 700 and sequencia mod 2 = 1", "andamento");
                    try
                    { prefixo_sujerido_interno = odt_aux3.Rows[0]["prefixo"].ToString(); }
                    catch
                    { prefixo_sujerido_interno = null; }

                    odt_aux3 = odtPar;
                    odt_aux4 = odtImpar;
                }
                else if (odtAux.Rows[i]["sequencia_local_vira"].ToString() == "1")
                {
                    odt_aux3 = consulta("select max(prefixo) as prefixo from andamento where linha = " + linha + " and prefixo < 700 and sequencia mod 2 = 0", "andamento");
                    try
                    { prefixo_sujerido_externo = odt_aux3.Rows[0]["prefixo"].ToString(); }
                    catch
                    { prefixo_sujerido_externo = null; }


                    odt_aux3 = consulta("select max(prefixo) as prefixo from andamento where linha = " + linha + " and prefixo >= 700 and sequencia mod 2 = 0", "andamento");
                    try
                    { prefixo_sujerido_interno = odt_aux3.Rows[0]["prefixo"].ToString(); }
                    catch
                    { prefixo_sujerido_interno = null; }

                    odt_aux3 = odtImpar;
                    odt_aux4 = odtPar;
                }
                else
                {
                    prefixo_sujerido_interno = null;
                    prefixo_sujerido_externo = null;
                    odt_aux3 = null;
                    odt_aux4 = null;
                }


                //dentro de cada local vira, os maquinistas
                for (int n = 0; n < odt_aux2.Rows.Count; n++)
                {
                    string trem_vira;

                    try
                    {
                        //se for o primeiro ou último vira
                        if (sequencia_vira == 0 || sequencia_vira == 1)
                        {
                            //pega o trem
                            trem_vira = odt_aux3.Rows[n]["id"].ToString() + " ";
                            if (int.Parse(odt_aux3.Rows[n]["prefixo"].ToString()) < 700)
                            {
                                //incrementa o prefixo e concatena com o trem
                                prefixo_sujerido_externo = (int.Parse(prefixo_sujerido_externo) + 2).ToString();
                                trem_vira = trem_vira + " " + (int.Parse(prefixo_sujerido_externo).ToString("000"));

                            }
                            else
                            {
                                prefixo_sujerido_interno = (int.Parse(prefixo_sujerido_interno) + 2).ToString();
                                trem_vira = trem_vira + (int.Parse(prefixo_sujerido_interno).ToString("000"));

                            }
                        }
                        else
                            trem_vira = "";
                    }
                    catch
                    {
                        trem_vira = "";
                    }
                    local.Controls.Add(cria_label_maquinista(odt_aux2, n, trem_vira));
                }
                if (sequencia_vira == 0)
                    left += (local.Width + 15);
                else
                    left += (local.Width + 15) * -1;

                //if (sequencia_vira % 2 == 0)
                //    odtPar = null;
                //else
                //    odtImpar = null;
                local.ContextMenuStrip.Items[2].Enabled = false;
                panel3.Controls.Add(local);
            }
        }

        private void carrega_outros(int height)
        {
            odtAux = consulta("select codigo, local_vira from locais_vira where codigo_linha = " + linha + " and tela = 'LOCAIS'", "Locais_vira");
            left = 10;
            DataTable odt_aux2;


            //for para colocar os locais de vira
            for (int i = 0; i < odtAux.Rows.Count; i++)
            {
                Panel local = cria_local_maquinista(odtAux, i, height);

                //odt_aux2 = consulta("Select trens.Trem, trens.observacao, locais.local, trens.mensagem from Trens inner Join Locais on trens.local = locais.codigo where Linha = " + cmbLinha.SelectedValue + " and Trens.local = " + odtAux.Rows[i][0].ToString() + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id and Trens.Linha = " + cmbLinha.SelectedValue + ")", "Trens");
                string sql = "Select nome, data_hora_entrada, matricula, sequencia, destacado, obs, Tv ";
                sql = sql + "from (viras ";
                sql = sql + "inner join maquinistas on maquinistas.matricula = viras.maquinista) ";
                sql = sql + "inner join maquinistas_registros on viras.maquinista = maquinistas_registros.matricula_maquinista ";
                sql = sql + "where viras.codigo_linha = " + linha + " ";
                sql = sql + "and viras.codigo_local_vira = " + odtAux.Rows[i]["codigo"].ToString() + " ";
                sql = sql + "and viras.hora_saida Is Null ";
                sql = sql + "and maquinistas_registros.Data_hora_saida Is Null order by viras.sequencia";
                odt_aux2 = consulta(sql, "Viras");

                //dentro de cada local vira, os maquinistas
                for (int n = 0; n < odt_aux2.Rows.Count; n++)
                {
                    local.Controls.Add(cria_label_maquinista(odt_aux2, n, ""));

                }
                left += local.Width + 20;


                panel4.Controls.Add(local);
            }
        }

        private void carrega_nao_definido(int height)
        {
            odtAux = consulta("select codigo, local_vira from locais_vira where tela = 'NÃO DEFINIDO'", "Locais_vira");
            left = panel3.Width / 2 - 100;
            DataTable odt_aux2;


            //for para colocar os locais de vira
            for (int i = 0; i < odtAux.Rows.Count; i++)
            {
                Panel local = cria_local_maquinista(odtAux, i, height);

                string sql = "Select distinct nome, data_hora_entrada, matricula, sequencia, destacado, obs, Tv ";
                sql = sql + "from (viras ";
                sql = sql + "inner join maquinistas on maquinistas.matricula = viras.maquinista) ";
                sql = sql + "inner join maquinistas_registros on viras.maquinista = maquinistas_registros.matricula_maquinista ";
                sql = sql + "where viras.codigo_linha = " + linha + " ";
                sql = sql + "and viras.codigo_local_vira = " + odtAux.Rows[i]["codigo"].ToString() + " ";
                sql = sql + "and viras.hora_saida Is Null ";
                sql = sql + "and maquinistas_registros.Data_hora_saida Is Null ";
                sql = sql + "union ";
                sql = sql + "Select nome, data_hora_entrada, matricula, data_hora_saida as sequencia, destacado, data_hora_saida as obs, data_hora_saida as Tv ";
                sql = sql + "from (maquinistas_registros ";
                sql = sql + "inner join maquinistas on maquinistas.matricula = maquinistas_registros.matricula_maquinista) ";
                sql = sql + "Where not exists (select * from andamento where andamento.maquinista = maquinistas_registros.matricula_maquinista) ";
                sql = sql + "and not exists (select * from viras where viras.maquinista = maquinistas_registros.matricula_maquinista and hora_saida is null) ";
                sql = sql + "and maquinistas_registros.Data_hora_saida is null";

                //sql = sql + "inner join viras on viras.maquinista = maquinistas_registros.matricula_maquinista ";
                //sql = sql + "where hora_saida is not null ";
                //sql = sql + "and not exists (select * from andamento where andamento.maquinista = maquinistas_registros.matricula_maquinista) ";
                //sql = sql + "and viras.codigo = (select max(viras.codigo) from viras where maquinista = matricula_maquinista) ";
                //sql = sql + "and maquinistas_registros.Data_hora_saida is null";
                
                
                odt_aux2 = consulta(sql, "Viras");

                //dentro de cada local vira, os maquinistas
                for (int n = 0; n < odt_aux2.Rows.Count; n++)
                {
                    string sub_sql = "Select prefixo, trem ";
                    sub_sql = sub_sql + "from historico ";
                    sub_sql = sub_sql + "where linha = " + linha + " ";
                    sub_sql = sub_sql + "and codigo = (select max(codigo) from historico where maquinista = '" + odt_aux2.Rows[n]["matricula"].ToString() + "')";
                    DataTable odt_aux3;
                    odt_aux3 = consulta(sub_sql, "historico");
                    string pe;
                    try
                    {
                        pe = odt_aux3.Rows[0]["trem"].ToString() + " " + int.Parse(odt_aux3.Rows[0]["prefixo"].ToString()).ToString("000");
                    }
                    catch
                    {
                        pe = "";
                    }
                    local.Controls.Add(cria_label_maquinista(odt_aux2, n, pe));

                }
                left += local.Width + 20;

                local.AllowDrop = false;
                local.ContextMenuStrip = null;
                panel3.Controls.Add(local);
            }
        }

        private Label cria_label_maquinista(DataTable odt, int n, string proxima_escala)
        {
            Label texto = new Label();

            texto.Name = odt.Rows[n]["nome"].ToString();
            texto.Font = new Font("Arial Black", 8);
            texto.TextAlign = ContentAlignment.MiddleLeft;
            texto.Width = panel4.Width / 6 - 20;
            texto.Height = 50;
            texto.Top = n * 50 + 30;
            texto.BackColor = Color.Transparent;

            if (odt.Rows[n]["destacado"].ToString() == "SIM")
                texto.ForeColor = Color.FromArgb(70, 200, 255);
            else
                texto.ForeColor = Color.White;


            texto.AllowDrop = true;
            Info info = new Info();
            try
            { info.sequencia = int.Parse(odt.Rows[n]["sequencia"].ToString()); }
            catch { info.sequencia = 0; }
            texto.ContextMenuStrip = cms_remove_maquinista;
            info.maquinista = odt.Rows[n]["matricula"].ToString();
            texto.Tag = info;

            if (odt.Rows[n]["obs"].ToString() != "")
            {
                ToolTip yourToolTip = new ToolTip();
                //The below are optional, of course,
                yourToolTip.IsBalloon = true;
                yourToolTip.ShowAlways = true;
                yourToolTip.SetToolTip(texto, odt.Rows[n]["obs"].ToString());

            }

            texto.MouseMove += new MouseEventHandler(texto_MouseMove);
            texto.DragEnter += texto_DragEnter;
            texto.DragDrop += texto_DragDrop;
            texto.DragLeave += texto_DragLeave;

            texto.Text = proxima_escala + " " + maquinista_com_tempo(odt, n) + " " + odt.Rows[n]["Tv"].ToString();

            return texto;
        }

        private string maquinista_com_tempo(DataTable odt, int n)
        {
            int h = int.Parse(odt.Rows[n]["data_hora_entrada"].ToString().Substring(11, 2));
            int m = int.Parse(odt.Rows[n]["data_hora_entrada"].ToString().Substring(14, 2));
            int s = int.Parse(odt.Rows[n]["data_hora_entrada"].ToString().Substring(17, 2));
            TimeSpan t_maquinista = new TimeSpan(h, m, s);
            //TimeSpan t_maquinista = new TimeSpan(21, 00, 00);
            TimeSpan t_agora = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
            //TimeSpan t_agora = new TimeSpan(06, 0, 0);
            TimeSpan t_restante = new TimeSpan();
            TimeSpan t_servico;
            if (t_maquinista >= new TimeSpan(21, 00, 00) || t_maquinista <= new TimeSpan(04, 59, 00))
                t_servico = new TimeSpan(07, 00, 00);
            else
                t_servico = new TimeSpan(08, 00, 00);

            t_restante = t_maquinista.Add(t_servico);
            if (t_restante >= new TimeSpan(1, 00, 00, 00) && t_maquinista > t_agora)
                t_restante = t_restante.Subtract(new TimeSpan(1, 00, 00, 00));
            t_restante = t_restante.Subtract(t_agora);
            
            if (t_restante > new TimeSpan(23, 59, 59))
                t_restante = t_restante.Subtract(new TimeSpan(1, 00, 00, 00));

            if (t_restante < new TimeSpan(00, 00, 00))
                return odt.Rows[n]["nome"].ToString() + " " + t_restante.ToString().Substring(0, 6);
            else
                return odt.Rows[n]["nome"].ToString() + " " + t_restante.ToString().Substring(0, 5);
        }

        private Panel cria_local_maquinista(DataTable odt, int i, int height)
        {
            Panel local = new Panel();
            local.Name = odt.Rows[i]["codigo"].ToString();
            local.Width = panel4.Width / 6;
            local.Height = panel4.Height - 20;
            local.Top = 0;
            local.Left = left;
            local.BorderStyle = BorderStyle.Fixed3D;
            local.AutoScroll = true;
            local.ContextMenuStrip = cms_maquinista;
            local.DragDrop += new DragEventHandler(Local_DragDrop);
            local.DragEnter += new DragEventHandler(Local_DragEnter);
            local.AllowDrop = true;

            Label texto1 = new Label();

            texto1.Name = odt.Rows[i]["local_vira"].ToString();
            texto1.Font = new Font("Arial Black", 12);
            texto1.TextAlign = ContentAlignment.MiddleCenter;
            texto1.Width = local.Width - 20;
            texto1.Height = height;
            texto1.Top = 0;
            texto1.ForeColor = Color.Red;
            texto1.Text = odt.Rows[i]["local_vira"].ToString();
            texto1.BackColor = Color.Transparent;

            local.Controls.Add(texto1);

            return local;
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

            if (odt.Rows[i]["Destino_Diferente"].ToString() == "SIM")
                texto.BackColor = Color.FromArgb(70, 200, 255);
            else
                texto.BackColor = Color.Transparent;

            texto.TextAlign = ContentAlignment.MiddleCenter;
            texto.Width = prefixo.Width;
            texto.Height = prefixo.Height / 3;
            texto.ForeColor = Color.White;
            texto.Text = info.prefixo;
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
            texto3.Font = new Font("Arial Black", 08);
            texto3.TextAlign = ContentAlignment.TopCenter;
            texto3.Width = prefixo.Width;
            texto3.Height = prefixo.Height / 3;
            if (odt.Rows[i]["destacado"].ToString() == "SIM")
                texto3.ForeColor = Color.FromArgb(70, 200, 255);
            else
                texto3.ForeColor = Color.Yellow;
            texto3.Text = odt.Rows[i]["Nome"].ToString();
            texto3.Top = prefixo.Height / 3;
            texto3.BackColor = Color.Transparent;
            texto3.Tag = info;
            texto3.AllowDrop = true;
            texto3.MouseMove += new MouseEventHandler(texto3_MouseMove);
            texto3.DragEnter += new DragEventHandler(texto3_DragEnter);
            texto3.DragDrop += new DragEventHandler(texto3_DragDrop);

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
            sSqlImpar = "SELECT * from ((andamento Left Join Trens on andamento.id = trens.trem) left join maquinistas on maquinistas.matricula = andamento.maquinista) left join maquinistas_registros on maquinistas_registros.matricula_maquinista = andamento.maquinista Where andamento.Linha = " + linha + " and (codigo = (select max(codigo) from maquinistas_registros where maquinistas_registros.matricula_maquinista = andamento.maquinista) or codigo is null) and (Sequencia mod 2) = 1 order by sequencia";
            sSqlPar = "SELECT * from ((andamento Left Join Trens on andamento.id = trens.trem) left join maquinistas on maquinistas.matricula = andamento.maquinista) left join maquinistas_registros on andamento.maquinista = maquinistas_registros.matricula_maquinista Where andamento.Linha = " + linha + " and (codigo = (select max(codigo) from maquinistas_registros where maquinistas_registros.matricula_maquinista = andamento.maquinista) or codigo is null) and (Sequencia mod 2) = 0 order by sequencia";
            try
            {
                System.Threading.Thread.Sleep(50);
                odtImpar = consulta(sSqlImpar, "andamento");
                odtPar = consulta(sSqlPar, "andamento");
                lbl_circulacao.Text = "Circulando: " + (odtImpar.Rows.Count + odtPar.Rows.Count).ToString();
                left = 30;
                for (int i = panel1.Controls.Count - 1; i >= 0; i--)
                    panel1.Controls[i].Dispose();

                for (int i = panel2.Controls.Count - 1; i >= 0; i--)
                    panel2.Controls[i].Dispose();

                for (int i = panel3.Controls.Count - 1; i >= 0; i--)
                    panel3.Controls[i].Dispose();

                for (int i = panel4.Controls.Count - 1; i >= 0; i--)
                    panel4.Controls[i].Dispose();

                panel1.Controls.Clear();
                panel2.Controls.Clear();
                panel3.Controls.Clear();
                panel4.Controls.Clear();

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


                //preenche aba recolhidos
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
                        local.Name = odt_aux2.Rows[n]["Trem"].ToString();
                        ToolTip yourToolTip = new ToolTip();
                        //The below are optional, of course,
                        yourToolTip.IsBalloon = true;
                        yourToolTip.ShowAlways = true;


                        Label texto = new Label();

                        texto.Name = odt_aux2.Rows[n]["Trem"].ToString();
                        texto.Font = new Font("Arial Black", 14);
                        texto.TextAlign = ContentAlignment.MiddleCenter;
                        texto.Width = 90;
                        //texto.Height = local.Height / 20 + 10;
                        texto.Top = n * 40 + 30;
                        texto.ForeColor = Color.White;
                        texto.Text = odt_aux2.Rows[n]["Trem"].ToString();
                        texto.BackColor = Color.Transparent;

                        if (odt_aux2.Rows[n]["mensagem"].ToString() != "")
                        {
                            yourToolTip.SetToolTip(texto, odt_aux2.Rows[n]["observacao"].ToString() + " " + odt_aux2.Rows[n]["mensagem"].ToString());
                            texto.BackColor = Color.AntiqueWhite;
                            texto.ForeColor = Color.Black;
                        }
                        else
                            yourToolTip.SetToolTip(texto, odt_aux2.Rows[n]["observacao"].ToString());

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

                carrega_vira(1, prefixo.Height / 5 + 10);
                carrega_vira(0, prefixo.Height / 5 + 10);
                carrega_nao_definido(prefixo.Height / 5 + 10);
                carrega_outros(prefixo.Height / 5 + 10);
                odtImpar = null;
                odtPar = null;

            }
            catch (Exception e)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err escalao na atualização!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err escala Atualização - " + e.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
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
                this.Height = Screen.PrimaryScreen.Bounds.Height;
                tabControl1.Width = this.Width - 15;
                tabControl1.Height = this.Height - 110;
                panel1.Width = tabControl1.Width - 10;
                panel1.Height = tabControl1.Height / 2 - 10;
                panel2.Width = tabControl1.Width - 10;
                panel3.Top = panel1.Top + panel1.Height;
                panel3.Width = tabControl1.Width - 10;
                panel3.Height = tabControl1.Height / 2 - 10;
                panel4.Top = 0;
                panel4.Width = tabControl1.Width - 10;
                panel4.Height = tabControl1.Height / 2 - 10;
                dataGridView1.Width = this.Width - 40;




                Tamanho = panel1.Width / 13 - 10;
                //panel1.Left = (this.Width - panel1.Width) / 2 - 5;

                dia = DateTime.Now;
                if (dia.Hour < 3)
                    dia = dia.AddDays(-1);

                
                cmbLinha.DisplayMember = "Linha";
                cmbLinha.ValueMember = "codigo";
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
                //MessageBox.Show("Err escalao na conexão codigo 01!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err escala Load - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;

            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            oCn.Close();
            oCn.Dispose();
            Environment.Exit(0);
        }


        private void btnGravar_Click(object sender, EventArgs e)
        {
            timer1_Tick(null, null);
            if (status_consulta == false)
                return;
            try
            {

                if (groupBox1.Text == "Criar Local")
                {
                    if (txt_obs.Text == "")
                        MessageBox.Show("Digite um local!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        consulta("insert into locais_vira (codigo_linha, local_vira, Tela) values (" + linha + ", '" + txt_obs.Text.ToUpper() + "', 'LOCAIS')", "locais_vira");
                        atualiza();
                    }
                }
                if (groupBox1.Text == "Editar Local")
                {
                    if (txt_obs.Text == "")
                        MessageBox.Show("Digite um local!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        consulta("update locais_vira set local_vira = '" + txt_obs.Text.ToUpper() + "' where codigo = " + codigo_local_vira, "locais_vira");
                        atualiza();
                    }
                }
                if (groupBox1.Text == "Cadastrar Maquinista")
                {
                    if (cmb_maquinista.Text.Trim() == "")
                    {
                        MessageBox.Show("Digite um maquinista!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (txt_data_hora_entrada.Text.Trim() == "" || txt_data_hora_entrada.Text.Length < 9)
                    {
                        MessageBox.Show("Digite uma matricula válida!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    odtAux = consulta("select * from maquinistas where matricula = '" + txt_data_hora_entrada.Text + "'", "maquinistas");
                    if (odtAux.Rows.Count > 0)
                    {
                        MessageBox.Show("Esta matricula já está cadastrada!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    consulta("insert into maquinistas (matricula, nome) values ('" + txt_data_hora_entrada.Text + "', '" + cmb_maquinista.Text.ToUpper() + "')", "maquinistas");
                    //MessageBox.Show("Maquinista incluído com sucesso!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


                if (groupBox1.Text == "Inserir Maquinista")
                {

                    if (cmb_maquinista.SelectedValue == null)
                    {
                        if (MessageBox.Show("Maquinista " + cmb_maquinista.Text + " não está cadastrado, deseja cadastrá-lo?", "Sistema de Controle de Viagens", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            groupBox1.Text = "Cadastrar Maquinista";
                            checkBox1.Visible = false;
                            lbl_obs.Visible = false;
                            txt_obs.Visible = false;
                            chkTv.Visible = false;
                            lbl_hora.Text = "Informe a matricula";
                            txt_data_hora_entrada.Mask = "00.000.000-C";
                            txt_data_hora_entrada.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                        }

                        maquinista = null;
                        maquinista = new Maquinista();
                        return;
                    }

                    DateTime temp;
                    if (!DateTime.TryParse(txt_data_hora_entrada.Text, out temp))
                    {
                        MessageBox.Show("Digite uma data e hora válida!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }


                    maquinista.matricula = cmb_maquinista.SelectedValue.ToString();
                    maquinista.data_hora_entrada = txt_data_hora_entrada.Text;
                    maquinista.hora_entrada_vira = txt_data_hora_entrada.Text;

                    odtAux = consulta("select * from maquinistas_registros where matricula_maquinista = '" + maquinista.matricula + "' and data_hora_saida is null", "maquinistas_registros");
                    if (odtAux.Rows.Count > 0)
                    {
                        MessageBox.Show("Este maquinista ainda está com um período aberto. EncErr escalae o período para continuar!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        maquinista = null;
                        maquinista = new Maquinista();
                        return;
                    }


                    string destacado = "NÃO";
                    string Tv = "";
                    if (checkBox1.Checked == true)
                        destacado = "SIM";
                    if (chkTv.Checked == true)
                        Tv = "Tv";

                    odtAux = consulta("select Max(sequencia) as sequencia from viras where codigo_local_vira = " + codigo_local_vira + " and hora_saida is null", "viras");
                    consulta("Insert into viras (codigo_local_vira, codigo_linha, hora_entrada, maquinista, sequencia, obs, Tv) values "
                        + "(" + codigo_local_vira + ", " + linha + ", '" + Convert.ToDateTime(maquinista.hora_entrada_vira).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + maquinista.matricula + "', " + odtAux.Rows[0]["sequencia"].ToString() + " + 1, '" + txt_obs.Text + "', '" + Tv + "')", "viras");

                    if (status_consulta == true)
                    {
                        consulta("Insert into maquinistas_registros (matricula_maquinista, data_hora_entrada, destacado) values ('" + maquinista.matricula + "', '" + Convert.ToDateTime(maquinista.data_hora_entrada).ToString("yyyy-MM-dd HH:mm:ss") + ":00', '" + destacado + "')", "maquinistas_registros");

                        if (status_consulta == true)
                            consulta("update Linha set atualizacao = (atualizacao + 1) where codigo = " + linha, "linha");
                    }
                    atualizacao = atualizacao + 1;
                }

                if (groupBox1.Text == "Remover Maquinista")
                {

                    DateTime temp;
                    if (!DateTime.TryParse(txt_data_hora_entrada.Text, out temp))
                    {
                        MessageBox.Show("Digite uma data e hora válida!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }


                    maquinista.data_hora_entrada = txt_data_hora_entrada.Text;
                    maquinista.hora_entrada_vira = txt_data_hora_entrada.Text;

                    string destacado = "NÃO";
                    if (checkBox1.Checked == true)
                        destacado = "SIM";
                    consulta("update maquinistas_registros set data_hora_saida = '" + Convert.ToDateTime(maquinista.data_hora_entrada).ToString("yyyy-MM-dd HH:mm:ss") + "', destacado = '" + destacado + "' where matricula_maquinista = '" + maquinista.matricula + "' and data_hora_saida is null", "maquinistas_registros");
                    if (status_consulta == true)
                        consulta("update viras set hora_saida = '" + Convert.ToDateTime(maquinista.hora_entrada_vira).ToString("yyyy-MM-dd HH:mm:ss") + "' where maquinista = '" + maquinista.matricula + "' and hora_saida is null", "viras");

                    odtAux = consulta("update Linha set atualizacao = (atualizacao + 1) where codigo = " + linha, "linha");
                    atualizacao = atualizacao + 1;
                }

                if (groupBox1.Text == "Editar Maquinista")
                {

                    DateTime temp;
                    if (!DateTime.TryParse(txt_data_hora_entrada.Text, out temp))
                    {
                        MessageBox.Show("Digite uma data e hora válida!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }


                    maquinista.data_hora_entrada = txt_data_hora_entrada.Text;
                    maquinista.hora_entrada_vira = txt_data_hora_entrada.Text;

                    string destacado = "NÃO";
                    string Tv = "";
                    if (checkBox1.Checked == true)
                        destacado = "SIM";

                    if (chkTv.Checked == true)
                        Tv = "Tv";

                    consulta("update maquinistas_registros set data_hora_entrada = '" + Convert.ToDateTime(maquinista.data_hora_entrada).ToString("yyyy-MM-dd HH:mm:ss") + "', destacado = '" + destacado + "' where matricula_maquinista = '" + maquinista.matricula + "' and data_hora_saida is null", "maquinistas_registros");

                    if (status_consulta == true)
                        consulta("update viras set hora_entrada = '" + Convert.ToDateTime(maquinista.hora_entrada_vira).ToString("yyyy-MM-dd HH:mm:ss") + "', obs = '" + txt_obs.Text + "', Tv = '" + Tv + "' where maquinista = '" + maquinista.matricula + "' and hora_saida is null", "viras");

                    odtAux = consulta("update Linha set atualizacao = (atualizacao + 1) where codigo = " + linha, "linha");
                    atualizacao = atualizacao + 1;
                }
                groupBox1.Text = "Inserir Maquinista";
                lbl_hora.Text = "Entrada";
                txt_data_hora_entrada.Mask = "00/00/0000 90:00";
                txt_data_hora_entrada.TextMaskFormat = MaskFormat.IncludeLiterals;
                cmb_maquinista.Text = "";
                checkBox1.Checked = false;
                chkTv.Checked = false;
                lbl_obs.Text = "";
                cmb_maquinista.Enabled = true;
                txt_data_hora_entrada.Clear();
                chkTv.Visible = true;
                atualiza();
                pnl_acao.Visible = false;
                maquinista = null;
                maquinista = new Maquinista();
                atualiza();
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err escalao na conexão codigo 02!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err escala - Gravar" + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void prefixo_MouseDoubleClick(object sender, EventArgs e)
        {
            try
            {
                Label Trem = (Label)sender;
                Info info = (Info)Trem.Tag;

                Trem.Parent.BackColor = Color.Green;
                pnl_acao.Visible = true;
                groupBox1.Text = "Editar Maquinista";
                lbl_nome.Text = "Maquinista";
                lbl_nome.Visible = true;
                cmb_maquinista.Visible = true;
                prefixo = info.prefixo;
                trem = info.trem;
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err escalao ao Editar Mensagem!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err escala editar - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            pnl_acao.Visible = false;
            cmb_maquinista.Enabled = true;
            chkTv.Visible = true;
            checkBox1.Checked = false;
            chkTv.Checked = false;
            lbl_obs.Text = "";
            trem = "";
            prefixo = "";
            groupBox1.Text = "Inserir Maquinista";
            lbl_hora.Text = "Entrada";
            txt_data_hora_entrada.Mask = "00/00/0000 90:00";
            txt_data_hora_entrada.TextMaskFormat = MaskFormat.IncludeLiterals;
            atualiza();
        }

        private void texto_MouseMove(object sender, MouseEventArgs e)
        {
            Label l = (Label)sender;
            l.Parent.Focus();
            if (e.Button == MouseButtons.Left)
            {

                sendo_movido = (Label)sender;
                Info info = (Info)sendo_movido.Tag;
                if (info.maquinista != "")
                    sendo_movido.DoDragDrop(sendo_movido.Tag, DragDropEffects.Link);
            }

        }

        private void texto3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                sendo_movido = (Label)sender;
                Info info = (Info)sendo_movido.Tag;
                if (info.maquinista != "")
                    sendo_movido.DoDragDrop(sendo_movido.Tag, DragDropEffects.Link);
            }

        }

        void texto3_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (sendo_movido != sender)
                    e.Effect = DragDropEffects.Link;
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err escalao na conexão codigo 08!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err escala drag enter - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        void texto3_DragDrop(object sender, DragEventArgs e)
        {
            timer1_Tick(null, null);
            if (status_consulta == false)
            {
                MessageBox.Show("Uma atualização foi feita por outro usuário, favor, repita a operação", "Sistema de Controle de Viagens");
                return;
            }
            Label trem = (Label)sender;
            Info info = (Info)trem.Tag;
            Info info_sendo_movido = (Info)sendo_movido.Tag;
            if (info.maquinista != "")
            {
                MessageBox.Show("Não é possível inserir dois maquinistas no mesmo trem!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Info matricula = (Info)sendo_movido.Tag;

            maquinista = null;
            maquinista = new Maquinista();
            maquinista.matricula = matricula.maquinista;

            if (info_sendo_movido.trem == "")
                consulta("update viras set Hora_saida = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString() + "', proxima_escala = '" + info.prefixo + "' where maquinista = '" + maquinista.matricula + "' and hora_saida is null", "viras");
            else
                consulta("update andamento set Maquinista = '' where id = '" + matricula.trem + "'", "viras");

            if (status_consulta == true)
                consulta("update andamento set maquinista = '" + maquinista.matricula + "' where id = '" + info.trem + "' ", "andamento");
            odtAux = consulta("update Linha set atualizacao = (atualizacao + 1) where codigo = " + linha, "linha");
            atualizacao = atualizacao + 1;
            atualiza();
        }

        void texto_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                Label mqt = (Label)sender;

                if (sendo_movido.Parent == mqt.Parent && sendo_movido != sender)
                {
                    mqt.BackColor = Color.Green;
                    e.Effect = DragDropEffects.Link;
                }
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err escalao na conexão codigo 08!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err escala drag enter - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        void texto_DragLeave(object sender, EventArgs e)
        {
            try
            {
                Label mqt = (Label)sender;
                mqt.BackColor = Color.Transparent;

            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err escalao na conexão codigo 08!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err escala drag enter - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        void texto_DragDrop(object sender, DragEventArgs e)
        {
            timer1_Tick(null, null);
            if (status_consulta == false)
            {
                MessageBox.Show("Uma atualização foi feita por outro usuário, favor, repita a operação", "Sistema de Controle de Viagens");
                return;
            }
                Label mqt = (Label)sender;
            Info info_sendo_movido = (Info)sendo_movido.Tag;
            if (sendo_movido.Parent == mqt.Parent && sendo_movido != sender)
            {
                Info info = (Info)mqt.Tag;
                consulta("update viras set sequencia = sequencia + 1 where sequencia >= " + info.sequencia + " and codigo_local_vira = " + mqt.Parent.Name + " and hora_saida is null", "viras");
                if (status_consulta == true)
                    consulta("update viras set sequencia = " + info.sequencia + " where maquinista = '" + info_sendo_movido.maquinista + "' and hora_saida is null", "viras");
                odtAux = consulta("update Linha set atualizacao = (atualizacao + 1) where codigo = " + linha, "linha");
                atualizacao = atualizacao + 1;
                atualiza();
            }

        }

        void Local_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (sendo_movido.Parent != sender)
                    e.Effect = DragDropEffects.Link;
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err escalao na conexão codigo 08!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err escala drag enter - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
            }
        }

        void Local_DragDrop(object sender, DragEventArgs e)
        {
            timer1_Tick(null, null);
            if (status_consulta == false)
            {
                MessageBox.Show("Uma atualização foi feita por outro usuário, favor, repita a operação", "Sistema de Controle de Viagens");
                return;
            }
            Panel local = (Panel)sender;
            Label matricula = (Label)sendo_movido;
            Info info = (Info)matricula.Tag;

            maquinista = null;
            maquinista = new Maquinista();
            maquinista.matricula = info.maquinista;
            maquinista.hora_entrada_vira = DateTime.Now.ToString();
            if (info.trem == "")
                consulta("update viras set hora_saida = '" + Convert.ToDateTime(maquinista.hora_entrada_vira).ToString("yyyy-MM-dd HH:mm:ss") + "', proxima_escala = '" + local.Controls[0].Name + "' where maquinista = '" + maquinista.matricula + "' and hora_saida is null", "viras");
            else
                consulta("update andamento set maquinista = '' where id = '" + info.trem + "' ", "andamento");
            if (status_consulta == true)
            {
                odtAux = consulta("select Max(sequencia) as sequencia from viras where codigo_local_vira = " + local.Name + " and hora_saida is null", "viras");
                if (status_consulta == true)
                    consulta("Insert into viras (codigo_local_vira, codigo_linha, hora_entrada, maquinista, sequencia) values "
                              + "(" + local.Name + ", " + linha + ", '" + Convert.ToDateTime(maquinista.hora_entrada_vira).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + maquinista.matricula + "', " + odtAux.Rows[0]["sequencia"].ToString() + " + 1)", "viras");
                if (status_consulta == true)
                    odtAux = consulta("update Linha set atualizacao = (atualizacao + 1) where codigo = " + linha, "linha");
                atualizacao = atualizacao + 1;
            }
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
                btn_Gravar.PerformClick();
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
                btn_Gravar.Focus();

                cmb_maquinista.DataSource = consulta("select matricula, concat(nome,' ',matricula) as name from maquinistas where status <> 'Inativo' and not exists (select matricula_maquinista from maquinistas_registros where maquinistas.matricula = maquinistas_registros.matricula_maquinista and data_hora_saida is null) order by nome", "maquinistas");
                cmb_maquinista.DisplayMember = "name";
                cmb_maquinista.ValueMember = "matricula";
                cmb_maquinista.Focus();
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
                        atualizacao = int.Parse(odtAux.Rows[0][0].ToString());
                        atualiza();
                        status_consulta = false;
                    }
                odtAux = consulta("Select versao from versao", "versao");
                if (odtAux.Rows.Count > 0)
                    if (odtAux.Rows[0]["versao"].ToString() != versao)
                    {
                        timer1.Enabled = false;
                        MessageBox.Show("Essa versão do aplicativo não é a mais atual, favor abrir a nova versão!", "Sistema de Controle de Viagens");
                        this.Close();
                    }
                status_consulta = true;
            }
                
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Err escalao na conexão codigo 10!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err escala timer - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                timer1.Enabled = true;
                status_consulta = true;
            }
        }

        private void cmbLinha_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_maquinista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btn_Gravar.PerformClick();
        }

        private void cmbTrem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btn_Gravar.PerformClick();
        }

        private void adicionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            codigo_local_vira = int.Parse(cms_maquinista.SourceControl.Name);
            pnl_acao.Visible = true;
            checkBox1.Visible = true;
            lbl_obs.Visible = true;
            lbl_obs.Text = "Observação";
            lbl_hora.Visible = true;
            lbl_nome.Visible = true;
            panel1.Visible = true;
            chkTv.Visible = true;
            txt_obs.Visible = true;
            txt_data_hora_entrada.Visible = true;
            txt_data_hora_entrada.Text = DateTime.Now.ToString();
            cmb_maquinista.Visible = true;
            cmb_maquinista.Enabled = true;

        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            maquinista = null;
            maquinista = new Maquinista();
            Info info = new Info();
            info = (Info)cms_remove_maquinista.SourceControl.Tag;
            maquinista.matricula = info.maquinista;
            odtAux = consulta("Select * from maquinistas_registros where matricula_maquinista = '" + info.maquinista + "' and data_hora_saida is null", "maquinistas_registros");

            groupBox1.Text = "Editar Maquinista";
            lbl_hora.Text = "Entrada";
            lbl_hora.Visible = true;
            txt_data_hora_entrada.Mask = "00/00/0000 90:00";
            txt_data_hora_entrada.TextMaskFormat = MaskFormat.IncludeLiterals;
            txt_data_hora_entrada.Visible = true;
            pnl_acao.Visible = true;
            checkBox1.Visible = true;
            chkTv.Visible = true;
            lbl_obs.Visible = true;
            lbl_obs.Text = "Observação";
            txt_obs.Visible = true;

            txt_obs.Text = "";
            cmb_maquinista.Enabled = false;
            cmb_maquinista.Visible = true;
            lbl_nome.Visible = true;
            try
            {
                txt_data_hora_entrada.Text = odtAux.Rows[0]["data_hora_entrada"].ToString();
            }
            catch
            {
                txt_data_hora_entrada.Text = "";
            }
            atualiza();
            cmb_maquinista.Text = cms_remove_maquinista.SourceControl.Name;
        }

        private void removerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            maquinista = null;
            maquinista = new Maquinista();
            Info info = new Info();
            info = (Info)cms_remove_maquinista.SourceControl.Tag;
            maquinista.matricula = info.maquinista;

            groupBox1.Text = "Remover Maquinista";
            lbl_hora.Text = "saida";
            lbl_hora.Visible = true;
            lbl_nome.Visible = true;
            txt_data_hora_entrada.Mask = "00/00/0000 90:00";
            txt_data_hora_entrada.TextMaskFormat = MaskFormat.IncludeLiterals;
            txt_data_hora_entrada.Visible = true;
            pnl_acao.Visible = true;
            checkBox1.Visible = false;
            chkTv.Visible = false;
            lbl_obs.Visible = false;
            txt_obs.Visible = false;
            cmb_maquinista.Enabled = false;
            cmb_maquinista.Visible = true;
            txt_data_hora_entrada.Text = DateTime.Now.ToString();
            cmb_maquinista.Text = cms_remove_maquinista.SourceControl.Name;

            atualiza();

        }



        private void tabControl1_DragEnter(object sender, DragEventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
                tabControl1.SelectedIndex = 0;
            else
                tabControl1.SelectedIndex = 2;
        }

        private void editarNomeDoLocalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            codigo_local_vira = int.Parse(cms_maquinista.SourceControl.Name);
            pnl_acao.Visible = true;
            cmb_maquinista.Visible = false;
            lbl_obs.Text = "Novo nome:";
            lbl_obs.Visible = true;
            lbl_nome.Visible = false;
            lbl_hora.Visible = false;
            panel1.Visible = true;
            checkBox1.Visible = false;
            chkTv.Visible = false;
            txt_data_hora_entrada.Visible = false;
            txt_obs.Visible = true;

            txt_obs.Text = "";

            groupBox1.Text = "Editar Local";

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void criarNovoLocalToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pnl_acao.Visible = true;
            cmb_maquinista.Visible = false;
            checkBox1.Visible = false;
            panel1.Visible = true;
            chkTv.Visible = false;
            txt_data_hora_entrada.Visible = false;
            txt_obs.Visible = true;
            txt_obs.Text = "";
            lbl_obs.Text = "Nome:";
            lbl_obs.Visible = true;
            lbl_nome.Visible = false;
            lbl_hora.Visible = false;
            groupBox1.Text = "Criar Local";
        }

        private void cms_maquinista_Opening(object sender, CancelEventArgs e)
        {

        }

        private void excluirLocalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cms_maquinista.SourceControl.Controls.Count > 1)
                MessageBox.Show("Remova todos os maquinistas primeiro", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                codigo_local_vira = int.Parse(cms_maquinista.SourceControl.Name);
                if (MessageBox.Show("Deseja Realmente remover este local?", "Sistema de Controle de Viagens", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    consulta("Delete from locais_vira where codigo = " + codigo_local_vira, "Locais_vira");
                    atualiza();
                }
            }
        }

        private void lbl_nome_Click(object sender, EventArgs e)
        {

        }

        private void lbl_obs_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_editar_mqt_Click(object sender, EventArgs e)
        {
            if (txt_nome_editar.Text == "")
                MessageBox.Show("Digite um nome válido!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                consulta("update maquinistas set nome = '" + txt_nome_editar.Text.ToUpper() + "' where matricula = '" + txt_matricula_editar.Text + "'", "maquinistas");
                
                MessageBox.Show("Alteração Efetuada!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                atualiza();
                cmbLinha.DataSource = consulta("Select * From Linha", "Linha");
                cmbLinha.DisplayMember = "Linha";
                cmbLinha.ValueMember = "codigo";
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            consulta("update maquinistas set status = 'Inativo' where matricula = '" + txt_matricula_editar.Text + "'", "maquinistas");

            MessageBox.Show("Alteração Efetuada!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cmbLinha.DataSource = consulta("Select * From Linha", "Linha");
            cmbLinha.DisplayMember = "Linha";
            cmbLinha.ValueMember = "codigo";
            atualiza();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            printDGV.Print_DataGridView(dataGridView1, "Linha " + cmbLinha.Text + " - " + dateTimePicker1.Value.ToShortDateString());
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

        private void ToCsV(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "Linha " + cmbLinha.Text + " - " + dateTimePicker1.Value.ToShortDateString() + "\r\n";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount - 1; i++)
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
            DataTable odt_aux = consulta("select * from (select prefixo as Prefixo, trem as Trem, nome as Nome, if(final_viagem = 'Excluído', final_viagem, null) as final_viagem "
                + "from historico left join maquinistas on maquinistas.matricula = historico.maquinista "
                + "where data = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' "
                + "and historico.linha = " + linha + " "
                + "and (trem like '%"+txt_filtro.Text+"%' or prefixo like '%"+txt_filtro.Text+"%' or nome like '%"+txt_filtro.Text+"%') "
                + "union "
                + "select convert(proxima_escala, unsigned) as Prefixo, trem as Trem, nome as Nome, null as final_viagem "
                + "from viras "
                + "inner join historico on viras.proxima_escala = historico.prefixo "
                + "inner join maquinistas on maquinistas.matricula = viras.maquinista "
                + "where data = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' "
                + "and hora_saida >= '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + " 03:00:00' "
                + "and hora_saida <= '" + dateTimePicker1.Value.AddDays(1).ToString("yyyy-MM-dd") + " 03:00:00' "
                + "and (trem like '%" + txt_filtro.Text + "%' or prefixo like '%" + txt_filtro.Text + "%' or nome like '%" + txt_filtro.Text + "%') "
                + "and viras.codigo_linha = " + linha + ") a "
                + "order by convert(prefixo, unsigned)", "historico");
            int i;
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Prefixo", "Prefixo");
            dataGridView1.Columns.Add("Trem", "Trem");
            dataGridView1.Columns.Add("Nome", "Nome");
        
            for (i = 0; i < odt_aux.Rows.Count; i++)
            {
                if (dataGridView1.Rows.Count <= i + 1)
                    dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = odt_aux.Rows[i][0].ToString();
                dataGridView1.Rows[i].Cells[1].Value = odt_aux.Rows[i][1].ToString();
                dataGridView1.Rows[i].Cells[2].Value = odt_aux.Rows[i][2].ToString();
                if (odt_aux.Rows[i]["final_viagem"].ToString() == "Excluído")
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
            }
            oCn.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                dateTimePicker1_ValueChanged(null, null);
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void txt_filtro_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {
            dateTimePicker1_ValueChanged(null, null);
        }



    }

    class Maquinista
    {
        public string matricula;
        public int sequencia;
        public string data_hora_entrada = "";
        public string hora_entrada_vira = "";


        public Maquinista()
        {
            matricula = "";
            data_hora_entrada = "";
            hora_entrada_vira = "";
            sequencia = 0;
        }

    }

}
