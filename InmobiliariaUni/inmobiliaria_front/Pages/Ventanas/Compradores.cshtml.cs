using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inmobiliaria_front.Pages.Ventanas
{
    [Authorize(Roles = "Empleado")]
    public class CompradoresModel : PageModel
    {
        private ICompradoresNegocio? ICompradoresnegocio;
        private INacionalidadesNegocio? INacionalidadesnegocio;

        public List<Compradores>? Lista { get; set; }
        [BindProperty] public Compradores? Comprador { get; set; }
        public List<Nacionalidades>? listaNacionalidades { get; set; }
        public bool Borrando { get; set; }

        public CompradoresModel()
        {
            ICompradoresnegocio = new CompradoresNegocio();
            INacionalidadesnegocio = new NacionalidadesNegocio();
        }

        private void CargarListas()
        {
            listaNacionalidades = INacionalidadesnegocio!.Consultar();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = ICompradoresnegocio!.Consultar();
                CargarListas();
                Comprador = null;
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
                Comprador = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Comprador == null)
                    return;
                Comprador = ICompradoresnegocio!.Modificar(Comprador!);
                if (Comprador.Id == 0)
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
                if (Comprador == null)
                    return;
                ViewData["Mensaje"] = ICompradoresnegocio!.Eliminar(Comprador!);
                Comprador = null;
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
                Comprador = Lista!.FirstOrDefault(x => x.Id == data);
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