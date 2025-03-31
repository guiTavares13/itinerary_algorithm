namespace poc_recommended_trip
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnListDestinations = new Button();
            lstDestinos = new ListBox();
            txtDestination = new TextBox();
            txtDays = new TextBox();
            btnKmeans = new Button();
            lbRoteiro = new ListBox();
            label1 = new Label();
            label2 = new Label();
            groupBox1 = new GroupBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnListDestinations
            // 
            btnListDestinations.Location = new Point(530, 267);
            btnListDestinations.Name = "btnListDestinations";
            btnListDestinations.Size = new Size(224, 36);
            btnListDestinations.TabIndex = 0;
            btnListDestinations.Text = "Consultar Lista de Destinos Disponiveis";
            btnListDestinations.UseVisualStyleBackColor = true;
            btnListDestinations.Click += button1_Click;
            // 
            // lstDestinos
            // 
            lstDestinos.FormattingEnabled = true;
            lstDestinos.ItemHeight = 15;
            lstDestinos.Items.AddRange(new object[] { "Clique em \"Consultar Lista de Destinos Disponiveis\" para visualizar os destinos cadastrados" });
            lstDestinos.Location = new Point(12, 12);
            lstDestinos.Name = "lstDestinos";
            lstDestinos.Size = new Size(742, 244);
            lstDestinos.TabIndex = 1;
            // 
            // txtDestination
            // 
            txtDestination.Location = new Point(13, 58);
            txtDestination.Name = "txtDestination";
            txtDestination.Size = new Size(143, 23);
            txtDestination.TabIndex = 4;
            // 
            // txtDays
            // 
            txtDays.Location = new Point(13, 128);
            txtDays.Name = "txtDays";
            txtDays.Size = new Size(143, 23);
            txtDays.TabIndex = 5;
            // 
            // btnKmeans
            // 
            btnKmeans.Location = new Point(16, 256);
            btnKmeans.Name = "btnKmeans";
            btnKmeans.Size = new Size(140, 55);
            btnKmeans.TabIndex = 6;
            btnKmeans.Text = "Gerar Roteiro";
            btnKmeans.UseVisualStyleBackColor = true;
            btnKmeans.Click += btnKmeans_Click;
            // 
            // lbRoteiro
            // 
            lbRoteiro.FormattingEnabled = true;
            lbRoteiro.ItemHeight = 15;
            lbRoteiro.Location = new Point(183, 22);
            lbRoteiro.Name = "lbRoteiro";
            lbRoteiro.Size = new Size(547, 289);
            lbRoteiro.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 31);
            label1.Name = "label1";
            label1.Size = new Size(131, 15);
            label1.TabIndex = 8;
            label1.Text = "Qual pais deseja visitar?";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 110);
            label2.Name = "label2";
            label2.Size = new Size(143, 15);
            label2.TabIndex = 9;
            label2.Text = "Quantos dias deseja ficar?";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(lbRoteiro);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnKmeans);
            groupBox1.Controls.Add(txtDestination);
            groupBox1.Controls.Add(txtDays);
            groupBox1.Location = new Point(12, 311);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(742, 321);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Calcula Roteiro";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(764, 644);
            Controls.Add(groupBox1);
            Controls.Add(lstDestinos);
            Controls.Add(btnListDestinations);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnListDestinations;
        private ListBox lstDestinos;
        private TextBox txtDestination;
        private TextBox txtDays;
        private Button btnKmeans;
        private ListBox lbRoteiro;
        private Label label1;
        private Label label2;
        private GroupBox groupBox1;
    }
}
