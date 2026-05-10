using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IContratosContadosNegocio
    {
        ContratosContados Guardar(ContratosContados entidad);
        List<ContratosContados> Consultar();
        string Eliminar(int Id);
        ContratosContados Modificar(ContratosContados entidad);
    }
}
