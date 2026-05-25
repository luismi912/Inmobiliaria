using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class AdmnistradoresDepartamentosUT
    {
        private IConexion? conexion { get; set; }
        private AdministradoresDepartamentos? admin { get; set; }
        private EntidadesApoyoUT? apoyo { get; set; }
        private Departamentos? departamento { get; set; }
        private Nacionalidades? nacionalidad { get; set; }
        private UsuariosRoles? usuarioRol { get; set; }

        [TestMethod]
        public void Ejecutar()
        {
            Guardar();
            Consultar();
            Modificar();
            Eliminar();
        }

        public AdmnistradoresDepartamentosUT()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
            this.apoyo = new EntidadesApoyoUT();
        }

        private void Guardar()
        {
            this.departamento = apoyo!.GuardarDepartamento();
            this.usuarioRol = apoyo.GuardarUsuariosRoles();
            this.nacionalidad = apoyo.GuardarNacionalidad();

            this.admin = new AdministradoresDepartamentos()
            {
                Cedula = "1017929281",
                Nombre = "luis alfonso",
                Apellido = "martinez lopez",
                FechaNacimiento = DateTime.Now,
                FechaRegistro = DateTime.Now,
                Estado = true,
                Nacionalidad = nacionalidad.Id,
                PresupuestoDepartamento = 750000000,
                HorarioTrabajo = "6am a 4pm",
                Sueldo = 10000000,
                Departamento = departamento.Id,
                UsuarioRol = usuarioRol.Id
            };

            this.conexion!.AdministradoresDepartamentos.Add(admin);
            this.conexion!.SaveChanges();
        }

        private void Consultar()
        {
            var lista = this.conexion!.AdministradoresDepartamentos.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No hay forma de hacer consultas");
        }

        private void Modificar()
        {
            this.admin!.Sueldo = 9000000;
            this.conexion!.Entry(this.admin).State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        public void Eliminar()
        {
            this.conexion!.AdministradoresDepartamentos.Remove(this.admin!);
            this.conexion!.UsuariosRoles.Remove(this.usuarioRol!);
            this.conexion!.Nacionalidades.Remove(this.nacionalidad!);
            this.conexion!.Departamentos.Remove(this.departamento!);

            this.conexion.SaveChanges();
        }
    }
}
