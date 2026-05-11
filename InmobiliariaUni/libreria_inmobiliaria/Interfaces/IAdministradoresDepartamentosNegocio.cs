using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IAdministradoresDepartamentosNegocio
    {
        List<AdministradoresDepartamentos> Consultar();
        string Eliminar(AdministradoresDepartamentos entidad);
        AdministradoresDepartamentos Modificar(AdministradoresDepartamentos entidad);
        AdministradoresDepartamentos Guardar(CrearUsuariosAdministradoresDtos dto);
    }
}
