using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class JefesSectoresController : ControllerBase
    {
        private IJefesSectoresNegocio? IJefesSectoresnegocio { get; set; }

        public JefesSectoresController()
        {
            this.IJefesSectoresnegocio = new JefesSectoresNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpPost]
        public JefesSectores Guardar(CrearUsuariosJefesDtos dto)
        {
            if (this.IJefesSectoresnegocio == null)
                throw new Exception("No implementado");
            return this.IJefesSectoresnegocio!.Guardar(dto);
        }

        [HttpGet]
        public List<JefesSectores> Consultar()
        {
            if (this.IJefesSectoresnegocio == null)
                throw new Exception("No implementado");
            return this.IJefesSectoresnegocio!.Consultar();
        }

        [HttpDelete]
        public string Eliminar(JefesSectores entidad)
        {
            if (this.IJefesSectoresnegocio == null)
                throw new Exception("No implementado");
            return this.IJefesSectoresnegocio!.Eliminar(entidad);
        }

        [HttpPut]
        public JefesSectores Modificar(JefesSectores entidad)
        {
            if (this.IJefesSectoresnegocio == null)
                throw new Exception("No implementado");
            return this.IJefesSectoresnegocio!.Modificar(entidad);
        }
    }
}
