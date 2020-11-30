namespace ReframeTools.GUI
{
    partial class FrmNamespaceFilter
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
            this.btnApply = new System.Windows.Forms.Button();
            this.gbFilterByParent = new System.Windows.Forms.GroupBox();
            this.btnDeselectAllNamespaces = new System.Windows.Forms.Button();
            this.btnSelectAllNamespaces = new System.Windows.Forms.Button();
            this.btnDeselectAllAssemblies = new System.Windows.Forms.Button();
            this.btnSelecteAllAssemblies = new System.Windows.Forms.Button();
            this.lblNamespaces = new System.Windows.Forms.Label();
            this.clbNamespaceNodes = new System.Windows.Forms.CheckedListBox();
            this.lblAssemblies = new System.Windows.Forms.Label();
            this.clbAssemblyNodes = new System.Windows.Forms.CheckedListBox();
            this.gbFilterByParent.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.LightCoral;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Location = new System.Drawing.Point(448, 322);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(102, 23);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "OK";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // gbFilterByParent
            // 
            this.gbFilterByParent.Controls.Add(this.btnDeselectAllNamespaces);
            this.gbFilterByParent.Controls.Add(this.btnSelectAllNamespaces);
            this.gbFilterByParent.Controls.Add(this.btnDeselectAllAssemblies);
            this.gbFilterByParent.Controls.Add(this.btnSelecteAllAssemblies);
            this.gbFilterByParent.Controls.Add(this.lblNamespaces);
            this.gbFilterByParent.Controls.Add(this.clbNamespaceNodes);
            this.gbFilterByParent.Controls.Add(this.lblAssemblies);
            this.gbFilterByParent.Controls.Add(this.clbAssemblyNodes);
            this.gbFilterByParent.Location = new System.Drawing.Point(12, 12);
            this.gbFilterByParent.Name = "gbFilterByParent";
            this.gbFilterByParent.Size = new System.Drawing.Size(538, 304);
            this.gbFilterByParent.TabIndex = 5;
            this.gbFilterByParent.TabStop = false;
            this.gbFilterByParent.Text = "Filter by parent:";
            // 
            // btnDeselectAllNamespaces
            // 
            this.btnDeselectAllNamespaces.BackColor = System.Drawing.Color.LightCoral;
            this.btnDeselectAllNamespaces.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeselectAllNamespaces.Location = new System.Drawing.Point(444, 204);
            this.btnDeselectAllNamespaces.Name = "btnDeselectAllNamespaces";
            this.btnDeselectAllNamespaces.Size = new System.Drawing.Size(75, 23);
            this.btnDeselectAllNamespaces.TabIndex = 11;
            this.btnDeselectAllNamespaces.Text = "Deselect All";
            this.btnDeselectAllNamespaces.UseVisualStyleBackColor = false;
            this.btnDeselectAllNamespaces.Click += new System.EventHandler(this.btnDeselectAllNamespaces_Click);
            // 
            // btnSelectAllNamespaces
            // 
            this.btnSelectAllNamespaces.BackColor = System.Drawing.Color.LightCoral;
            this.btnSelectAllNamespaces.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAllNamespaces.Location = new System.Drawing.Point(444, 175);
            this.btnSelectAllNamespaces.Name = "btnSelectAllNamespaces";
            this.btnSelectAllNamespaces.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAllNamespaces.TabIndex = 10;
            this.btnSelectAllNamespaces.Text = "Select All";
            this.btnSelectAllNamespaces.UseVisualStyleBackColor = false;
            this.btnSelectAllNamespaces.Click += new System.EventHandler(this.btnSelectAllNamespaces_Click);
            // 
            // btnDeselectAllAssemblies
            // 
            this.btnDeselectAllAssemblies.BackColor = System.Drawing.Color.LightCoral;
            this.btnDeselectAllAssemblies.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeselectAllAssemblies.Location = new System.Drawing.Point(444, 64);
            this.btnDeselectAllAssemblies.Name = "btnDeselectAllAssemblies";
            this.btnDeselectAllAssemblies.Size = new System.Drawing.Size(75, 23);
            this.btnDeselectAllAssemblies.TabIndex = 9;
            this.btnDeselectAllAssemblies.Text = "Deselect All";
            this.btnDeselectAllAssemblies.UseVisualStyleBackColor = false;
            this.btnDeselectAllAssemblies.Click += new System.EventHandler(this.btnDeselectAllAssemblies_Click);
            // 
            // btnSelecteAllAssemblies
            // 
            this.btnSelecteAllAssemblies.BackColor = System.Drawing.Color.LightCoral;
            this.btnSelecteAllAssemblies.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecteAllAssemblies.Location = new System.Drawing.Point(444, 35);
            this.btnSelecteAllAssemblies.Name = "btnSelecteAllAssemblies";
            this.btnSelecteAllAssemblies.Size = new System.Drawing.Size(75, 23);
            this.btnSelecteAllAssemblies.TabIndex = 8;
            this.btnSelecteAllAssemblies.Text = "Select All";
            this.btnSelecteAllAssemblies.UseVisualStyleBackColor = false;
            this.btnSelecteAllAssemblies.Click += new System.EventHandler(this.btnSelecteAllAssemblies_Click);
            // 
            // lblNamespaces
            // 
            this.lblNamespaces.AutoSize = true;
            this.lblNamespaces.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblNamespaces.Location = new System.Drawing.Point(20, 159);
            this.lblNamespaces.Name = "lblNamespaces";
            this.lblNamespaces.Size = new System.Drawing.Size(115, 13);
            this.lblNamespaces.TabIndex = 3;
            this.lblNamespaces.Text = "Namespace nodes:";
            // 
            // clbNamespaceNodes
            // 
            this.clbNamespaceNodes.FormattingEnabled = true;
            this.clbNamespaceNodes.Location = new System.Drawing.Point(19, 175);
            this.clbNamespaceNodes.Name = "clbNamespaceNodes";
            this.clbNamespaceNodes.Size = new System.Drawing.Size(419, 109);
            this.clbNamespaceNodes.TabIndex = 2;
            this.clbNamespaceNodes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbNamespaceNodes_ItemCheck);
            // 
            // lblAssemblies
            // 
            this.lblAssemblies.AutoSize = true;
            this.lblAssemblies.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblAssemblies.Location = new System.Drawing.Point(21, 19);
            this.lblAssemblies.Name = "lblAssemblies";
            this.lblAssemblies.Size = new System.Drawing.Size(101, 13);
            this.lblAssemblies.TabIndex = 1;
            this.lblAssemblies.Text = "Assembly nodes:";
            // 
            // clbAssemblyNodes
            // 
            this.clbAssemblyNodes.FormattingEnabled = true;
            this.clbAssemblyNodes.Location = new System.Drawing.Point(20, 35);
            this.clbAssemblyNodes.Name = "clbAssemblyNodes";
            this.clbAssemblyNodes.Size = new System.Drawing.Size(418, 109);
            this.clbAssemblyNodes.TabIndex = 0;
            this.clbAssemblyNodes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbAssemblyNodes_ItemCheck);
            // 
            // FrmNamespaceFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(564, 360);
            this.Controls.Add(this.gbFilterByParent);
            this.Controls.Add(this.btnApply);
            this.Name = "FrmNamespaceFilter";
            this.Text = "Options:";
            this.Load += new System.EventHandler(this.FrmOptions_Load);
            this.gbFilterByParent.ResumeLayout(false);
            this.gbFilterByParent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblAssemblies;
        protected System.Windows.Forms.CheckedListBox clbAssemblyNodes;
        private System.Windows.Forms.Button btnDeselectAllAssemblies;
        private System.Windows.Forms.Button btnSelecteAllAssemblies;
        protected System.Windows.Forms.Button btnApply;
        protected System.Windows.Forms.GroupBox gbFilterByParent;
        private System.Windows.Forms.Button btnDeselectAllNamespaces;
        private System.Windows.Forms.Button btnSelectAllNamespaces;
        private System.Windows.Forms.Label lblNamespaces;
        protected System.Windows.Forms.CheckedListBox clbNamespaceNodes;
    }
}