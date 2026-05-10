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

            return entidad;
        }

        public string Eliminar(int Id)
        {
            var activo = this.conexion!.ActivosFinancieros.FirstOrDefault(a => a.Id == Id);

            if (activo == null)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion.ActivosFinancieros.Remove(activo);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public ActivosFinancieros Guardar(ActivosFinancieros entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear este registro");

            this.conexion!.ActivosFinancieros.Add(entidad);
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
