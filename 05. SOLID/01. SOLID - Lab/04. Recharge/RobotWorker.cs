using _04._Recharge.Interfaces;

namespace P04.Recharge
{
    public abstract class RobotWorker : IRechargeable
    {
        private string id;
        private int workingHours;

        public RobotWorker(string id)
        {
            this.id = id;
        }

        public void Work(int hours)
        {
            this.workingHours += hours;
        }

        public abstract void Recharge();
    }
}