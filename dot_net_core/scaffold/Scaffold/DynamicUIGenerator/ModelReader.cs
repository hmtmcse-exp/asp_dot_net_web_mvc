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

        public static void ModelListByNamespace(string modelNamespace)
        {
            if (modelNamespace == null)
            {
                Console.WriteLine("Please provide Model Namespace");
            }
            else
            {
                Assembly myAssembly = Assembly.GetEntryAssembly();
                foreach (Type type in myAssembly.GetTypes())
                {
                    var klassName = type.FullName;
                    if (klassName != null && klassName.StartsWith(modelNamespace))
                    {
                        Console.WriteLine(klassName); 
                    }
                }
            }
        }

        public static void Generate(string modelNamespace)
        {
            if (modelNamespace == null)
            {
                Console.WriteLine("Please provide Model With Namespace");
            }
            else
            {
                Console.WriteLine("Code Base: " + Directory.GetParent(Directory.GetCurrentDirectory()).FullName);
                Assembly myAssembly = Assembly.GetEntryAssembly();
                Type type = myAssembly.GetType(modelNamespace);
                if (type == null)
                {
                    Console.WriteLine("Invalid Model.");
                }
                else
                {
                    var modelName = type.Name;  
                    
                    // Need To Check. boolean, enum, object, Integer, float
                    
                    
                    foreach(var prop in type.GetProperties())
                    {                    
//                        var atttrs = prop.GetCustomAttributes(false);
//                        foreach (var attr in atttrs)
//                        {
//                            if (attr.GetType().Name.Equals("DisplayAttribute"))
//                            {
//                                Console.WriteLine("Display Name {0}",  prop.GetCustomAttribute<DisplayAttribute>().Name); 
//                            }
//                            else if (attr.GetType().Name.Equals("DataTypeAttribute"))
//                            {
//                                Console.WriteLine("Data Type {0}",  prop.GetCustomAttribute<DataTypeAttribute>().DataType); 
//                            }
//                            Console.WriteLine("{0}", attr.GetType().Name);   
//                        }

                        if (prop.PropertyType.Name.Equals("Boolean"))
                        {
                            Console.WriteLine(" Checkbox "); 
                        }
                        else if (prop.PropertyType.Name.Equals("String"))
                        {
                            Console.WriteLine(" Textbox "); 
                        }
                        else if (prop.PropertyType.Name.Equals("Byte[]"))
                        {
                            Console.WriteLine(" Hidden Row "); 
                        }
                        else if (prop.PropertyType.Name.Equals("Int32"))
                        {
                            Console.WriteLine(" Number ");
                        }
                        else
                        {
                            Console.WriteLine(" Select "); 
                        }
                            


                        
                        Console.WriteLine("{0} {1} ", prop.Name, prop.PropertyType.Name);
                    }
                }


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