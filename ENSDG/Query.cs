using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ENSDG
{
    [DataContract]
    class Query
    {
        [DataMember]
        public Data data;

        public Query(int size, int xorigin, int yorigin, int zorigin)
        {
            this.data = new Data(size, xorigin, yorigin, zorigin);
        }
    }
}
