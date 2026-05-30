using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inmobiliaria_front.Pages
{
    [Authorize(Roles = "Empleado")]
    public class CrearCodeudorModel : PageModel
    {
        private IDepartamentosNegocio? IDepartamentosnegocio { get; set; }
        private INacionalidadesNegocio? INacionalidadesnegocio { get; set; }
        private ICiudadesNegocio? ICiudadesnegocio { get; set; }
        private ICodeudoresNegocio? ICodeudoresnegocio { get; set; }
        private ICompradoresNegocio? ICompradoresnegocio { get; set; }

        //El usuario llena un campo en el formulario y se necesita ese valor en el servidor
        [BindProperty] public CodeudoresDtos? CodeudorDto { get; set; }
        public List<Compradores>? compradores { get; set; }
        public List<Nacionalidades>? nacionalidades { get; set; }
        public List<Ciudades>? ciudades { get; set; }

        public CrearCodeudorModel()
        {
            INacionalidadesnegocio = new NacionalidadesNegocio();
            ICiudadesnegocio = new CiudadesNegocio();
            ICodeudoresnegocio = new CodeudoresNegocio();
            ICompradoresnegocio = new CompradoresNegocio(); 
        }

        public void OnGet()
        {
            Refrescar();
        }

        public void Refrescar()
        {
            compradores = ICompradoresnegocio!.Consultar();
            nacionalidades = INacionalidadesnegocio!.Consultar();
            ciudades = ICiudadesnegocio!.Consultar();
            CodeudorDto = new CodeudoresDtos()
            {
                Estado = true,
                FechaNacimiento = DateTime.Now,
                FechaRegistro = DateTime.Now,
                Expediente = new ExpedientesLaborales() { FechaIngreso = DateTime.Now },
                RespaldoCodeudor = new RespaldosCodeudoresDtos()
                {
                    Bien = new BienesDtos() { FechaAdquisicion = DateTime.Now },
                    ActivoFinanciero = new ActivosFinancierosDtos() { FechaAdquisicion = DateTime.Now }
                }
            };
        }

        public void OnPostBtEnviar()
        {
            try
            {
                ICodeudoresnegocio!.Guardar(CodeudorDto!);
                ViewData["Mensaje"] = "Se creó correctamente";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
            finally
            {
                Refrescar();
            }
        }
    }
}
