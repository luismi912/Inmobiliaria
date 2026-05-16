using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.interfaces;
using Newtonsoft.Json;

namespace libreria_presentaciones_inmobiliaria.implemtanciones
{
    public class CodeudoresNegocio : ICodeudoresNegocio
    {
        private IComunicaciones? iComunicaciones;

        public List<Codeudores> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Codeudores/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Codeudores>();

            return JsonConvert.DeserializeObject<List<Codeudores>>(
                respuesta["Valor"].ToString()!)!;
        }

        public string Eliminar(Codeudores entidad)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Codeudores/Eliminar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return "No se logro concretar la eliminacion, intenlo de nuevo o mas tarde";

            return respuesta["Valor"].ToString()!;
        }

        public Codeudores Guardar(CrearUsuariosCodeudoresDtos codeudorDto)
        {
            if (codeudorDto == null)
                throw new ArgumentNullException("La entidad no puede ser nula");

            if (string.IsNullOrWhiteSpace(codeudorDto.PrimerNombre))
                throw new Exception("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(codeudorDto.Cedula))
                throw new Exception("El correo es obligatorio");

            if (string.IsNullOrWhiteSpace(codeudorDto.PrimerApellido))
                throw new Exception("La contraseña es obligatoria");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Codeudores/Guardar";
            datos["Entidad"] = codeudorDto;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarGuardar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Codeudores();

            return JsonConvert.DeserializeObject<Codeudores>(
                respuesta["Valor"].ToString()!)!;
        }

        public Codeudores Modificar(Codeudores entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/Codeudores/Modificar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Codeudores();

            return JsonConvert.DeserializeObject<Codeudores>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
