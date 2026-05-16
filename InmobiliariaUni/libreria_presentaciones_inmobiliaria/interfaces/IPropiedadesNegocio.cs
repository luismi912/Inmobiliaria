using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface IPropiedadesNegocio
    {
        List<Propiedades> Consultar();
        Propiedades Guardar(Propiedades entidad);
        string Eliminar(Propiedades entidad);
        Propiedades Modificar(Propiedades entidad);
    }
}
