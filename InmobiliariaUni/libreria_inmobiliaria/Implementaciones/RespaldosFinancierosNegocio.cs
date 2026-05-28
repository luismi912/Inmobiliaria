using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class RespaldosFinancierosNegocio : IRespaldosFinancierosNegocio
    {
        private IConexion? conexion { get; set; }

        public RespaldosFinancierosNegocio()
        {
            conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<RespaldosFinancieros> Consultar()
        {
            var Lista = this.conexion!.RespaldosFinancieros!.ToList();
            return Lista;
        }
    }
}
