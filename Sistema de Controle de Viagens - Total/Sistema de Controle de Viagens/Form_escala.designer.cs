namespace Sistema_de_Controle_de_Viagens
{
    partial class Form_escala
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_escala));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnl_acao = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_obs = new System.Windows.Forms.TextBox();
            this.chkTv = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cmb_maquinista = new System.Windows.Forms.ComboBox();
            this.txt_data_hora_entrada = new System.Windows.Forms.MaskedTextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btn_Gravar = new System.Windows.Forms.Button();
            this.lbl_hora = new System.Windows.Forms.Label();
            this.lbl_obs = new System.Windows.Forms.Label();
            this.lbl_nome = new System.Windows.Forms.Label();
            this.cmbLinha = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPic = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbl_circulacao = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cms_panel_local = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.criarNovoLocalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txt_nome_editar = new System.Windows.Forms.TextBox();
            this.txt_matricula_editar = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_editar_mqt = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.txt_filtro = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbVira = new System.Windows.Forms.ComboBox();
            this.cms_maquinista = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.adicionarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarNomeDoLocalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excluirLocalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_remove_maquinista = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnl_acao.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.cms_panel_local.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.cms_maquinista.SuspendLayout();
            this.cms_remove_maquinista.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1209, 193);
            this.panel1.TabIndex = 0;
            this.panel1.Visible = false;
            // 
            // pnl_acao
            // 
            this.pnl_acao.BackColor = System.Drawing.Color.Teal;
            this.pnl_acao.Controls.Add(this.groupBox1);
            this.pnl_acao.Location = new System.Drawing.Point(398, 12);
            this.pnl_acao.Name = "pnl_acao";
            this.pnl_acao.Size = new System.Drawing.Size(378, 376);
            this.pnl_acao.TabIndex = 7;
            this.pnl_acao.Visible = false;
            this.pnl_acao.VisibleChanged += new System.EventHandler(this.pnl_acao_VisibleChanged);
            this.pnl_acao.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnl_acao_MouseMove);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.txt_obs);
            this.groupBox1.Controls.Add(this.chkTv);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.cmb_maquinista);
            this.groupBox1.Controls.Add(this.txt_data_hora_entrada);
            this.groupBox1.Controls.Add(this.btnCancelar);
            this.groupBox1.Controls.Add(this.btn_Gravar);
            this.groupBox1.Controls.Add(this.lbl_hora);
            this.groupBox1.Controls.Add(this.lbl_obs);
            this.groupBox1.Controls.Add(this.lbl_nome);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 351);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inserir Maquinista";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txt_obs
            // 
            this.txt_obs.Location = new System.Drawing.Point(7, 119);
            this.txt_obs.Name = "txt_obs";
            this.txt_obs.Size = new System.Drawing.Size(358, 29);
            this.txt_obs.TabIndex = 9;
            // 
            // chkTv
            // 
            this.chkTv.AutoSize = true;
            this.chkTv.Location = new System.Drawing.Point(6, 197);
            this.chkTv.Name = "chkTv";
            this.chkTv.Size = new System.Drawing.Size(137, 28);
            this.chkTv.TabIndex = 8;
            this.chkTv.Text = "Termina Vira";
            this.chkTv.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(7, 153);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(197, 28);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Destacar Maquinista";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // cmb_maquinista
            // 
            this.cmb_maquinista.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_maquinista.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_maquinista.FormattingEnabled = true;
            this.cmb_maquinista.Location = new System.Drawing.Point(7, 54);
            this.cmb_maquinista.Name = "cmb_maquinista";
            this.cmb_maquinista.Size = new System.Drawing.Size(358, 32);
            this.cmb_maquinista.TabIndex = 7;
            // 
            // txt_data_hora_entrada
            // 
            this.txt_data_hora_entrada.Location = new System.Drawing.Point(6, 266);
            this.txt_data_hora_entrada.Mask = "00/00/0000 90:00";
            this.txt_data_hora_entrada.Name = "txt_data_hora_entrada";
            this.txt_data_hora_entrada.Size = new System.Drawing.Size(144, 29);
            this.txt_data_hora_entrada.TabIndex = 6;
            this.txt_data_hora_entrada.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCancelar
            // 
            this.btnCancelar.AutoSize = true;
            this.btnCancelar.Location = new System.Drawing.Point(6, 311);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(95, 34);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btn_Gravar
            // 
            this.btn_Gravar.AutoSize = true;
            this.btn_Gravar.Location = new System.Drawing.Point(119, 311);
            this.btn_Gravar.Name = "btn_Gravar";
            this.btn_Gravar.Size = new System.Drawing.Size(75, 34);
            this.btn_Gravar.TabIndex = 4;
            this.btn_Gravar.Text = "OK";
            this.btn_Gravar.UseVisualStyleBackColor = true;
            this.btn_Gravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // lbl_hora
            // 
            this.lbl_hora.AutoSize = true;
            this.lbl_hora.Location = new System.Drawing.Point(2, 239);
            this.lbl_hora.Name = "lbl_hora";
            this.lbl_hora.Size = new System.Drawing.Size(75, 24);
            this.lbl_hora.TabIndex = 1;
            this.lbl_hora.Text = "Entrada";
            // 
            // lbl_obs
            // 
            this.lbl_obs.AutoSize = true;
            this.lbl_obs.Location = new System.Drawing.Point(6, 89);
            this.lbl_obs.Name = "lbl_obs";
            this.lbl_obs.Size = new System.Drawing.Size(112, 24);
            this.lbl_obs.TabIndex = 1;
            this.lbl_obs.Text = "Observação";
            this.lbl_obs.Click += new System.EventHandler(this.lbl_obs_Click);
            // 
            // lbl_nome
            // 
            this.lbl_nome.AutoSize = true;
            this.lbl_nome.Location = new System.Drawing.Point(2, 26);
            this.lbl_nome.Name = "lbl_nome";
            this.lbl_nome.Size = new System.Drawing.Size(149, 24);
            this.lbl_nome.TabIndex = 1;
            this.lbl_nome.Text = "Nome de guerra";
            this.lbl_nome.Click += new System.EventHandler(this.lbl_nome_Click);
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
            this.timer1.Interval = 30000;
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
            this.panel2.Size = new System.Drawing.Size(1209, 357);
            this.panel2.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDrop = true;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Location = new System.Drawing.Point(0, 43);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1210, 710);
            this.tabControl1.TabIndex = 9;
            this.tabControl1.DragEnter += new System.Windows.Forms.DragEventHandler(this.tabControl1_DragEnter);
            // 
            // tabPage1
            // 
            this.tabPage1.AllowDrop = true;
            this.tabPage1.BackColor = System.Drawing.Color.Black;
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1202, 684);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Circulação";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(0, 207);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1209, 183);
            this.panel3.TabIndex = 1;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Black;
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1202, 684);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Recolhidos";
            // 
            // tabPage3
            // 
            this.tabPage3.AllowDrop = true;
            this.tabPage3.BackColor = System.Drawing.Color.Black;
            this.tabPage3.Controls.Add(this.panel4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1202, 684);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Maquinistas";
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.ContextMenuStrip = this.cms_panel_local;
            this.panel4.Location = new System.Drawing.Point(0, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1209, 224);
            this.panel4.TabIndex = 12;
            // 
            // cms_panel_local
            // 
            this.cms_panel_local.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.criarNovoLocalToolStripMenuItem});
            this.cms_panel_local.Name = "cms_panel_local";
            this.cms_panel_local.Size = new System.Drawing.Size(161, 26);
            // 
            // criarNovoLocalToolStripMenuItem
            // 
            this.criarNovoLocalToolStripMenuItem.Name = "criarNovoLocalToolStripMenuItem";
            this.criarNovoLocalToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.criarNovoLocalToolStripMenuItem.Text = "Criar novo Local";
            this.criarNovoLocalToolStripMenuItem.Click += new System.EventHandler(this.criarNovoLocalToolStripMenuItem_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Black;
            this.tabPage4.Controls.Add(this.txt_nome_editar);
            this.tabPage4.Controls.Add(this.txt_matricula_editar);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.button1);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Controls.Add(this.btn_editar_mqt);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1202, 684);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Editar Maquinista";
            // 
            // txt_nome_editar
            // 
            this.txt_nome_editar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_nome_editar.Location = new System.Drawing.Point(8, 122);
            this.txt_nome_editar.Name = "txt_nome_editar";
            this.txt_nome_editar.Size = new System.Drawing.Size(201, 29);
            this.txt_nome_editar.TabIndex = 13;
            // 
            // txt_matricula_editar
            // 
            this.txt_matricula_editar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_matricula_editar.Location = new System.Drawing.Point(8, 44);
            this.txt_matricula_editar.Name = "txt_matricula_editar";
            this.txt_matricula_editar.Size = new System.Drawing.Size(201, 29);
            this.txt_matricula_editar.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(10, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "Novo Nome";
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(228, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 34);
            this.button1.TabIndex = 10;
            this.button1.Text = "Remover";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 24);
            this.label1.TabIndex = 11;
            this.label1.Text = "Matrícula";
            // 
            // btn_editar_mqt
            // 
            this.btn_editar_mqt.AutoSize = true;
            this.btn_editar_mqt.BackColor = System.Drawing.Color.White;
            this.btn_editar_mqt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_editar_mqt.Location = new System.Drawing.Point(11, 184);
            this.btn_editar_mqt.Name = "btn_editar_mqt";
            this.btn_editar_mqt.Size = new System.Drawing.Size(75, 34);
            this.btn_editar_mqt.TabIndex = 10;
            this.btn_editar_mqt.Text = "OK";
            this.btn_editar_mqt.UseVisualStyleBackColor = false;
            this.btn_editar_mqt.Click += new System.EventHandler(this.btn_editar_mqt_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.Black;
            this.tabPage5.Controls.Add(this.txt_filtro);
            this.tabPage5.Controls.Add(this.dateTimePicker1);
            this.tabPage5.Controls.Add(this.button2);
            this.tabPage5.Controls.Add(this.button3);
            this.tabPage5.Controls.Add(this.dataGridView1);
            this.tabPage5.Controls.Add(this.label4);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1202, 684);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Relatório";
            this.tabPage5.Click += new System.EventHandler(this.tabPage5_Click);
            this.tabPage5.Enter += new System.EventHandler(this.tabPage5_Enter);
            // 
            // txt_filtro
            // 
            this.txt_filtro.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txt_filtro.Location = new System.Drawing.Point(224, 6);
            this.txt_filtro.Name = "txt_filtro";
            this.txt_filtro.Size = new System.Drawing.Size(100, 29);
            this.txt_filtro.TabIndex = 18;
            this.txt_filtro.TextChanged += new System.EventHandler(this.txt_filtro_TextChanged);
            this.txt_filtro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(8, 6);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(138, 29);
            this.dateTimePicker1.TabIndex = 17;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.button2.Location = new System.Drawing.Point(111, 309);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 34);
            this.button2.TabIndex = 16;
            this.button2.Text = "Excel";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.AutoSize = true;
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.button3.Location = new System.Drawing.Point(8, 309);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 34);
            this.button3.TabIndex = 15;
            this.button3.Text = "Imprimir";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(7, 44);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1188, 252);
            this.dataGridView1.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(161, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 24);
            this.label4.TabIndex = 4;
            this.label4.Text = "Filtro:";
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.Black;
            this.tabPage6.Controls.Add(this.panel5);
            this.tabPage6.Controls.Add(this.label5);
            this.tabPage6.Controls.Add(this.cmbVira);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(1202, 684);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Viras";
            this.tabPage6.Enter += new System.EventHandler(this.tabPage6_Enter);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Black;
            this.panel5.Location = new System.Drawing.Point(-3, 48);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1209, 630);
            this.panel5.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(23, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 24);
            this.label5.TabIndex = 4;
            this.label5.Text = "Vira:";
            // 
            // cmbVira
            // 
            this.cmbVira.DisplayMember = "Código";
            this.cmbVira.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVira.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbVira.FormattingEnabled = true;
            this.cmbVira.Location = new System.Drawing.Point(86, 10);
            this.cmbVira.Name = "cmbVira";
            this.cmbVira.Size = new System.Drawing.Size(121, 32);
            this.cmbVira.TabIndex = 3;
            this.cmbVira.ValueMember = "Código";
            this.cmbVira.SelectedIndexChanged += new System.EventHandler(this.cmbVira_SelectedIndexChanged);
            this.cmbVira.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbLinha_KeyPress);
            // 
            // cms_maquinista
            // 
            this.cms_maquinista.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adicionarToolStripMenuItem,
            this.editarNomeDoLocalToolStripMenuItem,
            this.excluirLocalToolStripMenuItem});
            this.cms_maquinista.Name = "contextMenuStrip1";
            this.cms_maquinista.Size = new System.Drawing.Size(188, 70);
            this.cms_maquinista.Opening += new System.ComponentModel.CancelEventHandler(this.cms_maquinista_Opening);
            // 
            // adicionarToolStripMenuItem
            // 
            this.adicionarToolStripMenuItem.Name = "adicionarToolStripMenuItem";
            this.adicionarToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.adicionarToolStripMenuItem.Text = "Adicionar Maquinista";
            this.adicionarToolStripMenuItem.Click += new System.EventHandler(this.adicionarToolStripMenuItem_Click);
            // 
            // editarNomeDoLocalToolStripMenuItem
            // 
            this.editarNomeDoLocalToolStripMenuItem.Name = "editarNomeDoLocalToolStripMenuItem";
            this.editarNomeDoLocalToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.editarNomeDoLocalToolStripMenuItem.Text = "Editar nome do Local";
            this.editarNomeDoLocalToolStripMenuItem.Click += new System.EventHandler(this.editarNomeDoLocalToolStripMenuItem_Click);
            // 
            // excluirLocalToolStripMenuItem
            // 
            this.excluirLocalToolStripMenuItem.Name = "excluirLocalToolStripMenuItem";
            this.excluirLocalToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.excluirLocalToolStripMenuItem.Text = "Excluir Local";
            this.excluirLocalToolStripMenuItem.Click += new System.EventHandler(this.excluirLocalToolStripMenuItem_Click);
            // 
            // cms_remove_maquinista
            // 
            this.cms_remove_maquinista.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removerToolStripMenuItem,
            this.editarToolStripMenuItem});
            this.cms_remove_maquinista.Name = "cms_remove_maquinista";
            this.cms_remove_maquinista.Size = new System.Drawing.Size(122, 48);
            // 
            // removerToolStripMenuItem
            // 
            this.removerToolStripMenuItem.Name = "removerToolStripMenuItem";
            this.removerToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.removerToolStripMenuItem.Text = "Remover";
            this.removerToolStripMenuItem.Click += new System.EventHandler(this.removerToolStripMenuItem_Click_1);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.editarToolStripMenuItem.Text = "Editar";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            // 
            // Form_escala
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1239, 753);
            this.Controls.Add(this.lblPic);
            this.Controls.Add(this.lbl_circulacao);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnl_acao);
            this.Controls.Add(this.cmbLinha);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form_escala";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Sistema de Controle de Viagens Escala";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnl_acao.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.cms_panel_local.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.cms_maquinista.ResumeLayout(false);
            this.cms_remove_maquinista.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Gravar;
        private System.Windows.Forms.ComboBox cmbLinha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblPic;
        private System.Windows.Forms.Panel pnl_acao;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbl_circulacao;
        private System.Windows.Forms.Label lbl_nome;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.MaskedTextBox txt_data_hora_entrada;
        private System.Windows.Forms.Label lbl_hora;
        private System.Windows.Forms.ContextMenuStrip cms_maquinista;
        private System.Windows.Forms.ToolStripMenuItem adicionarToolStripMenuItem;
        private System.Windows.Forms.ComboBox cmb_maquinista;
        private System.Windows.Forms.ContextMenuStrip cms_remove_maquinista;
        private System.Windows.Forms.ToolStripMenuItem removerToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txt_obs;
        private System.Windows.Forms.Label lbl_obs;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkTv;
        private System.Windows.Forms.ToolStripMenuItem editarNomeDoLocalToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cms_panel_local;
        private System.Windows.Forms.ToolStripMenuItem criarNovoLocalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excluirLocalToolStripMenuItem;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txt_nome_editar;
        private System.Windows.Forms.TextBox txt_matricula_editar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_editar_mqt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_filtro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbVira;
        private System.Windows.Forms.Panel panel5;
                
        }
}

