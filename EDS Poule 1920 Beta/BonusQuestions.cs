using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_Poule
{
    [Serializable]
    public struct Question
    {
        public dynamic Answer;
        public int Points;
        public bool IsArray;
        public int WeekAnswered;
    }

    [Serializable]
    public class BonusQuestions
    {
        public Dictionary<string, Question> Answers { get; private set; }
        public int WeekScore { get; private set; }

        public BonusQuestions(string kampioen, string degradant, string topscorer, string trainer,
            string winterkampioen, string championround, string kampioendivisie1, string[] finalisten, int[] weeks)
        {
            Answers = new Dictionary<string, Question>()
            {
                {"Kampioen", new Question(){Answer = kampioen, Points = 25, IsArray = false, WeekAnswered = weeks[0] } },
                {"Degradant", new Question(){Answer = degradant, Points = 20, IsArray = false, WeekAnswered = weeks[1] } },
                {"Topscorer", new Question(){Answer = topscorer, Points = 20, IsArray = false, WeekAnswered = weeks[2] } },
                {"Trainer", new Question(){Answer = trainer, Points = 20, IsArray = false, WeekAnswered = weeks[3] } },
                {"Winterkampioen", new Question(){Answer = winterkampioen, Points = 15, IsArray = false, WeekAnswered = weeks[4] } },
                {"Championround", new Question(){Answer = championround, Points = 10, IsArray = false, WeekAnswered = weeks[5] } },
                {"Kampioendivisie1", new Question(){Answer = kampioendivisie1, Points = 20, IsArray = false, WeekAnswered = weeks[6] } },
                {"Finalisten", new Question(){Answer = finalisten, Points = 10, IsArray = true, WeekAnswered = weeks[7] } },
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
                        foreach (var e in a.Value.Answer)
                        {
                            if (ans.Answer.Contains(e))
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
