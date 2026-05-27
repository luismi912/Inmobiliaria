using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IUsuariosRolesNegocio
    {
        List<UsuariosRoles> Consultar();
        UsuariosRoles? ConsultarCorreo(string correo);
        string Eliminar(UsuariosRoles entidad);
        UsuariosRoles Modificar(UsuariosRoles entidad);
    }
}
