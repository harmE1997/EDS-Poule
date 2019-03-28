using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace EDS_Poule
{
    [Serializable]
    public class PlayerManager
    {
        public List<Player> Players {get; private set;}
        private string FileName;

        public PlayerManager()
        {
            Players = new List<Player>();
            FileName = "EDS1920";
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public void SavePlayers()
        {
            using (FileStream stream = new FileStream(FileName, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, Players);
            }
        }

        public void LoadPlayers()
        {
            if (!File.Exists(FileName))
                throw new FileNotFoundException(FileName);

            using (FileStream stream = new FileStream(FileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Players = (List<Player>)formatter.Deserialize(stream);
            }
        }

        public void RankPlayers()
        {
            Players.Sort();
            Players.Reverse();

            int index = 1;
            int ranking = 1;
            int previousScore = 5000;

            foreach (Player player in Players)
            {
                if (player.TotalScore < previousScore)
                    ranking = index;

                player.SetRanking(ranking);
                index++;
                previousScore = player.TotalScore;

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

        public int removePlayer(string name)
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

        public void checkAllPlayers(Player Host, int currentWeek)
        {
            foreach (Player player in Players)
                player.checkPlayer(Host, currentWeek);
            
            SavePlayers();
        }
    }
}
