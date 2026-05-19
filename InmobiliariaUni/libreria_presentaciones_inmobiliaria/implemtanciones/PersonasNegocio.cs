using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.interfaces;
using Newtonsoft.Json;

namespace libreria_presentaciones_inmobiliaria.implemtanciones
{
    public class PersonasNegocio : IPersonasNegocio
    {
        private IComunicaciones? iComunicaciones;

        public List<Personas> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Personas/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Personas>();

            return JsonConvert.DeserializeObject<List<Personas>>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
