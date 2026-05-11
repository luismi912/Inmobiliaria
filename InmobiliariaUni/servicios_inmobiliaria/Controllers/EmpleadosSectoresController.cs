using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmpleadosSectoresController : ControllerBase
    {
        private IEmpleadosSectoresNegocio? IEmpleadosSectoresnegocio { get; set; }

        public EmpleadosSectoresController()
        {
            this.IEmpleadosSectoresnegocio = new EmpleadosSectoresNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpPost]
        public EmpleadosSectores Guardar(CrearUsuariosEmpleadosDtos dto)
        {
            if (this.IEmpleadosSectoresnegocio == null)
                throw new Exception("No implementado");
            return this.IEmpleadosSectoresnegocio!.Guardar(dto);
        }

        [HttpGet]
        public List<EmpleadosSectores> Consultar()
        {
            if (this.IEmpleadosSectoresnegocio == null)
                throw new Exception("No implementado");
            return this.IEmpleadosSectoresnegocio!.Consultar();
        }

        [HttpPut]
        public EmpleadosSectores Modificar(EmpleadosSectores entidad)
        {
            if (this.IEmpleadosSectoresnegocio == null)
                throw new Exception("No implementado");
            return this.IEmpleadosSectoresnegocio!.Modificar(entidad);
        }

        [HttpDelete]
        public string Eliminar(EmpleadosSectores entidad)
        {
            if (this.IEmpleadosSectoresnegocio == null)
                throw new Exception("No implementado");
            return this.IEmpleadosSectoresnegocio!.Eliminar(entidad);
        }
    }
}
