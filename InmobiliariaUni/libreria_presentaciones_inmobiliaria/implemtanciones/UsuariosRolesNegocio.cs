using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.interfaces;
using Newtonsoft.Json;

namespace libreria_presentaciones_inmobiliaria.implemtanciones
{
    public class UsuariosRolesNegocio : IUsuarioRolesNegocio
    {
        private IComunicaciones? iComunicaciones;

        public List<UsuariosRoles> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/UsuarioRoles/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<UsuariosRoles>();

            return JsonConvert.DeserializeObject<List<UsuariosRoles>>(
                respuesta["Valor"].ToString()!)!;
        }

        public UsuariosRoles ConsultarCorreo(string correo)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = $"https://localhost:7165/UsuarioRoles/ConsultarCorreo/{correo}";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new UsuariosRoles();

            return JsonConvert.DeserializeObject<UsuariosRoles>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<UsuariosRoles> ConsultarCorreoAsync(string correo)
        {
            var datos = new Dictionary<string, object>();

            datos["Url"] = $"https://localhost:7165/UsuarioRoles/ConsultarCorreo/{correo}";

            this.iComunicaciones = new Comunicaciones();

            // 2. Usamos await en lugar de task wait y task.Result ya que al realizar una accion wait y await juntas estalla el programa
            var respuesta = await this.iComunicaciones.EjecutarConsultar(datos);

            if (respuesta == null || !respuesta.ContainsKey("Valor"))
                throw new Exception("Hubo un error con el correo, reintente por favor");

            return JsonConvert.DeserializeObject<UsuariosRoles>(
                respuesta["Valor"].ToString()!)!;
        }

        public string Eliminar(UsuariosRoles entidad)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/UsuarioRoles/Eliminar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return "No se logro concretar la eliminacion, intenlo de nuevo o mas tarde";

            return respuesta["Valor"].ToString()!;
        }

        public UsuariosRoles Modificar(UsuariosRoles entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/UsuarioRoles/Modificar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new UsuariosRoles();

            return JsonConvert.DeserializeObject<UsuariosRoles>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
