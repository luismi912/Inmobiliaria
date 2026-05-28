using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface IUsuarioRolesNegocio
    {
        List<UsuariosRoles> Consultar();
        Task<UsuariosRoles>? ConsultarCorreo(string cedula);
        string Eliminar(UsuariosRoles entidad);
        UsuariosRoles Modificar(UsuariosRoles entidad);
    }
}
