using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class JefesSectoresNegocio : IJefesSectoresNegocio
    {
        private IConexion? conexion { get; set; }

        public JefesSectoresNegocio ()
        {
            this.conexion = new Conexion();
            this.conexion!.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<JefesSectores> Consultar()
        {
            var lista = this.conexion!.JefesSectores.ToList();
            return lista;
        }

        public JefesSectores Modificar(JefesSectores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encuentro ningun registro con ese valor");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }

        public string Eliminar(string cedula)
        {
            var jefe = this.conexion!.JefesSectores.FirstOrDefault(a => a.Cedula == cedula);

            if (jefe == null)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion.JefesSectores.Remove(jefe);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }
    }
}
