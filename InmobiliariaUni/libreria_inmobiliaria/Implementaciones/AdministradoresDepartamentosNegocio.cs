using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class AdministradoresDepartamentosNegocio : IAdministradoresDepartamentosNegocio
    {
        private IConexion? conexion { get; set; }

        public AdministradoresDepartamentosNegocio ()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<AdministradoresDepartamentos> Consultar()
        {
            var Lista = this.conexion!.AdministradoresDepartamentos!.ToList();
            return Lista;
        }

        public string Eliminar(AdministradoresDepartamentos entidad)
        {
            if (entidad.Id == 0)
                return "El administrador no existe";

            this.conexion!.AdministradoresDepartamentos.Remove(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "DELETE",
                Entidad = "Administrador",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se elimino un registro de administradores con id {entidad.Id}" +
                              $"\nIdentificado con cedula {entidad.Nombre}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return "Se elimino la Administrador correctamente";
        }

        public AdministradoresDepartamentos Modificar(AdministradoresDepartamentos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "MODIFICAR",
                Entidad = "Administrador",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se modifico un registro de administradores con id {entidad.Id}" +
                              $"\nIdentificado con cedula {entidad.Cedula}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }

        public AdministradoresDepartamentos Guardar(CrearUsuariosAdministradoresDtos dto)
        {
            var usuario = this.conexion!.UsuariosRoles.FirstOrDefault(u => u.Correo == dto.Correo);

            if (usuario != null)
                return null!;

            //CREAMOS EL USUARIO DEL ADMIN
            usuario = new UsuariosRoles()
            {
                Correo = dto.Correo,
                Contraseña = dto.Contraseña,
                Rol = "Administrador"
            };

            this.conexion!.UsuariosRoles.Add(usuario);
            this.conexion.SaveChanges();

            //CREAMOS AL ADMIN ENTIDAD PRINCIPAL 
            var admin = new AdministradoresDepartamentos()
            {
                Cedula = dto.Administrador.Cedula,
                Nombre = dto.Administrador.Nombre,
                Apellido = dto.Administrador.Apellido,
                FechaNacimiento = dto.Administrador.FechaNacimiento,
                FechaRegistro = dto.Administrador.FechaRegistro,
                Estado = dto.Administrador.Estado,
                HorarioTrabajo = dto.Administrador.HorarioTrabajo,
                Sueldo = dto.Administrador.Sueldo,
                PresupuestoDepartamento = dto.Administrador.PresupuestoDepartamento,
                Departamento = dto.Administrador.Departamento,
                Nacionalidad = dto.Administrador.Nacionalidad,
                UsuarioRol = usuario.Id,
            };

            this.conexion!.AdministradoresDepartamentos.Add(admin);
            this.conexion.SaveChanges();   //Guardamos cambios para generar el id y utilizarlo en las otras entidades

            // DIRECCIÓNES
            var direccion = new Direcciones ()
            {
                TipoVia = dto.Administrador.Direccion.TipoVia,
                Numero = dto.Administrador.Direccion.Numero,
                Complemento = dto.Administrador.Direccion.Complemento,
                Ciudad = dto.Administrador.Direccion.Ciudad,
                Persona = admin.Id
            };

            this.conexion!.Direcciones.Add(direccion);

            //TELÉFONOS
            var telefono = new Telefonos ()
            {
                Numero = dto.Administrador.Telefono.Numero,
                Prefijo = dto.Administrador.Telefono.Prefijo,
                Persona = admin.Id
            };

            this.conexion.Telefonos.Add(telefono);

            //EXPEDIENTE
            var expediente = new ExpedientesLaborales()
            {
                FechaIngreso = dto.Administrador.Expediente.FechaIngreso,
                Cargo = dto.Administrador.Expediente.Cargo,
                Antiguedad = dto.Administrador.Expediente.Antiguedad,
                EstadoLaboral = dto.Administrador.Expediente.EstadoLaboral,
                Persona = admin.Id
            };

            this.conexion.ExpedientesLaborales!.Add(expediente);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "INSERT",
                Entidad = "CrearAdministradorDto",
                IdEntidad = admin.Id,
                Fecha = DateTime.Now,
                Descripcion = $"id en el Usuario de {usuario.Id}" + 
                              $"\nSe creo al administrador con id {admin.Id}" +
                              $"\nCon id en el telefono de {telefono.Id}" +
                              $"\nCon id en la direccion de {direccion.Id}" +
                              $"\nCon id en el expediente de {expediente.Id}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return admin;
        }

        public AdministradoresDepartamentos ConsultarUsuario(int Id)
        {
            var admin = this.conexion!.AdministradoresDepartamentos
                .Include(a => a._UsuarioRol)
                .FirstOrDefault(a => a._UsuarioRol!.Id == Id);

            if (admin == null)
                throw new Exception("Hubo un error con su usuario, reintente de nuevo");

            return admin!;
        }
    }
}
