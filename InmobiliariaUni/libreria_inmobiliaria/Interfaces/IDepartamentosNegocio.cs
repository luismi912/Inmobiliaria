using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IDepartamentosNegocio
    {
        Departamentos Guardar(Departamentos entidad);
        List<Departamentos> Consultar();
        string Eliminar(int Id);
        Departamentos Modificar(Departamentos entidad);
    }
}
