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

            return entidad;
        }

        public string Eliminar(string cedula)
        {
            var jefe = this.conexion!.JefesSectores.FirstOrDefault(a => a.Cedula == cedula);

            if (jefe == null)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion.JefesSectores.Remove(jefe);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public string Guardar(CrearUsuariosJefesDtos dto)
        {
            var usuario = this.conexion!.UsuariosRoles.FirstOrDefault(u => u.Correo == dto.Correo);

            if (usuario != null)
                return "El usuario a crear ya existe ingrese otro correo por favor";

            usuario = new UsuarioRoles()
            {
                Correo = dto.Correo,
                Contraseña = dto.Contraseña
            };

            this.conexion!.UsuariosRoles.Add(usuario);
            this.conexion.SaveChanges();

            var Jefes = new JefesSectores()
            {
                Cedula = dto.JefeDto.Cedula,
                PrimerNombre = dto.JefeDto.PrimerNombre,
                PrimerApellido = dto.JefeDto.PrimerApellido,
                FechaNacimiento = dto.JefeDto.FechaNacimiento,
                FechaRegistro = dto.JefeDto.FechaRegistro,
                Estado = dto.JefeDto.Estado,
                HorarioTrabajo = dto.JefeDto.HorarioTrabajo,
                Sueldo = dto.JefeDto.Sueldo,
                Sector = dto.JefeDto.Sector,
                AdministradorSector = dto.JefeDto.AdministradorSector,
                Nacionalidad = dto.JefeDto.Nacionalidad,
                Genero = dto.JefeDto.Genero,
                UsuarioRol = usuario.Id,
            };

            this.conexion!.JefesSectores.Add(Jefes);
            this.conexion.SaveChanges(); 

            var direccion = new Direcciones
            {
                TipoVia = dto.JefeDto.Direccion.TipoVia,
                NumeroVia = dto.JefeDto.Direccion.NumeroVia,
                Complemento = dto.JefeDto.Direccion.Complemento,
                Ciudad = dto.JefeDto.Direccion.Ciudad,
                Persona = Jefes.Id
            };

            this.conexion!.Direcciones.Add(direccion);

            var telefono = new Telefonos
            {
                Numero = dto.JefeDto.Telefono.Numero,
                Prefijo = dto.JefeDto.Telefono.Prefijo,
                Persona = Jefes.Id
            };

            this.conexion.Telefonos.Add(telefono);
            this.conexion.SaveChanges();

            return $"Jefes creado correctamente con id: {Jefes.Id} \nCon correo {usuario.Correo}";
        }
    }
}
