using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inmobiliaria_front.Pages.Ventanas
{
    [Authorize(Roles = "Empleado")]
    public class ClientesModel : PageModel
    {
        private IClientesNegocio? IClientesnegocio;
        private INacionalidadesNegocio? INacionalidadesnegocio;
        private IEmpleadosSectoresNegocio? IEmpleadosSectoresnegocio;

        public List<Clientes>? Lista { get; set; }
        [BindProperty] public Clientes? Cliente { get; set; }
        public List<Nacionalidades>? listaNacionalidades { get; set; }
        public List<EmpleadosSectores>? listaEmpleados { get; set; }
        public bool Borrando { get; set; }

        public ClientesModel()
        {
            IClientesnegocio = new ClientesNegocio();
            INacionalidadesnegocio = new NacionalidadesNegocio();
            IEmpleadosSectoresnegocio = new EmpleadosSectoresNegocio();
        }

        private void CargarListas()
        {
            listaNacionalidades = INacionalidadesnegocio!.Consultar();
            listaEmpleados = IEmpleadosSectoresnegocio!.Consultar();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = IClientesnegocio!.Consultar();
                CargarListas();
                Cliente = null;
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
                Cliente = Lista!.FirstOrDefault(x => x.Id == data);
                CargarListas();
                Lista = null;
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
                if (Cliente == null)
                    return;
                Cliente = IClientesnegocio!.Modificar(Cliente!);
                if (Cliente.Id == 0)
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
                if (Cliente == null)
                    return;
                ViewData["Mensaje"] = IClientesnegocio!.Eliminar(Cliente!);
                Cliente = null;
                OnPostBtRefrescar();
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
                Cliente = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
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