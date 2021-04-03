using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EDS_Poule
{
    [Serializable]
    public enum BonusKeys
    {
        Kampioen,
        Degradanten,
        Topscorer,
        Trainer,
        Winterkampioen,
        Ronde,
        Promovendi,
        Finalisten,
        Teamrood,
        Assists,
        Defensie,
        Prodeg
    }

    [Serializable]
    public struct Question
    {
        public string Answer;
        public string[] AnswerArray;
        public int Points;
        public bool IsArray;
        public int WeekAnswered;
    }

    public struct Topscorer
    {
        public int Total;
        public List<int> Rounds;
    }

    [Serializable]
    public class BonusQuestions
    {
        public Dictionary<BonusKeys, Question> Answers { get; private set; }
        public int WeekScore { get; private set; }

        public BonusQuestions(string kampioen, string prodeg, string topscorer, string trainer,
            string winterkampioen, string championround, string teamReds, string[] finalisten, string[] degradanten, string[] promovendi,
             int[] weeks)
        {
            Answers = new Dictionary<BonusKeys, Question>()
            {
                {BonusKeys.Kampioen, new Question(){Answer = kampioen, Points = 140, IsArray = false, WeekAnswered = weeks[0] } },
                {BonusKeys.Prodeg, new Question(){Answer = prodeg, Points = 70, IsArray = false, WeekAnswered = weeks[1] } },
                {BonusKeys.Topscorer, new Question(){Answer = topscorer, Points = 0, IsArray = false, WeekAnswered = weeks[2] } },
                {BonusKeys.Trainer, new Question(){Answer = trainer, Points = 120, IsArray = false, WeekAnswered = weeks[3] } },
                {BonusKeys.Winterkampioen, new Question(){Answer = winterkampioen, Points = 90, IsArray = false, WeekAnswered = weeks[4] } },
                {BonusKeys.Ronde, new Question(){Answer = championround, Points = 70, IsArray = false, WeekAnswered = weeks[5] } },
                {BonusKeys.Teamrood, new Question(){Answer = teamReds, Points = 90, IsArray = false, WeekAnswered = weeks[6]} },
                {BonusKeys.Finalisten, new Question(){AnswerArray = finalisten, Points = 50, IsArray = true, WeekAnswered = weeks[7] } },
                {BonusKeys.Degradanten, new Question(){AnswerArray = degradanten, Points = 50, IsArray = true, WeekAnswered = weeks[8] } },
                {BonusKeys.Promovendi, new Question(){AnswerArray = promovendi, Points = 50, IsArray = true, WeekAnswered = weeks[9] } },

            };
        }

        public void CheckBonus(BonusQuestions HostQuestions, int currentweek, Dictionary<string, Topscorer> topscorers)
        {
            if (HostQuestions == null)
            {
                throw new ArgumentNullException("hostquestions");
            }
            WeekScore = 0;

            foreach (var a in Answers)
            {
                var ans = HostQuestions.Answers[a.Key];
                if (ans.WeekAnswered > 0)
                {
                    if (a.Value.IsArray)
                    {
                        foreach (var e in a.Value.AnswerArray)
                        {
                            if (ans.AnswerArray.Contains(e) && ans.WeekAnswered == currentweek)
                            {
                                WeekScore += a.Value.Points;
                            }
                        }
                    }

                    else
                    {
                        if (a.Value.Answer == ans.Answer && ans.WeekAnswered == currentweek)
                        {
                            WeekScore += a.Value.Points;
                        }
                    }
                }
            }

            checkTopscorer(currentweek, topscorers);
        }

        private void checkTopscorer(int round, Dictionary<string, Topscorer> topscorers)
        {
            ExcelManager ex = new ExcelManager();
            try
            {
                var ans = topscorers[Answers[BonusKeys.Topscorer].Answer];
                WeekScore += ans.Rounds[round-1] * 5;
            }

            catch { }
        }
    }
}
