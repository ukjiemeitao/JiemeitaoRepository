using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UKjiemeitao.Application.DataObjects;
using UKjiemeitao.Application.Implementation;
using UKjiemeitao.DataObjects;
using UKjiemeitao.Domain.Model;

namespace UKjiemeitao.Application.Interface
{
    [ServiceContract(Namespace = "http://UKjiemeitao.taobao.com")]
    public interface ITBProductUploadService
    {
        /// <summary>
        /// 初始化淘宝分类数据
        /// </summary>
        [OperationContract]
        void InitializationItemCat();

        /// <summary>
        /// 初始化 属性表 TB_ItemProp
        /// </summary>
        [OperationContract]
        void InitializationItemProp();

        /// <summary>
        /// 初始化 属性值表 TB_PropValue
        /// </summary>
        [OperationContract]
        void InitializationPropValue();

        [OperationContract]
        void ConvertSSProductToTaoBao();

        [OperationContract]
        void UploadProduct();

    }
}
