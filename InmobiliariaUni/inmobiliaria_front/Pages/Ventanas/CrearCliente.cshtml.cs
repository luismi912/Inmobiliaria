using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inmobiliaria_front.Pages.Ventanas
{
    [Authorize(Roles = "Empleado")]
    public class CrearClienteModel : PageModel
    {
        private INacionalidadesNegocio? INacionalidadesnegocio { get; set; }
        private ICiudadesNegocio? ICiudadesnegocio { get; set; }
        private IClientesNegocio? IClientesnegocio { get; set; }
        private IEmpleadosSectoresNegocio? IEmpleadosSectoresnegocio { get; set; }

        //El usuario llena un campo en el formulario y se necesita ese valor en el servidor
        [BindProperty] public ClientesDtos? ClienteDto { get; set; }
        public List<Nacionalidades>? Nacionalidades { get; set; }
        public List<Ciudades>? Ciudades { get; set; }

        public CrearClienteModel()
        {
            INacionalidadesnegocio = new NacionalidadesNegocio();
            ICiudadesnegocio = new CiudadesNegocio();
            IClientesnegocio = new ClientesNegocio();
            IEmpleadosSectoresnegocio = new EmpleadosSectoresNegocio();
        }

        public void OnGet()
        {
            Refrescar();
        }

        public void Refrescar()
        {
            Nacionalidades = INacionalidadesnegocio!.Consultar();
            Ciudades = ICiudadesnegocio!.Consultar();
            ClienteDto = new ClientesDtos()
            {
                Estado = true,
                FechaNacimiento = DateTime.Now,
                FechaRegistro = DateTime.Now,
                Expediente = new ExpedientesLaborales() { FechaIngreso = DateTime.Now },
            };
        }

        public void OnPostBtEnviar()
        {
            try
            {
                var id = IEmpleadosSectoresnegocio!.ConsultarPorCedula(ClienteDto!.CedulaRelacionEmpleado!);
                ClienteDto.Empleado = id;
                IClientesnegocio!.Guardar(ClienteDto!);
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
