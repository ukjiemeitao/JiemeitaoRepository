using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKjiemeitao.Domain.Model
{
    public partial class SS_TB_Brand_Mapping : AggregateRoot
    {
        public long brand_id { get; set; }
        public string brand_name { get; set; }
        public string url { get; set; }
        public long Cid { get; set; }
        public string Name { get; set; }
        public string NameAlias { get; set; }
        public long Pid { get; set; }
        public long Vid { get; set; }
    }
}
