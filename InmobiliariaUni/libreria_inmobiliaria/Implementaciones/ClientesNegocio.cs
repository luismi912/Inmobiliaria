using libreria_inmobiliaria.crearDTOS;
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

        public string Eliminar(Clientes entidad)
        {
            if (entidad == null)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.Clientes.Remove(entidad);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public Clientes Modificar(Clientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }

        public Clientes Guardar(CrearUsuariosClientesDtos dto)
        {
            var usuario = this.conexion!.UsuariosRoles.FirstOrDefault(u => u.Correo == dto.Correo);

            if (usuario != null)
                return null!;

            //CREAMOS EL USUARIO DEL ADMIN
            usuario = new UsuarioRoles()
            {
                Correo = dto.Correo,
                Contraseña = dto.Contraseña,
                Rol = dto.Rol
            };

            this.conexion!.UsuariosRoles.Add(usuario);
            this.conexion.SaveChanges();

            //CREAMOS AL ADMIN ENTIDAD PRINCIPAL 
            var cliente = new Clientes()
            {
                Cedula = dto.Cliente.Cedula,
                PrimerNombre = dto.Cliente.PrimerNombre,
                PrimerApellido = dto.Cliente.PrimerApellido,
                FechaNacimiento = dto.Cliente.FechaNacimiento,
                FechaRegistro = dto.Cliente.FechaRegistro,
                Estado = dto.Cliente.Estado,
                PorcentajeComision = dto.Cliente.PorcentajeComision,
                Nacionalidad = dto.Cliente.Nacionalidad,
                Genero = dto.Cliente.Genero,
                UsuarioRol = usuario.Id,
            };

            this.conexion!.Clientes.Add(cliente);
            this.conexion.SaveChanges();   //Guardamos cambios para generar el id y utilizarlo en las otras entidades

            // DIRECCIÓNES
            var direccion = new Direcciones
            {
                TipoVia = dto.Cliente.Direccion.TipoVia,
                NumeroVia = dto.Cliente.Direccion.NumeroVia,
                Complemento = dto.Cliente.Direccion.Complemento,
                Ciudad = dto.Cliente.Direccion.Ciudad,
                Persona = cliente.Id
            };

            this.conexion!.Direcciones.Add(direccion);

            //TELÉFONOS
            var telefono = new Telefonos
            {
                Numero = dto.Cliente.Telefono.Numero,
                Prefijo = dto.Cliente.Telefono.Prefijo,
                Persona = cliente.Id
            };

            this.conexion.Telefonos.Add(telefono);

            //EXPEDIENTE
            var expediente = new ExpedientesLaborales()
            {
                FechaIngreso = dto.Cliente.Expediente.FechaIngreso,
                Cargo = dto.Cliente.Expediente.Cargo,
                Antiguedad = dto.Cliente.Expediente.Antiguedad,
                EstadoLaboral = dto.Cliente.Expediente.EstadoLaboral,
                Persona = cliente.Id
            };

            this.conexion.ExpedientesLaborales!.Add(expediente);
            this.conexion.SaveChanges();

            
            return cliente;
        }
    }
}
