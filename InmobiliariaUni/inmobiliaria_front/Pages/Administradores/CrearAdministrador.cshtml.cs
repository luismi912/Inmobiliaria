using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inmobiliaria_front.Pages
{
    public class CrearAdministradorModel : PageModel
    {
        private INacionalidadesNegocio? INacionalidadesnegocio { get; set; }
        private ICiudadesNegocio? ICiudadesnegocio { get; set; }
        private IDepartamentosNegocio? IDepartamentosnegocio { get; set; }
        private IAdministradoresDepartamentosNegocio? IAdministradoresDepartamentosnegocio { get; set; }
        private IUsuarioRolesNegocio? IUsuarioRolesnegocio { get; set; }

        //El usuario llena un campo en el formulario y se necesita ese valor en el servidor
        [BindProperty] public CrearUsuariosAdministradoresDtos? AdminDto { get; set; }
        public List<Nacionalidades>? Nacionalidades { get; set; }
        public List<Ciudades>? Ciudades { get; set; }
        public List<Departamentos>? Departamentos { get; set; }

        public CrearAdministradorModel()
        {
            INacionalidadesnegocio = new NacionalidadesNegocio();
            ICiudadesnegocio = new CiudadesNegocio();
            IDepartamentosnegocio = new DepartamentosNegocio();
            IUsuarioRolesnegocio = new UsuariosRolesNegocio();
            IAdministradoresDepartamentosnegocio = new AdministradoresDepartamentosNegocio();
        }

        public void OnGet()
        {
            Refrescar();
        }

        public void Refrescar()
        {
            Nacionalidades = INacionalidadesnegocio!.Consultar();
            Ciudades = ICiudadesnegocio!.Consultar();
            Departamentos = IDepartamentosnegocio!.Consultar();
            AdminDto = new CrearUsuariosAdministradoresDtos()
            {
                Administrador = new AdministradoresDtos()
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
                var correo = IUsuarioRolesnegocio!.ConsultarCorreo(AdminDto!.Correo!);
                if (correo == AdminDto.Correo)
                    throw new Exception("El correo que intentas crear ya existe");
                IAdministradoresDepartamentosnegocio!.Guardar(AdminDto!);
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
