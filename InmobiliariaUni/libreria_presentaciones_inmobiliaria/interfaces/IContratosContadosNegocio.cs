using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface IContratosContadosNegocio
    {
        List<ContratosContados> Consultar();
        ContratosContados Guardar(ContratosContados entidad);
        string Eliminar(ContratosContados entidad);
        ContratosContados Modificar(ContratosContados entidad);
    }
}
