using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKjiemeitao.Domain.Model;
using UKjiemeitao.Domain.Specifications;

namespace UKjiemeitao.Domain.Repositories.Specifications
{
    internal class TBItemCatCIDSpecification : Specification<TB_ItemCat>
    {
        private readonly long cid;

        public TBItemCatCIDSpecification(long cid)
        {
            this.cid = cid;
        }

        public override System.Linq.Expressions.Expression<Func<TB_ItemCat, bool>> GetExpression()
        {
            return p => p.Cid == this.cid;
        }
    }
}
