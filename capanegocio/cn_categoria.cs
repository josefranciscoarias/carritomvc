using capadato;
using capaentidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capanegocio
{
  public  class cn_categoria
    {
        private cd_categoria ojbcapadato = new cd_categoria();

        public List<categoria> Listar()
        {
            return ojbcapadato.Listar();
        }
        public int registrar(categoria obj, out string mensaje)

        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                mensaje = " el nombre no puede estar vacio";
            }
            
            if (string.IsNullOrEmpty(mensaje))
            {

                return ojbcapadato.registrar(obj , out mensaje);

            }
            else { return 0; }
        }

        public bool editar(categoria obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                mensaje = "no puede estar vacio";
            }
           
            

            if (string.IsNullOrEmpty(mensaje))
            {
                return ojbcapadato.editar(obj, out mensaje);
            }
            else
            {
                return false;
            }

        }
        public bool eliminar(int id, out string mensaje)
        {
            return ojbcapadato.eliminar(id, out mensaje);
        }
    }
}
