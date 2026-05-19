using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuditoriasController : ControllerBase
    {
        private IAuditoriasNegocio? IAuditoriasnegocio { get; set; }

        public AuditoriasController()
        {
            this.IAuditoriasnegocio = new AuditoriasNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<Auditorias> Consultar()
        {
            if (this.IAuditoriasnegocio == null)
                throw new Exception("No implementado");
            return this.IAuditoriasnegocio!.Consultar();
        }
    }
}
