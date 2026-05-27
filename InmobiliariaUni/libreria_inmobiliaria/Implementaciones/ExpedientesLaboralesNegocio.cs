using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class ExpedientesLaboralesNegocio : IExpedientesLaboralesNegocio
    {
        private IConexion? conexion { get; set; }

        public ExpedientesLaboralesNegocio ()
        {
            this.conexion = new Conexion();
            this.conexion!.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<ExpedientesLaborales> Consultar()
        {
            var lista = this.conexion!.ExpedientesLaborales.ToList();
            return lista;
        }

        public ExpedientesLaborales Modificar(ExpedientesLaborales entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encuentro ningun registro con ese valor");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "MODIFICO",
                Entidad = "Expediente",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se modifico un registro de expedientes con id {entidad.Id}" +
                              $"\nDe la persona con id {entidad.Persona}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }

        public string Eliminar(ExpedientesLaborales entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.ExpedientesLaborales.Remove(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "DELETE",
                Entidad = "Expediente",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se elimino un registro de expedientes con id {entidad.Id}" +
                              $"\nDe la persona con id {entidad.Persona}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public ExpedientesLaborales Guardar(ExpedientesLaborales entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear este registro");

            this.conexion!.ExpedientesLaborales.Add(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "INSERT",
                Entidad = "Expediente",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se guardo un registro de expedientes con id {entidad.Id}" +
                              $"\nDe la persona con id {entidad.Persona}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
