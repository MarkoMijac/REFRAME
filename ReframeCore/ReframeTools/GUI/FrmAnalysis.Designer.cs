namespace ReframeTools.GUI
{
    partial class FrmAnalysis
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpAnalysis = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvAnalysis = new System.Windows.Forms.DataGridView();
            this.tpSourceXML = new System.Windows.Forms.TabPage();
            this.rtxtXMLSource = new System.Windows.Forms.RichTextBox();
            this.lblAnalysisDescription = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tpAnalysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnalysis)).BeginInit();
            this.tpSourceXML.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tpAnalysis);
            this.tabControl.Controls.Add(this.tpSourceXML);
            this.tabControl.Location = new System.Drawing.Point(12, 41);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(609, 444);
            this.tabControl.TabIndex = 0;
            // 
            // tpAnalysis
            // 
            this.tpAnalysis.Controls.Add(this.label1);
            this.tpAnalysis.Controls.Add(this.dgvAnalysis);
            this.tpAnalysis.Location = new System.Drawing.Point(4, 22);
            this.tpAnalysis.Name = "tpAnalysis";
            this.tpAnalysis.Padding = new System.Windows.Forms.Padding(3);
            this.tpAnalysis.Size = new System.Drawing.Size(601, 418);
            this.tpAnalysis.TabIndex = 1;
            this.tpAnalysis.Text = "Table view";
            this.tpAnalysis.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Involved nodes:";
            // 
            // dgvAnalysis
            // 
            this.dgvAnalysis.AllowUserToAddRows = false;
            this.dgvAnalysis.AllowUserToDeleteRows = false;
            this.dgvAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAnalysis.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAnalysis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnalysis.Location = new System.Drawing.Point(6, 19);
            this.dgvAnalysis.Name = "dgvAnalysis";
            this.dgvAnalysis.RowHeadersVisible = false;
            this.dgvAnalysis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAnalysis.Size = new System.Drawing.Size(589, 393);
            this.dgvAnalysis.TabIndex = 0;
            // 
            // tpSourceXML
            // 
            this.tpSourceXML.Controls.Add(this.rtxtXMLSource);
            this.tpSourceXML.Location = new System.Drawing.Point(4, 22);
            this.tpSourceXML.Name = "tpSourceXML";
            this.tpSourceXML.Padding = new System.Windows.Forms.Padding(3);
            this.tpSourceXML.Size = new System.Drawing.Size(601, 418);
            this.tpSourceXML.TabIndex = 0;
            this.tpSourceXML.Text = "XML Source";
            this.tpSourceXML.UseVisualStyleBackColor = true;
            // 
            // rtxtXMLSource
            // 
            this.rtxtXMLSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtXMLSource.Location = new System.Drawing.Point(3, 3);
            this.rtxtXMLSource.Name = "rtxtXMLSource";
            this.rtxtXMLSource.Size = new System.Drawing.Size(595, 412);
            this.rtxtXMLSource.TabIndex = 0;
            this.rtxtXMLSource.Text = "";
            // 
            // lblAnalysisDescription
            // 
            this.lblAnalysisDescription.AutoSize = true;
            this.lblAnalysisDescription.Location = new System.Drawing.Point(12, 9);
            this.lblAnalysisDescription.Name = "lblAnalysisDescription";
            this.lblAnalysisDescription.Size = new System.Drawing.Size(35, 13);
            this.lblAnalysisDescription.TabIndex = 1;
            this.lblAnalysisDescription.Text = "label2";
            // 
            // FrmAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 497);
            this.Controls.Add(this.lblAnalysisDescription);
            this.Controls.Add(this.tabControl);
            this.Name = "FrmAnalysis";
            this.ShowInTaskbar = false;
            this.Text = "Class analysis";
            this.Load += new System.EventHandler(this.FrmClassAnalysis_Load);
            this.tabControl.ResumeLayout(false);
            this.tpAnalysis.ResumeLayout(false);
            this.tpAnalysis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnalysis)).EndInit();
            this.tpSourceXML.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpSourceXML;
        private System.Windows.Forms.TabPage tpAnalysis;
        private System.Windows.Forms.RichTextBox rtxtXMLSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAnalysisDescription;
        public System.Windows.Forms.DataGridView dgvAnalysis;
    }
}