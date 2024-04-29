using capadato;
using capaentidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capanegocio
{
   public class cn_reporte
    {
        private cd_reporte ojbcapareporte = new cd_reporte();

        public List<reporte> venta(string fechainicio, string fechafin, string idtransaccion)
        {
            return ojbcapareporte.venta(fechainicio,fechafin,idtransaccion);
        }

        private cd_reporte ojbcaparepo = new cd_reporte();

        public dashboard verdashboard()
        {
            return ojbcaparepo.verdashboard();
        }

    }
}
