using libreria_inmobiliaria.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;

namespace inmobiliaria_front.Pages.Administradores
{
    public class DepartamentosModel : PageModel
    {
        private IDepartamentosNegocio? IDepartamentoesnegocio;

        [BindProperty] public List<Departamentos>? Lista { get; set; }
        [BindProperty] public Departamentos? Departamento { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public DepartamentosModel()
        {
            IDepartamentoesnegocio = new DepartamentosNegocio();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IDepartamentoesnegocio == null)
                    return;
                Lista = IDepartamentoesnegocio.Consultar();
                Departamento = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Departamento = new Departamentos()
            {
                Estado = true
            };
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Departamento = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Departamento == null)
                    return;
                if (Departamento.Id == 0)
                    Departamento = IDepartamentoesnegocio!.Guardar(Departamento!);
                else
                    Departamento = IDepartamentoesnegocio!.Modificar(Departamento!);
                if (Departamento.Id == 0)
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
                if (Departamento == null)
                    return;
                ViewData["Mensaje"] = IDepartamentoesnegocio!.Eliminar(Departamento!);
                Departamento = null;
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
                Departamento = Lista!.FirstOrDefault(x => x.Id == data);
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