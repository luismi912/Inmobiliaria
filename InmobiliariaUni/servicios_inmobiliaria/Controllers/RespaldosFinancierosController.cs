using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RespaldosFinancierosController : ControllerBase
    {
        private IRespaldosFinancierosNegocio? IRespaldosFinancierosnegocio { get; set; }

        public RespaldosFinancierosController()
        {
            this.IRespaldosFinancierosnegocio = new RespaldosFinancierosNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<RespaldosFinancieros> Consultar()
        {
            if (this.IRespaldosFinancierosnegocio == null)
                throw new Exception("No implementado");
            return this.IRespaldosFinancierosnegocio.Consultar();
        }
    }
}

