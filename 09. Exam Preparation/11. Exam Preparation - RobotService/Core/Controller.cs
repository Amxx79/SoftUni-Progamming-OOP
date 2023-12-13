using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private SupplementRepository supplements;
        private RobotRepository robots;

        public Controller()
        {
             supplements = new SupplementRepository();
            robots = new RobotRepository();
        }
        public string CreateRobot(string model, string typeName)
        {
            if (typeName != nameof(DomesticAssistant) && typeName != nameof(IndustrialAssistant))
            {
                return String.Format(OutputMessages.RobotCannotBeCreated, typeName);
            }
            if (typeName == "DomesticAssistant")
            {
                IRobot robot = new DomesticAssistant(model);
                robots.AddNew(robot);
            }
            else if (typeName == "IndustrialAssistant")
            {
                IRobot robot = new IndustrialAssistant(model);
                robots.AddNew(robot);
            }
            return String.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
        }

        public string CreateSupplement(string typeName)
        {
            if (typeName != nameof(SpecializedArm) && typeName != nameof(LaserRadar))
            {
                return String.Format(OutputMessages.SupplementCannotBeCreated, typeName);
            }
            else
            {
                if (typeName == "SpecializedArm")
                {
                    ISupplement suplement = new SpecializedArm();
                    supplements.AddNew(suplement);
                }
                else if (typeName == "LaserRadar")
                {
                    ISupplement suplement = new LaserRadar();
                    supplements.AddNew(suplement);
                }
                return String.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
            }
        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            List<IRobot> robotsWithCorrectInterFace = new List<IRobot>();
            int counter = 0;

            foreach (var robot in robots.Models())
            {
                if (robot.InterfaceStandards.Contains(intefaceStandard))
                {
                    robotsWithCorrectInterFace.Add(robot);
                }
            }
            if (robotsWithCorrectInterFace.Count == 0)
            {
                return String.Format(OutputMessages.UnableToPerform, intefaceStandard);
            }
            robotsWithCorrectInterFace = robotsWithCorrectInterFace
                .OrderByDescending(r => r.BatteryLevel).ToList();
            int sumOfBatteryOfRobots = robotsWithCorrectInterFace.Sum(r => r.BatteryLevel);
            if (sumOfBatteryOfRobots < totalPowerNeeded)
            {
                return String.Format(OutputMessages.MorePowerNeeded, serviceName, (totalPowerNeeded - sumOfBatteryOfRobots));
            }
            else
            {
                foreach (var robot in robotsWithCorrectInterFace)
                {
                    if (robot.BatteryLevel >= totalPowerNeeded)
                    {
                        robot.ExecuteService(totalPowerNeeded);
                        counter++;
                        break;
                    }
                    else
                    {
                        totalPowerNeeded -= robot.BatteryLevel;
                        robot.ExecuteService(robot.BatteryLevel);
                        counter++;
                    }
                }
            }

            return String.Format(OutputMessages.PerformedSuccessfully, serviceName, counter);

        }

        public string Report()
        {
            StringBuilder sb = new();
            List<IRobot> allRobots = robots.Models().OrderByDescending(r => r.BatteryLevel).ThenBy(r => r.BatteryCapacity).ToList();
            foreach (var robot in allRobots)
            {
                sb.AppendLine(robot.ToString());
            }
            return sb.ToString().Trim();
        }

        public string RobotRecovery(string model, int minutes)
        {
            List<IRobot> robotsToFeed = new List<IRobot>();
            robotsToFeed = robots.Models().Where(r => r.Model == model && r.BatteryLevel < r.BatteryCapacity / 2).ToList();

            foreach (var robot in robotsToFeed)
            {
                robot.Eating(minutes);
            }
            return $"Robots fed: {robotsToFeed.Count}";
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            ISupplement supplement = supplements.Models()
                .FirstOrDefault(s => s.GetType().Name == supplementTypeName);
            int interfaceValue = supplement.InterfaceStandard;

            List<IRobot> robotsWhichHaveNotSupplement = new List<IRobot>();
            foreach (var robot in robots.Models())
            {
                foreach (var interfaceValueOnCurrentRobot in robot.InterfaceStandards)
                {
                    if (interfaceValueOnCurrentRobot == interfaceValue)
                    {
                        break;
                    }
                    break;
                }
                if (robot.Model == model && !robot.InterfaceStandards.Contains(interfaceValue))
                {
                    robotsWhichHaveNotSupplement.Add(robot);
                }
            }

            if (robotsWhichHaveNotSupplement.Count == 0)
            {
                return String.Format(OutputMessages.AllModelsUpgraded, model);
            }
            else
            {
                robotsWhichHaveNotSupplement = robotsWhichHaveNotSupplement.Take(1).ToList();
                foreach (var robot in robotsWhichHaveNotSupplement)
                {
                    robot.InstallSupplement(supplement);
                }
            }
            return String.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);
        }
    }
}
