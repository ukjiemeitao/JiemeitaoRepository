using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKjiemeitao.Domain.Model;
using UKjiemeitao.Domain.Repositories.EntityFramework;

namespace UKjiemeitao.Domain.Repositories
{


    public class SSProductColorImageMappinRepository : EntityFrameworkRepository<SS_Product_Color_Image_Mapping>, ISSProductColorImageMappinRepository
    {
        public SSProductColorImageMappinRepository(IRepositoryContext context)
            : base(context)
        { }

     
    }
}
