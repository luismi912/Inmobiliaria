using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class ContratosArriendosUT
    {
        private IConexion? conexion { get; set; }
        private EntidadesApoyoUT? apoyo { get; set; }
        private ContratosArriendos? contrato { get; set; }
        private Departamentos? departamento { get; set; }
        private Ciudades? ciudad { get; set; }
        private Sectores? sector { get; set; }
        private Nacionalidades? nacionalidad { get; set; }
        private UsuariosRoles? usuarioRol { get; set; }
        private AdministradoresDepartamentos? admin { get; set; }
        private JefesSectores? jefe { get; set; }
        private EmpleadosSectores? empleado { get; set; }
        private Clientes? cliente { get; set; }
        private Compradores? comprador { get; set; }
        private Codeudores? codeudor { get; set; }
        private Propiedades? propiedad { get; set; }
        private TiposPropiedades? tipoPropiedad { get; set; }

        public ContratosArriendosUT()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
            this.apoyo = new EntidadesApoyoUT();
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
            this.departamento = apoyo!.GuardarDepartamento();
            this.ciudad = apoyo!.GuardarCiudad(departamento);
            this.usuarioRol = apoyo.GuardarUsuariosRoles();
            this.nacionalidad = apoyo.GuardarNacionalidad();
            this.sector = apoyo.GuardarSector(ciudad);
            this.admin = apoyo.GuardarAdministrador(departamento, nacionalidad, usuarioRol);
            this.jefe = apoyo.GuardarJefe(sector, nacionalidad, usuarioRol, admin);
            this.empleado = apoyo.GuardarEmpleado(nacionalidad, sector, usuarioRol, jefe);
            this.cliente = apoyo.GuardarCliente(empleado, nacionalidad);
            this.comprador = apoyo.GuardarComprador(nacionalidad);
            this.codeudor = apoyo.GuardarCodeudor(comprador);
            this.tipoPropiedad = apoyo.GuardarTipoPropiedad();
            this.propiedad = apoyo.GuardarPropiedad(cliente, tipoPropiedad, sector);

            this.contrato = new ContratosArriendos()
            {
                FechaContrato = DateTime.Now,
                FechaFinalizacion = DateTime.Now.AddYears(1),
                Codeudor = codeudor.Id,
                Cliente = cliente.Id,
                Comprador = comprador.Id,
                EmpleadoSector = empleado.Id,
                Propiedad = propiedad.Id,
                ValorMensual = 1500000,
                DiaPago = 5,
                Renovable = true,
                Estado = "Pendiente"
            };

            this.conexion!.ContratosArriendos.Add(contrato);
            this.conexion!.SaveChanges();
        }

        private void Consultar()
        {
            var lista = this.conexion!.ContratosArriendos.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No hay forma de hacer consultas");
        }

        private void Modificar()
        {
            this.contrato!.ValorMensual = 1800000;
            this.conexion!.Entry(this.contrato).State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        public void Eliminar()
        {
            this.conexion!.ContratosArriendos.Remove(this.contrato!);
            this.conexion!.Propiedades.Remove(this.propiedad!);
            this.conexion!.TiposPropiedades.Remove(this.tipoPropiedad!);
            this.conexion!.Codeudores.Remove(this.codeudor!);
            this.conexion!.Compradores.Remove(this.comprador!);
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