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

        public int ConsultarPorCedula(string cedula)
        {
            var persona = this.conexion!.Personas.FirstOrDefault(p => p.Cedula == cedula);

            if (persona == null)
                throw new Exception("No se encontro ninguna persona con esa cedula");

            var auditoria = new Auditorias()
            {
                TipoAccion = "Consulta por cedula",
                Entidad = "Personas",
                IdEntidad = persona.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se consulto un registro de nacionalidades con id {persona.Id}" +
                              $"\nCon cedula {persona.Nombre}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return persona.Id;
        }
    }
}
