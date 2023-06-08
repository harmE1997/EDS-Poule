using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_V4.Code
{
    public class Match
    {
        public int ResultA { get; set; }
        public int ResultB { get; set; }
        public string Winner { get; set; }
        public bool MOTW { get; set; }
        public int Postponement { get; set; }

        public Match()
        {
            //this parameterless constructor is used for json deserialization. Do not use it for implementations!
        }

        public Match(int resA, int resB, bool motw = false, int postponement=0)
        {
            ResultA = resA;
            ResultB = resB;
            MOTW = motw;
            Postponement = postponement;
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

        public int CheckMatch(Match hostmatch)
        {
            var matchScore = 0;

            if (hostmatch.ResultA != 99 && ResultA != 99)
            {
                if (Winner == hostmatch.Winner)
                {
                    matchScore += 15;
                }

                if (ResultA == hostmatch.ResultA)
                {
                    matchScore += 5;
                }

                if (ResultB == hostmatch.ResultB)
                {
                    matchScore += 5;
                }

                if (hostmatch.MOTW)
                {
                    matchScore *= 2;
                }
            }
            return matchScore;
        }

        public string MatchToString()
        { 
            var res = ResultA.ToString() + " - " + ResultB.ToString();
            return res;
        }
    }
}
