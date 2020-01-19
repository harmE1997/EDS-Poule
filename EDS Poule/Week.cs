using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_Poule
{
    [Serializable]
    public class Week
    {
        public int Weeknr { get; private set; }
        public Match[] Matches { get; private set; }

        public Week(int nr, Match[] matches)
        {
            Matches = matches;
            Weeknr = nr;
        }
        public int Checkweek(Player host)
        {
            Week hostweek = host.Weeks[Weeknr - 1];
            int score = 0;
            int counter = 0;

            while (counter < Matches.Length)
            {
                score += CheckMatch(hostweek, counter);
                counter++;
            }

            return score;
        }

        private int CheckMatch(Week hostweek, int counter)
        {
            var ThisA = Matches[counter].ResultA;
            var ThisB = Matches[counter].ResultB;
            var HostA = hostweek.Matches[counter].ResultA;
            var HostB = hostweek.Matches[counter].ResultB;
            var ThisWinner = Matches[counter].Winner;
            var HostWinner = hostweek.Matches[counter].Winner;
            var matchScore = 0;

            if (HostA != 99 && ThisA != 99)
            {
                if (ThisWinner == HostWinner)
                {
                    matchScore += 15;
                }

                if (ThisA == HostA)
                {
                    matchScore += 5;
                }

                if (ThisB == HostB)
                {
                    matchScore += 5;
                }

                if (counter == 0)
                {
                    matchScore *= 2;
                }
            }
            return matchScore;
        }

        public string GetMatch(int matchID)
        {
            return Matches[matchID].Winner;
        }

        public int CheckMatch(Player Host, int matchID)
        {
            //MOTW has matchID 0.
            Match HostMatch = Host.Weeks[Weeknr - 1].Matches[matchID];
            Match ThisMatch = Matches[matchID];

            if (HostMatch.ResultA != 99 && ThisMatch.ResultA != 99)
            {
                if (HostMatch.ResultA == ThisMatch.ResultA && ThisMatch.ResultB == HostMatch.ResultB)
                {
                    return 2;
                }

                else if (ThisMatch.Winner == HostMatch.Winner)
                {
                    return 1;
                }

                else
                {
                    return 0;
                }
            }

            return -1;
        }
    }
}
