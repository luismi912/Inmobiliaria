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
            if (entidad == null)
                return "El administrador no existe";

            this.conexion!.AdministradoresDepartamentos.Remove(entidad);
            this.conexion.SaveChanges();

            return "Se elimino la Administrador correctamente";
        }

        public AdministradoresDepartamentos Modificar(AdministradoresDepartamentos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }

        public AdministradoresDepartamentos Guardar(CrearUsuariosAdministradoresDtos dto)
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
            var admin = new AdministradoresDepartamentos ()
            {
                Cedula = dto.Administrador.Cedula,
                PrimerNombre = dto.Administrador.PrimerNombre,
                PrimerApellido = dto.Administrador.PrimerApellido,
                FechaNacimiento = dto.Administrador.FechaNacimiento,
                FechaRegistro = dto.Administrador.FechaRegistro,
                Estado = dto.Administrador.Estado,
                HorarioTrabajo = dto.Administrador.HorarioTrabajo,
                Sueldo = dto.Administrador.Sueldo,
                PresupuestoDepartamento = dto.Administrador.PresupuestoDepartamento,
                Departamento = dto.Administrador.Departamento,
                Nacionalidad = dto.Administrador.Nacionalidad,
                Genero = dto.Administrador.Genero,
                UsuarioRol = usuario.Id,
            };

            this.conexion!.AdministradoresDepartamentos.Add(admin);
            this.conexion.SaveChanges();   //Guardamos cambios para generar el id y utilizarlo en las otras entidades

            // DIRECCIÓNES
            var direccion = new Direcciones ()
            {
                TipoVia = dto.Administrador.Direccion.TipoVia,
                NumeroVia = dto.Administrador.Direccion.NumeroVia,
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

            
            return admin;
        }
    }
}
