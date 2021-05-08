
namespace SNOMEDCTSelector
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtSCTQuery = new System.Windows.Forms.TextBox();
            this.btnSCTQuery = new System.Windows.Forms.Button();
            this.btnShowCandIDs = new System.Windows.Forms.Button();
            this.gbContextQuestions = new System.Windows.Forms.GroupBox();
            this.pnlContextQuestions = new System.Windows.Forms.Panel();
            this.gbChildren = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlChildren = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTH = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblIteration = new System.Windows.Forms.Label();
            this.lblNumOfChildren = new System.Windows.Forms.Label();
            this.lblNumOfCandidates = new System.Windows.Forms.Label();
            this.lblReductionPercent = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSaveStates = new System.Windows.Forms.Button();
            this.tvNodes = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbContextQuestions.SuspendLayout();
            this.gbChildren.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SNOMED CT ID:";
            // 
            // txtSCTQuery
            // 
            this.txtSCTQuery.Location = new System.Drawing.Point(104, 19);
            this.txtSCTQuery.Name = "txtSCTQuery";
            this.txtSCTQuery.Size = new System.Drawing.Size(273, 20);
            this.txtSCTQuery.TabIndex = 1;
            // 
            // btnSCTQuery
            // 
            this.btnSCTQuery.Location = new System.Drawing.Point(104, 45);
            this.btnSCTQuery.Name = "btnSCTQuery";
            this.btnSCTQuery.Size = new System.Drawing.Size(273, 26);
            this.btnSCTQuery.TabIndex = 2;
            this.btnSCTQuery.Text = "Search for SNOMED CT ID";
            this.btnSCTQuery.UseVisualStyleBackColor = true;
            this.btnSCTQuery.Click += new System.EventHandler(this.btnSCTQuery_Click);
            // 
            // btnShowCandIDs
            // 
            this.btnShowCandIDs.Location = new System.Drawing.Point(389, 73);
            this.btnShowCandIDs.Name = "btnShowCandIDs";
            this.btnShowCandIDs.Size = new System.Drawing.Size(378, 26);
            this.btnShowCandIDs.TabIndex = 3;
            this.btnShowCandIDs.Text = "Show Candidate SNOMED CT Terms";
            this.btnShowCandIDs.UseVisualStyleBackColor = true;
            this.btnShowCandIDs.Click += new System.EventHandler(this.btnShowCandIDs_Click);
            // 
            // gbContextQuestions
            // 
            this.gbContextQuestions.AutoSize = true;
            this.gbContextQuestions.Controls.Add(this.pnlContextQuestions);
            this.gbContextQuestions.Location = new System.Drawing.Point(3, 184);
            this.gbContextQuestions.Name = "gbContextQuestions";
            this.gbContextQuestions.Size = new System.Drawing.Size(380, 416);
            this.gbContextQuestions.TabIndex = 4;
            this.gbContextQuestions.TabStop = false;
            this.gbContextQuestions.Text = "Contextualized Questions";
            // 
            // pnlContextQuestions
            // 
            this.pnlContextQuestions.AutoScroll = true;
            this.pnlContextQuestions.AutoSize = true;
            this.pnlContextQuestions.Location = new System.Drawing.Point(10, 19);
            this.pnlContextQuestions.Name = "pnlContextQuestions";
            this.pnlContextQuestions.Size = new System.Drawing.Size(364, 378);
            this.pnlContextQuestions.TabIndex = 0;
            // 
            // gbChildren
            // 
            this.gbChildren.AutoSize = true;
            this.gbChildren.Controls.Add(this.label8);
            this.gbChildren.Controls.Add(this.pnlChildren);
            this.gbChildren.Location = new System.Drawing.Point(389, 105);
            this.gbChildren.Name = "gbChildren";
            this.gbChildren.Size = new System.Drawing.Size(378, 495);
            this.gbChildren.TabIndex = 4;
            this.gbChildren.TabStop = false;
            this.gbChildren.Text = "Child SNOMED CT Terms";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(317, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Click the arrow to expand, and the text to select as a correct term.";
            // 
            // pnlChildren
            // 
            this.pnlChildren.AutoScroll = true;
            this.pnlChildren.AutoSize = true;
            this.pnlChildren.Location = new System.Drawing.Point(6, 35);
            this.pnlChildren.Name = "pnlChildren";
            this.pnlChildren.Size = new System.Drawing.Size(366, 441);
            this.pnlChildren.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(48, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Threshold";
            // 
            // lblTH
            // 
            this.lblTH.AutoSize = true;
            this.lblTH.Location = new System.Drawing.Point(48, 36);
            this.lblTH.Name = "lblTH";
            this.lblTH.Size = new System.Drawing.Size(16, 13);
            this.lblTH.TabIndex = 0;
            this.lblTH.Text = "...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Iteration";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(101, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "#of Children";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(162, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "#of Candidates";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(238, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Reduction %";
            // 
            // lblIteration
            // 
            this.lblIteration.AutoSize = true;
            this.lblIteration.Location = new System.Drawing.Point(6, 36);
            this.lblIteration.Name = "lblIteration";
            this.lblIteration.Size = new System.Drawing.Size(16, 13);
            this.lblIteration.TabIndex = 0;
            this.lblIteration.Text = "...";
            // 
            // lblNumOfChildren
            // 
            this.lblNumOfChildren.AutoSize = true;
            this.lblNumOfChildren.Location = new System.Drawing.Point(101, 36);
            this.lblNumOfChildren.Name = "lblNumOfChildren";
            this.lblNumOfChildren.Size = new System.Drawing.Size(16, 13);
            this.lblNumOfChildren.TabIndex = 0;
            this.lblNumOfChildren.Text = "...";
            // 
            // lblNumOfCandidates
            // 
            this.lblNumOfCandidates.AutoSize = true;
            this.lblNumOfCandidates.Location = new System.Drawing.Point(162, 36);
            this.lblNumOfCandidates.Name = "lblNumOfCandidates";
            this.lblNumOfCandidates.Size = new System.Drawing.Size(16, 13);
            this.lblNumOfCandidates.TabIndex = 0;
            this.lblNumOfCandidates.Text = "...";
            // 
            // lblReductionPercent
            // 
            this.lblReductionPercent.AutoSize = true;
            this.lblReductionPercent.Location = new System.Drawing.Point(238, 36);
            this.lblReductionPercent.Name = "lblReductionPercent";
            this.lblReductionPercent.Size = new System.Drawing.Size(16, 13);
            this.lblReductionPercent.TabIndex = 0;
            this.lblReductionPercent.Text = "...";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Search Tree";
            // 
            // btnSaveStates
            // 
            this.btnSaveStates.Location = new System.Drawing.Point(305, 14);
            this.btnSaveStates.Name = "btnSaveStates";
            this.btnSaveStates.Size = new System.Drawing.Size(67, 37);
            this.btnSaveStates.TabIndex = 3;
            this.btnSaveStates.Text = "Save Stats";
            this.btnSaveStates.UseVisualStyleBackColor = true;
            this.btnSaveStates.Click += new System.EventHandler(this.btnSaveStates_Click);
            // 
            // tvNodes
            // 
            this.tvNodes.Location = new System.Drawing.Point(13, 83);
            this.tvNodes.Name = "tvNodes";
            this.tvNodes.Size = new System.Drawing.Size(364, 97);
            this.tvNodes.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSaveStates);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblIteration);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblNumOfChildren);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblNumOfCandidates);
            this.groupBox1.Controls.Add(this.lblTH);
            this.groupBox1.Controls.Add(this.lblReductionPercent);
            this.groupBox1.Location = new System.Drawing.Point(389, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 60);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Statistics";
            // 
            // Form1
            // 
            this.AcceptButton = this.btnSCTQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 606);
            this.Controls.Add(this.btnShowCandIDs);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tvNodes);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.gbChildren);
            this.Controls.Add(this.gbContextQuestions);
            this.Controls.Add(this.btnSCTQuery);
            this.Controls.Add(this.txtSCTQuery);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "SNOMED CT Selector";
            this.gbContextQuestions.ResumeLayout(false);
            this.gbContextQuestions.PerformLayout();
            this.gbChildren.ResumeLayout(false);
            this.gbChildren.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSCTQuery;
        private System.Windows.Forms.Button btnSCTQuery;
        private System.Windows.Forms.Button btnShowCandIDs;
        private System.Windows.Forms.GroupBox gbContextQuestions;
        private System.Windows.Forms.GroupBox gbChildren;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblIteration;
        private System.Windows.Forms.Label lblNumOfChildren;
        private System.Windows.Forms.Label lblNumOfCandidates;
        private System.Windows.Forms.Label lblReductionPercent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlContextQuestions;
        private System.Windows.Forms.Panel pnlChildren;
        private System.Windows.Forms.Button btnSaveStates;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TreeView tvNodes;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

