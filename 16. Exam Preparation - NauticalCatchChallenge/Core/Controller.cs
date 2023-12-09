using NauticalCatchChallenge.Core.Contracts;
using NauticalCatchChallenge.Models;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories;
using NauticalCatchChallenge.Repositories.Contracts;
using NauticalCatchChallenge.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Core
{
    public class Controller : IController
    {
        private IRepository<IDiver> diverRepo;
        private IRepository<IFish> fishRepo;

        public Controller()
        {
            diverRepo = new DiverRepository();
            fishRepo = new FishRepository();
        }
        public string ChaseFish(string diverName, string fishName, bool isLucky)
        {
            if (diverRepo.GetModel(diverName) == null)
            {
                return string.Format(OutputMessages.DiverNotFound, nameof(DiverRepository), diverName);

            }
            if (fishRepo.GetModel(fishName) == null)
            {
                return string.Format(OutputMessages.FishNotAllowed, fishName);
            }
            IDiver diver = diverRepo.GetModel(diverName);

            if (diver.HasHealthIssues == true)
            {
                return String.Format(OutputMessages.DiverHealthCheck, diverName);
            }

            IFish fish = fishRepo.GetModel(fishName);
            if (diver.OxygenLevel < fish.TimeToCatch)
            {
                diver.Miss(fish.TimeToCatch);
                if (diver.OxygenLevel == 0)
                {
                    diver.UpdateHealthStatus();
                }

                return string.Format(OutputMessages.DiverMisses, diverName, fishName);
            }

            else if (diver.OxygenLevel == fish.TimeToCatch && !isLucky)
            {
                diver.Miss(fish.TimeToCatch);
                if (diver.OxygenLevel == 0)
                {
                    diver.UpdateHealthStatus();
                }

                return string.Format(OutputMessages.DiverMisses, diverName, fishName);
            }
            else //(diver.OxygenLevel > fish.TimeToCatch)
            {
                diver.Hit(fish);
                if (diver.OxygenLevel <= 0)
                {
                    diver.UpdateHealthStatus();
                }
                return string.Format(OutputMessages.DiverHitsFish, diverName, fish.Points, fishName);
            }
        }

        public string CompetitionStatistics()
        {
            StringBuilder sb = new();
            List<IDiver> diversOrdered = diverRepo
                .Models.OrderByDescending(d => d.CompetitionPoints)
                .ThenByDescending(d => d.Catch.Count).ThenBy(d => d.Name)
                .Where(d => d.HasHealthIssues == false).ToList();

            sb.AppendLine("**Nautical-Catch-Challenge**");
            foreach (IDiver diver in diversOrdered)
            {
                sb.AppendLine($"{diver.ToString()}");
            }
            return sb.ToString().Trim();
        }

        public string DiveIntoCompetition(string diverType, string diverName)
        {
            string result = string.Empty;

            if (diverType != nameof(FreeDiver) && diverType != nameof(ScubaDiver))
            {
                result = string.Format(OutputMessages.DiverTypeNotPresented, diverType);

            }
            IDiver diver = diverRepo.Models.FirstOrDefault(d => d.Name == diverName);
            if (diver != null)
            {
                result = string.Format(OutputMessages.DiverNameDuplication, diverName, nameof(DiverRepository));

            }
            else
            {

                if (diverType == "ScubaDiver")
                {
                    diver = new ScubaDiver(diverName);
                    diverRepo.AddModel(diver);
                    result = string.Format(OutputMessages.DiverRegistered, diverName, nameof(DiverRepository));
                }
                else if (diverType == "FreeDiver")
                {
                    diver = new FreeDiver(diverName);
                    diverRepo.AddModel(diver);
                    result = string.Format(OutputMessages.DiverRegistered, diverName, nameof(DiverRepository));
                }
            }
            return result.Trim();
        }

        public string DiverCatchReport(string diverName)
        {
            StringBuilder sb = new();
            IDiver diver = diverRepo.GetModel(diverName);
            sb.AppendLine(diver.ToString());
            sb.AppendLine("Catch Report:");
            foreach (var diverFishs in diver.Catch)
            {
                IFish fish = fishRepo.GetModel(diverFishs);
                sb.AppendLine($"{fish.ToString()}");
            }
            return sb.ToString().Trim();
        }

        public string HealthRecovery()
        {
            int recoveredDivers = 0;
            foreach (var diver in diverRepo.Models.Where(d => d.HasHealthIssues == true))
            {
                diver.UpdateHealthStatus();
                diver.RenewOxy();
                recoveredDivers++;
            }
            return $"Divers recovered: {recoveredDivers}";
        }

        public string SwimIntoCompetition(string fishType, string fishName, double points)
        {
            if (fishType != "ReefFish" && fishType != "DeepSeaFish" && fishType != "PredatoryFish")
            {
                return string.Format(OutputMessages.FishTypeNotPresented, fishType);

            }
            IFish fish = fishRepo.Models.FirstOrDefault(f => f.Name == fishName);
            if (fish != null)
            {
                return string.Format(OutputMessages.FishNameDuplication, fishName, nameof(FishRepository));

            }
            else
            {

                if (fishType == "ReefFish")
                {
                    fish = new ReefFish(fishName, points);
                }
                else if (fishType == "DeepSeaFish")
                {
                    fish = new DeepSeaFish(fishName, points);
                }
                else if (fishType == "PredatoryFish")
                {
                    fish = new PredatoryFish(fishName, points);
                }
            }
            fishRepo.AddModel(fish);
            return $"{fish.Name} is allowed for chasing.";
        }
    }
}
