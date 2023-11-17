using _04._Recharge.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04._Recharge
{
    public abstract class HumanWorker : ISleeper
    {
        private string id;
        private int workingHours;

        public HumanWorker(string id)
        {
            this.id = id;
        }

        public abstract void Sleep();

        public void Work(int hours)
        {
            this.workingHours += hours;
        }

    }
}
