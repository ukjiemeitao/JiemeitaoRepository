using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UKjiemeitao.Application.DataObjects;
using UKjiemeitao.Application.Implementation;
using UKjiemeitao.DataObjects;

namespace UKjiemeitao.Application.Interface
{
    [ServiceContract(Namespace = "http://UKjiemeitao.taobao.com")]
    public interface IShopStyleService
    {
        [OperationContract]
        SSProductDataObjectList GetProduct(DateTime startDate, DateTime endDate);

        [OperationContract]
        bool TranslateProduct(SSProductDataObject product);

        [OperationContract]
        SSProductDataObject GetProductByID(string id);

        [OperationContract]
        void DownloadRetailers();

        [OperationContract]
        void DownloadBrands();

        [OperationContract]
        void DownloadColors();

        [OperationContract]
        void DownloadSizes(string catParentID);

    

        
    }
}
