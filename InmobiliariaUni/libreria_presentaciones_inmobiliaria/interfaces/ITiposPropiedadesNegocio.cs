using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface ITiposPropiedadesNegocio
    {
        List<TiposPropiedades> Consultar();
        TiposPropiedades Guardar(TiposPropiedades entidad);
        string Eliminar(TiposPropiedades entidad);
        TiposPropiedades Modificar(TiposPropiedades entidad);
    }
}
