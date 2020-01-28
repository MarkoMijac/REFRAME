namespace ReframeTools.GUI
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbAnalysisLevel = new System.Windows.Forms.GroupBox();
            this.rbAssemblyLevel = new System.Windows.Forms.RadioButton();
            this.rbNamespaceLevel = new System.Windows.Forms.RadioButton();
            this.rbClassLevel = new System.Windows.Forms.RadioButton();
            this.rbClassMemberLevel = new System.Windows.Forms.RadioButton();
            this.rbObjectLevel = new System.Windows.Forms.RadioButton();
            this.rbObjectMemberLevel = new System.Windows.Forms.RadioButton();
            this.gbFilterByParent = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.clbObjectNodes = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.clbClassNodes = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.clbNamespaceNodes = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.clbAssemblyNodes = new System.Windows.Forms.CheckedListBox();
            this.gbAnalysisLevel.SuspendLayout();
            this.gbFilterByParent.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(310, 672);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(391, 672);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
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
            this.gbAnalysisLevel.Size = new System.Drawing.Size(454, 80);
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
            this.gbFilterByParent.Controls.Add(this.label5);
            this.gbFilterByParent.Controls.Add(this.clbObjectNodes);
            this.gbFilterByParent.Controls.Add(this.label4);
            this.gbFilterByParent.Controls.Add(this.clbClassNodes);
            this.gbFilterByParent.Controls.Add(this.label3);
            this.gbFilterByParent.Controls.Add(this.clbNamespaceNodes);
            this.gbFilterByParent.Controls.Add(this.label2);
            this.gbFilterByParent.Controls.Add(this.clbAssemblyNodes);
            this.gbFilterByParent.Location = new System.Drawing.Point(12, 97);
            this.gbFilterByParent.Name = "gbFilterByParent";
            this.gbFilterByParent.Size = new System.Drawing.Size(454, 569);
            this.gbFilterByParent.TabIndex = 5;
            this.gbFilterByParent.TabStop = false;
            this.gbFilterByParent.Text = "Filter by parent:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(20, 427);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Object nodes:";
            // 
            // clbObjectNodes
            // 
            this.clbObjectNodes.FormattingEnabled = true;
            this.clbObjectNodes.Location = new System.Drawing.Point(19, 443);
            this.clbObjectNodes.Name = "clbObjectNodes";
            this.clbObjectNodes.Size = new System.Drawing.Size(419, 109);
            this.clbObjectNodes.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(20, 294);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Class nodes:";
            // 
            // clbClassNodes
            // 
            this.clbClassNodes.FormattingEnabled = true;
            this.clbClassNodes.Location = new System.Drawing.Point(19, 310);
            this.clbClassNodes.Name = "clbClassNodes";
            this.clbClassNodes.Size = new System.Drawing.Size(419, 109);
            this.clbClassNodes.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(20, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Namespace nodes:";
            // 
            // clbNamespaceNodes
            // 
            this.clbNamespaceNodes.FormattingEnabled = true;
            this.clbNamespaceNodes.Location = new System.Drawing.Point(19, 175);
            this.clbNamespaceNodes.Name = "clbNamespaceNodes";
            this.clbNamespaceNodes.Size = new System.Drawing.Size(419, 109);
            this.clbNamespaceNodes.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(21, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Assembly nodes:";
            // 
            // clbAssemblyNodes
            // 
            this.clbAssemblyNodes.FormattingEnabled = true;
            this.clbAssemblyNodes.Location = new System.Drawing.Point(20, 35);
            this.clbAssemblyNodes.Name = "clbAssemblyNodes";
            this.clbAssemblyNodes.Size = new System.Drawing.Size(418, 109);
            this.clbAssemblyNodes.TabIndex = 0;
            // 
            // FrmAdvancedAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(478, 704);
            this.Controls.Add(this.gbFilterByParent);
            this.Controls.Add(this.gbAnalysisLevel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Name = "FrmAdvancedAnalysis";
            this.Text = "Options:";
            this.Load += new System.EventHandler(this.FrmOptions_Load);
            this.gbAnalysisLevel.ResumeLayout(false);
            this.gbAnalysisLevel.PerformLayout();
            this.gbFilterByParent.ResumeLayout(false);
            this.gbFilterByParent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gbFilterByParent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
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
    }
}