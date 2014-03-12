using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductUploader.Services
{
    public static  class Utils
    {
        /// <summary>
        /// 下载图片保存到本地
        /// </summary>
        /// <param name="imgurl"></param>
        /// <param name="filename"></param>
        public static void SaveImage(string imgurl, out string filename)
        {
            WebClient mywebclient = new WebClient();
            string url = imgurl;
            filename = System.Guid.NewGuid().ToString() + ".jpg";
            string filepath =  ConfigurationManager.AppSettings["ImageFolderPath"] + filename;
            try
            {
                mywebclient.DownloadFile(url, filepath);
            }
            catch
            {
                throw;
            }
        }

        public static string GetPostBackControlId(this Page page)
        {
            if (!page.IsPostBack)
                return string.Empty;

            Control control = null;
            // first we will check the "__EVENTTARGET" because if post back made by the controls
            // which used "_doPostBack" function also available in Request.Form collection.
            string controlName = page.Request.Params["__EVENTTARGET"];
            if (!String.IsNullOrEmpty(controlName))
            {
                control = page.FindControl(controlName);
            }
            else
            {
                // if __EVENTTARGET is null, the control is a button type and we need to
                // iterate over the form collection to find it

                // ReSharper disable TooWideLocalVariableScope
                string controlId;
                Control foundControl;
                // ReSharper restore TooWideLocalVariableScope

                foreach (string ctl in page.Request.Form)
                {
                    // handle ImageButton they having an additional "quasi-property" 
                    // in their Id which identifies mouse x and y coordinates
                    if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                    {
                        controlId = ctl.Substring(0, ctl.Length - 2);
                        foundControl = page.FindControl(controlId);
                    }
                    else
                    {
                        foundControl = page.FindControl(ctl);
                    }

                    if (!(foundControl is Button || foundControl is ImageButton)) continue;

                    control = foundControl;
                    break;
                }
            }

            return control == null ? String.Empty : control.ID;
        }

        public static decimal CalculateSellPrice(decimal? ssPrice,double ratio,decimal exchangeRate)
        {
         
            decimal sellPrice = 0;

            double counter = 1d;

            if (ssPrice != null)
            {
                for (decimal i = 0; i < 1500m; i += 100m)
                {
                    if (i < ssPrice && ssPrice <= i + 100m)
                    {
                        sellPrice = ssPrice.Value + (decimal)(Math.Pow(ratio, counter) * 50);
                        break;
                    }

                    counter++;
                }
            }
            else
                throw new ArgumentNullException("ShopStyle商品价格不能为空");
           

            return sellPrice * exchangeRate;
        }
    }
}