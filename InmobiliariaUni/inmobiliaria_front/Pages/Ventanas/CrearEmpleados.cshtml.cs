using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inmobiliaria_front.Pages.Ventanas
{
    [Authorize(Roles = "Jefe")]
    public class CrearEmpleadosModel : PageModel
    {
        private IEmpleadosSectoresNegocio? IEmpleadosSectoresnegocio { get; set; }
        private ISectoresNegocio? ISectoresnegocio { get; set; }
        private IJefesSectoresNegocio? IJefesSectoresnegocio { get; set; }
        private INacionalidadesNegocio? INacionalidadesnegocio { get; set; }
        private ICiudadesNegocio? ICiudadesnegocio { get; set; }
        private IUsuarioRolesNegocio? IUsuarioRolesnegocio { get; set; }

        //El usuario llena un campo en el formulario y se necesita ese valor en el servidor
        [BindProperty] public CrearUsuariosEmpleadosDtos? EmpleadoDto { get; set; }
        public List<Nacionalidades>? Nacionalidades { get; set; }
        public List<Ciudades>? Ciudades { get; set; }
        public List<Sectores>? Sectores { get; set; }
        public List<JefesSectores>? jefes { get; set; }

        public CrearEmpleadosModel()
        {
            INacionalidadesnegocio = new NacionalidadesNegocio();
            ICiudadesnegocio = new CiudadesNegocio();
            IUsuarioRolesnegocio = new UsuariosRolesNegocio();
            IJefesSectoresnegocio = new JefesSectoresNegocio();
            IEmpleadosSectoresnegocio = new EmpleadosSectoresNegocio();
            ISectoresnegocio = new SectoresNegocio();
        }

        public void OnGet()
        {
            Refrescar();
        }

        public void Refrescar()
        {
            Nacionalidades = INacionalidadesnegocio!.Consultar();
            Ciudades = ICiudadesnegocio!.Consultar();
            Sectores = ISectoresnegocio!.Consultar();
            jefes = IJefesSectoresnegocio!.Consultar();
            EmpleadoDto = new CrearUsuariosEmpleadosDtos()
            {
                Empleado = new EmpleadosDtos()
                {
                   Estado = true,
                   FechaNacimiento = DateTime.Now,
                   FechaRegistro = DateTime.Now,
                   Expediente = new ExpedientesLaborales() { FechaIngreso = DateTime.Now },
                }
            };
        }

        public void OnPostBtEnviar()
        {
            try
            {
                var usuario = IUsuarioRolesnegocio!.ConsultarCorreo(EmpleadoDto!.Correo!);

                if (usuario != null && usuario.Correo == EmpleadoDto.Correo)
                    throw new Exception("El correo que intentas crear ya existe");
              

                IEmpleadosSectoresnegocio!.Guardar(EmpleadoDto!);
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
