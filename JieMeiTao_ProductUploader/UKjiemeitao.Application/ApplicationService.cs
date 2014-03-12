using System.Linq;
using AutoMapper;
using UKjiemeitao.Application.DataObjects;
using UKjiemeitao.Domain.Events;
using UKjiemeitao.Domain.Model;
using UKjiemeitao.Domain.Repositories;
using UKjiemeitao.Infrastructure;

namespace UKjiemeitao.Application
{

    /// <summary>
    /// 表示应用层服务的抽象类。
    /// </summary>
    public abstract class ApplicationService : DisposableObject
    {
        #region Private Fields
        private readonly IRepositoryContext context;
        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个<c>ApplicationService</c>类型的实例。
        /// </summary>
        /// <param name="context">用来初始化<c>ApplicationService</c>类型的仓储上下文实例。</param>
        public ApplicationService(IRepositoryContext context)
        {
            this.context = context;
        }
        #endregion

        #region Protected Properties
        /// <summary>
        /// 获取当前应用层服务所使用的仓储上下文实例。
        /// </summary>
        protected IRepositoryContext Context
        {
            get { return this.context; }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// 派发给定的领域事件。
        /// </summary>
        /// <typeparam name="TEvent">领域事件类型。</typeparam>
        /// <param name="evnt">领域事件实例。</param>
        protected virtual void DispatchDomainEvent<TEvent>(TEvent evnt)
            where TEvent : IDomainEvent
        {
            // TODO: 在此添加领域事件派发逻辑。此部分功能将在下个版本的案例中完善。
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// 对应用层服务进行初始化。
        /// </summary>
        /// <remarks>包含的初始化任务有：
        /// 1. AutoMapper框架的初始化</remarks>
        public static void Initialize()
        {
            //taobao
            Mapper.CreateMap<TBItemCatDataObject, TB_ItemCat>();
            Mapper.CreateMap<TB_ItemCat, TBItemCatDataObject>();
            Mapper.CreateMap<TBItemPropDataObject, TB_ItemProp>();
            Mapper.CreateMap<TB_ItemProp, TBItemPropDataObject>();
            Mapper.CreateMap<TBProductDataObject, TB_Product>();
            Mapper.CreateMap<TB_Product, TBProductDataObject>();
            Mapper.CreateMap<TBPropValueDataObject, TB_PropValue>();
            Mapper.CreateMap<TB_PropValue, TBPropValueDataObject>();

            //shopstyle
            Mapper.CreateMap<SS_Brand, SSBrandDataObject>();
            Mapper.CreateMap<SSBrandDataObject, SS_Brand>();
            Mapper.CreateMap<SS_Brand_Synonyms, SSBrandSynonymsDataObject>();
            Mapper.CreateMap<SSBrandSynonymsDataObject, SS_Brand_Synonyms>();
            Mapper.CreateMap<SS_Category, SSCategoryDataObject>();
            Mapper.CreateMap<SSCategoryDataObject, SS_Category>();
            Mapper.CreateMap<SS_Images, SSImageDataObject>();
            Mapper.CreateMap<SSImageDataObject, SS_Images>();
            Mapper.CreateMap<SS_Product, SSProductDataObject>();
            Mapper.CreateMap<SSProductDataObject, SS_Product>();
            Mapper.CreateMap<SS_Product_Color_Image_Mapping, SSProductColorImageMappingtDataObject>();
            Mapper.CreateMap<SSProductColorImageMappingtDataObject, SS_Product_Color_Image_Mapping>();
            Mapper.CreateMap<SS_Retailers, SSRetailerDataObject>();
            Mapper.CreateMap<SSRetailerDataObject, SS_Retailers>();
            Mapper.CreateMap<SS_Size, SSSizeDataObject>();
            Mapper.CreateMap<SSSizeDataObject, SS_Size>();    

        }
        #endregion
    }
}
