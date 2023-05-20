using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_V4.Code
{
    [Serializable]
    public class Match
    {
        public int ResultA;
        public int ResultB;
        public string Winner { get; private set; }
        public bool MOTW { get; set; }

        public Match(int resA, int resB, bool motw = false)
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

        public string MatchToString()
        { 
            var res = ResultA.ToString() + " - " + ResultB.ToString();
            return res;
        }
    }
}
