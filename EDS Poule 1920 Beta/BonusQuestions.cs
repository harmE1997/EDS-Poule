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
    }

    [Serializable]
    public class BonusQuestions
    {
        public Dictionary<string, Question> Answers { get; private set; }

        public BonusQuestions(string kampioen, string degradant, string topscorer, string trainer, 
            string winterkampioen, string championround, string kampioendivisie1, string[] finalisten)
        {
            Answers = new Dictionary<string, Question>()
            {
                {"Kampioen", new Question(){Answer = kampioen, Points = 25, IsArray = false } },
                {"Degradant", new Question(){Answer = degradant, Points = 20, IsArray = false } },
                {"Topscorer", new Question(){Answer = topscorer, Points = 20, IsArray = false } },
                {"Trainer", new Question(){Answer = trainer, Points = 20, IsArray = false } },
                {"Winterkampioen", new Question(){Answer = winterkampioen, Points = 15, IsArray = false } },
                {"Championround", new Question(){Answer = championround, Points = 10, IsArray = false } },
                {"Kampioendivisie1", new Question(){Answer = kampioendivisie1, Points = 20, IsArray = false } },
                {"Finalisten", new Question(){Answer = finalisten, Points = 10, IsArray = true } },
            };
        }

        public int checkBonus(BonusQuestions HostQuestions)
        {
            if (HostQuestions == null)
            {
                throw new ArgumentNullException();
            }
            int score = 0;
            foreach (var a in Answers)
            {
                if (a.Value.IsArray)
                {
                    foreach (var e in a.Value.Answer)
                    {
                        if (HostQuestions.Answers[a.Key].Answer.Contains(e))
                        {
                            score += a.Value.Points;
                        }
                    }
                }

                else
                {
                    if (a.Value.Answer == HostQuestions.Answers[a.Key].Answer)
                    {
                        score += a.Value.Points;
                    }
                }
            }
            return score;
        }
    }
}
