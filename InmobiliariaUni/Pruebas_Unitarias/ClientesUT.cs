using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class ClientesUT
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
        private Clientes? cliente { get; set; }

        [TestMethod]
        public void Ejecutar()
        {
            Guardar();
            Consultar();
            Modificar();
            Eliminar();
        }

        public ClientesUT()
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
            this.empleado = apoyo.GuardarEmpleado(nacionalidad,sector,usuarioRol,jefe);

            this.cliente = new Clientes()
            {
                Cedula = "1017929281",
                Nombre = "luis alfonso",
                Apellido = "martinez lopez",
                FechaNacimiento = DateTime.Now,
                FechaRegistro = DateTime.Now,
                Estado = true,
                Nacionalidad = nacionalidad.Id,
                PorcentajeComision = 0.5m,
                Calificacion = 4,
                EmpleadoSector = empleado.Id
            };

            this.conexion!.Clientes.Add(cliente);
            this.conexion!.SaveChanges();
        }

        private void Consultar()
        {
            var lista = this.conexion!.Clientes.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No hay forma de hacer consultas");
        }

        private void Modificar()
        {
            this.cliente!.Calificacion = 5;
            this.conexion!.Entry(this. cliente).State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        public void Eliminar()
        {
            this.conexion!.Clientes.Remove(this.cliente!);
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
