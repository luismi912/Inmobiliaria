using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class SectoresNegocio : ISectoresNegocio
    {
        private IConexion? conexion { get; set; }

        public SectoresNegocio()
        {
            conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public Sectores Guardar(Sectores entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear correctamente");

            this.conexion!.Sectores!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Sectores> Consultar()
        {
            var Lista = this.conexion!.Sectores!.ToList();
            return Lista;
        }

        public string Eliminar(int Id)
        {
            var sector = this.conexion!.Sectores!.FirstOrDefault(p => p.Id == Id);

            if (sector == null)
                return "No exsite la entidad a eliminar";

            this.conexion.Sectores.Remove(sector);
            this.conexion.SaveChanges();

            return "Se elimino la nacionalidad correctamente";
        }

        public Sectores Modificar(Sectores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
