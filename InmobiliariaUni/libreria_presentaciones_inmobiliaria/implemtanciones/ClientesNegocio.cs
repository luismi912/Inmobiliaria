using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.interfaces;
using Newtonsoft.Json;

namespace libreria_presentaciones_inmobiliaria.implemtanciones
{
    public class ClientesNegocio : IClientesNegocio
    {
        private IComunicaciones? iComunicaciones;

        public List<Clientes> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Clientes/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Clientes>();

            return JsonConvert.DeserializeObject<List<Clientes>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Clientes Eliminar(Clientes entidad)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Clientes/Eliminar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Clientes();

            return JsonConvert.DeserializeObject<Clientes>(
                respuesta["Valor"].ToString()!)!;
        }

        public Clientes Guardar(CrearUsuariosClientesDtos clienteDto)
        {
            if (clienteDto == null)
                throw new ArgumentNullException("La entidad no puede ser nula");

            if (string.IsNullOrWhiteSpace(clienteDto.Cliente.PrimerNombre))
                throw new Exception("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(clienteDto.Correo))
                throw new Exception("El correo es obligatorio");

            if (string.IsNullOrWhiteSpace(clienteDto.Contraseña))
                throw new Exception("La contraseña es obligatoria");

            if (!clienteDto.Correo!.Contains("@"))
                throw new Exception("El correo no tiene un formato válido");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Clientes/Guardar";
            datos["Entidad"] = clienteDto;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarGuardar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Clientes();

            return JsonConvert.DeserializeObject<Clientes>(
                respuesta["Valor"].ToString()!)!;
        }

        public Clientes Modificar(Clientes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Clientes/Modificar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Clientes();

            return JsonConvert.DeserializeObject<Clientes>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
