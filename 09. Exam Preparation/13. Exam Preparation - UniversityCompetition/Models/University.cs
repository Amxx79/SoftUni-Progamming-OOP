using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UniversityCompetition.Models.Contracts;

namespace UniversityCompetition.Models
{
    public class University : IUniversity
    {
        private string name;
        private string category;
        private int capacity;
        private List<int> requiredSubjects;
        private string[] validCatogories = { "Technical", "Economical", "Humanity" };

        public University(int universityId, string uniName, string category, int capacity, List<int> requiredSubs)
        {
            Id = universityId;
            this.name = uniName;
            this.category = category;
            this.capacity = capacity;
            requiredSubjects = requiredSubs;
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

        public string Category
        {
            get => category;
            private set
            {
                if (value != validCatogories[0] && value != validCatogories[1] && value != validCatogories[2])
                {
                    throw new ArgumentException($"University category {value} is not allowed in the application!");
                }
                category = value;
            }
        }

        public int Capacity
        {
            get => capacity;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("University capacity cannot be a negative value!");
                }
                capacity = value;
            }
        }



        public IReadOnlyCollection<int> RequiredSubjects => requiredSubjects.AsReadOnly();
    }
}
