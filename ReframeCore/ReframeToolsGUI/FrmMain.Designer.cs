namespace ReframeToolsGUI
{
    partial class FrmMain
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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.reactorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.classlevelAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analysesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSourceNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSinkNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showOrphanNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showIntermediaryNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLeafNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showEntireGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reactorToolStripMenuItem,
            this.analysesToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(851, 24);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "menuStrip1";
            // 
            // reactorToolStripMenuItem
            // 
            this.reactorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.classlevelAnalysisToolStripMenuItem});
            this.reactorToolStripMenuItem.Name = "reactorToolStripMenuItem";
            this.reactorToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.reactorToolStripMenuItem.Text = "Reactor";
            // 
            // classlevelAnalysisToolStripMenuItem
            // 
            this.classlevelAnalysisToolStripMenuItem.Name = "classlevelAnalysisToolStripMenuItem";
            this.classlevelAnalysisToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.classlevelAnalysisToolStripMenuItem.Text = "Class-level analysis";
            this.classlevelAnalysisToolStripMenuItem.Click += new System.EventHandler(this.classlevelAnalysisToolStripMenuItem_Click);
            // 
            // analysesToolStripMenuItem
            // 
            this.analysesToolStripMenuItem.BackColor = System.Drawing.Color.LightCoral;
            this.analysesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showEntireGraphToolStripMenuItem,
            this.showSourceNodesToolStripMenuItem,
            this.showSinkNodesToolStripMenuItem,
            this.showLeafNodesToolStripMenuItem,
            this.showOrphanNodesToolStripMenuItem,
            this.showIntermediaryNodesToolStripMenuItem});
            this.analysesToolStripMenuItem.Name = "analysesToolStripMenuItem";
            this.analysesToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.analysesToolStripMenuItem.Text = "Analyses";
            // 
            // showSourceNodesToolStripMenuItem
            // 
            this.showSourceNodesToolStripMenuItem.Name = "showSourceNodesToolStripMenuItem";
            this.showSourceNodesToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.showSourceNodesToolStripMenuItem.Text = "Show source nodes";
            this.showSourceNodesToolStripMenuItem.Click += new System.EventHandler(this.showSourceNodesToolStripMenuItem_Click);
            // 
            // showSinkNodesToolStripMenuItem
            // 
            this.showSinkNodesToolStripMenuItem.Name = "showSinkNodesToolStripMenuItem";
            this.showSinkNodesToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.showSinkNodesToolStripMenuItem.Text = "Show sink nodes";
            this.showSinkNodesToolStripMenuItem.Click += new System.EventHandler(this.showSinkNodesToolStripMenuItem_Click);
            // 
            // showOrphanNodesToolStripMenuItem
            // 
            this.showOrphanNodesToolStripMenuItem.Name = "showOrphanNodesToolStripMenuItem";
            this.showOrphanNodesToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.showOrphanNodesToolStripMenuItem.Text = "Show orphan nodes";
            this.showOrphanNodesToolStripMenuItem.Click += new System.EventHandler(this.showOrphanNodesToolStripMenuItem_Click);
            // 
            // showIntermediaryNodesToolStripMenuItem
            // 
            this.showIntermediaryNodesToolStripMenuItem.Name = "showIntermediaryNodesToolStripMenuItem";
            this.showIntermediaryNodesToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.showIntermediaryNodesToolStripMenuItem.Text = "Show intermediary nodes";
            this.showIntermediaryNodesToolStripMenuItem.Click += new System.EventHandler(this.showIntermediaryNodesToolStripMenuItem_Click);
            // 
            // showLeafNodesToolStripMenuItem
            // 
            this.showLeafNodesToolStripMenuItem.Name = "showLeafNodesToolStripMenuItem";
            this.showLeafNodesToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.showLeafNodesToolStripMenuItem.Text = "Show leaf nodes";
            this.showLeafNodesToolStripMenuItem.Click += new System.EventHandler(this.showLeafNodesToolStripMenuItem_Click);
            // 
            // showEntireGraphToolStripMenuItem
            // 
            this.showEntireGraphToolStripMenuItem.Name = "showEntireGraphToolStripMenuItem";
            this.showEntireGraphToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.showEntireGraphToolStripMenuItem.Text = "Show entire graph";
            this.showEntireGraphToolStripMenuItem.Click += new System.EventHandler(this.showEntireGraphToolStripMenuItem_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 661);
            this.Controls.Add(this.mainMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenu;
            this.Name = "FrmMain";
            this.Text = "REFRAME Tools";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem reactorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem classlevelAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analysesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showSourceNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showSinkNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showOrphanNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showIntermediaryNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLeafNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showEntireGraphToolStripMenuItem;
    }
}

