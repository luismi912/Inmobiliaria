using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.interfaces;
using Newtonsoft.Json;

namespace libreria_presentaciones_inmobiliaria.implemtanciones
{
    public class JefesSectoresNegocio : IJefesSectoresNegocio
    {
        private IComunicaciones? iComunicaciones;

        public List<JefesSectores> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/JefesSectores/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<JefesSectores>();

            return JsonConvert.DeserializeObject<List<JefesSectores>>(
                respuesta["Valor"].ToString()!)!;
        }

        public string Eliminar(JefesSectores entidad)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/JefesSectores/Eliminar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return "No se logro concretar la eliminacion, intenlo de nuevo o mas tarde";

            return respuesta["Valor"].ToString()!;
        }

        public JefesSectores Guardar(CrearUsuariosJefesDtos jefeDto)
        {
            if (jefeDto == null)
                throw new ArgumentNullException("La entidad no puede ser nula");

            if (string.IsNullOrWhiteSpace(jefeDto.Jefe.PrimerNombre))
                throw new Exception("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(jefeDto.Correo))
                throw new Exception("El correo es obligatorio");

            if (string.IsNullOrWhiteSpace(jefeDto.Contraseña))
                throw new Exception("La contraseña es obligatoria");

            if (!jefeDto.Correo!.Contains("@"))
                throw new Exception("El correo no tiene un formato válido");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/JefesSectores/Guardar";
            datos["Entidad"] = jefeDto;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarGuardar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new JefesSectores();

            return JsonConvert.DeserializeObject<JefesSectores>(
                respuesta["Valor"].ToString()!)!;
        }

        public JefesSectores Modificar(JefesSectores entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/JefesSectores/Modificar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new JefesSectores();

            return JsonConvert.DeserializeObject<JefesSectores>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
