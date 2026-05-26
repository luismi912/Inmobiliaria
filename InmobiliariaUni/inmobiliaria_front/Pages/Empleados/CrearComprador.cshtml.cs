using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inmobiliaria_front.Pages
{
    public class CrearCompradorModel : PageModel
    {
        private INacionalidadesNegocio? INacionalidadesnegocio { get; set; }
        private ICiudadesNegocio? ICiudadesnegocio { get; set; }
        private ICompradoresNegocio? ICompradoresnegocio { get; set; }

        [BindProperty] public CompradoresDtos? CompradorDto { get; set; }
        public List<Nacionalidades>? Nacionalidades { get; set; }
        public List<Ciudades>? Ciudades { get; set; }

        public CrearCompradorModel()
        {
            INacionalidadesnegocio = new NacionalidadesNegocio();
            ICiudadesnegocio = new CiudadesNegocio();
            ICompradoresnegocio = new CompradoresNegocio();
        }

        public void OnGet()
        {
            Refrescar();
        }

        public void Refrescar()
        {
            Nacionalidades = INacionalidadesnegocio!.Consultar();
            Ciudades = ICiudadesnegocio!.Consultar();
            CompradorDto = new CompradoresDtos()
            {
                Estado = true,
                FechaNacimiento = DateTime.Now,
                FechaRegistro = DateTime.Now,
                Expediente = new ExpedientesLaborales() { FechaIngreso = DateTime.Now },
                RespaldoComprador = new RespaldosCompradoresDtos()
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
                ICompradoresnegocio!.Guardar(CompradorDto!);
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
