using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class ContratosCuotasNegocio : IContratosCuotasNegocio
    {
        private IConexion? conexion { get; set; }

        public ContratosCuotasNegocio()
        {
            conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public ContratosCuotas Guardar(ContratosCuotas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear correctamente");

            this.conexion!.ContratosCuotas!.Add(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "INSERT",
                Entidad = "Contratos cuotas",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se guardo un registro de contratos cuotas con id {entidad.Id}" +
                              $"\nAl cliente con la propiedad {entidad.Propiedad}" +
                              $"\nAl comprado con id {entidad.Comprador}" +
                              $"\nCon la asesoria de {entidad.EmpleadoSector}" +
                              $"\nComo contrato cuotas"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }

        public List<ContratosCuotas> Consultar()
        {
            var Lista = this.conexion!.ContratosCuotas!.ToList();
            return Lista;
        }

        public string Eliminar(ContratosCuotas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.ContratosCuotas.Remove(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "DELETE",
                Entidad = "Contratos cuotas",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se elimino un registro de contratos cuotas con id {entidad.Id}" +
                              $"\nAl cliente con la propiedad {entidad.Propiedad}" +
                              $"\nAl comprado con id {entidad.Comprador}" +
                              $"\nCon la asesoria de {entidad.EmpleadoSector}" +
                              $"\nComo contrato cuotas"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public ContratosCuotas Modificar(ContratosCuotas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "MODIFICAR",
                Entidad = "Contratos cuotas",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se modifico un registro de contratos cuotas con id {entidad.Id}" +
                              $"\nAl cliente con la propiedad {entidad.Propiedad}" +
                              $"\nAl comprador con id {entidad.Comprador}" +
                              $"\nCon la asesoria de {entidad.EmpleadoSector}" +
                              $"\nComo contrato cuotas"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
