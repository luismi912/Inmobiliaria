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

        public int ConsultarPorCedula(string cedula)
        {
            var empleado = this.conexion!.EmpleadosSectores.FirstOrDefault(c => c.Cedula == cedula);

            if (empleado == null)
                throw new Exception("No se encontro ninguna persona con esa cedula");

            var auditoria = new Auditorias()
            {
                TipoAccion = "Consultar por cedula",
                Entidad = "Empleados",
                IdEntidad = empleado.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se consulto un registro de empleados con id {empleado.Id}" +
                              $"\nCon cedula {empleado.Cedula}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return empleado.Id;
        }

        public string Eliminar(EmpleadosSectores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.EmpleadosSectores.Remove(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "DELETE",
                Entidad = "Empleados",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se elimino un registro de empleados con id {entidad.Id}" +
                              $"\nCon cedula {entidad.Cedula}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public EmpleadosSectores Modificar(EmpleadosSectores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "MODIFICAR",
                Entidad = "Empleados",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se modifico un registro de empleados con id {entidad.Id}" +
                              $"\nCon cedula {entidad.Cedula}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }

        public EmpleadosSectores Guardar(CrearUsuariosEmpleadosDtos dto)
        {
            //CREAMOS EL USUARIO DEL ADMIN
            var usuario = new UsuariosRoles()
            {
                Correo = dto.Correo,
                Contraseña = dto.Contraseña,
                Rol = "Empleado"
            };

            this.conexion!.UsuariosRoles.Add(usuario);
            this.conexion.SaveChanges();

            //CREAMOS AL ADMIN ENTIDAD PRINCIPAL 
            var empleado = new EmpleadosSectores()
            {
                Cedula = dto.Empleado.Cedula,
                Nombre = dto.Empleado.Nombre,
                Apellido = dto.Empleado.Apellido,
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
                Numero = dto.Empleado.Direccion.Numero,
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

            var auditoria = new Auditorias()
            {
                TipoAccion = "INSERT",
                Entidad = "CrearEmpleadoDto",
                IdEntidad = empleado.Id,
                Fecha = DateTime.Now,
                Descripcion = $"id en el Usuario de {usuario.Id}" +
                              $"\nSe creo al administrador con id {empleado.Id}" +
                              $"\nCon id en el telefono de {telefono.Id}" +
                              $"\nCon id en la direccion de {direccion.Id}" +
                              $"\nCon id en el expediente de {expediente.Id}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return empleado;
        }

        public EmpleadosSectores ConsultarUsuario(int Id)
        {
            var empleado = this.conexion!.EmpleadosSectores
                .Include(a => a._UsuarioRol)
                .FirstOrDefault(a => a._UsuarioRol!.Id == Id);

            if (empleado == null)
                throw new Exception("Hubo un error con su usuario, reintente de nuevo");

            return empleado!;
        }
    }
}
