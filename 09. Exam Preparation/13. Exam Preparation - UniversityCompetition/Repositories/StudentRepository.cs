using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class StudentRepository : IRepository<IStudent>
    {
        private List<IStudent> models;
        public StudentRepository()
        {
            models = new List<IStudent>();
        }

        public IReadOnlyCollection<IStudent> Models => models.AsReadOnly();

        public void AddModel(IStudent model)
        {
            models.Add(model);
        }

        public IStudent FindById(int id)
        {
            return models.FirstOrDefault(s => s.Id == id);
        }

        public IStudent FindByName(string name)
        {
            string[] namesConcat = name.Split().ToArray();
            return models.FirstOrDefault(s => s.FirstName == namesConcat[0] && s.LastName == namesConcat[1]);
        }
    }
}
