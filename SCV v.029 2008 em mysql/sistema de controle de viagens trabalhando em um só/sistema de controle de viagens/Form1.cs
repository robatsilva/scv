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
    public partial class Form1 : Form
    {
        Svc svc;
        
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string u)
        {
            InitializeComponent();
            usuario = u;
        }
        
        //string mstring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\masterpixftp\scv\luz\SCV.accdb";
        string mstring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=SCV.accdb";
        OleDbConnection oCn;
        
        DataTable odtImpar;
        DataTable odtPar;
        DataTable odtAux;

        Label sendo_movido;
        
        string trem, prefixo, usuario;
        int left, atualizacao;
        int codigo_local_vira;

        Maquinista maquinista = new Maquinista();

        private void corPrefixo(Panel prefixo)
        {
            prefixo.Controls[0].BackColor = Color.Green;
            prefixo.Controls[1].BackColor = Color.Green;
            prefixo.Controls[2].BackColor = Color.Green;
   
        }

        private void carrega_vira(int sequencia_vira, int height)
        {
            odtAux = Svc.consulta("select código, local_vira from locais_vira where Código_linha = " + Svc.linha + " and (Sequencia_local_vira mod 2) = " + sequencia_vira + " and tela = 'VIRA'", "Locais_vira");
            if (sequencia_vira == 0)
                left = 10;
            else
                left = panel3.Width * sequencia_vira + (-sequencia_vira * 200);
                    DataTable odt_aux2;
                    

                    //for para colocar os locais de vira
                    for (int i = 0; i < odtAux.Rows.Count; i++)
                    {
                        Panel local = cria_local_maquinista(odtAux, i, height);

                        
                        string sql = "Select nome, data_hora_entrada, matrícula, sequencia ";
                        sql = sql + "from (viras ";
                        sql = sql + "inner join maquinistas on maquinistas.matrícula = viras.maquinista) ";
                        sql = sql + "inner join [maquinistas_registros] on [viras].maquinista = [maquinistas_registros].matricula_maquinista ";
                        sql = sql + "where [viras].código_linha = " + Svc.linha + " ";
                        sql = sql + "and [viras].código_local_vira = " + odtAux.Rows[i]["código"].ToString() + " ";
                        sql = sql + "and [viras].hora_saída Is Null ";
                        sql = sql + "and [maquinistas_registros].Data_hora_saída Is Null order by viras.sequencia";
                        odt_aux2 = Svc.consulta(sql, "Viras");

                        //dentro de cada local vira, os maquinistas
                        for (int n = 0; n < odt_aux2.Rows.Count; n++)
                        {
                            string trem_vira;
                            try
                            {
                                if (sequencia_vira % 2 == 0)
                                    trem_vira = odtPar.Rows[n]["id"].ToString();
                                else
                                    trem_vira = odtImpar.Rows[n]["id"].ToString();
                            }
                            catch
                            {
                                trem_vira = "";
                            }
                            local.Controls.Add(cria_label_maquinista(odt_aux2, n, trem_vira));
                        }
                        if (sequencia_vira == 0)
                            left += (local.Width + 20);
                        else
                            left += (local.Width + 20) * -1;

                        if (sequencia_vira % 2 == 0)
                            odtPar = null;
                        else
                            odtImpar = null;
                        
                        panel3.Controls.Add(local);
                    }
        }

        private void carrega_outros(int height)
        {
            odtAux = Svc.consulta("select código, local_vira from locais_vira where Código_linha = " + Svc.linha + " and tela = 'LOCAIS'", "Locais_vira");
            left = 10;
            DataTable odt_aux2;


            //for para colocar os locais de vira
            for (int i = 0; i < odtAux.Rows.Count; i++)
            {
                Panel local = cria_local_maquinista(odtAux, i, height);

                //odt_aux2 = Svc.consulta("Select trens.Trem, trens.observacao, locais.[local], trens.mensagem from Trens inner Join Locais on trens.[local] = locais.código where Linha = " + cmbLinha.SelectedValue + " and Trens.[Local] = " + odtAux.Rows[i][0].ToString() + " and Not Exists (Select Id from andamento where Trens.Trem = andamento.Id and Trens.Linha = " + cmbLinha.SelectedValue + ")", "Trens");
                string sql = "Select nome, data_hora_entrada, matrícula, sequencia ";
                sql = sql + "from (viras ";
                sql = sql + "inner join maquinistas on maquinistas.matrícula = viras.maquinista) ";
                sql = sql + "inner join [maquinistas_registros] on [viras].maquinista = [maquinistas_registros].matricula_maquinista ";
                sql = sql + "where [viras].código_linha = " + Svc.linha + " ";
                sql = sql + "and [viras].código_local_vira = " + odtAux.Rows[i]["código"].ToString() + " ";
                sql = sql + "and [viras].hora_saída Is Null ";
                sql = sql + "and [maquinistas_registros].Data_hora_saída Is Null";
                odt_aux2 = Svc.consulta(sql, "Viras");

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
            odtAux = Svc.consulta("select código, local_vira from locais_vira where Código_linha = " + Svc.linha + " and tela = 'NÃO DEFINIDO'", "Locais_vira");
            left = panel3.Width/2 - 100;
            DataTable odt_aux2;


            //for para colocar os locais de vira
            for (int i = 0; i < odtAux.Rows.Count; i++)
            {
                Panel local = cria_local_maquinista(odtAux, i, height);
                
                string sql = "Select distinct nome, data_hora_entrada, matrícula, sequencia ";
                sql = sql + "from (viras ";
                sql = sql + "inner join maquinistas on maquinistas.matrícula = viras.maquinista) ";
                sql = sql + "inner join [maquinistas_registros] on [viras].maquinista = [maquinistas_registros].matricula_maquinista ";
                sql = sql + "where [viras].código_linha = " + Svc.linha + " ";
                sql = sql + "and [viras].código_local_vira = " + odtAux.Rows[i]["código"].ToString() + " ";
                sql = sql + "and [viras].hora_saída Is Null ";
                sql = sql + "and [maquinistas_registros].Data_hora_saída Is Null ";
                odt_aux2 = Svc.consulta(sql, "Viras");

                //dentro de cada local vira, os maquinistas
                for (int n = 0; n < odt_aux2.Rows.Count; n++)
                {
                    string sub_sql = "Select próxima_escala ";
                    sub_sql = sub_sql + "from viras ";
                    sub_sql = sub_sql + "where [viras].código_linha = " + Svc.linha + " ";
                    sub_sql = sub_sql + "and [viras].hora_saída = (select max(hora_saída) from viras where maquinista = '" + odt_aux2.Rows[n]["matrícula"].ToString() + "')";
                    DataTable odt_aux3;
                    odt_aux3 = Svc.consulta(sub_sql, "viras");

                    local.Controls.Add(cria_label_maquinista(odt_aux2, n, odt_aux3.Rows[0]["próxima_escala"].ToString()));

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
            texto.Font = new Font("Arial Black", 10);
            texto.TextAlign = ContentAlignment.MiddleLeft;
            texto.Width = panel4.Width / 6 - 20;
            texto.Height = 50;
            texto.Top = n * 50 + 30;
            texto.ForeColor = Color.White;
            texto.AllowDrop = true;
            Info info = new Info();
            info.sequencia = int.Parse(odt.Rows[n]["sequencia"].ToString());
            texto.ContextMenuStrip = cms_remove_maquinista;
            info.maquinista = odt.Rows[n]["matrícula"].ToString();
            texto.Tag = info;

            texto.MouseMove += new MouseEventHandler(texto_MouseMove);
            texto.DragEnter += texto_DragEnter;
            texto.DragDrop += texto_DragDrop;
            texto.DragLeave += texto_DragLeave;

            texto.Text = proxima_escala + " " + maquinista_com_tempo(odt, n);
            texto.BackColor = Color.Transparent;
            return texto;
        }

        private string maquinista_com_tempo(DataTable odt, int n)
        {
            int h = int.Parse(odt.Rows[n]["data_hora_entrada"].ToString().Substring(11, 2));
            int m = int.Parse(odt.Rows[n]["data_hora_entrada"].ToString().Substring(14, 2));
            int s = int.Parse(odt.Rows[n]["data_hora_entrada"].ToString().Substring(17, 2));
            TimeSpan t_maquinista = new TimeSpan(h, m, s);
            TimeSpan t_agora = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
            TimeSpan t_restante = new TimeSpan();
            TimeSpan t_servico = new TimeSpan(08, 00, 00);


            t_restante = t_maquinista.Add(t_servico);

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
            local.Name = odt.Rows[i]["código"].ToString();
            local.Width = panel4.Width / 6;
            local.Height = panel4.Height - 60;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                oCn = new OleDbConnection(mstring);
                oCn.Open();
                Svc.form1 = this;
                Svc.oCn = oCn;
                this.Width = Screen.PrimaryScreen.Bounds.Width;
                tabControl1.Width = this.Width-15; 
                panel1.Width = this.Width - 40;
                panel2.Width = this.Width - 40;
                panel3.Width = this.Width - 40;
                panel4.Width = this.Width - 40;

                Svc.Tamanho = panel1.Width / 13 - 10;
                panel1.Left = (this.Width - panel1.Width) / 2 - 5;

                Svc.dia = DateTime.Now;
                if (Svc.dia.Hour < 3)
                    Svc.dia = Svc.dia.AddDays(-1);


                cmbLinha.DataSource = Svc.consulta("Select * From Linha", "Linha");
                cmbLinha.DisplayMember = "Linha";
                cmbLinha.ValueMember = "Código";

                Svc.linha = cmbLinha.SelectedValue.ToString();

                if (usuario == "Visualização")
                    svc = new Svc_visualiza();
                if (usuario == "Escalante")
                    svc = new Svc_escala();
                if (usuario == "CCT II")
                    svc = new Svc_cctII();
                
                panel1.Focus();
                panel1.Visible = true;
                Svc.consulta("Update Linha set Atualizacao = 0", "Linha");
                atualizacao = 0;
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Erro na conexão código 01!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Svc.consulta("Insert Into Logs (Log, data) Values ('Err Load - " + ex.Message + "', '" + DateTime.Now + "')", "Log");
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

                if (groupBox1.Text == "Cadastrar Maquinista")
                {
                    if (cmb_maquinista.Text.Trim() == "")
                    {
                        MessageBox.Show("Digite um maquinista!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (txt_data_hora_entrada.Text.Trim() == "" || txt_data_hora_entrada.Text.Length < 9)
                    {
                        MessageBox.Show("Digite uma matrícula válida!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    odtAux = Svc.consulta("select * from maquinistas where matrícula = '" + txt_data_hora_entrada.Text + "'", "maquinistas");
                    if (odtAux.Rows.Count > 0)
                    {
                        MessageBox.Show("Esta matrícula já está cadastrada!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Svc.consulta("insert into maquinistas (matrícula, nome) values ('" + txt_data_hora_entrada.Text + "', '" + cmb_maquinista.Text.ToUpper() + "')", "maquinistas");
                    MessageBox.Show("Maquinista incluído com sucesso!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }


                if (groupBox1.Text == "Inserir Maquinista")
                {

                    if (cmb_maquinista.SelectedValue == null)
                    {
                        if (MessageBox.Show("Maquinista " + cmb_maquinista.Text + " não está cadastrado, deseja cadastrá-lo?", "Sistema de Controle de Viagens", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            groupBox1.Text = "Cadastrar Maquinista";
                            lbl_hora.Text = "Informe a Matrícula";
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

                    odtAux = Svc.consulta("select * from maquinistas_registros where matricula_maquinista = '" + maquinista.matricula + "' and data_hora_saída is null", "maquinistas_registros");
                    if (odtAux.Rows.Count > 0)
                    {
                        MessageBox.Show("Este maquinista ainda está com um período aberto. Encerre o período para continuar!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        maquinista = null;
                        maquinista = new Maquinista();
                        return;
                    }

                    
                    
                    odtAux = Svc.consulta("select Max(sequencia) as sequencia from viras where código_local_vira = " + codigo_local_vira + " and hora_saída is null", "viras");
                    Svc.consulta("Insert into viras (código_local_vira, código_linha, hora_entrada, maquinista, sequencia) values "
                        + "(" + codigo_local_vira + ", " + Svc.linha + ", '" + maquinista.hora_entrada_vira + "', '" + maquinista.matricula + "', " + odtAux.Rows[0]["sequencia"].ToString() + " + 1)", "viras");
                    
                    Svc.consulta("Insert into maquinistas_registros (matricula_maquinista, data_hora_entrada) values ('" + maquinista.matricula + "', '" + maquinista.data_hora_entrada + ":00')", "maquinistas_registros");
                    
                    Svc.consulta("update Linha set atualizacao = (atualizacao + 1) where código = " + Svc.linha, "linha");
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

                    Svc.consulta("update maquinistas_registros set data_hora_saída = '" + maquinista.data_hora_entrada + "' where matricula_maquinista = '"+maquinista.matricula+"' and data_hora_saída is null", "maquinistas_registros");
                    Svc.consulta("update viras set hora_saída = '" + maquinista.hora_entrada_vira + "' where maquinista = '"+maquinista.matricula+"' and hora_saída is null", "viras");

                    odtAux = Svc.consulta("update Linha set atualizacao = (atualizacao + 1) where código = " + Svc.linha, "linha");
                    atualizacao = atualizacao + 1;
                }
                groupBox1.Text = "Inserir Maquinista";
                lbl_hora.Text = "Entrada";
                txt_data_hora_entrada.Mask = "00/00/0000 90:00";
                txt_data_hora_entrada.TextMaskFormat = MaskFormat.IncludeLiterals;
                cmb_maquinista.Text = "";
                cmb_maquinista.Enabled = true;
                txt_data_hora_entrada.Clear();
                svc.atualiza();
                pnl_acao.Visible = false;
                maquinista = null;
                maquinista = new Maquinista();
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Erro na conexão código 02!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Svc.consulta("Insert Into Logs (Log, data) Values ('Err - Gravar" + ex.Message + "', '" + DateTime.Now + "')", "Log");
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
                lbl_obs.Text = "Maquinista";
                lbl_obs.Visible = true;
                cmb_maquinista.Visible = true;
                prefixo = info.prefixo;
                trem = info.trem;
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Erro ao Editar Mensagem!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Svc.consulta("Insert Into Logs (Log, data) Values ('Err editar - " + ex.Message + "', '" + DateTime.Now + "')", "Log");
                timer1.Enabled = true;
            }
        }

         private void btnCancelar_Click(object sender, EventArgs e)
        {
            pnl_acao.Visible = false;
            cmb_maquinista.Enabled = true;
            trem = "";
            prefixo = "";
            groupBox1.Text = "Inserir Maquinista";
            lbl_hora.Text = "Entrada";
            txt_data_hora_entrada.Mask = "00/00/0000 90:00";
            txt_data_hora_entrada.TextMaskFormat = MaskFormat.IncludeLiterals; 
            svc.atualiza();
        }

         private void texto_MouseMove(object sender, MouseEventArgs e)
         {
             if (e.Button == MouseButtons.Left)
             {
                 sendo_movido = (Label)sender;
                 sendo_movido.DoDragDrop(sendo_movido.Tag, DragDropEffects.Link);
             }

         }

         private void texto3_MouseMove(object sender, MouseEventArgs e)
         {
             if (e.Button == MouseButtons.Left)
             {
                 sendo_movido = (Label)sender;
                 sendo_movido.DoDragDrop(sendo_movido.Tag, DragDropEffects.Link);
             }

         }

         void texto3_DragEnter (object sender, DragEventArgs e)
         {
             try
            {
                if (sendo_movido != sender)
                    e.Effect = DragDropEffects.Link;
            }
             catch (Exception ex)
             {
                 timer1.Enabled = false;
                 //MessageBox.Show("Erro na conexão código 08!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 Svc.consulta("Insert Into Logs (Log, data) Values ('Err drag enter - " + ex.Message + "', '" + DateTime.Now + "')", "Log");
                 timer1.Enabled = true;
             }
         }

         void texto3_DragDrop(object sender, DragEventArgs e)
         {
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
                Svc.consulta("update viras set Hora_saída = '" + DateTime.Now.ToString() + "', próxima_escala = '" + info.prefixo + "' where maquinista = '" + maquinista.matricula + "' and hora_saída is null", "viras");
             else
                Svc.consulta("update andamento set Maquinista = '' where id = '" + matricula.trem + "'", "viras");
             
             Svc.consulta("update andamento set maquinista = '" + maquinista.matricula + "' where id = '" + info.trem + "' ", "andamento");
             odtAux = Svc.consulta("update Linha set atualizacao = (atualizacao + 1) where código = " + Svc.linha, "linha");
             atualizacao = atualizacao + 1;
             svc.atualiza();
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
                 //MessageBox.Show("Erro na conexão código 08!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 Svc.consulta("Insert Into Logs (Log, data) Values ('Err drag enter - " + ex.Message + "', '" + DateTime.Now + "')", "Log");
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
                 //MessageBox.Show("Erro na conexão código 08!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 Svc.consulta("Insert Into Logs (Log, data) Values ('Err drag enter - " + ex.Message + "', '" + DateTime.Now + "')", "Log");
                 timer1.Enabled = true;
             }
         }

         void texto_DragDrop(object sender, DragEventArgs e)
         {
             Label mqt = (Label)sender;
             Info info_sendo_movido = (Info)sendo_movido.Tag;
             if (sendo_movido.Parent == mqt.Parent && sendo_movido != sender)
             {
                 Info info = (Info)mqt.Tag;
                 Svc.consulta("update viras set sequencia = sequencia + 1 where sequencia >= " + info.sequencia + " and código_local_vira = " + mqt.Parent.Name + " and hora_saída is null", "viras");
                 Svc.consulta("update viras set sequencia = " + info.sequencia + " where maquinista = '" + info_sendo_movido.maquinista + "' and hora_saída is null", "viras");
                 odtAux = Svc.consulta("update Linha set atualizacao = (atualizacao + 1) where código = " + Svc.linha, "linha");
                 atualizacao = atualizacao + 1;
                 svc.atualiza();
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
                 //MessageBox.Show("Erro na conexão código 08!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 Svc.consulta("Insert Into Logs (Log, data) Values ('Err drag enter - " + ex.Message + "', '" + DateTime.Now + "')", "Log");
                 timer1.Enabled = true;
             }
         }

         void Local_DragDrop(object sender, DragEventArgs e)
         {
             Panel local = (Panel)sender;
             Label matricula = (Label)sendo_movido;
             Info info = (Info)matricula.Tag;

             maquinista = null;
             maquinista = new Maquinista();
             maquinista.matricula = info.maquinista;
             maquinista.hora_entrada_vira = DateTime.Now.ToString();

             if (info.trem == "")
                 Svc.consulta("update viras set hora_saída = '" + maquinista.hora_entrada_vira + "', próxima_escala = '" + local.Controls[0].Name + "' where maquinista = '" + maquinista.matricula + "' and hora_saída is null", "viras");
             else
                 Svc.consulta("update andamento set maquinista = '' where id = '" + info.trem + "' ", "andamento");


             odtAux = Svc.consulta("select Max(sequencia) as sequencia from viras where código_local_vira = " + local.Name + " and hora_saída is null", "viras");
             Svc.consulta("Insert into viras (código_local_vira, código_linha, hora_entrada, maquinista, sequencia) values "
                        + "(" + local.Name + ", " + Svc.linha + ", '" + maquinista.hora_entrada_vira + "', '" + maquinista.matricula + "', " + odtAux.Rows[0]["sequencia"].ToString() + " + 1)", "viras");

             odtAux = Svc.consulta("update Linha set atualizacao = (atualizacao + 1) where código = " + Svc.linha, "linha");
             atualizacao = atualizacao + 1;
             
             svc.atualiza();
         }

        private void Ok()
        {
            Svc.linha = cmbLinha.SelectedValue.ToString();
            
            //Svc.dia = dateTimePicker1.Value.Date;
            //sSqlImpar = "SELECT * from andamento Where Linha = " + linha + " and Data = DateValue('" + Svc.dia.Date + "') and (Sequencia mod 2) = 1 order by sequencia";
            //sSqlPar = "SELECT * from andamento Where Linha = " + linha + " and Data = DateValue('" + Svc.dia.Date + "') and (Sequencia mod 2) = 0 order by sequencia";
            pnl_acao.Visible = false;
            if (svc != null)
                svc.atualiza();
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
                Svc.dia = DateTime.Now;

                if (Svc.dia.Hour < 3)
                    Svc.dia = Svc.dia.AddDays(-1);
                panel1.Enabled = false;
                btnGravar.Focus();

                cmb_maquinista.DataSource = Svc.consulta("select matrícula, (matrícula + ' ' + nome) as name from maquinistas where not exists (select matricula_maquinista from maquinistas_registros where maquinistas.matrícula = maquinistas_registros.matricula_maquinista and data_hora_saída is null) order by nome", "maquinistas");
                cmb_maquinista.DisplayMember = "name";
                cmb_maquinista.ValueMember = "matrícula";
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
                Svc.dia = DateTime.Now;

                if (Svc.dia.Hour < 3)
                    Svc.dia = Svc.dia.AddDays(-1);
                odtAux = Svc.consulta("Select atualizacao from linha where código = " + Svc.linha, "linha");
                if (atualizacao != int.Parse(odtAux.Rows[0][0].ToString()))
                {
                    atualizacao = int.Parse(odtAux.Rows[0][0].ToString());
                    svc.atualiza();
                }
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Erro na conexão código 10!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Svc.consulta("Insert Into Logs (Log, data) Values ('Err timer - " + ex.Message + "', '" + DateTime.Now + "')", "Log");
                timer1.Enabled = true;
            }
        }

        private void cmbLinha_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_maquinista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnGravar.PerformClick();
        }

        private void cmbTrem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnGravar.PerformClick();
        }

        private void adicionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            codigo_local_vira = int.Parse(cms_maquinista.SourceControl.Name);
            pnl_acao.Visible = true;
            txt_data_hora_entrada.Text = DateTime.Now.ToString();

        }

        private void removerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            maquinista = null;
            maquinista = new Maquinista();
            Info info = new Info();
            info = (Info)cms_remove_maquinista.SourceControl.Tag;
            maquinista.matricula = info.maquinista;
            
            groupBox1.Text = "Remover Maquinista";
            lbl_hora.Text = "Saída";
            txt_data_hora_entrada.Mask = "00/00/0000 90:00";
            txt_data_hora_entrada.TextMaskFormat = MaskFormat.IncludeLiterals;
            pnl_acao.Visible = true;
            cmb_maquinista.Enabled = false;
            txt_data_hora_entrada.Text = DateTime.Now.ToString();
            svc.atualiza();
            cmb_maquinista.Text = cms_remove_maquinista.SourceControl.Name;
        }

     

        private void tabControl1_DragEnter(object sender, DragEventArgs e)
        {
            if(tabControl1.SelectedIndex == 2)
                tabControl1.SelectedIndex = 0;
            else
                tabControl1.SelectedIndex = 2;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
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
