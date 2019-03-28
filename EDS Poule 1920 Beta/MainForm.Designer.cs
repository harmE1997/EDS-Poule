namespace EDS_Poule
{
    partial class MainForm
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
            this.btnNewPlayer = new System.Windows.Forms.Button();
            this.btnHost = new System.Windows.Forms.Button();
            this.lbRanking = new System.Windows.Forms.ListBox();
            this.btnRanking = new System.Windows.Forms.Button();
            this.btnRemovePlayer = new System.Windows.Forms.Button();
            this.tbPlayer = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.cbCheck = new System.Windows.Forms.ComboBox();
            this.btnLoadPlayer = new System.Windows.Forms.Button();
            this.btnStats = new System.Windows.Forms.Button();
            this.btnMOTW = new System.Windows.Forms.Button();
            this.rtbNotes = new System.Windows.Forms.RichTextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.btnRankingToExcel = new System.Windows.Forms.Button();
            this.ofdRanking = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnNewPlayer
            // 
            this.btnNewPlayer.BackColor = System.Drawing.Color.LawnGreen;
            this.btnNewPlayer.Location = new System.Drawing.Point(570, 228);
            this.btnNewPlayer.Margin = new System.Windows.Forms.Padding(4);
            this.btnNewPlayer.Name = "btnNewPlayer";
            this.btnNewPlayer.Size = new System.Drawing.Size(268, 28);
            this.btnNewPlayer.TabIndex = 3;
            this.btnNewPlayer.Text = "New Player";
            this.btnNewPlayer.UseVisualStyleBackColor = false;
            this.btnNewPlayer.Click += new System.EventHandler(this.btnNewPlayer_Click);
            // 
            // btnHost
            // 
            this.btnHost.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnHost.Location = new System.Drawing.Point(574, 18);
            this.btnHost.Margin = new System.Windows.Forms.Padding(4);
            this.btnHost.Name = "btnHost";
            this.btnHost.Size = new System.Drawing.Size(265, 28);
            this.btnHost.TabIndex = 1;
            this.btnHost.Text = "Host";
            this.btnHost.UseVisualStyleBackColor = false;
            this.btnHost.Click += new System.EventHandler(this.btnHost_Click);
            // 
            // lbRanking
            // 
            this.lbRanking.FormattingEnabled = true;
            this.lbRanking.ItemHeight = 16;
            this.lbRanking.Location = new System.Drawing.Point(16, 18);
            this.lbRanking.Margin = new System.Windows.Forms.Padding(4);
            this.lbRanking.Name = "lbRanking";
            this.lbRanking.Size = new System.Drawing.Size(512, 772);
            this.lbRanking.TabIndex = 4;
            // 
            // btnRanking
            // 
            this.btnRanking.Location = new System.Drawing.Point(573, 54);
            this.btnRanking.Margin = new System.Windows.Forms.Padding(4);
            this.btnRanking.Name = "btnRanking";
            this.btnRanking.Size = new System.Drawing.Size(266, 28);
            this.btnRanking.TabIndex = 5;
            this.btnRanking.Text = "Refresh Ranking";
            this.btnRanking.UseVisualStyleBackColor = true;
            this.btnRanking.Click += new System.EventHandler(this.btnRanking_Click);
            // 
            // btnRemovePlayer
            // 
            this.btnRemovePlayer.BackColor = System.Drawing.Color.Red;
            this.btnRemovePlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemovePlayer.Location = new System.Drawing.Point(570, 264);
            this.btnRemovePlayer.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemovePlayer.Name = "btnRemovePlayer";
            this.btnRemovePlayer.Size = new System.Drawing.Size(268, 28);
            this.btnRemovePlayer.TabIndex = 6;
            this.btnRemovePlayer.Text = "Remove Player";
            this.btnRemovePlayer.UseVisualStyleBackColor = false;
            this.btnRemovePlayer.Click += new System.EventHandler(this.btnRemovePlayer_Click);
            // 
            // tbPlayer
            // 
            this.tbPlayer.Location = new System.Drawing.Point(570, 371);
            this.tbPlayer.Margin = new System.Windows.Forms.Padding(4);
            this.tbPlayer.Name = "tbPlayer";
            this.tbPlayer.Size = new System.Drawing.Size(268, 22);
            this.tbPlayer.TabIndex = 7;
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(573, 90);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(266, 28);
            this.btnCheck.TabIndex = 8;
            this.btnCheck.Text = "Check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // cbCheck
            // 
            this.cbCheck.FormattingEnabled = true;
            this.cbCheck.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34"});
            this.cbCheck.Location = new System.Drawing.Point(572, 161);
            this.cbCheck.Margin = new System.Windows.Forms.Padding(4);
            this.cbCheck.Name = "cbCheck";
            this.cbCheck.Size = new System.Drawing.Size(266, 24);
            this.cbCheck.TabIndex = 9;
            this.cbCheck.Text = "1";
            // 
            // btnLoadPlayer
            // 
            this.btnLoadPlayer.Location = new System.Drawing.Point(570, 300);
            this.btnLoadPlayer.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoadPlayer.Name = "btnLoadPlayer";
            this.btnLoadPlayer.Size = new System.Drawing.Size(268, 28);
            this.btnLoadPlayer.TabIndex = 11;
            this.btnLoadPlayer.Text = "Load Player";
            this.btnLoadPlayer.UseVisualStyleBackColor = true;
            this.btnLoadPlayer.Click += new System.EventHandler(this.btnLoadPlayer_Click);
            // 
            // btnStats
            // 
            this.btnStats.Location = new System.Drawing.Point(570, 338);
            this.btnStats.Name = "btnStats";
            this.btnStats.Size = new System.Drawing.Size(269, 26);
            this.btnStats.TabIndex = 12;
            this.btnStats.Text = "Stats";
            this.btnStats.UseVisualStyleBackColor = true;
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);
            // 
            // btnMOTW
            // 
            this.btnMOTW.Location = new System.Drawing.Point(574, 125);
            this.btnMOTW.Name = "btnMOTW";
            this.btnMOTW.Size = new System.Drawing.Size(266, 29);
            this.btnMOTW.TabIndex = 13;
            this.btnMOTW.Text = "Check MOTW";
            this.btnMOTW.UseVisualStyleBackColor = true;
            this.btnMOTW.Click += new System.EventHandler(this.btnMOTW_Click);
            // 
            // rtbNotes
            // 
            this.rtbNotes.Location = new System.Drawing.Point(570, 432);
            this.rtbNotes.Name = "rtbNotes";
            this.rtbNotes.Size = new System.Drawing.Size(269, 358);
            this.rtbNotes.TabIndex = 14;
            this.rtbNotes.Text = "";
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(567, 412);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(45, 17);
            this.lblNotes.TabIndex = 15;
            this.lblNotes.Text = "Notes";
            // 
            // btnRankingToExcel
            // 
            this.btnRankingToExcel.Location = new System.Drawing.Point(572, 192);
            this.btnRankingToExcel.Name = "btnRankingToExcel";
            this.btnRankingToExcel.Size = new System.Drawing.Size(267, 29);
            this.btnRankingToExcel.TabIndex = 16;
            this.btnRankingToExcel.Text = "Export Ranking to Excel";
            this.btnRankingToExcel.UseVisualStyleBackColor = true;
            this.btnRankingToExcel.Click += new System.EventHandler(this.btnRankingToExcel_Click);
            // 
            // ofdRanking
            // 
            this.ofdRanking.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(860, 809);
            this.Controls.Add(this.btnRankingToExcel);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.rtbNotes);
            this.Controls.Add(this.btnMOTW);
            this.Controls.Add(this.btnStats);
            this.Controls.Add(this.btnLoadPlayer);
            this.Controls.Add(this.cbCheck);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.tbPlayer);
            this.Controls.Add(this.btnRemovePlayer);
            this.Controls.Add(this.btnRanking);
            this.Controls.Add(this.lbRanking);
            this.Controls.Add(this.btnHost);
            this.Controls.Add(this.btnNewPlayer);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNewPlayer;
        private System.Windows.Forms.Button btnHost;
        private System.Windows.Forms.ListBox lbRanking;
        private System.Windows.Forms.Button btnRanking;
        private System.Windows.Forms.Button btnRemovePlayer;
        private System.Windows.Forms.TextBox tbPlayer;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.ComboBox cbCheck;
        private System.Windows.Forms.Button btnLoadPlayer;
        private System.Windows.Forms.Button btnStats;
        private System.Windows.Forms.Button btnMOTW;
        private System.Windows.Forms.RichTextBox rtbNotes;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Button btnRankingToExcel;
        private System.Windows.Forms.OpenFileDialog ofdRanking;
    }
}

