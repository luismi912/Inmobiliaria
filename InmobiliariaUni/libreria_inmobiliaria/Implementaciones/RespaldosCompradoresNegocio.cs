using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class RespaldosCompradoresNegocio : IRespaldosCompradoresNegocio
    {
        private IConexion? conexion { get; set; }

        public RespaldosCompradoresNegocio()
        {
            conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public RespaldosCompradores Guardar(RespaldosCompradores entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear correctamente");

            this.conexion!.RespaldosCompradores!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<RespaldosCompradores> Consultar()
        {
            var Lista = this.conexion!.RespaldosCompradores!.ToList();
            return Lista;
        }

        public string Eliminar(RespaldosCompradores entidad)
        {
            if (entidad == null)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.RespaldosCompradores.Remove(entidad);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public RespaldosCompradores Modificar(RespaldosCompradores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
