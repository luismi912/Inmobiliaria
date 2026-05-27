using libreria_inmobiliaria.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace presentacion_aspnetcore.Pages
{
    [Authorize(Roles = "Administrador")]
    public class AuditoriasModel : PageModel
    {
        private IAuditoriasNegocio? IAuditoriasnegocio;
        [BindProperty] public List<Auditorias>? Lista { get; set; }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public AuditoriasModel()
        {
            IAuditoriasnegocio = new AuditoriasNegocio();
        }

        public void OnPostBtRefrescar()
        {
            Lista = IAuditoriasnegocio!.Consultar();
        }
    }
}