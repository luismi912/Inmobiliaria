using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IPropiedadesNegocio
    {
        List<Propiedades> Consultar();
        string Eliminar(int Id);
        Propiedades Modificar(Propiedades entidad);
        Propiedades Guardar(Propiedades entidad);
    }
}
