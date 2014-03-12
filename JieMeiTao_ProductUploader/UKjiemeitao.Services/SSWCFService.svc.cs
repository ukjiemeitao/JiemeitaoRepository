using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UKjiemeitao.Application.DataObjects;
using UKjiemeitao.Application.Interface;
using UKjiemeitao.DataObjects;
using UKjiemeitao.Infrastructure;

namespace UKjiemeitao.Services
{
    public class SSWCFService : IShopStyleService
    {
        private readonly IShopStyleService ssServiceImpl = ServiceLocator.Instance.GetService<IShopStyleService>();


        public SSProductDataObjectList GetProduct(DateTime startDate, DateTime endDate)
        {
            try
            {
                return ssServiceImpl.GetProduct(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }


        public bool TranslateProduct(SSProductDataObject product)
        {
            try
            {
                return ssServiceImpl.TranslateProduct(product);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }


        public SSProductDataObject GetProductByID(string id)
        {
            try
            {
                return ssServiceImpl.GetProductByID(id);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }

        public void DownloadBrands()
        {
            throw new NotImplementedException();
        }

        public void DownloadColors()
        {
            throw new NotImplementedException();
        }

        public void DownloadRetailers()
        {
            try
            {
                ssServiceImpl.DownloadRetailers();
            }
            catch (Exception ex)
            {
                
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }

        public void DownloadSizes(string catParentID)
        {
            throw new NotImplementedException();
        }
    }
}
