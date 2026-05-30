using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inmobiliaria_front.Pages.Ventanas
{
    [Authorize(Roles = "Empleado")]
    public class BienesModel : PageModel
    {
        private IBienesNegocio? IBienesnegocio;
        private IRespaldosFinancierosNegocio? IRespaldosFinancierosnegocio { get; set; }

        public List<Bienes>? Lista { get; set; }
        public List<RespaldosFinancieros>? ListaRespaldos{ get; set; }
        [BindProperty] public Bienes? Bien { get; set; }
        [BindProperty] public string? CedulaDelPropietario { get; set; }
        public bool Borrando { get; set; }

        public BienesModel()
        {
            IBienesnegocio = new BienesNegocio();
            IRespaldosFinancierosnegocio = new RespaldosFinancierosNegocio();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IBienesnegocio == null)
                    return;
                Lista = IBienesnegocio.Consultar();
                ListaRespaldos = IRespaldosFinancierosnegocio!.Consultar();
                Bien = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Bien = new Bienes()
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
                Bien = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Bien == null)
                    return;

                //Guardamos bien sea la entidad o la modificamos
                if (Bien.Id == 0)
                {
                    if (string.IsNullOrWhiteSpace(CedulaDelPropietario))
                        throw new Exception("La cedula del propietario es requerida");

                    //Mediante la cedula que pedimos buscamos el respaldo y le agregamos las observaciones ya añadidadas
                    var respaldo = IBienesnegocio!.ConsultarRespaldoCedula(CedulaDelPropietario!);

                    if (respaldo == null)
                        throw new Exception("No se encontro ningun respaldo para guardar la el bien, reintenta por favor");

                    Bien!.RespaldoFinanciero = respaldo.Id;

                    Bien = IBienesnegocio!.Guardar(Bien!);
                }
                else
                    Bien = IBienesnegocio!.Modificar(Bien!);
                if (Bien.Id == 0)
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
                if (Bien == null)
                    return;
                ViewData["Mensaje"] = IBienesnegocio!.Eliminar(Bien!);
                Bien = null;
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
                Bien = Lista!.FirstOrDefault(x => x.Id == data);
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