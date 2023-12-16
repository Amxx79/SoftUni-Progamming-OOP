using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityCompetition.Models
{
    public class EconomicalSubject : Subject
    {
        private const double rate = 1.0;

        public EconomicalSubject(string name, int id) 
            : base(name, id, rate)
        {
        }
    }
}
