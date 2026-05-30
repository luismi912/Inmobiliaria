using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class PropiedadesNegocio : IPropiedadesNegocio
    {
        private IConexion? conexion { get; set; }

        public PropiedadesNegocio ()
        {
            this.conexion = new Conexion();
            this.conexion!.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<Propiedades> Consultar()
        {
            var lista = this.conexion!.Propiedades.Include(p => p._Cliente).ToList();
            return lista;
        }

        public Propiedades Modificar(Propiedades entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encuentro ningun registro con ese valor");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "MODIFICAR",
                Entidad = "Propiedades",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se modifico un registro de propiedades con id {entidad.Id}" +
                              $"\nDel cliente con id {entidad.Cliente}" + 
                              $"\nEn el sector con id {entidad.Sector}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }

        public string Eliminar(Propiedades entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.Propiedades.Remove(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "DELETE",
                Entidad = "Propiedades",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se elimino un registro de propiedades con id {entidad.Id}" +
                              $"\nDel cliente con id {entidad.Cliente}" +
                              $"\nEn el sector con id {entidad.Sector}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public Propiedades Guardar(Propiedades entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear este registro");

            this.conexion!.Propiedades.Add(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "INSERT",
                Entidad = "Propiedades",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se guardo un registro de propiedades con id {entidad.Id}" +
                              $"\nDel cliente con id {entidad.Cliente}" +
                              $"\nEn el sector con id {entidad.Sector}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }

        public Propiedades ConsultarConContrato(Contratos contrato)
        {
            var propiedad = this.conexion!.Propiedades.FirstOrDefault(c => c.Id == contrato.Propiedad);

            if (propiedad == null)
                throw new Exception("Hubo un fallo con el contrato, reintente por favor");

            return propiedad;
        }

        public List<Propiedades> ConsultarSectorEmpleado(int Id)
        {
            var empleado = this.conexion!.EmpleadosSectores.Include(e => e._Sector).FirstOrDefault(e => e.Id == Id);

            if (empleado == null)
                throw new Exception("El empleado no fue encontrado, reintente por favor");

            var sector = empleado!._Sector;

            return this.conexion!.Propiedades.Where(p => p.Sector == sector!.Id).ToList();
        }

        public List<Propiedades> ConsultarSectorJefe(int Id)
        {
            var sector = this.conexion!.Sectores.Include(e => e.JefeSector).FirstOrDefault(e => e.JefeSector!.Id == Id);

            if (sector == null)
                throw new Exception("Error con el sector, reintente por favor");

            return this.conexion!.Propiedades.Where(p => p.Sector == sector.Id).ToList();
        }
    }
}
