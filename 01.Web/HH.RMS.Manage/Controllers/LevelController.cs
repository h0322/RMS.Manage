using HH.RMS.Common.Constant;
using HH.RMS.IService;
using HH.RMS.IService.Model;
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
    public class LevelController : ControllerService
    {
        private ILevelService _levelService { get; set; }
        public LevelController(ILevelService levelService)
        {
            this._levelService = levelService;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult QueryLevelToGrid(PagerModel pagerModel)
        {
            pagerModel.searchText = searchText;
            pagerModel.searchType = searchType;
            pagerModel.searchDateFrom = searchDateFrom;
            pagerModel.searchDateTo = searchDateTo;
            pagerModel.searchStatus = searchStatus;
            var model = _levelService.QueryLevelToGrid(pagerModel);
            return Json(model, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult CreateLevel(LevelModel model)
        {
            ResultType result = _levelService.CreateLevel(model);
            return Json(result);
        }
        [HttpPost]
        public JsonResult UpdateLevel(LevelModel model)
        {
            ResultType result = _levelService.UpdateLevel(model);
            return Json(result);
        }
        [HttpPost]
        public JsonResult QueryLevelById(long id)
        {
            var person = _levelService.QueryLevelById(id);
            return Json(person, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteLevelByIds(string idString)
        {
            long[] ids = Array.ConvertAll<string, long>(idString.Split(','), s => int.Parse(s));
            var result = _levelService.DeleteLevelByIds(ids);
            return Json(result);
        }
    }
}
