using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKjiemeitao.Domain.Model
{
    public partial class Mapping_CategoriesMap : AggregateRoot
    {
        public Nullable<long> tb_cid { get; set; }
        public string ss_cid_array { get; set; }
        public string keywords { get; set; }
        public string keywords_category { get; set; }
    }
}
