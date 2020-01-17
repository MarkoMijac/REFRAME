namespace ReframeTools.GUI
{
    partial class FrmReactors
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
            this.dgvReactors = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReactors)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvReactors
            // 
            this.dgvReactors.AllowUserToAddRows = false;
            this.dgvReactors.AllowUserToDeleteRows = false;
            this.dgvReactors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReactors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReactors.Location = new System.Drawing.Point(12, 12);
            this.dgvReactors.Name = "dgvReactors";
            this.dgvReactors.RowHeadersVisible = false;
            this.dgvReactors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReactors.Size = new System.Drawing.Size(541, 191);
            this.dgvReactors.TabIndex = 0;
            // 
            // FrmReactors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 217);
            this.Controls.Add(this.dgvReactors);
            this.Name = "FrmReactors";
            this.ShowInTaskbar = false;
            this.Text = "Registered reactors";
            this.Load += new System.EventHandler(this.FrmReactors_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReactors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReactors;
    }
}