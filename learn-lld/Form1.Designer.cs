namespace learn_lld
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnDemoLRU = new Button();
            lboxOutput = new ListBox();
            btnClear = new Button();
            btnLaderBoard = new Button();
            btnThreat = new Button();
            btnInventry = new Button();
            SuspendLayout();
            // 
            // btnDemoLRU
            // 
            btnDemoLRU.Location = new Point(23, 23);
            btnDemoLRU.Name = "btnDemoLRU";
            btnDemoLRU.Size = new Size(106, 23);
            btnDemoLRU.TabIndex = 0;
            btnDemoLRU.Text = "LRU Demo";
            btnDemoLRU.UseVisualStyleBackColor = true;
            btnDemoLRU.Click += btnDemoLRU_Click;
            // 
            // lboxOutput
            // 
            lboxOutput.FormattingEnabled = true;
            lboxOutput.Location = new Point(12, 357);
            lboxOutput.Name = "lboxOutput";
            lboxOutput.Size = new Size(434, 304);
            lboxOutput.TabIndex = 1;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(23, 52);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(111, 23);
            btnClear.TabIndex = 2;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnLaderBoard
            // 
            btnLaderBoard.Location = new Point(135, 23);
            btnLaderBoard.Name = "btnLaderBoard";
            btnLaderBoard.Size = new Size(106, 23);
            btnLaderBoard.TabIndex = 0;
            btnLaderBoard.Text = "Leaderboard";
            btnLaderBoard.UseVisualStyleBackColor = true;
            btnLaderBoard.Click += btnLaderBoard_Click;
            // 
            // btnThreat
            // 
            btnThreat.Location = new Point(247, 23);
            btnThreat.Name = "btnThreat";
            btnThreat.Size = new Size(129, 23);
            btnThreat.TabIndex = 3;
            btnThreat.Text = "Threat Deduction";
            btnThreat.UseVisualStyleBackColor = true;
            btnThreat.Click += btnThreat_Click;
            // 
            // btnInventry
            // 
            btnInventry.Location = new Point(247, 52);
            btnInventry.Name = "btnInventry";
            btnInventry.Size = new Size(129, 23);
            btnInventry.TabIndex = 4;
            btnInventry.Text = "Inventory";
            btnInventry.UseVisualStyleBackColor = true;
            btnInventry.Click += btnInventry_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(878, 685);
            Controls.Add(btnInventry);
            Controls.Add(btnThreat);
            Controls.Add(btnClear);
            Controls.Add(lboxOutput);
            Controls.Add(btnLaderBoard);
            Controls.Add(btnDemoLRU);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnDemoLRU;
        private ListBox lboxOutput;
        private Button btnClear;
        private Button btnLaderBoard;
        private Button btnThreat;
        private Button btnInventry;
    }
}
