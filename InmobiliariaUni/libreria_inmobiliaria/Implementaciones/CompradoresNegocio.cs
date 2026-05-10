using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class CompradoresNegocio : ICompradoresNegocio
    {
        private IConexion? conexion { get; set; }

        public CompradoresNegocio()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<Compradores> Consultar()
        {
            var Lista = this.conexion!.Compradores!.ToList();
            return Lista;
        }

        public string Eliminar(string cedula)
        {
            var admin = this.conexion!.Compradores!.FirstOrDefault(p => p.Cedula == cedula);

            if (admin == null)
                return "No exsite la entidad a eliminar";

            this.conexion.Compradores.Remove(admin);
            this.conexion.SaveChanges();

            return "Se elimino al empleado correctamente";
        }

        public Compradores Modificar(Compradores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
