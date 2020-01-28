namespace ReframeTools.GUI
{
    partial class FrmFilterClassAnalysis
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
            this.gbAnalysisLevel.SuspendLayout();
            this.SuspendLayout();
            // 
            // clbNamespaceNodes
            // 
            this.clbNamespaceNodes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbNamespaceNodes_ItemCheck);
            // 
            // clbAssemblyNodes
            // 
            this.clbAssemblyNodes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbAssemblyNodes_ItemCheck);
            // 
            // FrmFilterClassAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 704);
            this.Name = "FrmFilterClassAnalysis";
            this.Text = "Filter analysis";
            this.gbAnalysisLevel.ResumeLayout(false);
            this.gbAnalysisLevel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}