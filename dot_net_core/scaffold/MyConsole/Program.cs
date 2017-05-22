using System;

namespace MyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Type t = Type.GetType("MyConsole.Student");
            Console.WriteLine("Hello World!");
            
            Type myType = typeof(Student);
// Get the namespace of the myClass class.
            Console.WriteLine("Namespace: {0}.", myType.Namespace);
            
        }
    }
}
