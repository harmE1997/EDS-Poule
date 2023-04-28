using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EDS_V4.Code
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
    public class Question
    {
        public string Answer;
        public string[] AnswerArray;
        public int Points;
        public bool IsArray;
        public int WeekAnswered;
        public int[] WeeksAnsweredArray;
    }

    public class Topscorer
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
                {BonusKeys.Finalisten, new Question(){AnswerArray = finalisten, Points = 50, IsArray = true, WeekAnswered = 1, WeeksAnsweredArray = new int[]{ weeks[7], weeks[8] } } },
                {BonusKeys.Degradanten, new Question(){AnswerArray = degradanten, Points = 50, IsArray = true, WeekAnswered = 1, WeeksAnsweredArray = new int[]{ weeks[9], weeks[10] } } },
                {BonusKeys.Promovendi, new Question(){AnswerArray = promovendi, Points = 50, IsArray = true, WeekAnswered = 1, WeeksAnsweredArray = new int[]{ weeks[11], weeks[12] } } },

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
                        for (int i = 0; i < ans.AnswerArray.Length; i++)
                        {
                            if (a.Value.AnswerArray.Contains(ans.AnswerArray[i]) && ans.WeeksAnsweredArray[i] == currentweek)
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
            try
            {
                var ans = topscorers[Answers[BonusKeys.Topscorer].Answer];
                WeekScore += ans.Rounds[round-1] * 5;
            }

            catch { }
        }
    }
}
