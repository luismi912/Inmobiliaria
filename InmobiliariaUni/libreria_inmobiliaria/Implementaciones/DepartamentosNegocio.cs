using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class DepartamentosNegocio : IDepartamentosNegocio
    {
        private IConexion? conexion { get; set; }

        public DepartamentosNegocio()
        {
            conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public Departamentos Guardar(Departamentos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear correctamente");

            this.conexion!.Departamentos!.Add(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "INSERT",
                Entidad = "Departamentos",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se guardo un registro de Departamentos con id {entidad.Id}" +
                              $"\nAl departamento con llamado {entidad.Nombre}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }

        public List<Departamentos> Consultar()
        {
            var Lista = this.conexion!.Departamentos!.ToList();
            return Lista;
        }

        public string Eliminar(Departamentos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.Departamentos.Remove(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "DELETE",
                Entidad = "Departamentos",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se elimino un registro de Departamentos con id {entidad.Id}" +
                              $"\nAl departamento con llamado {entidad.Nombre}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public Departamentos Modificar(Departamentos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "MODIFICAR",
                Entidad = "Departamentos",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se modifico un registro de Departamentos con id {entidad.Id}" +
                              $"\nAl departamento con llamado {entidad.Nombre}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
