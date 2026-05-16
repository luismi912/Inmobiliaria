using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class EmpleadosSectoresNegocio : IEmpleadosSectoresNegocio
    {
        private IConexion? conexion { get; set; }

        public EmpleadosSectoresNegocio()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<EmpleadosSectores> Consultar()
        {
            var Lista = this.conexion!.EmpleadosSectores!.ToList();
            return Lista;
        }

        public string Eliminar(EmpleadosSectores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.EmpleadosSectores.Remove(entidad);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public EmpleadosSectores Modificar(EmpleadosSectores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }

        public EmpleadosSectores Guardar(CrearUsuariosEmpleadosDtos dto)
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
            var empleado = new EmpleadosSectores()
            {
                Cedula = dto.Empleado.Cedula,
                PrimerNombre = dto.Empleado.PrimerNombre,
                PrimerApellido = dto.Empleado.PrimerApellido,
                FechaNacimiento = dto.Empleado.FechaNacimiento,
                FechaRegistro = dto.Empleado.FechaRegistro,
                Estado = dto.Empleado.Estado,
                HorarioTrabajo = dto.Empleado.HorarioTrabajo,
                Sueldo = dto.Empleado.Sueldo,
                Sector = dto.Empleado.Sector,
                JefeSector = dto.Empleado.JefeSector,
                Nacionalidad = dto.Empleado.Nacionalidad,
                UsuarioRol = usuario.Id,
            };

            this.conexion!.EmpleadosSectores.Add(empleado);
            this.conexion.SaveChanges();   //Guardamos cambios para generar el id y utilizarlo en las otras entidades

            // DIRECCIÓNES
            var direccion = new Direcciones
            {
                TipoVia = dto.Empleado.Direccion.TipoVia,
                NumeroVia = dto.Empleado.Direccion.NumeroVia,
                Complemento = dto.Empleado.Direccion.Complemento,
                Ciudad = dto.Empleado.Direccion.Ciudad,
                Persona = empleado.Id
            };

            this.conexion!.Direcciones.Add(direccion);

            //TELÉFONOS
            var telefono = new Telefonos
            {
                Numero = dto.Empleado.Telefono.Numero,
                Prefijo = dto.Empleado.Telefono.Prefijo,
                Persona = empleado.Id
            };

            this.conexion.Telefonos.Add(telefono);

            //EXPEDIENTE
            var expediente = new ExpedientesLaborales()
            {
                FechaIngreso = dto.Empleado.Expediente.FechaIngreso,
                Cargo = dto.Empleado.Expediente.Cargo,
                Antiguedad = dto.Empleado.Expediente.Antiguedad,
                EstadoLaboral = dto.Empleado.Expediente.EstadoLaboral,
                Persona = empleado.Id
            };

            this.conexion.ExpedientesLaborales.Add(expediente);
            this.conexion.SaveChanges();

            return empleado;
        }
    }
}
