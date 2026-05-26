using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class UsuariosRolesNegocio : IUsuariosRolesNegocio
    {
        private IConexion? conexion { get; set; }

        public UsuariosRolesNegocio ()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<UsuariosRoles> Consultar()
        {
            var lista = this.conexion!.UsuariosRoles!.ToList();
            return lista;
        }

        public string? ConsultarCorreo(string correo)
        {
            var usuario = this.conexion!.UsuariosRoles.FirstOrDefault(u => u.Correo == correo);
            
            if (usuario == null)
                return null;

            return correo;
        }

        public UsuariosRoles Modificar(UsuariosRoles entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }

        public string Eliminar(UsuariosRoles entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.UsuariosRoles.Remove(entidad);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }
    }
}
