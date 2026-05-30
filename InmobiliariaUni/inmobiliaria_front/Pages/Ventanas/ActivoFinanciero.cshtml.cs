using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inmobiliaria_front.Pages.Ventanas
{
    [Authorize(Roles = "Empleado")]
    public class ActivoFinancieroModel : PageModel
    {
        private IActivosFinancierosNegocio? IActivosFinancierosnegocio { get; set; }
        private IRespaldosFinancierosNegocio? IRespaldosFinancierosnegocio { get; set; }
        private IPersonasNegocio? IPersonasnegocio { get; set; }
        private IBienesNegocio? IBienesnegocio { get; set; }

        public List<ActivosFinancieros>? Lista { get; set; }
        public List<Personas>? personas { get; set; }
        public List<RespaldosFinancieros>? respaldos{ get; set; }
        [BindProperty] public ActivosFinancieros? Activo { get; set; }
        public bool Borrando { get; set; }

        public ActivoFinancieroModel()
        {
            IActivosFinancierosnegocio = new ActivosFinancierosNegocio();
            IRespaldosFinancierosnegocio = new RespaldosFinancierosNegocio();
            IBienesnegocio = new BienesNegocio();
            IPersonasnegocio = new PersonasNegocio();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void cargarListas()
        {
            respaldos = IRespaldosFinancierosnegocio!.Consultar();
            Lista = IActivosFinancierosnegocio!.Consultar();
            personas = IPersonasnegocio!.Consultar(); 
        }

        public void OnPostBtRefrescar()
        {
            cargarListas();
            try
            {
                if (IActivosFinancierosnegocio == null)
                    return;
                Lista = IActivosFinancierosnegocio.Consultar();
                Activo = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            cargarListas();
            Activo = new ActivosFinancieros()
            {
                FechaAdquisicion = DateTime.Now
            };
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                cargarListas();
                OnPostBtRefrescar();
                Activo = Lista!.FirstOrDefault(x => x.Id == data);
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
            cargarListas();
            try
            {
                //Validacion 
                if (Activo == null)
                    return;

                //Guardamos bien sea la entidad o la modificamos
                if (Activo.Id == 0)
                {
                    Activo = IActivosFinancierosnegocio!.Guardar(Activo!);
                }
                else
                    Activo = IActivosFinancierosnegocio!.Modificar(Activo!);
                if (Activo.Id == 0)
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
                if (Activo == null)
                    return;
                ViewData["Mensaje"] = IActivosFinancierosnegocio!.Eliminar(Activo!);
                Activo = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrarVal(int data)
        {
            cargarListas();
            OnPostBtRefrescar();
            try
            {
                Activo = Lista!.FirstOrDefault(x => x.Id == data);
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