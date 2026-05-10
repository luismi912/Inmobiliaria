using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class ClientesNegocio : IClientesNegocio
    {
        private IConexion? conexion { get; set; }

        public ClientesNegocio()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<Clientes> Consultar()
        {
            var Lista = this.conexion!.Clientes!.ToList();
            return Lista;
        }

        public string Eliminar(string cedula)
        {
            var codeudor = this.conexion!.Clientes!.FirstOrDefault(p => p.Cedula == cedula);

            if (codeudor == null)
                return "No exsite la entidad a eliminar";

            this.conexion.Clientes.Remove(codeudor);
            this.conexion.SaveChanges();

            return "Se elimino al empleado correctamente";
        }

        public Clientes Modificar(Clientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
