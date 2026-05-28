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

        public int ConsultarPorCedula(string cedula)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = $"https://localhost:7165/Clientes/ConsultarPorCedula/{cedula}";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                throw new Exception("Hubo un error con la cedula, reintente por favor");

            return Convert.ToInt32(respuesta["Valor"]);
        }

        public string Eliminar(Clientes entidad)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Clientes/Eliminar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return "No se logro concretar la eliminacion, intenlo de nuevo o mas tarde";

            return respuesta["Valor"].ToString()!;
        }

        public Clientes Guardar(ClientesDtos clienteDto)
        {
            if (clienteDto == null)
                throw new ArgumentNullException("La entidad no puede ser nula");

            if (string.IsNullOrWhiteSpace(clienteDto.Nombre))
                throw new Exception("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(clienteDto.Apellido))
                throw new Exception("El correo es obligatorio");

            if (string.IsNullOrWhiteSpace(clienteDto.Cedula))
                throw new Exception("La contraseña es obligatoria");

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
            if (entidad.Id == 0)
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
