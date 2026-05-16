using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.interfaces;
using Newtonsoft.Json;

namespace libreria_presentaciones_inmobiliaria.implemtanciones
{
    public class BienesNegocio : IBienesNegocio
    {
        private IComunicaciones? iComunicaciones;

        public List<Bienes> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Bienes/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Bienes>();

            return JsonConvert.DeserializeObject<List<Bienes>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Bienes Eliminar(Bienes entidad)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Bienes/Eliminar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Bienes();

            return JsonConvert.DeserializeObject<Bienes>(
                respuesta["Valor"].ToString()!)!;
        }

        public Bienes Guardar(Bienes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Bienes/Guardar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarGuardar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Bienes();

            return JsonConvert.DeserializeObject<Bienes>(
                respuesta["Valor"].ToString()!)!;
        }

        public Bienes Modificar(Bienes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Bienes/Modificar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Bienes();

            return JsonConvert.DeserializeObject<Bienes>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
