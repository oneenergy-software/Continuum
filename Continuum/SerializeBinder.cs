using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{    
    public class SerializeBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Debug.WriteLine($"DESERIALIZING TYPE: {typeName}");

            return Type.GetType($"{typeName}, {assemblyName}");
        }
    }
    
}
