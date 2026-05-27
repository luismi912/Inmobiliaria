using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class ClientesNegocio : IClientesNegocio
    {
        private IConexion? conexion { get; set; }

        public ClientesNegocio()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public List<Clientes> Consultar()
        {
            var Lista = this.conexion!.Clientes!.ToList();
            return Lista;
        }

        public string Eliminar(Clientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.Clientes.Remove(entidad);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "DELETE",
                Entidad = "Clientes",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se elimino un registro de cliente con id {entidad.Id}" +
                              $"\nAl cliente con cedula {entidad.Cedula}"
            };

            return "La eliminacion se logro con exito";
        }

        public Clientes Modificar(Clientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();


            var auditoria = new Auditorias()
            {
                TipoAccion = "MODIFICAR",
                Entidad = "Clientes",
                IdEntidad = entidad.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se modifico un registro de cliente con id {entidad.Id}" +
                              $"\nAl cliente con cedula {entidad.Cedula}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return entidad;
        }

        public Clientes Guardar(ClientesDtos dto)
        {

            //CREAMOS AL ADMIN ENTIDAD PRINCIPAL 
            var cliente = new Clientes()
            {
                Cedula = dto.Cedula,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                FechaNacimiento = dto.FechaNacimiento,
                FechaRegistro = dto.FechaRegistro,
                Estado = dto.Estado,
                PorcentajeComision = dto.PorcentajeComision,
                Calificacion = dto.Calificacion,
                Nacionalidad = dto.Nacionalidad,
                EmpleadoSector = dto.Empleado
            };

            this.conexion!.Clientes.Add(cliente);
            this.conexion.SaveChanges();   //Guardamos cambios para generar el id y utilizarlo en las otras entidades

            // DIRECCIÓNES
            var direccion = new Direcciones
            {
                TipoVia = dto.Direccion.TipoVia,
                Numero = dto.Direccion.Numero,
                Complemento = dto.Direccion.Complemento,
                Ciudad = dto.Direccion.Ciudad,
                Persona = cliente.Id
            };

            this.conexion!.Direcciones.Add(direccion);

            //TELÉFONOS
            var telefono = new Telefonos
            {
                Numero = dto.Telefono.Numero,
                Prefijo = dto.Telefono.Prefijo,
                Persona = cliente.Id
            };

            this.conexion.Telefonos.Add(telefono);

            //EXPEDIENTE
            var expediente = new ExpedientesLaborales()
            {
                FechaIngreso = dto.Expediente.FechaIngreso,
                Cargo = dto.Expediente.Cargo,
                Antiguedad = dto.Expediente.Antiguedad,
                EstadoLaboral = dto.Expediente.EstadoLaboral,
                Persona = cliente.Id
            };

            this.conexion.ExpedientesLaborales!.Add(expediente);
            this.conexion.SaveChanges();

            var auditoria = new Auditorias()
            {
                TipoAccion = "INSERT",
                Entidad = "ClienteDto",
                IdEntidad = cliente.Id,
                Fecha = DateTime.Now,
                Descripcion = $"Se creo al cliente con id {cliente.Id}" +
                              $"\nCon id en el telefono de {telefono.Id}" +
                              $"\nCon id en la direccion de {direccion.Id}" +
                              $"\nCon id en el expediente de {expediente.Id}"
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();

            return cliente;
        }
    }
}
