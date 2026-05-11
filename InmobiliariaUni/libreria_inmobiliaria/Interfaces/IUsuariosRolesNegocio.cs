using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IUsuariosRolesNegocio
    {
        List<UsuarioRoles> Consultar();
        string Eliminar(UsuarioRoles entidad);
        UsuarioRoles Modificar(UsuarioRoles entidad);
    }
}
