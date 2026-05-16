using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface IUsuarioRolesNegocio
    {
        List<UsuarioRoles> Consultar();
        string Eliminar(UsuarioRoles entidad);
        UsuarioRoles Modificar(UsuarioRoles entidad);
    }
}
