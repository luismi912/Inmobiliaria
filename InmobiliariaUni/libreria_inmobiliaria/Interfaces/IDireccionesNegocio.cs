using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IDireccionesNegocio
    {
        List<Direcciones> Consultar(int Id);
        string Eliminar(int Id);
        Direcciones Modificar(Direcciones entidad);
        Direcciones Guardar(Direcciones entidad);
    }
}
