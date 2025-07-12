using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace EDS_V4.Code
{
    [Serializable]
    public class PlayerManager
    {
        public List<Player> Players { get; private set; }

        public PlayerManager()
        {
            Players = new List<Player>();
        }

        public int AddPlayer(Player player, bool AllowOverwrite)
        {
            if (player == null)
                return 1;
            
            if (FindPlayer(player.Name) != null && !AllowOverwrite)
                return 2;

            else if (FindPlayer(player.Name) != null && AllowOverwrite)
                RemovePlayer(player.Name);

            Players.Add(player);
            SavePlayers();
            return 0;          
        }

        private void SavePlayers()
        {
            if (!string.IsNullOrEmpty(GeneralConfiguration.SaveFileLocation))
            {
                try
                {
                    string output = JsonSerializer.Serialize(Players, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(GeneralConfiguration.SaveFileLocation, output);
                }

                catch { return; }
            }
        }

        public void LoadPlayers()
        {
            if (string.IsNullOrEmpty(GeneralConfiguration.SaveFileLocation) || !File.Exists(GeneralConfiguration.SaveFileLocation))
            {
                SavePlayers();
                return;
            }

            string input = File.ReadAllText(GeneralConfiguration.SaveFileLocation);
            Players = JsonSerializer.Deserialize<List<Player>>(input, new JsonSerializerOptions { WriteIndented = true });
        }

        public void RankPlayers(bool previous)
        {
            if (previous)
            {
                Players = Players.OrderBy(p => p.PreviousScore).ToList();
            }
            else
            {
                Players = Players.OrderBy(p => p.TotalScore).ToList();
            }

            Players.Reverse();
            int index = 1;
            int ranking = 1;
            int previousScore = 10000;

            foreach (Player player in Players)
            {
                if (previous)
                {
                    if (player.PreviousScore < previousScore)
                        ranking = index;

                    player.PreviousRanking = ranking;
                    previousScore = player.PreviousScore;
                }

                else
                {
                    if (player.TotalScore < previousScore)
                        ranking = index;

                    player.Ranking = ranking;
                    previousScore = player.TotalScore;
                }

                player.RankingDifference = (player.Ranking - player.PreviousRanking) * -1;
                index++;
            }
        }

        public Player FindPlayer(string name)
        {
            foreach (Player player in Players)
            {
                if (player.Name == name)
                {
                    return player;
                }
            }

            return null;
        }

        public int RemovePlayer(string name)
        {
            Player exitplayer = FindPlayer(name);
            if (exitplayer != null)
            {
                Players.Remove(exitplayer);
                SavePlayers();
                return 0;
            }

            else
            {
                return 1;
            }

        }

        public void CheckAllPlayers(Host host, int startWeek, int endWeek, bool periodCalculation)
        {

            foreach (Player player in Players)
            {
                player.CheckPlayer(host, startWeek, endWeek, host.getTopscorers(), periodCalculation);
            }

            SavePlayers();
        }

        public int GetAverageScore(int weeknr)
        {
            int total = 0;
            foreach (var p in Players)
            {
                total += p.Weeks[weeknr].WeekMatchesScore;
            }
            return total / Players.Count;
        }
    }
}
