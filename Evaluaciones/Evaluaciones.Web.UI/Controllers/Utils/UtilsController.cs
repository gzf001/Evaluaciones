using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Web.UI.Controllers.Utils
{
    public class UtilsController : Controller
    {
        [Authorize]
        [HttpGet]
        public JsonResult GenerateId()
        {
            return this.Json(Guid.NewGuid(), JsonRequestBehavior.AllowGet);
        }
    }
}