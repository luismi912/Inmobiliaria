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
            if (entidad.Id == 0)
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

        public Compradores Guardar(CompradoresDtos dto)
        {

            var Comprador = new Compradores()
            {
                Cedula = dto.Cedula,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                FechaNacimiento = dto.FechaNacimiento,
                FechaRegistro = dto.FechaRegistro,
                Estado = dto.Estado,
                PresupuestoMaximo = dto.PresupuestoMaximo,
                Nacionalidad = dto.Nacionalidad,
            };

            // DIRECCIÓNES
            var direccion = new Direcciones
            {
                TipoVia = dto.Direccion.TipoVia,
                Numero = dto.Direccion.Numero,
                Complemento = dto.Direccion.Complemento,
                Ciudad = dto.Direccion.Ciudad,
                Persona = Comprador.Id
            };

            this.conexion!.Direcciones.Add(direccion);

            //TELÉFONOS
            var telefono = new Telefonos
            {
                Numero = dto.Telefono.Numero,
                Prefijo = dto.Telefono.Prefijo,
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
                Nombre = dto.RespaldoComprador.Bien.Nombre,
                Descripcion = dto.RespaldoComprador.Bien.Descripcion,
                FechaAdquisicion = dto.RespaldoComprador.Bien.FechaAdquisicion,
                ValorAdquisicion = dto.RespaldoComprador.Bien.ValorAdquisicion,
                ValorActual = dto.RespaldoComprador.Bien.ValorActual,
                RespaldoFinanciero = respaldo.Id,
            };
            this.conexion.Bienes!.Add(bien);

            //ACTIVOFINANCIERO
            var Activo = new ActivosFinancieros()
            {
                Nombre = dto.RespaldoComprador.ActivoFinanciero.Nombre,
                Descripcion = dto.RespaldoComprador.ActivoFinanciero.Descripcion,
                FechaAdquisicion = dto.RespaldoComprador.ActivoFinanciero.FechaAdquisicion,
                Valor = dto.RespaldoComprador.ActivoFinanciero.Valor,
                RespaldoFinanciero = respaldo.Id,
            };

            this.conexion.ActivosFinancieros!.Add(Activo);

            //EXPEDIENTE
            var expediente = new ExpedientesLaborales()
            {
                FechaIngreso = dto.Expediente.FechaIngreso,
                Cargo = dto.Expediente.Cargo,
                Antiguedad = dto.Expediente.Antiguedad,
                EstadoLaboral = dto.Expediente.EstadoLaboral,
                Persona = Comprador.Id
            };

            this.conexion.ExpedientesLaborales!.Add(expediente);
            this.conexion.SaveChanges();

            return Comprador;
        }
    }
}
