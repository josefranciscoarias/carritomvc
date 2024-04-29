using capadato;
using capaentidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capanegocio
{
   public class cn_cliente
    {
        private cd_clientes ojbcapadato = new cd_clientes();

        public List<cliente> Listar()
        {
            return ojbcapadato.Listar();
        }

        public int registrar(cliente obj, out string mensaje) {

            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = " el nombre no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.apellido) || string.IsNullOrWhiteSpace(obj.apellido))
            {
                mensaje = " el apellido no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {
                mensaje = "el correo no puede estar vacio";
            }
            if (string.IsNullOrEmpty(mensaje))
            {
                    obj.clave = cn_recursos.ConvetirSha256(obj.clave);
                    return ojbcapadato.registrar(obj, out mensaje);
              } 
            else { return 0; }

        }
        public bool cambiarclave(int idcliente, string nuevaclave, out string mensaje)
        {
            return ojbcapadato.cambiarclave(idcliente, nuevaclave, out mensaje);
        }
        public bool reestablecerclave(int idcliente, string correo, out string mensaje)
        {
            mensaje = string.Empty;
            string nuevaclave = cn_recursos.generarclave();
            bool resultado = ojbcapadato.reestablecerclave(idcliente, cn_recursos.ConvetirSha256(nuevaclave), out mensaje);
            if (resultado)
            {
                string asunto = "contraseña Reestablecida";
                string mensaje_correo = "<h3> Su cuenta fue Reestablecida correctamente</h3></br><p>Su contraseña para acceder es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaclave);

                bool respuesta = cn_recursos.enviarcorreo(correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    return true;
                }
                else
                {
                    mensaje = "no se pudo enviar el correo";
                    return false;
                }
            }
            else
            {
                mensaje = "no se pudo reestablecer";
                return false;
            }
        }


    }
}
