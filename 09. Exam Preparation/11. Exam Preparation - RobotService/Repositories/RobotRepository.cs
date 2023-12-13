using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
{
    public class RobotRepository : IRepository<IRobot>
    {
        private List<IRobot> robots;
        public RobotRepository()
        {
            robots = new List<IRobot>();
        }
        public void AddNew(IRobot model)
        {
            robots.Add(model);
        }

        public IRobot FindByStandard(int interfaceStandard)
        {
            IRobot robot = robots.FirstOrDefault(r => r.InterfaceStandards.Contains(interfaceStandard));
            if (robot != null)
            {
                return robot;
            }
            else
            {
                return null;
            }
        }

        public IReadOnlyCollection<IRobot> Models() => robots;

        public bool RemoveByName(string robotModel)
        {
            IRobot robot = robots.FirstOrDefault(r => r.Model == robotModel);
            return robots.Remove(robot);
        }
    }
}
