using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class RespaldosCodeudoresNegocio : IRespaldosCodeudoresNegocio
    {
        private IConexion? conexion { get; set; }

        public RespaldosCodeudoresNegocio()
        {
            conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public RespaldosCodeudores Guardar(RespaldosCodeudores entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear correctamente");

            this.conexion!.RespaldosCodeudores!.Add(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "INSERT",
                Entidad = "RespaldosCodeudores",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se guardo un registro de RespaldosCodeudores con id {entidad.Id}" +
                              $"\nDel codeudor con id {entidad.Codeudor}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }

        public List<RespaldosCodeudores> Consultar()
        {
            var Lista = this.conexion!.RespaldosCodeudores!.ToList();
            return Lista;
        }

        public string Eliminar(RespaldosCodeudores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.RespaldosCodeudores.Remove(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "DELETE",
                Entidad = "RespaldosCodeudores",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se elimino un registro de RespaldosCodeudores con id {entidad.Id}" +
                              $"\nDel codeudor con id {entidad.Codeudor}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public RespaldosCodeudores Modificar(RespaldosCodeudores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "MODIFICAR",
                Entidad = "RespaldosCodeudores",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se modifico un registro de RespaldosCodeudores con id {entidad.Id}" +
                              $"\nDel codeudor con id {entidad.Codeudor}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
