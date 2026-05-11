using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CodeudoresController : ControllerBase
    {
        private ICodeudoresNegocio? ICodeudoresnegocio { get; set; }

        public CodeudoresController()
        {
            this.ICodeudoresnegocio = new CodeudoresNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<Codeudores> Consultar()
        {
            if (this.ICodeudoresnegocio == null)
                throw new Exception("No implementado");
            return this.ICodeudoresnegocio!.Consultar();
        }

        [HttpPut]
        public Codeudores Modificar(Codeudores entidad)
        {
            if (this.ICodeudoresnegocio == null)
                throw new Exception("No implementado");
            return this.ICodeudoresnegocio!.Modificar(entidad);
        }

        [HttpDelete]
        public string Eliminar(Codeudores entidad)
        {
            if (this.ICodeudoresnegocio == null)
                throw new Exception("No implementado");
            return this.ICodeudoresnegocio!.Eliminar(entidad);
        }

        [HttpPost]
        public Codeudores Guardar(CrearUsuariosCodeudoresDtos dto)
        {
            if (this.ICodeudoresnegocio == null)
                throw new Exception("No implementado");
            return this.ICodeudoresnegocio!.Guardar(dto);
        }
    }
}
