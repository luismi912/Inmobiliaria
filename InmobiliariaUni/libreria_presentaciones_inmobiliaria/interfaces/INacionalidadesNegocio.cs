using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface INacionalidadesNegocio
    {
        List<Nacionalidades> Consultar();
        Nacionalidades Guardar(Nacionalidades entidad);
        Nacionalidades Eliminar(Nacionalidades entidad);
        Nacionalidades Modificar(Nacionalidades entidad);
    }
}
