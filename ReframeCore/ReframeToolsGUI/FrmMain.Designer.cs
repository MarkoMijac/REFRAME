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
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayReactorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customGraphAnalysesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getImmediatePredecessorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getImmediateSuccessorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getImmediateNeighboursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem,
            this.customGraphAnalysesToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(851, 24);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayReactorsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.toolsToolStripMenuItem.Text = "Analyzer";
            // 
            // displayReactorsToolStripMenuItem
            // 
            this.displayReactorsToolStripMenuItem.Name = "displayReactorsToolStripMenuItem";
            this.displayReactorsToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.displayReactorsToolStripMenuItem.Text = "Show \'Class analysis\'";
            this.displayReactorsToolStripMenuItem.Click += new System.EventHandler(this.displayReactorsToolStripMenuItem_Click);
            // 
            // customGraphAnalysesToolStripMenuItem
            // 
            this.customGraphAnalysesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getImmediatePredecessorsToolStripMenuItem,
            this.getImmediateSuccessorsToolStripMenuItem,
            this.getImmediateNeighboursToolStripMenuItem,
            this.toolStripSeparator1,
            this.showToolStripMenuItem});
            this.customGraphAnalysesToolStripMenuItem.Name = "customGraphAnalysesToolStripMenuItem";
            this.customGraphAnalysesToolStripMenuItem.Size = new System.Drawing.Size(142, 20);
            this.customGraphAnalysesToolStripMenuItem.Text = "Custom graph analyses";
            // 
            // getImmediatePredecessorsToolStripMenuItem
            // 
            this.getImmediatePredecessorsToolStripMenuItem.Name = "getImmediatePredecessorsToolStripMenuItem";
            this.getImmediatePredecessorsToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.getImmediatePredecessorsToolStripMenuItem.Text = "Get immediate predecessors";
            // 
            // getImmediateSuccessorsToolStripMenuItem
            // 
            this.getImmediateSuccessorsToolStripMenuItem.Name = "getImmediateSuccessorsToolStripMenuItem";
            this.getImmediateSuccessorsToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.getImmediateSuccessorsToolStripMenuItem.Text = "Get immediate successors";
            // 
            // getImmediateNeighboursToolStripMenuItem
            // 
            this.getImmediateNeighboursToolStripMenuItem.Name = "getImmediateNeighboursToolStripMenuItem";
            this.getImmediateNeighboursToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.getImmediateNeighboursToolStripMenuItem.Text = "Get immediate neighbours";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(220, 6);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.showToolStripMenuItem.Text = "Show";
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
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayReactorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customGraphAnalysesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getImmediatePredecessorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getImmediateSuccessorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getImmediateNeighboursToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
    }
}

