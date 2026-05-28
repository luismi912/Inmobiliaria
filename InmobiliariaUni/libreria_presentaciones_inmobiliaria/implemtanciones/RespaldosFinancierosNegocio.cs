using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.interfaces;
using Newtonsoft.Json;

namespace libreria_presentaciones_inmobiliaria.implemtanciones
{
    public class RespaldosFinancierosNegocio : IRespaldosFinancierosNegocio
    {
        private IComunicaciones? iComunicaciones;

        public List<RespaldosFinancieros> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/RespaldosFinancieros/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<RespaldosFinancieros>();

            return JsonConvert.DeserializeObject<List<RespaldosFinancieros>>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
