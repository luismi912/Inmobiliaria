using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inmobiliaria_front.Pages.Ventanas
{
    [Authorize(Roles = "Empleado")]
    public class CodeudoresModel : PageModel
    {
        private ICodeudoresNegocio? ICodeudoresnegocio;
        private INacionalidadesNegocio? INacionalidadesnegocio;
        private ICompradoresNegocio? ICompradoresnegocio;

        public List<Codeudores>? Lista { get; set; }
        [BindProperty] public Codeudores? Codeudor { get; set; }
        public List<Nacionalidades>? listaNacionalidades { get; set; }
        public List<Compradores>? listaCompradores { get; set; }
        public bool Borrando { get; set; }

        public CodeudoresModel()
        {
            ICodeudoresnegocio = new CodeudoresNegocio();
            INacionalidadesnegocio = new NacionalidadesNegocio();
            ICompradoresnegocio = new CompradoresNegocio();
        }

        private void CargarListas()
        {
            listaNacionalidades = INacionalidadesnegocio!.Consultar();
            listaCompradores = ICompradoresnegocio!.Consultar();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = ICodeudoresnegocio!.Consultar();
                CargarListas();
                Codeudor = null;
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
                Codeudor = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Codeudor == null)
                    return;
                Codeudor = ICodeudoresnegocio!.Modificar(Codeudor!);
                if (Codeudor.Id == 0)
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
                if (Codeudor == null)
                    return;
                ViewData["Mensaje"] = ICodeudoresnegocio!.Eliminar(Codeudor!);
                Codeudor = null;
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
                Codeudor = Lista!.FirstOrDefault(x => x.Id == data);
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