using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_Poule
{
    [Serializable]
    public struct Estimation
    {
        public int Answer;
        public int Max;
    }

    [Serializable]
    public class Estimations
    {
        public Dictionary<string, Estimation> Answers { get; private set; }

        public Estimations(int reds, int goals)
        {
            Answers = new Dictionary<string, Estimation>()
            {
                { "Reds", new Estimation() {Answer = reds, Max = 20 } },
                { "Goals", new Estimation() { Answer = goals, Max = 10} }
            };
        }
        public int checkEstimations(Player host)
        {
            int score = 0;
            foreach (var a in Answers)
            {
                int miss = Math.Abs(a.Value.Answer - host.Estimations.Answers[a.Key].Answer);
                if (a.Value.Max > miss)
                {
                    score += (a.Value.Max - miss);
                }
            }

            return score;
        }
    }
}
