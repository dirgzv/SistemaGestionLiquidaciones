using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Empleado
    {
        public string Nombre { get; set; }
        public float SalarioDevengado { get; set; }

        public Empleado()
        {
        }
        public Empleado (string nombre, float salarioDevengado)
        {
            this.Nombre = nombre;
            this.SalarioDevengado = salarioDevengado;
        }
    }
}
