using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class GenerosNegocio : IGenerosNegocio
    {
        private IConexion? conexion { get; set; }

        public GenerosNegocio()
        {
            conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public Generos Guardar(Generos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear correctamente");

            this.conexion!.Generos!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Generos> Consultar()
        {
            var Lista = this.conexion!.Generos!.ToList();
            return Lista;
        }

        public string Eliminar(int Id)
        {
            var genero = this.conexion!.Generos!.FirstOrDefault(p => p.Id == Id);

            if (genero == null)
                return "No exsite la entidad a eliminar";

            this.conexion.Generos.Remove(genero);
            this.conexion.SaveChanges();

            return "Se elimino la nacionalidad correctamente";
        }

        public Generos Modificar(Generos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
