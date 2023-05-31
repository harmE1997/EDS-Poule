using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
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
            using (FileStream stream = new FileStream(GeneralConfiguration.SaveFileLocation, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, Players);
            }
        }

        public void LoadPlayers()
        {
            if (!File.Exists(GeneralConfiguration.SaveFileLocation))
                SavePlayers();

            using (FileStream stream = new FileStream(GeneralConfiguration.SaveFileLocation, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Players = (List<Player>)formatter.Deserialize(stream);
            }
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

        public void CheckAllPlayers(Host host, int currentWeek)
        {

            foreach (Player player in Players)
            {
                player.CheckPlayer(host, currentWeek, host.getTopscorers());
            }

            SavePlayers();
        }

        public int GetAverageScore()
        {
            int total = 0;
            foreach (var p in Players)
            {
                total += p.WeekMatchesScore;
            }
            return total / Players.Count;
        }
    }
}
