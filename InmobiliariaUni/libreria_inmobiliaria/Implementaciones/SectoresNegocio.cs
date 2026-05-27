using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class SectoresNegocio : ISectoresNegocio
    {
        private IConexion? conexion { get; set; }

        public SectoresNegocio()
        {
            conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public Sectores Guardar(Sectores entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear correctamente");

            this.conexion!.Sectores!.Add(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "INSERT",
                Entidad = "Sectores",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se guardo un registro de sectores con id {entidad.Id}" +
                              $"\nEn la ciudad con id {entidad.Ciudad}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }

        public List<Sectores> Consultar()
        {
            var Lista = this.conexion!.Sectores!.ToList();
            return Lista;
        }

        public string Eliminar(Sectores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.Sectores.Remove(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "DELETE",
                Entidad = "Sectores",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se elimino un registro de sectores con id {entidad.Id}" +
                              $"\nEn la ciudad con id {entidad.Ciudad}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public Sectores Modificar(Sectores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "MODIFICAR",
                Entidad = "Sectores",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se modifico un registro de sectores con id {entidad.Id}" +
                              $"\nEn la ciudad con id {entidad.Ciudad}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
