using capaentidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using capanegocio;
using System.Web.Security;

namespace capapresentaciontienda.Controllers
{
    public class accesotController : Controller
    {
        // GET: accesot
        public ActionResult Index()
        {
            return View();
        }
      
        public ActionResult registrar()
        {
            return View();
        }
        public ActionResult reestablecer()
        {
            return View();
        }
        public ActionResult cambiodeclave()
        {
            return View();
        }
        [HttpPost]
        public ActionResult registrar(cliente objeto)
        {
            int resultado;
            string mensaje = string.Empty;
            ViewData["nombre"] = string.IsNullOrEmpty(objeto.nombre) ? "" : objeto.nombre;
            ViewData["apellido"] = string.IsNullOrEmpty(objeto.apellido) ? "" : objeto.apellido;
            ViewData["correo"] = string.IsNullOrEmpty(objeto.correo) ? "" : objeto.correo;
            if(objeto.clave != objeto.confirmarclave)
            {
                ViewBag.Error = "las contraseña no coicide";
                return View();
            }
            resultado = new cn_cliente().registrar(objeto, out mensaje);

            if(resultado > 0)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "accesot");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }


          
        }
        [HttpPost]
       public ActionResult Index( string correo, string clave)
        {
            cliente ocliente = null;
            ocliente = new cn_cliente().Listar().Where(item => item.correo == correo && item.clave == cn_recursos.ConvetirSha256(clave)).FirstOrDefault();
            if (ocliente == null)
            {
                ViewBag.Error = "correo o contraseñas no son correcta";
                return View();
            }
            else { 
            if(ocliente.restablecer)
                {
                    TempData["idcliente"] = ocliente.idcliente;
                    return RedirectToAction("cambiodeclave", "accesot");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(ocliente.correo, false);
                    Session["cliente"] = ocliente;
                    ViewBag.Error = null;
                    return RedirectToAction("Index", "tienda");
                    
                }
                    
                        }
        }
        [HttpPost]
        public ActionResult reestablecer(string correo)
        {
            cliente ocliente = new cliente();
            ocliente = new cn_cliente().Listar().Where(item => item.correo == correo).FirstOrDefault();
            if (ocliente == null)
            {
                ViewBag.Error = "no se encontro un cliente relacionado a ese correo";
                return View();
            }
            string mensaje = string.Empty;
            bool respuesta = new cn_cliente().reestablecerclave(ocliente.idcliente, correo, out mensaje);
            if (respuesta)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index","accesot");

            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }

        }
        [HttpPost]
        public ActionResult cambiodeclave(string idcliente, string claveactual, string nuevaclave, string confimarclave)
        {
            cliente ocliente = new cliente();
            ocliente = new cn_cliente().Listar().Where(c => c.idcliente == int.Parse(idcliente)).FirstOrDefault();
            if (ocliente.clave != cn_recursos.ConvetirSha256(claveactual))
            {
                TempData["idcliente"] = idcliente;
                ViewData["vclave"] = "";
                ViewBag.Error = "la contraseña actual no es correcta";
                return View();
            }
            else if (confimarclave != nuevaclave)
            {

                TempData["idcliente"] = idcliente;
                ViewData["vclave"] = claveactual;
                ViewBag.Error = "la contraseña no coinciden";
                return View();





            }

            ViewData["vclave"] = "";

            nuevaclave = cn_recursos.ConvetirSha256(nuevaclave);
            string mensaje = string.Empty;
            bool respuesta = new cn_cliente().cambiarclave(int.Parse(idcliente), nuevaclave, out mensaje);
            if (respuesta)
            {
                return RedirectToAction("Index");

            }
            else
            {
                TempData["idcliente"] = idcliente;
                ViewBag.Error = mensaje;
                return View();
            }

        }
        public ActionResult cerrarseccion()
        {
            Session["cliente"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "accesot");
        }
    }
    }

//cap 32