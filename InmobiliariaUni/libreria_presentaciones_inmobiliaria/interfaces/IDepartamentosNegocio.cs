using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface IDepartamentosNegocio
    {
        List<Departamentos> Consultar();
        Departamentos Guardar(Departamentos entidad);
        string Eliminar(Departamentos entidad);
        Departamentos Modificar(Departamentos entidad);
    }
}
