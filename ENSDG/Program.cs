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

            DataGrabber dg = new DataGrabber();
            Console.ReadKey();
            // moved most of Program functionality to DataGrabber
            // so that we can create this as a DLL more easily.

            /*try
            {
                string locationsRequest = CreateRequest("New%20York");
                Response locationsResponse = MakeRequest(locationsRequest);
                ProcessResponse(locationsResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }*/
        }
    }
}