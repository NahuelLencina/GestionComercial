using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string  Telefono { get; set; }
        public string  Cuit { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }

    }
}
