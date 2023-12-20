using HighwayToPeak.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Models
{
    public abstract class Climber : IClimber
    {
        private string name;
        private int stamina;
        private List<string> conqueredPeaks;

        protected Climber(string name, int stamina)
        {
            Name = name;
            Stamina = stamina;
            conqueredPeaks = new List<string>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Climber's name cannot be null or whitespace.");
                }
                name = value;
            }
        }

        public int Stamina
        {
            get => stamina;
            protected set
            {
                if (value <= 0)
                {
                    stamina = 0;
                }
                if (value > 10)
                {
                    stamina = 10;
                }
                else
                {
                    stamina = value;
                }
            }
        }

        public IReadOnlyCollection<string> ConqueredPeaks => conqueredPeaks.AsReadOnly();

        public void Climb(IPeak peak)
        {
            if (peak.DifficultyLevel == "Extreme")
            {
                Stamina -= 6;
            }
            else if (peak.DifficultyLevel == "Hard")
            {
                Stamina -= 4;
            }
            else if (peak.DifficultyLevel == "Moderate")
            {
                Stamina -= 2;
            }
            if (conqueredPeaks.Contains(peak.Name))
                //LOOKIP
            {
                return;
            }
            conqueredPeaks.Add(peak.Name);
        }

        public abstract void Rest(int daysCount);

        public override string ToString()
        {
            StringBuilder sb = new();
            //LOOKIP - CLIMBER
            sb.AppendLine($"{this.GetType().Name} - Name: {Name}, Stamina: {Stamina}");
            if (ConqueredPeaks.Count == 0)
            {
                sb.AppendLine($"Peaks conquered: no peaks conquered");
            }
            else
            {
                sb.AppendLine($"Peaks conquered: {ConqueredPeaks.Count}");
            }
            return sb.ToString().Trim();
        }
    }
}
