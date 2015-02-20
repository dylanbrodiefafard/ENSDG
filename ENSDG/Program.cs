using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using ENSCC;
namespace ENSDG
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Downloading current database");
            DataGrabber dg = new DataGrabber();
            
            Console.ReadKey();

        }
    }
}