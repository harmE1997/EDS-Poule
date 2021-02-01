using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Configuration;

namespace EDS_Poule
{
    [Serializable]
    public class PlayerManager
    {
        public List<Player> Players { get; private set; }
        private string FilePath;

        public PlayerManager()
        {
            Players = new List<Player>();
            FilePath = ConfigurationManager.AppSettings.Get("SaveFileLocation");
        }

        public void AddPlayer(Player player)
        {
            if (player != null)
            {
                Players.Add(player);
                SavePlayers();
            }
        }

        private void SavePlayers()
        {
            using (FileStream stream = new FileStream(FilePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, Players);
            }
        }

        public void LoadPlayers()
        {
            if (!File.Exists(FilePath))
                SavePlayers();

            using (FileStream stream = new FileStream(FilePath, FileMode.Open))
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
                return -1;
            }

        }

        public IEnumerable<int> CheckAllPlayers(Player Host, int currentWeek, bool recalculate = false)
        {
            int i = 0;
            ExcelManager em = new ExcelManager();
            var topscorers = em.readtopscorers(currentWeek, ConfigurationManager.AppSettings.Get("AdminLocation"), 8);

            foreach (Player player in Players)
            {
                player.CheckPlayer(Host, currentWeek, topscorers, recalculate);
                i++;
                yield return i;
            }

            SavePlayers();
        }

        public int GetAverageScore()
        {
            int total = 0;
            foreach (var p in Players)
            {
                total += p.WeekScore;
            }
            return total / Players.Count;
        }
    }
}
