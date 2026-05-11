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
            return entidad;
        }

        public List<Departamentos> Consultar()
        {
            var Lista = this.conexion!.Departamentos!.ToList();
            return Lista;
        }

        public string Eliminar(Departamentos entidad)
        {
            if (entidad == null)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.Departamentos.Remove(entidad);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public Departamentos Modificar(Departamentos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
