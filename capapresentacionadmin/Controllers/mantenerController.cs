using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using capaentidad;
using capanegocio;
using Newtonsoft.Json;

namespace capapresentacionadmin.Controllers
{
    [Authorize]
    public class mantenerController : Controller
    {
        private NumberStyles numbersty;

        // GET: mantener
        public ActionResult Categoria()
        {
            return View();
        }
        public ActionResult Marca()
        {
            return View();
        }
        public ActionResult Producto()
        {
            return View();
        }
        // categoria
        #region 
        [HttpGet]
        public JsonResult Ls()
        {
            List<categoria> ff = new List<categoria>();
            ff = new cn_categoria().Listar();
            return Json(new { data = ff }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult guardarcategoria(categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;


            if (objeto.idcategoria == 0)
            {

                resultado = new cn_categoria().registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new cn_categoria().editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult eliminarcategoria(int id)
        {
            bool respuesta = true;
            string mensaje = string.Empty;

            respuesta = new cn_categoria().eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        //para la marca
        #region
        [HttpGet]
        public JsonResult Lsa()
        {
            List<marca> ff = new List<marca>();
            ff = new cn_marca().Listar();
            return Json(new { data = ff }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult guardarmarca(marca objeto)
        {
            object resultado;
            string mensaje = string.Empty;


            if (objeto.idmarca == 0)
            {

                resultado = new cn_marca().registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new cn_marca().editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult eliminarmarca(int id)
        {
            bool respuesta = true;
            string mensaje = string.Empty;

            respuesta = new cn_marca().eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        // para producto
        #region
        [HttpGet]
        public JsonResult Lsas()
        {
            List<producto> ffa = new List<producto>();
            ffa = new cn_producto().Listar();
            return Json(new { data = ffa }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult guardarproducto(string objeto, HttpPostedFileBase archivoimagen)
        {
            
            string mensaje = string.Empty;
            bool operacio_exitosa = true;
            bool guardar_imagen_exitosa = true;
            producto oproducto = new producto();
            oproducto = JsonConvert.DeserializeObject<producto>(objeto);

            decimal precio;

            if (decimal.TryParse(oproducto.preciotexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-PE"), out precio))
            {
                oproducto.precio = precio;
            }
            else
            {
                return Json(new { operacionexitosa = false, mensaje = "el formato de precio debe ser ##. ##" }, JsonRequestBehavior.AllowGet);
            }


            if (oproducto.idproducto == 0)
            {

                int idproductogenerado = new cn_producto().registrar(oproducto, out mensaje);

                if (idproductogenerado != 0)
                {
                    oproducto.idproducto = idproductogenerado;
                }
                else { operacio_exitosa = false; }
            }
            else
            {
                operacio_exitosa = new cn_producto().editar(oproducto, out mensaje);
            }
            if (operacio_exitosa)
            {
                if (archivoimagen != null)
                {
                    string ruta_guardar = ConfigurationManager.AppSettings["servidorfoto"]; 
                    string extesion = Path.GetExtension(archivoimagen.FileName);
                    string nombre_imagen = string.Concat(oproducto.idproducto.ToString(), extesion);

                    try
                    {
                        archivoimagen.SaveAs(Path.Combine(ruta_guardar, nombre_imagen));
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        guardar_imagen_exitosa = false;
                    }
                    if (guardar_imagen_exitosa)
                    {
                        oproducto.rutaimagen = ruta_guardar;
                        oproducto.nombreimagen = nombre_imagen;
                        bool rspta = new cn_producto().guardardatosimagen(oproducto, out mensaje);
                    }
                    else
                    {
                        mensaje = "se guardo el producto pero hubo problema con la imagen";
                    }
                }

               
            }
            return Json(new { operacionexitosa = operacio_exitosa, idgenerado = oproducto.idproducto, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult imagenproducto(int id, string jsonresquest)
        {
            bool conversion;
            producto oproducto = new cn_producto().Listar().Where(p => p.idproducto == id).FirstOrDefault();
            string textobase64 = cn_recursos.convertirbase64(Path.Combine(oproducto.rutaimagen, oproducto.nombreimagen), out conversion);
            return Json(new
            {
                conversion = conversion,
                textobase64 = textobase64,
                extension = Path.GetExtension(oproducto.nombreimagen)
            },
           JsonRequestBehavior.AllowGet
           );
        }







        [HttpPost]
        public JsonResult eliminarproducto(int id)
        {
            bool respuesta = true;
            string mensaje = string.Empty;

            respuesta = new cn_producto().eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion
       

        
        }
}
    
