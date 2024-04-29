using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capadato;
using capaentidad;

namespace capanegocio
{
    public class cn_usuario
    {
        private cd_usuario ojbcapadato = new cd_usuario();

        public List<usuario> Listar()
        {
            return ojbcapadato.Listar();
        }
        public int registrar(usuario obj, out string mensaje)

        {
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

                string clave = cn_recursos.generarclave();
                string asunto = "creacion de cuenta";
                string mensaje_correo = "<h3> Su cuenta fue creada correctamente</h3></br><p>Su contraseña para acceder es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);

                bool respuesta = cn_recursos.enviarcorreo(obj.correo, asunto, mensaje_correo);
                if (respuesta)
                {

                    obj.clave = cn_recursos.ConvetirSha256(clave);
                    return ojbcapadato.registrar(obj, out mensaje);
                   

                }

                else
                {
                    mensaje = " no se puede enviar el correo";
                    return 0;


                }

            }
            else { return 0; }
        }

        public bool editar(usuario obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.apellido) || string.IsNullOrWhiteSpace(obj.apellido))
            {
                mensaje = "no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {
                mensaje = "no puede estar vacio";
            }
           
            if (string.IsNullOrEmpty(mensaje))
            {
            // string clave = obj.clave;

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
        public bool cambiarclave(int idusuario, string nuevaclave, out string mensaje)
        {
            return ojbcapadato.cambiarclave(idusuario,nuevaclave, out mensaje);
        }
        public bool reestablecerclave(int idusuario, string correo, out string mensaje)
        {
            mensaje = string.Empty;
            string nuevaclave = cn_recursos.generarclave();
            bool resultado = ojbcapadato.reestablecerclave(idusuario, cn_recursos.ConvetirSha256(nuevaclave), out mensaje);
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

