using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class AuditoriasNegocio : IAuditoriasNegocio
    {
        private IConexion? conexion { get; set; }

        public AuditoriasNegocio ()
        {
            this.conexion = new Conexion();
            this.conexion!.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<Auditorias> Consultar()
        {
            var lista = this.conexion!.Auditorias.ToList();
            return lista;
        }
    }
}
