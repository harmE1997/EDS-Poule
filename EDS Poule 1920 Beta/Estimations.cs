using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_Poule
{
    [Serializable]
    public class Estimations
    {
        public int Redcards { get; private set; }
        public int RedsMax { get; private set; }

        public int GoalsTopscorer { get; private set; }
        public int GoalsMax { get; private set; }


        public Estimations(int reds, int goals)
        {
            GoalsTopscorer = goals;
            Redcards = reds;
            RedsMax = 20;
            GoalsMax = 10;
        }
        public int checkEstimations(Player host)
        {
            int score = 0;

            int miss = Math.Abs(Redcards - host.Estimations.Redcards); 
            if(RedsMax > miss)
                score += (RedsMax - miss);

            miss = Math.Abs(GoalsTopscorer - host.Estimations.GoalsTopscorer);
            if (GoalsMax > miss)
                score += (GoalsMax - miss);

            return score;
        }
    }
}
