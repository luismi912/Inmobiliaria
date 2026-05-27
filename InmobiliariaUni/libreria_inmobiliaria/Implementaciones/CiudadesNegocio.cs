using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class CiudadesNegocio : ICiudadesNegocio
    {
        private IConexion? conexion { get; set; }

        public CiudadesNegocio ()
        {
            this.conexion = new Conexion();
            this.conexion!.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<Ciudades> Consultar()
        {
            var lista = this.conexion!.Ciudades.ToList();
            return lista;
        }

        public Ciudades Modificar(Ciudades entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encuentro ningun registro con ese valor");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "MODIFICAR",
                Entidad = "Ciudades",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se modifico un registro de ciudades con id {entidad.Id}" +
                              $"\nA la ciudad con nombre {entidad.Nombre}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }

        public string Eliminar(Ciudades entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.Ciudades.Remove(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "MODIFICAR",
                Entidad = "Ciudades",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se elimino un registro de ciudades con id {entidad.Id}" +
                              $"\nA la ciudad con nombre {entidad.Nombre}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public Ciudades Guardar(Ciudades entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear este registro");

            this.conexion!.Ciudades.Add(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "INSERT",
                Entidad = "Ciudades",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se guardo un registro de ciudades con id {entidad.Id}" +
                              $"\nA la ciudad con nombre {entidad.Nombre}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
