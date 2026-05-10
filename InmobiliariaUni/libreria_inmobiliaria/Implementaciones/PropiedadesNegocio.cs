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
            var lista = this.conexion!.Propiedades.ToList();
            return lista;
        }

        public Propiedades Modificar(Propiedades entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encuentro ningun registro con ese valor");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }

        public string Eliminar(int Id)
        {
            var activo = this.conexion!.Propiedades.FirstOrDefault(a => a.Id == Id);

            if (activo == null)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion.Propiedades.Remove(activo);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public Propiedades Guardar(Propiedades entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear este registro");

            this.conexion!.Propiedades.Add(entidad);
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
