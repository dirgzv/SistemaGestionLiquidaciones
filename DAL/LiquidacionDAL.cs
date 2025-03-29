using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace DAL
{
    public class LiquidacionDAL
    {
        private readonly string FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "liquidaciones.txt");

        public void GuardarLiquidacion(Liquidacion liquidacion)
        {
            try
            {
                liquidacion.IdLiquidacionUnico = GenerarIdUnico(ListarLiquidaciones());
                using (StreamWriter sw = new StreamWriter(FileName, true)) 
                {
                    sw.WriteLine(string.Join(";",  
                        liquidacion.empleado.Nombre,
                        liquidacion.empleado.SalarioDevengado,
                        liquidacion.IdLiquidacionUnico,
                        liquidacion.DiasIncapacidad,
                        liquidacion.obligadoAPagar,
                        liquidacion.SalarioDiario,
                        liquidacion.ValorDejadoPercibir,
                        liquidacion.PorcentajeAplicado,
                        liquidacion.ValorCalculadoIncapacidad,
                        liquidacion.ValorIncapacidadMinimo,
                        liquidacion.ValorAPagar
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en Guardar Liquidacion: {ex.Message}");
            }
        }

        private int GenerarIdUnico(List<Liquidacion> liquidaciones)
        {
            Random rnd = new Random();
            int idLiquidacionUnico;

            do
            {
                idLiquidacionUnico = rnd.Next(1000, 9999);
            } while (liquidaciones.Any(l => l.IdLiquidacionUnico == idLiquidacionUnico));

            return idLiquidacionUnico;
        }

        public List<Liquidacion> ListarLiquidaciones()
        {
            List<Liquidacion> liquidaciones = new List<Liquidacion>();

            try
            {
                if (!File.Exists(FileName))
                {
                    Console.WriteLine("Archivo no encontrado o vacio. No hay liquidaciones guardadas.");
                    return liquidaciones;
                }

                string[] lineas = File.ReadAllLines(FileName);

                foreach (string linea in lineas)
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue; 

                    string[] datos = linea.Split(';');

                    if (datos.Length != 11) continue; 

                    if (!decimal.TryParse(datos[1], out decimal salarioDevengado) ||
                        !int.TryParse(datos[2], out int idLiquidacion) ||
                        !int.TryParse(datos[3], out int diasIncapacidad) ||
                        !Enum.TryParse(datos[4], out ObligadoAPagar obligadoAPagar) ||
                        !decimal.TryParse(datos[5], out decimal salarioDiario) ||
                        !decimal.TryParse(datos[6], out decimal valorDejadoPercibir) ||
                        !decimal.TryParse(datos[7], out decimal porcentajeAplicado) ||
                        !decimal.TryParse(datos[8], out decimal valorCalculadoIncapacidad) ||
                        !decimal.TryParse(datos[9], out decimal valorIncapacidadMinimo) ||
                        !decimal.TryParse(datos[10], out decimal valorAPagar))
                    {
                        continue; 
                    }
                    Liquidacion liquidacion = new Liquidacion
                    {
                        empleado = new Empleado
                        {
                            Nombre = datos[0],
                            SalarioDevengado = (float)salarioDevengado
                        },
                        IdLiquidacionUnico = idLiquidacion,
                        DiasIncapacidad = diasIncapacidad,
                        obligadoAPagar = obligadoAPagar,
                        SalarioDiario = (float)salarioDiario,
                        ValorDejadoPercibir = (float)valorDejadoPercibir,
                        PorcentajeAplicado = (float)porcentajeAplicado,
                        ValorCalculadoIncapacidad = (float)valorCalculadoIncapacidad,
                        ValorIncapacidadMinimo = (float)valorIncapacidadMinimo,
                        ValorAPagar = (float)valorAPagar
                    };
                    liquidaciones.Add(liquidacion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ListarLiquidaciones: {ex.Message}");
            }

            return liquidaciones;
        }
        public bool EliminarLiquidacion(int idLiquidacion)
        {
            List<Liquidacion> liquidaciones = ListarLiquidaciones();
            List<Liquidacion> liquidacionesFiltradas = liquidaciones
                .Where(l => l.IdLiquidacionUnico != idLiquidacion)
                .ToList();

            if (liquidacionesFiltradas.Count == liquidaciones.Count)
            {
                Console.WriteLine($" No se encontró ninguna liquidación con ID {idLiquidacion}.");
                return false; 
            }

            try
            {
                if (liquidacionesFiltradas.Count > 0)
                {
                    File.WriteAllText(FileName, ""); 
                    foreach (Liquidacion liquidacion in liquidacionesFiltradas)
                    {
                        GuardarLiquidacion(liquidacion);
                    }
                }
                else
                {
                    File.WriteAllText(FileName, "");
                }

                Console.WriteLine($" Liquidación con ID {idLiquidacion} eliminada correctamente.");
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en EliminarLiquidacion: {ex.Message}");
            }
        }



    }
}
