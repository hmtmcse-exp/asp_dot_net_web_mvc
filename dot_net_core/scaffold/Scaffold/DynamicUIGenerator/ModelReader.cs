using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Scaffold.DynamicUIGenerator
{
    public class ModelReader
    {
        public static void Read(string model)
        {
            var type = Type.GetType(model);
            if (type == null) return;
            foreach(var prop in type.GetProperties()) {
                Console.WriteLine("{0}", prop.Name);
            }
        }
        
        public static void ModelList()
        {
            Assembly myAssembly = Assembly.GetEntryAssembly();
            Console.WriteLine(myAssembly.CodeBase);
            Console.WriteLine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName);
            foreach (Type type in myAssembly.GetTypes())
            {
                var klassName = type.FullName;
                if (klassName != null && klassName.StartsWith("Scaffold.Models"))
                {
                    Console.WriteLine(klassName); 
                    foreach(var prop in type.GetProperties())
                    {
                        var atttrs = prop.GetCustomAttributes(false);
                        foreach (var attr in atttrs)
                        {
                            if (attr.GetType().Name.Equals("DisplayAttribute"))
                            {
                                Console.WriteLine("Display Name {0}",  prop.GetCustomAttribute<DisplayAttribute>().Name); 
                            }
                            else if (attr.GetType().Name.Equals("DataTypeAttribute"))
                            {
                                Console.WriteLine("Data Type {0}",  prop.GetCustomAttribute<DataTypeAttribute>().DataType); 
                            }
                            Console.WriteLine("{0}", attr.GetType().Name);  
                        }
                        Console.WriteLine("{0} {1}", prop.Name, prop.PropertyType );
                    }
                }
                
            }
        }
        
       
    }
}