using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BienesController : ControllerBase
    {
        private IBienesNegocio? IBienesnegocio { get; set; }

        public BienesController()
        {
            this.IBienesnegocio = new BienesNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<Bienes> Consultar()
        {
            if (this.IBienesnegocio == null)
                throw new Exception("No implementado");
            return this.IBienesnegocio!.Consultar();
        }

        [HttpDelete("{Id}")]
        public string Eliminar(int Id)
        {
            if (this.IBienesnegocio == null)
                throw new Exception("No implementado");
            return this.IBienesnegocio!.Eliminar(Id);
        }

        [HttpPut]
        public Bienes Modificar(Bienes entidad)
        {
            if (this.IBienesnegocio == null)
                throw new Exception("No implementado");
            return this.IBienesnegocio!.Modificar(entidad);
        }
    }
}
