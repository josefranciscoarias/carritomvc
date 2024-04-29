using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace capapresentaciontienda.Controllers
{
    public class tiendaController : Controller
    {
        // GET: tienda
        public ActionResult Index()
        {
            return View();
        }
    }
}