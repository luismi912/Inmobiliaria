using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class NacionalidadesNegocio : INacionalidadesNegocio
    {
        private IConexion? conexion { get; set; }

        public NacionalidadesNegocio ()
        {
            conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public Nacionalidades Guardar(Nacionalidades entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear correctamente");

            this.conexion!.Nacionalidades!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Nacionalidades> Consultar()
        {
            var Lista = this.conexion!.Nacionalidades!.ToList();
            return Lista;
        }

        public string Eliminar(Nacionalidades entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.Nacionalidades.Remove(entidad);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public Nacionalidades Modificar(Nacionalidades entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
