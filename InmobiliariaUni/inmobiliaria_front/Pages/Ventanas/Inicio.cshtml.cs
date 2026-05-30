using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inmobiliaria_front.Pages.Ventanas
{
    [Authorize(Roles = "Administrador,Jefe,Empleado")]
    public class InicioModel : PageModel
    {
       public void OnGet()
        {

        }
    }
}