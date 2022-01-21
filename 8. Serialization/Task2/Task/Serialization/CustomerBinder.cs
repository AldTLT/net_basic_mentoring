using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Task.DB;

namespace Task.Serialization
{
    public class CustomerBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            if (typeName.Contains("DynamicProxies.Category"))
            {
                return typeof(Category);
            }

            var typeToDeserialize = Type.GetType($"{typeName}, {assemblyName}");

            return typeToDeserialize;
        }
    }
}
