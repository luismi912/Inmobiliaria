using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class BienesNegocio : IBienesNegocio
    {
        private IConexion? conexion { get; set; }

        public BienesNegocio ()
        {
            this.conexion = new Conexion();
            this.conexion!.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<Bienes> Consultar()
        {
            var lista = this.conexion!.Bienes.ToList();
            return lista;
        }

        public Bienes Modificar(Bienes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encuentro ningun registro con ese valor");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "MODIFICAR",
                Entidad = "Bienes",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se modifico un registro de bienes con id {entidad.Id}" +
                              $"\nA el bien con nombre {entidad.Nombre}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }

        public string Eliminar(Bienes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.Bienes.Remove(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "DELETE",
                Entidad = "Bienes",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se elimino un registro de Bienes con id {entidad.Id}" +
                              $"\nA el bien con nombre {entidad.Nombre}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public Bienes Guardar(Bienes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear este registro");

            this.conexion!.Bienes.Add(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "INSERT",
                Entidad = "Bienes",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se guardo un registro de Bienes con id {entidad.Id}" +
                              $"\nA el bien con nombre {entidad.Nombre}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }

        public RespaldosFinancieros ConsultarRespaldoCedula(string cedula)
        {
            var comprador = this.conexion!.Compradores.FirstOrDefault(c => c.Cedula == cedula);

            if (comprador == null)
            {
                var codeudor = this.conexion!.Codeudores.FirstOrDefault(c => c.Cedula == cedula);

                if (codeudor == null)
                    throw new Exception("No hay ninguna persona con esta cedula, reintente por favor");

                var respaldocodeudor = this.conexion!.RespaldosCodeudores.FirstOrDefault(r => r.Codeudor == codeudor.Id);

                return respaldocodeudor!;
            }

            var respaldocomprador = this.conexion!.RespaldosCompradores.FirstOrDefault(r => r.Comprador == comprador.Id);

            return respaldocomprador!;
        }
    }
}
