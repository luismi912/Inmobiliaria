using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class TiposPropiedadesNegocio : ITiposPropiedadesNegocio
    {
        private IConexion? conexion { get; set; }

        public TiposPropiedadesNegocio()
        {
            conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public TiposPropiedades Guardar(TiposPropiedades entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear correctamente");

            this.conexion!.TiposPropiedades!.Add(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "INSERT",
                Entidad = "Tipos Propiedaes",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se guardo un registro de tipos propiedad con id {entidad.Id}" +
                              $"\nPara el manejo de un nuevo tipo de propiedad {entidad.Nombre}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }

        public List<TiposPropiedades> Consultar()
        {
            var Lista = this.conexion!.TiposPropiedades!.ToList();
            return Lista;
        }

        public string Eliminar(TiposPropiedades entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.TiposPropiedades.Remove(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "DELETE",
                Entidad = "Tipos Propiedaes",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se elimino un registro de tipos propiedad con id {entidad.Id}" +
                              $"\nPara el manejo de propiedades {entidad.Nombre}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public TiposPropiedades Modificar(TiposPropiedades entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "MODIFICAR",
                Entidad = "Tipos Propiedaes",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se modifico un registro de tipos propiedad con id {entidad.Id}" +
                              $"\nPara el manejo del tipo de propiedad {entidad.Nombre}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
