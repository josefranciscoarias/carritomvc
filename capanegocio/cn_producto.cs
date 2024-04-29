using capadato;
using capaentidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capanegocio
{
   public class cn_producto
    {
        private cd_producto ojbcapadato = new cd_producto();

        public List<producto> Listar()
        {
            return ojbcapadato.Listar();
        }
        public int registrar(producto obj, out string mensaje)

        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = " el nombre no puede estar vacio";
            }

           else if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                mensaje = " el descripcion no puede estar vacio";
            }

           else if (obj.omarca.idmarca == 0)
            {
                mensaje = "debe selecionar una marca"; 
            }
            else if(obj.ocategoria.idcategoria == 0)
            {
                mensaje = "Debe selecionar una categoria";
            }
           else if(obj.precio == 0)
            {
                mensaje = " debe selecionar un precio";
            }
            else if(obj.stock == 0)
            { mensaje = "debe selecionar un stock"; }
            if (string.IsNullOrEmpty(mensaje))
            {

                return ojbcapadato.registrar(obj, out mensaje);

            }
            else { return 0; }
        }

        public bool editar(producto obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = " el nombre no puede estar vacio";
            }

            else if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                mensaje = " el descripcion no puede estar vacio";
            }

            else if (obj.omarca.idmarca == 0)
            {
                mensaje = "debe selecionar una marca";
            }
            else if (obj.ocategoria.idcategoria == 0)
            {
                mensaje = "Debe selecionar una categoria";
            }
            else if (obj.precio == 0)
            {
                mensaje = " debe selecionar un precio";
            }
            else if (obj.stock == 0)
            { mensaje = "debe selecionar un stock"; }


            if (string.IsNullOrEmpty(mensaje))
            {
                return ojbcapadato.editar(obj, out mensaje);
            }
            else
            {
                return false;
            }

        }
        public bool guardardatosimagen(producto oproducto, out string mensaje)
        {
            return ojbcapadato.guardardatosimagen(oproducto, out mensaje);
        }
        public bool eliminar(int id, out string mensaje)
        {
            return ojbcapadato.eliminar(id, out mensaje);
        }
    }
}
