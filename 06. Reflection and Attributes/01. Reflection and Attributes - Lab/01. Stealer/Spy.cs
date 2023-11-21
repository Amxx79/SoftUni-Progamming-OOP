using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Stealer
{
    public class Spy
    {
        public string StealFieldInfo(string name, params string[] nameOfFields)
        {
            Type classType = Type.GetType(name);
            FieldInfo[] classFields = classType.GetFields((BindingFlags)60);

            StringBuilder sb = new();

            Object classInstance = Activator.CreateInstance(classType, new object[] { });

            sb.AppendLine($"Class under investigation: {name}");

            foreach (FieldInfo field in classFields.Where(f => nameOfFields.Contains(f.Name)))
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
            }

            return sb.ToString().TrimEnd();
        }

    }
}