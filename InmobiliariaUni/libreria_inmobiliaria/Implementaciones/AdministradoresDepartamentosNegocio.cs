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

        public string Eliminar(string cedula)
        {
            var admin = this.conexion!.AdministradoresDepartamentos!.FirstOrDefault(c => c.Cedula == cedula);

            if (admin == null)
                return "El administrador no existe";

            this.conexion.AdministradoresDepartamentos.Remove(admin);
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

        public string Guardar(CrearUsuarioAdministradorDto dto)
        {
            var usuario = this.conexion!.UsuariosRoles.FirstOrDefault(u => u.Correo == dto.Correo);

            if (usuario != null)
                return "El usuario a crear ya existe ingrese otro correo por favor";

            //CREAMOS EL USUARIO DEL ADMIN
            usuario = new UsuarioRoles()
            {
                Correo = dto.Correo,
                Contraseña = dto.Contraseña
            };

            this.conexion!.UsuariosRoles.Add(usuario);
            this.conexion.SaveChanges();

            //CREAMOS AL ADMIN ENTIDAD PRINCIPAL 
            var admin = new AdministradoresDepartamentos
            {
                Cedula = dto.AdministradorDto.Cedula,
                PrimerNombre = dto.AdministradorDto.PrimerNombre,
                PrimerApellido = dto.AdministradorDto.PrimerApellido,
                FechaNacimiento = dto.AdministradorDto.FechaNacimiento,
                FechaRegistro = dto.AdministradorDto.FechaRegistro,
                HorarioTrabajo = dto.AdministradorDto.HorarioTrabajo,
                PresupuestoDepartamento = dto.AdministradorDto.PresupuestoDepartamento,
                Estado = dto.AdministradorDto.Estado,
                UsuarioRol = usuario.Id,
                Nacionalidad = dto.AdministradorDto.Nacionalidad,
                EstadoCivil = dto.AdministradorDto.EstadoCivil,
                Genero = dto.AdministradorDto.Genero
            };

            this.conexion!.AdministradoresDepartamentos.Add(admin);
            this.conexion.SaveChanges();   //Guardamos cambios para generar el id y utilizarlo en las otras entidades

            // DIRECCIÓNES
            var direccion = new Direcciones
            {
                TipoVia = dto.AdministradorDto.Direccion.TipoVia,
                NumeroVia = dto.AdministradorDto.Direccion.NumeroVia,
                Complemento = dto.AdministradorDto.Direccion.Complemento,
                Ciudad = dto.AdministradorDto.Direccion.Ciudad,
                Persona = admin.Id
            };

            this.conexion!.Direcciones.Add(direccion);

            //TELÉFONOS
            var telefono = new Telefonos
            {
                Numero = dto.AdministradorDto.Telefono.Numero,
                Prefijo = dto.AdministradorDto.Telefono.Prefijo,
                Persona = admin.Id
            };

            this.conexion.Telefonos.Add(telefono);
            this.conexion.SaveChanges();

            return $"Admin creado correctamente con id: {admin.Id} \nCon correo {usuario.Correo}";
        }
    }
}
