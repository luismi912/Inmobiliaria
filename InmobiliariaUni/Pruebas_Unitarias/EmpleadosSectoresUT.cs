using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class EmpleadosSectoresUT
    {
        private IConexion? conexion { get; set; }
        private EmpleadosSectores? empleado { get; set; }
        private JefesSectores? jefe { get; set; }
        private EntidadesApoyoUT? apoyo { get; set; }
        private Departamentos? departamento { get; set; }
        private Ciudades? ciudad { get; set; }
        private Sectores? sector { get; set; }
        private Nacionalidades? nacionalidad { get; set; }
        private UsuariosRoles? usuarioRol { get; set; }
        private AdministradoresDepartamentos? admin { get; set; }

        [TestMethod]
        public void Ejecutar()
        {
            Guardar();
            Consultar();
            Modificar();
            Eliminar();
        }

        public EmpleadosSectoresUT()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
            this.apoyo = new EntidadesApoyoUT();
        }

        private void Guardar()
        {
            this.departamento = apoyo!.GuardarDepartamento();
            this.ciudad = apoyo!.GuardarCiudad(departamento);
            this.usuarioRol = apoyo.GuardarUsuariosRoles();
            this.nacionalidad = apoyo.GuardarNacionalidad();
            this.sector = apoyo.GuardarSector(ciudad);
            this.admin = apoyo.GuardarAdministrador(departamento, nacionalidad, usuarioRol);
            this.jefe = apoyo.GuardarJefe(sector, nacionalidad, usuarioRol, admin);

            this.empleado = new EmpleadosSectores()
            {
                Cedula = "1017929281",
                Nombre = "luis alfonso",
                Apellido = "martinez lopez",
                FechaNacimiento = DateTime.Now,
                FechaRegistro = DateTime.Now,
                Estado = true,
                Nacionalidad = nacionalidad.Id,
                HorarioTrabajo = "6am a 4pm",
                Sueldo = 2000000,
                Sector = sector.Id,
                JefeSector = jefe.Id,
                UsuarioRol = usuarioRol.Id
            };

            this.conexion!.EmpleadosSectores.Add(empleado);
            this.conexion!.SaveChanges();
        }

        private void Consultar()
        {
            var lista = this.conexion!.EmpleadosSectores.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No hay forma de hacer consultas");
        }

        private void Modificar()
        {
            this.empleado!.Sueldo = 3000000;
            this.conexion!.Entry(this.empleado).State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        public void Eliminar()
        {
            this.conexion!.EmpleadosSectores.Remove(this.empleado!);
            this.conexion!.JefesSectores.Remove(this.jefe!);
            this.conexion!.Sectores.Remove(this.sector!);
            this.conexion!.Ciudades.Remove(this.ciudad!);
            this.conexion!.AdministradoresDepartamentos.Remove(this.admin!);
            this.conexion!.UsuariosRoles.Remove(this.usuarioRol!);
            this.conexion!.Nacionalidades.Remove(this.nacionalidad!);
            this.conexion!.Departamentos.Remove(this.departamento!);

            this.conexion.SaveChanges();
        }
    }
}
