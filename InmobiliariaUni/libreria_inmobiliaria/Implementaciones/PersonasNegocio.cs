using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class PersonasNegocio : IPersonasNegocio
    {
        private IConexion? conexion { get; set; }

        public PersonasNegocio ()
        {
            this.conexion = new Conexion();
            this.conexion!.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<Personas> Consultar()
        {
            var lista = this.conexion!.Personas.ToList();
            return lista;
        }
    }
}
