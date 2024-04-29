using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaentidad
{
   public class detalleventa
    {
        public int iddetalleventa { get; set; }
        public int idventa { get; set; }
        public producto oproducto{ get; set; }
        public int  cantidad { get; set; }
        public decimal total { get; set; }
        public string idtransacion { get; set; }
    }
}
