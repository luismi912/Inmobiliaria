using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface IContratosArriendosNegocio
    {
        List<ContratosArriendos> Consultar();
        ContratosArriendos Guardar(ContratosArriendos entidad);
        string Eliminar(ContratosArriendos entidad);
        ContratosArriendos Modificar(ContratosArriendos entidad);
    }
}
