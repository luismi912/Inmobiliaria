using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IAdministradoresDepartamentosNegocio
    {
        List<AdministradoresDepartamentos> Consultar();
        string Eliminar(String Cedula);
        AdministradoresDepartamentos Modificar(AdministradoresDepartamentos entidad);
        string Guardar(CrearUsuariosAdministradoresDtos dto);
    }
}
