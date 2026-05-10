using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface ISectoresNegocio
    {
        Sectores Guardar(Sectores entidad);
        List<Sectores> Consultar();
        string Eliminar(int Id);
        Sectores Modificar(Sectores entidad);
    }
}
