using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKjiemeitao.Domain.Model;
using UKjiemeitao.Domain.Repositories.EntityFramework;

namespace UKjiemeitao.Domain.Repositories
{


    public class SSTBBrandMappingRepository : EntityFrameworkRepository<SS_TB_Brand_Mapping>, ISSTBBrandMappingRepository
    {
        public SSTBBrandMappingRepository(IRepositoryContext context)
            : base(context)
        { }

     
    }
}
