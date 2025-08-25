using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string UrlImagen { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        // Relaciones
        public int idProveedor { get; set; }
        public Proveedor Proveedor { get; set; }

        public int idCategoria { get; set; }
        public Categoria Categoria { get; set; }
    }

}
