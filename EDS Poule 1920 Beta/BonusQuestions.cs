using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    [Serializable]
    public class BonusQuestions
    {
        public Dictionary<BonusKeys, Question> Answers { get; private set; }
        public int WeekScore { get; private set; }

        public BonusQuestions(string kampioen, string[] degradanten, string topscorer, string trainer,
            string winterkampioen, string championround, string[] promovendi, string[] finalisten, 
            string teamReds, string topAssists, string worstdefence, string prodeg ,int[] weeks)
        {
            Answers = new Dictionary<BonusKeys, Question>()
            {
                {BonusKeys.Kampioen, new Question(){Answer = kampioen, Points = 100, IsArray = false, WeekAnswered = weeks[0] } },
                {BonusKeys.Degradanten, new Question(){AnswerArray = degradanten, Points = 40, IsArray = true, WeekAnswered = weeks[1] } },
                {BonusKeys.Topscorer, new Question(){Answer = topscorer, Points = 80, IsArray = false, WeekAnswered = weeks[2] } },
                {BonusKeys.Trainer, new Question(){Answer = trainer, Points = 80, IsArray = false, WeekAnswered = weeks[3] } },
                {BonusKeys.Winterkampioen, new Question(){Answer = winterkampioen, Points = 60, IsArray = false, WeekAnswered = weeks[4] } },
                {BonusKeys.Ronde, new Question(){Answer = championround, Points = 40, IsArray = false, WeekAnswered = weeks[5] } },
                {BonusKeys.Promovendi, new Question(){AnswerArray = promovendi, Points = 40, IsArray = true, WeekAnswered = weeks[6] } },
                {BonusKeys.Finalisten, new Question(){AnswerArray = finalisten, Points = 40, IsArray = true, WeekAnswered = weeks[7] } },
                {BonusKeys.Teamrood, new Question(){Answer = teamReds, Points = 60, IsArray = false, WeekAnswered = weeks[8]} },
                {BonusKeys.Assists, new Question(){Answer = topAssists, Points = 80, IsArray = false, WeekAnswered = weeks[9] } },
                {BonusKeys.Defensie, new Question(){Answer = worstdefence, Points = 60, IsArray = false, WeekAnswered = weeks[10] } },
                {BonusKeys.Prodeg, new Question(){Answer = prodeg, Points = 40, IsArray = false, WeekAnswered = weeks[11] } }
            };
        }

        public int checkBonus(BonusQuestions HostQuestions, int currentweek)
        {
            if (HostQuestions == null)
            {
                throw new ArgumentNullException();
            }
            int score = 0;
            
            foreach (var a in Answers)
            {
                var ans = HostQuestions.Answers[a.Key];
                if (ans.WeekAnswered <= currentweek)
                {
                    if (a.Value.IsArray)
                    {
                        foreach (var e in a.Value.AnswerArray)
                        {
                            if (ans.AnswerArray.Contains(e))
                            {
                                score += a.Value.Points;
                                if (ans.WeekAnswered == currentweek)
                                {
                                    WeekScore += a.Value.Points;
                                }
                            }
                        }
                    }

                    else
                    {
                        if (a.Value.Answer == ans.Answer)
                        {
                            score += a.Value.Points;
                            if (ans.WeekAnswered == currentweek)
                            {
                                WeekScore += a.Value.Points;
                            }
                        }
                    }
                }
            }
            return score;
        }
    }
}
