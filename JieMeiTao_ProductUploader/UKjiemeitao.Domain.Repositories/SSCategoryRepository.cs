using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKjiemeitao.Domain.Model;
using UKjiemeitao.Domain.Repositories.EntityFramework;

namespace UKjiemeitao.Domain.Repositories
{


    public class SSCategoryRepository : EntityFrameworkRepository<SS_Category>, ISSCategoryRepository
    {
        public SSCategoryRepository(IRepositoryContext context)
            : base(context)
        { }

     
    }
}
