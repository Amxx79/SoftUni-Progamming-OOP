﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityCompetition.Models
{
    public class TechnicalSubject : Subject
    {
        private const double rate = 1.3;
        public TechnicalSubject(string name, int id) 
            : base(name, id, rate)
        {
        }
    }
}
