using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_Poule.Code
{
    [Serializable]
    public class Week
    {
        public int Weeknr { get; private set; }
        public int WeekScore { get; private set; }
        public Match[] Matches { get; private set; }

        public Week(int nr, Match[] matches)
        {
            Matches = matches;
            Weeknr = nr;
            WeekScore = 0;
        }
        public void Checkweek(Player host, BonusQuestions questions, Dictionary<string, Topscorer> topscorers)
        {
            Week hostweek = host.Weeks[Weeknr - 1];
            WeekScore = 0;
            for (int counter = 0; counter < Matches.Length; counter++)
            {
                WeekScore += CheckMatch(hostweek, counter);
            }

            questions.CheckBonus(host.Questions, Weeknr, topscorers);
            WeekScore += questions.WeekScore;
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

                if (hostweek.Matches[counter].MOTW)
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
