using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PersonasController : ControllerBase
    {
        private IPersonasNegocio? IPersonasnegocio { get; set; }

        public PersonasController()
        {
            this.IPersonasnegocio = new PersonasNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<Personas> Consultar()
        {
            if (this.IPersonasnegocio == null)
                throw new Exception("No implementado");
            return this.IPersonasnegocio!.Consultar();
        }
    }
}
