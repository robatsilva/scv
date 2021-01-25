using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sistema_de_Controle_de_Viagens
{
    
    public partial class frm_transferencia : Form
    {
        Form1 frm;

        public frm_transferencia()
        {
            InitializeComponent();
        }

        public frm_transferencia(Form1 f)
        {
            InitializeComponent();
            frm = f;

        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            int sequencia_aux;
            string prefixo_aux;
            string msg_aux;
            msg_aux = frm.mensagem;
            if (rdb_msg.Checked == false)
                frm.mensagem = "";
            if (txtPrefixo.Text != "" && int.Parse(txtPrefixo.Text) % 2 == frm.sequencia % 2)
            {
                sequencia_aux = frm.sequencia;
                prefixo_aux = txtPrefixo.Text;

                Info info = (Info)frm.sendoMovido_panel.Tag;
                frm.sequencia = info.sequencia;
                txtPrefixo.Text = info.prefixo;
                executaRecolher(prefixo_aux, "", frm.mensagem, msg_aux);
                if (frm.status_consulta == true)
                {
                    frm.sequencia = sequencia_aux;
                    txtPrefixo.Text = prefixo_aux;
                    executaAdicionar(frm.mensagem);

                    frm.odtAux = frm.consulta("update Linha set atualizacao = (atualizacao + 1) where codigo = " + cmbLinha.SelectedValue, "linha");
                    frm.atualizacao = frm.atualizacao + 1;
                }

            }
            else
                MessageBox.Show("Prefixo inválido", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
            frm.atualiza();

        }

        private void executaRecolher(string final_viagem, string local, string mensagem_trens, string mensagem_historico)
        {
            try
            {
                string sql = "Delete From Andamento where linha = " + frm.linha + " and ID = '" + cmbTrem.Text.ToUpper() + "'";

                frm.consulta(sql, "andamento");
                if (frm.status_consulta == true)
                {
                    sql = "Update Trens Set trens.observacao = '', Trens.mensagem = '" + mensagem_trens + "' where Trem = '" + cmbTrem.Text.ToUpper() + "'";

                    frm.consulta(sql, "Trens");
                    if (frm.status_consulta == true)
                    {
                        sql = "INSERT INTO historico (Linha, Data, Prefixo, Trem, Maquinista, Sequencia, Final_viagem, Mensagem) Values ('"
                            + frm.linha + "', '"
                            + frm.data_viagem.ToString("yyyy-MM-dd HH:mm:ss") + "', '"
                            + txtPrefixo.Text + "', '"
                            + cmbTrem.Text.ToUpper() + "', '"
                            + frm.maquinista_saida + "' ,"
                            + frm.sequencia.ToString() + ", '"
                            + final_viagem + "', '"
                            + mensagem_historico + "')";

                        frm.consulta(sql, "historico");
                        if (frm.status_consulta == true)
                            if (frm.maquinista_saida != "" && (groupBox1.Text == "Recolher" || frm.codigo_local_vira == 5))
                            {
                                frm.consulta("Insert into viras (codigo_local_vira, codigo_linha, hora_entrada, maquinista, sequencia) values "
                                    + "(5, " + cmbLinha.SelectedValue + ", '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + frm.maquinista_saida + "', 0)", "viras");
                                frm.maquinista_saida = "";
                            }

                    }
                }
            }
            catch (Exception e)
            {
                frm.timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo ao Recolher!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frm.consulta("Insert Into Logs (Log, data) Values ('Err CCT II recolher - " + e.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                frm.timer1.Enabled = true;
            }
    
        }

        private void executaAdicionar(string msg)
        {
            try
            {

                string sql = "Update Trens Set Trens.observacao = '', Trens.mensagem = '" + msg + "' where Trem = '" + cmbTrem.Text.ToUpper() + "'";

                frm.consulta(sql, "Trens");

                sql = "Update Andamento Set Sequencia = (Sequencia + 2) where Linha = " + cmbLinha.SelectedValue + " and Data = '" + frm.dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and (sequencia mod 2) = (" + frm.sequencia.ToString() + " mod 2) and sequencia > " + frm.sequencia.ToString();

                frm.consulta(sql, "Andamento");
                string destino;
                if (lbl_destino_diferenciado.Checked == true)
                    destino = "SIM";
                else
                    destino = "NÃO";

                sql = "INSERT INTO Andamento (Linha, Data, Prefixo, Id, Maquinista, Sequencia, Destino_Diferente) Values ('"
                    + cmbLinha.SelectedValue + "', '"
                    + frm.dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "', '"
                    + txtPrefixo.Text + "', '"
                    + cmbTrem.Text.ToUpper() + "', '"
                    + "' ,"
                    + (frm.sequencia + 2).ToString() + ", '"
                    + destino + "')";
                frm.consulta(sql, "andamento");

                frm.maquinista_saida = "";
                frm.maquinista_entrada = "";
                cmbTrem.Text = "";
                pnl_acao.Visible = false;
            }

            catch (Exception e)
            {
                frm.timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo em Adicionar!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frm.consulta("Insert Into Logs (Log, data) Values ('Err CCT II adicionar - " + e.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                frm.timer1.Enabled = true;
            }

        }

        public void verifica_mensagem(object sender, EventArgs e)
        {
            try
            {
                frm.odtAux = frm.consulta("select mensagem from trens where trem = '" + cmbTrem.Text + "'", "trens");
                if (frm.odtAux.Rows.Count > 0 && frm.odtAux.Rows[0][0].ToString() != "")
                {
                    frm.mensagem = frm.odtAux.Rows[0][0].ToString() + " ";
                    rdb_msg.Visible = true;
                    rdb_msg.Text = "Manter mensagem: " + frm.mensagem;
                    rdb_msg.Checked = true;

                }
                else
                {
                    frm.mensagem = "";
                    rdb_msg.Visible = false;
                    rdb_msg.Text = "";
                    rdb_msg.Checked = true;
                }
            }
            catch
            {
                frm.mensagem = "";
                rdb_msg.Visible = false;
                rdb_msg.Text = "";
                rdb_msg.Checked = true;
            }

        }

        private void verifica_mensagem(object sender, KeyPressEventArgs e)
        {

        }

        private void frm_transferencia_Load(object sender, EventArgs e)
        {
            cmbLinha.DisplayMember = "linha";
            cmbLinha.ValueMember = "codigo";
            cmbLinha.DataSource = frm.consulta("Select * From linha where codigo <> "+frm.linha+"", "linha");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            frm.atualiza();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbLinha_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int define_sentido;
                Info info = (Info)frm.sendoMovido_panel.Tag;
                cmbTrem.Text = info.trem;
                frm.maquinista_saida = info.maquinista;
                frm.data_viagem = info.data_viagem;
                string maquinista = info.maquinista;
                frm.codigo_local_vira = 5;

                try
                {
                    define_sentido = int.Parse(info.prefixo) % 2;
                }
                catch
                {
                    MessageBox.Show("Não foi possível identificar o sentido de circulação!", "Sistema de Controle de Viagens");
                    return;
                }

                frm.odtPar = frm.consulta("Select max(sequencia) From Andamento Where Linha = " + cmbLinha.SelectedValue + " and Data = '" + frm.dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and (Sequencia mod 2) = " + define_sentido, "Andamento");
                if (frm.odtPar.Rows[0][0].ToString() == "")
                    frm.odtPar = frm.consulta("Select max(sequencia) From historico Where Linha = " + cmbLinha.SelectedValue + " and Data = '" + frm.dia.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and (Sequencia mod 2) = " + define_sentido, "historico");
                
                try
                {
                    txtPrefixo.Text = info.prefixo;
                }
                catch
                {
                    txtPrefixo.Text = "00" + define_sentido.ToString();
                }
          
                try
                {
                    frm.sequencia = (int.Parse(frm.odtPar.Rows[0][0].ToString()));
                }
                catch
                {
                    frm.sequencia = define_sentido;
                }

                frm.sendoMovido_panel.BackColor = Color.Green;
                txtPrefixo.Enabled = true;
                cmbTrem.Enabled = false;

            }
            catch (Exception ex)
            {
                frm.timer1.Enabled = false;
                //MessageBox.Show("Err CCT IIo na conexão codigo 06!", "Sistema de Controle de Viagens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frm.consulta("Insert Into Logs (Log, data) Values ('Err CCT II prefixo impar Dbclik - " + ex.Message + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "Log");
                frm.timer1.Enabled = true;
            }
        }

        private void pnl_acao_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
