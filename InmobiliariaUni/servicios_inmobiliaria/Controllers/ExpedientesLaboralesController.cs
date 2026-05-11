using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ExpedientesLaboralesController : ControllerBase
    {
        private IExpedientesLaboralesNegocio? IExpedientesLaboralesnegocio { get; set; }

        public ExpedientesLaboralesController()
        {
            this.IExpedientesLaboralesnegocio = new ExpedientesLaboralesNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<ExpedientesLaborales> Consultar()
        {
            if (this.IExpedientesLaboralesnegocio == null)
                throw new Exception("No implementado");
            return this.IExpedientesLaboralesnegocio!.Consultar();
        }

        [HttpDelete]
        public string Eliminar(ExpedientesLaborales entidad)
        {
            if (this.IExpedientesLaboralesnegocio == null)
                throw new Exception("No implementado");
            return this.IExpedientesLaboralesnegocio!.Eliminar(entidad);
        }

        [HttpPut]
        public ExpedientesLaborales Modificar(ExpedientesLaborales entidad)
        {
            if (this.IExpedientesLaboralesnegocio == null)
                throw new Exception("No implementado");
            return this.IExpedientesLaboralesnegocio!.Modificar(entidad);
        }
    }
}
