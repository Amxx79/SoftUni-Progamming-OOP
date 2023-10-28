using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayersAndMonsters
{
    public class Hero
    {
        private string username;
        private int level;

        public Hero(string username, int level)
        {
            this.Username = username;
            this.Level = level;
        }

        public string Username
        {
            get 
            { 
                return username; 
            }
            set
            {
                username = value;
            }
        }
        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }
        public override string ToString()
        {
            return $"Type: {GetType().Name} Username: {Username} Level: {Level}";
        }
    }
}
