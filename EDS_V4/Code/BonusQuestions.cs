using System;
using System.Collections.Generic;
using System.Linq;
using VoetbalPoolsBase;

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

    public class BonusQuestions
    {
        public Dictionary<BonusKeys, BonusQuestion> Answers { get; set; }

        public BonusQuestions()
        {
            //this parameterless constructor is used for json deserialization. Do not use it for implementations!
        }

        public BonusQuestions(List<KeyValuePair<string, int>> rawanswers)
        {
            var answers = new List<string>();
            var weeks = new List<int>();

            for (int i = 0; i < 13; i++)
            {
                answers.Add(string.Empty);
                weeks.Add(34);
            }

            if (rawanswers.Count == 13)
            {
                answers.Clear();
                weeks.Clear();
                foreach (var pair in rawanswers)
                {
                    answers.Add(pair.Key);
                    weeks.Add(pair.Value);
                }
            }

            Answers = new Dictionary<BonusKeys, BonusQuestion>()
            {
                {BonusKeys.Kampioen, new BonusQuestion(){Answer = new string[] {answers[0] }, Points = 140, RoundsAnswered = new int[] {weeks[0] } } },
                {BonusKeys.Prodeg, new BonusQuestion(){Answer = new string[] {answers[1] }, Points = 70, RoundsAnswered = new int[] {weeks[1] } } },
                {BonusKeys.Topscorer, new BonusQuestion(){Answer = new string[] {answers[2] }, Points = 0, RoundsAnswered = new int[] {weeks[2] } } },
                {BonusKeys.Trainer, new BonusQuestion(){Answer = new string[] {answers[3] }, Points = 120, RoundsAnswered = new int[] {weeks[3] } } },
                {BonusKeys.Winterkampioen, new BonusQuestion(){Answer = new string[] {answers[4] }, Points = 90, RoundsAnswered = new int[] {weeks[4] } } },
                {BonusKeys.Ronde, new BonusQuestion(){Answer = new string[] {answers[5] }, Points = 40, RoundsAnswered = new int[] { weeks[5] } } },
                {BonusKeys.Teamrood, new BonusQuestion(){Answer = new string[] {answers[6] }, Points = 90, RoundsAnswered = new int[] {weeks[6]} } },
                {BonusKeys.Finalisten, new BonusQuestion(){Answer = new string[]{answers[7], answers[8] }, Points = 50, RoundsAnswered = new int[]{ weeks[7], weeks[8] } } },
                {BonusKeys.Degradanten, new BonusQuestion(){Answer = new string[]{answers[9], answers[10] }, Points = 50, RoundsAnswered = new int[]{ weeks[9], weeks[10] } } },
                {BonusKeys.Promovendi, new BonusQuestion(){Answer = new string[]{answers[11], answers[12] }, Points = 50, RoundsAnswered = new int[]{ weeks[11], weeks[12] } } },
            };
        }

        public int CheckBonus(BonusQuestions HostQuestions, int currentweek, Dictionary<string, Topscorer> topscorers)
        {
            if (HostQuestions == null)
            {
                throw new ArgumentNullException("hostquestions");
            }
            int WeekScore = 0;

            //check all questions except topscorers
            foreach (var a in Answers)
            {
                var ans = HostQuestions.Answers[a.Key];
                if (ans.RoundsAnswered[0] > 0)
                {
                    for (int i = 0; i < ans.Answer.Length; i++)
                    {
                        if (a.Value.Answer.Contains(ans.Answer[i]) && ans.RoundsAnswered[i] == currentweek)
                        {
                            WeekScore += a.Value.Points;
                        }
                    }
                }
            }

            //check the topscorers
            var ansscorer = topscorers[Answers[BonusKeys.Topscorer].Answer[0]];
            WeekScore += ansscorer.Rounds[currentweek - 1] * 5;
            return WeekScore;
        }
    }
}
