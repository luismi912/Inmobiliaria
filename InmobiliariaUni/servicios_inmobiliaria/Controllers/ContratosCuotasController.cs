using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ContratosCuotasController : ControllerBase
    {
        private IContratosCuotasNegocio? IContratosCuotasnegocio { get; set; }

        public ContratosCuotasController()
        {
            this.IContratosCuotasnegocio = new ContratosCuotasNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<ContratosCuotas> Consultar()
        {
            if (this.IContratosCuotasnegocio == null)
                throw new Exception("No implementado");
            return this.IContratosCuotasnegocio.Consultar();
        }

        [HttpPost]
        public ContratosCuotas Guardar(ContratosCuotas entidad)
        {
            if (this.IContratosCuotasnegocio == null)
                throw new Exception("No implementado");
            return this.IContratosCuotasnegocio!.Guardar(entidad);
        }

        [HttpDelete("Id")]
        public string Eliminar(int Id)
        {
            if (this.IContratosCuotasnegocio == null)
                throw new Exception("No implementado");
            return this.IContratosCuotasnegocio!.Eliminar(Id);
        }

        [HttpPut]
        public ContratosCuotas Modificar(ContratosCuotas entidad)
        {
            if (this.IContratosCuotasnegocio == null)
                throw new Exception("No implementado");
            return this.IContratosCuotasnegocio!.Modificar(entidad);
        }
    }
}

