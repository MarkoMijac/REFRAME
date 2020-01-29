﻿namespace ReframeTools.GUI
{
    partial class FrmFilterAnalysis
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
            this.gbAnalysisLevel = new System.Windows.Forms.GroupBox();
            this.rbAssemblyLevel = new System.Windows.Forms.RadioButton();
            this.rbNamespaceLevel = new System.Windows.Forms.RadioButton();
            this.rbClassLevel = new System.Windows.Forms.RadioButton();
            this.rbClassMemberLevel = new System.Windows.Forms.RadioButton();
            this.rbObjectLevel = new System.Windows.Forms.RadioButton();
            this.rbObjectMemberLevel = new System.Windows.Forms.RadioButton();
            this.gbFilterByParent = new System.Windows.Forms.GroupBox();
            this.btnDeselectAllObjects = new System.Windows.Forms.Button();
            this.btnSelectAllObjects = new System.Windows.Forms.Button();
            this.btnDeselectAllClasses = new System.Windows.Forms.Button();
            this.btnSelectAllClasses = new System.Windows.Forms.Button();
            this.btnDeselectAllNamespaces = new System.Windows.Forms.Button();
            this.btnSelectAllNamespaces = new System.Windows.Forms.Button();
            this.btnDeselectAllAssemblies = new System.Windows.Forms.Button();
            this.btnSelecteAllAssemblies = new System.Windows.Forms.Button();
            this.lblObjectNodes = new System.Windows.Forms.Label();
            this.clbObjectNodes = new System.Windows.Forms.CheckedListBox();
            this.lblClassNodes = new System.Windows.Forms.Label();
            this.clbClassNodes = new System.Windows.Forms.CheckedListBox();
            this.lblNamespaces = new System.Windows.Forms.Label();
            this.clbNamespaceNodes = new System.Windows.Forms.CheckedListBox();
            this.lblAssemblies = new System.Windows.Forms.Label();
            this.clbAssemblyNodes = new System.Windows.Forms.CheckedListBox();
            this.gbAnalysisLevel.SuspendLayout();
            this.gbFilterByParent.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(209, 654);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(102, 23);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // gbAnalysisLevel
            // 
            this.gbAnalysisLevel.Controls.Add(this.rbAssemblyLevel);
            this.gbAnalysisLevel.Controls.Add(this.rbNamespaceLevel);
            this.gbAnalysisLevel.Controls.Add(this.rbClassLevel);
            this.gbAnalysisLevel.Controls.Add(this.rbClassMemberLevel);
            this.gbAnalysisLevel.Controls.Add(this.rbObjectLevel);
            this.gbAnalysisLevel.Controls.Add(this.rbObjectMemberLevel);
            this.gbAnalysisLevel.Enabled = false;
            this.gbAnalysisLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gbAnalysisLevel.Location = new System.Drawing.Point(12, 12);
            this.gbAnalysisLevel.Name = "gbAnalysisLevel";
            this.gbAnalysisLevel.Size = new System.Drawing.Size(538, 66);
            this.gbAnalysisLevel.TabIndex = 4;
            this.gbAnalysisLevel.TabStop = false;
            this.gbAnalysisLevel.Text = "Analysis level:";
            // 
            // rbAssemblyLevel
            // 
            this.rbAssemblyLevel.AutoSize = true;
            this.rbAssemblyLevel.Location = new System.Drawing.Point(293, 42);
            this.rbAssemblyLevel.Name = "rbAssemblyLevel";
            this.rbAssemblyLevel.Size = new System.Drawing.Size(94, 17);
            this.rbAssemblyLevel.TabIndex = 10;
            this.rbAssemblyLevel.Text = "Assembly-level";
            this.rbAssemblyLevel.UseVisualStyleBackColor = true;
            // 
            // rbNamespaceLevel
            // 
            this.rbNamespaceLevel.AutoSize = true;
            this.rbNamespaceLevel.Location = new System.Drawing.Point(293, 19);
            this.rbNamespaceLevel.Name = "rbNamespaceLevel";
            this.rbNamespaceLevel.Size = new System.Drawing.Size(107, 17);
            this.rbNamespaceLevel.TabIndex = 9;
            this.rbNamespaceLevel.Text = "Namespace-level";
            this.rbNamespaceLevel.UseVisualStyleBackColor = true;
            // 
            // rbClassLevel
            // 
            this.rbClassLevel.AutoSize = true;
            this.rbClassLevel.Location = new System.Drawing.Point(147, 42);
            this.rbClassLevel.Name = "rbClassLevel";
            this.rbClassLevel.Size = new System.Drawing.Size(75, 17);
            this.rbClassLevel.TabIndex = 8;
            this.rbClassLevel.Text = "Class-level";
            this.rbClassLevel.UseVisualStyleBackColor = true;
            // 
            // rbClassMemberLevel
            // 
            this.rbClassMemberLevel.AutoSize = true;
            this.rbClassMemberLevel.Location = new System.Drawing.Point(147, 19);
            this.rbClassMemberLevel.Name = "rbClassMemberLevel";
            this.rbClassMemberLevel.Size = new System.Drawing.Size(113, 17);
            this.rbClassMemberLevel.TabIndex = 7;
            this.rbClassMemberLevel.Text = "ClassMember-level";
            this.rbClassMemberLevel.UseVisualStyleBackColor = true;
            // 
            // rbObjectLevel
            // 
            this.rbObjectLevel.AutoSize = true;
            this.rbObjectLevel.Location = new System.Drawing.Point(6, 42);
            this.rbObjectLevel.Name = "rbObjectLevel";
            this.rbObjectLevel.Size = new System.Drawing.Size(81, 17);
            this.rbObjectLevel.TabIndex = 6;
            this.rbObjectLevel.Text = "Object-level";
            this.rbObjectLevel.UseVisualStyleBackColor = true;
            // 
            // rbObjectMemberLevel
            // 
            this.rbObjectMemberLevel.AutoSize = true;
            this.rbObjectMemberLevel.Checked = true;
            this.rbObjectMemberLevel.Location = new System.Drawing.Point(6, 19);
            this.rbObjectMemberLevel.Name = "rbObjectMemberLevel";
            this.rbObjectMemberLevel.Size = new System.Drawing.Size(119, 17);
            this.rbObjectMemberLevel.TabIndex = 5;
            this.rbObjectMemberLevel.TabStop = true;
            this.rbObjectMemberLevel.Text = "ObjectMember-level";
            this.rbObjectMemberLevel.UseVisualStyleBackColor = true;
            // 
            // gbFilterByParent
            // 
            this.gbFilterByParent.Controls.Add(this.btnDeselectAllObjects);
            this.gbFilterByParent.Controls.Add(this.btnSelectAllObjects);
            this.gbFilterByParent.Controls.Add(this.btnDeselectAllClasses);
            this.gbFilterByParent.Controls.Add(this.btnSelectAllClasses);
            this.gbFilterByParent.Controls.Add(this.btnDeselectAllNamespaces);
            this.gbFilterByParent.Controls.Add(this.btnSelectAllNamespaces);
            this.gbFilterByParent.Controls.Add(this.btnDeselectAllAssemblies);
            this.gbFilterByParent.Controls.Add(this.btnSelecteAllAssemblies);
            this.gbFilterByParent.Controls.Add(this.lblObjectNodes);
            this.gbFilterByParent.Controls.Add(this.clbObjectNodes);
            this.gbFilterByParent.Controls.Add(this.lblClassNodes);
            this.gbFilterByParent.Controls.Add(this.clbClassNodes);
            this.gbFilterByParent.Controls.Add(this.lblNamespaces);
            this.gbFilterByParent.Controls.Add(this.clbNamespaceNodes);
            this.gbFilterByParent.Controls.Add(this.lblAssemblies);
            this.gbFilterByParent.Controls.Add(this.clbAssemblyNodes);
            this.gbFilterByParent.Location = new System.Drawing.Point(12, 84);
            this.gbFilterByParent.Name = "gbFilterByParent";
            this.gbFilterByParent.Size = new System.Drawing.Size(538, 564);
            this.gbFilterByParent.TabIndex = 5;
            this.gbFilterByParent.TabStop = false;
            this.gbFilterByParent.Text = "Filter by parent:";
            // 
            // btnDeselectAllObjects
            // 
            this.btnDeselectAllObjects.Location = new System.Drawing.Point(444, 472);
            this.btnDeselectAllObjects.Name = "btnDeselectAllObjects";
            this.btnDeselectAllObjects.Size = new System.Drawing.Size(75, 23);
            this.btnDeselectAllObjects.TabIndex = 15;
            this.btnDeselectAllObjects.Text = "Deselect All";
            this.btnDeselectAllObjects.UseVisualStyleBackColor = true;
            this.btnDeselectAllObjects.Click += new System.EventHandler(this.btnDeselectAllObjects_Click);
            // 
            // btnSelectAllObjects
            // 
            this.btnSelectAllObjects.Location = new System.Drawing.Point(444, 443);
            this.btnSelectAllObjects.Name = "btnSelectAllObjects";
            this.btnSelectAllObjects.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAllObjects.TabIndex = 14;
            this.btnSelectAllObjects.Text = "Select All";
            this.btnSelectAllObjects.UseVisualStyleBackColor = true;
            this.btnSelectAllObjects.Click += new System.EventHandler(this.btnSelectAllObjects_Click);
            // 
            // btnDeselectAllClasses
            // 
            this.btnDeselectAllClasses.Location = new System.Drawing.Point(444, 339);
            this.btnDeselectAllClasses.Name = "btnDeselectAllClasses";
            this.btnDeselectAllClasses.Size = new System.Drawing.Size(75, 23);
            this.btnDeselectAllClasses.TabIndex = 13;
            this.btnDeselectAllClasses.Text = "Deselect All";
            this.btnDeselectAllClasses.UseVisualStyleBackColor = true;
            this.btnDeselectAllClasses.Click += new System.EventHandler(this.btnDeselectAllClasses_Click);
            // 
            // btnSelectAllClasses
            // 
            this.btnSelectAllClasses.Location = new System.Drawing.Point(444, 310);
            this.btnSelectAllClasses.Name = "btnSelectAllClasses";
            this.btnSelectAllClasses.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAllClasses.TabIndex = 12;
            this.btnSelectAllClasses.Text = "Select All";
            this.btnSelectAllClasses.UseVisualStyleBackColor = true;
            this.btnSelectAllClasses.Click += new System.EventHandler(this.btnSelectAllClasses_Click);
            // 
            // btnDeselectAllNamespaces
            // 
            this.btnDeselectAllNamespaces.Location = new System.Drawing.Point(444, 204);
            this.btnDeselectAllNamespaces.Name = "btnDeselectAllNamespaces";
            this.btnDeselectAllNamespaces.Size = new System.Drawing.Size(75, 23);
            this.btnDeselectAllNamespaces.TabIndex = 11;
            this.btnDeselectAllNamespaces.Text = "Deselect All";
            this.btnDeselectAllNamespaces.UseVisualStyleBackColor = true;
            this.btnDeselectAllNamespaces.Click += new System.EventHandler(this.btnDeselectAllNamespaces_Click);
            // 
            // btnSelectAllNamespaces
            // 
            this.btnSelectAllNamespaces.Location = new System.Drawing.Point(444, 175);
            this.btnSelectAllNamespaces.Name = "btnSelectAllNamespaces";
            this.btnSelectAllNamespaces.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAllNamespaces.TabIndex = 10;
            this.btnSelectAllNamespaces.Text = "Select All";
            this.btnSelectAllNamespaces.UseVisualStyleBackColor = true;
            this.btnSelectAllNamespaces.Click += new System.EventHandler(this.btnSelectAllNamespaces_Click);
            // 
            // btnDeselectAllAssemblies
            // 
            this.btnDeselectAllAssemblies.Location = new System.Drawing.Point(444, 64);
            this.btnDeselectAllAssemblies.Name = "btnDeselectAllAssemblies";
            this.btnDeselectAllAssemblies.Size = new System.Drawing.Size(75, 23);
            this.btnDeselectAllAssemblies.TabIndex = 9;
            this.btnDeselectAllAssemblies.Text = "Deselect All";
            this.btnDeselectAllAssemblies.UseVisualStyleBackColor = true;
            this.btnDeselectAllAssemblies.Click += new System.EventHandler(this.btnDeselectAllAssemblies_Click);
            // 
            // btnSelecteAllAssemblies
            // 
            this.btnSelecteAllAssemblies.Location = new System.Drawing.Point(444, 35);
            this.btnSelecteAllAssemblies.Name = "btnSelecteAllAssemblies";
            this.btnSelecteAllAssemblies.Size = new System.Drawing.Size(75, 23);
            this.btnSelecteAllAssemblies.TabIndex = 8;
            this.btnSelecteAllAssemblies.Text = "Select All";
            this.btnSelecteAllAssemblies.UseVisualStyleBackColor = true;
            this.btnSelecteAllAssemblies.Click += new System.EventHandler(this.btnSelecteAllAssemblies_Click);
            // 
            // lblObjectNodes
            // 
            this.lblObjectNodes.AutoSize = true;
            this.lblObjectNodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblObjectNodes.Location = new System.Drawing.Point(20, 427);
            this.lblObjectNodes.Name = "lblObjectNodes";
            this.lblObjectNodes.Size = new System.Drawing.Size(86, 13);
            this.lblObjectNodes.TabIndex = 7;
            this.lblObjectNodes.Text = "Object nodes:";
            // 
            // clbObjectNodes
            // 
            this.clbObjectNodes.FormattingEnabled = true;
            this.clbObjectNodes.Location = new System.Drawing.Point(19, 443);
            this.clbObjectNodes.Name = "clbObjectNodes";
            this.clbObjectNodes.Size = new System.Drawing.Size(419, 109);
            this.clbObjectNodes.TabIndex = 6;
            this.clbObjectNodes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbObjectNodes_ItemCheck);
            // 
            // lblClassNodes
            // 
            this.lblClassNodes.AutoSize = true;
            this.lblClassNodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblClassNodes.Location = new System.Drawing.Point(20, 294);
            this.lblClassNodes.Name = "lblClassNodes";
            this.lblClassNodes.Size = new System.Drawing.Size(79, 13);
            this.lblClassNodes.TabIndex = 5;
            this.lblClassNodes.Text = "Class nodes:";
            // 
            // clbClassNodes
            // 
            this.clbClassNodes.FormattingEnabled = true;
            this.clbClassNodes.Location = new System.Drawing.Point(19, 310);
            this.clbClassNodes.Name = "clbClassNodes";
            this.clbClassNodes.Size = new System.Drawing.Size(419, 109);
            this.clbClassNodes.TabIndex = 4;
            this.clbClassNodes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbClassNodes_ItemCheck);
            this.clbClassNodes.SelectedIndexChanged += new System.EventHandler(this.clbClassNodes_SelectedIndexChanged);
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
            this.clbNamespaceNodes.SelectedIndexChanged += new System.EventHandler(this.clbNamespaceNodes_SelectedIndexChanged);
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
            this.clbAssemblyNodes.SelectedIndexChanged += new System.EventHandler(this.clbAssemblyNodes_SelectedIndexChanged);
            // 
            // FrmFilterAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(564, 683);
            this.Controls.Add(this.gbFilterByParent);
            this.Controls.Add(this.gbAnalysisLevel);
            this.Controls.Add(this.btnApply);
            this.Name = "FrmFilterAnalysis";
            this.Text = "Options:";
            this.Load += new System.EventHandler(this.FrmOptions_Load);
            this.gbAnalysisLevel.ResumeLayout(false);
            this.gbAnalysisLevel.PerformLayout();
            this.gbFilterByParent.ResumeLayout(false);
            this.gbFilterByParent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblObjectNodes;
        private System.Windows.Forms.Label lblClassNodes;
        private System.Windows.Forms.Label lblNamespaces;
        private System.Windows.Forms.Label lblAssemblies;
        protected System.Windows.Forms.CheckedListBox clbObjectNodes;
        protected System.Windows.Forms.CheckedListBox clbClassNodes;
        protected System.Windows.Forms.CheckedListBox clbNamespaceNodes;
        protected System.Windows.Forms.CheckedListBox clbAssemblyNodes;
        protected System.Windows.Forms.GroupBox gbAnalysisLevel;
        protected System.Windows.Forms.RadioButton rbClassLevel;
        protected System.Windows.Forms.RadioButton rbClassMemberLevel;
        protected System.Windows.Forms.RadioButton rbObjectLevel;
        protected System.Windows.Forms.RadioButton rbObjectMemberLevel;
        protected System.Windows.Forms.RadioButton rbAssemblyLevel;
        protected System.Windows.Forms.RadioButton rbNamespaceLevel;
        private System.Windows.Forms.Button btnDeselectAllObjects;
        private System.Windows.Forms.Button btnSelectAllObjects;
        private System.Windows.Forms.Button btnDeselectAllClasses;
        private System.Windows.Forms.Button btnSelectAllClasses;
        private System.Windows.Forms.Button btnDeselectAllNamespaces;
        private System.Windows.Forms.Button btnSelectAllNamespaces;
        private System.Windows.Forms.Button btnDeselectAllAssemblies;
        private System.Windows.Forms.Button btnSelecteAllAssemblies;
        protected System.Windows.Forms.Button btnApply;
        protected System.Windows.Forms.GroupBox gbFilterByParent;
    }
}