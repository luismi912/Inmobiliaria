using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inmobiliaria_front.Pages.Ventanas
{
    [Authorize(Roles = "Jefe")]
    public class EmpleadosModel : PageModel
    {
        private IEmpleadosSectoresNegocio? IEmpleadosSectoresnegocio { get; set; }
        private ISectoresNegocio? ISectoresnegocio { get; set; }
        private IJefesSectoresNegocio? IJefesSectoresnegocio { get; set; }
        private INacionalidadesNegocio? INacionalidadesnegocio { get; set; }

        public List<EmpleadosSectores>? empleados { get; set; }
        public List<JefesSectores>? jefes { get; set; }
        public List<Sectores>? sectores { get; set; }
        public List<Nacionalidades>? nacionalidades { get; set; }

        [BindProperty] public EmpleadosSectores? empleado { get; set; }
        public bool Borrando { get; set; }

        public EmpleadosModel()
        {
            IEmpleadosSectoresnegocio = new EmpleadosSectoresNegocio();
            ISectoresnegocio = new SectoresNegocio();
            IJefesSectoresnegocio = new JefesSectoresNegocio();
            INacionalidadesnegocio = new NacionalidadesNegocio();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                empleados = IEmpleadosSectoresnegocio!.Consultar();
                sectores = ISectoresnegocio!.Consultar();
                jefes = IJefesSectoresnegocio!.Consultar();
                nacionalidades = INacionalidadesnegocio!.Consultar();

                if (IEmpleadosSectoresnegocio == null)
                    return;
                empleado = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                empleado = empleados!.FirstOrDefault(x => x.Id == data);
                empleados = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (empleado == null)
                    return;
                else
                    empleado = IEmpleadosSectoresnegocio!.Modificar(empleado!);
                if (empleado.Id == 0)
                    return;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (empleado == null)
                    return;
                ViewData["Mensaje"] = IEmpleadosSectoresnegocio!.Eliminar(empleado!);
                empleado = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrarVal(int data)
        {
            OnPostBtRefrescar();
            try
            {
                empleado = empleados!.FirstOrDefault(x => x.Id == data);
                empleados = null;
                Borrando = true;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtCerrar()
        {
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}