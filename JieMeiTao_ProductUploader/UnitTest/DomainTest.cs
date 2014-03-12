using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UKjiemeitao.Domain.Model;
using UKjiemeitao.Domain.Repositories;
using UKjiemeitao.Domain.Repositories.EntityFramework;
using UKjiemeitao.Domain.Specifications;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class DomainTest
    {


        [TestMethod]
        public void SSProductTest()
        {
            IRepositoryContext context = new EntityFrameworkRepositoryContext();

            ISSProductRepository ssp = new SSProductRepository(context);

            var p = ssp.Find(Specification<SS_Product>.Eval(o => o.Product_ID == 432527971));
            p.CategoryCollection.Where(o => o.Cat_ID == "");
            
            int c = p.CategoryCollection.Count;
            int s = p.SizeCollection.Count;
            int pc = p.ColorCollection.Count;
            //Assert.AreEqual(a, 3);
        }

        [TestMethod]
        public void SSCategoryTest()
        {
            IRepositoryContext context = new EntityFrameworkRepositoryContext();

            ISSCategoryRepository ss = new SSCategoryRepository(context);
            var b = ss.Find(Specification<SS_Category>.Eval(o => o.Cat_ID == "coats"));
          
            int a = b.ProductCollection.Count;
        }

        [TestMethod]
        public void SSRetailerTest()
        {
            IRepositoryContext context = new EntityFrameworkRepositoryContext();

            ISSRetailerRepository ssp = new SSRetailerRepository(context);
            var p = ssp.Find(Specification<SS_Retailers>.Eval(o => o.Name == "Burberry"));
            int c = p.ProductCollection.Count;
            Assert.AreEqual(c, 157);
        }

        [TestMethod]
        public void SSSizeTest()
        {
            IRepositoryContext context = new EntityFrameworkRepositoryContext();

            ISSSizeRepository ssp = new SSSizeRepository(context);
            var p = ssp.Find(Specification<SS_Size>.Eval(o => o.ID == Guid.Parse("CC6496AC-E361-4BA6-8F97-0E263E402541")));
            int c = p.ProductCollection.Count;
           // Assert.AreEqual(c, 157);
        }

         [TestMethod]
        public void SSImageTest()
        {
            IRepositoryContext context = new EntityFrameworkRepositoryContext();

            ISSImageRepository ssp = new SSImageRepository(context);
            var p = ssp.Find(Specification<SS_Images>.Eval(o => o.Image_ID == "08cf2944f322e3a25f81ad7318e0a8d9"));
           
        }

        [TestMethod]
         public void SSTBBrandMappingTest()
         {
             IRepositoryContext context = new EntityFrameworkRepositoryContext();
             ISSTBBrandMappingRepository ssp = new SSTBBrandMappingRepository(context);
             var list= ssp.FindAll();
             
         }

    }
}
