using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class EmpleadosSectoresNegocio : IEmpleadosSectoresNegocio
    {
        private IConexion? conexion { get; set; }

        public EmpleadosSectoresNegocio()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<EmpleadosSectores> Consultar()
        {
            var Lista = this.conexion!.EmpleadosSectores!.ToList();
            return Lista;
        }

        public string Eliminar(string cedula)
        {
            var empleado = this.conexion!.EmpleadosSectores!.FirstOrDefault(p => p.Cedula == cedula);

            if (empleado == null)
                return "No exsite la entidad a eliminar";

            this.conexion.EmpleadosSectores.Remove(empleado);
            this.conexion.SaveChanges();

            return "Se elimino al empleado correctamente";
        }

        public EmpleadosSectores Modificar(EmpleadosSectores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
