using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;
using capaentidad;
using capanegocio;
using ClosedXML.Excel;

namespace capapresentacionadmin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Usuario()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Ls()
        {
            List<usuario> ff = new List<usuario>();
            ff = new cn_usuario().Listar();
            return Json(new { data = ff }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult guardarusuario(usuario objeto)
        {
            Object resultado;
            string mensaje = string.Empty;


            if (objeto.idusuario == 0)
            {

                resultado = new cn_usuario().registrar(objeto, out mensaje);
            }
            else
            {
               resultado = new cn_usuario().editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult eliminarusuario(int id)
        {
            bool respuesta = true;
            string mensaje = string.Empty;

            respuesta = new cn_usuario().eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listareporte(string fechainicio, string fechafin, string idtransaccion)
        {
            List<reporte> olista = new List<reporte>();
            olista = new cn_reporte().venta(fechainicio, fechafin, idtransaccion);

            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult vistadashboard()
        {
            dashboard objeto = new cn_reporte().verdashboard();
            return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public FileResult exportarventa(string fechainicio, string fechafin, string idtransaccion)
        {
            List<reporte> olista = new List<reporte>();
            olista = new cn_reporte().venta(fechainicio, fechafin, idtransaccion);

            DataTable dt = new DataTable();

            dt.Locale = new System.Globalization.CultureInfo("es-PE");
            dt.Columns.Add("fechaventa", typeof(string));
            dt.Columns.Add("cliente", typeof(string));
            dt.Columns.Add("producto", typeof(string));
            dt.Columns.Add("precio", typeof(string));
            dt.Columns.Add("cantidad", typeof(string));
            dt.Columns.Add("total", typeof(string));
            dt.Columns.Add("idtransaccion", typeof(string));

            foreach (reporte rp in olista)
            {
                dt.Rows.Add(new object[]
                {
                    rp.fechaventa,
                    rp.cliente,
                    rp.producto,
                    rp.precio,
                    rp.cantidad,
                    rp.total,
                    rp.idtransaccion
                });
            }
            dt.TableName = "Datos";
            using (XLWorkbook wb = new XLWorkbook()) {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "reporteventa" + DateTime.Now.ToString() + ".xlsx");
                }
            } }
    }
}
