using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IUsuariosRolesNegocio
    {
        List<UsuarioRoles> Consultar();
        string Eliminar(int Id);
        UsuarioRoles Modificar(UsuarioRoles entidad);
    }
}
