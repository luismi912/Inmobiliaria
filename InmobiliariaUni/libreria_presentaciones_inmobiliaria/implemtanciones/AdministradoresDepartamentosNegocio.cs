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

        public AdministradoresDepartamentos ConsultarUsuario(int Id)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = $"https://localhost:7165/AdministradoresDepartamentos/ConsultarUsuario/{Id}";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new AdministradoresDepartamentos();

            return JsonConvert.DeserializeObject<AdministradoresDepartamentos>(
                respuesta["Valor"].ToString()!)!;
        }

        public string Eliminar(AdministradoresDepartamentos entidad)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7165/AdministradoresDepartamentos/Eliminar";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return "No se logro concretar la eliminacion, intenlo de nuevo o mas tarde";

            return respuesta["Valor"].ToString()!;
        }

        public AdministradoresDepartamentos Guardar(CrearUsuariosAdministradoresDtos adminDto)
        {
            //validaciones de usuario
            if (string.IsNullOrWhiteSpace(adminDto.Correo))
                throw new Exception("El correo es obligatorio");

            if (string.IsNullOrWhiteSpace(adminDto.Contraseña))
                throw new Exception("La contraseña es obligatoria");

            //Validaciones de Administrador
            if (string.IsNullOrWhiteSpace(adminDto.Administrador.Cedula))
                throw new Exception("La cédula es obligatoria");

            if (string.IsNullOrWhiteSpace(adminDto.Administrador.Nombre))
                throw new Exception("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(adminDto.Administrador.Apellido))
                throw new Exception("El apellido es obligatorio");

            if (adminDto.Administrador.Departamento == 0)
                throw new Exception("El departamento es obligatorio");

            if (adminDto.Administrador.Nacionalidad == 0)
                throw new Exception("La nacionalidad es obligatoria");

            if (adminDto.Administrador.Sueldo <= 0)
                throw new Exception("El sueldo debe ser mayor a 0");

            //validacion de la direccion
            if (string.IsNullOrWhiteSpace(adminDto.Administrador.Direccion.TipoVia))
                throw new Exception("El tipo de vía es obligatorio");

            if (string.IsNullOrWhiteSpace(adminDto.Administrador.Direccion.Numero))
                throw new Exception("El número de dirección es obligatorio");

            if (adminDto.Administrador.Direccion.Ciudad == 0)
                throw new Exception("La ciudad es obligatoria");

            //validacion del telefono
            if (string.IsNullOrWhiteSpace(adminDto.Administrador.Telefono.Numero))
                throw new Exception("El número de teléfono es obligatorio");

            if (string.IsNullOrWhiteSpace(adminDto.Administrador.Telefono.Prefijo))
                throw new Exception("El prefijo del teléfono es obligatorio");

            //validacion del expediente
            if (adminDto.Administrador.Expediente.FechaIngreso == default)
                throw new Exception("La fecha de ingreso es obligatoria");

            if (string.IsNullOrWhiteSpace(adminDto.Administrador.Expediente.Cargo))
                throw new Exception("El cargo es obligatorio");

            if (adminDto.Administrador.Expediente.Antiguedad < 0)
                throw new Exception("La antigüedad no puede ser negativa");

            if (string.IsNullOrWhiteSpace(adminDto.Administrador.Expediente.EstadoLaboral))
                throw new Exception("El estado laboral es obligatorio");

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
            if (entidad.Id == 0)
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
