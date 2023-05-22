using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_V4.Code
{
    [Serializable]
    public class Week
    {
        public int Weeknr { get; private set; }
        public int WeekMatchesScore { get; private set; }
        public int WeekBonusScore { get; private set; }
        public Match[] Matches { get; private set; }

        public Week(int nr, Match[] matches)
        {
            Matches = matches;
            Weeknr = nr;
            WeekMatchesScore = 0;
        }
        public void Checkweek(Player host, BonusQuestions questions, Dictionary<string, Topscorer> topscorers)
        {
            Week hostweek = host.Weeks[Weeknr - 1];
            WeekMatchesScore = 0;
            int counter = 0;
            foreach (var match in Matches)
            {
                WeekMatchesScore += match.CheckMatch(hostweek.Matches[counter]);
                counter++;
            }
            WeekBonusScore = questions.CheckBonus(host.Questions, Weeknr, topscorers);
        }

        public int CheckMatchOnResultOnly(Match[] Host, int matchID)
        {
            //MOTW has matchID 0.
            Match HostMatch = Host[matchID];
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
