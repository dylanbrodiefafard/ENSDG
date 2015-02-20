using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace ENSDG
{
    [DataContract]
    class Data
    {
        public Data(int size, int xorigin, int yorigin, int zorigin)
        {
            this.filter = new Filter(size, xorigin, yorigin, zorigin);
        }

        [DataMember(Name = "ver")]
        public int ver = 2;
        [DataMember]
        public bool test = true;
        [DataMember]
        public int outputmode = 2;
        [DataMember]
        public Filter filter;

        [DataContract]
        public class Filter
        {
            public Filter(int size, int xorigin, int yorigin, int zorigin)
            {
                this.coordcube = ccToArray(size, xorigin, yorigin, zorigin);
                this.date = "2014-09-18 12:34:56";
            }

            [DataMember]
            public int knownstatus = 0;
            [DataMember]
            public int cr = 2;
            [DataMember]
            public int[][] coordcube;
            [DataMember]
            public string date;

            private int[][] ccToArray(int size, int xorigin, int yorigin, int zorigin)
            {
                int[][] cc = new int[3][];
                cc[0] = new int[2];
                cc[1] = new int[2];
                cc[2] = new int[2];

                cc[0][0] = -size/2 + xorigin;
                cc[0][1] = size/2 + xorigin;
                cc[1][ 0] = -size / 2 + yorigin;
                cc[1][1] = size / 2 + yorigin;
                cc[2][ 0] = -size / 2 + zorigin;
                cc[2][ 1] = size / 2 + zorigin;
                return cc;
            }
        }
    }
}
