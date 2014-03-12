using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKjiemeitao.Domain.Model;
using UKjiemeitao.Domain.Repositories.EntityFramework;

namespace UKjiemeitao.Domain.Repositories
{


    public class SSRetailerRepository : EntityFrameworkRepository<SS_Retailers>, ISSRetailerRepository
    {
        public SSRetailerRepository(IRepositoryContext context)
            : base(context)
        { }

     
    }
}
