using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using ENSCC;
using System.Xml.Serialization;

namespace ENSDG
{
    class DataGrabber
    {
        public string URL = @"http://edstarcoordinator.com/api.asmx/GetSystems";

        public DataGrabber()
        {
            // possibly remove this from constructor... 
            ProcessResponse(MakeRequest(URL));
        }

        private Response MakeRequest(string requestUrl)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                Query queryParams = new Query(20, 0, 0, 0);

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Query));

                MemoryStream stream1 = new MemoryStream();
                ser.WriteObject(stream1, queryParams);
                stream1.Position = 0;

                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";
                request.ContentLength = stream1.Length;
                //request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                // add post data to request
                Stream rs = request.GetRequestStream();
                ser.WriteObject(rs, queryParams);
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

        private void ProcessResponse(Response response)
        {
            XmlSerializer x = new XmlSerializer(response.GetType());
            StreamWriter file = new StreamWriter(DateTime.Now.ToString("yyyyMMdd") + ".xml");
            x.Serialize(file, response);
        }

    }
}
