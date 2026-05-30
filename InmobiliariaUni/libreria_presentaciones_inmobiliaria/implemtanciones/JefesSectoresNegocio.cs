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

        public JefesSectores ConsultarUsuario(int Id)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = $"https://localhost:7165/JefesSectores/ConsultarUsuario/{Id}";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new JefesSectores();

            return JsonConvert.DeserializeObject<JefesSectores>(
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
            //validaciones de usuario
            if (string.IsNullOrWhiteSpace(jefeDto.Correo))
                throw new Exception("El correo es obligatorio");

            if (string.IsNullOrWhiteSpace(jefeDto.Contraseña))
                throw new Exception("La contraseña es obligatoria");

            //Validaciones de Administrador
            if (string.IsNullOrWhiteSpace(jefeDto.Jefe.Cedula))
                throw new Exception("La cédula es obligatoria");

            if (string.IsNullOrWhiteSpace(jefeDto.Jefe.Nombre))
                throw new Exception("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(jefeDto.Jefe.Apellido))
                throw new Exception("El apellido es obligatorio");

            if (jefeDto.Jefe.Sector == 0)
                throw new Exception("El departamento es obligatorio");

            if (jefeDto.Jefe.Nacionalidad == 0)
                throw new Exception("La nacionalidad es obligatoria");

            if (jefeDto.Jefe.Sueldo <= 0)
                throw new Exception("El sueldo debe ser mayor a 0");

            //validacion de la direccion
            if (string.IsNullOrWhiteSpace(jefeDto.Jefe.Direccion.TipoVia))
                throw new Exception("El tipo de vía es obligatorio");

            if (string.IsNullOrWhiteSpace(jefeDto.Jefe.Direccion.Numero))
                throw new Exception("El número de dirección es obligatorio");

            if (jefeDto.Jefe.Direccion.Ciudad == 0)
                throw new Exception("La ciudad es obligatoria");

            //validacion del telefono
            if (string.IsNullOrWhiteSpace(jefeDto.Jefe.Telefono.Numero))
                throw new Exception("El número de teléfono es obligatorio");

            if (string.IsNullOrWhiteSpace(jefeDto.Jefe.Telefono.Prefijo))
                throw new Exception("El prefijo del teléfono es obligatorio");

            //validacion del expediente
            if (jefeDto.Jefe.Expediente.FechaIngreso == default)
                throw new Exception("La fecha de ingreso es obligatoria");

            if (string.IsNullOrWhiteSpace(jefeDto.Jefe.Expediente.Cargo))
                throw new Exception("El cargo es obligatorio");

            if (jefeDto.Jefe.Expediente.Antiguedad < 0)
                throw new Exception("La antigüedad no puede ser negativa");

            if (string.IsNullOrWhiteSpace(jefeDto.Jefe.Expediente.EstadoLaboral))
                throw new Exception("El estado laboral es obligatorio");

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
            if (entidad.Id == 0)
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
