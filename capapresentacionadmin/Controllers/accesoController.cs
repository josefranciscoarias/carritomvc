using capaentidad;
using capanegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace capapresentacionadmin.Controllers
{
    public class accesoController : Controller
    {
        private readonly object Cn_recursos;

        // GET: acceso
        // cap27-22.22 del curso para iniciar de esta vista 
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult cambiarclave()
        {
            return View();
        }
        public ActionResult reestablecerclave()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            usuario ousuario = new usuario();

            ousuario = new cn_usuario().Listar().Where(u => u.correo == correo && u.clave == cn_recursos.ConvetirSha256(clave)).FirstOrDefault();

            if (ousuario == null)
            {

                ViewBag.Error = "correo o contraseña incorrecta";
                return View();
            }
            else
            {

                if (ousuario.restablecer)
                {
                    TempData["idusuario"] = ousuario.idusuario;
                    return RedirectToAction("cambiarclave");
                }
                FormsAuthentication.SetAuthCookie(ousuario.correo, false);
                ViewBag.Error = null;
                return RedirectToAction("Index", "Home");


            }

        }
        // me quede por el cap 28-22:55 del curso
        [HttpPost]
        public ActionResult cambiarclave(string idusuario, string claveactual, string nuevaclave, string confimarclave)
        {
            usuario ousuario = new usuario();
            ousuario = new cn_usuario().Listar().Where(u => u.idusuario == int.Parse(idusuario)).FirstOrDefault();
            if (ousuario.clave != cn_recursos.ConvetirSha256(claveactual))
            {
                TempData["idusuario"] = idusuario;
                ViewData["vclave"] = "";
                ViewBag.Error = "la contraseña actual no es correcta";
                return View();
            }
            else if (confimarclave != nuevaclave)
            {

                TempData["idusuario"] = idusuario;
                ViewData["vclave"] = claveactual;
                ViewBag.Error = "la contraseña no coinciden";
                return View();





            }

            ViewData["vclave"] = "";

            nuevaclave = cn_recursos.ConvetirSha256(nuevaclave);
            string mensaje = string.Empty;
            bool respuesta = new cn_usuario().cambiarclave(int.Parse(idusuario), nuevaclave, out mensaje);
            if (respuesta)
            {
                return RedirectToAction("Index");

            }
            else
            {
                TempData["idusuario"] = idusuario;
                ViewBag.Error = mensaje;
                return View();
            }

        }
        [HttpPost]
        public ActionResult reestablecerclave(string correo)
        {
            usuario ousuario = new usuario();
            ousuario = new cn_usuario().Listar().Where(item => item.correo == correo).FirstOrDefault();
            if (ousuario == null)
            {
                ViewBag.Error = "no se encontro un usuario relacionado a ese correo";
                return View();
            }
            string mensaje = string.Empty;
            bool respuesta = new cn_usuario().reestablecerclave(ousuario.idusuario, correo, out mensaje);
            if (respuesta)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "acceso");

            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }

        }

        public ActionResult cerrarseccion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","acceso");
        }

    }
}