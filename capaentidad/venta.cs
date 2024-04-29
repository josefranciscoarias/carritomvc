using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaentidad
{
    public class venta
    {
        public int idventa { get; set; }
        public int idcliente { get; set; }
        public int totalproducto { get; set; } 
        public decimal montototal  { get; set; }
        public string contacto { get; set; }
        public string iddistrito { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string fechatexto { get; set; }
        public string idtransacion { get; set; }
        public List<detalleventa> odetalleventa { get; set; }

    }
}
