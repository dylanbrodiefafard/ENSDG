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
        private string URL = @"http://edstarcoordinator.com/api.asmx/GetSystems";

        public DataGrabber()
        {
            // possibly remove this from constructor... 
            ProcessResponse(MakeRequest(20,0,0,0));
        }

        private HttpWebRequest GenerateWebRequest(Query queryParams)
        {
            HttpWebRequest request = WebRequest.Create(URL) as HttpWebRequest;
            

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Query));

            MemoryStream stream1 = new MemoryStream();
            ser.WriteObject(stream1, queryParams);
            stream1.Position = 0;
            if (request != null)
            {
                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";
                request.ContentLength = stream1.Length;
                Stream rs = request.GetRequestStream();
                ser.WriteObject(rs, queryParams);
                rs.Flush();
                rs.Close();
            }
            return request;
        }

        private Response GenerateJsonResponse(HttpWebResponse response)
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Response));
            object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
            Response jsonResponse
            = objResponse as Response;
            return jsonResponse;
        }

        private Response MakeRequest(int size, int xOrigin, int yOrigin, int zOrigin)
        {
            try
            {
                Query queryParams = new Query(size, xOrigin, yOrigin, zOrigin);
                HttpWebRequest request = GenerateWebRequest(queryParams);

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(String.Format(
                            "Server error (HTTP {0}: {1}).",response.StatusCode,response.StatusDescription));
                    }

                    return GenerateJsonResponse(response);

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
