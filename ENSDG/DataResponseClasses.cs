using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ENSDG
{
    [DataContract]
    public class Response
    {
        [DataMember(Name = "d")]
        public ResponseData response;

        public override string ToString()
        {
            return response.ToString();
        }
    }

    [DataContract]
    public class ResponseData
    {
        [DataMember(Name = "ver")]
        public int Version { get; set; }
        [DataMember(Name = "date")]
        public string Date { get; set; }
        [DataMember(Name = "status")]
        public Statuses StatusCode { get; set; }
        [DataMember(Name = "systems")]
        public System[] Systems { get; set; }
        public override string ToString()
        {
            StringBuilder sr = new StringBuilder();
            sr.Append(Version);
            sr.Append(Date);
            foreach(System s in Systems)
            {
                sr.Append(Environment.NewLine);
                sr.Append(s);
            }
            return sr.ToString();
        }
    }

    [DataContract]
    public class Statuses
    {
        [DataMember(Name = "input")]
        public Input[] Inputs { get; set; }
    }

    [DataContract]
    public class Input
    {
        [DataMember(Name = "status")]
        public Status Status { get; set; }
    }

    [DataContract]
    public class Status
    {
        [DataMember(Name = "statusnum")]
        public int StatusCode { get; set; }
        [DataMember(Name = "msg")]
        public string Message { get; set; }
    }

    [DataContract]
    public class System
    {
        [DataMember(Name = "id")]
        public int ID { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "coord")]
        public double[] Coord { get; set; }
        [DataMember(Name = "cr")]
        public int ConfidenceRating { get; set; }
        [DataMember(Name = "commandercreate")]
        public string CommanderCreate { get; set; }
        [DataMember(Name = "createdate")]
        public string CreateDate { get; set; }
        [DataMember(Name = "commanderupdate")]
        public string CommanderUpdate { get; set; }
        [DataMember(Name = "updatedate")]
        public string UpdateDate { get; set; }

        public override string ToString()
        {
            StringBuilder sr = new StringBuilder();
            sr.Append(ID);
            sr.Append(",");
            sr.Append(Name);
            sr.Append(",[");
            sr.Append(Coord[0]);
            sr.Append(",");
            sr.Append(Coord[1]);
            sr.Append(",");
            sr.Append(Coord[2]);
            sr.Append("],");
            sr.Append(ConfidenceRating);
            sr.Append(Environment.NewLine);
            return sr.ToString();
        }
    }

}
