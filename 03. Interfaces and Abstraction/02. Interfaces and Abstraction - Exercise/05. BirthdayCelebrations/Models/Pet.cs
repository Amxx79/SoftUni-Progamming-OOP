using BirthdayCelebrations.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayCelebrations.Models
{
    public class Pet : IPetable
    {
        public Pet(string name, string[] birthday)
        {
            Name = name;
            BirthDay = birthday;
        }
        public string Name { get; set; }

        public string[] BirthDay { get; set; }
    }
}
