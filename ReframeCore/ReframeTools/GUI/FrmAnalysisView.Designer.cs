namespace ReframeTools.GUI
{
    partial class FrmAnalysisView
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
            this.dgvNodes = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.generalAnalysesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showEntireGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.showSourceNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSinkNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLeafNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showOrphanNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showIntermediaryNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalNodeAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showNodePredecessorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSuccessorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showNodesNeighboursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphVisualizationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayEntireGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.displaySourceNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displaySinkNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayLeafNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayOrphanNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayIntermediaryNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nodeVisualizationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayNodesPredecessorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayNodesSuccessorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayNodesNeighboursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNodes)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvNodes
            // 
            this.dgvNodes.AllowUserToAddRows = false;
            this.dgvNodes.AllowUserToDeleteRows = false;
            this.dgvNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNodes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNodes.Location = new System.Drawing.Point(12, 27);
            this.dgvNodes.Name = "dgvNodes";
            this.dgvNodes.RowHeadersVisible = false;
            this.dgvNodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNodes.Size = new System.Drawing.Size(660, 350);
            this.dgvNodes.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.MistyRose;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generalAnalysesToolStripMenuItem,
            this.generalNodeAnalysisToolStripMenuItem,
            this.graphVisualizationsToolStripMenuItem,
            this.nodeVisualizationsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(684, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // generalAnalysesToolStripMenuItem
            // 
            this.generalAnalysesToolStripMenuItem.BackColor = System.Drawing.Color.Salmon;
            this.generalAnalysesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showEntireGraphToolStripMenuItem,
            this.toolStripSeparator1,
            this.showSourceNodesToolStripMenuItem,
            this.showSinkNodesToolStripMenuItem,
            this.showLeafNodesToolStripMenuItem,
            this.showOrphanNodesToolStripMenuItem,
            this.showIntermediaryNodesToolStripMenuItem});
            this.generalAnalysesToolStripMenuItem.Name = "generalAnalysesToolStripMenuItem";
            this.generalAnalysesToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.generalAnalysesToolStripMenuItem.Text = "Graph analyses";
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
            // generalNodeAnalysisToolStripMenuItem
            // 
            this.generalNodeAnalysisToolStripMenuItem.BackColor = System.Drawing.Color.Salmon;
            this.generalNodeAnalysisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showNodePredecessorsToolStripMenuItem,
            this.showSuccessorsToolStripMenuItem,
            this.showNodesNeighboursToolStripMenuItem});
            this.generalNodeAnalysisToolStripMenuItem.Name = "generalNodeAnalysisToolStripMenuItem";
            this.generalNodeAnalysisToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.generalNodeAnalysisToolStripMenuItem.Text = "Node analysis";
            // 
            // showNodePredecessorsToolStripMenuItem
            // 
            this.showNodePredecessorsToolStripMenuItem.Name = "showNodePredecessorsToolStripMenuItem";
            this.showNodePredecessorsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.showNodePredecessorsToolStripMenuItem.Text = "Show node\'s predecessors";
            this.showNodePredecessorsToolStripMenuItem.Click += new System.EventHandler(this.showNodePredecessorsToolStripMenuItem_Click);
            // 
            // showSuccessorsToolStripMenuItem
            // 
            this.showSuccessorsToolStripMenuItem.Name = "showSuccessorsToolStripMenuItem";
            this.showSuccessorsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.showSuccessorsToolStripMenuItem.Text = "Show node\'s successors";
            this.showSuccessorsToolStripMenuItem.Click += new System.EventHandler(this.showSuccessorsToolStripMenuItem_Click);
            // 
            // showNodesNeighboursToolStripMenuItem
            // 
            this.showNodesNeighboursToolStripMenuItem.Name = "showNodesNeighboursToolStripMenuItem";
            this.showNodesNeighboursToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.showNodesNeighboursToolStripMenuItem.Text = "Show node\'s neighbours";
            this.showNodesNeighboursToolStripMenuItem.Click += new System.EventHandler(this.showNodesNeighboursToolStripMenuItem_Click);
            // 
            // graphVisualizationsToolStripMenuItem
            // 
            this.graphVisualizationsToolStripMenuItem.BackColor = System.Drawing.Color.SandyBrown;
            this.graphVisualizationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayEntireGraphToolStripMenuItem,
            this.toolStripSeparator2,
            this.displaySourceNodesToolStripMenuItem,
            this.displaySinkNodesToolStripMenuItem,
            this.displayLeafNodesToolStripMenuItem,
            this.displayOrphanNodesToolStripMenuItem,
            this.displayIntermediaryNodesToolStripMenuItem});
            this.graphVisualizationsToolStripMenuItem.Name = "graphVisualizationsToolStripMenuItem";
            this.graphVisualizationsToolStripMenuItem.Size = new System.Drawing.Size(124, 20);
            this.graphVisualizationsToolStripMenuItem.Text = "Graph visualizations";
            // 
            // displayEntireGraphToolStripMenuItem
            // 
            this.displayEntireGraphToolStripMenuItem.Name = "displayEntireGraphToolStripMenuItem";
            this.displayEntireGraphToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.displayEntireGraphToolStripMenuItem.Text = "Display entire graph";
            this.displayEntireGraphToolStripMenuItem.Click += new System.EventHandler(this.displayEntireGraphToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(214, 6);
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
            // displayOrphanNodesToolStripMenuItem
            // 
            this.displayOrphanNodesToolStripMenuItem.Name = "displayOrphanNodesToolStripMenuItem";
            this.displayOrphanNodesToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.displayOrphanNodesToolStripMenuItem.Text = "Display orphan nodes";
            this.displayOrphanNodesToolStripMenuItem.Click += new System.EventHandler(this.displayOrphanNodesToolStripMenuItem_Click);
            // 
            // displayIntermediaryNodesToolStripMenuItem
            // 
            this.displayIntermediaryNodesToolStripMenuItem.Name = "displayIntermediaryNodesToolStripMenuItem";
            this.displayIntermediaryNodesToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.displayIntermediaryNodesToolStripMenuItem.Text = "Display intermediary nodes";
            this.displayIntermediaryNodesToolStripMenuItem.Click += new System.EventHandler(this.displayIntermediaryNodesToolStripMenuItem_Click);
            // 
            // nodeVisualizationsToolStripMenuItem
            // 
            this.nodeVisualizationsToolStripMenuItem.BackColor = System.Drawing.Color.SandyBrown;
            this.nodeVisualizationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayNodesPredecessorsToolStripMenuItem,
            this.displayNodesSuccessorsToolStripMenuItem,
            this.displayNodesNeighboursToolStripMenuItem});
            this.nodeVisualizationsToolStripMenuItem.Name = "nodeVisualizationsToolStripMenuItem";
            this.nodeVisualizationsToolStripMenuItem.Size = new System.Drawing.Size(121, 20);
            this.nodeVisualizationsToolStripMenuItem.Text = "Node visualizations";
            // 
            // displayNodesPredecessorsToolStripMenuItem
            // 
            this.displayNodesPredecessorsToolStripMenuItem.Name = "displayNodesPredecessorsToolStripMenuItem";
            this.displayNodesPredecessorsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.displayNodesPredecessorsToolStripMenuItem.Text = "Display node\'s predecessors";
            this.displayNodesPredecessorsToolStripMenuItem.Click += new System.EventHandler(this.displayNodesPredecessorsToolStripMenuItem_Click);
            // 
            // displayNodesSuccessorsToolStripMenuItem
            // 
            this.displayNodesSuccessorsToolStripMenuItem.Name = "displayNodesSuccessorsToolStripMenuItem";
            this.displayNodesSuccessorsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.displayNodesSuccessorsToolStripMenuItem.Text = "Display node\'s successors";
            this.displayNodesSuccessorsToolStripMenuItem.Click += new System.EventHandler(this.displayNodesSuccessorsToolStripMenuItem_Click);
            // 
            // displayNodesNeighboursToolStripMenuItem
            // 
            this.displayNodesNeighboursToolStripMenuItem.Name = "displayNodesNeighboursToolStripMenuItem";
            this.displayNodesNeighboursToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.displayNodesNeighboursToolStripMenuItem.Text = "Display node\'s neighbours";
            this.displayNodesNeighboursToolStripMenuItem.Click += new System.EventHandler(this.displayNodesNeighboursToolStripMenuItem_Click);
            // 
            // FrmAnalysisView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(684, 389);
            this.Controls.Add(this.dgvNodes);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmAnalysisView";
            this.Text = "Analysis";
            this.Load += new System.EventHandler(this.FrmObjectMemberAnalysis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNodes)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem generalAnalysesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showEntireGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem showSourceNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showSinkNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLeafNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showOrphanNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showIntermediaryNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generalNodeAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showNodePredecessorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showSuccessorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showNodesNeighboursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphVisualizationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayEntireGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem displaySourceNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displaySinkNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayLeafNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayOrphanNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayIntermediaryNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nodeVisualizationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayNodesPredecessorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayNodesSuccessorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayNodesNeighboursToolStripMenuItem;
        protected System.Windows.Forms.DataGridView dgvNodes;
    }
}