﻿using HH.RMS.Common.Constant;
using HH.RMS.Common.Utilities;
using HH.RMS.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HH.RMS.Service;
using System.Threading;
using HH.RMS.Common.Model;
using HH.RMS.IService;
using HH.RMS.IService.Model;

namespace HH.RMS.Manage.Controllers
{
    public class LoginController : ControllerService
    {
        //
        // GET: /Login/
        private ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public ActionResult Index(string redirectUrl)
        {

            LoginModel model = new LoginModel();
            model.redirectUrl = redirectUrl;
         
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Index(LoginModel model)
        {

            model.password = SecurityHelper.Hash(model.password);
            ResultModel<AccountModel> result = _loginService.UserLogin(model.accountName, model.password);
            if (result.resultType == ResultType.Success)
            {
                if (result.resultObj.status == AccountStatusType.Normal)
                {
                    FormsAuthentication.SetAuthCookie(result.resultObj.accountName, true);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                   (1,
                       result.resultObj.accountName,
                       DateTime.Now,
                       DateTime.Now.AddMinutes(20),
                       true,
                       result.resultObj.roleBitMap.ToString(),
                       "/"
                   );
                    SessionHelper.SetSession(Config.loginSession, result.resultObj);
                    return Json(new { msg = ResultType.Success });
                }
            }
            return Json(new { msg = ResultType.Fail });
        }
        //public JsonResult ForgetPassword(string email)
        //{
            
        //}
        public ActionResult Exit()
        {
            _loginService.ExitLogin();
            return Redirect("/Login/Index");
        }
        [HttpPost]
        public JsonResult ForgetPassword(AccountModel model)
        {
            var result = _loginService.ForgetPassword(model);
            return Json(result);
        }
        public ViewResult ForgetPassword()
        {
            return View();
        }
        public ViewResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ViewResult ResetPassword(AccountModel model)
        {
            return View();
        }
    }
}
