using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AdministradoresDepartamentosController : ControllerBase
    {
        private IAdministradoresDepartamentosNegocio? IAdministradoresDepartamentosnegocio { get; set; }

        public AdministradoresDepartamentosController()
        {
            this.IAdministradoresDepartamentosnegocio = new AdministradoresDepartamentosNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpPost]
        public string Guardar(CrearUsuarioAdministradorDto admin)
        {
            if (this.IAdministradoresDepartamentosnegocio == null)
                throw new Exception("No implementado");
            return this.IAdministradoresDepartamentosnegocio!.Guardar(admin);
        }

        [HttpGet]
        public List<AdministradoresDepartamentos> Consultar()
        {
            if (this.IAdministradoresDepartamentosnegocio == null)
                throw new Exception("No implementado");
            return this.IAdministradoresDepartamentosnegocio!.Consultar();
        }

        [HttpDelete("{Id}")]
        public string Eliminar(String Cedula)
        {
            if (this.IAdministradoresDepartamentosnegocio == null)
                throw new Exception("No implementado");
            return this.IAdministradoresDepartamentosnegocio!.Eliminar(Cedula);
        }

        [HttpPut]
        public AdministradoresDepartamentos Modificar(AdministradoresDepartamentos entidad)
        {
            if (this.IAdministradoresDepartamentosnegocio == null)
                throw new Exception("No implementado");
            return this.IAdministradoresDepartamentosnegocio!.Modificar(entidad);
        }
    }
}
