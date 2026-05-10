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

        public string Eliminar(string cedula)
        {
            var empleado = this.conexion!.EmpleadosSectores!.FirstOrDefault(p => p.Cedula == cedula);

            if (empleado == null)
                return "No exsite la entidad a eliminar";

            this.conexion.EmpleadosSectores.Remove(empleado);
            this.conexion.SaveChanges();

            return "Se elimino al empleado correctamente";
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
                Cedula = dto.EmpleadoDto.Cedula,
                PrimerNombre = dto.EmpleadoDto.PrimerNombre,
                PrimerApellido = dto.EmpleadoDto.PrimerApellido,
                FechaNacimiento = dto.EmpleadoDto.FechaNacimiento,
                FechaRegistro = dto.EmpleadoDto.FechaRegistro,
                Estado = dto.EmpleadoDto.Estado,
                HorarioTrabajo = dto.EmpleadoDto.HorarioTrabajo,
                Sueldo = dto.EmpleadoDto.Sueldo,
                Sector = dto.EmpleadoDto.Sector,
                JefeSector = dto.EmpleadoDto.JefeSector,
                Nacionalidad = dto.EmpleadoDto.Nacionalidad,
                Genero = dto.EmpleadoDto.Genero,
                UsuarioRol = usuario.Id,
            };

            this.conexion!.EmpleadosSectores.Add(empleado);
            this.conexion.SaveChanges();   //Guardamos cambios para generar el id y utilizarlo en las otras entidades

            // DIRECCIÓNES
            var direccion = new Direcciones
            {
                TipoVia = dto.EmpleadoDto.Direccion.TipoVia,
                NumeroVia = dto.EmpleadoDto.Direccion.NumeroVia,
                Complemento = dto.EmpleadoDto.Direccion.Complemento,
                Ciudad = dto.EmpleadoDto.Direccion.Ciudad,
                Persona = empleado.Id
            };

            this.conexion!.Direcciones.Add(direccion);

            //TELÉFONOS
            var telefono = new Telefonos
            {
                Numero = dto.EmpleadoDto.Telefono.Numero,
                Prefijo = dto.EmpleadoDto.Telefono.Prefijo,
                Persona = empleado.Id
            };

            this.conexion.Telefonos.Add(telefono);
            this.conexion.SaveChanges();

            return empleado;
        }
    }
}
