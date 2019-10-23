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
            ((System.ComponentModel.ISupportInitialize)(this.dgvConstructionParts)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Construction Parts:";
            // 
            // dgvConstructionParts
            // 
            this.dgvConstructionParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConstructionParts.Location = new System.Drawing.Point(12, 25);
            this.dgvConstructionParts.Name = "dgvConstructionParts";
            this.dgvConstructionParts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConstructionParts.Size = new System.Drawing.Size(685, 150);
            this.dgvConstructionParts.TabIndex = 1;
            this.dgvConstructionParts.SelectionChanged += new System.EventHandler(this.dgvConstructionParts_SelectionChanged);
            // 
            // FrmConstructionPartExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 462);
            this.Controls.Add(this.dgvConstructionParts);
            this.Controls.Add(this.label1);
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
    }
}