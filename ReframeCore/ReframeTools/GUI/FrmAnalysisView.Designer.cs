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
            this.btnVisualize = new System.Windows.Forms.Button();
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
            this.generalNodeAnalysisToolStripMenuItem});
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
            // btnVisualize
            // 
            this.btnVisualize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVisualize.Location = new System.Drawing.Point(597, 383);
            this.btnVisualize.Name = "btnVisualize";
            this.btnVisualize.Size = new System.Drawing.Size(75, 23);
            this.btnVisualize.TabIndex = 3;
            this.btnVisualize.Text = "Visualize...";
            this.btnVisualize.UseVisualStyleBackColor = true;
            this.btnVisualize.Click += new System.EventHandler(this.btnVisualize_Click);
            // 
            // FrmAnalysisView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(684, 414);
            this.Controls.Add(this.btnVisualize);
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
        protected System.Windows.Forms.DataGridView dgvNodes;
        private System.Windows.Forms.Button btnVisualize;
    }
}