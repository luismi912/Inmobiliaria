using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.interfaces;
using Newtonsoft.Json;

namespace libreria_presentaciones_inmobiliaria.implemtanciones
{
    public class NacionalidadesNegocio : INacionalidadesNegocio
    {
        private IComunicaciones? iComunicaciones;

        public List<Nacionalidades> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Nacionalidades/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Nacionalidades>();

            return JsonConvert.DeserializeObject<List<Nacionalidades>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Nacionalidades Eliminar(Nacionalidades entidad)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Nacionalidades/Eliminar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Nacionalidades();

            return JsonConvert.DeserializeObject<Nacionalidades>(
                respuesta["Valor"].ToString()!)!;
        }

        public Nacionalidades Guardar(Nacionalidades entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Nacionalidades/Guardar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarGuardar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Nacionalidades();

            return JsonConvert.DeserializeObject<Nacionalidades>(
                respuesta["Valor"].ToString()!)!;
        }

        public Nacionalidades Modificar(Nacionalidades entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Nacionalidades/Modificar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Nacionalidades();

            return JsonConvert.DeserializeObject<Nacionalidades>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
