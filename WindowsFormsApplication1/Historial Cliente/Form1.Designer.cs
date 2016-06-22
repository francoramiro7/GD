namespace WindowsFormsApplication1.Historial_Cliente
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.botonUltimaPagina = new System.Windows.Forms.Button();
            this.botonPaginaSiguiente = new System.Windows.Forms.Button();
            this.botonPaginaAnterior = new System.Windows.Forms.Button();
            this.botonPrimeraPagina = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.MediumAquamarine;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 160);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(860, 282);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // botonUltimaPagina
            // 
            this.botonUltimaPagina.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonUltimaPagina.Location = new System.Drawing.Point(797, 448);
            this.botonUltimaPagina.Name = "botonUltimaPagina";
            this.botonUltimaPagina.Size = new System.Drawing.Size(67, 25);
            this.botonUltimaPagina.TabIndex = 11;
            this.botonUltimaPagina.Text = "Ultima";
            this.botonUltimaPagina.UseVisualStyleBackColor = true;
            this.botonUltimaPagina.Click += new System.EventHandler(this.botonUltimaPagina_Click);
            // 
            // botonPaginaSiguiente
            // 
            this.botonPaginaSiguiente.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonPaginaSiguiente.Location = new System.Drawing.Point(725, 448);
            this.botonPaginaSiguiente.Name = "botonPaginaSiguiente";
            this.botonPaginaSiguiente.Size = new System.Drawing.Size(66, 25);
            this.botonPaginaSiguiente.TabIndex = 10;
            this.botonPaginaSiguiente.Text = "Siguiente";
            this.botonPaginaSiguiente.UseVisualStyleBackColor = true;
            this.botonPaginaSiguiente.Click += new System.EventHandler(this.botonPaginaSiguiente_Click);
            // 
            // botonPaginaAnterior
            // 
            this.botonPaginaAnterior.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonPaginaAnterior.Location = new System.Drawing.Point(654, 448);
            this.botonPaginaAnterior.Name = "botonPaginaAnterior";
            this.botonPaginaAnterior.Size = new System.Drawing.Size(65, 25);
            this.botonPaginaAnterior.TabIndex = 9;
            this.botonPaginaAnterior.Text = "Anterior";
            this.botonPaginaAnterior.UseVisualStyleBackColor = true;
            this.botonPaginaAnterior.Click += new System.EventHandler(this.botonPaginaAnterior_Click);
            // 
            // botonPrimeraPagina
            // 
            this.botonPrimeraPagina.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonPrimeraPagina.Location = new System.Drawing.Point(584, 447);
            this.botonPrimeraPagina.Name = "botonPrimeraPagina";
            this.botonPrimeraPagina.Size = new System.Drawing.Size(64, 26);
            this.botonPrimeraPagina.TabIndex = 8;
            this.botonPrimeraPagina.Text = "Primera";
            this.botonPrimeraPagina.UseVisualStyleBackColor = true;
            this.botonPrimeraPagina.Click += new System.EventHandler(this.botonPrimeraPagina_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 448);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 26);
            this.button1.TabIndex = 12;
            this.button1.Text = "Atrás";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(424, 448);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumAquamarine;
            this.ClientSize = new System.Drawing.Size(884, 482);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.botonUltimaPagina);
            this.Controls.Add(this.botonPaginaSiguiente);
            this.Controls.Add(this.botonPaginaAnterior);
            this.Controls.Add(this.botonPrimeraPagina);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Historial de Cliente";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button botonUltimaPagina;
        private System.Windows.Forms.Button botonPaginaSiguiente;
        private System.Windows.Forms.Button botonPaginaAnterior;
        private System.Windows.Forms.Button botonPrimeraPagina;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}