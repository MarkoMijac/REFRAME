namespace ReframeTools.GUI
{
    partial class FrmUpdateProcessInfo
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
            this.dgvUpdateInfo = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtInitialNodeOwnerObject = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtInitialNodeMemberName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtInitialNodeIdentifier = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUpdateCause = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtUpdateDuration = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtUpdateEndedAt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUpdateStartedAt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUpdateSuccessful = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUpdatedNodesCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGraphTotalNodeCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGraphIdentifier = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtInitialNodeCurrentValue = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtInitialNodePreviousValue = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpdateInfo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvUpdateInfo
            // 
            this.dgvUpdateInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUpdateInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUpdateInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUpdateInfo.Location = new System.Drawing.Point(12, 233);
            this.dgvUpdateInfo.Name = "dgvUpdateInfo";
            this.dgvUpdateInfo.RowHeadersVisible = false;
            this.dgvUpdateInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUpdateInfo.Size = new System.Drawing.Size(828, 225);
            this.dgvUpdateInfo.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtInitialNodePreviousValue);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtInitialNodeCurrentValue);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtInitialNodeOwnerObject);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtInitialNodeMemberName);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtInitialNodeIdentifier);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtUpdateCause);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtUpdateDuration);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtUpdateEndedAt);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtUpdateStartedAt);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtUpdateSuccessful);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtUpdatedNodesCount);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtGraphTotalNodeCount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtGraphIdentifier);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(828, 191);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basic update process information";
            // 
            // txtInitialNodeOwnerObject
            // 
            this.txtInitialNodeOwnerObject.BackColor = System.Drawing.Color.MistyRose;
            this.txtInitialNodeOwnerObject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInitialNodeOwnerObject.Location = new System.Drawing.Point(596, 97);
            this.txtInitialNodeOwnerObject.Name = "txtInitialNodeOwnerObject";
            this.txtInitialNodeOwnerObject.Size = new System.Drawing.Size(179, 20);
            this.txtInitialNodeOwnerObject.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(485, 99);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Initial node owner:";
            // 
            // txtInitialNodeMemberName
            // 
            this.txtInitialNodeMemberName.BackColor = System.Drawing.Color.MistyRose;
            this.txtInitialNodeMemberName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInitialNodeMemberName.Location = new System.Drawing.Point(596, 71);
            this.txtInitialNodeMemberName.Name = "txtInitialNodeMemberName";
            this.txtInitialNodeMemberName.Size = new System.Drawing.Size(179, 20);
            this.txtInitialNodeMemberName.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(485, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Initial node member:";
            // 
            // txtInitialNodeIdentifier
            // 
            this.txtInitialNodeIdentifier.BackColor = System.Drawing.Color.MistyRose;
            this.txtInitialNodeIdentifier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInitialNodeIdentifier.Location = new System.Drawing.Point(596, 45);
            this.txtInitialNodeIdentifier.Name = "txtInitialNodeIdentifier";
            this.txtInitialNodeIdentifier.Size = new System.Drawing.Size(179, 20);
            this.txtInitialNodeIdentifier.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(485, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Initial node identifier:";
            // 
            // txtUpdateCause
            // 
            this.txtUpdateCause.BackColor = System.Drawing.Color.MistyRose;
            this.txtUpdateCause.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUpdateCause.Location = new System.Drawing.Point(596, 17);
            this.txtUpdateCause.Name = "txtUpdateCause";
            this.txtUpdateCause.Size = new System.Drawing.Size(179, 20);
            this.txtUpdateCause.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(485, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Update cause:";
            // 
            // txtUpdateDuration
            // 
            this.txtUpdateDuration.BackColor = System.Drawing.Color.MistyRose;
            this.txtUpdateDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUpdateDuration.Location = new System.Drawing.Point(310, 70);
            this.txtUpdateDuration.Name = "txtUpdateDuration";
            this.txtUpdateDuration.Size = new System.Drawing.Size(154, 20);
            this.txtUpdateDuration.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(248, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Duration:";
            // 
            // txtUpdateEndedAt
            // 
            this.txtUpdateEndedAt.BackColor = System.Drawing.Color.MistyRose;
            this.txtUpdateEndedAt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUpdateEndedAt.Location = new System.Drawing.Point(310, 44);
            this.txtUpdateEndedAt.Name = "txtUpdateEndedAt";
            this.txtUpdateEndedAt.Size = new System.Drawing.Size(154, 20);
            this.txtUpdateEndedAt.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(248, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Ended at:";
            // 
            // txtUpdateStartedAt
            // 
            this.txtUpdateStartedAt.BackColor = System.Drawing.Color.MistyRose;
            this.txtUpdateStartedAt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUpdateStartedAt.Location = new System.Drawing.Point(310, 18);
            this.txtUpdateStartedAt.Name = "txtUpdateStartedAt";
            this.txtUpdateStartedAt.Size = new System.Drawing.Size(154, 20);
            this.txtUpdateStartedAt.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(248, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Started at:";
            // 
            // txtUpdateSuccessful
            // 
            this.txtUpdateSuccessful.BackColor = System.Drawing.Color.MistyRose;
            this.txtUpdateSuccessful.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUpdateSuccessful.Location = new System.Drawing.Point(130, 95);
            this.txtUpdateSuccessful.Name = "txtUpdateSuccessful";
            this.txtUpdateSuccessful.Size = new System.Drawing.Size(100, 20);
            this.txtUpdateSuccessful.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Update successful:";
            // 
            // txtUpdatedNodesCount
            // 
            this.txtUpdatedNodesCount.BackColor = System.Drawing.Color.MistyRose;
            this.txtUpdatedNodesCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUpdatedNodesCount.Location = new System.Drawing.Point(130, 69);
            this.txtUpdatedNodesCount.Name = "txtUpdatedNodesCount";
            this.txtUpdatedNodesCount.Size = new System.Drawing.Size(100, 20);
            this.txtUpdatedNodesCount.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Updated nodes count:";
            // 
            // txtGraphTotalNodeCount
            // 
            this.txtGraphTotalNodeCount.BackColor = System.Drawing.Color.MistyRose;
            this.txtGraphTotalNodeCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGraphTotalNodeCount.Location = new System.Drawing.Point(130, 43);
            this.txtGraphTotalNodeCount.Name = "txtGraphTotalNodeCount";
            this.txtGraphTotalNodeCount.Size = new System.Drawing.Size(100, 20);
            this.txtGraphTotalNodeCount.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Total node count:";
            // 
            // txtGraphIdentifier
            // 
            this.txtGraphIdentifier.BackColor = System.Drawing.Color.MistyRose;
            this.txtGraphIdentifier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGraphIdentifier.Location = new System.Drawing.Point(130, 17);
            this.txtGraphIdentifier.Name = "txtGraphIdentifier";
            this.txtGraphIdentifier.Size = new System.Drawing.Size(100, 20);
            this.txtGraphIdentifier.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Graph identifier:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label12.Location = new System.Drawing.Point(12, 217);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "Updated nodes:";
            // 
            // txtInitialNodeCurrentValue
            // 
            this.txtInitialNodeCurrentValue.BackColor = System.Drawing.Color.MistyRose;
            this.txtInitialNodeCurrentValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInitialNodeCurrentValue.Location = new System.Drawing.Point(596, 123);
            this.txtInitialNodeCurrentValue.Name = "txtInitialNodeCurrentValue";
            this.txtInitialNodeCurrentValue.Size = new System.Drawing.Size(179, 20);
            this.txtInitialNodeCurrentValue.TabIndex = 23;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(485, 125);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(73, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "Current value:";
            // 
            // txtInitialNodePreviousValue
            // 
            this.txtInitialNodePreviousValue.BackColor = System.Drawing.Color.MistyRose;
            this.txtInitialNodePreviousValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInitialNodePreviousValue.Location = new System.Drawing.Point(596, 149);
            this.txtInitialNodePreviousValue.Name = "txtInitialNodePreviousValue";
            this.txtInitialNodePreviousValue.Size = new System.Drawing.Size(179, 20);
            this.txtInitialNodePreviousValue.TabIndex = 25;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(485, 151);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 13);
            this.label14.TabIndex = 24;
            this.label14.Text = "Previous value:";
            // 
            // FrmUpdateProcessInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(852, 470);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvUpdateInfo);
            this.Name = "FrmUpdateProcessInfo";
            this.Text = "Update process information";
            this.Load += new System.EventHandler(this.FrmUpdateProcessInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpdateInfo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUpdateInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtGraphTotalNodeCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGraphIdentifier;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUpdatedNodesCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUpdateDuration;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtUpdateEndedAt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUpdateStartedAt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUpdateSuccessful;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtInitialNodeOwnerObject;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtInitialNodeMemberName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtInitialNodeIdentifier;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtUpdateCause;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtInitialNodePreviousValue;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtInitialNodeCurrentValue;
        private System.Windows.Forms.Label label13;
    }
}