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
            this.generalNodeAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customAnalysesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnVisualize = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtGraphDensity = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMaxNumOfDependencies = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNumberOfDependencies = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNumberOfAnalyzedNodes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGraphTotalNodeCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGraphIdentifier = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNodes)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.dgvNodes.Location = new System.Drawing.Point(12, 133);
            this.dgvNodes.Name = "dgvNodes";
            this.dgvNodes.RowHeadersVisible = false;
            this.dgvNodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNodes.Size = new System.Drawing.Size(590, 291);
            this.dgvNodes.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightCoral;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generalAnalysesToolStripMenuItem,
            this.generalNodeAnalysisToolStripMenuItem,
            this.customAnalysesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(614, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // generalAnalysesToolStripMenuItem
            // 
            this.generalAnalysesToolStripMenuItem.BackColor = System.Drawing.Color.LightCoral;
            this.generalAnalysesToolStripMenuItem.Name = "generalAnalysesToolStripMenuItem";
            this.generalAnalysesToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.generalAnalysesToolStripMenuItem.Text = "Graph analyses";
            // 
            // generalNodeAnalysisToolStripMenuItem
            // 
            this.generalNodeAnalysisToolStripMenuItem.BackColor = System.Drawing.Color.LightCoral;
            this.generalNodeAnalysisToolStripMenuItem.Name = "generalNodeAnalysisToolStripMenuItem";
            this.generalNodeAnalysisToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.generalNodeAnalysisToolStripMenuItem.Text = "Node analyses";
            // 
            // customAnalysesToolStripMenuItem
            // 
            this.customAnalysesToolStripMenuItem.Name = "customAnalysesToolStripMenuItem";
            this.customAnalysesToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.customAnalysesToolStripMenuItem.Text = "Custom analyses";
            // 
            // btnVisualize
            // 
            this.btnVisualize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVisualize.BackColor = System.Drawing.Color.LightCoral;
            this.btnVisualize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVisualize.Location = new System.Drawing.Point(506, 430);
            this.btnVisualize.Name = "btnVisualize";
            this.btnVisualize.Size = new System.Drawing.Size(96, 23);
            this.btnVisualize.TabIndex = 3;
            this.btnVisualize.Text = "Visualize...";
            this.btnVisualize.UseVisualStyleBackColor = false;
            this.btnVisualize.Click += new System.EventHandler(this.btnVisualize_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtGraphDensity);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtMaxNumOfDependencies);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtNumberOfDependencies);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtNumberOfAnalyzedNodes);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtGraphTotalNodeCount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtGraphIdentifier);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(590, 100);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Graph details:";
            // 
            // txtGraphDensity
            // 
            this.txtGraphDensity.BackColor = System.Drawing.Color.MistyRose;
            this.txtGraphDensity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGraphDensity.Location = new System.Drawing.Point(465, 71);
            this.txtGraphDensity.Name = "txtGraphDensity";
            this.txtGraphDensity.Size = new System.Drawing.Size(100, 20);
            this.txtGraphDensity.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(307, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Graph density [0-1]:";
            // 
            // txtMaxNumOfDependencies
            // 
            this.txtMaxNumOfDependencies.BackColor = System.Drawing.Color.MistyRose;
            this.txtMaxNumOfDependencies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaxNumOfDependencies.Location = new System.Drawing.Point(465, 46);
            this.txtMaxNumOfDependencies.Name = "txtMaxNumOfDependencies";
            this.txtMaxNumOfDependencies.Size = new System.Drawing.Size(100, 20);
            this.txtMaxNumOfDependencies.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(307, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Max number of dependencies:";
            // 
            // txtNumberOfDependencies
            // 
            this.txtNumberOfDependencies.BackColor = System.Drawing.Color.MistyRose;
            this.txtNumberOfDependencies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNumberOfDependencies.Location = new System.Drawing.Point(465, 19);
            this.txtNumberOfDependencies.Name = "txtNumberOfDependencies";
            this.txtNumberOfDependencies.Size = new System.Drawing.Size(100, 20);
            this.txtNumberOfDependencies.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(307, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Number of dependencies:";
            // 
            // txtNumberOfAnalyzedNodes
            // 
            this.txtNumberOfAnalyzedNodes.BackColor = System.Drawing.Color.MistyRose;
            this.txtNumberOfAnalyzedNodes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNumberOfAnalyzedNodes.Location = new System.Drawing.Point(160, 71);
            this.txtNumberOfAnalyzedNodes.Name = "txtNumberOfAnalyzedNodes";
            this.txtNumberOfAnalyzedNodes.Size = new System.Drawing.Size(100, 20);
            this.txtNumberOfAnalyzedNodes.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Number of analyzed nodes:";
            // 
            // txtGraphTotalNodeCount
            // 
            this.txtGraphTotalNodeCount.BackColor = System.Drawing.Color.MistyRose;
            this.txtGraphTotalNodeCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGraphTotalNodeCount.Location = new System.Drawing.Point(160, 45);
            this.txtGraphTotalNodeCount.Name = "txtGraphTotalNodeCount";
            this.txtGraphTotalNodeCount.Size = new System.Drawing.Size(100, 20);
            this.txtGraphTotalNodeCount.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Total nodes count:";
            // 
            // txtGraphIdentifier
            // 
            this.txtGraphIdentifier.BackColor = System.Drawing.Color.MistyRose;
            this.txtGraphIdentifier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGraphIdentifier.Location = new System.Drawing.Point(160, 19);
            this.txtGraphIdentifier.Name = "txtGraphIdentifier";
            this.txtGraphIdentifier.Size = new System.Drawing.Size(100, 20);
            this.txtGraphIdentifier.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Graph identifier:";
            // 
            // FrmAnalysisView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(614, 461);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnVisualize);
            this.Controls.Add(this.dgvNodes);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(630, 500);
            this.Name = "FrmAnalysisView";
            this.Text = "Analysis";
            this.Load += new System.EventHandler(this.FrmObjectMemberAnalysis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNodes)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem generalAnalysesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generalNodeAnalysisToolStripMenuItem;
        protected System.Windows.Forms.DataGridView dgvNodes;
        private System.Windows.Forms.Button btnVisualize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtGraphTotalNodeCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGraphIdentifier;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumberOfAnalyzedNodes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGraphDensity;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMaxNumOfDependencies;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNumberOfDependencies;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripMenuItem customAnalysesToolStripMenuItem;
    }
}