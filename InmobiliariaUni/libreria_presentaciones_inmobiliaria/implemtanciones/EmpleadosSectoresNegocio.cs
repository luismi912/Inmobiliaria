using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.interfaces;
using Newtonsoft.Json;

namespace libreria_presentaciones_inmobiliaria.implemtanciones
{
    public class EmpleadosSectoresNegocio : IEmpleadosSectoresNegocio
    {
        private IComunicaciones? iComunicaciones;

        public List<EmpleadosSectores> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/EmpleadosSectores/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<EmpleadosSectores>();

            return JsonConvert.DeserializeObject<List<EmpleadosSectores>>(
                respuesta["Valor"].ToString()!)!;
        }

        public string Eliminar(EmpleadosSectores entidad)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/EmpleadosSectores/Eliminar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return "No se logro concretar la eliminacion, intenlo de nuevo o mas tarde";

            return respuesta["Valor"].ToString()!;
        }

        public EmpleadosSectores Guardar(CrearUsuariosEmpleadosDtos empleadoDto)
        {
            if (empleadoDto == null)
                throw new ArgumentNullException("La entidad no puede ser nula");

            if (string.IsNullOrWhiteSpace(empleadoDto.Empleado.PrimerNombre))
                throw new Exception("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(empleadoDto.Correo))
                throw new Exception("El correo es obligatorio");

            if (string.IsNullOrWhiteSpace(empleadoDto.Contraseña))
                throw new Exception("La contraseña es obligatoria");

            if (!empleadoDto.Correo!.Contains("@"))
                throw new Exception("El correo no tiene un formato válido");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/EmpleadosSectores/Guardar";
            datos["Entidad"] = empleadoDto;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarGuardar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new EmpleadosSectores();

            return JsonConvert.DeserializeObject<EmpleadosSectores>(
                respuesta["Valor"].ToString()!)!;
        }

        public EmpleadosSectores Modificar(EmpleadosSectores entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/EmpleadosSectores/Modificar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new EmpleadosSectores();

            return JsonConvert.DeserializeObject<EmpleadosSectores>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
