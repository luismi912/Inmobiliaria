using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class ActivosFinancierosNegocio : IActivosFinancierosNegocio
    {
        private IConexion? conexion { get; set; }

        public ActivosFinancierosNegocio ()
        {
            this.conexion = new Conexion();
            this.conexion!.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<ActivosFinancieros> Consultar()
        {
            var lista = this.conexion!.ActivosFinancieros.ToList();
            return lista;
        }

        public ActivosFinancieros Modificar(ActivosFinancieros entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encuentro ningun registro con ese valor");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "MODIFICAR",
                Entidad = "Activo financiero",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se modifico un registro de activos financieros con id {entidad.Id}" +
                              $"\nCon el activo de {entidad.Nombre}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }

        public string Eliminar(ActivosFinancieros entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.ActivosFinancieros.Remove(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "DELETE",
                Entidad = "Activo financiero",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se elimino un registro de activos financieros con id {entidad.Id}" +
                              $"\nCon el activo de {entidad.Nombre}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public ActivosFinancieros Guardar(ActivosFinancieros entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear este registro");

            this.conexion!.ActivosFinancieros.Add(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "INSERT",
                Entidad = "Activo financiero",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se registro un activo con id {entidad.Id}" +
                              $"\nCon el activo de {entidad.Nombre}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
