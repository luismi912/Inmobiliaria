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

        public string Eliminar(Codeudores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.Codeudores.Remove(entidad);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
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
            var codeudor = new Codeudores()
            {
                Cedula = dto.Cedula,
                PrimerNombre = dto.PrimerNombre,
                PrimerApellido = dto.PrimerApellido,
                FechaNacimiento = dto.FechaNacimiento,
                FechaRegistro = dto.FechaRegistro,
                Estado = dto.Estado,
                IngresosMensuales = dto.IngresosMensuales,
                Comprador = dto.Comprador,
                Nacionalidad = dto.Nacionalidad
            };

            this.conexion!.Codeudores.Add(codeudor);
            this.conexion.SaveChanges();   //Guardamos cambios para generar el id y utilizarlo en las otras entidades

            // DIRECCIÓNES
            var direccion = new Direcciones
            {
                TipoVia = dto.Direccion.TipoVia,
                NumeroVia = dto.Direccion.NumeroVia,
                Complemento = dto.Direccion.Complemento,
                Ciudad = dto.Direccion.Ciudad,
                Persona = codeudor.Id
            };

            this.conexion!.Direcciones.Add(direccion);

            //TELÉFONOS
            var telefono = new Telefonos
            {
                Numero = dto.Telefono.Numero,
                Prefijo = dto.Telefono.Prefijo,
                Persona = codeudor.Id
            };

            this.conexion.Telefonos.Add(telefono);
            this.conexion.SaveChanges();

            //RESPALDO CodeudorES
            var respaldo = new RespaldosCodeudores()
            {
                Codeudor = codeudor.Id
            };

            this.conexion.RespaldosCodeudores.Add(respaldo);
            this.conexion.SaveChanges();

            //BIENES
            var bien = new Bienes()
            {
                Nombre = dto.RespaldoCodeudor.Bien.Nombre,
                Descripcion = dto.RespaldoCodeudor.Bien.Descripcion,
                FechaAdquisicion = dto.RespaldoCodeudor.Bien.FechaAdquisicion,
                ValorAdquisicion = dto.RespaldoCodeudor.Bien.ValorAdquisicion,
                ValorActual = dto.RespaldoCodeudor.Bien.ValorActual,
                RespaldoFinanciero = respaldo.Id,
            };
            this.conexion.Bienes!.Add(bien);

            //ACTIVOFINANCIERO
            var Activo = new ActivosFinancieros()
            {
                Nombre = dto.RespaldoCodeudor.ActivoFinanciero.Nombre,
                Descripcion = dto.RespaldoCodeudor.ActivoFinanciero.Descripcion,
                FechaAdquisicion = dto.RespaldoCodeudor.ActivoFinanciero.FechaAdquisicion,
                Valor = dto.RespaldoCodeudor.ActivoFinanciero.Valor,
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
                Persona = codeudor.Id
            };

            this.conexion.ExpedientesLaborales!.Add(expediente);
            this.conexion.SaveChanges();

            return codeudor;
        }
    }
}
