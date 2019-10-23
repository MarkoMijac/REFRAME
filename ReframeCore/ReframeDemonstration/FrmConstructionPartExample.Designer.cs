namespace ReframeDemonstration
{
    partial class FrmConstructionPartExample
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvConstructionParts = new System.Windows.Forms.DataGridView();
            this.btnAddConstructionPart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConstructionParts)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Construction Parts:";
            // 
            // dgvConstructionParts
            // 
            this.dgvConstructionParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConstructionParts.Location = new System.Drawing.Point(16, 31);
            this.dgvConstructionParts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvConstructionParts.Name = "dgvConstructionParts";
            this.dgvConstructionParts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConstructionParts.Size = new System.Drawing.Size(913, 374);
            this.dgvConstructionParts.TabIndex = 1;
            this.dgvConstructionParts.SelectionChanged += new System.EventHandler(this.dgvConstructionParts_SelectionChanged);
            // 
            // btnAddConstructionPart
            // 
            this.btnAddConstructionPart.Location = new System.Drawing.Point(12, 412);
            this.btnAddConstructionPart.Name = "btnAddConstructionPart";
            this.btnAddConstructionPart.Size = new System.Drawing.Size(171, 31);
            this.btnAddConstructionPart.TabIndex = 2;
            this.btnAddConstructionPart.Text = "Add Construction Part";
            this.btnAddConstructionPart.UseVisualStyleBackColor = true;
            this.btnAddConstructionPart.Click += new System.EventHandler(this.btnAddConstructionPart_Click);
            // 
            // FrmConstructionPartExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 569);
            this.Controls.Add(this.btnAddConstructionPart);
            this.Controls.Add(this.dgvConstructionParts);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmConstructionPartExample";
            this.Text = "FrmConstructionPartExample";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmConstructionPartExample_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConstructionParts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvConstructionParts;
        private System.Windows.Forms.Button btnAddConstructionPart;
    }
}