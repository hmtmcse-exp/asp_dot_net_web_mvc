using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Scaffold.DynamicUIGenerator;

namespace Scaffold
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            var action = args[0];
            switch (action)
            {
                case "list":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Please provide Model Namespace");
                    }
                    else
                    {
                        ModelReader.ModelListByNamespace(args[1]);  
                    }
                    break;
                case "generate":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Please provide Model With Namespace");
                    }
                    else
                    {
                        ModelReader.Generate(args[1]);  
                    }
                    break;
            }

            
            
//            var host = new WebHostBuilder()
//                .UseKestrel()
//                .UseContentRoot(Directory.GetCurrentDirectory())
//                .UseIISIntegration()
//                .UseStartup<Startup>()
//                .Build();
//
//            host.Run();
        }
    }
}
