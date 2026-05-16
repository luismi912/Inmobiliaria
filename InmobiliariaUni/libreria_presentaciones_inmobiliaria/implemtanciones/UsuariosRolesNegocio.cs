using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.interfaces;
using Newtonsoft.Json;

namespace libreria_presentaciones_inmobiliaria.implemtanciones
{
    public class UsuariosRolesNegocioNegocio : IUsuarioRolesNegocio
    {
        private IComunicaciones? iComunicaciones;

        public List<UsuarioRoles> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/UsuarioRoles/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<UsuarioRoles>();

            return JsonConvert.DeserializeObject<List<UsuarioRoles>>(
                respuesta["Valor"].ToString()!)!;
        }

        public string Eliminar(UsuarioRoles entidad)
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

        public UsuarioRoles Modificar(UsuarioRoles entidad)
        {
            if (entidad.Id != 0)
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
                return new UsuarioRoles();

            return JsonConvert.DeserializeObject<UsuarioRoles>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
