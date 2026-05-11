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

        public string Eliminar(Compradores entidad)
        {
            if (entidad == null)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.Compradores.Remove(entidad);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
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
                Cedula = dto.Comprador.Cedula,
                PrimerNombre = dto.Comprador.PrimerNombre,
                PrimerApellido = dto.Comprador.PrimerApellido,
                FechaNacimiento = dto.Comprador.FechaNacimiento,
                FechaRegistro = dto.Comprador.FechaRegistro,
                Estado = dto.Comprador.Estado,
                PresupuestoMaximo = dto.Comprador.PresupuestoMaximo,
                Nacionalidad = dto.Comprador.Nacionalidad,
                Genero = dto.Comprador.Genero,
                UsuarioRol = usuario.Id,
            };

            this.conexion!.Compradores.Add(Comprador);
            this.conexion.SaveChanges();   //Guardamos cambios para generar el id y utilizarlo en las otras entidades

            // DIRECCIÓNES
            var direccion = new Direcciones
            {
                TipoVia = dto.Comprador.Direccion.TipoVia,
                NumeroVia = dto.Comprador.Direccion.NumeroVia,
                Complemento = dto.Comprador.Direccion.Complemento,
                Ciudad = dto.Comprador.Direccion.Ciudad,
                Persona = Comprador.Id
            };

            this.conexion!.Direcciones.Add(direccion);

            //TELÉFONOS
            var telefono = new Telefonos
            {
                Numero = dto.Comprador.Telefono.Numero,
                Prefijo = dto.Comprador.Telefono.Prefijo,
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
                Nombre = dto.Comprador.RespaldoComprador.Bien.Nombre,
                Descripcion = dto.Comprador.RespaldoComprador.Bien.Descripcion,
                FechaAdquisicion = dto.Comprador.RespaldoComprador.Bien.FechaAdquisicion,
                ValorAdquisicion = dto.Comprador.RespaldoComprador.Bien.ValorAdquisicion,
                ValorActual = dto.Comprador.RespaldoComprador.Bien.ValorActual,
                RespaldoFinanciero = respaldo.Id,
            };
            this.conexion.Bienes!.Add(bien);

            //ACTIVOFINANCIERO
            var Activo = new ActivosFinancieros()
            {
                Nombre = dto.Comprador.RespaldoComprador.ActivoFinanciero.Nombre,
                Descripcion = dto.Comprador.RespaldoComprador.ActivoFinanciero.Descripcion,
                FechaAdquisicion = dto.Comprador.RespaldoComprador.ActivoFinanciero.FechaAdquisicion,
                Valor = dto.Comprador.RespaldoComprador.ActivoFinanciero.Valor,
                RespaldoFinanciero = respaldo.Id,
            };

            this.conexion.ActivosFinancieros!.Add(Activo);

            //EXPEDIENTE
            var expediente = new ExpedientesLaborales()
            {
                FechaIngreso = dto.Comprador.Expediente.FechaIngreso,
                Cargo = dto.Comprador.Expediente.Cargo,
                Antiguedad = dto.Comprador.Expediente.Antiguedad,
                EstadoLaboral = dto.Comprador.Expediente.EstadoLaboral,
                Persona = Comprador.Id
            };

            this.conexion.ExpedientesLaborales!.Add(expediente);
            this.conexion.SaveChanges();

            return Comprador;
        }
    }
}
