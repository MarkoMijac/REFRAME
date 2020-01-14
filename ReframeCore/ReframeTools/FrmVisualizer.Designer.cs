namespace ReframeTools
{
    partial class FrmVisualizer
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
            this.btnDisplayReactors = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvRegisteredGraphs = new System.Windows.Forms.DataGridView();
            this.btnGetGraphNodes = new System.Windows.Forms.Button();
            this.btnGetRegisteredGraphs = new System.Windows.Forms.Button();
            this.btnDisplayGraph = new System.Windows.Forms.Button();
            this.btnClassGraph = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegisteredGraphs)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDisplayReactors
            // 
            this.btnDisplayReactors.Location = new System.Drawing.Point(12, 262);
            this.btnDisplayReactors.Name = "btnDisplayReactors";
            this.btnDisplayReactors.Size = new System.Drawing.Size(112, 23);
            this.btnDisplayReactors.TabIndex = 11;
            this.btnDisplayReactors.Text = "Display reactors";
            this.btnDisplayReactors.UseVisualStyleBackColor = true;
            this.btnDisplayReactors.Click += new System.EventHandler(this.btnDisplayReactors_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Registered graphs:";
            // 
            // dgvRegisteredGraphs
            // 
            this.dgvRegisteredGraphs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRegisteredGraphs.Location = new System.Drawing.Point(12, 25);
            this.dgvRegisteredGraphs.Name = "dgvRegisteredGraphs";
            this.dgvRegisteredGraphs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRegisteredGraphs.Size = new System.Drawing.Size(638, 231);
            this.dgvRegisteredGraphs.TabIndex = 9;
            // 
            // btnGetGraphNodes
            // 
            this.btnGetGraphNodes.Location = new System.Drawing.Point(512, 417);
            this.btnGetGraphNodes.Name = "btnGetGraphNodes";
            this.btnGetGraphNodes.Size = new System.Drawing.Size(75, 66);
            this.btnGetGraphNodes.TabIndex = 8;
            this.btnGetGraphNodes.Text = "Get graph nodes";
            this.btnGetGraphNodes.UseVisualStyleBackColor = true;
            this.btnGetGraphNodes.Click += new System.EventHandler(this.btnGetGraphNodes_Click);
            // 
            // btnGetRegisteredGraphs
            // 
            this.btnGetRegisteredGraphs.Location = new System.Drawing.Point(431, 417);
            this.btnGetRegisteredGraphs.Name = "btnGetRegisteredGraphs";
            this.btnGetRegisteredGraphs.Size = new System.Drawing.Size(75, 66);
            this.btnGetRegisteredGraphs.TabIndex = 7;
            this.btnGetRegisteredGraphs.Text = "Get registered graphs";
            this.btnGetRegisteredGraphs.UseVisualStyleBackColor = true;
            this.btnGetRegisteredGraphs.Click += new System.EventHandler(this.btnGetRegisteredGraphs_Click);
            // 
            // btnDisplayGraph
            // 
            this.btnDisplayGraph.Location = new System.Drawing.Point(50, 417);
            this.btnDisplayGraph.Name = "btnDisplayGraph";
            this.btnDisplayGraph.Size = new System.Drawing.Size(112, 23);
            this.btnDisplayGraph.TabIndex = 6;
            this.btnDisplayGraph.Text = "Display graph";
            this.btnDisplayGraph.UseVisualStyleBackColor = true;
            this.btnDisplayGraph.Click += new System.EventHandler(this.btnDisplayGraph_Click);
            // 
            // btnClassGraph
            // 
            this.btnClassGraph.Location = new System.Drawing.Point(168, 417);
            this.btnClassGraph.Name = "btnClassGraph";
            this.btnClassGraph.Size = new System.Drawing.Size(112, 23);
            this.btnClassGraph.TabIndex = 12;
            this.btnClassGraph.Text = "Display class graph";
            this.btnClassGraph.UseVisualStyleBackColor = true;
            this.btnClassGraph.Click += new System.EventHandler(this.btnClassGraph_Click);
            // 
            // FrmVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 499);
            this.Controls.Add(this.btnClassGraph);
            this.Controls.Add(this.btnDisplayReactors);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvRegisteredGraphs);
            this.Controls.Add(this.btnGetGraphNodes);
            this.Controls.Add(this.btnGetRegisteredGraphs);
            this.Controls.Add(this.btnDisplayGraph);
            this.Name = "FrmVisualizer";
            this.Text = "FrmVisualizer";
            this.Load += new System.EventHandler(this.FrmVisualizer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegisteredGraphs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDisplayReactors;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvRegisteredGraphs;
        private System.Windows.Forms.Button btnGetGraphNodes;
        private System.Windows.Forms.Button btnGetRegisteredGraphs;
        private System.Windows.Forms.Button btnDisplayGraph;
        private System.Windows.Forms.Button btnClassGraph;
    }
}