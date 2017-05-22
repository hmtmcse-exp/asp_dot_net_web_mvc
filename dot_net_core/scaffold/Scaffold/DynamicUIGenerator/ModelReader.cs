using System;
using System.Reflection;

namespace Scaffold.DynamicUIGenerator
{
    public class ModelReader
    {
        public static void Read(string model)
        {
            Type type = Type.GetType(model);
            foreach(var prop in type.GetProperties()) {
                Console.WriteLine("{0}", prop.Name);
            }
            
        }
    }
}