using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UKJiemeitao.ProductUploader.MVC.Models;

namespace UKJiemeitao.ProductUploader.MVC.Controllers
{
    public class DemoController : Controller
    {
        //
        // GET: /Demo/

        public ActionResult Index()
        {
            ViewBag.UsageSample =
            @"Please Use the Url 'Demo/Test' or 'Demo/Test/1' to see the Book Details";

            return View();
        }

        public ActionResult Test()
        {
            var book = new Book() { ID = 1, AuthorName = "Yang", BookName = "My life", ISBN = "1233432" };
            return View(book);
        }

    }
}
