using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UKJiemeitao.ProductUploader.MVC.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string ISBN { get; set; }

    }
}