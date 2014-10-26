﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HallOfBeorn.Controllers
{
    public class ImageController : Controller
    {
        public ActionResult GetCardImage(string setName, string imageName)
        {
            if (string.IsNullOrEmpty(setName) || string.IsNullOrEmpty(imageName))
            {
                return HttpNotFound("Card Image Not Found");
            }

            return Redirect(string.Format("https://s3.amazonaws.com/hallofbeorn-resources/Images/Cards/{0}/{1}", setName, imageName));
        }


        public ActionResult GetProductImage(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
            {
                return HttpNotFound("Product Image Not Found");
            }

            return Redirect(string.Format("https://s3.amazonaws.com/hallofbeorn-resources/Images/Products/{0}", imageName));
        }
    }
}
