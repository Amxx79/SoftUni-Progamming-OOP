using HighwayToPeak.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Models
{
    public class BaseCamp : IBaseCamp
    {
        private List<string> camps;

        public BaseCamp()
        {
            camps = new List<string>();
        }
        public IReadOnlyCollection<string> Residents => camps.OrderBy(x => x).ToList().AsReadOnly();

        public void ArriveAtCamp(string climberName)
        {
            camps.Add(climberName);
        }

        public void LeaveCamp(string climberName)
        {
            camps.Remove(climberName);
        }
    }
}
