using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using BLL;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;

namespace Presentacion
{
    class Program
    {
        static void Main(string[] args)
        {
            LiquidacionBLL logicaLiquidacion = new LiquidacionBLL();
            string opcion;

            do
            {
                Console.WriteLine("1. Agregar liquidación");
                Console.WriteLine("2. Listar liquidaciones");
                Console.WriteLine("3. Eliminar liquidación");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");
                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarLiquidacion(logicaLiquidacion);
                        break;
                    case "2":
                        ListarLiquidaciones(logicaLiquidacion);
                        break;
                    case "3":
                        EliminarLiquidacion(logicaLiquidacion);
                        break;
                    case "4":
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            } while (opcion != "4");
        }
        static void AgregarLiquidacion(LiquidacionBLL logicaLiquidacion)
        {
            Empleado empleado = new Empleado();
            Liquidacion liquidacion = new Liquidacion();

            Console.Write("Digite el nombre del empleado: ");
            empleado.Nombre = Console.ReadLine();

            Console.Write("Digite el salario devengado del empleado: ");
            empleado.SalarioDevengado = float.Parse(Console.ReadLine());

            Console.Write("Digite los días de incapacidad: ");
            liquidacion.DiasIncapacidad = int.Parse(Console.ReadLine());

            liquidacion.empleado = empleado;
            logicaLiquidacion.LiquidacionDeIncapacidad(liquidacion);
            logicaLiquidacion.GuardarLiquidacion(liquidacion);

            Console.WriteLine(" Liquidación agregada exitosamente.\n");
        }
        static void ListarLiquidaciones(LiquidacionBLL logicaLiquidacion)
        {
            List<Liquidacion> liquidaciones = logicaLiquidacion.ListarLiquidaciones();

            if (liquidaciones.Count == 0)
            {
                Console.WriteLine("No hay liquidaciones registradas.\n");
                return;
            }

            Console.WriteLine("\n Listado de Liquidaciones:");
            foreach (var item in liquidaciones)
            {
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine($"Nombre: {item.empleado.Nombre}");
                Console.WriteLine($"Id unico: {item.IdLiquidacionUnico}");
                Console.WriteLine($"Salario Devengado: {item.empleado.SalarioDevengado}");
                Console.WriteLine($"Días de Incapacidad: {item.DiasIncapacidad}");
                Console.WriteLine($"Obligado a pagar: {item.obligadoAPagar.ToString()}");
                Console.WriteLine($"Salario Diario: {item.SalarioDiario}");
                Console.WriteLine($"Valor Dejado Percibir: {item.ValorDejadoPercibir}");
                Console.WriteLine($"Porcentaje Aplicado: {item.PorcentajeAplicado * 100:F2}%");
                Console.WriteLine($"Valor Calculado Incapacidad: {item.ValorCalculadoIncapacidad}");
                Console.WriteLine($"Valor Incapacidad Mínimo: {item.ValorIncapacidadMinimo}");
                Console.WriteLine($"Valor a Pagar: {item.ValorAPagar}");
            }
            Console.WriteLine("-------------------------------------------------\n");
        }
        static void EliminarLiquidacion(LiquidacionBLL logicaLiquidacion)
        {
            Console.Write("Digite el ID de la liquidación a eliminar: ");
            int idLiquidacion = int.Parse(Console.ReadLine());
            ListarLiquidaciones(logicaLiquidacion);
            bool Eliminada = logicaLiquidacion.EliminarLiquidacion(idLiquidacion);
            ListarLiquidaciones(logicaLiquidacion);
        }
    }
}
