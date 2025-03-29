using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;

namespace BLL
{
    public class LiquidacionBLL
    {
        LiquidacionDAL liquidacionDAL = new LiquidacionDAL();
        public void GuardarLiquidacion(Liquidacion liquidacion)
        {
            liquidacionDAL.GuardarLiquidacion(liquidacion);
        }
        public void LiquidacionDeIncapacidad(Liquidacion liquidacion)
        {
            liquidacion.LiquidacionDeIncapacidad();
        }
        public List<Liquidacion> ListarLiquidaciones()
        {
            return liquidacionDAL.ListarLiquidaciones();
        }
        public bool EliminarLiquidacion(int idLiquidacion)
        {
            return liquidacionDAL.EliminarLiquidacion(idLiquidacion);
        }
    }
}
