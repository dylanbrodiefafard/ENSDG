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

namespace ENSDG
{
    class Program
    {
        static void Main(string[] args)
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

        public static string CreateRequest(string queryString)
        {
            string UrlRequest = "http://edstarcoordinator.com/api.asmx/GetSystems";
            return (UrlRequest);
        }

        public static Response MakeRequest(string requestUrl)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                Query test = new Query(20, 0, 0, 0);

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Query));
                MemoryStream stream1 = new MemoryStream();
                ser.WriteObject(stream1, test);
                
                stream1.Position = 0;
                
                StreamReader sr = new StreamReader(stream1);
                Console.Write("JSON form of Query object: ");
                Console.WriteLine(sr.ReadToEnd());
                stream1.Position = 0;
                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";
                request.ContentLength = stream1.Length;
                //request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                
                // add post data to request
                Stream rs = request.GetRequestStream();
                ser.WriteObject(rs, test);
                rs.Flush();
                rs.Close();
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format(
                        "Server error (HTTP {0}: {1}).",
                        response.StatusCode,
                        response.StatusDescription));

                    //StreamReader st = new StreamReader(response.GetResponseStream());
                    //Console.Write("JSON form of Response object: ");
                    //Console.WriteLine(st.ReadToEnd());
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Response));
                    object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                    Response jsonResponse
                    = objResponse as Response;
                    return jsonResponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        static public void ProcessResponse(Response response)
        {
            XmlSerializer x = new XmlSerializer(response.GetType());
            StreamWriter file = new StreamWriter(DateTime.Now.ToString("yyyyMMdd") + ".xml");
            x.Serialize(file, response);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
