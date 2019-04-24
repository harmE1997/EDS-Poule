using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_Poule
{
    class Stat : IComparable<Stat>
    {
        public string Name { get; private set; }
        public int Number { get; private set; }
        public List<string> Names { get; private set; }

        public Stat(string name, string playername)
        {
            Name = name;
            Number = 1;
            Names = new List<string>();
            Names.Add(playername);
        }

        public void Add(string playerName)
        {
            Number++;
            Names.Add(playerName);
        }

        public string statToString()
        {
            return Name + "\t\t" + Number.ToString();
        }

        public int CompareTo(Stat other)
        {
            if (other != null)
            {
                return Number.CompareTo(other.Name);
            }

            else
            {
                throw new ArgumentNullException("OtherStat");
            }
        }
    }
}
