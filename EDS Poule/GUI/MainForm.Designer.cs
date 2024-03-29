﻿namespace EDS_Poule.GUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnNewPlayer = new System.Windows.Forms.Button();
            this.lbRanking = new System.Windows.Forms.ListBox();
            this.btnRanking = new System.Windows.Forms.Button();
            this.btnRemovePlayer = new System.Windows.Forms.Button();
            this.tbPlayer = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.cbCheck = new System.Windows.Forms.ComboBox();
            this.btnLoadPlayer = new System.Windows.Forms.Button();
            this.btnStats = new System.Windows.Forms.Button();
            this.btnMatch = new System.Windows.Forms.Button();
            this.rtbNotes = new System.Windows.Forms.RichTextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.btnRankingToExcel = new System.Windows.Forms.Button();
            this.lblWeek = new System.Windows.Forms.Label();
            this.cbMatches = new System.Windows.Forms.ComboBox();
            this.lblMatch = new System.Windows.Forms.Label();
            this.gbPlayerOptions = new System.Windows.Forms.GroupBox();
            this.gbMatchInfo = new System.Windows.Forms.GroupBox();
            this.btnGetMatch = new System.Windows.Forms.Button();
            this.gbOtherOptions = new System.Windows.Forms.GroupBox();
            this.btnAverage = new System.Windows.Forms.Button();
            this.btnResetHost = new System.Windows.Forms.Button();
            this.gbRankingOptions = new System.Windows.Forms.GroupBox();
            this.cbRecalculate = new System.Windows.Forms.CheckBox();
            this.gbPlayerOptions.SuspendLayout();
            this.gbMatchInfo.SuspendLayout();
            this.gbOtherOptions.SuspendLayout();
            this.gbRankingOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNewPlayer
            // 
            this.btnNewPlayer.BackColor = System.Drawing.Color.LawnGreen;
            this.btnNewPlayer.Location = new System.Drawing.Point(5, 18);
            this.btnNewPlayer.Name = "btnNewPlayer";
            this.btnNewPlayer.Size = new System.Drawing.Size(202, 24);
            this.btnNewPlayer.TabIndex = 3;
            this.btnNewPlayer.Text = "New Player";
            this.btnNewPlayer.UseVisualStyleBackColor = false;
            this.btnNewPlayer.Click += new System.EventHandler(this.btnNewPlayer_Click);
            // 
            // lbRanking
            // 
            this.lbRanking.FormattingEnabled = true;
            this.lbRanking.Location = new System.Drawing.Point(12, 15);
            this.lbRanking.Name = "lbRanking";
            this.lbRanking.Size = new System.Drawing.Size(385, 628);
            this.lbRanking.TabIndex = 4;
            // 
            // btnRanking
            // 
            this.btnRanking.Location = new System.Drawing.Point(5, 18);
            this.btnRanking.Name = "btnRanking";
            this.btnRanking.Size = new System.Drawing.Size(202, 24);
            this.btnRanking.TabIndex = 5;
            this.btnRanking.Text = "Refresh Ranking";
            this.btnRanking.UseVisualStyleBackColor = true;
            this.btnRanking.Click += new System.EventHandler(this.btnRanking_Click);
            // 
            // btnRemovePlayer
            // 
            this.btnRemovePlayer.BackColor = System.Drawing.Color.Red;
            this.btnRemovePlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemovePlayer.Location = new System.Drawing.Point(5, 47);
            this.btnRemovePlayer.Name = "btnRemovePlayer";
            this.btnRemovePlayer.Size = new System.Drawing.Size(202, 24);
            this.btnRemovePlayer.TabIndex = 6;
            this.btnRemovePlayer.Text = "Remove Player";
            this.btnRemovePlayer.UseVisualStyleBackColor = false;
            this.btnRemovePlayer.Click += new System.EventHandler(this.btnRemovePlayer_Click);
            // 
            // tbPlayer
            // 
            this.tbPlayer.Location = new System.Drawing.Point(5, 106);
            this.tbPlayer.Name = "tbPlayer";
            this.tbPlayer.Size = new System.Drawing.Size(202, 20);
            this.tbPlayer.TabIndex = 7;
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(7, 74);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(202, 24);
            this.btnCheck.TabIndex = 8;
            this.btnCheck.Text = "Check All";
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
            this.cbCheck.Location = new System.Drawing.Point(54, 79);
            this.cbCheck.Name = "cbCheck";
            this.cbCheck.Size = new System.Drawing.Size(160, 21);
            this.cbCheck.TabIndex = 9;
            this.cbCheck.Text = "1";
            // 
            // btnLoadPlayer
            // 
            this.btnLoadPlayer.Location = new System.Drawing.Point(5, 76);
            this.btnLoadPlayer.Name = "btnLoadPlayer";
            this.btnLoadPlayer.Size = new System.Drawing.Size(202, 24);
            this.btnLoadPlayer.TabIndex = 11;
            this.btnLoadPlayer.Text = "Load Player";
            this.btnLoadPlayer.UseVisualStyleBackColor = true;
            this.btnLoadPlayer.Click += new System.EventHandler(this.btnLoadPlayer_Click);
            // 
            // btnStats
            // 
            this.btnStats.Location = new System.Drawing.Point(10, 46);
            this.btnStats.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStats.Name = "btnStats";
            this.btnStats.Size = new System.Drawing.Size(202, 24);
            this.btnStats.TabIndex = 12;
            this.btnStats.Text = "Stats";
            this.btnStats.UseVisualStyleBackColor = true;
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);
            // 
            // btnMatch
            // 
            this.btnMatch.Location = new System.Drawing.Point(10, 15);
            this.btnMatch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnMatch.Name = "btnMatch";
            this.btnMatch.Size = new System.Drawing.Size(202, 24);
            this.btnMatch.TabIndex = 13;
            this.btnMatch.Text = "Check Match";
            this.btnMatch.UseVisualStyleBackColor = true;
            this.btnMatch.Click += new System.EventHandler(this.btnMatch_Click);
            // 
            // rtbNotes
            // 
            this.rtbNotes.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbNotes.Location = new System.Drawing.Point(414, 336);
            this.rtbNotes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rtbNotes.Name = "rtbNotes";
            this.rtbNotes.Size = new System.Drawing.Size(448, 241);
            this.rtbNotes.TabIndex = 14;
            this.rtbNotes.Text = "";
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(417, 319);
            this.lblNotes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(35, 13);
            this.lblNotes.TabIndex = 15;
            this.lblNotes.Text = "Notes";
            // 
            // btnRankingToExcel
            // 
            this.btnRankingToExcel.Location = new System.Drawing.Point(7, 103);
            this.btnRankingToExcel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRankingToExcel.Name = "btnRankingToExcel";
            this.btnRankingToExcel.Size = new System.Drawing.Size(202, 24);
            this.btnRankingToExcel.TabIndex = 16;
            this.btnRankingToExcel.Text = "Export Ranking to Excel";
            this.btnRankingToExcel.UseVisualStyleBackColor = true;
            this.btnRankingToExcel.Click += new System.EventHandler(this.btnRankingToExcel_Click);
            // 
            // lblWeek
            // 
            this.lblWeek.AutoSize = true;
            this.lblWeek.Location = new System.Drawing.Point(13, 81);
            this.lblWeek.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(39, 13);
            this.lblWeek.TabIndex = 17;
            this.lblWeek.Text = "Week:";
            // 
            // cbMatches
            // 
            this.cbMatches.FormattingEnabled = true;
            this.cbMatches.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "MOTW"});
            this.cbMatches.Location = new System.Drawing.Point(54, 104);
            this.cbMatches.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbMatches.Name = "cbMatches";
            this.cbMatches.Size = new System.Drawing.Size(160, 21);
            this.cbMatches.TabIndex = 18;
            // 
            // lblMatch
            // 
            this.lblMatch.AutoSize = true;
            this.lblMatch.Location = new System.Drawing.Point(13, 106);
            this.lblMatch.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMatch.Name = "lblMatch";
            this.lblMatch.Size = new System.Drawing.Size(40, 13);
            this.lblMatch.TabIndex = 19;
            this.lblMatch.Text = "Match:";
            this.lblMatch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // gbPlayerOptions
            // 
            this.gbPlayerOptions.Controls.Add(this.btnNewPlayer);
            this.gbPlayerOptions.Controls.Add(this.btnRemovePlayer);
            this.gbPlayerOptions.Controls.Add(this.btnLoadPlayer);
            this.gbPlayerOptions.Controls.Add(this.tbPlayer);
            this.gbPlayerOptions.Location = new System.Drawing.Point(414, 15);
            this.gbPlayerOptions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbPlayerOptions.Name = "gbPlayerOptions";
            this.gbPlayerOptions.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbPlayerOptions.Size = new System.Drawing.Size(214, 135);
            this.gbPlayerOptions.TabIndex = 20;
            this.gbPlayerOptions.TabStop = false;
            this.gbPlayerOptions.Text = "PlayerOptions";
            // 
            // gbMatchInfo
            // 
            this.gbMatchInfo.Controls.Add(this.btnGetMatch);
            this.gbMatchInfo.Controls.Add(this.btnMatch);
            this.gbMatchInfo.Controls.Add(this.cbCheck);
            this.gbMatchInfo.Controls.Add(this.lblMatch);
            this.gbMatchInfo.Controls.Add(this.cbMatches);
            this.gbMatchInfo.Controls.Add(this.lblWeek);
            this.gbMatchInfo.Location = new System.Drawing.Point(640, 15);
            this.gbMatchInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbMatchInfo.Name = "gbMatchInfo";
            this.gbMatchInfo.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbMatchInfo.Size = new System.Drawing.Size(220, 135);
            this.gbMatchInfo.TabIndex = 21;
            this.gbMatchInfo.TabStop = false;
            this.gbMatchInfo.Text = "MatchInfo";
            // 
            // btnGetMatch
            // 
            this.btnGetMatch.Location = new System.Drawing.Point(10, 47);
            this.btnGetMatch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGetMatch.Name = "btnGetMatch";
            this.btnGetMatch.Size = new System.Drawing.Size(202, 24);
            this.btnGetMatch.TabIndex = 20;
            this.btnGetMatch.Text = "Get Match Info";
            this.btnGetMatch.UseVisualStyleBackColor = true;
            this.btnGetMatch.Click += new System.EventHandler(this.btnGetMatch_Click);
            // 
            // gbOtherOptions
            // 
            this.gbOtherOptions.Controls.Add(this.btnAverage);
            this.gbOtherOptions.Controls.Add(this.btnResetHost);
            this.gbOtherOptions.Controls.Add(this.btnStats);
            this.gbOtherOptions.Location = new System.Drawing.Point(640, 154);
            this.gbOtherOptions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbOtherOptions.Name = "gbOtherOptions";
            this.gbOtherOptions.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbOtherOptions.Size = new System.Drawing.Size(221, 109);
            this.gbOtherOptions.TabIndex = 22;
            this.gbOtherOptions.TabStop = false;
            this.gbOtherOptions.Text = "Other Options";
            // 
            // btnAverage
            // 
            this.btnAverage.Location = new System.Drawing.Point(10, 18);
            this.btnAverage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAverage.Name = "btnAverage";
            this.btnAverage.Size = new System.Drawing.Size(202, 24);
            this.btnAverage.TabIndex = 9;
            this.btnAverage.Text = "Average";
            this.btnAverage.UseVisualStyleBackColor = true;
            this.btnAverage.Click += new System.EventHandler(this.btnAverage_Click);
            // 
            // btnResetHost
            // 
            this.btnResetHost.Location = new System.Drawing.Point(10, 76);
            this.btnResetHost.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnResetHost.Name = "btnResetHost";
            this.btnResetHost.Size = new System.Drawing.Size(202, 24);
            this.btnResetHost.TabIndex = 17;
            this.btnResetHost.Text = "Reset Host";
            this.btnResetHost.UseVisualStyleBackColor = true;
            this.btnResetHost.Click += new System.EventHandler(this.btnResetHost_Click);
            // 
            // gbRankingOptions
            // 
            this.gbRankingOptions.Controls.Add(this.cbRecalculate);
            this.gbRankingOptions.Controls.Add(this.btnRanking);
            this.gbRankingOptions.Controls.Add(this.btnCheck);
            this.gbRankingOptions.Controls.Add(this.btnRankingToExcel);
            this.gbRankingOptions.Location = new System.Drawing.Point(414, 154);
            this.gbRankingOptions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbRankingOptions.Name = "gbRankingOptions";
            this.gbRankingOptions.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbRankingOptions.Size = new System.Drawing.Size(214, 131);
            this.gbRankingOptions.TabIndex = 23;
            this.gbRankingOptions.TabStop = false;
            this.gbRankingOptions.Text = "Ranking Options";
            // 
            // cbRecalculate
            // 
            this.cbRecalculate.AutoSize = true;
            this.cbRecalculate.Location = new System.Drawing.Point(52, 51);
            this.cbRecalculate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbRecalculate.Name = "cbRecalculate";
            this.cbRecalculate.Size = new System.Drawing.Size(120, 17);
            this.cbRecalculate.TabIndex = 17;
            this.cbRecalculate.Text = "Recalculate Weeks";
            this.cbRecalculate.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(870, 648);
            this.Controls.Add(this.gbRankingOptions);
            this.Controls.Add(this.gbOtherOptions);
            this.Controls.Add(this.gbMatchInfo);
            this.Controls.Add(this.gbPlayerOptions);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.rtbNotes);
            this.Controls.Add(this.lbRanking);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.gbPlayerOptions.ResumeLayout(false);
            this.gbPlayerOptions.PerformLayout();
            this.gbMatchInfo.ResumeLayout(false);
            this.gbMatchInfo.PerformLayout();
            this.gbOtherOptions.ResumeLayout(false);
            this.gbRankingOptions.ResumeLayout(false);
            this.gbRankingOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNewPlayer;
        private System.Windows.Forms.ListBox lbRanking;
        private System.Windows.Forms.Button btnRanking;
        private System.Windows.Forms.Button btnRemovePlayer;
        private System.Windows.Forms.TextBox tbPlayer;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.ComboBox cbCheck;
        private System.Windows.Forms.Button btnLoadPlayer;
        private System.Windows.Forms.Button btnStats;
        private System.Windows.Forms.Button btnMatch;
        private System.Windows.Forms.RichTextBox rtbNotes;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Button btnRankingToExcel;
        private System.Windows.Forms.Label lblWeek;
        private System.Windows.Forms.ComboBox cbMatches;
        private System.Windows.Forms.Label lblMatch;
        private System.Windows.Forms.GroupBox gbPlayerOptions;
        private System.Windows.Forms.GroupBox gbMatchInfo;
        private System.Windows.Forms.Button btnGetMatch;
        private System.Windows.Forms.GroupBox gbOtherOptions;
        private System.Windows.Forms.GroupBox gbRankingOptions;
        private System.Windows.Forms.Button btnAverage;
        private System.Windows.Forms.Button btnResetHost;
        private System.Windows.Forms.CheckBox cbRecalculate;
    }
}

