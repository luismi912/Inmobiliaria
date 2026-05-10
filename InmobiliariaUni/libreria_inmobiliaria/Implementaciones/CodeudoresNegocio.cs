using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class CodeudoresNegocio : ICodeudoresNegocio
    {
        private IConexion? conexion { get; set; }

        public CodeudoresNegocio()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<Codeudores> Consultar()
        {
            var Lista = this.conexion!.Codeudores!.ToList();
            return Lista;
        }

        public string Eliminar(string cedula)
        {
            var codeudor = this.conexion!.Codeudores!.FirstOrDefault(p => p.Cedula == cedula);

            if (codeudor == null)
                return "No exsite la entidad a eliminar";

            this.conexion.Codeudores.Remove(codeudor);
            this.conexion.SaveChanges();

            return "Se elimino al empleado correctamente";
        }

        public Codeudores Modificar(Codeudores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
