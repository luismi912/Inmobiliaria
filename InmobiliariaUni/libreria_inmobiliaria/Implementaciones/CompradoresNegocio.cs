using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class CompradoresNegocio : ICompradoresNegocio
    {
        private IConexion? conexion { get; set; }

        public CompradoresNegocio()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<Compradores> Consultar()
        {
            var Lista = this.conexion!.Compradores!.ToList();
            return Lista;
        }

        public string Eliminar(string cedula)
        {
            var admin = this.conexion!.Compradores!.FirstOrDefault(p => p.Cedula == cedula);

            if (admin == null)
                return "No exsite la entidad a eliminar";

            this.conexion.Compradores.Remove(admin);
            this.conexion.SaveChanges();

            return "Se elimino al Comprador correctamente";
        }

        public Compradores Modificar(Compradores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }

        public Compradores Guardar(CrearUsuariosCompradoresDtos dto)
        {
            var usuario = this.conexion!.UsuariosRoles.FirstOrDefault(u => u.Correo == dto.Correo);

            if (usuario != null)
                return null!;

            usuario = new UsuarioRoles()
            {
                Correo = dto.Correo,
                Contraseña = dto.Contraseña,
                Rol = dto.Rol
            };

            this.conexion!.UsuariosRoles.Add(usuario);
            this.conexion.SaveChanges();

            var Comprador = new Compradores()
            {
                Cedula = dto.CompradorDto.Cedula,
                PrimerNombre = dto.CompradorDto.PrimerNombre,
                PrimerApellido = dto.CompradorDto.PrimerApellido,
                FechaNacimiento = dto.CompradorDto.FechaNacimiento,
                FechaRegistro = dto.CompradorDto.FechaRegistro,
                Estado = dto.CompradorDto.Estado,
                PresupuestoMaximo = dto.CompradorDto.PresupuestoMaximo,
                Nacionalidad = dto.CompradorDto.Nacionalidad,
                Genero = dto.CompradorDto.Genero,
                UsuarioRol = usuario.Id,
            };

            this.conexion!.Compradores.Add(Comprador);
            this.conexion.SaveChanges();   //Guardamos cambios para generar el id y utilizarlo en las otras entidades

            // DIRECCIÓNES
            var direccion = new Direcciones
            {
                TipoVia = dto.CompradorDto.Direccion.TipoVia,
                NumeroVia = dto.CompradorDto.Direccion.NumeroVia,
                Complemento = dto.CompradorDto.Direccion.Complemento,
                Ciudad = dto.CompradorDto.Direccion.Ciudad,
                Persona = Comprador.Id
            };

            this.conexion!.Direcciones.Add(direccion);

            //TELÉFONOS
            var telefono = new Telefonos
            {
                Numero = dto.CompradorDto.Telefono.Numero,
                Prefijo = dto.CompradorDto.Telefono.Prefijo,
                Persona = Comprador.Id
            };

            this.conexion.Telefonos.Add(telefono);
            this.conexion.SaveChanges();

            //RESPALDO COMPRADORES
            var respaldo = new RespaldosCompradores()
            {
                Comprador = Comprador.Id
            };

            this.conexion.RespaldosCompradores.Add(respaldo);
            this.conexion.SaveChanges();

            //BIENES
            var bien = new Bienes()
            {
                Nombre = dto.CompradorDto.RespaldoComprador.Bien.Nombre,
                Descripcion = dto.CompradorDto.RespaldoComprador.Bien.Descripcion,
                FechaAdquisicion = dto.CompradorDto.RespaldoComprador.Bien.FechaAdquisicion,
                ValorAdquisicion = dto.CompradorDto.RespaldoComprador.Bien.ValorAdquisicion,
                ValorActual = dto.CompradorDto.RespaldoComprador.Bien.ValorActual,
                RespaldoFinanciero = respaldo.Id,
            };
            this.conexion.Bienes!.Add(bien);

            //ACTIVOFINANCIERO
            var Activo = new ActivosFinancieros()
            {
                Nombre = dto.CompradorDto.RespaldoComprador.ActivoFinanciero.Nombre,
                Descripcion = dto.CompradorDto.RespaldoComprador.ActivoFinanciero.Descripcion,
                FechaAdquisicion = dto.CompradorDto.RespaldoComprador.ActivoFinanciero.FechaAdquisicion,
                Valor = dto.CompradorDto.RespaldoComprador.ActivoFinanciero.Valor,
                RespaldoFinanciero = respaldo.Id,
            };

            this.conexion.ActivosFinancieros!.Add(Activo);
            this.conexion.SaveChanges();

            return Comprador;
        }
    }
}
