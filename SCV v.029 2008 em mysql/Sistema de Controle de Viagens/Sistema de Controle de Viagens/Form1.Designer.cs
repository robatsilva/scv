namespace Sistema_de_Controle_de_Viagens
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnl_acao = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_destino_diferenciado = new System.Windows.Forms.CheckBox();
            this.rdb_msg = new System.Windows.Forms.CheckBox();
            this.cmb_local = new System.Windows.Forms.ComboBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.lblLocal = new System.Windows.Forms.Label();
            this.lblTrem = new System.Windows.Forms.Label();
            this.cmbTrem = new System.Windows.Forms.ComboBox();
            this.txt_obs = new System.Windows.Forms.TextBox();
            this.txtPrefixo = new System.Windows.Forms.TextBox();
            this.lbl_obs = new System.Windows.Forms.Label();
            this.lbl_prefixo = new System.Windows.Forms.Label();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.recolherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excluirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbLinha = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPic = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbl_circulacao = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cms_recolhidos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.alterarLocalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnl_acao.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.cmsMenu.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.cms_recolhidos.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1209, 357);
            this.panel1.TabIndex = 0;
            this.panel1.Visible = false;
            // 
            // pnl_acao
            // 
            this.pnl_acao.BackColor = System.Drawing.Color.Teal;
            this.pnl_acao.Controls.Add(this.groupBox1);
            this.pnl_acao.Location = new System.Drawing.Point(441, 12);
            this.pnl_acao.Name = "pnl_acao";
            this.pnl_acao.Size = new System.Drawing.Size(325, 388);
            this.pnl_acao.TabIndex = 7;
            this.pnl_acao.Visible = false;
            this.pnl_acao.VisibleChanged += new System.EventHandler(this.pnl_acao_VisibleChanged);
            this.pnl_acao.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnl_acao_MouseMove);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.lbl_destino_diferenciado);
            this.groupBox1.Controls.Add(this.rdb_msg);
            this.groupBox1.Controls.Add(this.cmb_local);
            this.groupBox1.Controls.Add(this.btnCancelar);
            this.groupBox1.Controls.Add(this.btnGravar);
            this.groupBox1.Controls.Add(this.lblLocal);
            this.groupBox1.Controls.Add(this.lblTrem);
            this.groupBox1.Controls.Add(this.cmbTrem);
            this.groupBox1.Controls.Add(this.txt_obs);
            this.groupBox1.Controls.Add(this.txtPrefixo);
            this.groupBox1.Controls.Add(this.lbl_obs);
            this.groupBox1.Controls.Add(this.lbl_prefixo);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(319, 363);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selecione uma ação";
            // 
            // lbl_destino_diferenciado
            // 
            this.lbl_destino_diferenciado.Location = new System.Drawing.Point(10, 266);
            this.lbl_destino_diferenciado.Name = "lbl_destino_diferenciado";
            this.lbl_destino_diferenciado.Size = new System.Drawing.Size(298, 33);
            this.lbl_destino_diferenciado.TabIndex = 5;
            this.lbl_destino_diferenciado.Text = "Destino Diferenciado";
            this.lbl_destino_diferenciado.UseVisualStyleBackColor = true;
            this.lbl_destino_diferenciado.Visible = false;
            this.lbl_destino_diferenciado.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // rdb_msg
            // 
            this.rdb_msg.Location = new System.Drawing.Point(10, 192);
            this.rdb_msg.Name = "rdb_msg";
            this.rdb_msg.Size = new System.Drawing.Size(298, 79);
            this.rdb_msg.TabIndex = 5;
            this.rdb_msg.Text = "Manter mensagem:";
            this.rdb_msg.UseVisualStyleBackColor = true;
            this.rdb_msg.Visible = false;
            // 
            // cmb_local
            // 
            this.cmb_local.FormattingEnabled = true;
            this.cmb_local.Location = new System.Drawing.Point(90, 99);
            this.cmb_local.Name = "cmb_local";
            this.cmb_local.Size = new System.Drawing.Size(218, 32);
            this.cmb_local.TabIndex = 3;
            this.cmb_local.SelectedIndexChanged += new System.EventHandler(this.cmb_local_SelectedIndexChanged);
            this.cmb_local.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmb_local_KeyPress);
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
            this.lblLocal.Location = new System.Drawing.Point(6, 99);
            this.lblLocal.Name = "lblLocal";
            this.lblLocal.Size = new System.Drawing.Size(60, 24);
            this.lblLocal.TabIndex = 4;
            this.lblLocal.Text = "Local:";
            // 
            // lblTrem
            // 
            this.lblTrem.AutoSize = true;
            this.lblTrem.Location = new System.Drawing.Point(178, 44);
            this.lblTrem.Name = "lblTrem";
            this.lblTrem.Size = new System.Drawing.Size(60, 24);
            this.lblTrem.TabIndex = 4;
            this.lblTrem.Text = "Trem:";
            // 
            // cmbTrem
            // 
            this.cmbTrem.FormattingEnabled = true;
            this.cmbTrem.Location = new System.Drawing.Point(244, 40);
            this.cmbTrem.MaxLength = 3;
            this.cmbTrem.Name = "cmbTrem";
            this.cmbTrem.Size = new System.Drawing.Size(64, 32);
            this.cmbTrem.TabIndex = 2;
            this.cmbTrem.VisibleChanged += new System.EventHandler(this.verifica_mensagem);
            this.cmbTrem.SelectedIndexChanged += new System.EventHandler(this.verifica_mensagem);
            this.cmbTrem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbTrem_KeyPress);
            this.cmbTrem.DisplayMemberChanged += new System.EventHandler(this.verifica_mensagem);
            this.cmbTrem.TextChanged += new System.EventHandler(this.verifica_mensagem);
            // 
            // txt_obs
            // 
            this.txt_obs.Location = new System.Drawing.Point(90, 157);
            this.txt_obs.MaxLength = 0;
            this.txt_obs.Name = "txt_obs";
            this.txt_obs.Size = new System.Drawing.Size(218, 29);
            this.txt_obs.TabIndex = 4;
            this.txt_obs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_obs_KeyPress);
            // 
            // txtPrefixo
            // 
            this.txtPrefixo.Location = new System.Drawing.Point(90, 43);
            this.txtPrefixo.MaxLength = 3;
            this.txtPrefixo.Name = "txtPrefixo";
            this.txtPrefixo.Size = new System.Drawing.Size(65, 29);
            this.txtPrefixo.TabIndex = 1;
            this.txtPrefixo.Tag = "\"teste\"";
            this.txtPrefixo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrefixo_KeyPress);
            // 
            // lbl_obs
            // 
            this.lbl_obs.AutoSize = true;
            this.lbl_obs.Location = new System.Drawing.Point(6, 159);
            this.lbl_obs.Name = "lbl_obs";
            this.lbl_obs.Size = new System.Drawing.Size(50, 24);
            this.lbl_obs.TabIndex = 1;
            this.lbl_obs.Text = "Obs:";
            // 
            // lbl_prefixo
            // 
            this.lbl_prefixo.AutoSize = true;
            this.lbl_prefixo.Location = new System.Drawing.Point(6, 45);
            this.lbl_prefixo.Name = "lbl_prefixo";
            this.lbl_prefixo.Size = new System.Drawing.Size(73, 24);
            this.lbl_prefixo.TabIndex = 1;
            this.lbl_prefixo.Text = "Prefixo:";
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recolherToolStripMenuItem,
            this.editarToolStripMenuItem,
            this.excluirToolStripMenuItem});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(121, 70);
            // 
            // recolherToolStripMenuItem
            // 
            this.recolherToolStripMenuItem.Name = "recolherToolStripMenuItem";
            this.recolherToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.recolherToolStripMenuItem.Text = "Recolher";
            this.recolherToolStripMenuItem.Click += new System.EventHandler(this.recolherToolStripMenuItem_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.editarToolStripMenuItem.Text = "Editar";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            // 
            // excluirToolStripMenuItem
            // 
            this.excluirToolStripMenuItem.Name = "excluirToolStripMenuItem";
            this.excluirToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.excluirToolStripMenuItem.Text = "Excluir";
            this.excluirToolStripMenuItem.Click += new System.EventHandler(this.excluirToolStripMenuItem_Click);
            // 
            // cmbLinha
            // 
            this.cmbLinha.DisplayMember = "Código";
            this.cmbLinha.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLinha.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLinha.FormattingEnabled = true;
            this.cmbLinha.Location = new System.Drawing.Point(90, 5);
            this.cmbLinha.Name = "cmbLinha";
            this.cmbLinha.Size = new System.Drawing.Size(121, 32);
            this.cmbLinha.TabIndex = 3;
            this.cmbLinha.ValueMember = "Código";
            this.cmbLinha.SelectedIndexChanged += new System.EventHandler(this.cmbLinha_SelectedIndexChanged);
            this.cmbLinha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbLinha_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(27, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Linha:";
            // 
            // lblPic
            // 
            this.lblPic.AllowDrop = true;
            this.lblPic.Image = ((System.Drawing.Image)(resources.GetObject("lblPic.Image")));
            this.lblPic.Location = new System.Drawing.Point(979, 445);
            this.lblPic.Name = "lblPic";
            this.lblPic.Size = new System.Drawing.Size(32, 103);
            this.lblPic.TabIndex = 6;
            this.lblPic.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbl_circulacao
            // 
            this.lbl_circulacao.AutoSize = true;
            this.lbl_circulacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_circulacao.ForeColor = System.Drawing.Color.White;
            this.lbl_circulacao.Location = new System.Drawing.Point(228, 9);
            this.lbl_circulacao.Name = "lbl_circulacao";
            this.lbl_circulacao.Size = new System.Drawing.Size(106, 24);
            this.lbl_circulacao.TabIndex = 4;
            this.lbl_circulacao.Text = "Circulando:";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1209, 363);
            this.panel2.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 43);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1210, 390);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Black;
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1202, 364);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Circulação";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Black;
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1202, 364);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Recolhidos";
            // 
            // cms_recolhidos
            // 
            this.cms_recolhidos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alterarLocalToolStripMenuItem});
            this.cms_recolhidos.Name = "cms_recolhidos";
            this.cms_recolhidos.Size = new System.Drawing.Size(141, 26);
            // 
            // alterarLocalToolStripMenuItem
            // 
            this.alterarLocalToolStripMenuItem.Name = "alterarLocalToolStripMenuItem";
            this.alterarLocalToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.alterarLocalToolStripMenuItem.Text = "Alterar Local";
            this.alterarLocalToolStripMenuItem.Click += new System.EventHandler(this.alterarLocalToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1276, 434);
            this.Controls.Add(this.pnl_acao);
            this.Controls.Add(this.lblPic);
            this.Controls.Add(this.lbl_circulacao);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbLinha);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Sistema de Controle de Viagens";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.pnl_acao.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.cmsMenu.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.cms_recolhidos.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_prefixo;
        private System.Windows.Forms.Label lblTrem;
        private System.Windows.Forms.ComboBox cmbTrem;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem recolherToolStripMenuItem;
        private System.Windows.Forms.ComboBox cmbLinha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblPic;
        private System.Windows.Forms.Label lblLocal;
        private System.Windows.Forms.TextBox txtPrefixo;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.Panel pnl_acao;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbl_circulacao;
        private System.Windows.Forms.ComboBox cmb_local;
        private System.Windows.Forms.TextBox txt_obs;
        private System.Windows.Forms.Label lbl_obs;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox rdb_msg;
        private System.Windows.Forms.ToolStripMenuItem excluirToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cms_recolhidos;
        private System.Windows.Forms.ToolStripMenuItem alterarLocalToolStripMenuItem;
        private System.Windows.Forms.CheckBox lbl_destino_diferenciado;
                
        }
}

