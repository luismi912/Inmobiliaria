using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class UsuariosRolesUT
    {
        private IConexion? conexion { get; set; }
        private UsuariosRoles? usuarioRol { get; set; }

        public UsuariosRolesUT()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        [TestMethod]
        public void Ejecutar()
        {
            Guardar();
            Consultar();
            Modificar();
            Eliminar();
        }

        private void Guardar()
        {
            this.usuarioRol = new UsuariosRoles()
            {
                Correo = "luismifortnite@gmail.com",
                Contraseña = "Luismisito123",
                Rol = "AdministradorDepartamento"
            };

            this.conexion!.UsuariosRoles!.Add(this.usuarioRol);
            this.conexion!.SaveChanges();
        }

        private void Consultar()
        {
            var lista = this.conexion!.UsuariosRoles.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No hay forma de hacer consultas");
        }

        private void Modificar()
        {
            this.usuarioRol!.Correo = "Luismig6g@gmail.com";
            this.conexion!.Entry(this.usuarioRol).State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        public void Eliminar()
        {
            this.conexion!.UsuariosRoles.Remove(this.usuarioRol!);
            this.conexion.SaveChanges();
        }
    }
}
