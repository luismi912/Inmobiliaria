using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface ITelefonosNegocio
    {
        List<Telefonos> Consultar(int Id);
        string Eliminar(Telefonos entidad);
        Telefonos Modificar(Telefonos entidad);
        Telefonos Guardar(Telefonos entidad);
    }
}
