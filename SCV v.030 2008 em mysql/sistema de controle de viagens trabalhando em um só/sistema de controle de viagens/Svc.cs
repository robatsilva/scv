using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Sistema_de_Controle_de_Viagens
{
    public class Svc
    {
        public static string linha { get; set; }
        public static DateTime dia { get; set; }
        public static int Tamanho;
        private DataTable odtImpar;
        private DataTable odtPar;
        private int left;
        public static OleDbConnection oCn;
        public static Form1 form1;
        public static bool status_consulta = false;

        protected Label texto;
        protected Label texto2;
        protected Label texto3;
        protected Label sendoMovido;
        
        public Svc()
        {

        }

        public static DataTable consulta(string sql, string tabela)
        {
            try
            {
                if (oCn.State != ConnectionState.Open)
                    oCn.Open();
                OleDbDataAdapter oDA = new OleDbDataAdapter(sql, oCn);
                DataSet oDs = new DataSet();
                oDA.Fill(oDs, tabela);
                status_consulta = true;
                return oDs.Tables[tabela];
            }

            catch (Exception e)
            {
                status_consulta = false;
                //form1.timer1.Enabled = false;
                //MessageBox.Show("Ocorreu um erro no Banco de Dados!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (oCn.State == ConnectionState.Open)
                {

                    ////////////////////////////////////////////////////////////////////////////////////////////
                    oCn.Close();
                    oCn.Open();
                    ////////////////////////////////////////////////////////////////////////////////////////////
                    try
                    {
                        OleDbDataAdapter oDA = new OleDbDataAdapter("Insert Into Logs (Log, data) Values ('" + sql.Replace("'", "-") + " - " + e.Message.Replace("'", "-") + "', '" + DateTime.Now + "')", oCn); ;
                        DataSet oDs = new DataSet();
                        oDA.Fill(oDs, tabela);
                    }
                    catch
                    {
                    }
                }
                else
                {
                    MessageBox.Show("Você não está conectado, Se a conexão não retornar reinicie o programa!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                form1.timer1.Enabled = true;
                return null;
            }
        }

        public void atualiza()
        {

            string sSqlImpar = "SELECT * from (andamento Left Join Trens on andamento.id = trens.trem) left join maquinistas on maquinistas.matrícula = andamento.maquinista Where andamento.Linha = " + linha + " and (Sequencia mod 2) = 1 order by sequencia";
            string sSqlPar = "SELECT * from (andamento Left Join Trens on andamento.id = trens.trem) left join maquinistas on maquinistas.matrícula = andamento.maquinista Where andamento.Linha = " + linha + "  and (Sequencia mod 2) = 0 order by sequencia";
            try
            {
                System.Threading.Thread.Sleep(50);
                odtImpar = consulta(sSqlImpar, "andamento");
                odtPar = consulta(sSqlPar, "andamento");
                
                form1.lbl_circulacao.Text = "Circulando: " + (odtImpar.Rows.Count + odtPar.Rows.Count).ToString();
                left = 30;
                for (int i = form1.panel1.Controls.Count - 1; i >= 0; i--)
                    form1.panel1.Controls[i].Dispose();

                for (int i = form1.panel2.Controls.Count - 1; i >= 0; i--)
                    form1.panel2.Controls[i].Dispose();

                for (int i = form1.panel3.Controls.Count - 1; i >= 0; i--)
                    form1.panel3.Controls[i].Dispose();

                for (int i = form1.panel4.Controls.Count - 1; i >= 0; i--)
                    form1.panel4.Controls[i].Dispose();

                form1.panel1.Controls.Clear();
                form1.panel2.Controls.Clear();
                form1.panel3.Controls.Clear();
                form1.panel4.Controls.Clear();

                GC.Collect();

                int posicao = form1.panel1.Width - Tamanho;
                if (odtImpar.Rows.Count > 1)
                    posicao = (((form1.panel1.Width - 60 - (Tamanho * odtImpar.Rows.Count)) / (odtImpar.Rows.Count - 1)) + Tamanho);
                Panel prefixo = new Panel();
                for (int i = odtImpar.Rows.Count - 1; i >= 0; i--)
                {
                    carregaPanel(odtImpar, prefixo, posicao - Tamanho, Tamanho - posicao, i, 10, posicao);
                    left = left + posicao;
                }

                left = form1.panel1.Width - Tamanho - 30;
                if (odtPar.Rows.Count > 1)
                    posicao = (((form1.panel1.Width - 60 - (Tamanho * odtPar.Rows.Count)) / (odtPar.Rows.Count - 1)) + Tamanho);
                for (int i = odtPar.Rows.Count - 1; i >= 0; i--)
                {
                    carregaPanel(odtPar, prefixo, posicao - Tamanho, Tamanho, i, form1.panel1.Height - 60 - (form1.panel1.Height / 2 - 60), posicao);
                    left = left - posicao;
                }
                //carrega_vira(1, prefixo.Height / 5 + 10);
                //carrega_vira(0, prefixo.Height / 5 + 10);
                //carrega_nao_definido(prefixo.Height / 5 + 10);
                //carrega_outros(prefixo.Height / 5 + 10);

            }
            catch (Exception e)
            {
                form1.timer1.Enabled = false;
                //MessageBox.Show("Erro na atualização!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                consulta("Insert Into Logs (Log, data) Values ('Err Atualização - " + e.Message + "', '" + DateTime.Now + "')", "Log");
                form1.timer1.Enabled = true;
            }
        }

        public virtual void carregaPanel(DataTable odt, Panel prefixo, int widthPic, int leftPic, int i, int top, int posicao)
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
            prefixo.Left = left;
            prefixo.Top = top;
            prefixo.Width = Tamanho;
            prefixo.Height = form1.panel1.Height / 2 - 20; //subtrai 20 para espaçar por igual nas bordas laterais

            texto = new Label();
            texto.Name = "texto1";
            texto.Font = new Font("Arial Black", 16);
            texto.ForeColor = Color.White;
            texto.TextAlign = ContentAlignment.MiddleCenter;
            texto.Width = prefixo.Width;
            texto.Height = prefixo.Height / 3;

            texto.Text = info.prefixo;
            texto.Tag = info;
            prefixo.Controls.Add(texto);

            texto2 = new Label();
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

            texto3 = new Label();
            texto3.Font = new Font("Arial Black", 8);
            texto3.TextAlign = ContentAlignment.TopCenter;
            texto3.Width = prefixo.Width;
            texto3.Height = prefixo.Height / 3;
            texto3.ForeColor = Color.Yellow;
            texto3.Text = odt.Rows[i]["Nome"].ToString();
            texto3.Top = prefixo.Height / 3;
            texto3.BackColor = Color.Transparent;
            texto3.Tag = info;
            prefixo.Controls.Add(texto3);
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
            form1.panel1.Controls.Add(prefixo);

        }

        public virtual void carrega_panel_trens()
        {
            //preenche aba recolhidos
            DataTable odtAux = consulta("select distinct trens.[local], Locais.[local], Locais.código from Trens inner Join Locais on trens.[local] = locais.código where Trens.Linha = " + linha + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id and Trens.Linha = andamento.linha) order by locais.código", "Trens");
            int left = 10;
            DataTable odt_aux2;


            //for para colocar as estações
            for (int i = 0; i < odtAux.Rows.Count; i++)
            {
                Panel local = new Panel();
                local.Width = 95;
                local.Height = form1.panel2.Height - 10;
                local.Top = 0;
                local.Left = left;
                local.BorderStyle = BorderStyle.Fixed3D;
                local.AutoScroll = true;
                //odt_aux2 = consulta("Select trens.Trem, trens.observacao, locais.[local], trens.mensagem from Trens inner Join Locais on trens.[local] = locais.código where Linha = " + cmbLinha.SelectedValue + " and Trens.[Local] = " + odtAux.Rows[i][0].ToString() + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id and Trens.Linha = " + cmbLinha.SelectedValue + ")", "Trens");
                odt_aux2 = consulta("Select trens.Trem, trens.observacao, locais.[local], trens.mensagem from Trens inner Join Locais on trens.[local] = locais.código where Linha = " + linha + " and Trens.[Local] = " + odtAux.Rows[i][0].ToString() + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id and Trens.Linha = andamento.linha)", "Trens");

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
                texto1.Height = local.Height / 5 + 10;
                texto1.Top = 0;
                texto1.ForeColor = Color.Red;
                texto1.Text = odtAux.Rows[i][1].ToString();
                texto1.BackColor = Color.Transparent;
                local.Controls.Add(texto1);
                form1.panel2.Controls.Add(local);

            }
        }
    }


    public class Svc_cctII : Svc
    {
        public Svc_cctII()
        {
            form1.tabControl1.TabPages.RemoveByKey("tabPage3");
        }


        public override void carregaPanel(DataTable odt, Panel prefixo, int widthPic, int leftPic, int i, int top, int posicao)
        {
            base.carregaPanel(odt, prefixo, widthPic, leftPic, i, top, posicao);
            texto.MouseMove += prefixo_MouseMove;
            texto2.MouseMove += prefixo_MouseMove;
            texto3.MouseMove += prefixo_MouseMove;
            //texto3.DragEnter += new DragEventHandler(texto3_DragEnter);
            //texto3.DragDrop += new DragEventHandler(texto3_DragDrop);
            carrega_panel_trens();
        }
        private void prefixo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                sendoMovido = (Label)sender;
                sendoMovido.DoDragDrop(sendoMovido.Tag, DragDropEffects.Link);
            }

        }
    }


    public class Svc_escala : Svc
    {
        public Svc_escala()
        {
            form1.tabControl1.TabPages.RemoveByKey("tabPage3");
        }


        public override void carregaPanel(DataTable odt, Panel prefixo, int widthPic, int leftPic, int i, int top, int posicao)
        {
            base.carregaPanel(odt, prefixo, widthPic, leftPic, i, top, posicao);
            carrega_panel_trens();
        }

       
    }

    public class Svc_visualiza : Svc
    {
        public Svc_visualiza()
        {
            form1.tabControl1.TabPages.RemoveByKey("tabPage3");
            form1.tabControl1.TabPages.RemoveByKey("tabPage2");
        }
    }
    
}
