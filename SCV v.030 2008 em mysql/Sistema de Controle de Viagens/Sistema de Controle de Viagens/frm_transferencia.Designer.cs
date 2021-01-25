namespace Sistema_de_Controle_de_Viagens
{
    partial class frm_transferencia
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnl_acao = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_destino_diferenciado = new System.Windows.Forms.CheckBox();
            this.rdb_msg = new System.Windows.Forms.CheckBox();
            this.cmbLinha = new System.Windows.Forms.ComboBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.lblLocal = new System.Windows.Forms.Label();
            this.lblTrem = new System.Windows.Forms.Label();
            this.cmbTrem = new System.Windows.Forms.ComboBox();
            this.txtPrefixo = new System.Windows.Forms.TextBox();
            this.lbl_prefixo = new System.Windows.Forms.Label();
            this.pnl_acao.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_acao
            // 
            this.pnl_acao.BackColor = System.Drawing.Color.Teal;
            this.pnl_acao.Controls.Add(this.groupBox1);
            this.pnl_acao.Location = new System.Drawing.Point(0, 1);
            this.pnl_acao.Name = "pnl_acao";
            this.pnl_acao.Size = new System.Drawing.Size(325, 387);
            this.pnl_acao.TabIndex = 8;
            this.pnl_acao.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_acao_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.lbl_destino_diferenciado);
            this.groupBox1.Controls.Add(this.rdb_msg);
            this.groupBox1.Controls.Add(this.cmbLinha);
            this.groupBox1.Controls.Add(this.btnCancelar);
            this.groupBox1.Controls.Add(this.btnGravar);
            this.groupBox1.Controls.Add(this.lblLocal);
            this.groupBox1.Controls.Add(this.lblTrem);
            this.groupBox1.Controls.Add(this.cmbTrem);
            this.groupBox1.Controls.Add(this.txtPrefixo);
            this.groupBox1.Controls.Add(this.lbl_prefixo);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(319, 363);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Alterar Linha";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lbl_destino_diferenciado
            // 
            this.lbl_destino_diferenciado.Location = new System.Drawing.Point(10, 266);
            this.lbl_destino_diferenciado.Name = "lbl_destino_diferenciado";
            this.lbl_destino_diferenciado.Size = new System.Drawing.Size(298, 33);
            this.lbl_destino_diferenciado.TabIndex = 5;
            this.lbl_destino_diferenciado.Text = "Destino Diferenciado";
            this.lbl_destino_diferenciado.UseVisualStyleBackColor = true;
            // 
            // rdb_msg
            // 
            this.rdb_msg.Location = new System.Drawing.Point(10, 176);
            this.rdb_msg.Name = "rdb_msg";
            this.rdb_msg.Size = new System.Drawing.Size(298, 79);
            this.rdb_msg.TabIndex = 5;
            this.rdb_msg.Text = "Manter mensagem:";
            this.rdb_msg.UseVisualStyleBackColor = true;
            this.rdb_msg.Visible = false;
            // 
            // cmbLinha
            // 
            this.cmbLinha.FormattingEnabled = true;
            this.cmbLinha.Location = new System.Drawing.Point(90, 55);
            this.cmbLinha.Name = "cmbLinha";
            this.cmbLinha.Size = new System.Drawing.Size(218, 32);
            this.cmbLinha.TabIndex = 3;
            this.cmbLinha.SelectedIndexChanged += new System.EventHandler(this.cmbLinha_SelectedIndexChanged);
            // 
            // btnCancelar
            // 
            this.btnCancelar.AutoSize = true;
            this.btnCancelar.Location = new System.Drawing.Point(6, 323);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(95, 34);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGravar
            // 
            this.btnGravar.AutoSize = true;
            this.btnGravar.Location = new System.Drawing.Point(119, 323);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(75, 34);
            this.btnGravar.TabIndex = 6;
            this.btnGravar.Text = "OK";
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // lblLocal
            // 
            this.lblLocal.AutoSize = true;
            this.lblLocal.Location = new System.Drawing.Point(6, 55);
            this.lblLocal.Name = "lblLocal";
            this.lblLocal.Size = new System.Drawing.Size(56, 24);
            this.lblLocal.TabIndex = 4;
            this.lblLocal.Text = "Linha";
            // 
            // lblTrem
            // 
            this.lblTrem.AutoSize = true;
            this.lblTrem.Location = new System.Drawing.Point(178, 123);
            this.lblTrem.Name = "lblTrem";
            this.lblTrem.Size = new System.Drawing.Size(60, 24);
            this.lblTrem.TabIndex = 4;
            this.lblTrem.Text = "Trem:";
            // 
            // cmbTrem
            // 
            this.cmbTrem.FormattingEnabled = true;
            this.cmbTrem.Location = new System.Drawing.Point(244, 119);
            this.cmbTrem.MaxLength = 3;
            this.cmbTrem.Name = "cmbTrem";
            this.cmbTrem.Size = new System.Drawing.Size(64, 32);
            this.cmbTrem.TabIndex = 2;
            this.cmbTrem.VisibleChanged += new System.EventHandler(this.verifica_mensagem);
            this.cmbTrem.SelectedIndexChanged += new System.EventHandler(this.verifica_mensagem);
            this.cmbTrem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.verifica_mensagem);
            this.cmbTrem.DisplayMemberChanged += new System.EventHandler(this.verifica_mensagem);
            this.cmbTrem.TextChanged += new System.EventHandler(this.verifica_mensagem);
            // 
            // txtPrefixo
            // 
            this.txtPrefixo.Location = new System.Drawing.Point(90, 122);
            this.txtPrefixo.MaxLength = 3;
            this.txtPrefixo.Name = "txtPrefixo";
            this.txtPrefixo.Size = new System.Drawing.Size(65, 29);
            this.txtPrefixo.TabIndex = 1;
            this.txtPrefixo.Tag = "\"teste\"";
            // 
            // lbl_prefixo
            // 
            this.lbl_prefixo.AutoSize = true;
            this.lbl_prefixo.Location = new System.Drawing.Point(6, 124);
            this.lbl_prefixo.Name = "lbl_prefixo";
            this.lbl_prefixo.Size = new System.Drawing.Size(73, 24);
            this.lbl_prefixo.TabIndex = 1;
            this.lbl_prefixo.Text = "Prefixo:";
            // 
            // frm_transferencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 387);
            this.Controls.Add(this.pnl_acao);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_transferencia";
            this.Text = "Alterar Linha";
            this.Load += new System.EventHandler(this.frm_transferencia_Load);
            this.pnl_acao.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_acao;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTrem;
        private System.Windows.Forms.Label lbl_prefixo;
        private System.Windows.Forms.Label lblLocal;
        public System.Windows.Forms.CheckBox lbl_destino_diferenciado;
        public System.Windows.Forms.CheckBox rdb_msg;
        public System.Windows.Forms.Button btnCancelar;
        public System.Windows.Forms.Button btnGravar;
        public System.Windows.Forms.ComboBox cmbTrem;
        public System.Windows.Forms.TextBox txtPrefixo;
        public System.Windows.Forms.ComboBox cmbLinha;
    }
}