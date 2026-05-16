using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.interfaces;
using Newtonsoft.Json;

namespace libreria_presentaciones_inmobiliaria.implemtanciones
{
    public class AuditoriasNegocio : IAuditoriasNegocio
    {
        private IComunicaciones? iComunicaciones;

        public List<Auditorias> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Auditorias/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Auditorias>();

            return JsonConvert.DeserializeObject<List<Auditorias>>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
