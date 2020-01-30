namespace ReframeTools.GUI
{
    partial class FrmFilterAssemblyAnalysis
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
            this.gbFilterByParent.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrmFilterAssemblyAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 683);
            this.Name = "FrmFilterAssemblyAnalysis";
            this.Text = "Filter:";
            this.gbAnalysisLevel.ResumeLayout(false);
            this.gbAnalysisLevel.PerformLayout();
            this.gbFilterByParent.ResumeLayout(false);
            this.gbFilterByParent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}