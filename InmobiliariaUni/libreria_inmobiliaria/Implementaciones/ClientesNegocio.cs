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

            this.conexion!.ClienteesDepartamentos.Add(cliente);
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
            this.conexion.SaveChanges();

            return cliente;
        }
    }
}
