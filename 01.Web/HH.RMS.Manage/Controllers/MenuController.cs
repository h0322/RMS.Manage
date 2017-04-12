using HH.RMS.Manage.App_Start;
using HH.RMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HH.RMS.Manage.Controllers
{
    [RMSAuthorize]
    public class MenuController : ControllerService
    {
        //
        // GET: /Menu/

        public ActionResult Index()
        {
            return View();
        }

    }
}
