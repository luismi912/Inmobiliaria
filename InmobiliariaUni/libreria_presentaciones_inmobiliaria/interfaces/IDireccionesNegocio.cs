using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface IDireccionesNegocio
    {
        List<Direcciones> Consultar();
        Direcciones Guardar(Direcciones entidad);
        string Eliminar(Direcciones entidad);
        Direcciones Modificar(Direcciones entidad);
    }
}
