namespace EDS_Poule
{
    partial class StatsForm
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
            this.btnChampions = new System.Windows.Forms.Button();
            this.lbStats = new System.Windows.Forms.ListBox();
            this.btnDegrade = new System.Windows.Forms.Button();
            this.btnTopscorers = new System.Windows.Forms.Button();
            this.btnTrainers = new System.Windows.Forms.Button();
            this.btnWinterChamps = new System.Windows.Forms.Button();
            this.btnChampsDiv1 = new System.Windows.Forms.Button();
            this.btnCupFinalists = new System.Windows.Forms.Button();
            this.btnChampionRound = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnChampions
            // 
            this.btnChampions.Location = new System.Drawing.Point(340, 14);
            this.btnChampions.Name = "btnChampions";
            this.btnChampions.Size = new System.Drawing.Size(213, 96);
            this.btnChampions.TabIndex = 0;
            this.btnChampions.Text = "Champions";
            this.btnChampions.UseVisualStyleBackColor = true;
            this.btnChampions.Click += new System.EventHandler(this.btnChampions_Click);
            // 
            // lbStats
            // 
            this.lbStats.FormattingEnabled = true;
            this.lbStats.ItemHeight = 16;
            this.lbStats.Location = new System.Drawing.Point(12, 12);
            this.lbStats.Name = "lbStats";
            this.lbStats.Size = new System.Drawing.Size(316, 404);
            this.lbStats.TabIndex = 1;
            // 
            // btnDegrade
            // 
            this.btnDegrade.Location = new System.Drawing.Point(340, 116);
            this.btnDegrade.Name = "btnDegrade";
            this.btnDegrade.Size = new System.Drawing.Size(213, 96);
            this.btnDegrade.TabIndex = 2;
            this.btnDegrade.Text = "Degradanten";
            this.btnDegrade.UseVisualStyleBackColor = true;
            this.btnDegrade.Click += new System.EventHandler(this.btnDegrade_Click);
            // 
            // btnTopscorers
            // 
            this.btnTopscorers.Location = new System.Drawing.Point(559, 320);
            this.btnTopscorers.Name = "btnTopscorers";
            this.btnTopscorers.Size = new System.Drawing.Size(213, 96);
            this.btnTopscorers.TabIndex = 3;
            this.btnTopscorers.Text = "Topscorers";
            this.btnTopscorers.UseVisualStyleBackColor = true;
            this.btnTopscorers.Click += new System.EventHandler(this.btnTopscorers_Click);
            // 
            // btnTrainers
            // 
            this.btnTrainers.Location = new System.Drawing.Point(559, 116);
            this.btnTrainers.Name = "btnTrainers";
            this.btnTrainers.Size = new System.Drawing.Size(213, 96);
            this.btnTrainers.TabIndex = 4;
            this.btnTrainers.Text = "Trainers";
            this.btnTrainers.UseVisualStyleBackColor = true;
            this.btnTrainers.Click += new System.EventHandler(this.btnTrainers_Click);
            // 
            // btnWinterChamps
            // 
            this.btnWinterChamps.Location = new System.Drawing.Point(559, 14);
            this.btnWinterChamps.Name = "btnWinterChamps";
            this.btnWinterChamps.Size = new System.Drawing.Size(213, 96);
            this.btnWinterChamps.TabIndex = 5;
            this.btnWinterChamps.Text = "Winterkampioenen";
            this.btnWinterChamps.UseVisualStyleBackColor = true;
            this.btnWinterChamps.Click += new System.EventHandler(this.btnWinterChamps_Click);
            // 
            // btnChampsDiv1
            // 
            this.btnChampsDiv1.Location = new System.Drawing.Point(340, 320);
            this.btnChampsDiv1.Name = "btnChampsDiv1";
            this.btnChampsDiv1.Size = new System.Drawing.Size(213, 96);
            this.btnChampsDiv1.TabIndex = 6;
            this.btnChampsDiv1.Text = "Kampioenen 1e divisie";
            this.btnChampsDiv1.UseVisualStyleBackColor = true;
            this.btnChampsDiv1.Click += new System.EventHandler(this.btnChampsDiv1_Click);
            // 
            // btnCupFinalists
            // 
            this.btnCupFinalists.Location = new System.Drawing.Point(559, 218);
            this.btnCupFinalists.Name = "btnCupFinalists";
            this.btnCupFinalists.Size = new System.Drawing.Size(213, 96);
            this.btnCupFinalists.TabIndex = 7;
            this.btnCupFinalists.Text = "Bekerfinalisten";
            this.btnCupFinalists.UseVisualStyleBackColor = true;
            this.btnCupFinalists.Click += new System.EventHandler(this.btnCupFinalists_Click);
            // 
            // btnChampionRound
            // 
            this.btnChampionRound.Location = new System.Drawing.Point(340, 218);
            this.btnChampionRound.Name = "btnChampionRound";
            this.btnChampionRound.Size = new System.Drawing.Size(213, 96);
            this.btnChampionRound.TabIndex = 8;
            this.btnChampionRound.Text = "Ronde Kampioenschap";
            this.btnChampionRound.UseVisualStyleBackColor = true;
            this.btnChampionRound.Click += new System.EventHandler(this.btnChampionRound_Click);
            // 
            // StatsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 432);
            this.Controls.Add(this.btnChampionRound);
            this.Controls.Add(this.btnCupFinalists);
            this.Controls.Add(this.btnChampsDiv1);
            this.Controls.Add(this.btnWinterChamps);
            this.Controls.Add(this.btnTrainers);
            this.Controls.Add(this.btnTopscorers);
            this.Controls.Add(this.btnDegrade);
            this.Controls.Add(this.lbStats);
            this.Controls.Add(this.btnChampions);
            this.Name = "StatsForm";
            this.Text = "StatsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnChampions;
        private System.Windows.Forms.ListBox lbStats;
        private System.Windows.Forms.Button btnDegrade;
        private System.Windows.Forms.Button btnTopscorers;
        private System.Windows.Forms.Button btnTrainers;
        private System.Windows.Forms.Button btnWinterChamps;
        private System.Windows.Forms.Button btnChampsDiv1;
        private System.Windows.Forms.Button btnCupFinalists;
        private System.Windows.Forms.Button btnChampionRound;
    }
}