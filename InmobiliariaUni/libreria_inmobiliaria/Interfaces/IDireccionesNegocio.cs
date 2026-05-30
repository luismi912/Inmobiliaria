using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IDireccionesNegocio
    {
        List<Direcciones> Consultar();
        string Eliminar(Direcciones entidad);
        Direcciones Modificar(Direcciones entidad);
        Direcciones Guardar(Direcciones entidad);
    }
}
