using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_Poule
{
    [Serializable]
    public enum EstimationKeys
    {
        Reds,
        Goals
    }

    [Serializable]
    public struct Estimation
    {
        public int Answer;
        public int Max;
    }

    [Serializable]
    public class Estimations
    {
        public Dictionary<EstimationKeys, Estimation> Answers { get; private set; }

        public Estimations(int reds, int goals)
        {
            if ((reds < 0 && reds != -99) || (goals < 0 && goals != -99))
            {
                throw new ArgumentOutOfRangeException();
            }

            Answers = new Dictionary<EstimationKeys, Estimation>()
            {
                { EstimationKeys.Reds, new Estimation() {Answer = reds, Max = 80 } },
                { EstimationKeys.Goals, new Estimation() { Answer = goals, Max = 40} }
            };
        }
        public int CheckEstimations(Estimations hostestimations, int week)
        {
            if (hostestimations == null)
            {
                throw new ArgumentNullException();
            }

            if (week != 34)
            {
                return 0;
            }

            int score = 0;
            foreach (var a in Answers)
            {
                int miss = Math.Abs(a.Value.Answer - hostestimations.Answers[a.Key].Answer);
                if (a.Value.Max > miss)
                {
                    score += (a.Value.Max - (miss * 4));
                }
            }

            return score;
        }
    }
}
