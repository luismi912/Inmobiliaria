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

        public List<UsuarioRoles> Consultar()
        {
            var lista = this.conexion!.UsuariosRoles!.ToList();
            return lista;
        }

        public UsuarioRoles Modificar(UsuarioRoles entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }

        public string Eliminar(int Id)
        {
            var usuario = this.conexion!.UsuariosRoles.FirstOrDefault(u => u.Id == Id);

            if (usuario == null)
                throw new Exception("No existe el usuario a eliminar");

            this.conexion!.Entry(usuario).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return "El usuario a sido eliminado de manera exitosa";
        }
    }
}
