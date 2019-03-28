using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_Poule
{
    [Serializable]
    public class BonusQuestions
    {
        public string Kampioen { get; private set; }
        public string Degradant { get; private set; }
        public string Topscorer { get; private set; }
        public string Trainer { get; private set; }
        public string Winterkampioen{ get; private set; }
        public int ChampionRound { get; private set; }
        public string KamipoenDivisie1 { get; private set; }
        public string[] Bekerfinalisten { get; private set; }

        public BonusQuestions(string kampioen, string degradant, string topscorer, string trainer, 
            string winterkampioen, int championround, string kampioendivisie1, string[] finalisten)
        {
            Kampioen = kampioen;
            Degradant = degradant;
            Topscorer = topscorer;
            Trainer = trainer;
            Winterkampioen = winterkampioen;
            ChampionRound = championround;
            KamipoenDivisie1 = kampioendivisie1;
            Bekerfinalisten = finalisten;
        }

        public int checkBonus(Player Host)
        {
            int score = 0;
            BonusQuestions ans = Host.Questions;
            if (Kampioen.ToLower() == ans.Kampioen.ToLower() && ans.Kampioen != "")
            {
                score += 25;
            }

            if (Degradant.ToLower() == ans.Degradant.ToLower() && ans.Degradant != "")
            {
                score += 20;
            }

            if (Topscorer.ToLower() == ans.Topscorer.ToLower() && ans.Topscorer != "")
            {
                score += 20;
            }

            if (Trainer.ToLower() == ans.Trainer.ToLower() && ans.Trainer != "")
            {
                score += 20;
            }

            if (Winterkampioen.ToLower() == ans.Winterkampioen.ToLower() && ans.Winterkampioen != "")
            {
                score += 15;
            }

            if (ChampionRound == ans.ChampionRound && ans.ChampionRound != 0)
            {
                score += 10;
            }

            if (KamipoenDivisie1.ToLower() == ans.KamipoenDivisie1.ToLower() && ans.KamipoenDivisie1 != "")
            {
                score += 20;
            }

            for (int i = 0; i < 2; i++)
            {
                if (ans.Bekerfinalisten.Contains(Bekerfinalisten[i]) && !ans.Bekerfinalisten.Contains(""))
                {
                    score += 10;
                }
            }
            return score;
        }
    }
}
