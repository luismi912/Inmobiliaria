using libreria_inmobiliaria.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using System.Collections;

namespace presentacion_aspnetcore.Pages
{
    public class AuditoriasModel : PageModel
    {
        private IAuditoriasNegocio? iAuditoriasNegocio;
        [BindProperty] public List<Auditorias>? Lista { get; set; }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public AuditoriasModel()
        {
            iAuditoriasNegocio = new AuditoriasNegocio();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iAuditoriasNegocio!.Consultar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }
    }
}