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
            this.btnShowLayers = new System.Windows.Forms.Button();
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
            this.dgvConstructionParts.AllowUserToAddRows = false;
            this.dgvConstructionParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConstructionParts.Location = new System.Drawing.Point(12, 25);
            this.dgvConstructionParts.Name = "dgvConstructionParts";
            this.dgvConstructionParts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConstructionParts.Size = new System.Drawing.Size(685, 274);
            this.dgvConstructionParts.TabIndex = 1;
            this.dgvConstructionParts.SelectionChanged += new System.EventHandler(this.dgvConstructionParts_SelectionChanged);
            // 
            // btnAddConstructionPart
            // 
            this.btnAddConstructionPart.Location = new System.Drawing.Point(12, 304);
            this.btnAddConstructionPart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddConstructionPart.Name = "btnAddConstructionPart";
            this.btnAddConstructionPart.Size = new System.Drawing.Size(128, 25);
            this.btnAddConstructionPart.TabIndex = 2;
            this.btnAddConstructionPart.Text = "Add Construction Part";
            this.btnAddConstructionPart.UseVisualStyleBackColor = true;
            this.btnAddConstructionPart.Click += new System.EventHandler(this.btnAddConstructionPart_Click);
            // 
            // btnShowLayers
            // 
            this.btnShowLayers.Location = new System.Drawing.Point(148, 304);
            this.btnShowLayers.Margin = new System.Windows.Forms.Padding(2);
            this.btnShowLayers.Name = "btnShowLayers";
            this.btnShowLayers.Size = new System.Drawing.Size(128, 25);
            this.btnShowLayers.TabIndex = 5;
            this.btnShowLayers.Text = "Show layers";
            this.btnShowLayers.UseVisualStyleBackColor = true;
            this.btnShowLayers.Click += new System.EventHandler(this.btnShowLayers_Click);
            // 
            // FrmConstructionPartExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 476);
            this.Controls.Add(this.btnShowLayers);
            this.Controls.Add(this.btnAddConstructionPart);
            this.Controls.Add(this.dgvConstructionParts);
            this.Controls.Add(this.label1);
            this.Name = "FrmConstructionPartExample";
            this.Text = "FrmConstructionPartExample";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.FrmConstructionPartExample_Activated);
            this.Deactivate += new System.EventHandler(this.FrmConstructionPartExample_Deactivate);
            this.Load += new System.EventHandler(this.FrmConstructionPartExample_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConstructionParts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvConstructionParts;
        private System.Windows.Forms.Button btnAddConstructionPart;
        private System.Windows.Forms.Button btnShowLayers;
    }
}