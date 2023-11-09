using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayCelebrations.Models.Interfaces
{
    public interface IPetable
    {
        string Name { get; }
        string[] BirthDay { get; }
    }
}
