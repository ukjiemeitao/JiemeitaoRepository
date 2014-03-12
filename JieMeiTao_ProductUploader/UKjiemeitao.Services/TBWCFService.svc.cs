using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UKjiemeitao.Application.Interface;
using UKjiemeitao.DataObjects;
using UKjiemeitao.Infrastructure;

namespace UKjiemeitao.Services
{
    public class TBWCFService : ITBProductUploadService
    {
        private readonly ITBProductUploadService productServiceImpl = ServiceLocator.Instance.GetService<ITBProductUploadService>();

        /// <summary>
        /// 初始化淘宝分类表数据
        /// </summary>
        public void InitializationItemCat()
        {
            try
            {
                productServiceImpl.InitializationItemCat();
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }

        /// <summary>
        /// 初始化淘宝属性表数据
        /// </summary>
        public void InitializationItemProp()
        {
            try
            {
                productServiceImpl.InitializationItemProp();
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }

        /// <summary>
        /// 初始化淘宝属性值表数据
        /// </summary>
        public void InitializationPropValue()
        {
            try
            {
                productServiceImpl.InitializationPropValue();
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }

        public void ConvertSSProductToTaoBao()
        {
            try
            {
                productServiceImpl.ConvertSSProductToTaoBao();
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }

        public void UploadProduct()
        {
            try
            {
                productServiceImpl.UploadProduct();
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
    }
}
