using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaentidad
{
   public class carrito
    {

        public int idcarrito { get; set; }
        public cliente ocliente { get; set; }
        public producto oproducto { get; set; }
        public int cantidad { get; set; }
    }
}
