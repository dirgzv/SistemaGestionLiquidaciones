using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Liquidacion
    {
        public Empleado empleado { get; set; }
        public int IdLiquidacionUnico { get; set; }
        public int DiasIncapacidad { get; set; }
        public ObligadoAPagar obligadoAPagar { get; set; }
        public float SalarioDiario { get; set; }
        public float ValorDejadoPercibir { get; set; }
        public float PorcentajeAplicado { get; set; }
        public float ValorCalculadoIncapacidad { get; set; }
        public float ValorIncapacidadMinimo { get; set; }
        public float ValorAPagar { get; set; }

        public Liquidacion()
        {
        }

        public Liquidacion(Empleado empleado, int idLiquidacionUnico, int diasIncapacidad, ObligadoAPagar obligadoAPagar, float salarioDiario, float valorDejadoPercibir, float porcentajeAplicado, float valorCalculadoIncapacidad, float valorIncapacidadMinimo, float valorAPagar)
        {
            this.empleado = empleado;
            IdLiquidacionUnico = idLiquidacionUnico;
            DiasIncapacidad = diasIncapacidad;
            this.obligadoAPagar = obligadoAPagar;
            SalarioDiario = salarioDiario;
            ValorDejadoPercibir = valorDejadoPercibir;
            PorcentajeAplicado = porcentajeAplicado;
            ValorCalculadoIncapacidad = valorCalculadoIncapacidad;
            ValorIncapacidadMinimo = valorIncapacidadMinimo;
            ValorAPagar = valorAPagar;
        }

        public void LiquidacionDeIncapacidad() 
        {
            float SalarioMinimoDiario = 43333.33f;
            SalarioDiario = empleado.SalarioDevengado / 30;
            ValorDejadoPercibir = SalarioDiario * DiasIncapacidad;
            CalcularPorcentajeAplicado();
            CalcularAPagar();
            ValorCalculadoIncapacidad = ValorDejadoPercibir * PorcentajeAplicado;
            ValorIncapacidadMinimo = SalarioMinimoDiario * DiasIncapacidad;
            if ((SalarioDiario*PorcentajeAplicado) > SalarioMinimoDiario)
            {
                ValorAPagar = ValorCalculadoIncapacidad;
            }
            else
            {
                ValorAPagar = ValorIncapacidadMinimo;
            }
        }

        public void CalcularPorcentajeAplicado()
        {
            if (DiasIncapacidad >= 1 && DiasIncapacidad <= 2)
            {
                PorcentajeAplicado = 0.6666f;
            }
            else if (DiasIncapacidad >= 3 && DiasIncapacidad <= 15)
            {
                PorcentajeAplicado = 0.6666f;
            }
            else if (DiasIncapacidad >= 16 && DiasIncapacidad <= 90)
            {
                PorcentajeAplicado = 0.6f;
            }
            else if (DiasIncapacidad >= 91 && DiasIncapacidad <= 540)
            {
                PorcentajeAplicado = 0.5f;
            }
            
        }

        


        public void CalcularAPagar()
        {
            if (DiasIncapacidad <= 2)
            {
                obligadoAPagar = ObligadoAPagar.Empleador;
            }
            else if (DiasIncapacidad <= 90)
            {
                obligadoAPagar = ObligadoAPagar.Eps;
            }
            else if (DiasIncapacidad <= 540)
            {
                obligadoAPagar = ObligadoAPagar.FondoPensiones;
            }
        }
    }
}
