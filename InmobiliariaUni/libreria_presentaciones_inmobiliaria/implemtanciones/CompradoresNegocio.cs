using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.interfaces;
using Newtonsoft.Json;

namespace libreria_presentaciones_inmobiliaria.implemtanciones
{
    public class CompradoresNegocio : ICompradoresNegocio
    {
        private IComunicaciones? iComunicaciones;

        public List<Compradores> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Compradores/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Compradores>();

            return JsonConvert.DeserializeObject<List<Compradores>>(
                respuesta["Valor"].ToString()!)!;
        }

        public string Eliminar(Compradores entidad)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Compradores/Eliminar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return "No se logro concretar la eliminacion, intenlo de nuevo o mas tarde";

            return respuesta["Valor"].ToString()!;
        }

        public Compradores Guardar(CrearUsuariosCompradoresDtos compradoresDto)
        {
            if (compradoresDto == null)
                throw new ArgumentNullException("La entidad no puede ser nula");

            if (string.IsNullOrWhiteSpace(compradoresDto.Comprador.PrimerNombre))
                throw new Exception("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(compradoresDto.Comprador.PrimerApellido))
                throw new Exception("El correo es obligatorio");

            if (string.IsNullOrWhiteSpace(compradoresDto.Correo))
                throw new Exception("La contraseña es obligatoria");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Compradores/Guardar";
            datos["Entidad"] = compradoresDto;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarGuardar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Compradores();

            return JsonConvert.DeserializeObject<Compradores>(
                respuesta["Valor"].ToString()!)!;
        }

        public Compradores Modificar(Compradores entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Compradores/Modificar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Compradores();

            return JsonConvert.DeserializeObject<Compradores>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
