using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IDepartamentosNegocio
    {
        Departamentos Guardar(Departamentos entidad);
        List<Departamentos> Consultar();
        string Eliminar(Departamentos entidad);
        Departamentos Modificar(Departamentos entidad);
    }
}
