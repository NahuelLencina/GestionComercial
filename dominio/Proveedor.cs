using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cuit { get; set; }

        // Sobrescribimos el Metodo ToString y Retornamos la descripción
        public override string ToString()
        {
            return Nombre; // usar la propiedad correcta
        }
    }
}
