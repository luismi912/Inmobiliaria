using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface ISectoresNegocio
    {
        List<Sectores> Consultar();
        Sectores Guardar(Sectores entidad);
        string Eliminar(Sectores entidad);
        Sectores Modificar(Sectores entidad);
    }
}
