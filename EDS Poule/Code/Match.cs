using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_Poule
{
    [Serializable]
    public class Match
    {
        public int ResultA { get; private set; }
        public int ResultB { get; private set; }
        public string Winner { get; private set; }
        public bool MOTW { get; private set; }

        public Match(int resA, int resB, bool motw)
        {
            ResultA = resA;
            ResultB = resB;
            MOTW = motw;
            if (resA > resB)
            {
                Winner = "A";
            }

            else if (resB > resA)
            {
                Winner = "B";
            }

            else
            {
                Winner = "D";
            }
        }
    }
}
