using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaentidad
{
    public class producto
    {
        public int idproducto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public marca omarca { get; set; }
        public categoria ocategoria { get; set; }
        public decimal precio { get; set; }
        public string preciotexto { get; set; }
        public int stock { get; set; }
        public string rutaimagen { get; set; } 
      public string nombreimagen { get; set; }
       public bool activo { get; set; }

        public string base64 { get; set; }
        public string extesion { get; set; }
    }
}
