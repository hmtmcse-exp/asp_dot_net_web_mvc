using System;
using System.Runtime.InteropServices.ComTypes;
using isms.Data;

namespace isms.Services
{
    public class OperatorService
    {
        public static Boolean isContain(string data, string data2)
        {
            return data.Contains(data2);
        }
    }
}