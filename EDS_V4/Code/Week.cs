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
        public int WeekPostponementScore { get; set; }
        public Match[] Matches { get; private set; }

        public Week(int nr, Match[] matches)
        {
            Matches = matches;
            Weeknr = nr;
            WeekMatchesScore = 0;
            WeekPostponementScore = 0;
        }
        public Dictionary<int,int> Checkweek(Player host, BonusQuestions questions, Dictionary<string, Topscorer> topscorers, int currentcheckingweek)
        {
            Week hostweek = host.Weeks[Weeknr];
            WeekMatchesScore = 0;            
            Dictionary<int, int> postponementscores = new Dictionary<int, int>();
            for(int counter = 0; counter < Matches.Length; counter++)
            {
                var hostmatch = hostweek.Matches[counter];
                int matchscore = Matches[counter].CheckMatch(hostmatch);
                WeekMatchesScore += matchscore;
                if (hostmatch.Postponement > 0 && Weeknr + hostmatch.Postponement == currentcheckingweek)
                {
                    if(postponementscores.ContainsKey(currentcheckingweek))
                        postponementscores[currentcheckingweek] += matchscore;
                    else
                    postponementscores.Add(currentcheckingweek, matchscore);
                }
            }
            WeekBonusScore = questions.CheckBonus(host.Questions, Weeknr, topscorers);
            return postponementscores;
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
