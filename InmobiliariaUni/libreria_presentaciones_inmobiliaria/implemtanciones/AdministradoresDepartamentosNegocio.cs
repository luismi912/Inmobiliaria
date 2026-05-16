using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.interfaces;
using Newtonsoft.Json;

namespace libreria_presentaciones_inmobiliaria.implemtanciones
{
    public class AdministradoresDepartamentosNegocio : IAdministradoresDepartamentosNegocio
    {
        private IComunicaciones? iComunicaciones;

        public List<AdministradoresDepartamentos> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/AdministradoresDepartamentos/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<AdministradoresDepartamentos>();

            return JsonConvert.DeserializeObject<List<AdministradoresDepartamentos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public AdministradoresDepartamentos Eliminar(AdministradoresDepartamentos entidad)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/AdministradoresDepartamentos/Eliminar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new AdministradoresDepartamentos();

            return JsonConvert.DeserializeObject<AdministradoresDepartamentos>(
                respuesta["Valor"].ToString()!)!;
        }

        public AdministradoresDepartamentos Guardar(CrearUsuariosAdministradoresDtos adminDto)
        {
            if (adminDto == null)
                throw new ArgumentNullException("La entidad no puede ser nula");

            if (string.IsNullOrWhiteSpace(adminDto.Administrador.PrimerNombre))
                throw new Exception("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(adminDto.Correo))
                throw new Exception("El correo es obligatorio");

            if (string.IsNullOrWhiteSpace(adminDto.Contraseña))
                throw new Exception("La contraseña es obligatoria");

            if (!adminDto.Correo!.Contains("@"))
                throw new Exception("El correo no tiene un formato válido");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/AdministradoresDepartamentos/Guardar";
            datos["Entidad"] = adminDto;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarGuardar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new AdministradoresDepartamentos();

            return JsonConvert.DeserializeObject<AdministradoresDepartamentos>(
                respuesta["Valor"].ToString()!)!;
        }

        public AdministradoresDepartamentos Modificar(AdministradoresDepartamentos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/AdministradoresDepartamentos/Modificar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new AdministradoresDepartamentos();

            return JsonConvert.DeserializeObject<AdministradoresDepartamentos>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
