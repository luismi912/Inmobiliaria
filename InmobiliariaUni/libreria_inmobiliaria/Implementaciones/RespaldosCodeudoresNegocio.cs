using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class RespaldosCodeudoresNegocio : IRespaldosCodeudoresNegocio
    {
        private IConexion? conexion { get; set; }

        public RespaldosCodeudoresNegocio()
        {
            conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public RespaldosCodeudores Guardar(RespaldosCodeudores entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear correctamente");

            this.conexion!.RespaldosCodeudores!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<RespaldosCodeudores> Consultar()
        {
            var Lista = this.conexion!.RespaldosCodeudores!.ToList();
            return Lista;
        }

        public string Eliminar(int Id)
        {
            var respaldos = this.conexion!.RespaldosCodeudores!.FirstOrDefault(p => p.Id == Id);

            if (respaldos == null)
                return "No exsite la entidad a eliminar";

            this.conexion.RespaldosCodeudores.Remove(respaldos);
            this.conexion.SaveChanges();

            return "Se elimino la nacionalidad correctamente";
        }

        public RespaldosCodeudores Modificar(RespaldosCodeudores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
