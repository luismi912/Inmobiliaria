using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class EntidadesApoyoUT
    {
        private IConexion? conexion { get; set; }
        private Departamentos? departamento { get; set; }
        private UsuariosRoles? usuarioRol { get; set; }
        private Ciudades? ciudad { get; set; }
        private Sectores? sector { get; set; }
        private Nacionalidades? nacionalidad { get; set; }
        private RespaldosCodeudores? respaldo { get; set; }
        private Compradores? comprador { get; set; }
        private Codeudores? codeudor { get; set; }
        private AdministradoresDepartamentos? admin { get; set; }
        private JefesSectores? jefe { get; set; }
        private EmpleadosSectores? empleado { get; set; }
        private Clientes? cliente { get; set; }
        private Personas? persona { get; set; }
        private TiposPropiedades? tipoPropiedad { get; set; }
        private Propiedades? propiedad { get; set; }

        public EntidadesApoyoUT()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public Departamentos GuardarDepartamento()
        {
            //CREAR DEPARTAMENTO AUXILIAR
            this.departamento = new Departamentos()
            {
                Nombre = "Antioquia",
                Estado = true
            };

            this.conexion!.Departamentos.Add(this.departamento);
            this.conexion!.SaveChanges();

            return departamento;
        }

        //COMO LA CIUDAD DEPENDE DE DEPARTAMENTO PRIMERO UTILIZAMOS EL METODO DE DEPARTAMENTOS Y
        //PARA LA CIUDAD LE ENVIAMOS EL DEPARTAMENTO
        public Ciudades GuardarCiudad(Departamentos departamento)
        {
            this.ciudad = new Ciudades()
            {
                Nombre = "Medellin",
                Poblacion = 232323232,
                FechaCreacion = DateTime.Now,
                CodigoPostal = "+57",
                Estado = true,
                Departamento = departamento.Id
            };

            this.conexion!.Ciudades.Add(this.ciudad!);
            this.conexion!.SaveChanges();

            return ciudad;
        }

        public Nacionalidades GuardarNacionalidad()
        {
            //NACIONALIDAD AUXILIAR
            this.nacionalidad = new Nacionalidades()
            {
                Nombre = "Argentina",
                Estado = true
            };

            this.conexion!.Nacionalidades.Add(this.nacionalidad!);
            this.conexion!.SaveChanges();

            return nacionalidad;
        }

        public Compradores GuardarComprador(Nacionalidades nacionalidad)
        {
            //LE ENVIAMOS LA MISMA NACIONALIDAD QUE EL CODEUDOR PARA UNA PRUEBA MAS SENCILLA PERO CON LO NECESARIO
            this.comprador = new Compradores()
            {
                Cedula = "1017928809",
                Nombre = "Luis fernando",
                Apellido = "Arango Martinez",
                FechaNacimiento = DateTime.Now,
                FechaRegistro = DateTime.Now,
                Estado = true,
                PresupuestoMaximo = 350000000,
                Nacionalidad = nacionalidad!.Id
            };

            this.conexion!.Compradores.Add(this.comprador);
            this.conexion!.SaveChanges();

            return this.comprador;
        }

        public Codeudores GuardarCodeudor(Compradores comprador)
        {
            this.codeudor = new Codeudores()
            {
                Cedula = "1017928809",
                Nombre = "Luis fernando",
                Apellido = "Arango Martinez",
                FechaNacimiento = DateTime.Now,
                FechaRegistro = DateTime.Now,
                Estado = true,
                Nacionalidad = comprador._Nacionalidad!.Id,
                Comprador = comprador.Id
            };
            this.conexion!.Codeudores.Add(this.codeudor!);
            this.conexion!.SaveChanges();

            return this.codeudor;
        }

        public RespaldosCodeudores GuardarRespaldoCodeudor(Codeudores codeudor)
        {
            this.respaldo = new RespaldosCodeudores()
            {
                Codeudor = codeudor.Id,
                IngresosMensuales = 7500000,
                DeudasTotales = 350000,
                Observaciones = "El comprador esta en un estado, adminitible en su respaldo primordial"
            };

            this.conexion!.RespaldosCodeudores.Add(respaldo);
            this.conexion!.SaveChanges();

            return this.respaldo;
        }

        public UsuariosRoles GuardarUsuariosRoles()
        {
            this.usuarioRol = new UsuariosRoles()
            {
                Correo = "luismisito321@gmail.com",
                Contraseña = "luismi123",
                Rol = "empleado"
            };

            this.conexion!.UsuariosRoles.Add(usuarioRol);
            this.conexion!.SaveChanges();

            return usuarioRol;
        }

        public Sectores GuardarSector(Ciudades ciudad)
        {
            this.sector = new Sectores()
            {
                Nombre = "Inmobirapi",
                Estado = true,
                Ciudad = ciudad.Id
            };

            this.conexion!.Sectores.Add(sector);
            this.conexion!.SaveChanges();

            return sector;
        }

        public AdministradoresDepartamentos GuardarAdministrador(Departamentos departamento, 
            Nacionalidades nacionalidad, UsuariosRoles usuarioRol)
        {
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

            return admin;
        }

        public JefesSectores GuardarJefe(Sectores sector, Nacionalidades nacionalidad, UsuariosRoles usuarioRol, 
            AdministradoresDepartamentos admin)
        {
            this.jefe = new JefesSectores()
            {
                Cedula = "1017929281",
                Nombre = "luis alfonso",
                Apellido = "martinez lopez",
                FechaNacimiento = DateTime.Now,
                FechaRegistro = DateTime.Now,
                Estado = true,
                Nacionalidad = nacionalidad.Id,
                PresupuestoSector = 150000000,
                HorarioTrabajo = "6am a 4pm",
                Sueldo = 4000000,
                Sector = sector.Id,
                AdministradorSector = admin.Id,
                UsuarioRol = usuarioRol.Id
            };

            this.conexion!.JefesSectores.Add(jefe);
            this.conexion!.SaveChanges();

            return jefe;
        }

        public EmpleadosSectores GuardarEmpleado(Nacionalidades nacionalidad, Sectores sector, UsuariosRoles usuarioRol, 
            JefesSectores jefe)
        {
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

            return empleado;
        }

        public Personas GuardarPersona(Nacionalidades nacionalidad)
        {
            this.persona = new Personas()
            {
                Cedula = "10101023213",
                Nombre = "andres felipe",
                Apellido = "darlinton mosquer",
                FechaNacimiento = DateTime.Now,
                FechaRegistro = DateTime.Now,
                Estado = true,
                Nacionalidad = nacionalidad.Id
            };

            this.conexion!.Personas.Add(persona);
            this.conexion!.SaveChanges();

            return persona;
        }

        public Clientes GuardarCliente(EmpleadosSectores empleado, Nacionalidades nacionalidad)
        {
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

            return cliente;
        }

        public TiposPropiedades GuardarTipoPropiedad()
        {
            this.tipoPropiedad = new TiposPropiedades()
            {
                Nombre = "Apartamento",
                Estado = true
            };

            this.conexion!.TiposPropiedades.Add(tipoPropiedad);
            this.conexion!.SaveChanges();

            return tipoPropiedad;
        }

        public Propiedades GuardarPropiedad(Clientes cliente,TiposPropiedades tipoPropiedad,Sectores sector)
        {
            this.propiedad = new Propiedades()
            {
                Codigo = "POE-213",
                NumeroHabitaciones = 3,
                NumeroBaños = 4,
                Patio = true,
                Garaje = true,
                Pisos = 4,
                FechaConstruccion = DateTime.Now,
                ValorPropiedad = 350000000,
                ValorArriendo = 1000000,
                Disponible = true,
                Cliente = cliente.Id,
                TipoPropiedad = tipoPropiedad.Id,
                Sector = sector.Id
            };

            this.conexion!.Propiedades.Add(propiedad);
            this.conexion!.SaveChanges();

            return propiedad;
        }
    }
}
