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
                if (hostweek.Matches[counter].ResultA != 99 && Matches[counter].ResultA != 99)
                {
                    if (Matches[counter].ResultA == hostweek.Matches[counter].ResultA
                        && Matches[counter].ResultB == hostweek.Matches[counter].ResultB)
                    {
                        score += 10;
                    }

                    else if ((Matches[counter].ResultA == hostweek.Matches[counter].ResultA
                        || Matches[counter].ResultB == hostweek.Matches[counter].ResultB)
                        && Matches[counter].Winner == hostweek.Matches[counter].Winner)
                    {
                        score += 8;
                    }

                    else if ((Matches[counter].ResultA != hostweek.Matches[counter].ResultA
                        && Matches[counter].ResultB != hostweek.Matches[counter].ResultB)
                        && Matches[counter].Winner == hostweek.Matches[counter].Winner)
                    {
                        score += 6;
                    }

                    else if ((Matches[counter].ResultA == hostweek.Matches[counter].ResultA
                    || Matches[counter].ResultB == hostweek.Matches[counter].ResultB)
                    && Matches[counter].Winner != hostweek.Matches[counter].Winner)
                    {
                        score += 2;
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
        
        public int CheckMOTW(Player Host)
        {
            int matchID = 0;
            Week hostweek = Host.Weeks[Weeknr - 1];
            if (hostweek.Matches[matchID].ResultA != 99 && Matches[matchID].ResultA != 99)
            {
                if (Matches[matchID].ResultA == hostweek.Matches[matchID].ResultA
                    && Matches[matchID].ResultB == hostweek.Matches[matchID].ResultB)
                {
                    return 2;
                }

                else if (Matches[matchID].Winner == hostweek.Matches[matchID].Winner)
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
