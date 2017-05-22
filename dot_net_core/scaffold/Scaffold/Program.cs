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
            ModelReader.Read(args[0]);
            
            var action = args[0];

            switch (action)
            {
                case "list":
                    ModelReader.ModelList();
                    break;
                case "generate":
                    ModelReader.Read(args[0]);
                    break;
            }
            
            Console.WriteLine(action);

            
            
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
