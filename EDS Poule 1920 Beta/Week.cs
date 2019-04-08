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
        public int checkweek(Player host)
        {
            Week hostweek = host.Weeks[Weeknr - 1];
            int score = 0;
            int counter = 0;

            while (counter < 9)
            {
                var ThisA = Matches[counter].ResultA;
                var ThisB = Matches[counter].ResultB;
                var HostA = hostweek.Matches[counter].ResultA;
                var HostB = hostweek.Matches[counter].ResultB;
                var ThisWinner = Matches[counter].Winner;
                var HostWinner = hostweek.Matches[counter].Winner;

                if (HostA != 99 && ThisA != 99)
                {
                    if (ThisWinner == HostWinner)
                    {
                        score += 10;
                    }

                    if (ThisA == HostA)
                    {
                        score += 5;
                    }

                    if (ThisB == HostB)
                    {
                        score += 5;
                    }

                    if (counter == 0)
                    {
                        score *= 2;
                    }
                }
                    counter++;
            }

            return score;
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
