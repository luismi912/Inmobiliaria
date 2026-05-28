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
        private IBienesNegocio? IBienesnegocio { get; set; }

        public List<ActivosFinancieros>? Lista { get; set; }
        public List<RespaldosFinancieros>? ListaRespaldos{ get; set; }
        [BindProperty] public ActivosFinancieros? Activo { get; set; }
        [BindProperty] public string? CedulaDelPropietario { get; set; }
        public bool Borrando { get; set; }

        public ActivoFinancieroModel()
        {
            IActivosFinancierosnegocio = new ActivosFinancierosNegocio();
            IRespaldosFinancierosnegocio = new RespaldosFinancierosNegocio();
            IBienesnegocio = new BienesNegocio();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IActivosFinancierosnegocio == null)
                    return;
                Lista = IActivosFinancierosnegocio.Consultar();
                ListaRespaldos = IRespaldosFinancierosnegocio!.Consultar();
                Activo = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
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
            try
            {
                //Validacion 
                if (Activo == null)
                    return;

                //Guardamos bien sea la entidad o la modificamos
                if (Activo.Id == 0)
                {
                    if (string.IsNullOrWhiteSpace(CedulaDelPropietario))
                        throw new Exception("La cedula del propietario es requerida");

                    //Mediante la cedula que pedimos buscamos el respaldo y le agregamos las observaciones ya añadidadas
                    //Aunque el consultar este en bienesnegocio, este tambien sirve para activos financieros
                    var respaldo = IBienesnegocio!.ConsultarRespaldoCedula(CedulaDelPropietario!);  

                    if (respaldo == null)
                        throw new Exception("No se encontro ningun respaldo para guardar la el bien, reintenta por favor");

                    Activo!.RespaldoFinanciero = respaldo.Id;

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