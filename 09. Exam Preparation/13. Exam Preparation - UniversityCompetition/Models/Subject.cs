using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;

namespace UniversityCompetition.Models
{
    public abstract class Subject : ISubject
    {
        private string name;

        protected Subject(string name, int id, double rate)
        {
            Name = name;
            Id = id;
            Rate = rate;
        }

        public int Id { get; private set; }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace!");
                }
                name = value;
            }
        }

        public double Rate { get; private set; }
    }
}
