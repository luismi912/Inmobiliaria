using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PropiedadesController : ControllerBase
    {
        private IPropiedadesNegocio? IPropiedadesnegocio { get; set; }

        public PropiedadesController()
        {
            this.IPropiedadesnegocio = new PropiedadesNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<Propiedades> Consultar()
        {
            if (this.IPropiedadesnegocio == null)
                throw new Exception("No implementado");
            return this.IPropiedadesnegocio!.Consultar();
        }

        [HttpDelete]
        public string Eliminar(Propiedades entidad)
        {
            if (this.IPropiedadesnegocio == null)
                throw new Exception("No implementado");
            return this.IPropiedadesnegocio!.Eliminar(entidad);
        }

        [HttpPut]
        public Propiedades Modificar(Propiedades entidad)
        {
            if (this.IPropiedadesnegocio == null)
                throw new Exception("No implementado");
            return this.IPropiedadesnegocio!.Modificar(entidad);
        }

        [HttpGet]
        public Propiedades ConsultarConContrato(Contratos contrato)
        {
            if (this.IPropiedadesnegocio == null)
                throw new Exception("No implementado");
            return this.IPropiedadesnegocio!.ConsultarConContrato(contrato);
        }

        [HttpGet("{Id}")]
        public List<Propiedades> ConsultarSectorEmpleado(int Id)
        {
            if (this.IPropiedadesnegocio == null)
                throw new Exception("No implementado");
            return this.IPropiedadesnegocio!.ConsultarSectorEmpleado(Id);
        }

        [HttpGet("{Id}")]
        public List<Propiedades> ConsultarSectorJefe(int Id)
        {
            if (this.IPropiedadesnegocio == null)
                throw new Exception("No implementado");
            return this.IPropiedadesnegocio!.ConsultarSectorJefe(Id);
        }

        [HttpPost]
        public Propiedades Guardar(Propiedades entidad)
        {
            if (this.IPropiedadesnegocio == null)
                throw new Exception("No implementado");
            return this.IPropiedadesnegocio!.Guardar(entidad);
        }
    }
}
