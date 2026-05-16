using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class ContratosArriendosNegocio : IContratosArriendosNegocio
    {
        private IConexion? conexion { get; set; }

        public ContratosArriendosNegocio()
        {
            conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public ContratosArriendos Guardar(ContratosArriendos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear correctamente");

            this.conexion!.ContratosArriendos!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<ContratosArriendos> Consultar()
        {
            var Lista = this.conexion!.ContratosArriendos!.ToList();
            return Lista;
        }

        public string Eliminar(ContratosArriendos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.ContratosArriendos.Remove(entidad);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public ContratosArriendos Modificar(ContratosArriendos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
