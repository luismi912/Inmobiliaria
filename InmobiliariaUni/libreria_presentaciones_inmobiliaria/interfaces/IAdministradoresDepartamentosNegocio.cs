using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface IAdministradoresDepartamentosNegocio
    {
        List<AdministradoresDepartamentos> Consultar();
        AdministradoresDepartamentos Guardar(CrearUsuariosAdministradoresDtos adminDto);
        AdministradoresDepartamentos Eliminar(AdministradoresDepartamentos entidad);
        AdministradoresDepartamentos Modificar(AdministradoresDepartamentos entidad);
    }
}
