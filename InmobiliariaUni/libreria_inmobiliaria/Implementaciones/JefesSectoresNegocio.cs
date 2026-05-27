using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class JefesSectoresNegocio : IJefesSectoresNegocio
    {
        private IConexion? conexion { get; set; }

        public JefesSectoresNegocio ()
        {
            this.conexion = new Conexion();
            this.conexion!.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<JefesSectores> Consultar()
        {
            var lista = this.conexion!.JefesSectores.ToList();
            return lista;
        }

        public JefesSectores Modificar(JefesSectores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encuentro ningun registro con ese valor");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "MODIFICAR",
                Entidad = "Jefes",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se modifico un registro de jefes con id {entidad.Id}" +
                              $"\nCon la cedula {entidad.Cedula}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }

        public string Eliminar(JefesSectores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.JefesSectores.Remove(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "DELETE",
                Entidad = "Jefes",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se elimino un registro de jefes con id {entidad.Id}" +
                              $"\nCon la cedula {entidad.Cedula}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public JefesSectores Guardar(CrearUsuariosJefesDtos dto)
        {
            var usuario = this.conexion!.UsuariosRoles.FirstOrDefault(u => u.Correo == dto.Correo);

            if (usuario != null)
                return null!;

            usuario = new UsuariosRoles()
            {
                Correo = dto.Correo,
                Contraseña = dto.Contraseña,
                Rol = dto.Rol
            };

            this.conexion!.UsuariosRoles.Add(usuario);
            this.conexion.SaveChanges();

            var jefe = new JefesSectores()
            {
                Cedula = dto.Jefe.Cedula,
                Nombre = dto.Jefe.Nombre,
                Apellido = dto.Jefe.Apellido,
                FechaNacimiento = dto.Jefe.FechaNacimiento,
                FechaRegistro = dto.Jefe.FechaRegistro,
                Estado = dto.Jefe.Estado,
                HorarioTrabajo = dto.Jefe.HorarioTrabajo,
                Sueldo = dto.Jefe.Sueldo,
                Sector = dto.Jefe.Sector,
                AdministradorSector = dto.Jefe.Administrador,
                Nacionalidad = dto.Jefe.Nacionalidad,
                UsuarioRol = usuario.Id,
            };

            this.conexion!.JefesSectores.Add(jefe);
            this.conexion.SaveChanges(); 

            var direccion = new Direcciones
            {
                TipoVia = dto.Jefe.Direccion.TipoVia,
                Numero = dto.Jefe.Direccion.Numero,
                Complemento = dto.Jefe.Direccion.Complemento,
                Ciudad = dto.Jefe.Direccion.Ciudad,
                Persona = jefe.Id
            };

            this.conexion!.Direcciones.Add(direccion);

            var telefono = new Telefonos
            {
                Numero = dto.Jefe.Telefono.Numero,
                Prefijo = dto.Jefe.Telefono.Prefijo,
                Persona = jefe.Id
            };

            this.conexion.Telefonos.Add(telefono);

            //EXPEDIENTE
            var expediente = new ExpedientesLaborales()
            {
                FechaIngreso = dto.Jefe.Expediente.FechaIngreso,
                Cargo = dto.Jefe.Expediente.Cargo,
                Antiguedad = dto.Jefe.Expediente.Antiguedad,
                EstadoLaboral = dto.Jefe.Expediente.EstadoLaboral,
                Persona = jefe.Id
            };

            this.conexion.ExpedientesLaborales.Add(expediente);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "INSERT",
                Entidad = "CrearJefeDto",
                IdEntidad = jefe.Id,
                Fecha = DateTime.Now,
                Descripcion = $"id en el Usuario de {usuario.Id}" +
                              $"\nSe creo al administrador con id {jefe.Id}" +
                              $"\nCon id en el telefono de {telefono.Id}" +
                              $"\nCon id en la direccion de {direccion.Id}" +
                              $"\nCon id en el expediente de {expediente.Id}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return jefe;
        }

        public JefesSectores ConsultarUsuario(int Id)
        {
            var jefe = this.conexion!.JefesSectores
                .Include(a => a._UsuarioRol)
                .FirstOrDefault(a => a._UsuarioRol!.Id == Id);

            if (jefe == null)
                throw new Exception("Hubo un error con su usuario, reintente de nuevo");

            return jefe!;
        }
    }
}
