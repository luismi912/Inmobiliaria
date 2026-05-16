using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.interfaces;
using Newtonsoft.Json;

namespace libreria_presentaciones_inmobiliaria.implemtanciones
{
    public class ActivosFinancierosNegocio : IActivosFinancierosNegocio
    {
        private IComunicaciones? iComunicaciones;

        public List<ActivosFinancieros> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/ActivosFinancieros/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<ActivosFinancieros>();

            return JsonConvert.DeserializeObject<List<ActivosFinancieros>>(
                respuesta["Valor"].ToString()!)!;
        }

        public ActivosFinancieros Eliminar(ActivosFinancieros entidad)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/ActivosFinancieros/Eliminar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new ActivosFinancieros();

            return JsonConvert.DeserializeObject<ActivosFinancieros>(
                respuesta["Valor"].ToString()!)!;
        }

        public ActivosFinancieros Guardar(ActivosFinancieros entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/ActivosFinancieros/Guardar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarGuardar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new ActivosFinancieros();

            return JsonConvert.DeserializeObject<ActivosFinancieros>(
                respuesta["Valor"].ToString()!)!;
        }

        public ActivosFinancieros Modificar(ActivosFinancieros entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/ActivosFinancieros/Modificar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new ActivosFinancieros();

            return JsonConvert.DeserializeObject<ActivosFinancieros>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
