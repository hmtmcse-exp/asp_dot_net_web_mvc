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
            
            for(int i = 0; i < args.Length; i++)
            {
                Console.WriteLine("Arg[{0}] = [{1}]", i, args[i]);
            }
            
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
