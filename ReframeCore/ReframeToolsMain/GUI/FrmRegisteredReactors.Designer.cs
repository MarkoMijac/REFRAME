﻿namespace ReframeTools.GUI
{
    partial class FrmRegisteredReactors
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
            this.dgvReactors = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.chooseAnalysislevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectMemberlevelAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectlevelAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.classMemberlevelAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.classlevelAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assemblylevelAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.namespacelevelAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getLatestUpdateInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.archivedAnalysesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblReactors = new System.Windows.Forms.Label();
            this.btnShowDetails = new System.Windows.Forms.Button();
            this.btnFetchReactors = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReactors)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvReactors
            // 
            this.dgvReactors.AllowUserToAddRows = false;
            this.dgvReactors.AllowUserToDeleteRows = false;
            this.dgvReactors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReactors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReactors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReactors.Location = new System.Drawing.Point(12, 46);
            this.dgvReactors.Name = "dgvReactors";
            this.dgvReactors.RowHeadersVisible = false;
            this.dgvReactors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReactors.Size = new System.Drawing.Size(631, 412);
            this.dgvReactors.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.MistyRose;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseAnalysislevelToolStripMenuItem,
            this.updateAnalysisToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(655, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // chooseAnalysislevelToolStripMenuItem
            // 
            this.chooseAnalysislevelToolStripMenuItem.BackColor = System.Drawing.Color.Salmon;
            this.chooseAnalysislevelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectMemberlevelAnalysisToolStripMenuItem,
            this.objectlevelAnalysisToolStripMenuItem,
            this.classMemberlevelAnalysisToolStripMenuItem,
            this.classlevelAnalysisToolStripMenuItem,
            this.assemblylevelAnalysisToolStripMenuItem,
            this.namespacelevelAnalysisToolStripMenuItem});
            this.chooseAnalysislevelToolStripMenuItem.Name = "chooseAnalysislevelToolStripMenuItem";
            this.chooseAnalysislevelToolStripMenuItem.Size = new System.Drawing.Size(145, 20);
            this.chooseAnalysislevelToolStripMenuItem.Text = "Graph structure analysis";
            // 
            // objectMemberlevelAnalysisToolStripMenuItem
            // 
            this.objectMemberlevelAnalysisToolStripMenuItem.BackColor = System.Drawing.Color.Salmon;
            this.objectMemberlevelAnalysisToolStripMenuItem.Name = "objectMemberlevelAnalysisToolStripMenuItem";
            this.objectMemberlevelAnalysisToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.objectMemberlevelAnalysisToolStripMenuItem.Text = "ObjectMember-level analysis";
            this.objectMemberlevelAnalysisToolStripMenuItem.Click += new System.EventHandler(this.objectMemberlevelAnalysisToolStripMenuItem_Click);
            // 
            // objectlevelAnalysisToolStripMenuItem
            // 
            this.objectlevelAnalysisToolStripMenuItem.BackColor = System.Drawing.Color.Salmon;
            this.objectlevelAnalysisToolStripMenuItem.Name = "objectlevelAnalysisToolStripMenuItem";
            this.objectlevelAnalysisToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.objectlevelAnalysisToolStripMenuItem.Text = "Object-level analysis";
            this.objectlevelAnalysisToolStripMenuItem.Click += new System.EventHandler(this.objectlevelAnalysisToolStripMenuItem_Click);
            // 
            // classMemberlevelAnalysisToolStripMenuItem
            // 
            this.classMemberlevelAnalysisToolStripMenuItem.BackColor = System.Drawing.Color.Salmon;
            this.classMemberlevelAnalysisToolStripMenuItem.Name = "classMemberlevelAnalysisToolStripMenuItem";
            this.classMemberlevelAnalysisToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.classMemberlevelAnalysisToolStripMenuItem.Text = "ClassMember-level analysis";
            this.classMemberlevelAnalysisToolStripMenuItem.Click += new System.EventHandler(this.classMemberlevelAnalysisToolStripMenuItem_Click);
            // 
            // classlevelAnalysisToolStripMenuItem
            // 
            this.classlevelAnalysisToolStripMenuItem.BackColor = System.Drawing.Color.Salmon;
            this.classlevelAnalysisToolStripMenuItem.Name = "classlevelAnalysisToolStripMenuItem";
            this.classlevelAnalysisToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.classlevelAnalysisToolStripMenuItem.Text = "Class-level analysis";
            this.classlevelAnalysisToolStripMenuItem.Click += new System.EventHandler(this.classlevelAnalysisToolStripMenuItem_Click);
            // 
            // assemblylevelAnalysisToolStripMenuItem
            // 
            this.assemblylevelAnalysisToolStripMenuItem.BackColor = System.Drawing.Color.Salmon;
            this.assemblylevelAnalysisToolStripMenuItem.Name = "assemblylevelAnalysisToolStripMenuItem";
            this.assemblylevelAnalysisToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.assemblylevelAnalysisToolStripMenuItem.Text = "Assembly-level analysis";
            this.assemblylevelAnalysisToolStripMenuItem.Click += new System.EventHandler(this.assemblylevelAnalysisToolStripMenuItem_Click);
            // 
            // namespacelevelAnalysisToolStripMenuItem
            // 
            this.namespacelevelAnalysisToolStripMenuItem.BackColor = System.Drawing.Color.Salmon;
            this.namespacelevelAnalysisToolStripMenuItem.Name = "namespacelevelAnalysisToolStripMenuItem";
            this.namespacelevelAnalysisToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.namespacelevelAnalysisToolStripMenuItem.Text = "Namespace-level analysis";
            this.namespacelevelAnalysisToolStripMenuItem.Click += new System.EventHandler(this.namespacelevelAnalysisToolStripMenuItem_Click);
            // 
            // updateAnalysisToolStripMenuItem
            // 
            this.updateAnalysisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getLatestUpdateInfoToolStripMenuItem,
            this.archivedAnalysesToolStripMenuItem});
            this.updateAnalysisToolStripMenuItem.Name = "updateAnalysisToolStripMenuItem";
            this.updateAnalysisToolStripMenuItem.Size = new System.Drawing.Size(144, 20);
            this.updateAnalysisToolStripMenuItem.Text = "Update process analysis";
            // 
            // getLatestUpdateInfoToolStripMenuItem
            // 
            this.getLatestUpdateInfoToolStripMenuItem.Name = "getLatestUpdateInfoToolStripMenuItem";
            this.getLatestUpdateInfoToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.getLatestUpdateInfoToolStripMenuItem.Text = "Latest update";
            this.getLatestUpdateInfoToolStripMenuItem.Click += new System.EventHandler(this.getLatestUpdateInfoToolStripMenuItem_Click);
            // 
            // archivedAnalysesToolStripMenuItem
            // 
            this.archivedAnalysesToolStripMenuItem.Name = "archivedAnalysesToolStripMenuItem";
            this.archivedAnalysesToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.archivedAnalysesToolStripMenuItem.Text = "Archived analyses";
            // 
            // lblReactors
            // 
            this.lblReactors.AutoSize = true;
            this.lblReactors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblReactors.Location = new System.Drawing.Point(12, 30);
            this.lblReactors.Name = "lblReactors";
            this.lblReactors.Size = new System.Drawing.Size(62, 13);
            this.lblReactors.TabIndex = 4;
            this.lblReactors.Text = "Reactors:";
            // 
            // btnShowDetails
            // 
            this.btnShowDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowDetails.BackColor = System.Drawing.Color.LightCoral;
            this.btnShowDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowDetails.Location = new System.Drawing.Point(541, 470);
            this.btnShowDetails.Name = "btnShowDetails";
            this.btnShowDetails.Size = new System.Drawing.Size(102, 23);
            this.btnShowDetails.TabIndex = 5;
            this.btnShowDetails.Text = "Details...";
            this.btnShowDetails.UseVisualStyleBackColor = false;
            this.btnShowDetails.Click += new System.EventHandler(this.btnShowDetails_Click);
            // 
            // btnFetchReactors
            // 
            this.btnFetchReactors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFetchReactors.BackColor = System.Drawing.Color.LightCoral;
            this.btnFetchReactors.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFetchReactors.Location = new System.Drawing.Point(12, 470);
            this.btnFetchReactors.Name = "btnFetchReactors";
            this.btnFetchReactors.Size = new System.Drawing.Size(190, 23);
            this.btnFetchReactors.TabIndex = 6;
            this.btnFetchReactors.Text = "Fetch reactors";
            this.btnFetchReactors.UseVisualStyleBackColor = false;
            this.btnFetchReactors.Click += new System.EventHandler(this.btnFetchReactors_Click);
            // 
            // FrmRegisteredReactors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(655, 504);
            this.Controls.Add(this.btnFetchReactors);
            this.Controls.Add(this.btnShowDetails);
            this.Controls.Add(this.lblReactors);
            this.Controls.Add(this.dgvReactors);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmRegisteredReactors";
            this.Text = "Registered reactors";
            this.Load += new System.EventHandler(this.FrmRegisteredReactors_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReactors)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem chooseAnalysislevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem objectMemberlevelAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem objectlevelAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem classMemberlevelAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem classlevelAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assemblylevelAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem namespacelevelAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getLatestUpdateInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem archivedAnalysesToolStripMenuItem;
        private System.Windows.Forms.Label lblReactors;
        protected System.Windows.Forms.Button btnShowDetails;
        public System.Windows.Forms.DataGridView dgvReactors;
        protected System.Windows.Forms.Button btnFetchReactors;
    }
}