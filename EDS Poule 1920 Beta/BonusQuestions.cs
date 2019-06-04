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
        public string Answer;
        public string[] AnswerArray;
        public int Points;
        public bool IsArray;
        public int WeekAnswered;
    }

    [Serializable]
    public class BonusQuestions
    {
        public Dictionary<string, Question> Answers { get; private set; }
        public int WeekScore { get; private set; }

        public BonusQuestions(string kampioen, string[] degradanten, string topscorer, string trainer,
            string winterkampioen, string championround, string[] promovendi, string[] finalisten, 
            string teamReds, string topAssists, string worstdefence, string prodeg ,int[] weeks)
        {
            Answers = new Dictionary<string, Question>()
            {
                {"Kampioen", new Question(){Answer = kampioen, Points = 25, IsArray = false, WeekAnswered = weeks[0] } },
                {"Degradanten", new Question(){AnswerArray = degradanten, Points = 10, IsArray = true, WeekAnswered = weeks[1] } },
                {"Topscorer", new Question(){Answer = topscorer, Points = 20, IsArray = false, WeekAnswered = weeks[2] } },
                {"Trainer", new Question(){Answer = trainer, Points = 20, IsArray = false, WeekAnswered = weeks[3] } },
                {"Winterkampioen", new Question(){Answer = winterkampioen, Points = 15, IsArray = false, WeekAnswered = weeks[4] } },
                {"Championround", new Question(){Answer = championround, Points = 10, IsArray = false, WeekAnswered = weeks[5] } },
                {"Promovendi", new Question(){AnswerArray = promovendi, Points = 10, IsArray = true, WeekAnswered = weeks[6] } },
                {"Finalisten", new Question(){AnswerArray = finalisten, Points = 10, IsArray = true, WeekAnswered = weeks[7] } },
                {"Teamreds", new Question(){Answer = teamReds, Points = 15, IsArray = false, WeekAnswered = weeks[8]} },
                {"Assists", new Question(){Answer = topAssists, Points = 20, IsArray = false, WeekAnswered = weeks[9] } },
                {"Worstdefence", new Question(){Answer = worstdefence, Points = 15, IsArray = false, WeekAnswered = weeks[10] } },
                {"Prodeg", new Question(){Answer = prodeg, Points = 20, IsArray = false, WeekAnswered = weeks[11] } }
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
