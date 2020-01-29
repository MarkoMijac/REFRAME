namespace ReframeTools.GUI
{
    partial class FrmFilterClassMemberAnalysis
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
            // clbClassNodes
            // 
            this.clbClassNodes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbClassNodes_ItemCheck);
            // 
            // clbNamespaceNodes
            // 
            this.clbNamespaceNodes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbNamespaceNodes_ItemCheck);
            this.clbNamespaceNodes.SelectedIndexChanged += new System.EventHandler(this.clbNamespaceNodes_SelectedIndexChanged);
            // 
            // clbAssemblyNodes
            // 
            this.clbAssemblyNodes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbAssemblyNodes_ItemCheck);
            // 
            // FrmFilterClassMemberAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 701);
            this.Name = "FrmFilterClassMemberAnalysis";
            this.Text = "FrmFilterClassMemberAnalysis";
            this.gbAnalysisLevel.ResumeLayout(false);
            this.gbAnalysisLevel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}