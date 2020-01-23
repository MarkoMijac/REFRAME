namespace ReframeTools.GUI
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
            this.objectlevelAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectlevelAnalysisToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.assemblylevelAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.classlevelAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.namespacelevelAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analysesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showEntireGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.showSourceNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSinkNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLeafNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showOrphanNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showIntermediaryNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nodeAnalysesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPredecessorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSuccessorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showNeighboursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visualizationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showEntireGraphToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showPredecessorsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showSuccessorsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showNeighboursToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.displaySourceNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displaySinkNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayLeafNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayIntermediaryNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayOrphanNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.classMemberlevelAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reactorToolStripMenuItem,
            this.analysesToolStripMenuItem,
            this.nodeAnalysesToolStripMenuItem,
            this.visualizationsToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(851, 24);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "menuStrip1";
            // 
            // reactorToolStripMenuItem
            // 
            this.reactorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectlevelAnalysisToolStripMenuItem,
            this.objectlevelAnalysisToolStripMenuItem1,
            this.classMemberlevelAnalysisToolStripMenuItem,
            this.classlevelAnalysisToolStripMenuItem,
            this.assemblylevelAnalysisToolStripMenuItem,
            this.namespacelevelAnalysisToolStripMenuItem});
            this.reactorToolStripMenuItem.Name = "reactorToolStripMenuItem";
            this.reactorToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.reactorToolStripMenuItem.Text = "Reactor";
            // 
            // objectlevelAnalysisToolStripMenuItem
            // 
            this.objectlevelAnalysisToolStripMenuItem.Name = "objectlevelAnalysisToolStripMenuItem";
            this.objectlevelAnalysisToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.objectlevelAnalysisToolStripMenuItem.Text = "ObjectMember-level analysis";
            this.objectlevelAnalysisToolStripMenuItem.Click += new System.EventHandler(this.objectMemberlevelAnalysisToolStripMenuItem_Click);
            // 
            // objectlevelAnalysisToolStripMenuItem1
            // 
            this.objectlevelAnalysisToolStripMenuItem1.Name = "objectlevelAnalysisToolStripMenuItem1";
            this.objectlevelAnalysisToolStripMenuItem1.Size = new System.Drawing.Size(227, 22);
            this.objectlevelAnalysisToolStripMenuItem1.Text = "Object-level analysis";
            this.objectlevelAnalysisToolStripMenuItem1.Click += new System.EventHandler(this.objectlevelAnalysisToolStripMenuItem1_Click);
            // 
            // assemblylevelAnalysisToolStripMenuItem
            // 
            this.assemblylevelAnalysisToolStripMenuItem.Name = "assemblylevelAnalysisToolStripMenuItem";
            this.assemblylevelAnalysisToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.assemblylevelAnalysisToolStripMenuItem.Text = "Assembly-level analysis";
            this.assemblylevelAnalysisToolStripMenuItem.Click += new System.EventHandler(this.assemblylevelAnalysisToolStripMenuItem_Click);
            // 
            // classlevelAnalysisToolStripMenuItem
            // 
            this.classlevelAnalysisToolStripMenuItem.Name = "classlevelAnalysisToolStripMenuItem";
            this.classlevelAnalysisToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.classlevelAnalysisToolStripMenuItem.Text = "Class-level analysis";
            this.classlevelAnalysisToolStripMenuItem.Click += new System.EventHandler(this.classlevelAnalysisToolStripMenuItem_Click);
            // 
            // namespacelevelAnalysisToolStripMenuItem
            // 
            this.namespacelevelAnalysisToolStripMenuItem.Name = "namespacelevelAnalysisToolStripMenuItem";
            this.namespacelevelAnalysisToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.namespacelevelAnalysisToolStripMenuItem.Text = "Namespace-level analysis";
            this.namespacelevelAnalysisToolStripMenuItem.Click += new System.EventHandler(this.namespacelevelAnalysisToolStripMenuItem_Click);
            // 
            // analysesToolStripMenuItem
            // 
            this.analysesToolStripMenuItem.BackColor = System.Drawing.Color.LightCoral;
            this.analysesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showEntireGraphToolStripMenuItem,
            this.toolStripSeparator1,
            this.showSourceNodesToolStripMenuItem,
            this.showSinkNodesToolStripMenuItem,
            this.showLeafNodesToolStripMenuItem,
            this.showOrphanNodesToolStripMenuItem,
            this.showIntermediaryNodesToolStripMenuItem});
            this.analysesToolStripMenuItem.Name = "analysesToolStripMenuItem";
            this.analysesToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.analysesToolStripMenuItem.Text = "Analyses";
            // 
            // showEntireGraphToolStripMenuItem
            // 
            this.showEntireGraphToolStripMenuItem.Name = "showEntireGraphToolStripMenuItem";
            this.showEntireGraphToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.showEntireGraphToolStripMenuItem.Text = "Show entire graph";
            this.showEntireGraphToolStripMenuItem.Click += new System.EventHandler(this.showEntireGraphToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(205, 6);
            // 
            // showSourceNodesToolStripMenuItem
            // 
            this.showSourceNodesToolStripMenuItem.Name = "showSourceNodesToolStripMenuItem";
            this.showSourceNodesToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
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
            // showLeafNodesToolStripMenuItem
            // 
            this.showLeafNodesToolStripMenuItem.Name = "showLeafNodesToolStripMenuItem";
            this.showLeafNodesToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.showLeafNodesToolStripMenuItem.Text = "Show leaf nodes";
            this.showLeafNodesToolStripMenuItem.Click += new System.EventHandler(this.showLeafNodesToolStripMenuItem_Click);
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
            // nodeAnalysesToolStripMenuItem
            // 
            this.nodeAnalysesToolStripMenuItem.BackColor = System.Drawing.Color.MistyRose;
            this.nodeAnalysesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPredecessorsToolStripMenuItem,
            this.showSuccessorsToolStripMenuItem,
            this.showNeighboursToolStripMenuItem});
            this.nodeAnalysesToolStripMenuItem.Name = "nodeAnalysesToolStripMenuItem";
            this.nodeAnalysesToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.nodeAnalysesToolStripMenuItem.Text = "Node analyses";
            // 
            // showPredecessorsToolStripMenuItem
            // 
            this.showPredecessorsToolStripMenuItem.Name = "showPredecessorsToolStripMenuItem";
            this.showPredecessorsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.showPredecessorsToolStripMenuItem.Text = "Show predecessors";
            this.showPredecessorsToolStripMenuItem.Click += new System.EventHandler(this.showPredecessorsToolStripMenuItem_Click);
            // 
            // showSuccessorsToolStripMenuItem
            // 
            this.showSuccessorsToolStripMenuItem.Name = "showSuccessorsToolStripMenuItem";
            this.showSuccessorsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.showSuccessorsToolStripMenuItem.Text = "Show successors";
            this.showSuccessorsToolStripMenuItem.Click += new System.EventHandler(this.showSuccessorsToolStripMenuItem_Click);
            // 
            // showNeighboursToolStripMenuItem
            // 
            this.showNeighboursToolStripMenuItem.Name = "showNeighboursToolStripMenuItem";
            this.showNeighboursToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.showNeighboursToolStripMenuItem.Text = "Show neighbours";
            this.showNeighboursToolStripMenuItem.Click += new System.EventHandler(this.showNeighboursToolStripMenuItem_Click);
            // 
            // visualizationsToolStripMenuItem
            // 
            this.visualizationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showEntireGraphToolStripMenuItem1,
            this.toolStripSeparator2,
            this.showPredecessorsToolStripMenuItem1,
            this.showSuccessorsToolStripMenuItem1,
            this.showNeighboursToolStripMenuItem1,
            this.displaySourceNodesToolStripMenuItem,
            this.displaySinkNodesToolStripMenuItem,
            this.displayLeafNodesToolStripMenuItem,
            this.displayIntermediaryNodesToolStripMenuItem,
            this.displayOrphanNodesToolStripMenuItem});
            this.visualizationsToolStripMenuItem.Name = "visualizationsToolStripMenuItem";
            this.visualizationsToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.visualizationsToolStripMenuItem.Text = "Visualizations";
            // 
            // showEntireGraphToolStripMenuItem1
            // 
            this.showEntireGraphToolStripMenuItem1.Name = "showEntireGraphToolStripMenuItem1";
            this.showEntireGraphToolStripMenuItem1.Size = new System.Drawing.Size(217, 22);
            this.showEntireGraphToolStripMenuItem1.Text = "Show entire graph";
            this.showEntireGraphToolStripMenuItem1.Click += new System.EventHandler(this.showEntireGraphToolStripMenuItem1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(214, 6);
            // 
            // showPredecessorsToolStripMenuItem1
            // 
            this.showPredecessorsToolStripMenuItem1.Name = "showPredecessorsToolStripMenuItem1";
            this.showPredecessorsToolStripMenuItem1.Size = new System.Drawing.Size(217, 22);
            this.showPredecessorsToolStripMenuItem1.Text = "Show predecessors";
            this.showPredecessorsToolStripMenuItem1.Click += new System.EventHandler(this.showPredecessorsToolStripMenuItem1_Click);
            // 
            // showSuccessorsToolStripMenuItem1
            // 
            this.showSuccessorsToolStripMenuItem1.Name = "showSuccessorsToolStripMenuItem1";
            this.showSuccessorsToolStripMenuItem1.Size = new System.Drawing.Size(217, 22);
            this.showSuccessorsToolStripMenuItem1.Text = "Show successors";
            this.showSuccessorsToolStripMenuItem1.Click += new System.EventHandler(this.showSuccessorsToolStripMenuItem1_Click);
            // 
            // showNeighboursToolStripMenuItem1
            // 
            this.showNeighboursToolStripMenuItem1.Name = "showNeighboursToolStripMenuItem1";
            this.showNeighboursToolStripMenuItem1.Size = new System.Drawing.Size(217, 22);
            this.showNeighboursToolStripMenuItem1.Text = "Show neighbours";
            this.showNeighboursToolStripMenuItem1.Click += new System.EventHandler(this.showNeighboursToolStripMenuItem1_Click);
            // 
            // displaySourceNodesToolStripMenuItem
            // 
            this.displaySourceNodesToolStripMenuItem.Name = "displaySourceNodesToolStripMenuItem";
            this.displaySourceNodesToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.displaySourceNodesToolStripMenuItem.Text = "Display source nodes";
            this.displaySourceNodesToolStripMenuItem.Click += new System.EventHandler(this.displaySourceNodesToolStripMenuItem_Click);
            // 
            // displaySinkNodesToolStripMenuItem
            // 
            this.displaySinkNodesToolStripMenuItem.Name = "displaySinkNodesToolStripMenuItem";
            this.displaySinkNodesToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.displaySinkNodesToolStripMenuItem.Text = "Display sink nodes";
            this.displaySinkNodesToolStripMenuItem.Click += new System.EventHandler(this.displaySinkNodesToolStripMenuItem_Click);
            // 
            // displayLeafNodesToolStripMenuItem
            // 
            this.displayLeafNodesToolStripMenuItem.Name = "displayLeafNodesToolStripMenuItem";
            this.displayLeafNodesToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.displayLeafNodesToolStripMenuItem.Text = "Display leaf nodes";
            this.displayLeafNodesToolStripMenuItem.Click += new System.EventHandler(this.displayLeafNodesToolStripMenuItem_Click);
            // 
            // displayIntermediaryNodesToolStripMenuItem
            // 
            this.displayIntermediaryNodesToolStripMenuItem.Name = "displayIntermediaryNodesToolStripMenuItem";
            this.displayIntermediaryNodesToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.displayIntermediaryNodesToolStripMenuItem.Text = "Display intermediary nodes";
            this.displayIntermediaryNodesToolStripMenuItem.Click += new System.EventHandler(this.displayIntermediaryNodesToolStripMenuItem_Click);
            // 
            // displayOrphanNodesToolStripMenuItem
            // 
            this.displayOrphanNodesToolStripMenuItem.Name = "displayOrphanNodesToolStripMenuItem";
            this.displayOrphanNodesToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.displayOrphanNodesToolStripMenuItem.Text = "Display orphan nodes";
            this.displayOrphanNodesToolStripMenuItem.Click += new System.EventHandler(this.displayOrphanNodesToolStripMenuItem_Click);
            // 
            // classMemberlevelAnalysisToolStripMenuItem
            // 
            this.classMemberlevelAnalysisToolStripMenuItem.Name = "classMemberlevelAnalysisToolStripMenuItem";
            this.classMemberlevelAnalysisToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.classMemberlevelAnalysisToolStripMenuItem.Text = "ClassMember-level analysis";
            this.classMemberlevelAnalysisToolStripMenuItem.Click += new System.EventHandler(this.classMemberlevelAnalysisToolStripMenuItem_Click);
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem visualizationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showEntireGraphToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem showPredecessorsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showSuccessorsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showNeighboursToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nodeAnalysesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPredecessorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showSuccessorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showNeighboursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displaySourceNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displaySinkNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayLeafNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayIntermediaryNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayOrphanNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assemblylevelAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem namespacelevelAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem objectlevelAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem objectlevelAnalysisToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem classMemberlevelAnalysisToolStripMenuItem;
    }
}

