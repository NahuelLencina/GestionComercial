using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class VentaItem
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public double PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public double PrecioTotal { get; set; }
    }
}
