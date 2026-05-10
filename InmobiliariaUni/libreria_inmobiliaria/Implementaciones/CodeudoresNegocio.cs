using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace libreria_inmobiliaria.Implementaciones
{
    public class CodeudoresNegocio : ICodeudoresNegocio
    {
        private IConexion? conexion { get; set; }

        public CodeudoresNegocio()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<Codeudores> Consultar()
        {
            var Lista = this.conexion!.Codeudores!.ToList();
            return Lista;
        }

        public string Eliminar(string cedula)
        {
            var codeudor = this.conexion!.Codeudores!.FirstOrDefault(p => p.Cedula == cedula);

            if (codeudor == null)
                return "No exsite la entidad a eliminar";

            this.conexion.Codeudores.Remove(codeudor);
            this.conexion.SaveChanges();

            return "Se elimino al empleado correctamente";
        }

        public Codeudores Modificar(Codeudores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }

        public Codeudores Guardar(CrearUsuariosCodeudoresDtos dto)
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

            var Codeudor = new Codeudores()
            {
                Cedula = dto.CodeudorDto.Cedula,
                PrimerNombre = dto.CodeudorDto.PrimerNombre,
                PrimerApellido = dto.CodeudorDto.PrimerApellido,
                FechaNacimiento = dto.CodeudorDto.FechaNacimiento,
                FechaRegistro = dto.CodeudorDto.FechaRegistro,
                Estado = dto.CodeudorDto.Estado,
                IngresosMensuales = dto.CodeudorDto.IngresosMensuales,
                Comprador = dto.CodeudorDto.Comprador,
                Nacionalidad = dto.CodeudorDto.Nacionalidad,
                Genero = dto.CodeudorDto.Genero,
                UsuarioRol = usuario.Id,
            };

            this.conexion!.Codeudores.Add(Codeudor);
            this.conexion.SaveChanges();   //Guardamos cambios para generar el id y utilizarlo en las otras entidades

            // DIRECCIÓNES
            var direccion = new Direcciones
            {
                TipoVia = dto.CodeudorDto.Direccion.TipoVia,
                NumeroVia = dto.CodeudorDto.Direccion.NumeroVia,
                Complemento = dto.CodeudorDto.Direccion.Complemento,
                Ciudad = dto.CodeudorDto.Direccion.Ciudad,
                Persona = Codeudor.Id
            };

            this.conexion!.Direcciones.Add(direccion);

            //TELÉFONOS
            var telefono = new Telefonos
            {
                Numero = dto.CodeudorDto.Telefono.Numero,
                Prefijo = dto.CodeudorDto.Telefono.Prefijo,
                Persona = Codeudor.Id
            };

            this.conexion.Telefonos.Add(telefono);
            this.conexion.SaveChanges();

            //RESPALDO CodeudorES
            var respaldo = new RespaldosCodeudores()
            {
                Codeudor = Codeudor.Id
            };

            this.conexion.RespaldosCodeudores.Add(respaldo);
            this.conexion.SaveChanges();

            //BIENES
            var bien = new Bienes()
            {
                Nombre = dto.CodeudorDto.RespaldoCodeudorDto.BienDto.Nombre,
                Descripcion = dto.CodeudorDto.RespaldoCodeudorDto.BienDto.Descripcion,
                FechaAdquisicion = dto.CodeudorDto.RespaldoCodeudorDto.BienDto.FechaAdquisicion,
                ValorAdquisicion = dto.CodeudorDto.RespaldoCodeudorDto.BienDto.ValorAdquisicion,
                ValorActual = dto.CodeudorDto.RespaldoCodeudorDto.BienDto.ValorActual,
                RespaldoFinanciero = respaldo.Id,
            };
            this.conexion.Bienes!.Add(bien);

            //ACTIVOFINANCIERO
            var Activo = new ActivosFinancieros()
            {
                Nombre = dto.CodeudorDto.RespaldoCodeudorDto.ActivoFinancieroDto.Nombre,
                Descripcion = dto.CodeudorDto.RespaldoCodeudorDto.ActivoFinancieroDto.Descripcion,
                FechaAdquisicion = dto.CodeudorDto.RespaldoCodeudorDto.ActivoFinancieroDto.FechaAdquisicion,
                Valor = dto.CodeudorDto.RespaldoCodeudorDto.ActivoFinancieroDto.Valor,
                RespaldoFinanciero = respaldo.Id,
            };

            this.conexion.ActivosFinancieros!.Add(Activo);
            this.conexion.SaveChanges();

            return Codeudor;
        }
    }
}
