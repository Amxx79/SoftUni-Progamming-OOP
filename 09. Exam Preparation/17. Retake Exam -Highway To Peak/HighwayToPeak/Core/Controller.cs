using HighwayToPeak.Core.Contracts;
using HighwayToPeak.Models;
using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Core
{
    public class Controller : IController
    {
        private readonly PeakRepository peakRepository;
        private readonly ClimberRepository climberRepository;
        private readonly BaseCamp baseCamp;

        public Controller()
        {
            this.peakRepository = new PeakRepository();
            this.climberRepository = new ClimberRepository();
            this.baseCamp = new BaseCamp();
        }
        public string AddPeak(string name, int elevation, string difficultyLevel)
        {
            IPeak peak = peakRepository.All.FirstOrDefault(p => p.Name == name);
            if (peak != null)
            {
                return $"{name} is already added as a valid mountain destination.";
            }
            if (difficultyLevel != "Extreme" && difficultyLevel != "Hard" && difficultyLevel != "Moderate")
            {
                return $"{difficultyLevel} peaks are not allowed for international climbers.";
            }
            peak = new Peak(name, elevation, difficultyLevel);
            peakRepository.Add(peak);
            //LOOKUP
            return $"{name} is allowed for international climbing. See details in {peakRepository.GetType().Name}.";
        }

        public string AttackPeak(string climberName, string peakName)
        {
            IClimber climber = climberRepository.All.FirstOrDefault(c => c.Name == climberName);
            if (climber == null)
            {
                return $"Climber - {climberName}, has not arrived at the BaseCamp yet.";
            }
            IPeak peak = peakRepository.All.FirstOrDefault(p => p.Name == peakName);
            if (peak == null)
            {
                return $"{peakName} is not allowed for international climbing.";
            }
            if (!baseCamp.Residents.Contains(climberName))
            {
                return $"{climberName} not found for gearing and instructions. The attack of {peakName} will be postponed.";
            }
            if (peak.DifficultyLevel == "Extreme")
            {
                if (climber.GetType().Name == "NaturalClimber")
                {
                    return $"{climberName} does not cover the requirements for climbing {peakName}.";
                }
            }
            baseCamp.LeaveCamp(climber.Name);
            climber.Climb(peak);
            if (climber.Stamina <= 0)
            {
                return $"{climberName} did not return to BaseCamp.";
            }
            else
            {
                baseCamp.ArriveAtCamp(climber.Name);
                return $"{climberName} successfully conquered {peakName} and returned to BaseCamp.";
            }
        }

        public string BaseCampReport()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("BaseCamp residents:");
            if (baseCamp.Residents.Count == 0)
            {
                sb.AppendLine("BaseCamp is currently empty.");
            }
            else
            {
                foreach (var climber in baseCamp.Residents)
                {
                    IClimber climberFromRepo = climberRepository.All.FirstOrDefault(c => c.Name == climber);
                    sb.AppendLine($"Name: {climberFromRepo.Name}, Stamina: {climberFromRepo.Stamina}, Count of Conquered Peaks: {climberFromRepo.ConqueredPeaks.Count}");
                }
            }
            return sb.ToString().Trim();
        }

        public string CampRecovery(string climberName, int daysToRecover)
        {
            if (!baseCamp.Residents.Contains(climberName))
            {
                return $"{climberName} not found at the BaseCamp.";
            }
            IClimber climber = climberRepository.All.FirstOrDefault(c => c.Name == climberName);
            if (climber.Stamina == 10)
            {
                return $"{climberName} has no need of recovery.";
            }
            climber.Rest(daysToRecover);
            return $"{climberName} has been recovering for {daysToRecover} days and is ready to attack the mountain.";
        }

        public string NewClimberAtCamp(string name, bool isOxygenUsed)
        {
            IClimber climber = climberRepository.All.FirstOrDefault(c => c.Name == name);
            if (climber != null)
            {
                return $"{name} is a participant in {nameof(ClimberRepository)} and cannot be duplicated.";
            }
            if (isOxygenUsed)
            {
                climber = new OxygenClimber(name);
            }
            else if (isOxygenUsed == false)
            {
                climber = new NaturalClimber(name);
            }
            climberRepository.Add(climber);
            baseCamp.ArriveAtCamp(climber.Name);
            return $"{name} has arrived at the BaseCamp and will wait for the best conditions.";
        }

        public string OverallStatistics()
        {
            StringBuilder sb = new();
            List<IClimber> climbers = climberRepository.All
                .OrderByDescending(c => c.ConqueredPeaks.Count)
                .ThenBy(c => c.Name).ToList();
            List<IPeak> peaks = new List<IPeak>();
            sb.AppendLine("***Highway-To-Peak***");

            foreach (var climber in climbers)
            {
                peaks = new List<IPeak>();
                foreach (var peak in climber.ConqueredPeaks)
                {
                    IPeak currentPeak = peakRepository.All.FirstOrDefault(p => p.Name == peak);
                    peaks.Add(currentPeak);
                }
                peaks = peaks.OrderByDescending(p => p.Elevation).ToList();
                sb.AppendLine($"{climber.ToString()}");
                foreach (var peak in peaks)
                {
                    sb.AppendLine(peak.ToString());
                }
            }
            return sb.ToString().Trim();
        }
    }
}
