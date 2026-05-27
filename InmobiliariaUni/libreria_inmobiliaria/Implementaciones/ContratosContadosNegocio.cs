using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class ContratosContadosNegocio : IContratosContadosNegocio
    {
        private IConexion? conexion { get; set; }

        public ContratosContadosNegocio()
        {
            conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public ContratosContados Guardar(ContratosContados entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear correctamente");

            this.conexion!.ContratosContados!.Add(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "INSERT",
                Entidad = "Contratos contados",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se guardo un registro de contratos contados con id {entidad.Id}" +
                              $"\nAl cliente con la propiedad {entidad.Id}" +
                              $"\nAl comprado con id {entidad.Comprador}" +
                              $"\nCon la asesoria de {entidad.EmpleadoSector}" +
                              $"\nComo contrato contado"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }

        public List<ContratosContados> Consultar()
        {
            var Lista = this.conexion!.ContratosContados!.ToList();
            return Lista;
        }

        public string Eliminar(ContratosContados entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.ContratosContados.Remove(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "DELETE",
                Entidad = "Contratos contados",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se elimino un registro de contratos contados con id {entidad.Id}" +
                              $"\nAl cliente con la propiedad {entidad.Id}" +
                              $"\nAl comprado con id {entidad.Comprador}" +
                              $"\nCon la asesoria de {entidad.EmpleadoSector}" +
                              $"\nComo contrato contado"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public ContratosContados Modificar(ContratosContados entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "MODIFICAR",
                Entidad = "Contratos contados",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se modifico un registro de contratos contados con id {entidad.Id}" +
                              $"\nAl cliente con la propiedad {entidad.Propiedad}" +
                              $"\nAl comprado con id {entidad.Comprador}" +
                              $"\nCon la asesoria de {entidad.EmpleadoSector}" +
                              $"\nComo contrato contado"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
